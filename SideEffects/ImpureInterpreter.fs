namespace SideEffects.FSharp

open System
open System.Net.Http
open Microsoft.FSharp.Core
open NodaTime

module ImpureInterpreter =

    let create (client:HttpClient) = {
        CreateGuid = Guid.NewGuid
        GetTime = SystemClock.Instance.GetCurrentInstant
        Log = Console.WriteLine
        Http = (fun url -> client.GetStringAsync url |> Async.AwaitTask)
    }
    