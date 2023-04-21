namespace SideEffects.Api

open FSharp.Data
open System
    
type private Provider = JsonProvider<"appsettings.json">
    
module AppSettings =
  
    let private appSettings = Provider.Load("appsettings.json")
    
    let catFactsUrl = Uri appSettings.CatFactsUrl
    