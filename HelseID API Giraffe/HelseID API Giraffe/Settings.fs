
module HelseID_API_Giraffe.Settings


open System

type public Settings = 
    {
     Authority: string
     Audience: string
     ApiName: string
     ApiSecret: string
     Issuer: string
     }

let _Settings : Settings = 
    {
    Authority = "https://helseid-sts.utvikling.nhn.no"
    Audience = "norsk-helsenett:fsharp-api"
    ApiName = "norsk-helsenett:fsharp-api"
    ApiSecret = ""
    Issuer = "https://helseid-sts.utvikling.nhn.no"
    }