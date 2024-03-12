using ManageFinances.Migrations;

namespace ManageFinances.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
                try
                {
                }
                catch
                {
                    throw;
                }
            }
            return host;
        }
    }
}
