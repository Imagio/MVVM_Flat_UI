using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Ru.Imagio.Model
{
    public class DocsContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
    }
}
