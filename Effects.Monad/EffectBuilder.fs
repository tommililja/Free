namespace Effects.Monad

open System.Threading.Tasks
open Microsoft.FSharp.Core

[<AutoOpen>]
module EffectBuilder =

    open Effect

    type EffectBuilder() =

        member this.Bind(x, fn) = bind fn x

        member this.Bind(x:Task<_>, fn: 'a -> Effect<'b,_>) =
            Async.AwaitTask x
            |> Async.map fn
            |> retAsyncEffect

        member this.Bind(x:Async<_>, fn: 'a -> Effect<'b,_>) =
            Async.map fn x
            |> retAsyncEffect

        member this.Bind(x:Result<_,_>, fn: 'a -> Effect<'b,_>) =
            Result.map fn x
            |> retResultEffect

        member this.Bind(x:AsyncResult<_,_>, fn: 'a -> Effect<'b,_>) =
            AsyncResult.map fn x
            |> retAsyncResult

        member this.Combine (x, fn) = bind fn x

        member this.Zero() = ret ()

        member this.Return(x) = ret x

        member this.ReturnFrom(x) = x

        member this.Delay(f) = f

        member this.Run(f) = f ()

    let effect = EffectBuilder()
