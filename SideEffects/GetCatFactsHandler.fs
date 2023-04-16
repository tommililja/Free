namespace SideEffects.FSharp

open System.Net.Http
open Falco

module GetCatFactsHandler =

    [<Literal>]
    let FactsUrl = "https://cat-fact.herokuapp.com/facts"
    
    let private httpClient = new HttpClient()
    
    let handle : HttpHandler =
        fun ctx -> task {

        let! response =
            FactsUrl
            |> CatFacts.getFactsFrom
            |> SideEffects.handle httpClient
            
        return! Response.ofJson response ctx
    }
    