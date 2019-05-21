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

namespace RepairFlatWPF.UserControls
{
    /// <summary>
    /// Interaction logic for MainOrderUserControler.xaml
    /// </summary>
    public partial class MainOrderUserControler : UserControl
    {
        
        public MainOrderUserControler()
        {
            InitializeComponent();
            foreach(var type in Model.SomeEnums.RypeOfSearch)
            {
                SelectedType.Items.Add(type);
            }
        }


        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            var OrderWork = new OrderWork.MakeNewOrder(true);
            if (OrderWork.ShowDialog() != true)
            {
                MessageBox.Show("sad");
            }
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectOrder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
