using SQLite.CodeFirst;
using System.Data.Entity;


namespace DesktopCharacter.Model.Database
{
    class DatabaseContext: DbContext
    {
        public DatabaseContext(): base("name=mydatabase")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<DatabaseContext>(modelBuilder);
            System.Data.Entity.Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
