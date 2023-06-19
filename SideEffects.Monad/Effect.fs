namespace SideEffects.Monad

type 'a Effect =
    | Impure of 'a Effect Instruction
    | Pure of 'a

module Effect =
    
    let ret = Pure
    
    let rec bind fn = function
        | Impure instruction ->
            instruction
            |> Instruction.map (bind fn)
            |> Impure
        | Pure x -> fn x
    
    let map fn = bind (fn >> ret)
    
    let rec handle interpreter = function
        | Impure instruction ->
            instruction
            |> Instruction.run interpreter
            |> handle interpreter
        | Pure x -> x   

    // Lift
    
    let log str = Impure (Log (str, ret))
    
    let createGuid () = Impure (CreateGuid ((), ret))
    
    let getTime () = Impure (GetTime ((), ret))
    