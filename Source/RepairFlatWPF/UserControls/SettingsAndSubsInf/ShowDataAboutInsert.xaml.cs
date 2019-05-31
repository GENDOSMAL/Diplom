using Microsoft.Win32;
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
