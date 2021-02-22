using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BloodBanker.DAO;
using BloodBanker.Model;

namespace BloodBanker
{
    /// <summary>
    ///     Interaction logic for MIHomePage.xaml
    /// </summary>
    public partial class MIHomePage : Page
    {
        public MIHomePage(MedicalInstitution medInst)
        {
            InitializeComponent();
            InitDataGrid(medInst);
        }

        private void InitDataGrid(MedicalInstitution medInst)
        {
            try
            {
                if (medInst.TYPE_OF_MI.Equals("66"))
                {
                    BB.Visibility = Visibility.Visible;

                    var list1 = MedicalInstitutionDAO.LoadBB_Hos(medInst);
                    if (list1.Count() > 0)
                    {
                        BB_NoData.Visibility = Visibility.Hidden;
                        BB_grid.Visibility = Visibility.Visible;
                        BB_Hoslist.ItemsSource = list1;
                    }
                    else
                    {
                        BB_grid.Visibility = Visibility.Hidden;
                        BB_NoData.Visibility = Visibility.Visible;
                    }
                }
                else if (medInst.TYPE_OF_MI.Equals("72"))
                {
                    H.Visibility = Visibility.Visible;


                    var list2 = MedicalInstitutionDAO.LoadMedInst("66");
                    if (list2.Count() > 0)
                    {
                        H_BB_NoData.Visibility = Visibility.Hidden;
                        H_BB_grid.Visibility = Visibility.Visible;
                        H_BBlist.ItemsSource = list2;
                    }
                    else
                    {
                        H_BB_grid.Visibility = Visibility.Hidden;
                        H_BB_NoData.Visibility = Visibility.Visible;
                    }

                    var list3 = UserDAO.LoadUsers(medInst);
                    if (list3.Count() > 0)
                    {
                        H_P_NoData.Visibility = Visibility.Hidden;
                        H_P_grid.Visibility = Visibility.Visible;
                        Plist.ItemsSource = list3;
                    }
                    else
                    {
                        H_P_grid.Visibility = Visibility.Hidden;
                        H_P_NoData.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    //list.Visibility = Visibility.Hidden;
                    //label.Content = "ERROR";
                    //noData.Visibility = Visibility.Visible;
                    MessageBox.Show((string) Application.Current.Resources["UserHomeException"]);
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show((string) Application.Current.Resources["HomePageException"] + excep.Message);
            }
        }

        private void Plist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!(ItemsControl.ContainerFromElement((DataGrid) sender,
                e.OriginalSource as DependencyObject) is DataGridRow row))
            {
                e.Handled = true;
                return;
            }

            try
            {
                MessageBox.Show(((User) row.Item).DISEASE.Equals("None") ? "" : ((User) row.Item).DISEASE,
                    (string) Application.Current.Resources["Diseases"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("", (string) Application.Current.Resources["Diseases"]);
                Console.WriteLine(ex.Message);
            }
        }
    }
}