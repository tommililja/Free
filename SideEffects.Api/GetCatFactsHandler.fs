namespace SideEffects.Api

open Falco
open SideEffects.Monad

module GetCatFactsHandler =

    let handle interpreter : HttpHandler =
        fun ctx -> task {

        let url = AppSettings.catFactsUrl
        
        let! response =
            url
            |> CatFacts.getFactsAsync
            |> SideEffect.handle interpreter
            
        return! Response.ofJson response ctx
    }
    