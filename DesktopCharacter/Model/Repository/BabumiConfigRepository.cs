using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Database;
using System.Data.Entity.Migrations;
using DesktopCharacter.Model.Database.Domain;
using System.Xml.Serialization;
using System.IO;
using System.Windows;

namespace DesktopCharacter.Model.Repository
{
    class BabumiConfigRepository
    {
        /// <summary>
        /// オブジェクトファイルをセーブ
        /// </summary>
        /// <param name="settings"></param>
        public void Save(BabumiConfig settings)
        {
            using (var context = new DatabaseContext())
            {
                //!< Configデータ一個しか保存しないので削除してから入れる
                foreach ( var data in context.BabumiConfig.ToList() )
                {
                    context.BabumiConfig.Remove(data);
                }
                context.SaveChanges();
                context.BabumiConfig.AddOrUpdate(settings);
                context.SaveChanges();
             }
        }

        /// <summary>
        /// XMLの情報をセーブ
        /// </summary>
        /// <param name="xml"></param>
        public void Save( string xml )
        {
            BabumiConfig config = new BabumiConfig();
            try
            {
                // XmlSerializerを使ってファイルに保存
                XmlSerializer serializer = new XmlSerializer(typeof(BabumiConfig));
                var fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + xml, FileMode.Open);
                config = (BabumiConfig)serializer.Deserialize(fs);
                fs.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            Save(config);
        }

        /// <summary>
        /// コンフィグ取得
        /// </summary>
        /// <returns></returns>
        public BabumiConfig GetConfig()
        {
            List<BabumiConfig> list;
            using (var context = new DatabaseContext())
            {
                list = context.BabumiConfig.ToList();
            }
            return list.FirstOrDefault();
        }

        /// <summary>
        /// XMLに書き出し
        /// </summary>
        public void ExportXML(string xml)
        {
            // XmlSerializerを使ってファイルに保存（TwitSettingオブジェクトの内容を書き込む）
            XmlSerializer serializer = new XmlSerializer(typeof(BabumiConfig));

            // カレントディレクトリに"settings.xml"というファイルで書き出す
            FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + xml, FileMode.Create);

            // オブジェクトをシリアル化してXMLファイルに書き込む
            serializer.Serialize(fs, GetConfig());
            fs.Close();
        }
    }
}
