namespace Effects.Api

open System
open System.IO
open Effects.Monad
open NodaTime
open Oxpecker
open Thoth.Json.Net

module Routes =

    let private random = new Random()

    let private interpreter = {
        Log = Console.WriteLine
        CreateGuid = Guid.NewGuid
        GetTime = SystemClock.Instance.GetCurrentInstant
        Random = (fun size -> random.Next(1, size + 1))
    }

    let resolve effect : EndpointHandler =
        fun ctx -> task {
            let result =
                effect
                |> Effect.handle interpreter
                |> AsyncResult.value

            let! handler =
                result
                |> Async.map (
                    Result.id
                    >> ApiResponse.handle
                )

            return! handler ctx
        }

    let parseBody decoder handler : EndpointHandler =
        fun ctx -> task {
            use reader = new StreamReader(ctx.Request.Body)
            let! json = reader.ReadToEndAsync()

            let handler =
                match Decode.fromString decoder json with
                | Ok v -> resolve (handler v)
                | Error e -> setStatusCode 400 >=> text e

            return! handler ctx
        }

    let list =
        [
            POST [
                route "/roll" <| parseBody DiceHandler.decoder DiceHandler.handle
            ]

            GET [
                route "/health" <| text "It's alive!"
            ]
        ]
