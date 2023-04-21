namespace SideEffects.Monad

module Async =
    
    let ret x = async.Return x
  
    let bind f x = async.Bind (x, f)
    
    let map f = bind (f >> ret)
    