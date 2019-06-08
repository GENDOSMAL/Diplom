using RepairFlatWPF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static RepairFlatWPF.Model.OrderDesc;

namespace RepairFlatWPF.UserControls.OrderWork.AddInfromationUserControl
{
    /// <summary>
    /// Interaction logic for AddNewTaskInOrderUserControl.xaml
    /// </summary>
    public partial class AddNewTaskInOrderUserControl : UserControl
    {
        #region Переменные

        Guid idOrder;
        Guid idTask;
        bool NewTask = true;
        BaseWindow Window;

        DataTable TableOfMaterials, TableOfServises, TableOfWorker;
        int MaxServis = 1, MaxMaterial = 1, MaxWorker = 1;
        List<Tuple<int, Guid>> ListOfMateriaslId, ListOfServisesId, ListOfWorkerId;
        List<Guid> ListOfDeleteServ, ListOfDeleteMat, ListOfDeleteWorker;

        #endregion

        #region Конструктор
        public AddNewTaskInOrderUserControl(ref BaseWindow baseWindow, Guid idOrder, Guid idTask = new Guid())
        {
            InitializeComponent();
            this.idOrder = idOrder;
            ShowNeededPage(ForMainData);
            Window = baseWindow;

            if (idTask != new Guid())
            {
                this.idTask = idTask;
                NewTask = false;
            }
            else
            {
                MakePreparationTableColumnName();
                idTask = Guid.NewGuid();
            }

        }
        #endregion

        #region Обработки основной части формы
        private void SelectTabsClick(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            switch (index)
            {
                case 0:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowNeededPage(ForMainData);
                    break;
                case 1:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowNeededPage(ForServises);
                    break;
                case 2:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowNeededPage(ForMaterials);
                    break;
                case 3:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowNeededPage(ForWorker);
                    break;
            }
        }


        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            if (MakeSomeHelp.MSG("Вы действительно хотите выйти из создания задания?", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                Window.Close();
            }

        }


        private void ExtionButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewTask)
            {//Создание задания

            }
            else
            {//Редактирование задания

            }
        }

        private void ShowPremises_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Обработки при работе с услугами
        private void AddServises_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Добавление данных об услугах");
            baseWindow.MakeOpen(new AddServisesInOrder(ref baseWindow));
            baseWindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {
                SaveSomeData.MakeSomeOperation = false;
                var dataAbout = SaveSomeData.SomeObject as TaskServises;
                SaveSomeData.SomeObject = null;
                int numberOfRows = 0;
                if (ListOfServisesId.Any())
                {
                    numberOfRows = ListOfServisesId.Where(e2 => e2.Item2 == dataAbout.idServis).Select(e1 => e1.Item1).First();
                }
                decimal cost = dataAbout.cost ?? default;
                double count = dataAbout.count ?? default;
                decimal summa = cost * Convert.ToDecimal(count);
                if (numberOfRows != 0)
                {//Обновление
                    for (int i = 0; i < TableOfServises.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(TableOfServises.Rows[i][0].ToString()) == numberOfRows)
                        {
                            double countTek = Convert.ToDouble(TableOfServises.Rows[i][2].ToString());
                            TableOfServises.Rows[i][1] = dataAbout.NameOfServises;
                            TableOfServises.Rows[i][2] = dataAbout.count + countTek;
                            TableOfServises.Rows[i][3] = dataAbout.cost;
                            TableOfServises.Rows[i][4] = cost * Convert.ToDecimal((dataAbout.count + countTek));
                        }
                    }
                }
                else
                {//Добавление
                    DataRow dataRow = TableOfServises.NewRow();
                    dataRow[0] = MaxServis;
                    dataRow[1] = dataAbout.NameOfServises;
                    dataRow[2] = dataAbout.count;
                    dataRow[3] = dataAbout.cost;
                    dataRow[4] = summa;
                    TableOfServises.Rows.Add(dataRow);
                    ListOfServisesId.Add(new Tuple<int, Guid>(MaxServis, dataAbout.idServis));
                    MaxServis++;
                }
            }
        }

        private void EditServises_Click(object sender, RoutedEventArgs e)
        {
            int index = DataAboutServises.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataAboutServises, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    Guid idServis = ListOfServisesId.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                    TaskServises InfAbServ = null;
                    for (int i = 0; i < TableOfServises.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(TableOfServises.Rows[i][0].ToString()) == numberOfRows)
                        {
                            InfAbServ = new TaskServises
                            {
                                idServis = idServis,
                                cost = Convert.ToDecimal(TableOfServises.Rows[i][3].ToString()),
                                count = Convert.ToInt32(TableOfServises.Rows[i][2].ToString()),
                                NameOfServises = TableOfServises.Rows[i][1].ToString()
                            };
                        }
                    }
                    BaseWindow baseWindow = new BaseWindow("Редактирование данных о предоставляемых услугах");
                    baseWindow.MakeOpen(new AddServisesInOrder(ref baseWindow, InfAbServ));
                    baseWindow.ShowDialog();
                    if (SaveSomeData.MakeSomeOperation)
                    {
                        SaveSomeData.MakeSomeOperation = false;
                        var updatedData = SaveSomeData.SomeObject as TaskServises;
                        SaveSomeData.SomeObject = null;
                        decimal cost = updatedData.cost ?? default;
                        double count = updatedData.count ?? default;
                        decimal summa = cost * Convert.ToDecimal(count);
                        for (int i = 0; i < TableOfServises.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(TableOfServises.Rows[i][0].ToString()) == numberOfRows)
                            {
                                TableOfServises.Rows[i][1] = updatedData.NameOfServises;
                                TableOfServises.Rows[i][2] = updatedData.count;
                                TableOfServises.Rows[i][3] = updatedData.cost;
                                TableOfServises.Rows[i][4] = summa;
                            }
                        }
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Не выбрана услуга для редактирование!", MsgBoxImage: MessageBoxImage.Hand);
            }
        }

        private void DeleteSerises_Click(object sender, RoutedEventArgs e)
        {
            int index = DataAboutServises.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataAboutServises, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    Guid idServis = ListOfServisesId.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                    if (!NewTask)
                    {
                        ListOfDeleteServ.Add(idServis);
                    }
                    for (int i = 0; i < TableOfServises.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(TableOfServises.Rows[i][0].ToString()) == numberOfRows)
                        {
                            TableOfServises.Rows.Remove(TableOfServises.Rows[i]);
                        }
                    }
                    var delete = ListOfServisesId.Where(e1 => e1.Item2 == idServis).First();
                    ListOfServisesId.Remove(delete);
                }
            }
            else
            {
                MakeSomeHelp.MSG("Не выбрана услуга для удаления!", MsgBoxImage: MessageBoxImage.Hand);
            }
        }

        #endregion

        #region Обработки про материалы
        private void AddMaterials_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Добавление данных о материалах");
            baseWindow.MakeOpen(new AddInfromationAboutMaterials(ref baseWindow));
            baseWindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {
                SaveSomeData.MakeSomeOperation = false;
                var dataAboutMaterials = SaveSomeData.SomeObject as TaskMaterial;
                SaveSomeData.SomeObject = null;
                int numberOfRows = 0;
                if (ListOfMateriaslId.Any())
                {
                    numberOfRows = ListOfMateriaslId.Where(e2 => e2.Item2 == dataAboutMaterials.idMaterial).Select(e1 => e1.Item1).First();
                }
                decimal cost = dataAboutMaterials.cost ?? default;
                double count = dataAboutMaterials.count ?? default;
                decimal summa = cost * Convert.ToDecimal(count);
                if (numberOfRows != 0)
                {//Обновление
                    for (int i = 0; i < TableOfMaterials.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(TableOfMaterials.Rows[i][0].ToString()) == numberOfRows)
                        {
                            double countTek = Convert.ToDouble(TableOfMaterials.Rows[i][2].ToString());
                            TableOfMaterials.Rows[i][1] = dataAboutMaterials.NameOfMaterials;
                            TableOfMaterials.Rows[i][2] = dataAboutMaterials.count + countTek;
                            TableOfMaterials.Rows[i][3] = dataAboutMaterials.cost;
                            TableOfMaterials.Rows[i][4] = cost * Convert.ToDecimal((dataAboutMaterials.count + countTek));
                        }
                    }
                }
                else
                {//Добавление
                    DataRow dataRow = TableOfMaterials.NewRow();
                    dataRow[0] = MaxMaterial;
                    dataRow[1] = dataAboutMaterials.NameOfMaterials;
                    dataRow[2] = dataAboutMaterials.count;
                    dataRow[3] = dataAboutMaterials.cost;
                    dataRow[4] = summa;
                    TableOfMaterials.Rows.Add(dataRow);
                    ListOfMateriaslId.Add(new Tuple<int, Guid>(MaxMaterial, dataAboutMaterials.idMaterial));
                    MaxMaterial++;
                }
            }
        }

        private void EditMaterials_Click(object sender, RoutedEventArgs e)
        {
            int index = DataAboutMaterials.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataAboutMaterials, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    Guid idMaterial = ListOfMateriaslId.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                    TaskMaterial InfAbMat = null;
                    for (int i = 0; i < TableOfMaterials.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(TableOfMaterials.Rows[i][0].ToString()) == numberOfRows)
                        {
                            InfAbMat = new TaskMaterial
                            {
                                idMaterial = idMaterial,
                                cost = Convert.ToDecimal(TableOfMaterials.Rows[i][3].ToString()),
                                count = Convert.ToInt32(TableOfMaterials.Rows[i][2].ToString()),
                                NameOfMaterials = TableOfMaterials.Rows[i][1].ToString()
                            };
                        }
                    }
                    BaseWindow baseWindow = new BaseWindow("Редактирование данных о материале");
                    baseWindow.MakeOpen(new AddServisesInOrder(ref baseWindow, InfAbMat));
                    baseWindow.ShowDialog();
                    if (SaveSomeData.MakeSomeOperation)
                    {
                        SaveSomeData.MakeSomeOperation = false;
                        var updatedData = SaveSomeData.SomeObject as TaskMaterial;
                        SaveSomeData.SomeObject = null;
                        decimal cost = updatedData.cost ?? default;
                        double count = updatedData.count ?? default;
                        decimal summa = cost * Convert.ToDecimal(count);
                        for (int i = 0; i < TableOfMaterials.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(TableOfMaterials.Rows[i][0].ToString()) == numberOfRows)
                            {
                                TableOfMaterials.Rows[i][1] = updatedData.NameOfMaterials;
                                TableOfMaterials.Rows[i][2] = updatedData.count;
                                TableOfMaterials.Rows[i][3] = updatedData.cost;
                                TableOfMaterials.Rows[i][4] = summa;
                            }
                        }
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Не выбран материал для редактирования!", MsgBoxImage: MessageBoxImage.Hand);
            }
        }

        private void DeleteMaterials_Click(object sender, RoutedEventArgs e)
        {

            int index = DataAboutServises.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataAboutServises, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    Guid idMaterial = ListOfMateriaslId.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                    if (!NewTask)
                    {
                        ListOfDeleteMat.Add(idMaterial);
                    }
                    for (int i = 0; i < TableOfMaterials.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(TableOfMaterials.Rows[i][0].ToString()) == numberOfRows)
                        {
                            TableOfMaterials.Rows.Remove(TableOfMaterials.Rows[i]);
                        }
                    }
                    var delete = ListOfMateriaslId.Where(e1 => e1.Item2 == idMaterial).First();
                    ListOfMateriaslId.Remove(delete);
                }
            }
            else
            {
                MakeSomeHelp.MSG("Не выбран материал для удаления!", MsgBoxImage: MessageBoxImage.Hand);
            }
        }

        #endregion

        #region Обработки при работе с работниками


        private void SelectWorker_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Добавление новых работников в задание");
            baseWindow.MakeOpen(new AddWorkerInTask(ref baseWindow));
            baseWindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {
                SaveSomeData.MakeSomeOperation = false;
                var dataAbout = SaveSomeData.SomeObject as TaskWorker;
                SaveSomeData.SomeObject = null;
                int numberOfRows = 0;
                if (ListOfWorkerId.Any())
                {
                    numberOfRows = ListOfWorkerId.Where(e2 => e2.Item2 == dataAbout.idWorker).Select(e1 => e1.Item1).First();

                }
                if (numberOfRows != 0)
                {
                    MakeSomeHelp.MSG("Работник уже есть в задании", MsgBoxImage: MessageBoxImage.Hand);
                }
                else
                {
                    bool err = false;
                    for (int i = 0; i < TableOfWorker.Rows.Count; i++)
                    {
                        if (TableOfWorker.Rows[i][3].ToString() == SomeEnums.RoleOfWorker[0] && SomeEnums.RoleOfWorker[0] == dataAbout.Role)
                        {
                            MakeSomeHelp.MSG("В заказе не может быть больше 1 прораба", MsgBoxImage: MessageBoxImage.Hand);
                            err = true;
                        }
                    }
                    if (!err)
                    {
                        DataRow dataRow = TableOfWorker.NewRow();
                        dataRow[0] = MaxWorker;
                        dataRow[1] = dataAbout.FioOfWorker;
                        dataRow[2] = dataAbout.NameOfPost;
                        dataRow[3] = dataAbout.Role;
                        TableOfWorker.Rows.Add(dataRow);
                        ListOfWorkerId.Add(new Tuple<int, Guid>(MaxWorker, dataAbout.idWorker));
                        MaxWorker++;
                    }
                }
            }
        }

        private void EditSelectWorker_Click(object sender, RoutedEventArgs e)
        {
            int index = DataAboutWorker.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataAboutWorker, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    Guid idWorker = ListOfWorkerId.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                    TaskWorker InfAbWorker = null;
                    for (int i = 0; i < TableOfWorker.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(TableOfWorker.Rows[i][0].ToString()) == numberOfRows)
                        {
                            InfAbWorker = new TaskWorker
                            {
                                idWorker = idWorker,
                                FioOfWorker = TableOfWorker.Rows[i][1].ToString(),
                                NameOfPost = TableOfWorker.Rows[i][2].ToString(),
                                Role = TableOfWorker.Rows[i][3].ToString(),
                            };
                        }
                    }
                    BaseWindow baseWindow = new BaseWindow("Редактирование данных о  работниках");
                    baseWindow.MakeOpen(new AddWorkerInTask(ref baseWindow, InfAbWorker));
                    baseWindow.ShowDialog();
                    if (SaveSomeData.MakeSomeOperation)
                    {
                        SaveSomeData.MakeSomeOperation = false;
                        var updatedData = SaveSomeData.SomeObject as TaskWorker;
                        SaveSomeData.SomeObject = null;
                        bool isBrigadeHave = false;
                        for (int i = 0; i < TableOfWorker.Rows.Count; i++)
                        {
                            if (TableOfWorker.Rows[i][3].ToString() == SomeEnums.RoleOfWorker[0] && SomeEnums.RoleOfWorker[0] == updatedData.Role)
                            {
                                MakeSomeHelp.MSG("В заказе не может быть больше 1 прораба", MsgBoxImage: MessageBoxImage.Hand);
                                isBrigadeHave = true;
                            }
                        }
                        if (!isBrigadeHave)
                        {
                            var RowsHaveWorker = ListOfServisesId.Where(e2 => e2.Item2 == updatedData.idWorker).Select(e1 => e1.Item1);
                            if (RowsHaveWorker.Any())
                            {
                                MakeSomeHelp.MSG("Работник уже есть в задании!", MsgBoxImage: MessageBoxImage.Hand);

                            }
                            else
                            {
                                for (int i = 0; i < TableOfWorker.Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(TableOfWorker.Rows[i][0].ToString()) == numberOfRows)
                                    {
                                        TableOfWorker.Rows[i][1] = updatedData.FioOfWorker;
                                        TableOfWorker.Rows[i][2] = updatedData.NameOfPost;
                                        TableOfWorker.Rows[i][3] = updatedData.Role;
                                    }
                                }
                            }
                        }
                    }

                }
            }
            else
            {
                MakeSomeHelp.MSG("Не выбрана услуга для редактирование!", MsgBoxImage: MessageBoxImage.Hand);
            }
        }

        private void DeleteWorker_Click(object sender, RoutedEventArgs e)
        {
            int index = DataAboutWorker.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataAboutWorker, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    Guid idWorker = ListOfWorkerId.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                    if (NewTask)
                    {
                        for (int i = 0; i < TableOfWorker.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(TableOfWorker.Rows[i][0].ToString()) == numberOfRows)
                            {
                                TableOfWorker.Rows.Remove(TableOfWorker.Rows[i]);
                            }
                        }
                        var delete = ListOfWorkerId.Where(e1 => e1.Item2 == idWorker).First();
                        ListOfWorkerId.Remove(delete);
                    }
                    else
                    {//TODO Удаление при редактировании задании
                        ListOfDeleteWorker.Add(idWorker);
                        for (int i = 0; i < TableOfServises.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(TableOfWorker.Rows[i][0].ToString()) == numberOfRows)
                            {
                                TableOfWorker.Rows.Remove(TableOfWorker.Rows[i]);
                            }
                        }
                        var delete = ListOfWorkerId.Where(e1 => e1.Item2 == idWorker).First();
                        ListOfWorkerId.Remove(delete);
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Не выбрана работник для исплючения!", MsgBoxImage: MessageBoxImage.Hand);
            }
        }

        #endregion

        #region Вспомогательные

        #region При работе с основной формой

        private void ShowNeededPage(Grid grid)
        {
            ForMainData.Visibility = Visibility.Collapsed;
            ForServises.Visibility = Visibility.Collapsed;
            ForMaterials.Visibility = Visibility.Collapsed;
            ForWorker.Visibility = Visibility.Collapsed;
            grid.Visibility = Visibility.Visible;
        }

        #endregion

        private void MakePreparationTableColumnName()
        {

            TableOfMaterials = new DataTable();
            TableOfServises = new DataTable();
            TableOfWorker = new DataTable();
            ListOfMateriaslId = new List<Tuple<int, Guid>>();
            ListOfServisesId = new List<Tuple<int, Guid>>();
            ListOfWorkerId = new List<Tuple<int, Guid>>();
            foreach (string NameOfMat in SomeEnums.TaskMaterialTable)
            {
                TableOfMaterials.Columns.Add(NameOfMat);
            }
            DataAboutMaterials.ItemsSource = TableOfMaterials.DefaultView;
            foreach (string NameOfMat in SomeEnums.TaskServisTable)
            {
                TableOfServises.Columns.Add(NameOfMat);
            }
            DataAboutServises.ItemsSource = TableOfServises.DefaultView;
            foreach (string NameOfMat in SomeEnums.TaskWorkerTable)
            {
                TableOfWorker.Columns.Add(NameOfMat);
            }
            DataAboutWorker.ItemsSource = TableOfWorker.DefaultView;
        }


        #endregion
    }
}
