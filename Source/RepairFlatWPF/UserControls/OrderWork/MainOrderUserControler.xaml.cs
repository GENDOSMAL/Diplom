using System;
using System.Collections.Generic;
using System.Data;
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
        DataTable AllDataOfOrder;
        
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
            var OrderWork = new BaseWindow( new OrderWork.MakeNewOrderUserControl(),"Cоздание нового заказа");
            OrderWork.ShowDialog();
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            int NumberOfOrder = 0;
            var OrderWork = new BaseWindow(new OrderWork.MakeNewOrderUserControl(false),$"Редактирование заказа №{NumberOfOrder}");
            OrderWork.ShowDialog();
        }


        private bool MakeSomeCheck()
        {
            bool result = true;
            if (SelectedType.SelectedIndex == -1)
            {
                MakeSomeHelp.MakeMessageBox("Укажите критерий поиска", MsgBoxImage: MessageBoxImage.Warning);
                result = false;
            }
            if (string.IsNullOrEmpty(SearchText.Text.Trim()))
            {
                result = false;
                MakeSomeHelp.MakeMessageBox("Укажите текст для поиска", MsgBoxImage: MessageBoxImage.Warning);
            }
            return result;
        }

        private void SelectOrder_Click(object sender, RoutedEventArgs e)
        {
            if (MakeSomeCheck())
            {
                string sortOrder = "CompanyName ASC";
                string expression = "";
                DataRow[] foundRows;
                DataTable dataTable=new DataTable();
                foundRows = AllDataOfOrder.Select(expression, sortOrder);
                //Заполнить побочный
                
            }
        }
    }
}
