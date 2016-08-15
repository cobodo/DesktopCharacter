using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.Database.Domain
{
    /// <summary>
    /// ランチャーのデータセット
    /// </summary>
    public class LauncherSettingsDataSet : IEquatable<LauncherSettingsDataSet>
    {
        /// <summary>
        /// 表示名
        /// </summary>
        public string DisplayName { get; set; } = "";
        /// <summary>
        /// 起動するパス
        /// </summary>
        public string Path { get; set; } = "";

        /// <summary>
        /// 自作クラスのハッシュコードを返す
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.DisplayName.GetHashCode();
        }

        /// <summary>
        /// LinqのDistinctを動作させるためにインターフェイスを実装
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(LauncherSettingsDataSet other)
        {
            if(other == null)
            {
                return false;
            }
            return (this.DisplayName == other.DisplayName);
        }
    }
}
