namespace SideEffects.FSharp

open Falco
open Falco.HostBuilder

module Program =

    webHost [||] {
        endpoints [
            Routing.get "/" GetCatFactsHandler.handle
        ]
    }
    