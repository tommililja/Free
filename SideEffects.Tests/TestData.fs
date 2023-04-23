namespace SideEffects.Tests

open System
open NodaTime
open SideEffects.Monad

module TestData =
    
    let response = """[ { "text": "Cat fact" } ]"""

    let url = Uri "http://domain.com"

    module Interpreter =
        
        let withResponse response = {
            Log = Console.WriteLine
            CreateGuid = Guid.NewGuid
            GetTime = SystemClock.Instance.GetCurrentInstant
            HttpRequest = (fun _ -> Async.ret response)
        }
    