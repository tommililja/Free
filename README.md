Free monad example in F# to handle side effects and dependencies.

```fsharp

module CatFacts =

  let getFactsAsync url = sideEffect {

      let! time = SideEffect.getTime ()

      do! SideEffect.log $"Request to {url} at {time}."

      let! response =
          url
          |> SideEffect.httpRequest
          |> SideEffectAsync.map JsonSerializer.deserialize<CatFacts>

      return response
  }
```
