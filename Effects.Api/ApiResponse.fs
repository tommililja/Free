namespace Effects.Api

open Oxpecker
open Thoth.Json.Net

type ApiResponse =
    | Json of JsonValue
    | InternalServerError

module ApiResponse =

    let private json jsonValue =
        let json = Encode.toString 4 jsonValue
        setHttpHeader "Content-Type" "application/json"
        >=> text json

    let handle = function
        | Json x -> json x
        | InternalServerError -> setStatusCode 500
