namespace HelseID_m2m_app

open System
open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging

open HelseID_m2m_app.Startup

//Note: Logic in ???.fs, use of logic in Startup.fs under index page routing

module Program =
    let createHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webBuilder ->
                webBuilder.UseStartup<Startup>() |> ignore
            )
    [<EntryPoint>]
    let main args =
        
        createHostBuilder(args).Build().Run()
        

        0 // Exit code

