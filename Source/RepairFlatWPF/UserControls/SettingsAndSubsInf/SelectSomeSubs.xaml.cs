using RepairFlatWPF.Controller;
using RepairFlatWPF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace RepairFlatWPF.UserControls.SettingsAndSubsInf
{
    /// <summary>
    /// Interaction logic for SelectSomeSubs.xaml
    /// </summary>
    public partial class SelectSomeSubs : UserControl
    {
        #region Переменные 

        SomeEnums.TypeOfSubs typeOfSubs;
        BaseWindow BaseWindow;
        DataTable DataAboutSomeSubInf;
        List<Tuple<int, Guid>> ListofId;
        #endregion

        #region Конструкторы

        public SelectSomeSubs(ref BaseWindow newWindow, SomeEnums.TypeOfSubs typeOfSubs)
        {
            InitializeComponent();
            this.typeOfSubs = typeOfSubs;
            this.BaseWindow = newWindow;
            MakeDataFromDB();

        }
        #endregion

        #region Для подстановки данных
        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow.Close();
        }

        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    for (int i = 0; i < DataAboutSomeSubInf.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(DataAboutSomeSubInf.Rows[i][0].ToString()) == numberOfRows)
                        {
                            Guid idSubs = ListofId.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                            
                            SaveSomeData.MakeSomeOperation = true;
                            SaveSomeData.SomeObject = DataAboutSomeSubInf.Rows[i];
                            SaveSomeData.idSubs = idSubs;
                            BaseWindow.Close();
                        }
                    }                    
                }
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать данные для работы!", MsgBoxImage: MessageBoxImage.Hand);
            }
        }
        #endregion
        #region Дополнительные данные
        private void MakePreparateData()
        {
            DataAboutSomeSubInf = new DataTable();
            ListofId = new List<Tuple<int, Guid>>();
        }
        private void MakeDataFromDB()
        {
            MakePreparateData();
            DataAboutSomeSubInf = MakeSomeHelp.DataTableFromDataBase(typeOfSubs);
            ListofId = MakeSomeHelp.ListofId;
            DataGrid.ItemsSource = DataAboutSomeSubInf.DefaultView;
        }
        #endregion
    }
}
