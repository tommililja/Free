namespace SideEffects.FSharp

module SideEffectAsync =

    let bind f = SideEffect.map (Async.bind f)
    
    let map f = bind (f >> Async.ret)
    