namespace SideEffects.Monad

type SideEffect<'a> =
    | Free of 'a SideEffect Instruction
    | Pure of 'a
    
type SideEffectAsync<'a> = 'a Async SideEffect

module SideEffect =
    
    let rec bind fn = function
        | Free i -> Free (Instruction.map (bind fn) i)
        | Pure x -> fn x
    
    let map fn = bind (fn >> Pure)
    
    let rec handle interpreter = function
        | Free i -> Instruction.peel interpreter i |> handle interpreter
        | Pure x -> x   

    let log str = Free (Log (str, Pure))
    
    let createGuid () = Free (CreateGuid Pure)
    
    let getTime () = Free (GetTime Pure)
    
    let httpRequest url : SideEffectAsync<_> = Free (HttpRequest (url, Pure))
    