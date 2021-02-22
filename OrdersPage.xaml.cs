using System.Windows.Controls;
using BloodBanker.DAO;
using BloodBanker.Model;

namespace BloodBanker
{
    /// <summary>
    ///     Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        private readonly MedicalInstitution medInst;
        private readonly User user;

        public OrdersPage(object ob)
        {
            InitializeComponent();
            user = ob as User;
            if (!(ob is User))
            {
                user = null;
                medInst = ob as MedicalInstitution;
                InitDataGrid(MedicalInstitutionDAO.GetAppID(medInst));
            }
            else
            {
                InitDataGrid(UserDAO.GetAppID(user.SSN));
            }
        }

        private void InitDataGrid(long id)
        {
            ordersDataGrid.ItemsSource = OrdersDAO.LoadOrders(id);
            ordersDataGrid.CanUserAddRows = false;
        }
    }
}