namespace Effects.Tests

open Effects.Monad
open Effects.Tests
open Xunit

module EffectAsyncTests =

    open Common

    [<Fact>]
    let ``bind should bind value correctly`` () = async {

        let interpreter =
            TestInterpreter.withGuidAndTime
                expectedGuid
                expectedTime

        let bind guid =
            Expect.equal guid expectedGuid
            EffectAsync.getTime unit

        let! actualTime =
            unit
            |> EffectAsync.createGuid
            |> EffectAsync.bind bind
            |> EffectAsync.handle interpreter

        Expect.equal actualTime expectedTime
    }

    [<Fact>]
    let ``map should map value correctly`` () = async {

        let expectedString = string expectedGuid

        let interpreter =
            expectedGuid
            |> TestInterpreter.withGuid

        let! actualString =
            unit
            |> EffectAsync.createGuid
            |> EffectAsync.map string
            |> EffectAsync.handle interpreter

        Expect.equal actualString expectedString
    }

    [<Fact>]
    let ``handle should interpret createGuid correctly`` () = async {

        let interpreter =
            expectedGuid
            |> TestInterpreter.withGuid

        let! actualGuid =
            unit
            |> EffectAsync.createGuid
            |> EffectAsync.handle interpreter

        Expect.equal actualGuid expectedGuid
    }

    [<Fact>]
    let ``handle should interpret getTime correctly`` () = async {

        let interpreter =
            expectedTime
            |> TestInterpreter.withTime

        let! actualTime =
            unit
            |> EffectAsync.getTime
            |> EffectAsync.handle interpreter

        Expect.equal actualTime expectedTime
    }

    [<Fact>]
    let ``handle should interpret getJson correctly`` () = async {

        let interpreter = TestInterpreter.defaultInterpreter

        let! actualJson =
            url
            |> EffectAsync.getJson
            |> EffectAsync.handle interpreter

        Expect.equal actualJson expectedJson
    }
