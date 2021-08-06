# HelseID machine to machine app
This app demonstrates how to use the client credentials flow to obtain an access token from HelseID, and use that access token to request a protected resource from an API. Since the access token is obtained with the client credentials flow it is an access token that authenticates a client, not a user.
As an extra precaution, this app uses client assertion to authenticate the client to HelseID, rather than using a secret shared with HelseID. Since we use a asymmetric key to sign the client assertion token we keep the private key only on the local machine, and store a public key at HelseID only to verify the signature.


## Installation
Make sure you have downloaded and installed [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)

Open the project folder in PowerShell and run

```
dotnet watch run
```

## Usage
The machine to machine call requires the [API](https://github.com/pellepingo/helseid-samples-fsharp/tree/main/HelseID%20API%20Giraffe) to be running
