using System;
using System.Windows;
using System.Windows.Controls;
using BloodBanker.DAO;
using BloodBanker.Model;

namespace BloodBanker
{
    /// <summary>
    ///     Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private readonly MedicalInstitution medInst;
        private readonly User user;

        public SettingsPage(object ob)
        {
            InitializeComponent();
            if ((user = ob as User) == null)
            {
                medInst = ob as MedicalInstitution;
                InitMedInst();
            }
            else
            {
                InitUser();
            }
        }

        private void InitUser()
        {
            web_bgr.Content = (string) Application.Current.Resources["BloodGroup"];
            name.Text = user.NAME;
            type.Text = user.TYPE_OF_USER.Equals("68")
                ? (string) Application.Current.Resources["Donor"]
                : (string) Application.Current.Resources["Recepient"];
            ph_No.Text = user.PH_NO.ToString();
            website.Text = user.B_GRP;
            email.Text = user.EMAIL;
            address.Text = user.ADDRESS;
            city.Text = user.CITY;
            username.Text = user.USERNAME;
            passwordCurrent.Password = user.PASSWORD;
            web_bgrpButton.Visibility = Visibility.Hidden;
        }

        private void InitMedInst()
        {
            web_bgr.Content = (string) Application.Current.Resources["Website"];
            name.Text = medInst.NAME;
            type.Text = medInst.TYPE_OF_MI.Equals("72")
                ? (string) Application.Current.Resources["Hospital"]
                : (string) Application.Current.Resources["BloodBank"];
            ph_No.Text = medInst.PHONE.ToString();
            website.Text = medInst.WEBSITE;
            email.Text = medInst.EMAIL;
            address.Text = medInst.ADDRESS;
            city.Text = medInst.CITY;
            username.Text = medInst.USERNAME;
            passwordCurrent.Password = medInst.PASSWORD;
        }

        private void Ph_NoConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (!PhNoCheck(ph_No.Text))
            {
                MessageBox.Show((string) Application.Current.Resources["PhoneNumberInvalid"]);
                return;
            }

            try
            {
                if (user != null && user.PH_NO != decimal.Parse(ph_No.Text))
                    UserDAO.UpdateUser(user, "PH_NO", ph_No.Text);
                else if (medInst.PHONE != decimal.Parse(ph_No.Text))
                    MedicalInstitutionDAO.UpdateMedInst(medInst, "PHONE", ph_No.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["UpdatePhoneException"] + " " + ex.Message);
            }

            MessageBox.Show((string) Application.Current.Resources["UpdatePhoneSuccesful"]);
        }

        private void Web_bgrpConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!website.Text.Equals(medInst.WEBSITE))
                    MedicalInstitutionDAO.UpdateMedInst(medInst, "WEBSITE", website.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["UpdateWebsiteException"] + " " + ex.Message);
            }

            MessageBox.Show((string) Application.Current.Resources["UpdateWebsiteSuccesful"]);
        }

        private void EmailConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (user != null && !email.Text.Equals(user.EMAIL))
                    UserDAO.UpdateUser(user, "EMAIL", email.Text);
                else if (!email.Text.Equals(medInst.EMAIL))
                    MedicalInstitutionDAO.UpdateMedInst(medInst, "EMAIL", email.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["UpdateEmailException"] + "\n" + ex.Message);
            }

            MessageBox.Show((string) Application.Current.Resources["UpdateEmailSuccesful"]);
        }

        private void AddressConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (user != null && !address.Text.Equals(user.ADDRESS))
                    UserDAO.UpdateUser(user, "ADDRESS", address.Text);
                else if (!address.Text.Equals(medInst.ADDRESS))
                    MedicalInstitutionDAO.UpdateMedInst(medInst, "ADDRESS", address.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["UpdateAddressException"] + "\n" + ex.Message);
            }

            MessageBox.Show((string) Application.Current.Resources["UpdateAddressSuccessful"]);
        }

        private void CityConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (user != null && !city.Text.Equals(user.CITY))
                    UserDAO.UpdateUser(user, "CITY", city.Text);
                else if (!city.Text.Equals(medInst.CITY))
                    MedicalInstitutionDAO.UpdateMedInst(medInst, "CITY", city.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["UpdateCityException"] + "\n" + ex.Message);
            }

            MessageBox.Show((string) Application.Current.Resources["UpdateCitySuccesful"]);
        }

        private void UsernameConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (user != null && !username.Text.Equals(user.USERNAME))
                {
                    if (UserDAO.IsUsernameAvailable(username.Text) &&
                        MedicalInstitutionDAO.IsUsernameAvailable(username.Text))
                    {
                        UserDAO.UpdateUser(user, "USERNAME", username.Text);
                    }
                    else
                    {
                        MessageBox.Show((string) Application.Current.Resources["UsernameUnavailable"]);
                        return;
                    }
                }
                else if (!username.Text.Equals(medInst.USERNAME))
                {
                    if (UserDAO.IsUsernameAvailable(username.Text) &&
                        MedicalInstitutionDAO.IsUsernameAvailable(username.Text))
                    {
                        MedicalInstitutionDAO.UpdateMedInst(medInst, "USERNAME", username.Text);
                    }
                    else
                    {
                        MessageBox.Show((string) Application.Current.Resources["UsernameUnavailable"]);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["UpdateUsernameException"] + "\n" + ex.Message);
                return;
            }
            MessageBox.Show((string) Application.Current.Resources["UpdateUsernameSuccessful"]);
        }

        private void Ph_NoButton_Click(object sender, RoutedEventArgs e)
        {
            ph_No.IsEnabled = !ph_No.IsEnabled;
            if (ph_No.IsEnabled)
            {
                ph_NoConfirmButton.Visibility = Visibility.Visible;
                ph_No.SelectAll();
                ph_No.Focus();
            }
            else
            {
                ph_NoConfirmButton.Visibility = Visibility.Hidden;
            }
        }

        private void Web_bgrpButton_Click(object sender, RoutedEventArgs e)
        {
            website.IsEnabled = !website.IsEnabled;
            if (website.IsEnabled)
            {
                web_bgrpConfirmButton.Visibility = Visibility.Visible;
                website.SelectAll();
                website.Focus();
            }
            else
            {
                web_bgrpConfirmButton.Visibility = Visibility.Hidden;
            }
        }

        private void EmailButton_Click(object sender, RoutedEventArgs e)
        {
            email.IsEnabled = !email.IsEnabled;
            if (email.IsEnabled)
            {
                emailConfirmButton.Visibility = Visibility.Visible;
                email.SelectAll();
                email.Focus();
            }
            else
            {
                emailConfirmButton.Visibility = Visibility.Hidden;
            }
        }

        private void AddressButton_Click(object sender, RoutedEventArgs e)
        {
            address.IsEnabled = !address.IsEnabled;
            if (address.IsEnabled)
            {
                addressConfirmButton.Visibility = Visibility.Visible;
                address.SelectAll();
                address.Focus();
            }
            else
            {
                addressConfirmButton.Visibility = Visibility.Hidden;
            }
        }

        private void CityButton_Click(object sender, RoutedEventArgs e)
        {
            city.IsEnabled = !city.IsEnabled;
            if (city.IsEnabled)
            {
                cityConfirmButton.Visibility = Visibility.Visible;
                city.SelectAll();
                city.Focus();
            }
            else
            {
                cityConfirmButton.Visibility = Visibility.Hidden;
            }
        }

        private void UsernameButton_Click(object sender, RoutedEventArgs e)
        {
            username.IsEnabled = !username.IsEnabled;
            if (username.IsEnabled)
            {
                usernameConfirmButton.Visibility = Visibility.Visible;
                username.SelectAll();
                username.Focus();
            }
            else
            {
                usernameConfirmButton.Visibility = Visibility.Hidden;
            }
        }

        private void ChangePassButton_Click(object sender, RoutedEventArgs e)
        {
            passwordCurrent.IsEnabled = !passwordCurrent.IsEnabled;
            if (passwordCurrent.IsEnabled)
            {
                passwordCurrent.Password = "";
                passwordNew.Visibility = Visibility.Visible;
                passwordConfirm.Visibility = Visibility.Visible;
                newPassLabel.Visibility = Visibility.Visible;
                confirmPassLabel.Visibility = Visibility.Visible;
                confirmChangePassButton.Visibility = Visibility.Visible;
            }
            else
            {
                passwordCurrent.Password = "......";
                passwordNew.Visibility = Visibility.Hidden;
                passwordConfirm.Visibility = Visibility.Hidden;
                newPassLabel.Visibility = Visibility.Hidden;
                confirmPassLabel.Visibility = Visibility.Hidden;
                confirmChangePassButton.Visibility = Visibility.Hidden;
            }
        }

        private void ConfirmChangePassButton_Click(object sender, RoutedEventArgs e)
        {
            if (user != null)
            {
                if (passwordCurrent.Password.Equals(user.PASSWORD))
                {
                    if (passwordNew.Password.Equals(passwordConfirm.Password))
                    {
                        try
                        {
                            UserDAO.UpdateUser(user, "PASSWORD", passwordNew.Password);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show((string) Application.Current.Resources["UpdatePasswordException"] + "\n" +
                                            ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show((string) Application.Current.Resources["UpdatePasswordMissmatch"]);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show((string) Application.Current.Resources["UpdatePasswordCurrentMissmatch"]);
                    return;
                }
            }
            else
            {
                if (passwordCurrent.Password.Equals(medInst.PASSWORD))
                {
                    if (passwordNew.Password.Equals(passwordConfirm.Password))
                    {
                        try
                        {
                            MedicalInstitutionDAO.UpdateMedInst(medInst, "PASSWORD", passwordNew.Password);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show((string) Application.Current.Resources["UpdatePasswordException"] + "\n" +
                                            ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show((string) Application.Current.Resources["UpdatePasswordMissmatch"]);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show((string) Application.Current.Resources["UpdatePasswordCurrentMissmatch"]);
                    return;
                }
            }

            MessageBox.Show((string) Application.Current.Resources["UpdatePasswordSuccessful"]);
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