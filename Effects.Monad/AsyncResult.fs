namespace Effects.Monad

type AsyncResult<'a, 'e> = AsyncResult of Async<Result<'a, 'e>>

module AsyncResult =

    let ret x =
        Async.ret (Ok x)
        |> AsyncResult

    let bind fn (AsyncResult x) =
        async {
            let! a = x
            let! b =
                match a with
                | Ok a ->
                    let (AsyncResult b) = fn a
                    b
                | Error e -> Async.ret <| Error e

            return b
        }
        |> AsyncResult

    let map fn = bind (fn >> ret)
    
    let value (AsyncResult ar) = ar
