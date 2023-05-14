namespace SideEffects.Monad

open NodaTime
open System
open System.Text.Json

type 'a Instruction =
    | Log of String * (Unit -> 'a)
    | CreateGuid of Unit * (Guid -> 'a)
    | GetTime of Unit * (Instant -> 'a)
    | HttpRequest of Uri * (JsonDocument Async -> 'a)

module Instruction =
    
    let map fn = function
        | Log (str, next) -> Log (str, next >> fn)
        | CreateGuid (unit, next) -> CreateGuid (unit, next >> fn)
        | GetTime (unit, next) -> GetTime (unit, next >> fn)
        | HttpRequest (url, next) -> HttpRequest (url, next >> fn)
    
    let peel interpreter = function
        | Log (str, next) -> next (interpreter.Log str)
        | CreateGuid (unit, next) -> next (interpreter.CreateGuid unit)
        | GetTime (unit, next) -> next (interpreter.GetTime unit)
        | HttpRequest (url, next) -> next (interpreter.HttpRequest url)
    