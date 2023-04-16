namespace SideEffects.FSharp

module SideEffectAsync =
    
    let bind fn effect : SideEffectAsync<_> =
        effect
        |> SideEffect.map (Async.bind fn)
    
    let map fn effect : SideEffectAsync<_> =
        effect
        |> SideEffect.map (Async.map fn)
    