namespace SideEffects.Tests

open SideEffects.Monad
open SideEffects.Tests
open Xunit

module EffectTests =

    open Common
    
    [<Fact>]
    let ``bind should bind value correctly`` () =
  
        let interpreter =
            TestInterpreter.withGuidAndTime
                expectedGuid
                expectedTime
                
        let bind guid =
            Expect.equal guid expectedGuid 
            Effect.getTime unit 
                
        let actualTime =
            unit
            |> Effect.createGuid
            |> Effect.bind bind
            |> Effect.handle interpreter
            
        Expect.equal actualTime expectedTime
    
    [<Fact>]
    let ``map should map value correctly`` () =
        
        let expectedString = string expectedGuid
        
        let interpreter =
            expectedGuid
            |> TestInterpreter.withGuid
        
        let actualString =
            unit
            |> Effect.createGuid
            |> Effect.map string
            |> Effect.handle interpreter
            
        Expect.equal actualString expectedString    
    
    [<Fact>]
    let ``handle should interpret createGuid correctly`` () =

        let interpreter =
            expectedGuid
            |> TestInterpreter.withGuid
        
        let actualGuid =
            unit
            |> Effect.createGuid
            |> Effect.handle interpreter
        
        Expect.equal actualGuid expectedGuid
        
    [<Fact>]
    let ``handle should interpret getTime correctly`` () =
        
        let interpreter =
            expectedTime
            |> TestInterpreter.withTime
        
        let actualTime =
            unit
            |> Effect.getTime
            |> Effect.handle interpreter
        
        Expect.equal actualTime expectedTime
    