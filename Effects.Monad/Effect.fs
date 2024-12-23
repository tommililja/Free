namespace Effects.Monad

type Effect<'a, 'e> =
    | Impure of Instruction<AsyncResult<Effect<'a, 'e>, 'e>>
    | Pure of 'a

module Effect =

    let ret = Pure

    let retAsyncEffect (x:Async<_>) =
        Async.map Ok x
        |> AsyncResult
        |> Instruction.ret
        |> Impure

    let retResultEffect x =
        Async.ret x
        |> AsyncResult
        |> Instruction.ret
        |> Impure

    let retAsyncResult x =
        Instruction.ret x
        |> Impure

    let rec bind fn = function
        | Impure effect ->
            effect
            |> Instruction.map (AsyncResult.map (bind fn))
            |> Impure
        | Pure x -> fn x

    let map fn = bind (fn >> ret)

    let rec handle interpreter = function
        | Impure instruction ->
            instruction
            |> Instruction.run interpreter
            |> AsyncResult.bind (handle interpreter)
        | Pure x -> AsyncResult.ret x

    // Lift

    let private impureRet x = ret x |> AsyncResult.ret

    let log str = Impure <| Log (str, impureRet)

    let createGuid () = Impure <| CreateGuid impureRet

    let getTime () = Impure <| GetTime impureRet

    let random max = Impure <| Random (max, impureRet)