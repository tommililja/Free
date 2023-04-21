namespace SideEffects.Api

open System
open SideEffects.Monad

type CatFact = {
    Text: String
}

and CatFacts = CatFact list

module CatFacts =

    let private fromJson = JsonSerializer.deserialize<CatFacts>
    
    let getAsync url : SideEffectAsync<_> = sideEffect {
        
        let! time = SideEffect.getTime ()
        
        do! SideEffect.log $"Request to {url} at {time}."
        
        let! response =
            url
            |> SideEffect.httpRequest
            |> SideEffectAsync.map fromJson
        
        return response
    }
    