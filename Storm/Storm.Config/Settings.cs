using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Config
{
    public class Settings
    {
        private const string SectionName = "appSettings";

        private const string ServerAdressKey = "serverAddress";
        private static System.Configuration.Configuration Config;
        
        public static void CreateAppSettings()
        {
            // Get the application configuration file.
            Config =
              ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None);

           // string sectionName = "appSettings";

            // Add an entry to appSettings.
            int appStgCnt =
                ConfigurationManager.AppSettings.Count;
            string newKey = "NewKey" + appStgCnt.ToString();

            string newValue = DateTime.Now.ToLongDateString() +
              " " + DateTime.Now.ToLongTimeString();

            Config.AppSettings.Settings.Add(newKey, newValue);

            Config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection(SectionName);

            // Get the AppSettings section.
            AppSettingsSection appSettingSection =
              (AppSettingsSection)Config.GetSection(SectionName);
            Console.WriteLine(
              appSettingSection.Settings["NewKey0"].Value);
        
            Console.WriteLine();
            Console.WriteLine("Using GetSection(string).");
            Console.WriteLine("AppSettings section:");
            Console.WriteLine(
              appSettingSection.SectionInformation.GetRawXml());
        }

        public static string GetServerAddress()
        {
            return "net.pipe://localhost/Server";
            AppSettingsSection appSettingSection =
           (AppSettingsSection)Config.GetSection(SectionName);
            Console.WriteLine(
              appSettingSection.Settings["NewKey0"].Value);
        
        }


    }
}
