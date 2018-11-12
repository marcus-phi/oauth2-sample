FROM microsoft/dotnet:sdk AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:aspnetcore-runtime
EXPOSE 80
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "OAuth2.WebApi.dll"]