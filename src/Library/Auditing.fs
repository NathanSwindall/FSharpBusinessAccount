namespace Library

module Auditing=

    open Domain

    // let auditAs (operationName: string) (audit: Account -> string -> unit) 
    //             (operation: decimal -> Account -> Account) 
    //             (amount: decimal)
    //             (account: Account): Account = 

    let consoleSuccessOrFail (account) (performed: string) (amount: decimal) = 
        let performedMessage = sprintf "Account %A: Transaction %A! Balance is now $%A..." account.AccountId performed amount
        System.Console.WriteLine(performedMessage)

    let auditAs (operationName: string) (audit: Account -> string -> unit) 
                (operation: decimal -> Account -> Account) 
                (amount: decimal)
                (account: Account)
                (performed: string): Account = 
                    audit account operationName 
                    let myAccount = operation amount account
                    consoleSuccessOrFail account performed amount
                    myAccount

    let auditAs_ (operationName: string) (audit: Account -> string -> decimal-> unit) 
                (operation: decimal -> Account -> Account) 
                (amount: decimal)
                (account: Account): Account = 
                        audit account operationName 
                        operation amount account

                    

    let consoleMessage (account: Account) (message: string) = 
        let startMessage = sprintf "Account %A : " account.AccountId
        printf "%s" (startMessage + message)


    let consoleMessage_ (account: Account) (message: string) (amount: decimal) = 
        let startMessage = sprintf "Account %A : Performing a %A operation for $%A..." account.AccountId message amount
        System.Console.WriteLine(startMessage)


