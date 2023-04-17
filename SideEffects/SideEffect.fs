namespace SideEffects.FSharp

open System
open NodaTime

type Impure<'a> =
    | CreateGuid of (Guid -> 'a SideEffect)
    | GetTime of (Instant -> 'a SideEffect)
    | Log of String * (Unit -> 'a SideEffect)
    | Http of Uri * (String Async -> 'a SideEffect)

and SideEffect<'a> =
    | Free of 'a Impure
    | Pure of 'a
    
and SideEffectAsync<'a> = 'a Async SideEffect

module SideEffect =

    // Monad
         
    let private convert mb fn = function
        | CreateGuid next -> CreateGuid (next >> mb fn)
        | GetTime next -> GetTime (next >> mb fn)
        | Log (str, next) -> Log (str, next >> mb fn)
        | Http (url, next) -> Http (url, next >> mb fn)
        
    let rec map fn = function
        | Free impure -> Free (convert map fn impure)
        | Pure x -> Pure (fn x)
    
    let rec bind fn = function
        | Free impure -> Free (convert bind fn impure)
        | Pure x -> fn x
    
    // Handle
    
    let rec handle interpreter sideEffect =
        
        let cont next =
            next >> handle interpreter
        
        let select = function
            | Log (str, next) -> cont next (interpreter.Log str)
            | CreateGuid next -> cont next (interpreter.CreateGuid ())
            | GetTime next -> cont next (interpreter.GetTime ())
            | Http (url, next) -> cont next (interpreter.Http url)
        
        match sideEffect with
        | Free impure -> select impure
        | Pure x -> x
    
    // Lift    

    let log str = Free (Log (str, Pure))
    
    let createGuid () = Free (CreateGuid Pure)
    
    let getTime () = Free (GetTime Pure)
    
    let http url : SideEffectAsync<_> = Free (Http (url, Pure))
    