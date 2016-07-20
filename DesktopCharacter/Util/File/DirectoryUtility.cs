using System;
using System.Collections.Generic;
using System.IO;

namespace DesktopCharacter.Util.File
{
    public static class DirectoryUtility
    {
        public static List<string> GetFileList( string directory, string name )
        {
            try
            {
                List<string> result = new List<string>();
                string[] dirs = Directory.GetDirectories(directory);
                if (dirs.Length == 0)
                {
                    return result;
                }
                foreach (var dir in dirs)
                {
                    //!< 下の階層ファイルを検索して*.model.jsonのパスを探す
                    string[] files = System.IO.Directory.GetFiles(dir, "*", System.IO.SearchOption.AllDirectories);
                    foreach (var file in files)
                    {
                        if (file.Contains(name))
                        {
                            result.Add(file);
                        }
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
