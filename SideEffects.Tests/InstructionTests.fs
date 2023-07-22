namespace SideEffects.Tests

open SideEffects.Monad
open Xunit

module InstructionTests =

    open Common

    [<Fact>]
    let ``createGuid should be interpreted correctly`` () =

        let instruction = CreateGuid ((), id)

        let interpreter =
            expectedGuid
            |> TestInterpreter.withGuid

        let actualGuid =
            instruction
            |> Instruction.run interpreter

        Expect.equal actualGuid expectedGuid

    [<Fact>]
    let ``getTime should be interpreted correctly`` () =

        let instruction = GetTime ((), id)

        let interpreter =
            expectedTime
            |> TestInterpreter.withTime

        let actualTime =
            instruction
            |> Instruction.run interpreter

        Expect.equal actualTime expectedTime

    [<Fact>]
    let ``getJson should be interpreted correctly`` () = async {

        let instruction = GetJson (url, id)

        let interpreter = TestInterpreter.defaultInterpreter

        let! actualJson =
            instruction
            |> Instruction.run interpreter

        Expect.equal actualJson expectedJson
    }
