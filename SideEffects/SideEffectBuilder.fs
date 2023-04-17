namespace SideEffects.FSharp

[<AutoOpen>]
module SideEffectBuilder =

    type SideEffectBuilder() =
        
        member this.Bind(x, fn) = SideEffect.bind fn x
        
        member this.Return(x) = Pure x

        member this.Zero() = Pure ()

    let sideEffect = SideEffectBuilder()
    