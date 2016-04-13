using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Domain;
using System.IO;
using System.Data;


namespace DesktopCharacter.Model
{
    class DatabaseModel
    {
        public static DatabaseModel Instance { get; } = new DatabaseModel();

        private DatabaseModel()
        {
        }

        public void Innitialize()
        {
            var conf = new SQLiteConnectionStringBuilder()
            {
                DataSource = "test.db"
            };
            var connection = new SQLiteConnection(conf.ToString());
            var context = new DataContext(connection);
            context.Database.CreateIfNotExists();
        }
    }

    public class DataContext: DbContext
    {
        public DbSet<TwitterAccount> TwitterAccounts { get; set; }

        public DataContext(SQLiteConnection connection): base()
        {
        }
    }
}
