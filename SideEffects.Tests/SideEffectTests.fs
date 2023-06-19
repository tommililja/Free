namespace SideEffects.Tests

open SideEffects.Monad
open SideEffects.Tests
open Xunit

module SideEffectTests =

    open Common
    
    [<Fact>]
    let ``bind should bind value correctly`` () =
  
        let interpreter =
            TestInterpreter.withGuidAndTime
                expectedGuid
                expectedTime
                
        let bind guid =
            Expect.equal guid expectedGuid 
            SideEffect.getTime unit 
                
        let actualTime =
            unit
            |> SideEffect.createGuid
            |> SideEffect.bind bind
            |> SideEffect.handle interpreter
            
        Expect.equal actualTime expectedTime
    
    [<Fact>]
    let ``map should map value correctly`` () =
        
        let expectedString = string expectedGuid
        
        let interpreter =
            expectedGuid
            |> TestInterpreter.withGuid
        
        let actualString =
            unit
            |> SideEffect.createGuid
            |> SideEffect.map string
            |> SideEffect.handle interpreter
            
        Expect.equal actualString expectedString    
    
    [<Fact>]
    let ``handle should interpret createGuid correctly`` () =

        let interpreter =
            expectedGuid
            |> TestInterpreter.withGuid
        
        let actualGuid =
            unit
            |> SideEffect.createGuid
            |> SideEffect.handle interpreter
        
        Expect.equal actualGuid expectedGuid
        
    [<Fact>]
    let ``handle should interpret getTime correctly`` () =
        
        let interpreter =
            expectedTime
            |> TestInterpreter.withTime
        
        let actualTime =
            unit
            |> SideEffect.getTime
            |> SideEffect.handle interpreter
        
        Expect.equal actualTime expectedTime
    