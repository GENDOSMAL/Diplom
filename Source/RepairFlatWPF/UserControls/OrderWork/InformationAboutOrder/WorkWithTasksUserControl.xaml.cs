using Newtonsoft.Json;
using RepairFlatWPF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RepairFlatWPF.UserControls.OrderWork
{
    public partial class WorkWithTasksUserControl : UserControl
    {
        #region Переменные
        Guid idOrder;
        DataTable DataAboutTask;
        List<Tuple<int, Guid>> ListOfId;
        #endregion

        #region Конструктор
        public WorkWithTasksUserControl(Guid idOrder, object DataAboutTasks = null)
        {
            InitializeComponent();
            this.idOrder = idOrder;
            MakePreparateDataTable();
        }
        #endregion

        #region Обработка событий 
        private void AddTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BaseWindow redactwindow = new BaseWindow("Создание нового задания");
            redactwindow.MakeOpen(new AddInfromationUserControl.AddNewTaskInOrderUserControl(idOrder, ref redactwindow));
            redactwindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {
                MakePreparateDataTable();
            }
        }

        private void EditTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    Guid idTask = ListOfId.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                    BaseWindow redactwindow = new BaseWindow("Редактирование сущестующего задания");
                    redactwindow.MakeOpen(new AddInfromationUserControl.AddNewTaskInOrderUserControl(idOrder, ref redactwindow, idTask));
                    redactwindow.ShowDialog();
                    if (SaveSomeData.MakeSomeOperation)
                    {
                        SaveSomeData.MakeSomeOperation = false;
                        MakePreparateDataTable();
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Не выбрано задание для редактирование!", MsgBoxImage: MessageBoxImage.Hand);
            }
        }

        private void DeleteTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    Guid idTask = ListOfId.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                    //TODO Удаление заданий
                }
            }
            else
            {
                MakeSomeHelp.MSG("Не выбрано задание для удаления!", MsgBoxImage: MessageBoxImage.Hand);
            }
        }
        #endregion

        #region Прочие обработки

        private void MakePreparateDataTable()
        {
            DataAboutTask = new DataTable("Task");
            ListOfId = new List<Tuple<int, Guid>>();
            foreach(string NameOfColumn in SomeEnums.TaskTable)
            {
                DataAboutTask.Columns.Add(NameOfColumn);
            }
            DataGrid.ItemsSource = DataAboutTask.DefaultView;
        }

        private async void MakeDataAboutTaskFromServer()
        {
            var InformFromserver = await Task.Run(() => MakeSomeHelp.MakeDownloadByLink($"api/order/get/task?idOrder={idOrder}"));
            var ListOfPayment = JsonConvert.DeserializeObject<Model.OrderDesc.DataAboutTaskInOrder>(InformFromserver.ToString());
            MakeDataTableData(ListOfPayment);

        }
        private void MakeDataTableData(Model.OrderDesc.DataAboutTaskInOrder taskInOrderdataAbout)
        {
            if (taskInOrderdataAbout.success)
            {
                int number = 1;
                foreach(var task in taskInOrderdataAbout.InfTask)
                {
                    DataRow dataRow = DataAboutTask.NewRow();
                    dataRow[0] = number;
                    dataRow[1] = task.Summa.Value.ToString();
                    dataRow[2] = task.Description?.Trim();
                    dataRow[3] = task.DateStart.Value.ToString("dd.MM.yyyy");
                    dataRow[4] = task.DateEnd.Value.ToString("dd.MM.yyyy");
                    DataAboutTask.Rows.Add(dataRow);
                    ListOfId.Add(new Tuple<int, Guid>(number, task.idTask));
                    number++;
                }
            }
        }
        #endregion

    }
}
