namespace SideEffects.FSharp

open Falco
open Falco.HostBuilder
open System.Net.Http

module Program =

    let private httpClient = new HttpClient()
    
    let private interpreter =
        ImpureInterpreter.create httpClient
    
    webHost [||] {
        endpoints [
            Routing.get "/" (GetCatFactsHandler.handle interpreter)
        ]
    }
    