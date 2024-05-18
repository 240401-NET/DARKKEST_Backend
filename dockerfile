# Start with .NET SDK 8 image
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build

# Set my word dir to /app so everything happens here within the image
# Will make /app if it does not exist
WORKDIR /app

# Copy over the API project
COPY DarkkestP3.API /app/DarkkestP3.API

# Run dotnet publish and make artifact
RUN dotnet publish DarkkestP3.API -c Release -o dist

# Start a new layer from the .NET 8 runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as run

# Tells aspnet to serve app over port 80
ENV ASPNETCORE_URLS=http://*:80

# Set work dir to /app again
WORKDIR /app

# Copy over the application artifact from /app/dist
COPY --from=build /app/dist .

# When "docker run" is called, execute "dotnet DarkkestP3.API.dll"
CMD [ "dotnet", "DarkkestP3.API.dll" ]