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
            foreach(var type in Model.SomeEnums.RypeOfSearchOrder)
            {
                SelectedType.Items.Add(type);
            }
        }


        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            var OrderWork = new BaseWindow( new OrderWork.MakeNewOrderUserControl(),"Cоздание нового заказа");
            try
            {
                OrderWork.ShowDialog();
            }
            catch
            {
                OrderWork.Close();
            }
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            int NumberOfOrder = 0;
            var OrderWork = new BaseWindow(new OrderWork.MakeNewOrderUserControl(false),$"Редактирование заказа №{NumberOfOrder}");
            try
            {
                OrderWork.ShowDialog();
            }
            catch
            {
                OrderWork.Close();
            }
        }


        private bool MakeSomeCheck()
        {
            bool result = true;
            if (SelectedType.SelectedIndex == -1)
            {
                MakeSomeHelp.MSG("Укажите критерий поиска", MsgBoxImage: MessageBoxImage.Warning);
                result = false;
            }
            if (string.IsNullOrEmpty(SearchText.Text.Trim()))
            {
                result = false;
                MakeSomeHelp.MSG("Укажите текст для поиска", MsgBoxImage: MessageBoxImage.Warning);
            }
            return result;
        }

        private void SelectOrder_Click(object sender, RoutedEventArgs e)
        {//TODO тут открытие без проверки
            MakeSomeHelp.DataGridMakeWork(new OrderWork.MainWorkWithOrderUserControl(Guid.NewGuid()));

            int SelectIndex = DataGrid.SelectedIndex;
            if (SelectIndex != -1)
            {
                Guid IdOrder = SelectGuidById(SelectIndex);
                MakeSomeHelp.DataGridMakeWork(new OrderWork.MainWorkWithOrderUserControl(IdOrder));
            }
            else
            {
                MakeSomeHelp.MSG("Не выбрана заказ для дальнейшей работы",MsgBoxImage: MessageBoxImage.Warning);
            }
        }



        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (MakeSomeCheck())
            {
                try
                {
                    string sortOrder = $"{AllDataOfOrder.Columns[0].Caption} ASC";
                    string expression = "";
                    DataRow[] foundRows;
                    DataTable dataTable = new DataTable();
                    foundRows = AllDataOfOrder.Select(expression, sortOrder);

                }
                catch
                {
                    MakeSomeHelp.MSG("Произошла ошибка при поиске");
                }
                finally
                {

                }
            }
        }


        private int SelectNummberById(int id)
        {
            //TODO скопировать способ нахождения номера по id

            throw new NotImplementedException();
        }

        private Guid SelectGuidById(int SelectIndex)
        {
            int Number = SelectNummberById(SelectIndex);
            throw new NotImplementedException();
        }
    }
}
