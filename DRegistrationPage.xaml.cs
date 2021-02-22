using System;
using System.Globalization;
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
    ///     Interaction logic for DRegistrationPage.xaml
    /// </summary>
    public partial class DRegistrationPage : Page
    {
        private readonly User user;

        public DRegistrationPage(User user)
        {
            this.user = user;
            InitializeComponent();
            InitializeBloodBanksCB();
        }

        private void InitializeBloodBanksCB()
        {
            var bloodBanks = MedicalInstitutionDAO.LoadMedInst("66");
            if (bloodBanks != null && bloodBanks.Count > 0)
            {
                bloodBank.DisplayMemberPath = "NAME";
                bloodBank.SelectedValuePath = "MI_ID";
                bloodBank.ItemsSource = bloodBanks;
            }
            else
            {
                bloodBank.Items.Add((string) Application.Current.Resources["Empty"]);
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
                DateTime dt1, dt2;
                if (!DateTime.TryParse(dobText.Text, new CultureInfo("fr-FR"), DateTimeStyles.AdjustToUniversal,
                    out dt1))
                {
                    MessageBox.Show((string) Application.Current.Resources["DatePickerError"] + " " +
                                    (string) Application.Current.Resources["DateOfBirth"]);
                    return;
                }

                if (!DateTime.TryParse(lastDonationDate.Text, new CultureInfo("fr-FR"),
                    DateTimeStyles.AdjustToUniversal, out dt2))
                {
                    MessageBox.Show((string) Application.Current.Resources["LastDonationDate"] + " " +
                                    (string) Application.Current.Resources["DateOfBirth"]);
                    return;
                }

                var donor = new Donor(user, dt1, int.Parse(weight.Text), dt2);
                donor.DISEASE = disease.Text;
                donor.MI_ID = long.Parse(bloodBank.SelectedValue.ToString());
                DonorDAO.StoreDonor(donor);
                CustomMessageBox.ShowOK((string) Application.Current.Resources["UserRegistered"],
                    (string) Application.Current.Resources["Registration"], "OK");
                NavigationService.Navigate(new LoginPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["RecipientRegistrationException"] + "\n" +
                                ex.Message);
            }
        }


        private void Weight_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (weight.Text.Equals((string) Application.Current.Resources["Weight"]))
            {
                weight.Foreground = Brushes.Black;
                weight.Clear();
            }
        }

        private void Weight_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (weight.Text.Equals(""))
            {
                weight.Text = (string) Application.Current.Resources["Weght"];
                weight.Foreground = Brushes.White;
            }
        }

        private void Disease_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (disease.Text.Equals((string) Application.Current.Resources["Disease"]))
            {
                disease.Foreground = Brushes.Black;
                disease.Clear();
            }
        }

        private void Disease_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (disease.Text.Equals(""))
            {
                disease.Text = (string) Application.Current.Resources["Disease"];
                disease.Foreground = Brushes.White;
            }
        }


        private void DobText_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (dobText.Text.Equals((string) Application.Current.Resources["DatePicker"]))
            {
                dobText.Foreground = Brushes.Black;
                dobText.Clear();
            }
        }

        private void DobText_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (dobText.Text.Equals(""))
            {
                dobText.Text = (string) Application.Current.Resources["DatePicker"];
                dobText.Foreground = Brushes.White;
            }
        }

        private void LastDonationDate_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (lastDonationDate.Text.Equals((string) Application.Current.Resources["DatePicker"]))
            {
                lastDonationDate.Foreground = Brushes.Black;
                lastDonationDate.Clear();
            }
        }

        private void LastDonationDate_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (lastDonationDate.Text.Equals(""))
            {
                lastDonationDate.Text = (string) Application.Current.Resources["DatePicker"];
                lastDonationDate.Foreground = Brushes.White;
            }
        }
    }
}