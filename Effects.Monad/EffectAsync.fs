namespace Effects.Monad

type 'a EffectAsync =
    | Impure of 'a EffectAsync Async Instruction
    | Pure of 'a

module EffectAsync =

    let ret = Pure

    let retAsync x = ret x |> Async.ret

    let rec bind fn = function
        | Impure instruction ->
            instruction
            |> InstructionAsync.map (bind fn)
            |> Impure
        | Pure x -> fn x

    let map fn = bind (fn >> ret)

    let rec handle interpreter = function
        | Impure instruction ->
            instruction
            |> Instruction.run interpreter
            |> Async.bind (handle interpreter)
        | Pure x -> Async.ret x

    // Lift

    let log str = Log (str, retAsync) |> Impure

    let createGuid guid = CreateGuid (guid, retAsync) |> Impure

    let getTime () = GetTime ((), retAsync) |> Impure

    let getJson url = GetJson (url, Async.map ret) |> Impure
