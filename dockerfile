FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
COPY DarkkestP3.API /app/DarkkestP3.API
RUN dotnet publish DarkkestP3.API -c Release -o dist

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as run
ENV ASPNETCORE_URLS=http://*:80

WORKDIR /app

COPY --from=build /app/dist .
CMD [ "dotnet", "DarkkestP3.API.dll" ]