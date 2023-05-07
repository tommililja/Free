namespace SideEffects.Monad

type SideEffectBuilder() =
    
    member this.Bind(x, fn) = SideEffect.bind fn x
    
    member this.Return(x) = SideEffect.ret x
    
    member this.ReturnFrom(x) = x

    member this.Zero() = SideEffect.ret ()
    
    member this.Delay(f) = f
    
    member this.Run(f) = f ()

[<AutoOpen>]
module SideEffectBuilder =

    let sideEffect = SideEffectBuilder()
    