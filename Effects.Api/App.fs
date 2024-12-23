namespace Effects.Api

open Oxpecker
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting

module App =

    let (!) = ignore

    let app =
        WebApplication
            .CreateBuilder()
            .Build()

    !app
        .UseRouting()
        .UseOxpecker(Routes.list)

    app.Run()