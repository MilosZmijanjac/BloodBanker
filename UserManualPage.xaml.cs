using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace BloodBanker
{
    /// <summary>
    /// Interaction logic for UserManualPage.xaml
    /// </summary>
    public partial class UserManualPage : Page
    {
        public UserManualPage()
        {
            InitializeComponent();
            
            myBrowser.Navigate(new Uri( Path.GetFullPath((string)App.Current.Resources["UserManualLocation"]), UriKind.Absolute));


        }
    }
}
