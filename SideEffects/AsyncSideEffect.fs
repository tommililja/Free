namespace SideEffects.FSharp

module SideEffectAsync =
    
    let map fn effect : SideEffectAsync<_> =
        SideEffect.map (Async.map fn) effect
    
    let bind fn effect : SideEffectAsync<_> =
        SideEffect.map (Async.bind fn) effect
    