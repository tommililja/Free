module CatFactsTests

open SideEffects.Api
open SideEffects.Monad
open SideEffects.Tests
open System.Text.Json
open Xunit
open Expecto

let private interpreter =
    TestData.response
    |> JsonDocument.Parse
    |> TestData.Interpreter.withResponse

[<Fact>]
let ``getAsync should return cat facts`` () =
    
    let facts =
        CatFacts.getAsync TestData.url
        |> SideEffect.map Async.RunSynchronously
        |> SideEffect.handle interpreter
    
    let first = List.head facts
    
    "facts.Length = 1"
    |> Expect.equal facts.Length 1
    
    "first.Text = Cat Facts"
    |> Expect.equal first.Text "Cat fact"
    