namespace SideEffects.Monad

open System
open System.Text.Json
open NodaTime

type 'a Instruction =
    | Log of string * (unit -> 'a)
    | CreateGuid of unit * (Guid -> 'a)
    | GetTime of unit * (Instant -> 'a)
    | GetJson of Uri * (JsonDocument Async -> 'a)

module Instruction =
    
    let map fn = function
        | Log (str, next) -> Log (str, next >> fn)
        | CreateGuid (unit, next) -> CreateGuid (unit, next >> fn)
        | GetTime (unit, next) -> GetTime (unit, next >> fn)
        | GetJson (url, next) -> GetJson (url, next >> fn)
    
    let run interpreter = function
        | Log (str, next) -> next (interpreter.Log str)
        | CreateGuid (unit, next) -> next (interpreter.CreateGuid unit)
        | GetTime (unit, next) -> next (interpreter.GetTime unit)
        | GetJson (url, next) -> next (interpreter.GetJson url)
    