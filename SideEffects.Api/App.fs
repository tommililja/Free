namespace SideEffects.Api

open System.Net.Http
open Falco
open Falco.HostBuilder

module App =

    let private httpClient = new HttpClient()

    let private interpreter =
        httpClient
        |> ImpureInterpreter.create

    let routes = [
        Routing.get "/" (GetCatFactsHandler.handle interpreter)
        Routing.get "/health" (Response.ofPlainText "It's alive!")
    ]

    webHost [||] {
        endpoints routes
    }
