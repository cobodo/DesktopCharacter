using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Database;
using DesktopCharacter.Model.Database.Domain;
using NLog;

namespace DesktopCharacter.Model.Repository
{
    class WindowPositionRepository
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 保存されているウィンドウの位置を取得
        /// </summary>
        /// <returns>保存されているウィンドウの位置</returns>
        public WindowPosition FetchPosition()
        {
            /*
            using (var context = new DatabaseContext())
            {
                var position = context.WindowPosition.FirstOrDefault(p => p.Id == 1) ??
                                     new WindowPosition(0, 0);
                logger.Info("[FetchPosition] id={0}, x={1}, y={2}", position.Id, position.PosX, position.PosY);
                return position;
            }*/
            return null;
        }

        /// <summary>
        /// ウィンドウの位置を保存する
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        public void Save(int x, int y)
        {
            using (var context = new DatabaseContext())
            {
                var pos = new WindowPosition(x, y);
                logger.Info("[Save] id={0}, x={1}, y={2}", pos.Id, pos.PosX, pos.PosY);
                //context.WindowPosition.AddOrUpdate(pos);
                context.SaveChanges();
            }
        }
    }
}
