using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BloodBanker.DAO;
using BloodBanker.Model;

namespace BloodBanker
{
    /// <summary>
    ///     Interaction logic for UserBloodRequestPage.xaml
    /// </summary>
    public partial class UserBloodRequestPage : Page
    {
        private decimal d_id;
        private List<User> donors;
        private List<MyDonor> myDonors;
        private decimal or_id;

        private readonly User user;

        public UserBloodRequestPage(User u)
        {
            user = u;
            InitializeComponent();
            InitComboBoxes();
        }

        private void InitComboBoxes()
        {
            try
            {
                donors = UserDAO.LoadUsers("68");
                    if (donors != null && donors.Count > 0)
                {
                    Req_List.DisplayMemberPath = "NAME";
                    Req_List.SelectedValuePath = "SSN";
                    Req_List.ItemsSource = donors;
                }
                else
                {
                    Req_List.Items.Add((string) Application.Current.Resources["Empty"]);
                }

                myDonors = MyDonorDAO.LoadMyDonor(user.SSN);
                if (myDonors != null && myDonors.Count > 0)
                {
                    Reciv_List.DisplayMemberPath = "Display";
                    Reciv_List.SelectedValuePath = "OR_ID";
                    Reciv_List.ItemsSource = myDonors;
                }
                else
                {
                    Reciv_List.Items.Add((string) Application.Current.Resources["Empty"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["Exception"] + " " + ex.Message);
            }
        }

        private void LoadValues(User u)
        {
            try
            {
                donors.Clear();
                donors = UserDAO.LoadUsers("68");

                if (donors == null || donors.Count == 0)
                {
                    donors.Clear();
                    Req_List.Items.Add((string) Application.Current.Resources["Empty"]);
                }

                myDonors.Clear();
                myDonors = MyDonorDAO.LoadMyDonor(u.SSN);
                if (myDonors == null || myDonors.Count < 0)
                {
                    myDonors.Clear();
                    Reciv_List.Items.Add((string) Application.Current.Resources["Empty"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["Exception"] + " " + ex.Message);
            }
        }

        private void Req_ComboBoxClosed(object sender, EventArgs e)
        {
            Reciv.Visibility = Visibility.Hidden;
            Req.Visibility = Visibility.Visible;
           
            if (Req_List.SelectedItem==null||Req_List.SelectedItem.ToString().Equals((string)App.Current.Resources["Empty"])) return;
            var donor = (User) Req_List.SelectedItem;
            if (donor != null)
            {
                d_id = donor.SSN;
                D_Name.Text = donor.NAME;
                B_grp.Text = donor.B_GRP;
                Email.Text = donor.EMAIL;
                Address.Text = donor.ADDRESS;
                City.Text = donor.CITY;
            }
        }

        private void Donate_ComboBoxClosed(object sender, EventArgs e)
        {
            Req.Visibility = Visibility.Hidden;
            Reciv.Visibility = Visibility.Visible;
            if (Reciv_List.SelectedItem ==null|| Reciv_List.SelectedItem.ToString().Equals((string)App.Current.Resources["Empty"])) return;
            if (Reciv_List.SelectedValue != null)
                try
                {
                    var order = OrdersDAO.GetOrder(Reciv_List.SelectedValue.ToString());

                    if (order != null)
                    {
                        d_id = UserDAO.GetUserID(order.DONOR_ID);
                        Console.WriteLine(d_id);
                        or_id = order.OR_ID;
                        Reciv_Or_Id.Text = order.OR_ID.ToString();
                        Reciv_D_Name.Text = UserDAO.GetUser(d_id).NAME;
                        Reciv_B_grp.Text = order.B_GRP;
                        Reciv_Hos.Text = MedicalInstitutionDAO.GetMedicalInstitution(order.MI_ID).NAME;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show((string) Application.Current.Resources["Exception"] + " " + ex.Message);
                }
        }

        private void BloodRequest_Click(object sender, RoutedEventArgs e)
        {
            var newOrder = new Orders();
            newOrder.B_GRP = B_grp.Text;
            newOrder.MI_ID = user.MI_ID;
            newOrder.RECIP_ID = UserDAO.GetAppID(user.SSN);
            newOrder.DONOR_ID = UserDAO.GetAppID(d_id);
            newOrder.REQ_DATE = DateTime.Now;
            newOrder.QUANTITY = 1;
            OrdersDAO.StoreOrders(newOrder);
            LoadValues(user);
        }

        private void BloodRecived_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt;
            if (DateTime.TryParse(Reciv_Date.Text, new CultureInfo("fr-FR"), DateTimeStyles.AdjustToUniversal, out dt))
                try
                {
                    OrdersDAO.UpdateOrders(or_id, dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show((string) Application.Current.Resources["OrdersUpdateException"] + " " + ex.Message);
                }
            else
                MessageBox.Show((string) Application.Current.Resources["DatePickerError"]);

            LoadValues(user);
        }

        private void Reciv_Date_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (Reciv_Date.Text.Equals(""))
            {
                Reciv_Date.Text = (string) Application.Current.Resources["DatePicker"];
                Reciv_Date.Foreground = Brushes.White;
            }
        }

        private void Reciv_Date_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (Reciv_Date.Text.Equals((string) Application.Current.Resources["DatePicker"]))
            {
                Reciv_Date.Foreground = Brushes.Black;
                Reciv_Date.Clear();
            }
        }
    }
}