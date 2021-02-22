using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows;

namespace BloodBanker
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string _DefaultSettingspath =
         Assembly.GetEntryAssembly().Location +
         "\\..\\Settings\\" + "default.xml";

        public static string _DefaultDirectoryPath =
      Assembly.GetEntryAssembly().Location +
      "\\..\\Settings\\";

        public static MySettings settings { get; set; }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            settings = new MySettings();
            if (!Directory.Exists(_DefaultDirectoryPath) || !File.Exists(_DefaultSettingspath))
            {
                Directory.CreateDirectory(_DefaultDirectoryPath);
                settings.Save(_DefaultSettingspath);
            }
            settings = MySettings.Read(_DefaultSettingspath);
            (new MainWindow()).Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            settings.Save(_DefaultSettingspath);
        }


    }
}