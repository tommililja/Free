namespace SideEffects.Api

open SideEffects.Monad

module CatFacts =

    open EffectAsync

    let getAsync url = effectAsync {
        
        let! time = getTime ()
      
        do! log $"Request to {url} at {time}."
        
        let! catFacts =
            url
            |> EffectAsync.getJson
            |> EffectAsync.map CatFact.fromJson

        do! log $"Retreived {catFacts.Length} cat facts."
        
        return catFacts
    }
    