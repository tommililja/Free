namespace SideEffects.Api

open SideEffects.Monad

type CatFact = {
    Text: string
}

and CatFacts = CatFact list

module CatFacts =

    open EffectAsync
    open JsonSerializer

    let getAsync url = effectAsync {

        let! time = getTime ()

        do! log $"Request to {url} at {time}."

        let! facts =
            url
            |> EffectAsync.getJson
            |> EffectAsync.map deserialize<CatFacts>

        do! log $"Retrieved {facts.Length} cat facts."

        return facts
    }
