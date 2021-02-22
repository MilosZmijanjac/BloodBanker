using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace BloodBanker
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

      /*  public string _UserSettingsPath =
            Assembly.GetEntryAssembly().Location +
            "\\Settings\\UserSettings\\" +
            UserSettingsFilename;*/
        //public MySettings Settings { get; set; }
        public MainWindow()
        {
            
            InitializeComponent();
            ApplyResources(App.settings.Language);
            ApplyResources(App.settings.Theme);
            Main.Content = new LoginPage();
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        public static void ApplyResources(string src)
        {
            var a = Application.Current as App;
            var dict = new ResourceDictionary {Source = new Uri(src, UriKind.Relative)};
            foreach (var mergeDict in dict.MergedDictionaries) a.Resources.MergedDictionaries.Add(mergeDict);
            foreach (var key in dict.Keys) a.Resources[key] = dict[key];
        }
    }
}