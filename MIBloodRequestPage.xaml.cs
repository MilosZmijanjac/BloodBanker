using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BloodBanker.DAO;
using BloodBanker.Model;

namespace BloodBanker
{
    /// <summary>
    ///     Interaction logic for MIBloodRequestPage.xaml
    /// </summary>
    public partial class MIBloodRequestPage : Page
    {
        private decimal bb_ID;
        private List<MedicalInstitution> bloodBanksList;
        private readonly MedicalInstitution medInst;
        private List<MyDonor> myDonorBBList;

        public MIBloodRequestPage(MedicalInstitution medInst)
        {
            this.medInst = medInst;
            InitializeComponent();
            LoadValues();
        }

        private void LoadValues()
        {
            try
            {
                bloodBanksList = MedicalInstitutionDAO.LoadMedInst("66");
                if (bloodBanksList != null && bloodBanksList.Count > 0)
                {
                    Req_List.DisplayMemberPath = "NAME";
                    Req_List.SelectedValuePath = "MI_ID";
                    Req_List.ItemsSource = bloodBanksList;
                }
                else
                {
                    Req_List.Items.Add((string) Application.Current.Resources["Empty"]);
                }

                myDonorBBList = MyDonorDAO.LoadMyDonorMI(medInst.MI_ID);
                if (myDonorBBList != null && myDonorBBList.Count > 0)
                {
                    Reciv_List.DisplayMemberPath = "Display";
                    Reciv_List.SelectedValuePath = "OR_ID";
                    Reciv_List.ItemsSource = myDonorBBList;
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

        private void Req_ComboBoxClosed(object sender, EventArgs e)
        {
            Reciv.Visibility = Visibility.Hidden;
            Req.Visibility = Visibility.Visible;
            if (Req_List.SelectedItem.ToString().Equals((string)App.Current.Resources["Empty"])) return;
            try
            {
                var mi = MedicalInstitutionDAO.GetMedicalInstitution(decimal.Parse(Req_List.SelectedValue.ToString()));
                if (mi != null)
                {
                    bb_ID = mi.MI_ID;
                    BB_Name.Content = mi.NAME;
                    Email.Text = mi.EMAIL;
                    Address.Text = mi.ADDRESS;
                    City.Text = mi.CITY;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["Exception"] + " " + ex.Message);
            }
        }

        private void BloodRequest_Click(object sender, RoutedEventArgs e)
        {
            if (Quantity.Text.Equals("") && !numberCheck(Quantity))
            {
                MessageBox.Show((string) Application.Current.Resources["InvalidQuantity"]);
                return;
            }

            try
            {
                var newOrder = new Orders();
                newOrder.B_GRP = B_grp.Text;
                newOrder.MI_ID = medInst.MI_ID;
                newOrder.QUANTITY = int.Parse(Quantity.Text);
                newOrder.REQ_DATE = DateTime.Now;
                newOrder.RECIP_ID = MedicalInstitutionDAO.GetAppID(medInst);
                newOrder.DONOR_ID = UserDAO.GetAppID(bb_ID);
                OrdersDAO.StoreOrders(newOrder);
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["Exception"] + " " + ex.Message);
                return;
            }

            MessageBox.Show((string) Application.Current.Resources["OrderSuccess"]);
        }

        private void Reciv_ComboBoxClosed(object sender, EventArgs e)
        {
            Req.Visibility = Visibility.Hidden;
            Reciv.Visibility = Visibility.Visible;
            if (Reciv_List.SelectedItem.ToString().Equals((string)App.Current.Resources["Empty"])) return;
            if (Reciv_List.SelectedValue != null)
                try
                {
                    var order = OrdersDAO.GetOrder(Reciv_List.SelectedValue.ToString());
                    if (order != null)
                    {
                        Reciv_Or_Id.Text = order.OR_ID.ToString();
                        Reciv_BB_Name.Text = MedicalInstitutionDAO.GetMedicalInstitution(order.MI_ID).NAME.Trim();
                        Reciv_B_grp.Text = order.B_GRP;
                        Reciv_quantity.Text = order.QUANTITY.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show((string) Application.Current.Resources["Exception"] + " " + ex.Message);
                }
        }

        private void BloodRecived_Click(object sender, RoutedEventArgs e)
        {
            if (Reciv_quantity.Text.Equals("") ||
                Reciv_Date.Text.Equals((string) Application.Current.Resources["DatePicker"]) ||
                !numberCheck(Reciv_quantity))
            {
                MessageBox.Show((string) Application.Current.Resources["InvalidQuantityDate"]);
                return;
            }

            DateTime dt;
            if (DateTime.TryParse(Reciv_Date.Text, new CultureInfo("fr-FR"), DateTimeStyles.AdjustToUniversal, out dt))
                try
                {
                    OrdersDAO.UpdateOrders(decimal.Parse(Reciv_Or_Id.Text), dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show((string) Application.Current.Resources["OrdersUpdateException"] + " " + ex.Message);
                }
            else
                MessageBox.Show((string) Application.Current.Resources["DatePickerError"]);

            LoadValues();
            MessageBox.Show((string) Application.Current.Resources["OrderSuccess"]);
        }

        private void Reciv_Date_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (Reciv_Date.Text.Equals(""))
            {
                Reciv_Date.Text = (string) Application.Current.Resources["DatePicker"];
                Reciv_Date.Foreground = Brushes.White;
            }
        }

        private void Reciv_Date_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (Reciv_Date.Text.Equals((string) Application.Current.Resources["DatePicker"]))
            {
                Reciv_Date.Foreground = Brushes.Black;
                Reciv_Date.Clear();
            }
        }

        private void number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private bool numberCheck(TextBox textBox)
        {
            foreach (var i in textBox.Text)
                if (i > '9' || i < '0')
                    return false;
            return true;
        }

        
    }
}