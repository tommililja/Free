namespace SideEffects.FSharp

open System
open Newtonsoft.Json

type CatFact = {
    Text: String
}

and CatFacts = CatFact list

module CatFacts =
    
    let private deserialize = JsonConvert.DeserializeObject<CatFacts>
    
    let getFacts url =
        url
        |> SideEffect.http
        |> SideEffectAsync.map deserialize
        
    let getFactsFrom url : SideEffectAsync<_> = sideEffect {
        
        let! guid = SideEffect.createGuid ()
        let! time = SideEffect.getTime ()
        
        do! SideEffect.log $"Generated guid {guid} at {time}."
        
        let! response =
            url
            |> SideEffect.http
            |> SideEffectAsync.map deserialize
        
        return response
    }
    