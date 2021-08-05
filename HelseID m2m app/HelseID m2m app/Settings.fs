module HelseID_m2m_app.Settings
type public Settings = 
    {
     Authority:                 string
     Audience:                  string
     ApiName:                   string
     ApiSecret:                 string
     Issuer_alternate_clientID: string
     Authorization_endpoint:    string
     Token_endpoint:            string
     End_session_endpoint:      string

     Client_id:                 string
     Client_secret:             string

     Scope:                     string
     }

let _Settings : Settings = 
    {
    Authority =                     "https://helseid-sts.utvikling.nhn.no"
    Audience =                      "https://helseid-sts.utvikling.nhn.no/connect/token"
    ApiName =                       "norsk-helsenett:fsharp-api"
    ApiSecret =                     ""


    Issuer_alternate_clientID =     "helseid-fsharp-m2m-app"  //In case you want to register with this
    Authorization_endpoint =        "https://helseid-sts.utvikling.nhn.no/connect/authorize"
    Token_endpoint =                "https://helseid-sts.utvikling.nhn.no/connect/token"
    End_session_endpoint =          "https://helseid-sts.utvikling.nhn.no/connect/endsession"

    //Client ID secret pair
    Client_id =                     "b2a6c103-7e3c-466c-8229-b47d38efc3bb"
    Client_secret =                 "VX_b60QKIymAdFdMu2n5333kZ7c1X0e-sypZp5ElEV2bBfq1VkujQudUv_modTjl" //OBSOLETE

    Scope = "norsk-helsenett:fsharp-api/hello norsk-helsenett:fsharp-api/v1/foo norsk-helsenett:fsharp-api/v1/bar"
    }

let JWK_PUBLIC_X509_PEM : string =         //DO NOT SAVE ANY OF THESE IN THE ACTUAL CODE. Save them externally and retrieve them/parse them in the code
    """
    -----BEGIN PUBLIC KEY-----
    MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsvxPXDd0B7ctbN7kT/Qd
    eEMZKgdISgGxIEzeXngziWxDWRcJGQlKaoXqV1J6LfwM/yvynLeJNyLqb2wfvzkq
    q3yiPsR/RUvvLXYMqmg2gE9SDC1kOzAEaPFv6QvxCAIMTmjTQXjdTH/KhDj8F/T1
    YWQSEYLOzMVxKTO0SToFQiFJ6+n8vWtak/c4m2b9SnMzSmZc/3+fXyo8gzip851R
    yZdfvGV5pHk6RyT98KV6IqJRJBmOxygiQUWk6OXqp7yeRD+wE/q5pvelI7nSuwA/
    nmjU4WxdmMEj2xH7FAWTrwpA9nLDyqO3uyvz/gwvzOegTysDwKn/NxvQyFakmup8
    0wIDAQAB
    -----END PUBLIC KEY-----
    """

let JWK_PRIVATE_X509_PEM : string =
    """
    -----BEGIN PRIVATE KEY-----
    MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCy/E9cN3QHty1s
    3uRP9B14QxkqB0hKAbEgTN5eeDOJbENZFwkZCUpqhepXUnot/Az/K/Kct4k3Iupv
    bB+/OSqrfKI+xH9FS+8tdgyqaDaAT1IMLWQ7MARo8W/pC/EIAgxOaNNBeN1Mf8qE
    OPwX9PVhZBIRgs7MxXEpM7RJOgVCIUnr6fy9a1qT9zibZv1KczNKZlz/f59fKjyD
    OKnznVHJl1+8ZXmkeTpHJP3wpXoiolEkGY7HKCJBRaTo5eqnvJ5EP7AT+rmm96Uj
    udK7AD+eaNThbF2YwSPbEfsUBZOvCkD2csPKo7e7K/P+DC/M56BPKwPAqf83G9DI
    VqSa6nzTAgMBAAECggEAR9n77dQhEzvY03zDAMnJzs542qNjxLnbJlFVb20nm0JH
    4wrZTyCorAyoX3evrqghe4pj8b/IKMcuf55TPEuxsnftFe+B/AqYsJJUUVBWmLm/
    AT5zn0MOciaCO3bcws83q35vWDgx9pTScrQBl4hxvTAUBM2TQSb934FvHXpaRAo1
    UJsLpU8E++0gqs9niII8vu9S1FO7qG2XtRzgvimSRSI8Mc5wbdHQU4k7R0O8x6Fs
    B8N9GHMPvugOWUXPvYmFRf7Dq/0jfdpm1lQ8Z4FWfVJo5HsuZVnoaiT/aU0NVziR
    RbbK70HstPcnxt7vK21fEdWPjLGDKYtUgvUnBEHxSQKBgQD33rKqTPyBrHh9RIoz
    h76vsNTohMJsQXuo7AKHAVt41ido9Jsj8LhMUUYgZBoAuHf1TCqp9ktkn06kinZz
    ENM72ar3FfhTkzAyjHqkqqfmhs6xY13+Ho02Jza/DgouvxfVhROyVF31jlAyNK6r
    WDH5/vsHO3dUP0HNBWE0u3WkrQKBgQC42zUe2DZPZyiDgZkg6GnAh9km1VgXD7Je
    v0QrV+YpkjIJK7J0LPRalqRx+61ElY1Fz9KW0RMm4NDu5kCYW8RfFV7ZGHftzGqk
    Gi4JkTVx2ZSv/gf2NR98rCVTr2A+qh8n4ZU9yzEoy1SzM0ZltrTT1J+P+9TqcJLB
    84lmPtNXfwKBgQC1pQqziKWG9fh6fG724mSYZfMwzpGYOcjAeuxDOXhqLi5FWPQU
    O4Uj0G+erxzrKEQoi/+7/BU2ERTVue6h1lOmSZZjakNII9YvRQlfgPx4EJSKJHKu
    Fn+Pjt8s1cIajcmOnO5ZQkB1Aiy9DiRuxrwhI32cyDRjldPHxkh9F19YJQKBgETe
    Zgv3wUQTyqQzxDjeXYe9FMnC8e6lim6CE4OSW81hCgMgpHtjxCV7ugg9G3BRxzAb
    HPnTKq9xUmWuoqIgjihebdezpfqSDajVmBE4aBDiXuKyYYT4haKM/9sNm229M55I
    DLHY4ZexKP0aPT/nsba5oDikOc6d4g8gDDBrd5FBAoGAXXJmcohAYHlrdKxrpO41
    1ujO+8m8FCQezMeP6vpkubt+qlNSfnqrHBiCS7biv9F0qmwGGxIw7tyIZoi0lYne
    m+36T+wsPm6TpIoweB+I3Vt6N+26vhoFa9zWqjrkfkG9hikXT2ZFGsxLf3GWDpFx
    o3HLH49fPOL8k1zGXS+4pxM=
    -----END PRIVATE KEY-----
    """

let JWK_public_key : string =
    """
    {
        "kty": "RSA",
        "e": "AQAB",
        "use": "sig",
        "kid": "K5UqpKdQQ3E/Ky12PzLdV043qh4=",
        "alg": "PS256",
        "n": "svxPXDd0B7ctbN7kT_QdeEMZKgdISgGxIEzeXngziWxDWRcJGQlKaoXqV1J6LfwM_yvynLeJNyLqb2wfvzkqq3yiPsR_RUvvLXYMqmg2gE9SDC1kOzAEaPFv6QvxCAIMTmjTQXjdTH_KhDj8F_T1YWQSEYLOzMVxKTO0SToFQiFJ6-n8vWtak_c4m2b9SnMzSmZc_3-fXyo8gzip851RyZdfvGV5pHk6RyT98KV6IqJRJBmOxygiQUWk6OXqp7yeRD-wE_q5pvelI7nSuwA_nmjU4WxdmMEj2xH7FAWTrwpA9nLDyqO3uyvz_gwvzOegTysDwKn_NxvQyFakmup80w"
    }
    """

let JWK_public_private_keyPair : string =
    """
    {
        "p": "996yqkz8gax4fUSKM4e-r7DU6ITCbEF7qOwChwFbeNYnaPSbI_C4TFFGIGQaALh39UwqqfZLZJ9OpIp2cxDTO9mq9xX4U5MwMox6pKqn5obOsWNd_h6NNic2vw4KLr8X1YUTslRd9Y5QMjSuq1gx-f77Bzt3VD9BzQVhNLt1pK0",
        "kty": "RSA",
        "q": "uNs1Htg2T2cog4GZIOhpwIfZJtVYFw-yXr9EK1fmKZIyCSuydCz0WpakcfutRJWNRc_SltETJuDQ7uZAmFvEXxVe2Rh37cxqpBouCZE1cdmUr_4H9jUffKwlU69gPqofJ-GVPcsxKMtUszNGZba009Sfj_vU6nCSwfOJZj7TV38",
        "d": "R9n77dQhEzvY03zDAMnJzs542qNjxLnbJlFVb20nm0JH4wrZTyCorAyoX3evrqghe4pj8b_IKMcuf55TPEuxsnftFe-B_AqYsJJUUVBWmLm_AT5zn0MOciaCO3bcws83q35vWDgx9pTScrQBl4hxvTAUBM2TQSb934FvHXpaRAo1UJsLpU8E--0gqs9niII8vu9S1FO7qG2XtRzgvimSRSI8Mc5wbdHQU4k7R0O8x6FsB8N9GHMPvugOWUXPvYmFRf7Dq_0jfdpm1lQ8Z4FWfVJo5HsuZVnoaiT_aU0NVziRRbbK70HstPcnxt7vK21fEdWPjLGDKYtUgvUnBEHxSQ",
        "e": "AQAB",
        "use": "sig",
        "kid": "K5UqpKdQQ3E/Ky12PzLdV043qh4=",
        "qi": "XXJmcohAYHlrdKxrpO411ujO-8m8FCQezMeP6vpkubt-qlNSfnqrHBiCS7biv9F0qmwGGxIw7tyIZoi0lYnem-36T-wsPm6TpIoweB-I3Vt6N-26vhoFa9zWqjrkfkG9hikXT2ZFGsxLf3GWDpFxo3HLH49fPOL8k1zGXS-4pxM",
        "dp": "taUKs4ilhvX4enxu9uJkmGXzMM6RmDnIwHrsQzl4ai4uRVj0FDuFI9Bvnq8c6yhEKIv_u_wVNhEU1bnuodZTpkmWY2pDSCPWL0UJX4D8eBCUiiRyrhZ_j47fLNXCGo3JjpzuWUJAdQIsvQ4kbsa8ISN9nMg0Y5XTx8ZIfRdfWCU",
        "alg": "PS256",
        "dq": "RN5mC_fBRBPKpDPEON5dh70UycLx7qWKboITg5JbzWEKAyCke2PEJXu6CD0bcFHHMBsc-dMqr3FSZa6ioiCOKF5t17Ol-pINqNWYEThoEOJe4rJhhPiFooz_2w2bbb0znkgMsdjhl7Eo_Ro9P-extrmgOKQ5zp3iDyAMMGt3kUE",
        "n": "svxPXDd0B7ctbN7kT_QdeEMZKgdISgGxIEzeXngziWxDWRcJGQlKaoXqV1J6LfwM_yvynLeJNyLqb2wfvzkqq3yiPsR_RUvvLXYMqmg2gE9SDC1kOzAEaPFv6QvxCAIMTmjTQXjdTH_KhDj8F_T1YWQSEYLOzMVxKTO0SToFQiFJ6-n8vWtak_c4m2b9SnMzSmZc_3-fXyo8gzip851RyZdfvGV5pHk6RyT98KV6IqJRJBmOxygiQUWk6OXqp7yeRD-wE_q5pvelI7nSuwA_nmjU4WxdmMEj2xH7FAWTrwpA9nLDyqO3uyvz_gwvzOegTysDwKn_NxvQyFakmup80w"
    }
    """

let JWK_public_private_keyPair_set :string = 
    """
    {
        "keys": [
            {
                "p": "996yqkz8gax4fUSKM4e-r7DU6ITCbEF7qOwChwFbeNYnaPSbI_C4TFFGIGQaALh39UwqqfZLZJ9OpIp2cxDTO9mq9xX4U5MwMox6pKqn5obOsWNd_h6NNic2vw4KLr8X1YUTslRd9Y5QMjSuq1gx-f77Bzt3VD9BzQVhNLt1pK0",
                "kty": "RSA",
                "q": "uNs1Htg2T2cog4GZIOhpwIfZJtVYFw-yXr9EK1fmKZIyCSuydCz0WpakcfutRJWNRc_SltETJuDQ7uZAmFvEXxVe2Rh37cxqpBouCZE1cdmUr_4H9jUffKwlU69gPqofJ-GVPcsxKMtUszNGZba009Sfj_vU6nCSwfOJZj7TV38",
                "d": "R9n77dQhEzvY03zDAMnJzs542qNjxLnbJlFVb20nm0JH4wrZTyCorAyoX3evrqghe4pj8b_IKMcuf55TPEuxsnftFe-B_AqYsJJUUVBWmLm_AT5zn0MOciaCO3bcws83q35vWDgx9pTScrQBl4hxvTAUBM2TQSb934FvHXpaRAo1UJsLpU8E--0gqs9niII8vu9S1FO7qG2XtRzgvimSRSI8Mc5wbdHQU4k7R0O8x6FsB8N9GHMPvugOWUXPvYmFRf7Dq_0jfdpm1lQ8Z4FWfVJo5HsuZVnoaiT_aU0NVziRRbbK70HstPcnxt7vK21fEdWPjLGDKYtUgvUnBEHxSQ",
                "e": "AQAB",
                "use": "sig",
                "kid": "K5UqpKdQQ3E/Ky12PzLdV043qh4=",
                "qi": "XXJmcohAYHlrdKxrpO411ujO-8m8FCQezMeP6vpkubt-qlNSfnqrHBiCS7biv9F0qmwGGxIw7tyIZoi0lYnem-36T-wsPm6TpIoweB-I3Vt6N-26vhoFa9zWqjrkfkG9hikXT2ZFGsxLf3GWDpFxo3HLH49fPOL8k1zGXS-4pxM",
                "dp": "taUKs4ilhvX4enxu9uJkmGXzMM6RmDnIwHrsQzl4ai4uRVj0FDuFI9Bvnq8c6yhEKIv_u_wVNhEU1bnuodZTpkmWY2pDSCPWL0UJX4D8eBCUiiRyrhZ_j47fLNXCGo3JjpzuWUJAdQIsvQ4kbsa8ISN9nMg0Y5XTx8ZIfRdfWCU",
                "alg": "PS256",
                "dq": "RN5mC_fBRBPKpDPEON5dh70UycLx7qWKboITg5JbzWEKAyCke2PEJXu6CD0bcFHHMBsc-dMqr3FSZa6ioiCOKF5t17Ol-pINqNWYEThoEOJe4rJhhPiFooz_2w2bbb0znkgMsdjhl7Eo_Ro9P-extrmgOKQ5zp3iDyAMMGt3kUE",
                "n": "svxPXDd0B7ctbN7kT_QdeEMZKgdISgGxIEzeXngziWxDWRcJGQlKaoXqV1J6LfwM_yvynLeJNyLqb2wfvzkqq3yiPsR_RUvvLXYMqmg2gE9SDC1kOzAEaPFv6QvxCAIMTmjTQXjdTH_KhDj8F_T1YWQSEYLOzMVxKTO0SToFQiFJ6-n8vWtak_c4m2b9SnMzSmZc_3-fXyo8gzip851RyZdfvGV5pHk6RyT98KV6IqJRJBmOxygiQUWk6OXqp7yeRD-wE_q5pvelI7nSuwA_nmjU4WxdmMEj2xH7FAWTrwpA9nLDyqO3uyvz_gwvzOegTysDwKn_NxvQyFakmup80w"
            }
        ]
    }
    """

let JWK_self_signed_certificate : string = 
    """
    -----BEGIN CERTIFICATE-----
    MIIC1DCCAbygAwIBAgIGAXsGBgnHMA0GCSqGSIb3DQEBCwUAMCsxKTAnBgNVBAMM
    IEs1VXFwS2RRUTNFJTJGS3kxMlB6TGRWMDQzcWg0JTNEMB4XDTIxMDgwMjA4NDEw
    NFoXDTIyMDUyOTA4NDEwNFowKzEpMCcGA1UEAwwgSzVVcXBLZFFRM0UlMkZLeTEy
    UHpMZFYwNDNxaDQlM0QwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCy
    /E9cN3QHty1s3uRP9B14QxkqB0hKAbEgTN5eeDOJbENZFwkZCUpqhepXUnot/Az/
    K/Kct4k3IupvbB+/OSqrfKI+xH9FS+8tdgyqaDaAT1IMLWQ7MARo8W/pC/EIAgxO
    aNNBeN1Mf8qEOPwX9PVhZBIRgs7MxXEpM7RJOgVCIUnr6fy9a1qT9zibZv1KczNK
    Zlz/f59fKjyDOKnznVHJl1+8ZXmkeTpHJP3wpXoiolEkGY7HKCJBRaTo5eqnvJ5E
    P7AT+rmm96UjudK7AD+eaNThbF2YwSPbEfsUBZOvCkD2csPKo7e7K/P+DC/M56BP
    KwPAqf83G9DIVqSa6nzTAgMBAAEwDQYJKoZIhvcNAQELBQADggEBAEVjDPSDL7UF
    CncKslB1E72fM1IlwqZfcdD/iLyuJrNdtotbQESPGGHNyH32KFyLAujyUY3P7fmo
    jy4Ifqz38mqCQaUB1Xq2LNv58NcgzN5ngdAHn/46OWp/EH6W6RomCkVNOzaKspYq
    XMRRycTRGVFhCA3FfccFS3FWwz/WVmTPzDt118XwE4oeF3pL0UsEGGdy+sRpV4Zs
    g9Jic+VNZi7cXlNR6X0PHP+VfqkYzrCljTh9CjkbBudxVtCIKiWRjuGlMFn31bne
    P+Z7oyTYBoDeHoV7PU0XhX5IfR0hFA5fJDVwkA3bn4EE0kusAgVauI+kzbiVg4kU
    X5QYAOHWQ8k=
    -----END CERTIFICATE-----
    """
