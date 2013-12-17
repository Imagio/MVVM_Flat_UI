using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;

namespace Ru.Imagio.Model
{
    public class DocsContext : DbContext
    {
        public static string ConnectionString = new MySqlConnectionStringBuilder
        {
            Server = "192.168.0.48",
            UserID = "doc_client",
            Database = "docs",
            Password = "oxT5BQ7i827K5teg98Zp",
            CharacterSet = "utf8"
        }.ConnectionString;

        private static readonly MySqlConnectionFactory ConnectionFactory  = new MySqlConnectionFactory();

        public DocsContext()
            : base(ConnectionFactory.CreateConnection(ConnectionString), false)
        {
            
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
