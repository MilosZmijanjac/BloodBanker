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
    ///     Interaction logic for RRegistrationPage.xaml
    /// </summary>
    public partial class RRegistrationPage : Page
    {
        private readonly User user;

        public RRegistrationPage(User user)
        {
            this.user = user;
            InitializeComponent();
            InitializeHospitalsCB();
        }

        private void InitializeHospitalsCB()
        {
            var hospitals = MedicalInstitutionDAO.LoadMedInst("72");
            if (hospitals != null && hospitals.Count > 0)
            {
                hospital.DisplayMemberPath = "NAME";
                hospital.SelectedValuePath = "MI_ID";
                hospital.ItemsSource = hospitals;
            }
            else
            {
                hospital.Items.Add((string) Application.Current.Resources["Empty"]);
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
                if (!DateTime.TryParse(dob.Text, new CultureInfo("fr-FR"), DateTimeStyles.AdjustToUniversal,
                    out var dt))
                {
                    MessageBox.Show((string) Application.Current.Resources["DatePickerError"] + " " +
                                    (string) Application.Current.Resources["DateOfBirth"]);
                    return;
                }

                user.DISEASE = disease.Text;
                user.MI_ID = long.Parse(hospital.SelectedValue.ToString());
                UserDAO.StoreUser(user);
                CustomMessageBox.ShowOK((string) Application.Current.Resources["UserRegistered"],
                    (string) Application.Current.Resources["Registration"], "OK");
                NavigationService.Navigate(new LoginPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    (string) Application.Current.Resources["DonorRegistrationException"] + "\n" + ex.Message);
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

        private void Dob_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (dob.Text.Equals((string) Application.Current.Resources["DatePicker"]))
            {
                dob.Foreground = Brushes.Black;
                dob.Clear();
            }
        }

        private void Dob_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (dob.Text.Equals(""))
            {
                dob.Text = (string) Application.Current.Resources["DatePicker"];
                dob.Foreground = Brushes.White;
            }
        }
    }
}