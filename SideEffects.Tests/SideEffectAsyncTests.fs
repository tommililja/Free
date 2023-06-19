namespace SideEffects.Tests

open SideEffects.Monad
open SideEffects.Tests
open Xunit

module SideEffectAsyncTests =

    open Common
    
    [<Fact>]
    let ``bind should bind value correctly`` () =
  
        let interpreter =
            TestInterpreter.withGuidAndTime
                expectedGuid
                expectedTime
                
        let bind guid =
            Expect.equal guid expectedGuid    
            SideEffectAsync.getTime unit 
                
        let actualTime =
            unit
            |> SideEffectAsync.createGuid
            |> SideEffectAsync.bind bind
            |> SideEffectAsync.handle interpreter
            |> Async.RunSynchronously
            
        Expect.equal actualTime expectedTime
    
    [<Fact>]
    let ``map should map value correctly`` () =
        
        let expectedString = string expectedGuid
        
        let interpreter =
            expectedGuid
            |> TestInterpreter.withGuid
        
        let actualString =
            unit
            |> SideEffectAsync.createGuid
            |> SideEffectAsync.map string
            |> SideEffectAsync.handle interpreter
            |> Async.RunSynchronously
            
        Expect.equal actualString expectedString    
    
    [<Fact>]
    let ``handle should interpret createGuid correctly`` () =

        let interpreter =
            expectedGuid
            |> TestInterpreter.withGuid
        
        let actualGuid =
            unit
            |> SideEffectAsync.createGuid
            |> SideEffectAsync.handle interpreter
            |> Async.RunSynchronously
        
        Expect.equal actualGuid expectedGuid
        
    [<Fact>]
    let ``handle should interpret getTime correctly`` () =
        
        let interpreter =
            expectedTime
            |> TestInterpreter.withTime
        
        let actualTime =
            unit
            |> SideEffectAsync.getTime
            |> SideEffectAsync.handle interpreter
            |> Async.RunSynchronously
        
        Expect.equal actualTime expectedTime
        
    [<Fact>]
    let ``handle should interpret getJson correctly`` () =
        
        let interpreter = TestInterpreter.def
        
        let actualJson =
            url
            |> SideEffectAsync.getJson
            |> SideEffectAsync.handle interpreter
            |> Async.RunSynchronously
        
        Expect.equal actualJson expectedJson
    