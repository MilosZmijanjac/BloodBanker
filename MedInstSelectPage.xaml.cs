using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BloodBanker.DAO;
using BloodBanker.Model;
using WPFCustomMessageBox;

namespace BloodBanker
{
    /// <summary>
    ///     Interaction logic for MedInstSelectPage.xaml
    /// </summary>
    public partial class MedInstSelectPage : Page
    {
        public MedInstSelectPage()
        {
            InitializeComponent();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            
            if (!IsFilled()) return;
            if (!MedicalInstitutionDAO.IsUsernameAvailable(username.Text))
            {
                CustomMessageBox.ShowOK((string) Application.Current.Resources["UsernameUnavailable"], "", "OK");
                return;
            }

            if (!PhNoCheck(phonNo.Text))
                CustomMessageBox.ShowOK("", (string) Application.Current.Resources["PhoneNumberInvalid"], "OK");
            else
                try
                {
                    var medical = new MedicalInstitution(name.Text, long.Parse(phonNo.Text), address.Text, city.Text
                        , webSite.Text, eMail.Text, username.Text, password.Password
                        , medInstType.SelectedItem.Equals((string) Application.Current.Resources["Hospital"]) ? "72" : "66");

                    MedicalInstitutionDAO.StoreMedInst(medical);

                    medical.MI_ID = MedicalInstitutionDAO.GetLastId();

                    if (medical.TYPE_OF_MI == "66")
                    {
                        NavigationService.Navigate(new StockRegisterPage(medical));
                    }
                    else
                    {
                        MessageBox.Show((string) Application.Current.Resources["UserRegistered"]);
                        NavigationService.Navigate(new LoginPage());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show((string) Application.Current.Resources["RegistrationPageException"] + "\n" +
                                    ex.Message);
                }
        }

        private void Name_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (name.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                name.Foreground = Brushes.Black;
                name.Clear();
            }
        }

        private void Name_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (name.Text.Equals(""))
            {
                name.Text = (string) Application.Current.Resources["Enter"];
                name.Foreground = Brushes.White;
            }
        }

        private void EMail_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (eMail.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                eMail.Foreground = Brushes.Black;
                eMail.Clear();
            }
        }

        private void EMail_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (eMail.Text.Equals(""))
            {
                eMail.Text = (string) Application.Current.Resources["Enter"];
                eMail.Foreground = Brushes.White;
            }
        }

        private void PhonNo_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (phonNo.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                phonNo.Foreground = Brushes.Black;
                phonNo.Clear();
            }
        }

        private void PhonNo_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (phonNo.Text.Equals(""))
            {
                phonNo.Text = (string) Application.Current.Resources["Enter"];
                phonNo.Foreground = Brushes.White;
            }
        }

        private void WebSite_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (webSite.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                webSite.Foreground = Brushes.Black;
                webSite.Clear();
            }
        }

        private void WebSite_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (webSite.Text.Equals(""))
            {
                webSite.Text = (string) Application.Current.Resources["Enter"];
                webSite.Foreground = Brushes.White;
            }
        }

        private void Address_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (address.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                address.Foreground = Brushes.Black;
                address.Clear();
            }
        }

        private void Address_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (address.Text.Equals(""))
            {
                address.Text = (string) Application.Current.Resources["Enter"];
                address.Foreground = Brushes.White;
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

        private void Username_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (username.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                username.Foreground = Brushes.Black;
                username.Clear();
            }
        }

        private void Username_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (username.Text.Equals(""))
            {
                username.Text = (string) Application.Current.Resources["Enter"];
                username.Foreground = Brushes.White;
            }
        }

        private void City_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (city.Text.Equals(""))
            {
                city.Text = (string) Application.Current.Resources["Enter"];
                city.Foreground = Brushes.White;
            }
        }

        private void City_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (city.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                city.Foreground = Brushes.Black;
                city.Clear();
            }
        }

        public bool IsFilled()
        {
            foreach (var i in mainGrid.Children)
                if (i is TextBox && ((TextBox) i).Text.Equals((string) Application.Current.Resources["Enter"]))
                {
                    CustomMessageBox.ShowOK( (string) Application.Current.Resources["IncompleteReg"],"", "OK");
                    return false;
                }

            return true;
        }

        private bool PhNoCheck(string ph)
        {
            var flag = true;
            foreach (var i in ph)
                if (i > '9' || i < '0')
                    flag = false;
            return flag;
        }
    }
}