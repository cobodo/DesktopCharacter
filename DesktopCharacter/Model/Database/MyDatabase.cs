using DesktopCharacter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.CodeFirst;

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
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
