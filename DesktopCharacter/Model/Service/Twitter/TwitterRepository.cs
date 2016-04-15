using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Database;
using DesktopCharacter.Model.Database.Domain;

namespace DesktopCharacter.Model.Service.Twitter
{
    class TwitterRepository
    {
        /// <summary>
        /// 保存されているアクセストークンからTwitterを作成
        /// </summary>
        /// <returns>保存されているユーザー全てのTwitter</returns>
        public List<TwitterUser> Load()
        {
            List<TwitterUser> twitterUsers;
            using (var context = new DatabaseContext())
            {
                twitterUsers = context.TwitterUser.Include("Filter").ToList();
            }
            return twitterUsers.ToList();
        }

        /// <summary>
        /// ユーザーを保存する
        /// </summary>
        /// <param name="user">保存するユーザー</param>
        public void Save(TwitterUser user)
        {
            using (var context = new DatabaseContext())
            {
                context.TwitterUser.AddOrUpdate(user);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// ユーザーを保存する
        /// </summary>
        /// <param name="users">保存するユーザー</param>
        public void Save(List<TwitterUser> users)
        {
            using (var context = new DatabaseContext())
            {
                context.TwitterUser.AddOrUpdate(users.ToArray());
                context.TwitterNotificationFilter.AddOrUpdate(users.Select(u => u.Filter).ToArray());
                context.SaveChanges();
            }
        }

        public Twitter CreateTwitter(TwitterUser user)
        {
            return new Twitter(user);
        }
    }
}
