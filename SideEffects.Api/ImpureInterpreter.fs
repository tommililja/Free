namespace SideEffects.Api

open System
open System.Net.Http
open SideEffects.Monad
open NodaTime

module ImpureInterpreter =

    let create (httpClient:HttpClient) = {
        Log = Console.WriteLine
        CreateGuid = Guid.NewGuid
        GetTime = SystemClock.Instance.GetCurrentInstant
        GetJson = HttpClient.getJsonAsync httpClient
    }
