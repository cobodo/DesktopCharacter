using SQLite.CodeFirst;
using System.Data.Entity;
using DesktopCharacter.Model.Domain;
using System;

namespace DesktopCharacter.Model.Database
{
    class DatabaseContext: DbContext
    {
        public DatabaseContext(): base("name=mydatabase")
        {

        }

        public DbSet<CodicUser> CodicUser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<DatabaseContext>(modelBuilder);
            System.Data.Entity.Database.SetInitializer(sqliteConnectionInitializer);
            Database.Log = s => Console.WriteLine(@"[Database] " + s);
        }
    }
}
