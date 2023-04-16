namespace SideEffects.FSharp

open System
open System.Net.Http
open Microsoft.FSharp.Core
open NodaTime

module SideEffects =

    let private logEffect (str:String) = Console.WriteLine str
    
    let private guidEffect = Guid.NewGuid
    
    let private dateEffect = SystemClock.Instance.GetCurrentInstant
    
    let private httpRequestEffect (client:HttpClient) (url:String) =
        url
        |> client.GetStringAsync
        |> Async.AwaitTask
    
    let rec handle (client:HttpClient) sideEffect =
        
        let cont next =
            next >> handle client
        
        match sideEffect with
        | Log (str, next) -> cont next (logEffect str)
        | HttpRequest (url, next) -> cont next (httpRequestEffect client url)
        | Date (_, next) -> cont next (dateEffect ())
        | Guid (_, next) -> cont next (guidEffect ())
        | Pure x -> x
    