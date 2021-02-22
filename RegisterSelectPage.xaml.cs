using System.Windows;
using System.Windows.Controls;
using WPFCustomMessageBox;

namespace BloodBanker
{
    /// <summary>
    ///     Interaction logic for RegisterSelectPage.xaml
    /// </summary>
    public partial class RegisterSelectPage : Page
    {
        public RegisterSelectPage()
        {
            InitializeComponent();
        }

        private void MedicalInstitutionClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MedInstSelectPage());
        }

        private void IndividualClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new IndividualSelectPage());
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            if (CustomMessageBox.ShowYesNo((string) Application.Current.Resources["ExitQuestion"],
                    (string) Application.Current.Resources["Confirm"],
                    (string) Application.Current.Resources["Yes"], (string) Application.Current.Resources["No"]) ==
                MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }
        private void Theme1ButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow.ApplyResources("Theme1.xaml");
            App.settings.Theme = "Theme1.xaml";
        }

        private void Theme2ButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow.ApplyResources("Theme2.xaml");
            App.settings.Theme = "Theme2.xaml";
        }
        private void Theme3ButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow.ApplyResources("Theme3.xaml");
            App.settings.Theme = "Theme3.xaml";
        }
        private void EnglishButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow.ApplyResources("en-US.xaml");
            App.settings.Language = "en-US.xaml";
        }
        private void SerbianButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow.ApplyResources("sr-Latn-RS.xaml");
            App.settings.Language = "sr-Latn-RS.xaml";
        }
    }
}