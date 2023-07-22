namespace SideEffects.Api

open System.Threading.Tasks
open SideEffects.Monad
open Falco

module GetCatFactsHandler =

    let handle interpreter ctx : Task = task {

        let url = AppSettings.catFactsUrl

        let! facts =
            url
            |> CatFacts.getAsync
            |> EffectAsync.handle interpreter

        return! Response.ofJson facts ctx
    }
