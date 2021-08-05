module HelseID_m2m_app.Startup

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open System.Collections.Generic

open HelseID_m2m_app.Client_credentials

type Startup() =
    
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    member _.ConfigureServices(services: IServiceCollection) =
        ()

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member _.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if env.IsDevelopment() then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseRouting()
           .UseEndpoints(fun endpoints ->
                endpoints.MapGet("/", fun context ->
                    let clientAssertionToken = generateClientAssertionToken()
                    let authenticationResponse = clientAssertionToken |> requestAuthenticationToken
                    let badResponse = requestWithBearerToken "https://localhost:5001/api/hello" "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c" 
                    let bearerToken = authenticationResponse |> jObjFromJson |> getTokenFromJObj                    
                    let goodResponse = requestWithBearerToken "https://localhost:5001/api/hello" ("Bearer " + bearerToken)

                    context.Response.WriteAsync("This is the client assertions token: \r\r") |> ignore
                    context.Response.WriteAsync(clientAssertionToken) |> ignore
                    context.Response.WriteAsync("\r\rThe token is used to request an Authorization bearer token. \r\rThis is the Authorization bearer token: \r\r") |> ignore
                    context.Response.WriteAsync(authenticationResponse) |> ignore
                    context.Response.WriteAsync("\r\rThis is the response from an Athorized request to the API: \r\r") |> ignore
                    context.Response.WriteAsync(goodResponse) |> ignore
                    context.Response.WriteAsync("\r\rThis is the response from an Unauthorized request to the API: \r\r") |> ignore
                    context.Response.WriteAsync(badResponse) |> ignore
                    context.Response.WriteAsync("\r\r\r\rRefresh page to repeat the process.")
                    
                    ) |> ignore
                    
            ) |> ignore
