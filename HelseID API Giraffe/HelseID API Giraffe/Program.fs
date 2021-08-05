module HelseID_API_Giraffe.App

open System
open System.IdentityModel.Tokens
open System.IdentityModel.Tokens.Jwt
open System.Collections.Generic
open System.Linq  
open System.Threading.Tasks
open System.Security.Cryptography
open System.Text
open System.Text.RegularExpressions

open Microsoft.AspNetCore.Authentication
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.HttpsPolicy

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.AspNetCore.ApiAuthorization.IdentityServer
open Microsoft.AspNetCore.Cors
open Microsoft.AspNetCore.Cors.Infrastructure

open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection

open Microsoft.IdentityModel.JsonWebTokens
open Microsoft.IdentityModel.Tokens

open IdentityServer4
open IdentityServer4.AccessTokenValidation


open FSharp.Control.Tasks.V2.ContextInsensitive          //This needs IdentityModel4
open Giraffe
open Giraffe.ViewEngine

open HelseID_API_Giraffe.Settings

open HelseID_API_Giraffe.HelseID_public_key


let settings = Settings._Settings       //opening configurable settings. Not as json, but this can be changed (to get settings from appsettings.json)

// ---------------------------------
// Models
// ---------------------------------
module Models =

    [<CLIMutable>]
    type Message =
        {
            Text : string
        }

// ---------------------------------
// Base Logic
// ---------------------------------
module Constants = 
    let apiName = settings.ApiName
    let apiSecret = settings.ApiSecret      //Secret, Audience, And Authority might not be needed
    let audience = settings.Audience
    let authority = settings.Authority
    let issuer = settings.Issuer

    let assurance_level = "high"
    let security_level = "4"

    let alternate_assurance_level = "substantial"
    let alternate_security_level = "3"

    //You can download the public key from HelseID Well Known, or you can use the static version
    let publicKeyJson = HelseID_public_key._PublicKeyJson   //static
    //let publicKeyJson = HelseID_public_key.getPublicKeyJson()  //This is the download TODO: Parse the download to return the key instead of a list of the key. See HelseID_public_keys.fs

// ---------------------------------
// Web app
// ---------------------------------


module Handlers =
    let setHttpHeader key value : HttpHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            ctx.SetHttpHeader (key , value)
            next ctx


    let challenge (scheme : string) (redirectUri : string) : HttpHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let prop = new AuthenticationProperties(RedirectUri = redirectUri)
            
                do! ctx.ChallengeAsync(
                        scheme,
                        prop)

                return! next ctx
            }
    let handleGetHello =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let response : Models.Message = {
                    Text = "Hello world, from Giraffe! This is a protected message"
                }
                return! json response next ctx
                
                
            }

    let getTokenFromHeaderAndValidate : HttpHandler =       //NOTE: Improper error handling
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                
                let handler = new JsonWebTokenHandler()
                //Todo:
                let authHeader : string =
                    match ctx.GetRequestHeader("Authorization") with
                     | Ok str -> str
                     | Error msg -> "error: GetRequestHeader"
                 
                 //if authHeader.Equals("error:GetRequestHeader") then return! ServerErrors.INTERNAL_ERROR "Couldn't request header" next ctx
                

                let authHeaderParts : string array =
                    authHeader.Split [|' '|]
                
                //if authHeaderParts.Length <> 2 || authHeaderParts.[0] <> "bearer" then return! ServerErrors.INTERNAL_ERROR "no bearer token" next ctx
                let tokenString =
 //                   try 
                        match authHeaderParts.[0] with
                        | "Bearer" -> authHeaderParts.[1]
                        | "bearer" -> authHeaderParts.[1]
                        | _        -> "notABearerToken"
 //                   with
//                    | exn -> "notABearerToken"
                
                
                let checkedAlg = "RS256" //handler.ReadJsonWebToken(tokenString).Alg


                //Uses the public JWK Json file to create a JWK. This JWK will bu used by the tokenhandler to validate the JWT
                let publicJwk = new JsonWebKey(Constants.publicKeyJson)

                publicJwk.Alg <-
                    match checkedAlg with
                    | "RS256" -> checkedAlg
                    | "PS256" -> checkedAlg
                    | _       -> HelseID_public_key._PublicKeyObject._alg         //currently defaults to RS256

                //Verify claims in the access token:
                //exp (hasn't expired)                      X
                //nbf (is "active")
                //iss (you trust the issuer)                X
                //aud (the token is intended for you)       X
                //scopes (access control)
                //Claims:
                //  “helseid://claims/identity/assurance_level“, 
                //            alternatively  “helseid://claims/identity/security_level”  (corresponds with the requirements for your API)
                //  ? Verify "helseid://claims/identity/pid"
                //  ? Verify"helseid://claims/hpr/hpr_number"
                //  ? Verify organizational identifiers, e.g “helseid://claims/client/claims/orgnr_parent“ and “helseid://claims/client/claims/orgnr_child“
                                
                //Token Validation: Validate 
                let result : TokenValidationResult =
                    handler.ValidateToken(
                        tokenString, 
                        new TokenValidationParameters(
                            ValidIssuer = Constants.issuer,
                            ValidAudience = Constants.audience,
                            ValidateAudience = true,
                            ValidateLifetime = true ,
                            ValidateIssuer = true,
                             
                            IssuerSigningKey = publicJwk
                            )
                    )

                match (result.IsValid) with 
                |true ->
                    return! next ctx
                |false -> 
                    return! RequestErrors.UNAUTHORIZED "HTTPS" " " "Authorization token was not properly verified" next ctx
                
             }



    let webApp : HttpHandler=

        choose [
            GET >=>
                choose [
                    route "/" >=> text "The API is now running from Giraffe"
                    routeStartsWithCi "/api/" >=>
                    getTokenFromHeaderAndValidate >=> 
                    //Pre-filter because only API calls require Auth

                    
                        choose [
                            //alternatively per endpoint:
                            // route "/api/x" >=> validateScope ("norsk-helsenett:fsharp-api/x") >=> handleGetX 
                            route "/api/v1/foo" >=> text "Foo"
                            route "/api/v1/bar" >=> text "Bar"
                            route "/api/hello"  >=> handleGetHello
                        ]
                ]
        ]

    let errorHandler (ex : Exception) (logger : ILogger) =
        logger.LogError(EventId(), ex, "An unhandled exception has occurred while executing the request.")
        clearResponse >=> text ex.Message

// ---------------------------------
// Config and Main
// ---------------------------------

//Config has been done in a functional way. Can be also be done by plugging the web app into the ASP.NET middleware. See https://github.com/giraffe-fsharp/Giraffe/blob/master/DOCUMENTATION.md#basics

let configureCors (builder : CorsPolicyBuilder) =
    builder.WithOrigins("http://localhost:5003")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .WithExposedHeaders"WWW-Authenticate"
           |> ignore

let configureApp (app : IApplicationBuilder) =
    
        
    
    app.UseGiraffeErrorHandler(Handlers.errorHandler)
       .UseHttpsRedirection()
       .UseStaticFiles()
       .UseRouting()
       //.UseAuthentication()
       //.UseAuthorization()
       .UseSession()
       .UseResponseCaching()
       .UseCors(configureCors)    
       .UseGiraffe Handlers.webApp      //If you are going to use MVC, you can configure it with .UseMvc instead
       

let configureServices (services : IServiceCollection) =
    
    //adding response caching
    services.AddCors() |> ignore
    services.AddResponseCaching()
            .AddGiraffe()
            |> ignore
    services.AddDistributedMemoryCache() |> ignore
    services.AddSession() |> ignore

    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear() |> ignore
           

    // Add framework services. 
    services.AddGiraffe() |> ignore

                
let configureLogging (builder : ILoggingBuilder) =
    let filter (l : LogLevel) = l.Equals LogLevel.Error
    builder.AddFilter(filter)
           .AddConsole()
           .AddDebug()
    |> ignore

// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.      
[<EntryPoint>]
let main _ =
    Console.Title <- "Sample API"
    Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder
                    //.UseKestrel()
                    //.UseIISIntegration()
                    .Configure(configureApp)
                    .ConfigureServices(configureServices)
                    .ConfigureLogging(configureLogging)
                    |> ignore)
        .Build()
        .Run()
    0

