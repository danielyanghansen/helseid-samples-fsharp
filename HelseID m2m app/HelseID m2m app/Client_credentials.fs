module HelseID_m2m_app.Client_credentials

open FSharp.Data
open Microsoft.IdentityModel.Tokens
open Microsoft.IdentityModel.JsonWebTokens

open System.Security.Claims
open System.Collections.Generic
open System
open System.Net

open Newtonsoft.Json.Linq


open HelseID_m2m_app.Settings



let settings = Settings._Settings

let generateRandomString(length: int) : string = 
    let r = Random()
    let chars = Array.concat([[|'a' .. 'z'|];[|'A' .. 'Z'|];[|'0' .. '9'|]])
    let sz = Array.length chars in
    String(Array.init length (fun _ -> chars.[r.Next sz]))


let generateClientAssertionToken() : string = 

    let alg = SecurityAlgorithms.RsaSsaPssSha256   //"PS256"    //should get alg from header

    let privateKey = new JsonWebKey(Settings.JWK_public_private_keyPair)        //Could also just genereate an entirely new JWK from scratch, but might as well use a pregenerated JWK


    let identityClaim = new Claim("sub", "helseid-fsharp-m2m-app")      //some random stuff

    let identityClaimList =
        [|
        identityClaim
        |]
    
    let audienceList = new List<string>()
    audienceList.Add settings.Audience
    let jti = generateRandomString(24)

    let claimsDictionary = new Dictionary<string, obj>()
    claimsDictionary.Add("aud", audienceList)
    //claimsDictionary.Add("grant_type","client_credentials")
    claimsDictionary.Add ("jti", jti)
    claimsDictionary.Add("sub", settings.Client_id)
    claimsDictionary.Add("iss", settings.Client_id)



    //Token Creation
    let handler = new JsonWebTokenHandler()
    let now = DateTime.UtcNow
    let descriptor =
        new SecurityTokenDescriptor(                    
            Claims = claimsDictionary,
            IssuedAt = now,
            NotBefore = now,
            Expires = now.AddMinutes(5.),
            SigningCredentials = new SigningCredentials(privateKey, alg)               
        )
                

    let clientAssertionJwt = handler.CreateToken(descriptor)   

    let extratoken = handler.ReadJsonWebToken clientAssertionJwt
    extratoken.Alg |> ignore
    //Token Validation
    let result : TokenValidationResult =
        handler.ValidateToken(
            clientAssertionJwt, 
            new TokenValidationParameters(ValidIssuer = settings.Client_id, ValidAudience = settings.Audience, ValidateAudience = true, IssuerSigningKey = privateKey)
            )

    clientAssertionJwt

let requestAuthenticationToken (clientAssertionToken: string) : string =
    
    let authToken =
        let response : string =
            Http.RequestString(
                settings.Token_endpoint,
                body =  
                    FormValues [
                        "grant_type", "client_credentials";
                        "client_assertion", clientAssertionToken;
                        "client_assertion_type" , "urn:ietf:params:oauth:client-assertion-type:jwt-bearer";
                        "scope", settings.Scope;
                        "client_id", settings.Client_id
                    ]
                )
        response

        //TODO: Handle error responses.
        //Alternative: Make authToken monadic?
    authToken

let parseResponseJson (json:string) : IDictionary<string,string> = //THERE IS SOMETHING WRONG HERE
    let data = 
        System.Text.Json.JsonDocument.Parse(json)
            .RootElement
            .EnumerateObject()
        |> Seq.map (fun o -> o.Name, o.Value.GetString())
        |> dict
    data

let requestWithBearerToken (apiAdress:string) (bearerAuthToken:string) : string = 
    let response =
        try
            Http.RequestString(
                           apiAdress,
                           headers = ["Authorization", bearerAuthToken]
                           )
        with
        | exn -> () |> exn.ToString

    response

let jObjFromJson (json:string) : JObject = 
    JObject.Parse(json)

let getTokenFromJObj (jobj : JObject): string =
    () |> jobj.Item("access_token").ToString