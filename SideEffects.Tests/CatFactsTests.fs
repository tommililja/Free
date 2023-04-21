module CatFactsTests

open Expecto
open SideEffects.Api
open SideEffects.Monad
open SideEffects.Tests
open System
open System.Text.Json
open Xunit

let private interpreter =
    """[ { "text": "Cat fact" } ]"""
    |> JsonDocument.Parse
    |> TestInterpreter.withResponse

[<Fact>]
let ``getFactsAsync should return cat facts`` () =
    
    let url = Uri "http://domain.com"

    let catFacts =
        CatFacts.getAsync url
        |> SideEffect.map Async.RunSynchronously
        |> SideEffect.handle interpreter
    
    let first = List.head catFacts
    
    "catFacts.Length = 1"
    |> Expect.equal catFacts.Length 1
    
    "first.Text = Cat Facts"
    |> Expect.equal first.Text "Cat fact"
    