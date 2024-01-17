using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WizarStore_API.Models;

namespace WizarStore_API.Models;

    public class WizardStoreContext : DbContext
    {
        public WizardStoreContext (DbContextOptions<WizardStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Item { get; set; } = default!;
    }
