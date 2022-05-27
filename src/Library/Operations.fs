namespace Library
module Operations = 

    open Domain
    open System
    open Auditing

    let deposit (amount: decimal) (account: Account): Account = 
        { account with Balance = account.Balance + amount}


    let myPersonalAccount = {AccountId = Guid.NewGuid(); Owner = { Name = "Nathan"}; Balance = 10M }

    let withdraw (amount: decimal) (account: Account) = 
        match amount with
        | amount when amount > account.Balance -> account 
        | amount when amount <= account.Balance -> { account with Balance = account.Balance - amount}
        | _ -> account

    let betterWithdraw (amount: decimal) (account: Account) = 
        match amount > account.Balance with
        | true -> account 
        | false -> { account with Balance = account.Balance - amount}


    let isRejected (previousAccount: Account) (newAccount: Account) = 
                            if previousAccount.Balance = newAccount.Balance 
                            then "rejected"
                            else "accepted"

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

    let easyCreate = createAccount 
                                ("Please print your Name: ") 
                                ("What is your starting Balance: ") 


    let withdrawWithConsoleAudit (amount: decimal) (account: Account) = 
            auditAs "withdraw" 
                    consoleMessage 
                    withdraw 
                    amount 
                    account

    let depositWithConsoleAudit (amount: decimal) (account: Account) = 
            auditAs "deposit"
                    consoleMessage 
                    deposit 
                    amount 
                    account



// let withdrawWithConsoleAudit = 