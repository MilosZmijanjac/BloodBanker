using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BloodBanker.DAO;
using WPFCustomMessageBox;

namespace BloodBanker
{
    /// <summary>
    ///     Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            if (username.Text.Equals("Username") || password.Password.Equals("........"))
            {
                invalidLogin.Visibility = Visibility.Collapsed;
                notFoundLogin.Visibility = Visibility.Collapsed;
                emptyLogin.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    if (!MedicalInstitutionDAO.IsUsernameAvailable(username.Text) &&
                        !UserDAO.IsUsernameAvailable(username.Text))
                    {
                        invalidLogin.Visibility = Visibility.Visible;
                        notFoundLogin.Visibility = Visibility.Collapsed;
                        emptyLogin.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        var user = UserDAO.LoginUser(username.Text, password.Password);
                        if (user != null)
                        {
                            new DashboardWindow(user).Show();
                            Window.GetWindow(this).Close();
                        }
                        else
                        {
                            var med_inst = MedicalInstitutionDAO.LoginMedInst(username.Text, password.Password);
                            if (med_inst != null)
                            {
                                new DashboardWindow(med_inst).Show();
                                Window.GetWindow(this).Close();
                            }
                            else
                            {
                                invalidLogin.Visibility = Visibility.Visible;
                                notFoundLogin.Visibility = Visibility.Collapsed;
                                emptyLogin.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                }
                catch (Exception excp)
                {
                    MessageBox.Show((string) Application.Current.Resources["LoginPageException"] + "\n" + excp.Message);
                }
            }
        }
   
        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            if (CustomMessageBox.ShowYesNo((string)Application.Current.Resources["ExitQuestion"],
                    (string)Application.Current.Resources["Confirm"],
                    (string)Application.Current.Resources["Yes"], (string)Application.Current.Resources["No"]) ==
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
            username.Text = App.Current.Resources["UsernameLabel"].ToString();
        }
        private void SerbianButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow.ApplyResources("sr-Latn-RS.xaml");
            App.settings.Language = "sr-Latn-RS.xaml";
            username.Text = App.Current.Resources["UsernameLabel"].ToString();
        }
        private void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterSelectPage());
        }

        private void Username_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (username.Text.Equals((string) Application.Current.Resources["UsernameLabel"]))
            {
                username.Foreground = Brushes.Black;
                username.Clear();
            }
        }

        private void Username_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (username.Text.Equals(""))
            {
                username.Text = (string) Application.Current.Resources["UsernameLabel"];
                username.Foreground = Brushes.White;
            }
        }

        private void Password_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (password.Password.Equals("........")) password.Clear();
        }

        private void Password_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (password.Password.Equals("")) password.Password = "........";
        }
    }
}