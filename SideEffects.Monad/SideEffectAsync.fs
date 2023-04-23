namespace SideEffects.Monad

module SideEffectAsync =

    let bind fn = SideEffect.map (Async.bind fn)
    
    let map fn = bind (fn >> Async.ret)
    