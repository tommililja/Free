namespace SideEffects.Api

open SideEffects.Monad

type CatFact = {
    Text: string
}

and CatFacts = CatFact list

module CatFacts =

    open EffectAsync

    let private fromJson = JsonSerializer.deserialize<CatFacts>
    
    let getAsync url = effectAsync {
        
        let! time = getTime ()
      
        do! log $"Request to {url} at {time}."
        
        let! facts =
            url
            |> EffectAsync.getJson
            |> EffectAsync.map fromJson

        do! log $"Retrieved {facts.Length} cat facts."
        
        return facts
    }
    