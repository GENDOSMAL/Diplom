using Microsoft.Win32;
using Newtonsoft.Json;
using RepairFlat.Model;
using RepairFlatWPF.Model;
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
using static RepairFlat.Model.MakeSubs;

namespace RepairFlatWPF.UserControls.SettingsAndSubsInf
{
    /// <summary>
    /// Interaction logic for ShowDataAboutInsert.xaml
    /// </summary>
    public partial class ShowDataAboutInsert : UserControl
    {
        List<int> ListOfSelectSheet = new List<int>();
        DataSet AllSheet;
        public ShowDataAboutInsert(List<int> ListOfSelectSheet)
        {
            InitializeComponent();
            this.ListOfSelectSheet = ListOfSelectSheet;
            Element.IsEnabled = ListOfSelectSheet.Contains(1) ? true : false;
            Material.IsEnabled = ListOfSelectSheet.Contains(2) ? true : false;
            Servises.IsEnabled = ListOfSelectSheet.Contains(3) ? true : false;
            Post.IsEnabled = ListOfSelectSheet.Contains(4) ? true : false;
            if (!Element.IsEnabled)
            {
                if (!Material.IsEnabled)
                {
                    if (!Servises.IsEnabled)
                    {
                        ShowNeeded(ForPost);
                        GridCursor.SetValue(Grid.ColumnProperty, 3);
                    }
                    else
                    {
                        ShowNeeded(ForServises);
                        GridCursor.SetValue(Grid.ColumnProperty, 2);
                    }
                }
                else
                {
                    ShowNeeded( ForMaterial);
                    GridCursor.SetValue(Grid.ColumnProperty, 1);
                }
            }
            Select();
            if (ListOfSelectSheet.Contains(1))
            {
                DataAboutElement.ItemsSource = AllSheet.Tables["1"].DefaultView;
            }
            if (ListOfSelectSheet.Contains(2))
            {
                DataAboutMaterial.ItemsSource = AllSheet.Tables["2"].DefaultView;
            }
            if (ListOfSelectSheet.Contains(3))
            {
                DataAboutServises.ItemsSource = AllSheet.Tables["3"].DefaultView;
            }
            if (ListOfSelectSheet.Contains(4))
            {
                DataAboutPost.ItemsSource = AllSheet.Tables["4"].DefaultView;
            }
        }

        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.WorkWithSubInfromation());
        }

        private void SaveDataBTN_Click(object sender, RoutedEventArgs e)
        {
            if (MakeSomeHelp.MSG("Вы согласны с данными таблицах приведенных на экране?", MsgBoxImage: MessageBoxImage.Question, MsgBoxButton: MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                if (ListOfSelectSheet.Contains(1))
                {
                    DataTable Data = AllSheet.Tables["1"];
                    MakeUpdOrInsPremises insPremises = new MakeUpdOrInsPremises();
                    List<ListOfPremises> ListOfPremise = new List<ListOfPremises>(); 
                    for (int i=0;i< Data.Rows.Count; i++)
                    {
                        ListOfPremises premisesUpd = new ListOfPremises
                        {
                            Description = Data.Rows[i][2].ToString(),
                            idPremises = Guid.Parse(Data.Rows[i][0].ToString()),
                            Name = Data.Rows[i][1].ToString()
                        };
                        ListOfPremise.Add(premisesUpd);
                    }
                    insPremises.idUser = SaveSomeData.IdUser?? default(Guid);
                    insPremises.ListOfPremises = ListOfPremise;
                    insPremises.DateOfMake = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                    MakeSendDataToserver("api/substring/premises/update", insPremises);
                }
                if (ListOfSelectSheet.Contains(2))
                {
                    DataTable Data = AllSheet.Tables["2"];
                    MakeUpdOrInsMaterials InsMaterial = new MakeUpdOrInsMaterials();
                    List<ListOfMaterials> ListOfMaterials = new List<ListOfMaterials>();
                    for (int i = 0; i < Data.Rows.Count; i++)
                    {
                        ListOfMaterials premisesUpd = new ListOfMaterials
                        {
                            idMaterials = Guid.Parse(Data.Rows[i][0].ToString()),
                            NameOfMaterial = Data.Rows[i][1].ToString(),
                            UnitOfMeasue = Data.Rows[i][2].ToString(),
                            TypeOfMaterial = Data.Rows[i][3].ToString(),
                            Cost =Convert.ToDecimal(Data.Rows[i][4].ToString()),                          
                            Description = Data.Rows[i][5].ToString(),
                        };
                        ListOfMaterials.Add(premisesUpd);
                    }
                    InsMaterial.idUser = SaveSomeData.IdUser ?? default(Guid);
                    InsMaterial.ListOfMaterials = ListOfMaterials;
                    InsMaterial.DateOfMake = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                    MakeSendDataToserver("api/substring/material/update", InsMaterial);
                }
                if (ListOfSelectSheet.Contains(3))
                {
                    DataTable Data = AllSheet.Tables["3"];
                    MakeUpdOrInsServises InsServises = new MakeUpdOrInsServises();
                    List<ListOfServises> ListOfServises = new List<ListOfServises>();
                    for (int i = 0; i < Data.Rows.Count; i++)
                    {
                        ListOfServises ServisesUpdate = new ListOfServises
                        {
                            idServises = Guid.Parse(Data.Rows[i][0].ToString()),
                            Nomination = Data.Rows[i][1].ToString(),
                            TypeOfServises= Data.Rows[i][2].ToString(),
                            Cost = Convert.ToDecimal(Data.Rows[i][3].ToString()),
                            Description = Data.Rows[i][4].ToString(),
                        };
                        ListOfServises.Add(ServisesUpdate);
                    }
                    InsServises.idUser = SaveSomeData.IdUser ?? default(Guid);
                    InsServises.ListOfServises = ListOfServises;
                    InsServises.DateOfMake = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                    MakeSendDataToserver("api/substring/servises/update", InsServises);
                }
                if (ListOfSelectSheet.Contains(4))
                {
                    DataTable Data = AllSheet.Tables["4"];
                    MakeUpdOrInsPost InsPost = new MakeUpdOrInsPost();
                    List<ListOfPost> ListOfPost = new List<ListOfPost>();
                    for (int i = 0; i < Data.Rows.Count; i++)
                    {

                        ListOfPost ServisesUpdate = new ListOfPost
                        {

                            idPost = Guid.Parse(Data.Rows[i][0].ToString()),
                            NameOfPost = Data.Rows[i][1].ToString(),
                            BaseWage = Convert.ToDecimal(Data.Rows[i][2].ToString()),
                            MakeWork=Convert.ToBoolean(Data.Rows[i][3].ToString())
                        };
                        ListOfPost.Add(ServisesUpdate);
                    }
                    InsPost.idUser = SaveSomeData.IdUser ?? default(Guid);
                    InsPost.listOfPostUpdate = ListOfPost;
                    InsPost.ListOfPostInsert = ListOfPost;
                    InsPost.DateOfMake = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                    MakeSendDataToserver("api/substring/post/update", InsPost);
                }
                MakeSomeHelp.MakeLoading(false);
                MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.WorkWithSubInfromation());
            }
            else
            {
                MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.WorkWithSubInfromation());
            }
        }

        private void SelectTabsClick(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            switch (index)
            {
                case 1:
                    GridCursor.SetValue(Grid.ColumnProperty, index-1);
                    ShowNeeded(ForElement);
                    break;
                case 2:
                    GridCursor.SetValue(Grid.ColumnProperty, index-1);
                    ShowNeeded(ForMaterial);
                    break;
                case 3:
                    GridCursor.SetValue(Grid.ColumnProperty, index-1);
                    ShowNeeded(ForServises);
                    break;
                case 4:
                    GridCursor.SetValue(Grid.ColumnProperty, index-1);
                    ShowNeeded(ForPost);
                    break;
            }
        }


        private async void MakeSendDataToserver(string urlSend,object json)
        {
            string Json = JsonConvert.SerializeObject(json);
            SaveDataBTN.Content = "Ожидайте...";
            RetutnBTN.Content = "Ожидайте...";
            SaveDataBTN.IsEnabled = false;
            RetutnBTN.IsEnabled = false;

            var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(MakeSendDataToserver)));
            var deserializedProduct = JsonConvert.DeserializeObject<BaseResult>(task.ToString());

            if (!deserializedProduct.success)
            {
                MakeSomeHelp.MSG($"Произошла ошибка при работе {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
            }

        }

        private void ShowNeeded( Grid grid)
        {
            ForElement.Visibility = Visibility.Collapsed;
            ForMaterial.Visibility = Visibility.Collapsed;
            ForServises.Visibility = Visibility.Collapsed;
            ForPost.Visibility = Visibility.Collapsed;
            grid.Visibility = Visibility.Visible;
        }

        private void Select()
        {
            AllSheet = new DataSet();
            DataTable dataTable = new DataTable("FromExcel");

            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xlsm";
            openfile.Filter = "(.xlsm)|*.xlsm";
            string pathToExcel = "";
            if (openfile.ShowDialog() == true)
            {
                pathToExcel = openfile.FileName;
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

                Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(pathToExcel, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                for (int i = 1; i <= excelBook.Worksheets.Count; i++)
                {
                    if (ListOfSelectSheet.Contains(i))
                    {
                        Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(i); ;
                        DataTable dataTable1 = new DataTable(i.ToString());
                        AllSheet.Tables.Add(dataTable1);
                        Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;
                        string strCellData = "";
                        double douCellData;
                        int rowCnt = 0;
                        int colCnt = 0;
                        for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                        {
                            string strColumn = "";
                            strColumn = (string)(excelRange.Cells[1, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                            dataTable1.Columns.Add(strColumn, typeof(string));
                        }
                        for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
                        {
                            string strData = "";
                            for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                            {
                                try
                                {
                                    strCellData = (string)(excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                                    strData += strCellData + "|";
                                }
                                catch
                                {
                                    douCellData = (excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                                    strData += douCellData.ToString() + "|";
                                }
                            }
                            strData = strData.Remove(strData.Length - 1, 1);
                            dataTable1.Rows.Add(strData.Split('|'));
                        }
                    }

                }
                excelBook.Close();
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо указать файл для дальнейшей работы");
            }
        }
    }
}
