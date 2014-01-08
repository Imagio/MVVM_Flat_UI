using System.Data.Entity;
using System.Linq;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;

namespace Ru.Imagio.Model
{
    public class DocsContext : DbContext
    {
        public static string ConnectionString = new MySqlConnectionStringBuilder
        {
            /*
            Server = "192.168.0.48",
            UserID = "doc_client",
            Database = "docs",
            Password = "oxT5BQ7i827K5teg98Zp",
             */
            Server = "localhost",
            UserID = "root",
            Database = "doc_reg",
            Password = "root",
            CharacterSet = "utf8"
        }.ConnectionString;

        private static readonly MySqlConnectionFactory ConnectionFactory  = new MySqlConnectionFactory();

        public DocsContext()
            : base(ConnectionFactory.CreateConnection(ConnectionString), false)
        {
            
        }

        public Account GetAccount(int id)
        {
            return Accounts.FirstOrDefault(account => account.Id == id);
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<SendAccount> SendAccounts { get; set; }

        public DbSet<Document> Documents { get; set; }
    }
}
