namespace Effects.Api

open Effects.Monad
open Thoth.Json.Net

type Dice = {
    Size: int
    Delay: int option
}

module DiceHandler =

    let decoder: Decoder<Dice> =
        Decode.object (fun get -> {
            Size = get.Required.Field "size" Decode.int
            Delay = get.Optional.Field "delay" Decode.int
        })

    let handle dice =
        effect {
            let! number =
                dice.Size
                |> Effect.random

            if number = dice.Size then
                do! Error InternalServerError

            do!
                dice.Delay
                |> Option.map Async.Sleep
                |> Option.defaultWith Async.ret

            do! Effect.log $"Dice rolled: %i{number}."

            let response =
                Encode.object [
                    "number", Encode.int number
                ]

            return Json response
        }
