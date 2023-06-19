namespace SideEffects.Monad

type 'a EffectAsync =
    | Free of 'a EffectAsync Async Instruction
    | Pure of 'a

module EffectAsync =
    
    let ret = Pure
    
    let retAsync x =
        ret x
        |> Async.ret
    
    let rec bind fn = function
        | Free instruction ->
            instruction
            |> InstructionAsync.map (bind fn)
            |> Free
        | Pure x -> fn x
    
    let map fn = bind (fn >> ret)
    
    let rec handle interpreter = function
        | Free instruction ->
            instruction
            |> Instruction.run interpreter 
            |> Async.bind (handle interpreter)
        | Pure x -> Async.ret x

    // Lift
    
    let log str = Free (Log (str, retAsync))

    let createGuid () = Free (CreateGuid ((), retAsync))

    let getTime () = Free (GetTime ((), retAsync))
    
    let getJson url = Free (GetJson (url, Async.map ret))
    