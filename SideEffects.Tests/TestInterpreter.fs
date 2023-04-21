namespace SideEffects.Tests

open NodaTime
open SideEffects.Monad
open System

module TestInterpreter =
    
    let withResponse response = {
        Log = Console.WriteLine
        CreateGuid = Guid.NewGuid
        GetTime = SystemClock.Instance.GetCurrentInstant
        HttpRequest = (fun _ -> Async.ret response)
    }
    