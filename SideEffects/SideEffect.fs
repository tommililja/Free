namespace SideEffects.FSharp

open System
open NodaTime

type SideEffect<'a> =
    | Log of String * (Unit -> 'a SideEffect)
    | HttpRequest of String * (String Async -> 'a SideEffect)
    | Date of Unit * (Instant -> 'a SideEffect)
    | Guid of Unit * (Guid -> 'a SideEffect)
    | Pure of 'a
    
and SideEffectAsync<'a> = 'a Async SideEffect

module SideEffect =

    // Monad
    
    let rec bind fn = function
        | Log (str, next) -> Log (str, next >> bind fn)
        | HttpRequest (url, next) -> HttpRequest (url, next >> bind fn)
        | Date (_, next) -> Date ((), next >> bind fn)
        | Guid (_, next) -> Guid ((), next >> bind fn)
        | Pure x -> fn x
      
    let rec map fn = function
        | Log (str, next) -> Log (str, next >> map fn)
        | HttpRequest (url, next) -> HttpRequest (url, next >> map fn)
        | Date (_, next) -> Date ((), next >> map fn)
        | Guid (_, next) -> Guid ((), next >> map fn)
        | Pure x -> Pure (fn x)
        
    // Helpers    

    let log str = Log (str, Pure)
    
    let guid () = Guid ((), Pure)
    
    let date () = Date ((), Pure)
    
    let httpRequest url : SideEffectAsync<_> = HttpRequest (url, Pure)
    