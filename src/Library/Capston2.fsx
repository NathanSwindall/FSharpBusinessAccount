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

let myAccount: Account =  {AccountId = Guid.NewGuid(); Owner = { Name = "Nathan"}; Balance = 10M }

/// Deposits an amount into an account 
let deposit (amount: decimal) (account: Account): Account =  // Account is last argument in order to pipe it. 
    {AccountId = Guid.NewGuid(); Owner = { Name = "Nathan"}; Balance = 10M }

let withdraw (amount: decimal) (account: Account) = 
    match amount with
    | amount when amount > account.Balance -> account 
    | amount when amount <= account.Balance -> { account with Balance = account.Balance - amount}


let runDeposit = deposit (10M) myAccount
let runWithdraw: Account = withdraw (10M) myAccount


// creating pluggable audi function 
// let fileSystemAudit account message = 
// let console account message = 

let consoleMessage (account: Account) (message: string) = 
    let startMessage = sprintf "Account %A :" account.AccountId
    startMessage + message

let testConsole = consoleMessage myAccount "Testing console audit"

