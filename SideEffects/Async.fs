namespace SideEffects.FSharp

module Async =
    
    let ret x = async.Return x
 
    let map fn x = async {
        let! x = x
        return fn x
    }
    
    let bind fn x = async {
        let! x = x
        return! fn x
    }
    