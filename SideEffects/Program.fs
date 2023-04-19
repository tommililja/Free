namespace SideEffects.FSharp

open System
open Falco
open Falco.HostBuilder
open System.Net.Http
open NodaTime

module ImpureInterpreter =

    let create (client:HttpClient) = {
        CreateGuid = Guid.NewGuid
        GetTime = SystemClock.Instance.GetCurrentInstant
        Log = Console.WriteLine
        HttpRequest = (fun url -> client.GetStringAsync url |> Async.AwaitTask)
    }

module Program =

    let private httpClient = new HttpClient()
    
    let private interpreter =
        ImpureInterpreter.create httpClient
    
    webHost [||] {
        endpoints [
            Routing.get "/" (GetCatFactsHandler.handle interpreter)
        ]
    }
    