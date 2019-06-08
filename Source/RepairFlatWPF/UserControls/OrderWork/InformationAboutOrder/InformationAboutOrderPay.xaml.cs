using Newtonsoft.Json;
using RepairFlatWPF.Controller;
using RepairFlatWPF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
    /// Interaction logic for InformationAboutOrderPay.xaml
    /// </summary>
    public partial class InformationAboutOrderPay : UserControl
    {
        #region Переменные
        DataTable DataAboutPayment;
        Guid idOrder;
        List<Tuple<int, Guid>> ListOfIdPayment;
        #endregion
        #region Контструктор
        public InformationAboutOrderPay(Guid idOrder)
        {
            InitializeComponent();
            this.idOrder = idOrder;
            MakeDefaultDataAboutDT();
        }
        #endregion

        #region Обработчики событий
        private void AddPayment_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Добавление данных о платежах");
            baseWindow.MakeOpen(new AddInfromationUserControl.AddPaymentInformation(idOrder, ref baseWindow));
            baseWindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {
                SaveSomeData.MakeSomeOperation = false;
                MakeDefaultDataAboutDT();
            }

        }

        private void EditPayment_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    Guid idPayment = ListOfIdPayment.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                    BaseWindow baseWindow = new BaseWindow("Редактирование данных об оплате");
                    baseWindow.MakeOpen(new AddInfromationUserControl.AddPaymentInformation(idOrder, ref baseWindow, idPayment));
                    baseWindow.ShowDialog();
                    if (SaveSomeData.MakeSomeOperation)
                    {
                        SaveSomeData.MakeSomeOperation = false;
                        MakeDefaultDataAboutDT();
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Не выбрана оплата для редактирование!",MsgBoxImage:MessageBoxImage.Hand);
            }
        }
        #endregion

        #region Прочие методы
        private void MakeDefaultDataAboutDT()
        {
            DataAboutPayment = new DataTable();
            ListOfIdPayment = new List<Tuple<int, Guid>>();
            foreach (string asd in SomeEnums.PayInf)
            {
                DataAboutPayment.Columns.Add(asd);
            }
            DataGrid.ItemsSource = DataAboutPayment.DefaultView;
            MakeDataAboutPayment();
        }

        async private void MakeDataAboutPayment()
        {
            var InformFromserver = await Task.Run(() => MakeSomeHelp.MakeDownloadByLink($"api/order/get/payment?idOrder={idOrder}"));
            var ListOfPayment = JsonConvert.DeserializeObject<Model.OrderDesc.DataAboutPaymentInOrder>(InformFromserver.ToString());
            MakeDataTable(ListOfPayment);
        }
        private void MakeDataTable(Model.OrderDesc.DataAboutPaymentInOrder paymentInOrder)
        {
            if (paymentInOrder.success)
            {
                int number = 1;
                foreach(var Pay in paymentInOrder.InfPayment)
                {
                    DataRow dataRow = DataAboutPayment.NewRow();
                    dataRow[0] = number;
                    dataRow[1] = Pay.DateOfMake.Value.ToString("dd.MM.yyy");
                    dataRow[2] = Pay.Summa?.ToString();
                    dataRow[3] = Pay.FioMake?.Trim();
                    dataRow[4] = Pay.Description;
                    DataAboutPayment.Rows.Add(dataRow);
                    ListOfIdPayment.Add(new Tuple<int, Guid>(number, Pay.idPayment));
                    number++;
                }

            }
        }

        #endregion





    }
}
