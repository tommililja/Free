namespace SideEffects.FSharp

open System
open NodaTime

type Interpreter = {
    CreateGuid: Unit -> Guid
    GetTime: Unit -> Instant
    Log: String -> Unit
    Http: Uri -> String Async
}

module Interpreter =
    
    let create createGuid getTime log http = {
        CreateGuid = createGuid
        GetTime = getTime
        Log = log
        Http = http
    }
    