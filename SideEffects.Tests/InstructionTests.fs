namespace SideEffects.Tests

open SideEffects.Monad
open Xunit

module InstructionTests =
    
    open Common
    
    let private run instruction interpreter =
        Instruction.run interpreter instruction
    
    [<Fact>]
    let ``createGuid should be interpreted correctly`` () =
        
        let createGuid = CreateGuid ((), id)

        let actualGuid =
            expectedGuid
            |> TestInterpreter.withGuid
            |> run createGuid

        Expect.equal actualGuid expectedGuid
        
    [<Fact>]
    let ``getTime should be interpreted correctly`` () =
        
        let getTime = GetTime ((), id)
        
        let actualTime =
            expectedTime
            |> TestInterpreter.withTime
            |> run getTime

        Expect.equal actualTime expectedTime
        
    [<Fact>]
    let ``getJson should be interpreted correctly`` () = async {
        
        let getJson = GetJson (url, id)
        let interpreter = TestInterpreter.def
        
        let! actualJson =
            interpreter
            |> run getJson

        Expect.equal actualJson expectedJson
    }
    