using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BloodBanker.DAO;
using BloodBanker.Model;

namespace BloodBanker
{
    /// <summary>
    ///     Interaction logic for UserHomePage.xaml
    /// </summary>
    public partial class UserHomePage : Page
    {
        public UserHomePage(User u)
        {
            InitializeComponent();
            InitDataGrid(u.TYPE_OF_USER, u.B_GRP);
        }

        private void InitDataGrid(string type, string bloodGroup)
        {
            try
            {
                if (type.Equals("68"))
                {
                    D.Visibility = Visibility.Visible;
                    var list = MedicalInstitutionDAO.LoadMedInst("66");
                    if (list.Count() != 0)
                    {
                        D_BB_NoData.Visibility = Visibility.Hidden;
                        D_BB_grid.Visibility = Visibility.Visible;
                        D_BBlist.ItemsSource = list;
                    }
                    else
                    {
                        D_BB_grid.Visibility = Visibility.Hidden;
                        D_BB_NoData.Visibility = Visibility.Visible;
                    }
                }
                else if (type.Equals("82"))
                {
                    R.Visibility = Visibility.Visible;
                    var list = MedicalInstitutionDAO.LoadMedInst("66");
                    if (list.Count() != 0)
                    {
                        R_BB_NoData.Visibility = Visibility.Hidden;
                        R_BB_grid.Visibility = Visibility.Visible;
                        R_BBlist.ItemsSource = list;
                    }
                    else
                    {
                        R_BB_grid.Visibility = Visibility.Hidden;
                        R_BB_NoData.Visibility = Visibility.Visible;
                    }

                    var list1 = UserDAO.LoadUsers("68", bloodGroup);
                    if (list1.Count() != 0)
                    {
                        R_D_NoData.Visibility = Visibility.Hidden;
                        R_D_grid.Visibility = Visibility.Visible;
                        Dlist.ItemsSource = list1;
                    }
                    else
                    {
                        R_D_grid.Visibility = Visibility.Hidden;
                        R_D_NoData.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    MessageBox.Show((string) Application.Current.Resources["UserHomeException"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show((string) Application.Current.Resources["UserHomeException"] + " " + ex.Message);
            }
        }
    }
}