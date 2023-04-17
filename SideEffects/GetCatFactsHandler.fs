namespace SideEffects.FSharp

open System
open Falco

module GetCatFactsHandler =

    [<Literal>]
    let FactsUrl = "https://cat-fact.herokuapp.com/facts"

    let handle interpreter : HttpHandler =
        fun ctx -> task {

        let url = Uri FactsUrl
        
        let! response =
            url
            |> CatFacts.getFactsFrom
            |> SideEffect.handle interpreter
            
        return! Response.ofJson response ctx
    }
    