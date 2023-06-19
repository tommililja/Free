namespace SideEffects.Api

open SideEffects.Monad

module CatFacts =

    open EffectAsync

    let getAsync url = effectAsync {
        
        let! time = getTime ()
      
        do! log $"Request to {url} at {time}."
        
        let! facts =
            url
            |> EffectAsync.getJson
            |> EffectAsync.map CatFact.fromJson

        do! log $"Retreived {facts.Length} cat facts."
        
        return facts
    }
    