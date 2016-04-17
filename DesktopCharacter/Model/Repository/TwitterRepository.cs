using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DesktopCharacter.Model.Database;
using DesktopCharacter.Model.Database.Domain;
using DesktopCharacter.Model.Service.Twitter;

namespace DesktopCharacter.Model.Repository
{
    class TwitterRepository
    {
        /// <summary>
        /// 保存されているアクセストークンからTwitterUserを作成
        /// </summary>
        /// <returns>保存されているユーザー全てのTwitter</returns>
        public List<TwitterUser> FindAll()
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
    }
}
