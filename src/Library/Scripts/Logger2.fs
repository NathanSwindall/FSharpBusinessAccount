namespace Loggers

module Logger2 =

    open System 
    open System.IO 

    // a logging library such as Log4net 
    // or System.Diagnostics.Trace 

    type Logger(name) = 

        let currentTime (tw: TextWriter) = 
            tw.Write("{0:s}", DateTime.Now) 

        let logEvent level msg = 
            printfn "%t %s [%s] %s" currentTime level name msg 

        member this.LogInfo msg =
            logEvent "INFO" msg 

        member this.LogError msg = 
            logEvent "ERROR" msg 
        
        static member CreateLogger name = 
            new Logger(name)

    
module MyApplication =

    open Logger2
    open type Logger2.Logger

    let logger = Logger.CreateLogger("MyApp")
    let logger2 = CreateLogger("MyApp2")

    // create a LogError using the Logger class 
    let logError format = 
        let doAfter s = 
            logger.LogError(s)
        Printf.ksprintf doAfter format

    let logInfo format = 
        let doAfter s = 
            logger.LogInfo(s)
        Printf.ksprintf doAfter format

    // function to exercise the Logging 
    // let test() = 
    //     do logger.LogInfo "Message @%i" 1 
    //     do logger.LogInfo "Message #%i" 2
    //     do logger.LogError "Oops! an error occurred in my app"