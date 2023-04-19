namespace SideEffects.FSharp

open System
open NodaTime

type Interpreter = {
    Log: String -> Unit
    CreateGuid: Unit -> Guid
    GetTime: Unit -> Instant
    HttpRequest: Uri -> String Async
}

module Interpreter =
    
    let create log createGuid getTime httpRequest = {
        CreateGuid = createGuid
        GetTime = getTime
        Log = log
        HttpRequest = httpRequest
    }
    