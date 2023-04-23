namespace SideEffects.Monad

open NodaTime
open System
open System.Text.Json

type Instruction<'a> =
    | Log of String * (Unit -> 'a)
    | CreateGuid of (Guid -> 'a)
    | GetTime of (Instant -> 'a)
    | HttpRequest of Uri * (JsonDocument Async -> 'a)

module Instruction =
    
    let map fn = function
        | Log (str, next) -> Log (str, next >> fn)
        | CreateGuid next -> CreateGuid (next >> fn)
        | GetTime next -> GetTime (next >> fn)
        | HttpRequest (url, next) -> HttpRequest (url, next >> fn)
    
    let peel interpreter = function
        | Log (str, next) -> next (interpreter.Log str)
        | CreateGuid next -> next (interpreter.CreateGuid ())
        | GetTime next -> next (interpreter.GetTime ())
        | HttpRequest (url, next) -> next (interpreter.HttpRequest url)
    