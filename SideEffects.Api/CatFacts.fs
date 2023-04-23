namespace SideEffects.Api

open System
open SideEffects.Monad

type CatFact = {
    Text: String
}

and CatFacts = CatFact list

module CatFacts =

    let private fromJson = JsonSerializer.deserialize<CatFacts>
    
    let getAsync url = sideEffect {
        
        let! time = SideEffect.getTime ()
        
        do! SideEffect.log $"Request to {url} at {time}."
        
        let! facts =
            url
            |> SideEffect.httpRequest
            |> SideEffectAsync.map fromJson
        
        return facts
    }
    