namespace SideEffects.Monad

module InstructionAsync =

    let map fn = Async.map fn |> Instruction.map
