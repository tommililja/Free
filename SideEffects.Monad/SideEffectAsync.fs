namespace SideEffects.Monad

type 'a SideEffectAsync = 'a Async SideEffect

module SideEffectAsync =

    let ret x = SideEffect.ret (Async.ret x)
    
    let bind fn = SideEffect.map (Async.bind fn)
    
    let map fn = bind (fn >> Async.ret)
    