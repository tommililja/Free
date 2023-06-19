namespace SideEffects.Monad

type 'a EffectAsync =
    | Impure of 'a EffectAsync Async Instruction
    | Pure of 'a

module EffectAsync =
    
    let ret = Pure
    
    let retAsync x =
        ret x
        |> Async.ret
    
    let rec bind fn = function
        | Impure instruction ->
            instruction
            |> InstructionAsync.map (bind fn)
            |> Impure
        | Pure x -> fn x
    
    let map fn = bind (fn >> ret)
    
    let rec handle interpreter = function
        | Impure instruction ->
            instruction
            |> Instruction.run interpreter 
            |> Async.bind (handle interpreter)
        | Pure x -> Async.ret x

    // Lift

    let log str = Impure (Log (str, retAsync))

    let createGuid () = Impure (CreateGuid ((), retAsync))

    let getTime () = Impure (GetTime ((), retAsync))
    
    let getJson url = Impure (GetJson (url, Async.map ret))
    