using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Domain;
using DesktopCharacter.Model.Database.Domain;
using DesktopCharacter.Model.Database;
using System.Data.Entity.Migrations;

namespace DesktopCharacter.Model.Repository
{
    class CodicRepository
    {
        public void Save(CodicUser settings)
        {
            using (var context = new DatabaseContext())
            {
                context.CodicUser.AddOrUpdate(settings);
                context.SaveChanges();
            }
        }

        public CodicUser Load()
        {
            CodicUser codicUser;
            using( var context = new DatabaseContext())
            {
                codicUser = context.CodicUser.FirstOrDefault();
            }
            return codicUser;
        }
    }
}
