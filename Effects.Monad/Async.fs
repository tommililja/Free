namespace Effects.Monad

module Async =

    let ret x =
        async {
            return x
        }

    let bind fn x =
        async {
            let! a = x
            let! b = fn a
            return b
        }

    let map fn = bind (fn >> ret)
