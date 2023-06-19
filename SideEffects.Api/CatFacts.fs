namespace SideEffects.Api

open System
open SideEffects.Monad

type CatFact = {
    Text: String
}

type CatFacts = CatFact list

module CatFacts =

    open SideEffectAsync
    
    let private fromJson = JsonSerializer.deserialize<CatFacts>
    
    let getAsync url = sideEffectAsync {
        
        let! time = getTime ()
      
        do! log $"Request to {url} at {time}."
        
        let! catFacts =
            url
            |> SideEffectAsync.getJson
            |> SideEffectAsync.map fromJson

        do! log $"Retreived {catFacts.Length} cat facts."
        
        return catFacts
    }
    