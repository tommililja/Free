namespace SideEffects.Api

open NodaTime
open SideEffects.Monad
open System
open System.Net.Http

module ImpureInterpreter =

    let create (httpClient:HttpClient) = {
        Log = Console.WriteLine
        CreateGuid = Guid.NewGuid
        GetTime = SystemClock.Instance.GetCurrentInstant
        HttpRequest = HttpClient.getJsonAsync httpClient
    }
    