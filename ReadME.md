dotnet ef migrations --project Orders.DataAccess --startup-project Orders.Api add <name of migration>

dotnet ef migrations remove

dotnet ef database update --project Orders.DataAccess --startup-project Orders.Api