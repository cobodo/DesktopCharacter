using System;
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

        public DbSet<CodicUser> CodicUser { get; set; }
        public DbSet<TwitterUser> TwitterUser { get; set; }
        public DbSet<TwitterNotificationFilter> TwitterNotificationFilter { get; set; }
        public DbSet<WindowPosition> WindowPosition { get; set; }
        public DbSet<CharacterData> CharacterData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<DatabaseContext>(modelBuilder);
            System.Data.Entity.Database.SetInitializer(sqliteConnectionInitializer);
            Database.Log = s => Console.WriteLine(@"[Database] " + s);
        }
    }
}
