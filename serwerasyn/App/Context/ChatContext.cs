using serwer.App.Models;
using System.Data.Entity;

namespace serwer.App.Context
{
    class ChatContext : DbContext
    {
        private const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = Chat;";


        public ChatContext() : base()
        {
            this.Database.Connection.ConnectionString = connectionString;
        }


        public DbSet<Usser> Ussers { get; set; }
    }
}
