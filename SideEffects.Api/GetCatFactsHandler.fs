namespace SideEffects.Api

open Falco
open SideEffects.Monad

module GetCatFactsHandler =

    let handle interpreter : HttpHandler =
        fun ctx -> task {

        let url = AppSettings.catFactsUrl     

        let! facts =
            url
            |> CatFacts.getAsync 
            |> SideEffectAsync.handle interpreter 
            
        return! Response.ofJson facts ctx
    }
    