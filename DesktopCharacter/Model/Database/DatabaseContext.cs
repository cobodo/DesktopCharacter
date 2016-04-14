using System.Data;
using SQLite.CodeFirst;
using System.Data.Entity;
using DesktopCharacter.Model.Database.Domain;


namespace DesktopCharacter.Model.Database
{
    class DatabaseContext: DbContext
    {
        public DatabaseContext(): base("name=mydatabase")
        {

        }

        public DbSet<TwitterUser> TwitterUser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<DatabaseContext>(modelBuilder);
            System.Data.Entity.Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
