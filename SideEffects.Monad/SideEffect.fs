namespace SideEffects.Monad

type 'a SideEffect =
    | Free of 'a SideEffect Instruction
    | Pure of 'a

module SideEffect =
    
    let ret = Pure
    
    let rec bind fn = function
        | Free i -> Free (Instruction.map (bind fn) i)
        | Pure x -> fn x
    
    let map fn = bind (fn >> ret)
    
    let rec handle interpreter = function
        | Free i -> Instruction.peel interpreter i |> handle interpreter
        | Pure x -> x   

    let log str = Free (Log (str, ret))
    
    let createGuid () = Free (CreateGuid ret)
    
    let getTime () = Free (GetTime ret)
    
    let httpRequest url = Free (HttpRequest (url, ret))
    