docker-compose up -d

dotnet-ef database drop -f -c PetFamily.Volunteers.Infrastructure.DbContexts.WriteDbContext -p .\src\Volunteers\PetFamily.Volunteers.Infrastructure\ -s .\src\PetFamily.Web\
dotnet-ef database drop -f -c PetFamily.Species.Infrastructure.DbContexts.WriteDbContext -p .\src\Species\PetFamily.Species.Infrastructure\ -s .\src\PetFamily.Web\
dotnet-ef database drop -f -c PetFamily.Accounts.Infrastructure.DbContexts.AccountsDbContext -p .\src\Accounts\PetFamily.Accounts.Infrastructure\ -s .\src\PetFamily.Web\

dotnet-ef migrations remove -c PetFamily.Volunteers.Infrastructure.DbContexts.WriteDbContext -p .\src\Volunteers\PetFamily.Volunteers.Infrastructure\ -s .\src\PetFamily.Web\
dotnet-ef migrations remove -c PetFamily.Species.Infrastructure.DbContexts.WriteDbContext -p .\src\Species\PetFamily.Species.Infrastructure\ -s .\src\PetFamily.Web\
dotnet-ef migrations remove -c PetFamily.Accounts.Infrastructure.DbContexts.AccountsDbContext -p .\src\Accounts\PetFamily.Accounts.Infrastructure\ -s .\src\PetFamily.Web\

dotnet-ef migrations add Volunteers_Init -c PetFamily.Volunteers.Infrastructure.DbContexts.WriteDbContext -p .\src\Volunteers\PetFamily.Volunteers.Infrastructure\ -s .\src\PetFamily.Web\
dotnet-ef migrations add Species_Init -c PetFamily.Species.Infrastructure.DbContexts.WriteDbContext -p .\src\Species\PetFamily.Species.Infrastructure\ -s .\src\PetFamily.Web\
dotnet-ef migrations add Accounts_Init -c PetFamily.Accounts.Infrastructure.DbContexts.AccountsDbContext -p .\src\Accounts\PetFamily.Accounts.Infrastructure\ -s .\src\PetFamily.Web\

dotnet-ef database update -c PetFamily.Volunteers.Infrastructure.DbContexts.WriteDbContext -p .\src\Volunteers\PetFamily.Volunteers.Infrastructure\ -s .\src\PetFamily.Web\
dotnet-ef database update -c PetFamily.Species.Infrastructure.DbContexts.WriteDbContext -p .\src\Species\PetFamily.Species.Infrastructure\ -s .\src\PetFamily.Web\
dotnet-ef database update -c PetFamily.Accounts.Infrastructure.DbContexts.AccountsDbContext -p .\src\Accounts\PetFamily.Accounts.Infrastructure\ -s .\src\PetFamily.Web\

pause