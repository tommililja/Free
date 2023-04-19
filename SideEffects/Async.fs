namespace SideEffects.FSharp

module Async =
    
    let ret x = async.Return x
  
    let bind f x = async {
        let! x = x
        return! f x
    }
    
    let map f = bind (f >> ret)
    