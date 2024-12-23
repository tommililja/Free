namespace Effects.Monad

open System
open NodaTime

type Instruction<'a> =
    | Log of string * (unit -> 'a)
    | CreateGuid of (Guid -> 'a)
    | GetTime of (Instant -> 'a)
    | Random of int * (int -> 'a)
    | Ret of 'a

type Interpreter = {
    Log: string -> unit
    CreateGuid: unit -> Guid
    GetTime: unit -> Instant
    Random: int -> int
}

module Instruction =

    let ret = Ret
    
    let map fn = function
        | Log (str, next) -> Log (str, next >> fn)
        | CreateGuid next -> CreateGuid (next >> fn)
        | GetTime next -> GetTime (next >> fn)
        | Random (unit, next) -> Random (unit, next >> fn)
        | Ret a -> ret <| fn a

    let run interpreter = function
        | Log (str, next) -> next (interpreter.Log str)
        | CreateGuid next -> next (interpreter.CreateGuid ())
        | GetTime next -> next (interpreter.GetTime ())
        | Random (max, next) -> next (interpreter.Random max)
        | Ret a -> a
