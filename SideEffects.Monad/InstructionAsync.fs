namespace SideEffects.Monad

module InstructionAsync =
    
    let map fn = Instruction.map (Async.map fn)
    