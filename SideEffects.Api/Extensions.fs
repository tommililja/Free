namespace SideEffects.Api

open SideEffects.Monad
open System
open System.Net.Http
open System.Text.Json

module JsonSerializer =
    
    let private options = JsonSerializerOptions(
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    )
    
    let deserialize<'a> (json:JsonDocument) =
        JsonSerializer.Deserialize<'a>(json, options)
        
module HttpClient =
    
    let getJsonAsync (httpClient:HttpClient) (url:Uri) =
        httpClient.GetStringAsync url
        |> Async.AwaitTask
        |> Async.map JsonDocument.Parse
    