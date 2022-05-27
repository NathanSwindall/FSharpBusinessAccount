namespace Library

module Domain =

    open System


    type Customer = { 
        Name: string
    }


    type Account = {
        AccountId: Guid 
        Owner: Customer
        Balance: decimal
    }


    type AuditOperation = 
            | ConsoleOp 
            | FileSystemOp