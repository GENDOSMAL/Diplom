using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        DataTable SelectedData;

        List<Tuple<int, Guid?>> DataAboutidOrder;
        public MainOrderUserControler()
        {
            InitializeComponent();
            foreach (var type in SomeEnums.RypeOfSearchOrder)
            {
                SertedType.Items.Add(type);
            }
            DownloadDataAboutOrder();
        }

        private async void DownloadDataAboutOrder()
        {
            AllDataOfOrder = new DataTable("Client");
            foreach (string NameOfColumn in SomeEnums.OrderMainTable)
            {
                AllDataOfOrder.Columns.Add(NameOfColumn);
            }
            DataGrid.ItemsSource = AllDataOfOrder.DefaultView;
            DataAboutidOrder = new List<Tuple<int, Guid?>>();
            var InformFromserver = await Task.Run(() => MakeDownloadByLink($"api/order/allorders"));
            var ListofOrders = JsonConvert.DeserializeObject<Model.OrderDesc.AllOrder>(InformFromserver.ToString());
            if (ListofOrders.success)
            {
                int number = 1;
                foreach (var orderInf in ListofOrders.listOfOrders)
                {
                    DataRow newClientRow = AllDataOfOrder.NewRow();
                    newClientRow[0] = number;
                    newClientRow[1] = orderInf.DataStart.Value.ToString("dd.MM.yyyy");
                    newClientRow[2] = SomeEnums.StatusOfOrder[orderInf.Status ?? default(int)];
                    newClientRow[3] = orderInf.FIOClient;
                    newClientRow[4] = orderInf.ContactData;
                    newClientRow[5] = orderInf.DataAboutAdress;
                    newClientRow[6] = orderInf.AllSumma;
                    newClientRow[7] = orderInf.Desc;

                    AllDataOfOrder.Rows.Add(newClientRow);
                    DataAboutidOrder.Add(new Tuple<int, Guid?>(number, orderInf.idOrder));
                    number++;
                }
            }
        }

        public object MakeDownloadByLink(string UrlOfDownload)
        {
            return BaseWorkWithServer.CatchErrorWithGet(UrlOfDownload, "GET", nameof(MakeLoading), nameof(MakeDownloadByLink));
        }


        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Cоздание нового заказа");
            baseWindow.MakeOpen(new OrderWork.MakeNewOrderUserControl(ref baseWindow));
            baseWindow.ShowDialog();
            DownloadDataAboutOrder();
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            int SelectIndex = DataGrid.SelectedIndex;
            if (SelectIndex != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, SelectIndex);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    var idSelectOrder = DataAboutidOrder.Where(e1 => e1.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                    BaseWindow baseWindow = new BaseWindow($"Редактирование заказа");
                    baseWindow.MakeOpen(new OrderWork.MakeNewOrderUserControl(ref baseWindow, false, idSelectOrder ?? default(Guid)));
                    baseWindow.ShowDialog();
                    DownloadDataAboutOrder();
                }
            }
            else
            {
                MakeSomeHelp.MSG("Не выбрана заказ для редактирования", MsgBoxImage: MessageBoxImage.Warning);
            }

        }


        private bool MakeSomeCheck()
        {
            bool result = true;
            if (SertedType.SelectedIndex == -1)
            {
                MakeSomeHelp.MSG("Укажите критерий поиска", MsgBoxImage: MessageBoxImage.Warning);
                result = false;
            }
            if (string.IsNullOrEmpty(SearchText.Text.Trim()) && SertedType.SelectedIndex != 0)
            {
                result = false;
                MakeSomeHelp.MSG("Укажите текст для поиска", MsgBoxImage: MessageBoxImage.Warning);
            }
            return result;
        }

        private void SelectOrder_Click(object sender, RoutedEventArgs e)
        {
            int SelectIndex = DataGrid.SelectedIndex;
            if (SelectIndex != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, SelectIndex);
                string fioclient = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, SelectIndex,3).ToString();
                string adress = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, SelectIndex, 5).ToString();
                string allsumma = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, SelectIndex, 6).ToString();
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    var idSelectOrder = DataAboutidOrder.Where(e1 => e1.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                    MakeSomeHelp.DataGridMakeWork(new OrderWork.MainWorkWithOrderUserControl(idSelectOrder ?? default(Guid),FioClient:fioclient,Adress:adress,AllSumma:allsumma));
                }
            }
            else
            {
                MakeSomeHelp.MSG("Не выбрана заказ для дальнейшей работы", MsgBoxImage: MessageBoxImage.Warning);
            }
        }



        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (MakeSomeCheck())
            {
                try
                {
                    int idColumn = SertedType.SelectedIndex;

                    switch (SertedType.SelectedIndex)
                    {
                        case 1:
                            idColumn = 2;
                            break;
                        case 2:
                            idColumn = 1;
                            break;
                        case 3:
                            idColumn = 3;
                            break;
                    }


                    if (SertedType.SelectedIndex==0)
                    {
                        DataGrid.ItemsSource = AllDataOfOrder.DefaultView;
                    }
                    else
                    {
                        SelectedData = new DataTable("select");
                        foreach (string NameOfColumn in SomeEnums.OrderMainTable)
                        {
                            SelectedData.Columns.Add(NameOfColumn);
                        }
                        DataGrid.ItemsSource = SelectedData.DefaultView;
                        List<DataRow> listOfOrder = new List<DataRow>();
                        for (int i = 0; i < AllDataOfOrder.Rows.Count; i++)
                        {
                            if (AllDataOfOrder.Rows[i][idColumn].ToString().Trim().Contains( SearchText.Text.Trim()))
                            {
                                DataRow newOrderRow = SelectedData.NewRow();
                                newOrderRow[0] = AllDataOfOrder.Rows[i][0];
                                newOrderRow[1] = AllDataOfOrder.Rows[i][1];
                                newOrderRow[2] = AllDataOfOrder.Rows[i][2];
                                newOrderRow[3] = AllDataOfOrder.Rows[i][3];
                                newOrderRow[4] = AllDataOfOrder.Rows[i][4];
                                newOrderRow[5] = AllDataOfOrder.Rows[i][5];
                                newOrderRow[6] = AllDataOfOrder.Rows[i][6];
                                newOrderRow[7] = AllDataOfOrder.Rows[i][7];
                                listOfOrder.Add(newOrderRow);
                            }
                        }
                        if (listOfOrder.Count != 0)
                        {
                            SelectedData.Rows.Clear();
                            for(int i=0;i< listOfOrder.Count; i++)
                            {
                                SelectedData.Rows.Add(listOfOrder[i]);
                            }
                        }
                        else
                        {
                            DataGrid.ItemsSource = AllDataOfOrder.DefaultView;
                            MakeSomeHelp.MSG("Не найдено информации по критериям указанным в качестве искомых!", MsgBoxImage: MessageBoxImage.Warning);
                        }
                    }

                }
                catch(Exception ex)
                {
                    MakeSomeHelp.MSG($"Произошла ошибка при поиске{ex.ToString()}");
                }
                finally
                {

                }
            }
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.MSG("Не реализовано", MsgBoxImage: MessageBoxImage.Warning);

        }

    }
}
