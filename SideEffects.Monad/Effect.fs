namespace SideEffects.Monad

type 'a Effect =
    | Free of 'a Effect Instruction
    | Pure of 'a

module Effect =
    
    let ret = Pure
    
    let rec bind fn = function
        | Free instruction ->
            instruction
            |> Instruction.map (bind fn)
            |> Free
        | Pure x -> fn x
    
    let map fn = bind (fn >> ret)
    
    let rec handle interpreter = function
        | Free instruction ->
            instruction
            |> Instruction.run interpreter
            |> handle interpreter
        | Pure x -> x   

    // Lift
    
    let log str = Free (Log (str, ret))
    
    let createGuid () = Free (CreateGuid ((), ret))
    
    let getTime () = Free (GetTime ((), ret))
    