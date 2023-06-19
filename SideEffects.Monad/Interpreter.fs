namespace SideEffects.Monad

open System
open System.Text.Json
open NodaTime

type Interpreter = {
    Log: string -> unit
    CreateGuid: unit -> Guid
    GetTime: unit -> Instant
    GetJson: Uri -> JsonDocument Async
}

module Interpreter =
    
    let create log createGuid getTime getJson = {
        CreateGuid = createGuid
        GetTime = getTime
        Log = log
        GetJson = getJson
    }
    