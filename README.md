A free monad example in F# to handle side effects and dependencies.

```fsharp

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
```
