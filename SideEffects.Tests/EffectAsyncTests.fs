namespace SideEffects.Tests

open SideEffects.Monad
open SideEffects.Tests
open Xunit

module EffectAsyncTests =

    open Common
    
    [<Fact>]
    let ``bind should bind value correctly`` () =
  
        let interpreter =
            TestInterpreter.withGuidAndTime
                expectedGuid
                expectedTime
                
        let bind guid =
            Expect.equal guid expectedGuid    
            EffectAsync.getTime unit 
                
        let actualTime =
            unit
            |> EffectAsync.createGuid
            |> EffectAsync.bind bind
            |> EffectAsync.handle interpreter
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
            |> EffectAsync.createGuid
            |> EffectAsync.map string
            |> EffectAsync.handle interpreter
            |> Async.RunSynchronously
            
        Expect.equal actualString expectedString    
    
    [<Fact>]
    let ``handle should interpret createGuid correctly`` () =

        let interpreter =
            expectedGuid
            |> TestInterpreter.withGuid
        
        let actualGuid =
            unit
            |> EffectAsync.createGuid
            |> EffectAsync.handle interpreter
            |> Async.RunSynchronously
        
        Expect.equal actualGuid expectedGuid
        
    [<Fact>]
    let ``handle should interpret getTime correctly`` () =
        
        let interpreter =
            expectedTime
            |> TestInterpreter.withTime
        
        let actualTime =
            unit
            |> EffectAsync.getTime
            |> EffectAsync.handle interpreter
            |> Async.RunSynchronously
        
        Expect.equal actualTime expectedTime
        
    [<Fact>]
    let ``handle should interpret getJson correctly`` () =
        
        let interpreter = TestInterpreter.def
        
        let actualJson =
            url
            |> EffectAsync.getJson
            |> EffectAsync.handle interpreter
            |> Async.RunSynchronously
        
        Expect.equal actualJson expectedJson
    