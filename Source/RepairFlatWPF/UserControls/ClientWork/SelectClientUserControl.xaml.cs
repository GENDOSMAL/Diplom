using Newtonsoft.Json;
using RepairFlat.Model;
using RepairFlatWPF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static RepairFlat.Model.PersonDesctiption;

namespace RepairFlatWPF.UserControls.ClientWork
{
    /// <summary>
    /// Interaction logic for SelectWorkerUserControl.xaml
    /// </summary>
    public partial class SelectClientUserControl : UserControl
    {
        DataTable DataAboutClient;
        List<Tuple<int, Guid?>> DataOfClient = new List<Tuple<int, Guid?>>();
        PersonDesctiption.ListOfClient listOfUserclientInf;
        SomeEnums.TypeOfConrols typeOfConrols;
        BaseWindow window;
        public SelectClientUserControl(SomeEnums.TypeOfConrols typeOfConrols, ref BaseWindow baseWindow)
        {
            InitializeComponent();
            window = baseWindow;
            this.typeOfConrols = typeOfConrols;
            makeloadingListOfUser();
            if (this.typeOfConrols == SomeEnums.TypeOfConrols.UserControl)
            {
                ForUserControl.Visibility = Visibility.Visible;
            }
            else
            {
                ForWindow.Visibility = Visibility.Visible;
            }
        }

        public async void makeloadingListOfUser()
        {
            DataAboutClient = new DataTable("Client");
            foreach (string NameOfColumn in SomeEnums.ClientTables)
            {
                DataAboutClient.Columns.Add(NameOfColumn);
            }
            DataGrid.ItemsSource = DataAboutClient.DefaultView;
            var InformFromserver = await Task.Run(() => MakeDownloadByLink($"api/user/selectallClient"));
            listOfUserclientInf = JsonConvert.DeserializeObject<PersonDesctiption.ListOfClient>(InformFromserver.ToString());
            if (listOfUserclientInf.success)
            {
                int number = 1;
                foreach (var client in listOfUserclientInf.listOfClient)
                {
                    DataRow newClientRow = DataAboutClient.NewRow();
                    newClientRow[0] = number;
                    newClientRow[1] = client.Name?.Trim();
                    newClientRow[2] = client.Lastname?.Trim();
                    newClientRow[3] = client.Patronymic?.Trim();
                    newClientRow[4] = SomeEnums.FemaleType[client.Female ?? default];
                    newClientRow[5] = client.Description?.Trim();
                    DataAboutClient.Rows.Add(newClientRow);
                    DataOfClient.Add(new Tuple<int, Guid?>(number, client.idUser));
                    number++;
                }
            }
        }

        public object MakeDownloadByLink(string UrlOfDownload)
        {
            return BaseWorkWithServer.CatchErrorWithGet(UrlOfDownload, "GET", nameof(MakeLoading), nameof(MakeDownloadByLink));
        }

        private void SelectClient_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    var idSelectClient = DataOfClient.Where(e1 => e1.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                    var que = listOfUserclientInf.listOfClient.Where(ee => ee.idUser == idSelectClient);
                    DescriptionOfUser descriptionOfUser = que.Select(e1 => new DescriptionOfUser
                    {
                        idUser = e1.idUser,
                        Birstday = e1.Birstday,
                        Female = e1.Female,
                        Lastname = e1.Lastname,
                        Name = e1.Name,
                        Pasport = e1.Pasport,
                        Patronymic = e1.Patronymic

                    }).First();
                    SaveSomeData.MakeSomeOperation = true;
                    SaveSomeData.SomeObject = descriptionOfUser;
                    window.Close();
                }
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать клиента из списка представленных", MsgBoxImage: MessageBoxImage.Error);
            }
        }

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    Guid idWorker = DataOfClient.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First() ?? default;
                    BaseWindow baseWindow = new BaseWindow("Редактирование данных о клиенте");
                    baseWindow.MakeOpen(new AddUserControl(idWorker, ref baseWindow, true));
                    baseWindow.ShowDialog();
                    makeloadingListOfUser();

                }
            }

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Добавление данных о клиенте");
            baseWindow.MakeOpen(new AddUserControl(Guid.NewGuid(), ref baseWindow));
            baseWindow.ShowDialog();
            makeloadingListOfUser();
        }

        private void ReturnBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.MSG("Не реализовано");
        }
    }
}
