namespace SideEffects.Api

open Falco
open SideEffects.Monad

module GetCatFactsHandler =

    let handle interpreter : HttpHandler =
        fun ctx -> task {

        let url = AppSettings.catFactsUrl
        
        let sideEffect =
            url
            |> CatFacts.getAsync
            
        let! facts =
            sideEffect
            |> SideEffect.handle interpreter 
            
        return! Response.ofJson facts ctx
    }
    