namespace SideEffects.FSharp

open System
open Newtonsoft.Json

type CatFact = {
    Text: String
}

and CatFacts = CatFact list

module CatFacts =
    
    let private deserialize = JsonConvert.DeserializeObject<CatFacts>

    let getFacts url : SideEffectAsync<_> = sideEffect {
        
        let! time = SideEffect.getTime ()
        
        do! SideEffect.log $"Request to {url} at {time}."
        
        let! response =
            url
            |> SideEffect.httpRequest
            |> SideEffectAsync.map deserialize
        
        return response
    }
    