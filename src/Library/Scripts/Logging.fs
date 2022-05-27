

module Logging 

    open System 

    let Error = 0 
    let Warning = 1 
    let Information = 3 
    let Debug = 3 

    type Reader<'env,'a> = Reader of actions:('env -> 'a)
    let LevelToString level = 
        match level with 
            | 0 -> "Error"
            | 1 -> "Warning"
            | 2 -> "Information"
            | 3 -> "Debug"
            | _ -> "Unknown"

    let mutable current_log_level = Debug 

    type ILogger = 
        abstract Log : int -> Printf.StringFormat<'a,unit> -> 'a
        abstract Debug: Printf.StringFormat<'a,unit> -> 'a
        abstract Info: Printf.StringFormat<'a,unit> -> 'a

    type IPrint = 
        abstract Print : string -> unit 

    type JustType() = 

        member __.add x y = 
            x + y

    
    let MyPrinter: IPrint = {
        new IPrint with 
            member __.Print message = 
                printfn "Hello buddy"
    }

    let ConsoleLogger = {
        new ILogger with 
            member __.Log level format = 
                Printf.kprintf (printfn "[%s][%A] %s" (LevelToString level) System.DateTime.Now) format

            member __.Debug format = 
                Printf.kprintf (printfn "[%s][%A] %s" ("Debug") System.DateTime.Now) format

            member __.Info format = 
                Printf.kprintf (printfn "[%s][%A] %s" ("Information") System.DateTime.Now) format
    }

    // Defines which loggger to use
    let mutable DefaultLogger = ConsoleLogger 

    // Logs a message with the specified logger
    let logUsing (logger: ILogger) = logger.Log

    /// Logs a message using the default logger 
    let myLog level message = logUsing DefaultLogger level message


