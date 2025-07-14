using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using UserService.Data;

public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
{
    public UserDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
        
        // Configuración para tiempo de diseño (migraciones)
        optionsBuilder.UseMySql(
            "Server=localhost;Database=userdb;User=root;Password=;",
            ServerVersion.AutoDetect("Server=localhost;Database=userdb;User=root;Password=;"),
            options => options.MigrationsAssembly(typeof(UserDbContext).Assembly.GetName().Name)
        );
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.EnableDetailedErrors();

        return new UserDbContext(optionsBuilder.Options);
    }
}