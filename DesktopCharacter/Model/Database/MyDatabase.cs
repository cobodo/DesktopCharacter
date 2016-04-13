using DesktopCharacter.Model.Domain;
using SQLite.CodeFirst;
using System.Data.Entity;


namespace DesktopCharacter.Model.Database
{
    class MyDatabase: DbContext
    {
        public MyDatabase(): base("name=mydatabase")
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<MyDatabase>(modelBuilder);
            System.Data.Entity.Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
