namespace Effects.Api

open System
open FSharp.Data

open Constants

type private Provider = JsonProvider<AppSettings>

module AppSettings =

    let private appSettings = Provider.Load(AppSettings)

    let catFactsUrl = Uri appSettings.CatFactsUrl
