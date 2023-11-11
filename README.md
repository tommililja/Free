A free monad example in F#

```fsharp
module CatFacts =

    let private fromJson = JsonSerializer.deserialize<CatFacts>

    let getAsync url = effectAsync {

        let! time = EffectAsync.getTime ()

        do! EffectAsync.log $"Request to {url} at {time}."

        let! facts =
            url
            |> EffectAsync.getJson
            |> EffectAsync.map fromJson

        do! EffectAsync.log $"Retrieved {facts.Length} cat facts."

        return facts
    }

```
