## This is how we setup the project so far.
- dotnet new webapi --name PokemonTeamBuilder.API -o ./PokemonTeamBuilder.API
- dotnet add package Microsoft.EntityFrameworkCore.SqlServer
- dotnet add package Microsoft.EntityFrameworkCore.Design
- dotnet add package Microsoft.EntityFrameworkCore.Tools
- dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore

## To run
- dotnet run

## Create initial user secrets vault 
- dotnet user-secrets init
- dotnet user-secrets list
- dotnet user-secrets set {Name of secret} {value of secret}
- dotnet user-secrets remove {name of secret}

## Azure connection string flatten
- dotnet user-secrets set {ConnectionStrings:{name of db}} {ConnectionString}

## Implementing a new feature
- git pull
- git checkout -b nameOfMybranch
- *do coding work
- git add {dir,files, or all}
- git commit -m "commit message"
- git push --set-upstream origin nameOfMybranch

## To get back to main
- git checkout main

#### Run the below commands to make migrations
- dotnet ef database update --context {DBContext}

#### Add a new migration
- dotnet ef migrations add {NameofMigration} --context {DBContext}

#### Undo all database updates (migrations)
- dotnet ef database update 0 --context {DBContext}

#### Remove last migration made
- dotnet ef migrations remove --context {DBContext}

#### Common DB commands to reset DB
- dotnet ef database update 0 --context CommunityDBContext; dotnet ef database update --context CommunityDBContext
- dotnet ef database update 0 --context UserDBContext; dotnet ef database update --context UserDBContext

dotnet ef migrations add InitialCreate --context CommunityDBContext;dotnet ef migrations add InitialCreate --context UserDBContext

dotnet ef database update --context CommunityDBContext;dotnet ef database update --context UserDBContext