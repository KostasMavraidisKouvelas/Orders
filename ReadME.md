dotnet ef migrations --project Orders.DataAccess --startup-project Orders.Api add <name of migration>

dotnet ef migrations remove

dotnet ef database update --project Orders.DataAccess --startup-project Orders.Api

Change the connection strings to the name of your pc.
Create the hangfire database in your local sql server.
Update the database with the following command:
dotnet ef database update --project Orders.DataAccess --startup-project Orders.Api
Run appication
```