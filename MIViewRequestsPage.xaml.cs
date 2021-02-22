using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BloodBanker.DAO;
using BloodBanker.Model;

namespace BloodBanker
{
    /// <summary>
    ///     Interaction logic for MIViewRequestsPage.xaml
    /// </summary>
    public partial class MIViewRequestsPage : Page
    {
        private readonly MedicalInstitution medInst;
        private List<MyDonor> milist;

        public MIViewRequestsPage(MedicalInstitution medInst)
        {
            this.medInst = medInst;
            InitializeComponent();
            LoadValues();
        }

        private void LoadValues()
        {
            try
            {
                milist = MyDonorDAO.LoadMyDonorInverseMI(medInst.MI_ID);
                if (milist != null && milist.Count > 0)
                {
                    ReqList.DisplayMemberPath = "DisplayLong";
                    ReqList.SelectedValue = "OR_ID";
                    ReqList.ItemsSource = milist;
                }
                else
                {
                    ReqList.Items.Add((string) Application.Current.Resources["Empty"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["Exception"] + " " + ex.Message);
            }
        }

        private void ReqList_DropDownClosed(object sender, EventArgs e)
        {
            
            if (ReqList.SelectedValue == null || ReqList.SelectedItem.ToString().Equals((string)App.Current.Resources["Empty"])) return;
                try
            {
                var order = OrdersDAO.GetOrder(long.Parse(ReqList.SelectedValue.ToString()));
                var medList = MedicalInstitutionDAO.GetMedicalInstitution(UserDAO.GetUserID(order.RECIP_ID));
                if (medList != null)
                {
                    HosName.Content = medList.NAME;
                    HosPh.Content = medList.PHONE.ToString();
                    HosB_Grp.Content = order.B_GRP;
                    HosLoc.Content = medList.ADDRESS;
                    HosCity.Content = medList.CITY;
                    Quantity.Content = order.QUANTITY;
                }
                else
                {
                    MessageBox.Show((string) Application.Current.Resources["Empty"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["Exception"] + " " + ex.Message);
            }
        }
    }
}