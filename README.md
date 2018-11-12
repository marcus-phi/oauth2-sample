## OAuth 2.0 Sample

A working setup to learn about OAuth 2.0

#### Projects:

- **OAuth2.IdentityServer**: authenticate user and return access token
- **OAuth2.WebApi**: require access token to authorize user to return data
- **OAuth2.ClientApp**: take user credentials to request token from IdentityServer, then pass it to WebApi to get data

#### Build & Run:

- Build: `docker-compose build`
- Run: `docker-compose up`
