namespace SideEffects.Monad

open NodaTime
open System
open System.Text.Json

type Interpreter = {
    Log: String -> Unit
    CreateGuid: Unit -> Guid
    GetTime: Unit -> Instant
    HttpRequest: Uri -> JsonDocument Async
}

module Interpreter =
    
    let create log createGuid getTime httpRequest = {
        CreateGuid = createGuid
        GetTime = getTime
        Log = log
        HttpRequest = httpRequest
    }
    