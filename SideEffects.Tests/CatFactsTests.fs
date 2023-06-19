namespace SideEffects.Tests

open SideEffects.Api
open SideEffects.Monad
open SideEffects.Tests
open Xunit

module CatFactsTests =

    open Common
    
    [<Fact>]
    let ``getAsync should return cat facts`` () =
         
        let interpreter = TestInterpreter.def
        
        let facts = 
            url
            |> CatFacts.getAsync
            |> SideEffectAsync.handle interpreter
            |> Async.RunSynchronously
        
        let first = List.head facts
        
        Expect.equal facts.Length 1
        Expect.equal first.Text "Cat fact"
        