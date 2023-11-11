namespace Effects.Api

open System.Net.Http
open Falco
open Falco.HostBuilder

module App =

    open Routing

    let private httpClient = new HttpClient()

    let private interpreter =
        httpClient
        |> ImpureInterpreter.create

    let routes = [
        get "/" (GetCatFactsHandler.handle interpreter)
        get "/health" (Response.ofPlainText "It's alive!")
    ]

    webHost [||] {
        endpoints routes
    }
