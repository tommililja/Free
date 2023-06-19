namespace SideEffects.Api

open System
open FSharp.Data
    
type private Provider = JsonProvider<"appsettings.json">
    
module AppSettings =
  
    let private appSettings = Provider.Load("appsettings.json")
    
    let catFactsUrl = Uri appSettings.CatFactsUrl
    