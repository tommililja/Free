A free monad example in F#

```fsharp
module CatFacts =

    open EffectAsync

    let private fromJson = JsonSerializer.deserialize<CatFacts>

    let getAsync url = effectAsync {

        let! time = getTime ()

        do! log $"Request to {url} at {time}."

        let! facts =
            url
            |> getJson
            |> map fromJson

        do! log $"Retrieved {facts.Length} cat facts."

        return facts
    }

```
