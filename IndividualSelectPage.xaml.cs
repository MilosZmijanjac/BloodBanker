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
    ///     Interaction logic for IndividualSelectPage.xaml
    /// </summary>
    public partial class IndividualSelectPage : Page
    {
        public IndividualSelectPage()
        {
            InitializeComponent();
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

        private void PhoneNumber_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (phoneNumber.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                phoneNumber.Foreground = Brushes.Black;
                phoneNumber.Clear();
            }
        }

        private void PhoneNumber_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (phoneNumber.Text.Equals(""))
            {
                phoneNumber.Text = (string) Application.Current.Resources["Enter"];
                phoneNumber.Foreground = Brushes.White;
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

        private void Ssn_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (ssn.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                ssn.Foreground = Brushes.Black;
                ssn.Clear();
            }
        }

        private void Ssn_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (ssn.Text.Equals(""))
            {
                ssn.Text = (string) Application.Current.Resources["Enter"];
                ssn.Foreground = Brushes.White;
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

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var i in mainGrid.Children)
                    if (i is TextBox && ((TextBox) i).Text.Equals((string) Application.Current.Resources["Enter"]))
                    {
                        CustomMessageBox.ShowOK((string) Application.Current.Resources["IncompleteReg"], "", "OK");
                        return;
                    }


                if (!UserDAO.IsUsernameAvailable(username.Text))
                {
                    CustomMessageBox.ShowOK((string) Application.Current.Resources["UsernameUnavailable"], "", "OK");
                }

                else
                {
                    Console.WriteLine("--------usao-------");
                    if (!PhNoCheck(phoneNumber.Text))
                    {
                        CustomMessageBox.ShowOK((string) Application.Current.Resources["PhoneNumberInvalid"], "", "OK");
                    }
                    else
                    {
                        var user = new User(decimal.Parse(ssn.Text), decimal.Parse(phoneNumber.Text), name.Text,
                            bloodGroup.Text, eMail.Text, address.Text, city.Text,
                            username.Text, password.Password,
                            userType.Text.Equals((string) Application.Current.Resources["Donor"]) ? "68" : "82");
                        if (user.TYPE_OF_USER == "68")
                            NavigationService.Navigate(new DRegistrationPage(user));
                        else if (user.TYPE_OF_USER == "82")
                            NavigationService.Navigate(new RRegistrationPage(user));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["RegistrationPageException"] + "\n" +
                                ex.Message);
            }
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