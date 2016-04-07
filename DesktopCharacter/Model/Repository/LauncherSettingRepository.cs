using DesktopCharacter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.Repository
{
    public class LauncherSettingRepository
    {
        public void save(LauncherSetting settings)
        {
            //TODO データをjson形式で書き出し、すでに存在すれば上書きする
        }

        public LauncherSetting load()
        {
            //TODO ローカルのデータを読み込み、存在しなければnew LauncherSettings()を返す
            return null;
        }
    }
}
