using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Database;
using DesktopCharacter.Model.Database.Domain;

namespace DesktopCharacter.Model.Repository
{
    public class SlackUserRepository
    {
        public List<SlackUser> FindAll()
        {
            using (var context = new DatabaseContext())
            {
                return context.SlackUser.Include("Filter").ToList();
            }
        }

        public void Save(SlackUser user)
        {
            using (var context = new DatabaseContext())
            {
                context.SlackUser.AddOrUpdate(user);
                context.SlackNotificationFilter.AddOrUpdate(user.Filter);
                context.SaveChanges();
            }
        }

        public void Save(List<SlackUser> users)
        {
            using (var context = new DatabaseContext())
            {
                context.SlackUser.AddOrUpdate(users.ToArray());
                context.SlackNotificationFilter.AddOrUpdate(users.Select(u => u.Filter).ToArray());
                context.SaveChanges();
            }
        }

        /// <summary>
        /// ユーザーを削除する
        /// </summary>
        /// <param name="users">削除するユーザー</param>
        public void Delete(List<SlackUser> users)
        {
            using (var context = new DatabaseContext())
            {
                foreach (var slackUser in users)
                {
                    context.SlackUser.Attach(slackUser);
                }
                context.SlackUser.RemoveRange(users);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// ユーザを削除する
        /// </summary>
        /// <param name="user">削除するユーザー</param>
        public void Delete(SlackUser user)
        {
            using (var context = new DatabaseContext())
            {
                context.SlackUser.Attach(user);
                context.SlackUser.Remove(user);
                context.SaveChanges();
            }
        }
    }
}
