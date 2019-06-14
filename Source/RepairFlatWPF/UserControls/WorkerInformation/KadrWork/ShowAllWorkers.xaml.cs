using Newtonsoft.Json;
using RepairFlat.Model;
using RepairFlatWPF.Model;
using RepairFlatWPF.UserControls.KadrWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static RepairFlatWPF.Model.WorkerDescriptiom;
using static RepairFlatWPF.SomeEnums;
using DataTable = System.Data.DataTable;

namespace RepairFlatWPF.UserControls.WorkerInformation.KadrWork
{
    /// <summary>
    /// Interaction logic for ShowAllWorkers.xaml
    /// </summary>
    public partial class ShowAllWorkers : UserControl
    {
        DataTable DataAboutWorker;
        List<Tuple<int, Guid>> DataAboutGuid;
        TypeOfUserNeed typeOfUserNeed;
        string Route = "";
        BaseWindow BaseWindow;
        public ShowAllWorkers(ref BaseWindow baseWindow, TypeOfUserNeed typeOfUserNeed = TypeOfUserNeed.All)
        {
            InitializeComponent();
            this.typeOfUserNeed = typeOfUserNeed;
            switch (typeOfUserNeed)
            {
                case TypeOfUserNeed.All: Route = $"api/worker/allworker"; break;
                case TypeOfUserNeed.ForOrder: Route = $"api/worker/allworkerfororder"; break;
                case TypeOfUserNeed.ForRedact: Route = $"api/worker/allworkerforredact"; break;
                case TypeOfUserNeed.KD: Route = $"api/worker/allworkercanditate"; break;
                case TypeOfUserNeed.forpayment: Route = $"api/worker/allworkerforpay"; break;

            }
            this.BaseWindow = baseWindow;
            MakeWorkBetter();

            if (typeOfUserNeed != TypeOfUserNeed.All)
            {
                ForWindow.Visibility = Visibility.Visible;
            }
            else
            {
                ForUserControl.Visibility = Visibility.Visible;
            }
        }
        private async void MakeWorkBetter()
        {
            DataAboutWorker = new DataTable("WorkerInf");
            DataAboutGuid = new List<Tuple<int, Guid>>();
           
            if (typeOfUserNeed == TypeOfUserNeed.ForRedact || typeOfUserNeed == TypeOfUserNeed.ForOrder|| typeOfUserNeed == TypeOfUserNeed.forpayment)
            {
                foreach (string ColumnName in SomeEnums.WorkerTablesRedact)
                {
                    DataAboutWorker.Columns.Add(ColumnName);
                }
                var InformFromserver = await Task.Run(() => MakeDownloadByLink(Route));
                var ListOfWorkers = JsonConvert.DeserializeObject<ListOfWorkingWorker>(InformFromserver.ToString());
                if (ListOfWorkers.success)
                {
                    int number = 1;
                    foreach (var WorkerInf in ListOfWorkers.Workers)
                    {
                        DataRow newClientRow = DataAboutWorker.NewRow();
                        newClientRow[0] = number;
                        newClientRow[1] = WorkerInf.LastName.ToString()?.Trim();
                        newClientRow[2] = WorkerInf.Name.ToString()?.Trim();
                        newClientRow[3] = WorkerInf.Patronymic.ToString()?.Trim();
                        newClientRow[4] = WorkerInf.Female.ToString()?.Trim();
                        newClientRow[5] = WorkerInf.DateRozd?.Trim().Substring(0, 10);
                        newClientRow[6] = WorkerInf.NameOfPost?.Trim();
                        newClientRow[7] = WorkerInf.Salary?.ToString().Trim();
                        DataAboutWorker.Rows.Add(newClientRow);
                        DataAboutGuid.Add(new Tuple<int, Guid>(number, WorkerInf.idUser));
                        number++;
                    }
                }
            }
            else
            {
                foreach (string ColumnName in SomeEnums.WorkerTables)
                {
                    DataAboutWorker.Columns.Add(ColumnName);
                }
                var InformFromserver = await Task.Run(() => MakeDownloadByLink(Route));
                var ListOfWorkers = JsonConvert.DeserializeObject<ListOfAllWorkers>(InformFromserver.ToString());
                if (ListOfWorkers.success)
                {
                    int number = 1;
                    foreach (var WorkerInf in ListOfWorkers.Workers)
                    {
                        DataRow newClientRow = DataAboutWorker.NewRow();
                        newClientRow[0] = number;
                        newClientRow[1] = WorkerInf.LastName.ToString()?.Trim();
                        newClientRow[2] = WorkerInf.Name.ToString()?.Trim();
                        newClientRow[3] = WorkerInf.Patronymic.ToString()?.Trim();
                        newClientRow[4] = WorkerInf.Female.ToString()?.Trim();
                        newClientRow[5] = WorkerInf.DateRozd?.Trim().Substring(0, 10);
                        DataAboutWorker.Rows.Add(newClientRow);
                        DataAboutGuid.Add(new Tuple<int, Guid>(number, WorkerInf.idUser));
                        number++;
                    }
                }
            }
            DataGrid.ItemsSource = DataAboutWorker.DefaultView;
        }

        public object MakeDownloadByLink(string UrlOfDownload)
        {
            return BaseWorkWithServer.CatchErrorWithGet(UrlOfDownload, "GET", nameof(MakeLoading), nameof(MakeDownloadByLink));
        }

        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Создание нового работника");
            baseWindow.MakeOpen(new CreateNewWorker(ref baseWindow));
            baseWindow.ShowDialog();
            MakeWorkBetter();
        }

        private void EditWorker_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    for (int i = 0; i < DataAboutWorker.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(DataAboutWorker.Rows[i][0].ToString()) == numberOfRows)
                        {
                            Guid idWorker = DataAboutGuid.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                            BaseWindow baseWindow = new BaseWindow("Редактирование данных о работниках");
                            baseWindow.MakeOpen(new CreateNewWorker(ref baseWindow, idWorker));
                            baseWindow.ShowDialog();
                            MakeWorkBetter();
                        }
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать работника!", MsgBoxImage: MessageBoxImage.Hand);
            }
        }

        private void SelectWorker_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    for (int i = 0; i < DataAboutWorker.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(DataAboutWorker.Rows[i][0].ToString()) == numberOfRows)
                        {
                            Guid idWorker = DataAboutGuid.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                            SaveSomeData.MakeSomeOperation = true;
                            SaveSomeData.SomeObject = DataAboutWorker.Rows[i];
                            SaveSomeData.idSubs = idWorker;
                            BaseWindow.Close();
                        }
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать работника!", MsgBoxImage: MessageBoxImage.Hand);
            }
        }

        private void ReturnBTN_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow.Close();

        }

        private void PrintData_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    for (int i = 0; i < DataAboutWorker.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(DataAboutWorker.Rows[i][0].ToString()) == numberOfRows)
                        {
                            Guid idWorker = DataAboutGuid.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();

                            if (MakeSomeHelp.MSG("Вы дейсвительно хотите напечать информацию о работнике под номером ", MsgBoxImage: MessageBoxImage.Question, MsgBoxButton: MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                            {
                                var Director = System.IO.Path.Combine(Environment.CurrentDirectory, "Temp");
                                if (!Directory.Exists(Director))
                                {
                                    Directory.CreateDirectory(Director);
                                }

                                string NameOfFile = System.IO.Path.Combine(Director, "woker.dotx");
                                if (!File.Exists(NameOfFile))
                                    File.WriteAllBytes(NameOfFile, RepairFlatWPF.Properties.Resources.WorkerData);

                                var DataAboutWorker = makeotherSelectData(idWorker) as MakeNewWorker;
                                var Databoutcontact = makeloadingListOfContact(idWorker) as ContactModel.ListOfUserContactInf;
                                var DataAboutAdress = MakeDataAboutAdress(DataAboutWorker.idAdress) as ModelAdress.DataAboutAdress;

                                using (var application = new NetOffice.WordApi.Application { Visible = true })
                                {
                                    using (var document = application.Documents.Add(NameOfFile))
                                    {
                                        var Name = document.Bookmarks["Name"].Range;
                                        Name.Text = $" {DataAboutWorker.Name}";
                                        var LastName = document.Bookmarks["LastName"].Range;
                                        LastName.Text = $" {DataAboutWorker.Lastname}";
                                        var Patronymic = document.Bookmarks["Patronymic"].Range;
                                        Patronymic.Text = $" {DataAboutWorker.Patronymic}";
                                        var PasportData = document.Bookmarks["PasportData"].Range;
                                        PasportData.Text = $" {DataAboutWorker.Pasport}";
                                        var DataRozd = document.Bookmarks["DataRozd"].Range;
                                        DataRozd.Text = $" {DataAboutWorker.Birstday?.ToString("dd.MM.yyyy")}";


                                        if (!String.IsNullOrEmpty(DataAboutAdress.description?.Trim()))
                                        {
                                            var Description = document.Bookmarks["Description"].Range;
                                            Description.Text = $"Описание: {DataAboutAdress.description?.Trim()}{Environment.NewLine}";
                                        }

                                        if (!String.IsNullOrEmpty(DataAboutAdress.NumberOfDelen?.Trim()))
                                        {
                                            var NumberOfDelen = document.Bookmarks["NumberOfDelen"].Range;
                                            NumberOfDelen.Text = $"Квартира/офис: {DataAboutAdress.NumberOfDelen?.Trim()}{Environment.NewLine}";
                                        }
                                        if (!String.IsNullOrEmpty(DataAboutAdress.Entrance?.Trim()))
                                        {
                                            var Entrance = document.Bookmarks["Entrance"].Range;
                                            Entrance.Text = $"Подъезд: {DataAboutAdress.Entrance?.Trim()}{Environment.NewLine}";
                                        }
                                        if (!String.IsNullOrEmpty(DataAboutAdress.House?.Trim()))
                                        {
                                            var House = document.Bookmarks["House"].Range;
                                            House.Text = $"Дом: {DataAboutAdress.House?.Trim()}{Environment.NewLine}";
                                        }
                                        if (!String.IsNullOrEmpty(DataAboutAdress.Street?.Trim()))
                                        {
                                            var Street = document.Bookmarks["Street"].Range;
                                            Street.Text = $"Улица: {DataAboutAdress.Street?.Trim()}{Environment.NewLine}";
                                        }
                                        if (!String.IsNullOrEmpty(DataAboutAdress.MicroAreaName?.Trim()))
                                        {
                                            var MicroAreaName = document.Bookmarks["MicroAreaName"].Range;
                                            MicroAreaName.Text = $"Микрорайон: {DataAboutAdress.MicroAreaName?.Trim()}{Environment.NewLine}";
                                        }
                                        if (!String.IsNullOrEmpty(DataAboutAdress.CityName?.Trim()))
                                        {
                                            var CityName = document.Bookmarks["CityName"].Range;
                                            CityName.Text = $"Город: {DataAboutAdress.CityName?.Trim()}{Environment.NewLine}";
                                        }
                                        if (!String.IsNullOrEmpty(DataAboutAdress.AreaName?.Trim()))
                                        {
                                            var AreaName = document.Bookmarks["AreaName"].Range;
                                            AreaName.Text = $"Область: {DataAboutAdress.AreaName?.Trim()}{Environment.NewLine}";

                                        }
                                        if (!String.IsNullOrEmpty(DataAboutAdress.RegionName?.Trim()))
                                        {
                                            var RegionName = document.Bookmarks["RegionName"].Range;
                                            RegionName.Text = $"Страна: {DataAboutAdress.RegionName?.Trim()}{Environment.NewLine}";
                                        }
                                        if (Databoutcontact.listOfContact != null)
                                        {
                                            var TableContact = document.Bookmarks["Contact"].Range.Tables[1];
                                            TableContact.Rows[2].Cells.Delete();
                                            int Number = 1;
                                            foreach (var conact in Databoutcontact.listOfContact)
                                            {
                                                var TableRow = TableContact.Rows.Add();
                                                var rows = TableContact.Rows.Add();

                                                rows.Cells[1].Range.Text = Number.ToString();
                                                rows.Cells[2].Range.Text = conact.ValueTypeOfContact.ToString();
                                                rows.Cells[3].Range.Text = conact.Value.ToString();
                                                rows.Cells[4].Range.Text = conact.Desctription.ToString();
                                                Number++;
                                            }
                                        }
                                    }
                                    application.Activate();

                                }

                            }
                        }
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать работника!", MsgBoxImage: MessageBoxImage.Hand);
            }
        }

        private object makeotherSelectData(Guid idUser)
        {
            var InformationFromServer = MakeDownloadByLink($"api/worker/getData?idWorker={idUser}");
            var dataAbWorker = JsonConvert.DeserializeObject<MakeNewWorker>(InformationFromServer.ToString());
            return dataAbWorker;
        }

        private object makeloadingListOfContact(Guid idUser)
        {

            var InformationFromServer = MakeDownloadByLink($"api/contact/getusercontact?idUser={idUser}");
            ContactModel.ListOfUserContactInf listOfUserContactInf = JsonConvert.DeserializeObject<ContactModel.ListOfUserContactInf>(InformationFromServer.ToString());
            return listOfUserContactInf;
        }

        private object MakeDataAboutAdress(Guid idAdress)
        {
            var InformationFromServer = MakeDownloadByLink($"api/adress/data?idAdress={idAdress}");
            ModelAdress.DataAboutAdress listOfUserAdressInf = JsonConvert.DeserializeObject<ModelAdress.DataAboutAdress>(InformationFromServer.ToString());
            return listOfUserAdressInf;
        }

        private void RetInMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new MenuKadrWork());
        }
    }
}
