namespace SideEffects.Monad

module Async =

    let ret = async.Return

    let bind fn x = async.Bind(x, fn)

    let map fn = bind (fn >> ret)
