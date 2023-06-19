namespace SideEffects.Monad

open System
open System.Text.Json
open NodaTime

type Interpreter = {
    Log: String -> Unit
    CreateGuid: Unit -> Guid
    GetTime: Unit -> Instant
    GetJson: Uri -> JsonDocument Async
}

module Interpreter =
    
    let create log createGuid getTime getJson = {
        CreateGuid = createGuid
        GetTime = getTime
        Log = log
        GetJson = getJson
    }
    