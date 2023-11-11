namespace Effects.Tests

open System
open System.Text.Json
open NodaTime
open Expecto

module Common =

    let unit = ()

    let url = Uri "https://fakeurl.com"

    let expectedGuid = Guid.NewGuid()

    let expectedTime = SystemClock.Instance.GetCurrentInstant()

    let expectedJson = JsonDocument.Parse """[ { "text": "Cat fact" } ]"""

module Expect =

    let equal actual expected =
        $"Values are not equal: {actual} - {expected}."
        |> Expect.equal actual expected
