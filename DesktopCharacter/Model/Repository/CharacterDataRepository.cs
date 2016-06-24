using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Database;
using System.Data.Entity.Migrations;
using DesktopCharacter.Model.Database.Domain;

namespace DesktopCharacter.Model.Repository
{
    class CharacterDataRepository
    {
        public void Save(CharacterData settings)
        {
            using (var context = new DatabaseContext())
            {
                context.CharacterData.AddOrUpdate(settings);
                context.SaveChanges();
            }
        }

        public string GetDataName()
        {
            CharacterData data;
            using (var context = new DatabaseContext())
            {
                data = context.CharacterData.FirstOrDefault();
            }
            return data?.Name;
        }
    }
}
