using Newtonsoft.Json;
using RepairFlat.Model;
using RepairFlatWPF.Controller;
using RepairFlatWPF.Properties;
using RepairFlatWPF.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RepairFlatWPF
{
    /// <summary>
    /// Выполнение некоторых повторяющихся событий
    /// </summary>
    public static class MakeSomeHelp
    {
        public static MessageBoxResult MSG(string Message, MessageBoxButton MsgBoxButton = MessageBoxButton.OK, MessageBoxImage MsgBoxImage = MessageBoxImage.None)
        {
            return MessageBox.Show(Message, Settings.Default.DefaultHeaderOfMessageBox, MsgBoxButton, MsgBoxImage);
        }

        public static object MakeDownloadByLink(string UrlOfDownload)
        {
            return BaseWorkWithServer.CatchErrorWithGet(UrlOfDownload, "GET", nameof(MakeLoading), nameof(MakeDownloadByLink));
        }        
        public static bool MakePingToServer(string ServerAdress)
        {
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = null;
                pingReply = ping.Send("ya.ru");
                if (pingReply.Status != IPStatus.TimedOut)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static WorkWithDB.Tableses ReturnJsonOfTable()
        {
            var Resourses = Properties.Resources.ResourceManager.GetObject("DescriprionOfDB") as byte[];
            string json = Encoding.UTF8.GetString(Resourses);
            var ListOFTablesDescription = JsonConvert.DeserializeObject<WorkWithDB.Tableses>(json);
            return ListOFTablesDescription;
        }

        public static void ChengeGridInMainWindow(UserControl controls)
        {
            ((MainWindow)Application.Current.MainWindow).MainGrid.Children.Clear();
            if (controls != null)
                ((MainWindow)Application.Current.MainWindow).MainGrid.Children.Add(controls);
        }


        public static void MakeShowLogining()
        {
            ((MainWindow)Application.Current.MainWindow).ForLogin.Visibility = Visibility.Visible;
            ((MainWindow)Application.Current.MainWindow).ForLogin.Children.Clear();
            ((MainWindow)Application.Current.MainWindow).ForLogin.Children.Add(new LoginUserControl());
            ((MainWindow)Application.Current.MainWindow).ForLogin.Background = (System.Windows.Media.Brush)Application.Current.Resources["BackLogAndLoadColor"];
        }

        public static void MakeLoading(bool forWindow = true)
        {
            ((MainWindow)Application.Current.MainWindow).ForLogin.Children.Clear();
            ((MainWindow)Application.Current.MainWindow).ForLogin.Children.Add(new MakeLoading(forWindow));
            if (!forWindow)
                ((MainWindow)Application.Current.MainWindow).ForLogin.Visibility = Visibility.Visible;
            if (forWindow)
                ((MainWindow)Application.Current.MainWindow).ForLogin.Background = (System.Windows.Media.Brush)Application.Current.Resources["GradientForLoading"];
        }

        public static void ShowMainGrid()
        {
            ((MainWindow)Application.Current.MainWindow).ForLogin.Visibility = Visibility.Collapsed;
            ((MainWindow)Application.Current.MainWindow).ForLogin.Children.Clear();
        }
        public static void ChengeGridBackGroundStyle(string NameOfStyle)
        {
            ((MainWindow)Application.Current.MainWindow).MainGrid.Background = (System.Windows.Media.Brush)Application.Current.Resources[NameOfStyle];
        }
        public static void GridChengeGridBackGroundStyle(string NameOfStyle)
        {
            ((MainWindow)Application.Current.MainWindow).ForLogin.Background = (System.Windows.Media.Brush)Application.Current.Resources[NameOfStyle];
        }

        public static void DataGridMakeWork(UserControl controls)
        {
            ((MainWindow)Application.Current.MainWindow).GridForContent.Children.Clear();
            if (controls != null)
                ((MainWindow)Application.Current.MainWindow).GridForContent.Children.Add(controls);
        }

        public static void ShowOrCloseMenu(bool Open)
        {
            if (Open)
                ((MainWindow)Application.Current.MainWindow).GridMenu.Width = 300;
            else
                ((MainWindow)Application.Current.MainWindow).GridMenu.Width = 0;
        }

        public static int SelectMaxValueinColumn(ref DataTable dataTable, string NameOfColumn)
        {
            int tempVariable = 0;
            int highestNumber = dataTable.AsEnumerable()
                        .Where(x => int.TryParse(x.Field<string>(NameOfColumn), out tempVariable))
                        .Max(m => int.Parse(m.Field<string>(NameOfColumn)));
            return highestNumber + 1;
        }

        public static object SelectedRowsInDataGrid(ref DataGrid DataGrid, int rowsNumber, int column = 0)
        {
            var ci = new DataGridCellInfo(DataGrid.Items[rowsNumber], DataGrid.Columns[column]);
            var content = ci.Column.GetCellContent(ci.Item) as TextBlock;
            if (content.Text != "")
            {
                if (content != null)
                {
                    return content.Text;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async static void UpdloadDataToServer(string Url, string Json)
        {
            var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(Url, "POST", Json, nameof(BaseWorkWithServer), nameof(UpdloadDataToServer)));
            var deserializedProduct = JsonConvert.DeserializeObject<BaseResult>(task.ToString());
            if (!deserializedProduct.success)
            {
                MakeSomeHelp.MSG($"Произошла ошикбка при работе {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
            }
            else
            {
                MakeSomeHelp.MSG("Операции над данными были произведены!", MsgBoxImage: MessageBoxImage.Information);
            }
        }

        public static List<Tuple<int, Guid>> ListofId;



        public static DataTable DataTableFromDataBase(SomeEnums.TypeOfSubs typeOfSubs)
        {
            DataTable DataAboutSomeSubInf = new DataTable();
            ListofId = new List<Tuple<int, Guid>>();
            if (typeOfSubs == SomeEnums.TypeOfSubs.Materials)
            {//Материалы
                foreach (string NameOfColumn in SomeEnums.MaterialSubs)
                {
                    DataAboutSomeSubInf.Columns.Add(NameOfColumn);
                }
                string query = "select * from OurMaterials";
                var datafr = MakeWorkWirthDataBase.MakeSomeQueryWork(query, WorkWithTables: true);
                if (datafr != null)
                {
                    DataTable dataTable = new DataTable();
                    dataTable = datafr as DataTable;

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        DataRow rowsForInsert = DataAboutSomeSubInf.NewRow();
                        rowsForInsert[0] = i + 1;
                        rowsForInsert[1] = dataTable.Rows[i][1].ToString()?.Trim();
                        rowsForInsert[2] = dataTable.Rows[i][2].ToString()?.Trim();
                        rowsForInsert[3] = dataTable.Rows[i][4].ToString()?.Trim();
                        rowsForInsert[4] = dataTable.Rows[i][3].ToString()?.Trim();
                        rowsForInsert[5] = dataTable.Rows[i][5].ToString()?.Trim();
                        DataAboutSomeSubInf.Rows.Add(rowsForInsert);
                        ListofId.Add(new Tuple<int, Guid>(i + 1, Guid.Parse(dataTable.Rows[i][0].ToString())));
                    }
                }

            }
            else if (typeOfSubs == SomeEnums.TypeOfSubs.Post)
            {//Дожности
                foreach (string NameOfColumn in SomeEnums.PostSubs)
                {
                    DataAboutSomeSubInf.Columns.Add(NameOfColumn);
                }

                string query = "select * from PostData";
                var datafr = MakeWorkWirthDataBase.MakeSomeQueryWork(query, WorkWithTables: true);
                if (datafr != null)
                {
                    DataTable dataTable = new DataTable();
                    dataTable = datafr as DataTable;

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        DataRow rowsForInsert = DataAboutSomeSubInf.NewRow();
                        string MakeWork = "";
                        if (!string.IsNullOrEmpty(dataTable.Rows[i][3].ToString()))
                        {
                            MakeWork = Convert.ToInt32(dataTable.Rows[i][3].ToString()) == 1 ? "Да" : "Нет";
                        }
                        else
                        {
                            MakeWork = "Нет данных";
                        }
                        rowsForInsert[0] = i + 1;
                        rowsForInsert[1] = dataTable.Rows[i][1].ToString()?.Trim();
                        rowsForInsert[2] = dataTable.Rows[i][2].ToString()?.Trim();
                        rowsForInsert[3] = MakeWork;
                        DataAboutSomeSubInf.Rows.Add(rowsForInsert);
                        ListofId.Add(new Tuple<int, Guid>(i + 1, Guid.Parse(dataTable.Rows[i][0].ToString())));
                    }
                }
            }
            else if (typeOfSubs == SomeEnums.TypeOfSubs.Servises)
            {//Услуги
                foreach (string NameOfColumn in SomeEnums.ServisesSubs)
                {
                    DataAboutSomeSubInf.Columns.Add(NameOfColumn);
                }

                string query = "select * from OurServices";
                var datafr = MakeWorkWirthDataBase.MakeSomeQueryWork(query, WorkWithTables: true);
                if (datafr != null)
                {
                    DataTable dataTable = new DataTable();
                    dataTable = datafr as DataTable;

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        DataRow rowsForInsert = DataAboutSomeSubInf.NewRow();
                        rowsForInsert[0] = i + 1;
                        rowsForInsert[1] = dataTable.Rows[i][1].ToString()?.Trim();
                        rowsForInsert[2] = dataTable.Rows[i][2].ToString()?.Trim();
                        rowsForInsert[3] = dataTable.Rows[i][3].ToString()?.Trim();
                        rowsForInsert[4] = dataTable.Rows[i][4].ToString()?.Trim();
                        DataAboutSomeSubInf.Rows.Add(rowsForInsert);
                        ListofId.Add(new Tuple<int, Guid>(i + 1, Guid.Parse(dataTable.Rows[i][0].ToString())));
                    }
                }
            }
            else if (typeOfSubs == SomeEnums.TypeOfSubs.Premises)
            {//Помещения
                foreach (string NameOfColumn in SomeEnums.PremisesSubs)
                {
                    DataAboutSomeSubInf.Columns.Add(NameOfColumn);
                }

                string query = "select * from PremisesType";
                var datafr = MakeWorkWirthDataBase.MakeSomeQueryWork(query, WorkWithTables: true);
                if (datafr != null)
                {
                    DataTable dataTable = new DataTable();
                    dataTable = datafr as DataTable;

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        DataRow rowsForInsert = DataAboutSomeSubInf.NewRow();
                        rowsForInsert[0] = i + 1;
                        rowsForInsert[1] = dataTable.Rows[i][1].ToString()?.Trim();
                        rowsForInsert[2] = dataTable.Rows[i][2].ToString()?.Trim();
                        DataAboutSomeSubInf.Rows.Add(rowsForInsert);
                        ListofId.Add(new Tuple<int, Guid>(i + 1, Guid.Parse(dataTable.Rows[i][0].ToString())));
                    }
                }
            }
            else if (typeOfSubs == SomeEnums.TypeOfSubs.Contact)
            {//Типы контактов
                foreach (string NameOfColumn in SomeEnums.ContactSubs)
                {
                    DataAboutSomeSubInf.Columns.Add(NameOfColumn);
                }

                string query = "select * from ContactType";
                var datafr = MakeWorkWirthDataBase.MakeSomeQueryWork(query, WorkWithTables: true);
                if (datafr != null)
                {
                    DataTable dataTable = new DataTable();
                    dataTable = datafr as DataTable;

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        DataRow rowsForInsert = DataAboutSomeSubInf.NewRow();
                        rowsForInsert[0] = i + 1;
                        rowsForInsert[1] = dataTable.Rows[i][1].ToString()?.Trim();
                        rowsForInsert[2] = dataTable.Rows[i][2].ToString()?.Trim();
                        rowsForInsert[3] = dataTable.Rows[i][3].ToString()?.Trim();
                        DataAboutSomeSubInf.Rows.Add(rowsForInsert);
                        ListofId.Add(new Tuple<int, Guid>(i + 1, Guid.Parse(dataTable.Rows[i][0].ToString())));
                    }
                }
            }
            return DataAboutSomeSubInf;
        }
    }
}
