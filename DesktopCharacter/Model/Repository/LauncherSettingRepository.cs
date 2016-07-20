﻿using Newtonsoft.Json;
using System.IO;
using DesktopCharacter.Model.Database.Domain;

namespace DesktopCharacter.Model.Repository
{
    public class LauncherSettingRepository
    {
        private const string SETTINGS_FILE = "Launchersetting.cfg";
        public void Save(LauncherSetting settings)
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            using (StreamWriter writeToFile = new StreamWriter(SETTINGS_FILE, false))
            {
                writeToFile.Write(json);
            }
        }

        public LauncherSetting Load()
        {
            if (!File.Exists(SETTINGS_FILE))
            {
                return new LauncherSetting();
            }

            using (StreamReader sr = new StreamReader(SETTINGS_FILE))
            {
                string fileToJson = sr.ReadToEnd();
                LauncherSetting ls = JsonConvert.DeserializeObject<LauncherSetting>(fileToJson);
                return ls;
            }
        }
    }
}
