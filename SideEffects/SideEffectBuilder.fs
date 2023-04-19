namespace SideEffects.FSharp

[<AutoOpen>]
module SideEffectBuilder =

    type SideEffectBuilder() =
        
        member this.Bind(x, f) = SideEffect.bind f x
        
        member this.Return(x) = Pure x

        member this.Zero() = Pure ()

    let sideEffect = SideEffectBuilder()
    