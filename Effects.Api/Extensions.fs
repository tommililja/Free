namespace Effects.Api

module Result =
    
    let id = function
        | Ok v -> v
        | Error e -> e