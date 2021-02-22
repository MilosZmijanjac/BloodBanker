using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BloodBanker.DAO;
using BloodBanker.Model;

namespace BloodBanker
{
    /// <summary>
    ///     Interaction logic for UserViewRequestsPage.xaml
    /// </summary>
    public partial class UserViewRequestsPage : Page
    {
        private decimal bb_ID;
        private List<User> list1;
        private List<MedicalInstitution> list2;
        private readonly User user;

        public UserViewRequestsPage(User u)
        {
            user = u;
            InitializeComponent();
            InitComboBoxes();
        }

        private void InitComboBoxes()
        {
            try
            {
                list1 = UserDAO.LoadUsersDistinct(user.SSN);
                if (list1 != null && list1.Count > 0)
                {
                    RList.DisplayMemberPath = "NAME";
                    RList.SelectedValuePath = "SSN";
                    RList.ItemsSource = list1;
                }
                else
                {
                    RList.Items.Add((string) Application.Current.Resources["Empty"]);
                }

                list2 = MedicalInstitutionDAO.LoadMedInst("66");
                if (list2 != null && list2.Count > 0)
                {
                    BBList.DisplayMemberPath = "NAME";
                    BBList.SelectedValuePath = "MI_ID";
                    BBList.ItemsSource = list2;
                }
                else
                {
                    BBList.Items.Add((string) Application.Current.Resources["Empty"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["Exception"] + " " + ex.Message);
            }
        }

        private void onR_ComboBoxClosed(object sender, EventArgs e)
        {
            BB_details.Visibility = Visibility.Hidden;
            R_details.Visibility = Visibility.Visible;
            if (RList.SelectedItem.ToString().Equals((string)App.Current.Resources["Empty"])) return;
                if (RList.SelectedValue != null )
                try
            {
                
                var u = UserDAO.GetUser(decimal.Parse(RList.SelectedValue.ToString()));
                if (u != null)
                {
                    Ph_no.Text = u.PH_NO.ToString();
                    R_Name.Text = u.NAME;
                    R_Bgrp.Text = u.B_GRP;
                    R_Email.Text = u.EMAIL;
                    R_Address.Text = u.ADDRESS;
                    R_City.Text = u.CITY;
                    }
                    else
                    {
                        MessageBox.Show((string)Application.Current.Resources["Empty"]);
                    }
                }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["Exception"] + " " + ex.Message);
            }
        }

        private void onBB_ComboBoxClosed(object sender, EventArgs e)
        {
            R_details.Visibility = Visibility.Hidden;
            BB_details.Visibility = Visibility.Visible;
            if (BBList.SelectedItem.ToString().Equals((string)App.Current.Resources["Empty"])) return;
            if (BBList.SelectedValue != null)
                try
            {
                var bb = MedicalInstitutionDAO.GetMedicalInstitution(decimal.Parse(BBList.SelectedValue.ToString()));
                if (bb != null)
                {
                    bb_ID = bb.MI_ID;
                    BB_Name.Text = bb.NAME;
                    BB_Ph.Text = bb.PHONE.ToString();
                    BB_Website.Text = bb.WEBSITE;
                    BB_Email.Text = bb.EMAIL;
                    BB_Loc.Text = bb.ADDRESS;
                    BB_City.Text = bb.CITY;
                }
                    else
                    {
                        MessageBox.Show((string)Application.Current.Resources["Empty"]);
                    }
                }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["Exception"] + " " + ex.Message);
            }
        }

        private void DonateBB(object sender, RoutedEventArgs e)
        {
            try
            {
                var newOrder = new Orders();
                newOrder.MI_ID = (long) bb_ID;
                newOrder.QUANTITY = 1;
                newOrder.REQ_DATE = DateTime.Now;
                newOrder.DEL_DATE = DateTime.Now;
                newOrder.B_GRP = user.B_GRP;
                newOrder.DONOR_ID = UserDAO.GetAppID(user.SSN);
                newOrder.RECIP_ID = UserDAO.GetAppID(bb_ID);
                OrdersDAO.StoreOrders(newOrder);
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["Exception"] + " " + ex.Message);
            }

            MessageBox.Show((string) Application.Current.Resources["Exception"]);
        }
    }
}