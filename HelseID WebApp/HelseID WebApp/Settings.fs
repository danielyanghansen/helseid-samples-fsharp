
module HelseID_WebApp.Settings

type public Settings = //Couldn't figure out how to get the settings from the json file, so they're here for now
    { Authority: string
      ClientId: string
      Scope: string
      ClientSecret: string
      ResponseType: string
      DefaultChallengeScheme: string
      SignInScheme: string
      SignOutScheme: string
      RedirectUri: string
      SignedOutRedirectUri: string
      PublicJWK: string
      HostBaseUrl: string
    }
let _Settings : Settings = 
    { 
      Authority = "https://helseid-sts.utvikling.nhn.no"
      ClientId = "3fcb02f4-1f53-4e75-b51a-29f7315319fb"  
      ClientSecret = "J6jXobPJG5-O3L-e6SYgb9cGkAmPXXWS5O3XCLoz__LU6ot9eXXP76d_Zq6Petyx" 
      DefaultChallengeScheme = "oidc"
      ResponseType = "code"
      RedirectUri = "https://localhost:44388/signin-oidc" // Important note: The giraffe Framwork automatically creates this endpoint which handles the response from HelseID oidc
      Scope = "openid profile"
      SignInScheme = "Cookies"
      SignOutScheme = "Cookies"
      SignedOutRedirectUri = "https://localhost:44388"
      HostBaseUrl = "https://localhost:44388"
      

      //Currently using ClientId and ClientSecret instead of asymmetric client assertion. Public JWK only here for reference. Generate your own PrivateJWK if you want client assertion auth.
      PublicJWK = //change this if necessary. Currently info from well https://helseid-sts.utvikling.nhn.no/.well-known/openid-configuration/jwks, dated 19.07.2021. Otherwise download the data on each launch
          """{
          "keys": [
          {
          "kty": "RSA",
          "use": "sig",
          "kid": "B4CAE452C8B6A893B6A840AC8C84FB070A426E41",
          "x5t": "tMrkUsi2qJO2qECsjIT7BwpCbkE",
          "e": "AQAB",
          "n": "puikxvVtwBiWAPJSFKWYkP928zON_qYZe4iVqx9Qkea9j4Gt2XhK9pwBtQOjaZPY9WZuU83POsqTDYcNfFtTAcBkrXPSKEN4E9SAngrX3GlL0wdqVLLraJirv9MziqasH4hsOPQmdCu9NNH5Q98QnkhDZIseRcpNiUrG6QRSwcVwB-GJaqraA5m97Xy18UgDgwLobpf7hTTxAsXReuvuKuLISCBn3oi54yK73UZ3-VVed8BCeL56hZynY7hcHMRGzM6s3fOrvANBOAzBitsHtLIjSAek4PBlc3zXLZvQJMUMd7XOI8vB72CDWWXeOes9UQkynZglkNUoyUfylmpbgw",
          "x5c": [
          "MIIFJjCCBA6gAwIBAgILAn8/xMfmR7nT/g4wDQYJKoZIhvcNAQELBQAwUTELMAkGA1UEBhMCTk8xHTAbBgNVBAoMFEJ1eXBhc3MgQVMtOTgzMTYzMzI3MSMwIQYDVQQDDBpCdXlwYXNzIENsYXNzIDMgVGVzdDQgQ0EgMzAeFw0yMDEwMjkxMDQ4MjlaFw0yMzEwMjkyMjU5MDBaMG0xCzAJBgNVBAYTAk5PMRswGQYDVQQKDBJOT1JTSyBIRUxTRU5FVFQgU0YxEDAOBgNVBAsMB0hlbHNlSUQxGzAZBgNVBAMMEk5PUlNLIEhFTFNFTkVUVCBTRjESMBAGA1UEBRMJOTk0NTk4NzU5MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEApuikxvVtwBiWAPJSFKWYkP928zON/qYZe4iVqx9Qkea9j4Gt2XhK9pwBtQOjaZPY9WZuU83POsqTDYcNfFtTAcBkrXPSKEN4E9SAngrX3GlL0wdqVLLraJirv9MziqasH4hsOPQmdCu9NNH5Q98QnkhDZIseRcpNiUrG6QRSwcVwB+GJaqraA5m97Xy18UgDgwLobpf7hTTxAsXReuvuKuLISCBn3oi54yK73UZ3+VVed8BCeL56hZynY7hcHMRGzM6s3fOrvANBOAzBitsHtLIjSAek4PBlc3zXLZvQJMUMd7XOI8vB72CDWWXeOes9UQkynZglkNUoyUfylmpbgwIDAQABo4IB4TCCAd0wCQYDVR0TBAIwADAfBgNVHSMEGDAWgBQ/rvV4C5KjcCA1X1r69ySgUgHwQTAdBgNVHQ4EFgQUv0MGR18TeeqT2FOnaLH/vuMcmEkwDgYDVR0PAQH/BAQDAgZAMB0GA1UdJQQWMBQGCCsGAQUFBwMCBggrBgEFBQcDBDAWBgNVHSAEDzANMAsGCWCEQgEaAQADAjCBuwYDVR0fBIGzMIGwMDegNaAzhjFodHRwOi8vY3JsLnRlc3Q0LmJ1eXBhc3Mubm8vY3JsL0JQQ2xhc3MzVDRDQTMuY3JsMHWgc6Bxhm9sZGFwOi8vbGRhcC50ZXN0NC5idXlwYXNzLm5vL2RjPUJ1eXBhc3MsZGM9Tk8sQ049QnV5cGFzcyUyMENsYXNzJTIwMyUyMFRlc3Q0JTIwQ0ElMjAzP2NlcnRpZmljYXRlUmV2b2NhdGlvbkxpc3QwgYoGCCsGAQUFBwEBBH4wfDA7BggrBgEFBQcwAYYvaHR0cDovL29jc3AudGVzdDQuYnV5cGFzcy5uby9vY3NwL0JQQ2xhc3MzVDRDQTMwPQYIKwYBBQUHMAKGMWh0dHA6Ly9jcnQudGVzdDQuYnV5cGFzcy5uby9jcnQvQlBDbGFzczNUNENBMy5jZXIwDQYJKoZIhvcNAQELBQADggEBALzmOwJvN9LuNgS9LrbfQf2ydr/ig6smqH0AU8fkA+9z6oGL6jhfI9umtfC39PlalVlfSlVcB2EVOcXTdjxsTrVbtb1kb2wOC7gC/cj7b/acpehGQMxk3jvMgQfDAJg5UIbyi5vrDi80YoxBpwge12OGn7BgLFeQh+KUQ1I9XGBujJfzpYyk1fTka941fCm8XGNa4Z8TiMktXtoAWeAXZB5juvCoO32n3WlYMJvUZKv9gFipYbvzDLXgTvxQfK2Hku8HeN7khwCGL8KyU8D3dYzFsMrELNueaouPzoTWHwIqzkY3ARzzJ+EZDK4VXuAbmLliVOgJmMLgkP5gJNPAf9w="
          ],
          "alg": "RS256"
          }
          ]
          }"""
     }