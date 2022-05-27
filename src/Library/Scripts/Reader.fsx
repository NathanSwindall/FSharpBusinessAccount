#load "Logging.fs"
#load "Logger2.fs"
#load "Reader.fs"

open Loggers.MyApplication
open Logging
open MyReader

logger.LogInfo "Peopl"
logError "This is an error string"
let loggingStuff = myLog 2 "Hello"

Logging.myLog 0 "I am going to the park"


type ComparisonResult = 
    | Bigger 
    | Smaller 
    | Equal

let compareTwoStrings (logger: Logging.ILogger) str1 str2 =
    logger.Log 0 "compareTwoStrings: Starting"

    let result = 
        if str1 > str2 then 
            Bigger 
        else if str1 < str2 then 
            Smaller 
        else 
            Equal

    // logger.Log 2 (sprintf "compareTwoStrings: result =%A" result)
    logger.Log 3 "compareTwoStrings: finished"
    result

compareTwoStrings Logging.ConsoleLogger "Nathan" "Swindall"

Logging.MyPrinter.Print "Hello from my printer"
Logging.ConsoleLogger.Debug "My friend right here"

type StringComparisons(logger: Logging.ILogger) =
    member __.CompaeTwoStrings str1 str2 = 
        logger.Debug


let compareTwoStringsFP str1 str2 (logger: ILogger) = 
    logger.Debug "compareTwoStrings: Starting"

    let result = 
        if str1 > str2 then 
            Bigger 
        else if str1 < str2 then 
            Smaller 
        else 
            Equal 
    let myinfo = sprintf "compareTwoStrings: result=%A" result
    logger.Info "comapreTwoStrings: result=%A" result
    result

compareTwoStringsFP "Nathan" "Swindall" ConsoleLogger


let doAfter s = 
    printfn "Done"
    s

let result = Printf.ksprintf doAfter "%s" "Hello"

type Reader<'env,'a> = Reader of actions:('env -> 'a)

let compareTwoStringsReader str1 str2 : Reader<ILogger, ComparisonResult> = 
    fun (logger: ILogger) -> 
        logger.Debug "compareTwoStrings: Starting"

        let result = 
            if str1 > str2 then 
                Bigger 
            else if str1 < str2 then 
                Smaller 
            else 
                Equal 
        
        logger.Info "compareTwoStrings: result=%A" result
        logger.Debug ("compareTwoStrings: Finisehd")
        result
    |> Reader


let myResult = compareTwoStringsReader "nathan" "Swindall"
//let runResult = run myResult
let (Reader insideReader) = myResult
let workedOutFunction = insideReader ConsoleLogger




