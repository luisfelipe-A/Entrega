using Microsoft.EntityFrameworkCore;
using WizardStoreAPI.Data;

namespace WizardStoreAPI.Services;

public static class DatabaseManagementService
{
    public static void MigrationInitialization(this IApplicationBuilder app)
    {
        using(var serviceScope = app.ApplicationServices.CreateScope())
        {
            var serviceDB = serviceScope.ServiceProvider.GetService<WizardStoreContext>();
            
            if(serviceDB is not null) serviceDB.Database.Migrate();
        }
    }
}