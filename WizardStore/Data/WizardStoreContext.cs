using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WizardStoreAPI.Models;

namespace WizardStoreAPI.Data;

    public class WizardStoreContext : DbContext
    {
        public WizardStoreContext (DbContextOptions<WizardStoreContext> options)
            : base(options)
        {
        }

        public DbSet<MagicItem> MagicItems { get; set; } = default!;

        public DbSet<User> Users { get; set;} = default!;
        
}
