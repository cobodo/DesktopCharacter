using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.Database.Domain
{
    /// <summary>
    /// ランチャーの設定
    /// </summary>
    public class LauncherSetting
    {
        /// <summary>
        /// ランチャーのデータ
        /// </summary>
        public List<LauncherSettingsDataSet> Dataset { get; set; } = new List<LauncherSettingsDataSet>();
    }

    /// <summary>
    /// ランチャーのデータセット
    /// </summary>
    public class LauncherSettingsDataSet
    {
        /// <summary>
        /// 表示名
        /// </summary>
        public string DisplayName { get; set; } = "";
        /// <summary>
        /// 起動するパス
        /// </summary>
        public string Path { get; set; } = "";
    }
}
