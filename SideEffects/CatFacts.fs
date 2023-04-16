namespace SideEffects.FSharp

open System
open Newtonsoft.Json

type CatFact = {
    Text: String
}

module CatFacts =
    
    let private deserialize = JsonConvert.DeserializeObject<CatFact list>
    
    let getFacts url =
        url
        |> SideEffect.httpRequest
        |> SideEffectAsync.map deserialize
        
    let getFactsFrom str = sideEffect {
        
        let! guid = SideEffect.guid ()
        let! date = SideEffect.date ()
        
        do! SideEffect.log $"Generated guid {guid} at {date}."
        
        let! response =
            str
            |> SideEffect.httpRequest
            |> SideEffectAsync.map deserialize
        
        return response
    }
    