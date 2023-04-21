namespace SideEffects.Monad

type SideEffectBuilder() =
    
    member this.Bind(x, f) = SideEffect.bind f x
    
    member this.Return(x) = Pure x
    
    member this.ReturnFrom(x) = x

    member this.Zero() = Pure ()

[<AutoOpen>]
module SideEffectBuilder =

    let sideEffect = SideEffectBuilder()
    