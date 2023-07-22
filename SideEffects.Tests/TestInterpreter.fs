namespace SideEffects.Tests

open System
open System.Text.Json
open SideEffects.Monad
open NodaTime

module TestInterpreter =

    open Common

    let private returnWith x = fun _ -> x

    let private getJson : Uri -> JsonDocument Async =
        expectedJson
        |> Async.ret
        |> returnWith

    let def = {
        Log = Console.WriteLine
        CreateGuid = Guid.NewGuid
        GetTime = SystemClock.Instance.GetCurrentInstant
        GetJson = getJson
    }

    let withGuid guid = {
        def with CreateGuid = returnWith guid
    }

    let withTime time = {
        def with GetTime = returnWith time
    }

    let withGuidAndTime guid time = {
        def with
            CreateGuid = returnWith guid
            GetTime = returnWith time
    }
