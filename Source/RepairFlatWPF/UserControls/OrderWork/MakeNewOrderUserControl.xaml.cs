using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace RepairFlatWPF.UserControls.OrderWork
{
    /// <summary>
    /// Interaction logic for MakeNewOrderUserControl.xaml
    /// </summary>
    public partial class MakeNewOrderUserControl : UserControl
    {
        public MakeNewOrderUserControl()
        {
            InitializeComponent();

            foreach (var StatOfOrder in Model.SomeEnums.StatusOfOrder)
            {
                StatusOfOrders.Items.Add(StatOfOrder);
            }
        }

        private void CreateNewOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectClient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddAdress_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectBrigade_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            UserControls.BaseWindow baseWindow = new BaseWindow(false,new MainOrderUserControler(),"ЧТОТО");
            baseWindow.ShowDialog();
        }

        private void AddContactData_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
