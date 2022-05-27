namespace MyReader

type Reader<'env,'a> = Reader of actions:('env -> 'a)

module Reader = 
  /// Run a Reader with a given environment
  let run env (Reader action)  =
    action env  // simply call the inner function

  /// Create a Reader which returns the environment itself
  let ask = Reader id

  /// Map a function over a Reader
  let map f reader =
    Reader (fun env -> f (run env reader))

  /// flatMap a function over a Reader
  let bind f reader =
    let newAction env =
      let x = run env reader
      run env (f x)
    Reader newAction