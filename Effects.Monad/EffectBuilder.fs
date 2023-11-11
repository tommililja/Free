namespace Effects.Monad

open Effect

type EffectBuilder() =

    member this.Bind(x, fn) = bind fn x

    member this.Zero() = ret ()

    member this.Return(x) = ret x

    member this.ReturnFrom(x) = x

    member this.Delay(f) = f

    member this.Run(f) = f ()

[<AutoOpen>]
module EffectBuilder =

    let effect = EffectBuilder()
