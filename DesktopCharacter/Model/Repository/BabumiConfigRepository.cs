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
using System.Runtime.Serialization.Formatters.Binary;

namespace DesktopCharacter.Model.Repository
{
    class BabumiConfigRepository
    {
        /// <summary>
        /// 保存先
        /// </summary>
        private BabumiConfig _babumiConfig = null;

        /// <summary>
        /// オブジェクトファイルをセーブ
        /// </summary>
        /// <param name="settings"></param>
        public void Save(BabumiConfig settings)
        {
            _babumiConfig = settings;
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
                using (var fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + xml, FileMode.Open))
                {
                    // XmlSerializerを使ってファイルに保存
                    XmlSerializer serializer = new XmlSerializer(typeof(BabumiConfig));
                    config = (BabumiConfig)serializer.Deserialize(fs);
                }
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
            return _babumiConfig;
        }

        /// <summary>
        /// XMLに書き出し
        /// </summary>
        public void ExportXML(string xml)
        {
            using (FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + xml, FileMode.Create))
            {
                // XmlSerializerを使ってファイルに保存（TwitSettingオブジェクトの内容を書き込む）
                XmlSerializer serializer = new XmlSerializer(typeof(BabumiConfig));
                // オブジェクトをシリアル化してXMLファイルに書き込む
                serializer.Serialize(fs, GetConfig());
            }
        }
    }
}
