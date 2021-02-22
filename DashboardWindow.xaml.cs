using BloodBanker.Model;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFCustomMessageBox;

namespace BloodBanker
{
    /// <summary>
    ///     Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : Window
    {
        private readonly Donor donor;
        private readonly MedicalInstitution medInst;
        private readonly User user;
        private string xml;


        public DashboardWindow(object ob)
        {
            InitializeComponent();
            if ((donor = ob as Donor) != null)
            {
                usernameTextBlock.Text = donor.NAME;
                xml = donor.USERNAME;
                ShowReqView();
                
                DashboardFrame.Content = new UserHomePage(donor);


            }
            else if ((medInst = ob as MedicalInstitution) != null)
            {
                usernameTextBlock.Text = medInst.NAME;
                xml = medInst.USERNAME;
                if (medInst.TYPE_OF_MI.Equals("66"))
                    ShowReqView();
                else
                    ShowReqBlood();
                DashboardFrame.Content = new MIHomePage(medInst);
                
            }
            else
            {
                user = ob as User;
                usernameTextBlock.Text = user.NAME;
                xml = user.USERNAME;
                if (user.TYPE_OF_USER.Equals("68"))
                    ShowReqView();
                else
                    ShowReqBlood();
                DashboardFrame.Content = new UserHomePage(user);
                

            }
            InitSettings();
        }

        private void InitSettings()
        {
            if (File.Exists(App._DefaultDirectoryPath + xml + ".xml"))
                App.settings = MySettings.Read(App._DefaultDirectoryPath + xml + ".xml");
            MainWindow.ApplyResources(App.settings.Language);
            MainWindow.ApplyResources(App.settings.Theme);
            ListViewItem_PreviewMouseLeftButtonUp(ListViewMenu.SelectedItem, null);
        }
        public void Dashboard_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewMenu.SelectedItems.Clear();

            var item = sender as ListViewItem;
            if (item != null)
            {
                item.IsSelected = true;
                ListViewMenu.SelectedItem = item;
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item == home && item.IsSelected)
            {
                if (user == null && donor == null) DashboardFrame.Content = new MIHomePage(medInst);

                else if (user == null) DashboardFrame.Content = new UserHomePage(donor);

                else DashboardFrame.Content = new UserHomePage(user);
            }
            else if (item == ReqBlood && item.IsSelected)
            {
                if (user != null)
                    DashboardFrame.Content = new UserBloodRequestPage(user);
                else if (donor != null)
                    DashboardFrame.Content = new UserBloodRequestPage(donor);
                else
                    DashboardFrame.Content = new MIBloodRequestPage(medInst);
            }
            else if (item == ReqView && item.IsSelected)
            {
                if (user != null)
                    DashboardFrame.Content = new UserViewRequestsPage(user);
                else if (donor != null)
                    DashboardFrame.Content = new UserViewRequestsPage(donor);
                else
                    DashboardFrame.Content = new MIViewRequestsPage(medInst);
            }
            else if (item == orders && item.IsSelected)
            {
                if (user != null)
                    DashboardFrame.Content = new OrdersPage(user);
                else if (donor != null)
                    DashboardFrame.Content = new OrdersPage(donor);
                else
                    DashboardFrame.Content = new OrdersPage(medInst);
            }
            else if (item == settings && item.IsSelected)
            {
                if (user != null)
                    DashboardFrame.Content = new SettingsPage(user);
                else if (donor != null)
                    DashboardFrame.Content = new SettingsPage(donor);
                else
                    DashboardFrame.Content = new SettingsPage(medInst);
            }
            else if (item == manual && item.IsSelected)
            {
                DashboardFrame.Content = new UserManualPage();
            }
            else if (item == logout && item.IsSelected)
            {
                if (CustomMessageBox.ShowYesNo((string)Application.Current.Resources["LogOutQuestion"],
                        (string)Application.Current.Resources["Confirm"],
                        (string)Application.Current.Resources["Yes"], (string)Application.Current.Resources["No"]) ==
                    MessageBoxResult.Yes)
                {
                    App.settings.Save(App._DefaultDirectoryPath + xml + ".xml");
                    new MainWindow().Show();
                    Close();
                }
            }
        }

        public void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CustomMessageBox.ShowYesNo((string)Application.Current.Resources["ExitQuestion"],
                    (string)Application.Current.Resources["Confirm"],
                    (string)Application.Current.Resources["Yes"], (string)Application.Current.Resources["No"]) ==
                MessageBoxResult.Yes)
            {

                var task = new Task(() => { App.settings.Save(App._DefaultDirectoryPath + xml + ".xml"); });
                task.Start();

                Close();
            }

        }

        public void Minimise_Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ShowReqView()
        {
            ReqView.Visibility = Visibility.Visible;
            ReqBlood.Visibility = Visibility.Collapsed;
        }

        private void ShowReqBlood()
        {
            ReqView.Visibility = Visibility.Collapsed;
            ReqBlood.Visibility = Visibility.Visible;
        }

        private void Theme1ButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow.ApplyResources("Theme1.xaml");
            App.settings.Theme = "Theme1.xaml";
        }

        private void Theme2ButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow.ApplyResources("Theme2.xaml");
            App.settings.Theme = "Theme2.xaml";
        }
        private void Theme3ButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow.ApplyResources("Theme3.xaml");
            App.settings.Theme = "Theme3.xaml";
        }
        private void EnglishButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow.ApplyResources("en-US.xaml");
            App.settings.Language = "en-US.xaml";
            ListViewItem_PreviewMouseLeftButtonUp(ListViewMenu.SelectedItem, null);
        }
        private void SerbianButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow.ApplyResources("sr-Latn-RS.xaml");
            App.settings.Language = "sr-Latn-RS.xaml";
            ListViewItem_PreviewMouseLeftButtonUp(ListViewMenu.SelectedItem, null);
        }

    }
}