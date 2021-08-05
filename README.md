# HelseID Samples in F#
HelseID is a national authentication service for the health sector in Norway. These samples are targeted at technical personnel such as application architects and developers on how to implement HelseID in F# applications. The samples consist of 3 applications.

## [Web App](HelseID WebApp)
The web app demonstrates the authorization code flow (OpenID Connect). It uses the id token to verify the identity of the user.

## [Machine to machine](HelseID m2m app)
The m2m app uses the client credentials flow (OAuth 2.0) to get an access token, and uses the access token to retrieve a resource from the [API]("HelseID API Giraffe").

## [API](HelseID API Giraffe)
The API provides simple mock data protected with HelseID. To retrive data the user must provide a valid access token issued by HelseID in the header of a GET request.


More info on https://nhn.no/helseid/ (Norwegian) and https://dokumentasjon.helseid.no/
