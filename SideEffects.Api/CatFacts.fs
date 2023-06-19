namespace SideEffects.Api

open SideEffects.Monad

type CatFact = {
    Text: string
}

type CatFacts = CatFact list

module CatFacts =

    open EffectAsync
    
    let private fromJson = JsonSerializer.deserialize<CatFacts>
    
    let getAsync url = effectAsync {
        
        let! time = getTime ()
      
        do! log $"Request to {url} at {time}."
        
        let! catFacts =
            url
            |> EffectAsync.getJson
            |> EffectAsync.map fromJson

        do! log $"Retreived {catFacts.Length} cat facts."
        
        return catFacts
    }
    