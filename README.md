# NourishNexus
Design and Implementation of a Personalized Meal Planning Webapp with Nutritional Balance as the Key Feature

## Resources

Google drive: https://drive.google.com/drive/folders/1asFmWhll3NPPb0TKMrkeHLnq51C29-cM

LaTex report: https://www.overleaf.com/read/mvvsfbyktffz

Azure DevOps: https://dev.azure.com/NourishNexus/NourishNexus



## How to run

Make sure you are in the server folder

Create a database in a docker container with the following command, where {password} is replaced with a password for the database

```docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD={password}" -e "MSSQL_PID=Developer" -e "MSSQL_USER=SA" -p 1433:1433 -d --name=NourishNexusDatabase mcr.microsoft.com/azure-sql-edge```


Add the details in ``server/appsettings.json`` like below:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "NourishNexus": "Server=localhost,1433;Database=NourishNexusDatabase;User Id=sa;Password={password};TrustServerCertificate=true"
  },
  "Jwt": {
    "Secret": "{secret}"
  }
}
```


Create/Update the database with the following command:

```dotnet ef database update```


Run the server with:

```dotnet run```

Go to the client folder and run the client with:
```dotnet run```

