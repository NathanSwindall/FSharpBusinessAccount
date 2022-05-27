open System
// This is just an interactive file to test out function for our library

type Customer = { 
    Name: string
}
type Account_ = { customerName: string 
                  currentBalance: decimal
                  id: Guid
                  customer: Customer}

type Account = {
    AccountId: Guid 
    Owner: Customer
    Balance: decimal

}


type AuditOperation = 
        | ConsoleOp 
        | FileSystemOp

let myAccount: Account =  {AccountId = Guid.NewGuid(); Owner = { Name = "Nathan"}; Balance = 10M }

/// Deposits an amount into an account 
let deposit_ (amount: decimal) (account: Account): Account =  // Account is last argument in order to pipe it. 
    {AccountId = Guid.NewGuid(); Owner = { Name = "Nathan"}; Balance = 10M }

let deposit (amount: decimal) (account: Account): Account = 
    { account with Balance = account.Balance + amount}

let withdraw (amount: decimal) (account: Account) = 
    match amount with
    | amount when amount > account.Balance -> account 
    | amount when amount <= account.Balance -> { account with Balance = account.Balance - amount}
    | _ -> account


let runDeposit = deposit (10M) myAccount
let runWithdraw: Account = withdraw (10M) myAccount


// creating pluggable audi function 
// let fileSystemAudit account message = 
// let console account message = 

let consoleMessage_ (account: Account) (message: string) = 
    let startMessage = sprintf "Account %A :" account.AccountId
    printf "%s" (startMessage + message)

let consoleMessage (account: Account) (message: string) (amount: decimal) = 
    let startMessage = sprintf "Account %A : Performing a %A operation for $%A..." account.AccountId message amount
    System.Console.WriteLine(startMessage)

let consoleMessage2 (account: Account) (message: string) (amount: decimal) = 
    let startMessage = sprintf "Account %A : Performing a %A operation for $%A..." account.AccountId message amount
    System.Console.WriteLine(startMessage)

let testConsole = consoleMessage myAccount "Testing console audit"


let auditAs_ (operationName: AuditOperation) (audit: Account -> string -> unit) 
            (operation: decimal -> Account -> Account) 
            (amount: decimal)
            (account: Account): Account = 
            match operationName with 
            | ConsoleOp -> 
                operation amount account
            | FileSystemOp -> 
                operation amount account


let auditAs1 (operationName: string) (audit: Account -> string -> unit) 
             (operation: decimal -> Account -> Account) 
             (amount: decimal)
             (account: Account): Account = 
                    audit account operationName 
                    operation amount account



let createAccount (getNameMessage: string) (getBalanceMessage: string): Account = 
    printf "%s" getNameMessage
    let name = System.Console.ReadLine()
    printf "%s" getBalanceMessage
    let balanceString = System.Console.ReadLine()
    let balanceNumber = Decimal.Parse balanceString
    {   AccountId = Guid.NewGuid()
        ;Owner = {Name = name}
        ;Balance = balanceNumber
    }

let consoleSuccessOrFail (account) (performed: string) (amount: decimal) = 
    let performedMessage = sprintf "Account %A: Transaction %A! Balance is now $%A..." account.AccountId performed amount
    System.Console.WriteLine(performedMessage)

let isRejected (previousAccount: Account) (newAccount: Account) = 
                        if previousAccount.Balance = newAccount.Balance 
                        then "rejected"
                        else "accepted"


let auditAs (operationName: string) (audit: Account -> string -> decimal -> unit) 
            (operation: decimal -> Account -> Account) 
            (amount: decimal)
            (account: Account)
            (performed: string): Account = 
                audit account operationName 
                let myAccount = operation amount account
                consoleSuccessOrFail account performed amount
                myAccount

let easyCreate = createAccount 
                            ("Please print your Name: ") 
                            ("What is your starting Balance: ") 


let withdrawWithConsoleAudit (amount: decimal) (peformed:string) (account: Account) = 
        auditAs "withdraw" 
                consoleMessage2 
                withdraw 
                amount 
                account
                peformed

let betterWithdraw (amount: decimal) (account: Account) = 
        match amount > account.Balance with
        | true -> account 
        | false -> { account with Balance = account.Balance - amount}

// decorated versions of the