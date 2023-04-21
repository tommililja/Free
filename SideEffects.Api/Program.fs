namespace SideEffects.Api

open Falco
open Falco.HostBuilder
open System.Net.Http

module Program =

    let private httpClient = new HttpClient()
    
    let private interpreter =
        httpClient
        |> ImpureInterpreter.create 

    let routes = [
        Routing.get "/" (GetCatFactsHandler.handle interpreter)
        Routing.get "/health" (Response.ofPlainText "It's alive!")
    ]
    
    webHost [||] { endpoints routes }
    