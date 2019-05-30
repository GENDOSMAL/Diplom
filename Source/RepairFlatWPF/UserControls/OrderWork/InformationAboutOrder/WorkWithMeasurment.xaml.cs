using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RepairFlatWPF.UserControls.OrderWork
{
    /// <summary>
    /// Interaction logic for WorkWithMeasurment.xaml
    /// </summary>
    public partial class WorkWithMeasurment : UserControl
    {
        Guid idOrder;
        DataTable AllDataAboutMeasurment;
        List<Tuple<int, Guid?>> DataAboutMeasurment = new List<Tuple<int, Guid?>>();
        public WorkWithMeasurment(Guid IdOrder)
        {
            InitializeComponent();
            this.idOrder = IdOrder;
            MakeDataAboutMeasurment();

        }

        private async void MakeDataAboutMeasurment()
        {
            AllDataAboutMeasurment = new DataTable("Measurment");
            foreach (string NameOfColumn in SomeEnums.MeasurmentMainTable)
            {
                AllDataAboutMeasurment.Columns.Add(NameOfColumn);
            }
            DataGrid.ItemsSource = AllDataAboutMeasurment.DefaultView;
            DataAboutMeasurment = new List<Tuple<int, Guid?>>();
            var InformFromserver = await Task.Run(() => MakeDownloadByLink($"api/measurment/allmeastbl?idOrder={idOrder}"));
            var ListofOrders = JsonConvert.DeserializeObject<Model.MeasuModel.AllDataAbMeas>(InformFromserver.ToString());
            if (ListofOrders.listofmeas != null)
            {
                int number = 1;
                foreach (var MeasInf in ListofOrders.listofmeas)
                {
                    DataRow newMesRow = AllDataAboutMeasurment.NewRow();
                    newMesRow[0] = number;
                    newMesRow[1] = MeasInf.NameOfPremises?.Trim();
                    newMesRow[2] = MeasInf.Description?.Trim();
                    newMesRow[3] = MeasInf.Height;
                    newMesRow[4] = MeasInf.Width;
                    newMesRow[5] = MeasInf.Lenght;
                    newMesRow[6] = MeasInf.Pwalls;
                    newMesRow[7] = MeasInf.PCelling;
                    newMesRow[8] = MeasInf.Swalls;
                    newMesRow[9] = MeasInf.Sfloor;

                    AllDataAboutMeasurment.Rows.Add(newMesRow);
                    DataAboutMeasurment.Add(new Tuple<int, Guid?>(number, MeasInf.idMeasurment));
                    number++;
                }
            }

        }

        public object MakeDownloadByLink(string UrlOfDownload)
        {
            return BaseWorkWithServer.CatchErrorWithGet(UrlOfDownload, "GET", nameof(MakeLoading), nameof(MakeDownloadByLink));
        }

        private void AddMeasurmant_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Добавление данных о помещениях");
            baseWindow.MakeOpen(new AddInfromationUserControl.AddPremises(idOrder, ref baseWindow));
            baseWindow.ShowDialog();
            MakeDataAboutMeasurment();
        }

        private void EditMeasurment_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    Guid idPremises = DataAboutMeasurment.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First() ?? default(Guid);
                    BaseWindow baseWindow = new BaseWindow("Редактирование данных о помещениях");
                    baseWindow.MakeOpen(new AddInfromationUserControl.AddPremises(idOrder, ref baseWindow, idPremises));
                    baseWindow.ShowDialog();
                    MakeDataAboutMeasurment();
                }
            }
            else
            {
                MakeSomeHelp.MSG("Не выбрано помещений для редактирование!");
            }


        }

        private void DeleteMeasurment_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.MSG("Не реализовано");
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.MSG("Не реализовано");
        }
    }
}
