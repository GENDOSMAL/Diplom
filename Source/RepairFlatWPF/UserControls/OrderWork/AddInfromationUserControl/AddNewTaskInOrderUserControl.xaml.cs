using Newtonsoft.Json;
using RepairFlatWPF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
        List<Tuple<int, Guid, Guid>> ListOfMateriaslId, ListOfServisesId, ListOfWorkerId;
        List<Guid> ListOfDeleteServ, ListOfDeleteMat, ListOfDeleteWorker;

        TextBlock textBoxSumma, textBoxost;
        #endregion

        #region Конструктор
        public AddNewTaskInOrderUserControl(ref BaseWindow baseWindow, ref TextBlock textBoxSumma, ref TextBlock textBoxost, Guid idOrder, Guid idTask = new Guid())
        {
            InitializeComponent();
            this.textBoxSumma = textBoxSumma;
            this.textBoxost = textBoxost;
            this.idOrder = idOrder;
            ShowNeededPage(ForMainData);
            Window = baseWindow;
            MakePreparationTableColumnName();
            if (idTask != new Guid())
            {
                this.idTask = idTask;
                NewTask = false;
                MakeDataByid();
                ExtionButton.Content = "Редактировать";
            }
            else
            {
                this.idTask = Guid.NewGuid();
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


        private async void ExtionButton_Click(object sender, RoutedEventArgs e)
        {
            if (MakeCheckData())
            {
                InformationAboutTask informationAboutTask = new InformationAboutTask();
                informationAboutTask.idOrder = idOrder;
                informationAboutTask.idTask = idTask;

                informationAboutTask.InfAbMat = new MaterialInfTask();
                informationAboutTask.InfAbWorkers = new WorkersInfTask();
                informationAboutTask.InfAbServ = new ServisesInfTask();
                informationAboutTask.InfAbMat.materialsInf = new List<TaskMaterial>();
                informationAboutTask.InfAbWorkers.WorkerInf = new List<TaskWorker>();
                informationAboutTask.InfAbServ.ServisInf = new List<TaskServises>();
                if (!NewTask)
                {
                    if (ListOfDeleteServ.Any())
                    {
                        informationAboutTask.InfAbServ.DeleteServises = new List<Guid>();
                        informationAboutTask.InfAbServ.DeleteServises = ListOfDeleteServ;
                    }
                    if (ListOfDeleteMat.Any())
                    {
                        informationAboutTask.InfAbMat.DeleteMaterials = new List<Guid>();
                        informationAboutTask.InfAbMat.DeleteMaterials = ListOfDeleteMat;
                    }

                    if (ListOfDeleteWorker.Any())
                    {
                        informationAboutTask.InfAbWorkers.DeleteWorkers = new List<Guid>();
                        informationAboutTask.InfAbWorkers.DeleteWorkers = ListOfDeleteWorker;
                    }

                }
                informationAboutTask.DateEnd = DateEnd.SelectedDate;
                informationAboutTask.DateStart = DateStart.SelectedDate;
                informationAboutTask.Description = Description.Text?.Trim();
                for (int i = 0; i < TableOfMaterials.Rows.Count; i++)
                {
                    var dd = ListOfMateriaslId.Where(ee => ee.Item1 == Convert.ToInt32(TableOfMaterials.Rows[i][0].ToString())).First();
                    TaskMaterial taskMaterial = new TaskMaterial
                    {
                        cost = Convert.ToDouble(TableOfMaterials.Rows[i][3].ToString()),
                        count = Convert.ToDouble(TableOfMaterials.Rows[i][2].ToString()),
                        idMaterial = dd.Item2,
                        idTaskMaterial = dd.Item3,
                        NameOfMaterials = TableOfMaterials.Rows[i][1].ToString()
                    };
                    informationAboutTask.InfAbMat.materialsInf.Add(taskMaterial);
                }
                for (int i = 0; i < TableOfServises.Rows.Count; i++)
                {
                    var dd = ListOfServisesId.Where(ee => ee.Item1 == Convert.ToInt32(TableOfServises.Rows[i][0].ToString())).First();
                    TaskServises taskServises = new TaskServises
                    {
                        cost = Convert.ToDouble(TableOfServises.Rows[i][3].ToString()),
                        count = Convert.ToDouble(TableOfServises.Rows[i][2].ToString()),
                        idServis = dd.Item2,
                        idTaskServises = dd.Item3,
                        NameOfServises = TableOfServises.Rows[i][1].ToString()
                    };
                    informationAboutTask.InfAbServ.ServisInf.Add(taskServises);
                }
                for (int i = 0; i < TableOfWorker.Rows.Count; i++)
                {
                    var dd = ListOfWorkerId.Where(ee => ee.Item1 == Convert.ToInt32(TableOfWorker.Rows[i][0].ToString())).First();
                    TaskWorker TaskWorker = new TaskWorker
                    {
                        FioOfWorker = TableOfWorker.Rows[i][1].ToString(),
                        idWorker = dd.Item2,
                        Role = TableOfWorker.Rows[i][3].ToString(),
                        idTaskWorker = dd.Item3,
                    };
                    informationAboutTask.InfAbWorkers.WorkerInf.Add(TaskWorker);
                }
                informationAboutTask.Summa = Convert.ToDecimal(AllSumma.Text.ToString());
                string Json = JsonConvert.SerializeObject(informationAboutTask);
                string urlSend = "api/order/create/task";
                var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(ExtionButton_Click)));
                var deserializedProduct = JsonConvert.DeserializeObject<SummaOfOrder>(task.ToString());
                if (!deserializedProduct.success)
                {
                    MakeSomeHelp.MSG($"Произошла ошикбка при работе {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
                }
                else
                {
                    textBoxost.Text = deserializedProduct.NeedPay.ToString();
                    textBoxSumma.Text = deserializedProduct.summaOfOrder.ToString();
                    MakeSomeHelp.MSG("Операции над данными были произведены!", MsgBoxImage: MessageBoxImage.Information);
                }
                Window.Close();
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
                    var sel = ListOfServisesId.Where(e2 => e2.Item2 == dataAbout.idServis);
                    if (sel.Any())
                    {
                        numberOfRows = sel.Select(e1 => e1.Item1).First();
                    }
                }
                double cost = dataAbout.cost ?? default;
                double count = dataAbout.count ?? default;
                double summa = cost * count;
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
                            TableOfServises.Rows[i][4] = cost * (dataAbout.count + countTek);
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
                    ListOfServisesId.Add(new Tuple<int, Guid, Guid>(MaxServis, dataAbout.idServis, Guid.NewGuid()));
                    MaxServis++;
                }
                MakeSumma();
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
                                cost = Convert.ToDouble(TableOfServises.Rows[i][3].ToString()),
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
                        double cost = updatedData.cost ?? default;
                        double count = updatedData.count ?? default;
                        double summa = cost * count;
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
                    MakeSumma();
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
                        ListOfDeleteServ.Add(ListOfServisesId.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item3).First());
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
                    MakeSumma();
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
                    var sele = ListOfMateriaslId.Where(e2 => e2.Item2 == dataAboutMaterials.idMaterial);
                    if (sele.Any())
                        numberOfRows = sele.Select(e1 => e1.Item1).First();
                }
                double cost = dataAboutMaterials.cost ?? default;
                double count = dataAboutMaterials.count ?? default;
                double summa = cost * count;
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
                            TableOfMaterials.Rows[i][4] = cost * (dataAboutMaterials.count + countTek);
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
                    ListOfMateriaslId.Add(new Tuple<int, Guid, Guid>(MaxMaterial, dataAboutMaterials.idMaterial, Guid.NewGuid()));
                    MaxMaterial++;
                }
                MakeSumma();
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
                                cost = Convert.ToDouble(TableOfMaterials.Rows[i][3].ToString()),
                                count = Convert.ToInt32(TableOfMaterials.Rows[i][2].ToString()),
                                NameOfMaterials = TableOfMaterials.Rows[i][1].ToString()
                            };
                        }
                    }
                    BaseWindow baseWindow = new BaseWindow("Редактирование данных о материале");
                    baseWindow.MakeOpen(new AddInfromationAboutMaterials(ref baseWindow, InfAbMat));
                    baseWindow.ShowDialog();
                    if (SaveSomeData.MakeSomeOperation)
                    {
                        SaveSomeData.MakeSomeOperation = false;
                        var updatedData = SaveSomeData.SomeObject as TaskMaterial;
                        SaveSomeData.SomeObject = null;
                        double cost = updatedData.cost ?? default;
                        double count = updatedData.count ?? default;
                        double summa = cost * count;
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
                    MakeSumma();
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
                        ListOfDeleteMat.Add(ListOfMateriaslId.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item3).First());
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
                    MakeSumma();
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
                    var sele = ListOfWorkerId.Where(e2 => e2.Item2 == dataAbout.idWorker);
                    if (sele.Any())
                        numberOfRows = sele.Select(e1 => e1.Item1).First();

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
                        ListOfWorkerId.Add(new Tuple<int, Guid, Guid>(MaxWorker, dataAbout.idWorker, Guid.NewGuid()));
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
                        ListOfDeleteWorker.Add(ListOfWorkerId.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item3).First());
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

        private async void MakeDataByid()
        {
            var DataDle = await Task.Run(() => MakeSomeHelp.MakeDownloadByLink($"api/order/get/task?idTask={idTask}"));
            var DataAbTask = JsonConvert.DeserializeObject<InformationAboutTask>(DataDle.ToString());
            if (DataAbTask.success)
            {
                DateStart.SelectedDate = DataAbTask.DateStart;
                DateEnd.SelectedDate = DataAbTask.DateEnd;
                Description.Text = DataAbTask.Description?.Trim();
                AllSumma.Text = DataAbTask.Summa.ToString();
                if (DataAbTask.InfAbWorkers != null)
                {
                    if (DataAbTask.InfAbWorkers.WorkerInf.Any())
                    {
                        foreach (var Worker in DataAbTask.InfAbWorkers.WorkerInf)
                        {
                            DataRow dataRow = TableOfWorker.NewRow();
                            dataRow[0] = MaxWorker;
                            dataRow[1] = Worker.FioOfWorker?.Trim();
                            dataRow[2] = Worker.NameOfPost?.Trim();
                            dataRow[3] = Worker.Role?.Trim();
                            TableOfWorker.Rows.Add(dataRow);
                            ListOfWorkerId.Add(new Tuple<int, Guid, Guid>(MaxWorker, Worker.idWorker, Worker.idTaskWorker));
                            MaxWorker++;
                        }
                    }
                }

                if (DataAbTask.InfAbMat != null)
                {
                    if (DataAbTask.InfAbMat.materialsInf.Any())
                    {
                        foreach (var Material in DataAbTask.InfAbMat.materialsInf)
                        {
                            double cost = Material.cost ?? default;
                            double count = Material.count ?? default;
                            double summa = cost * count;
                            DataRow dataRow = TableOfMaterials.NewRow();
                            dataRow[0] = MaxMaterial;
                            dataRow[1] = Material.NameOfMaterials?.Trim();
                            dataRow[2] = Material.count;
                            dataRow[3] = Material.cost;
                            dataRow[4] = summa;
                            TableOfMaterials.Rows.Add(dataRow);
                            ListOfMateriaslId.Add(new Tuple<int, Guid, Guid>(MaxMaterial, Material.idMaterial, Material.idTaskMaterial));
                            MaxMaterial++;
                        }
                    }
                }

                if (DataAbTask.InfAbServ != null)
                {
                    if (DataAbTask.InfAbServ.ServisInf.Any())
                    {
                        foreach (var Serv in DataAbTask.InfAbServ.ServisInf)
                        {
                            double cost = Serv.cost ?? default;
                            double count = Serv.count ?? default;
                            double summa = cost * count;
                            DataRow dataRow = TableOfServises.NewRow();
                            dataRow[0] = MaxServis;
                            dataRow[1] = Serv.NameOfServises?.Trim();
                            dataRow[2] = Serv.count;
                            dataRow[3] = Serv.cost;
                            dataRow[4] = summa;
                            TableOfServises.Rows.Add(dataRow);
                            ListOfServisesId.Add(new Tuple<int, Guid, Guid>(MaxServis, Serv.idServis, Serv.idTaskServises));
                            MaxServis++;
                        }
                    }
                }
            }
        }

        #endregion

        private void MakePreparationTableColumnName()
        {

            TableOfMaterials = new DataTable();
            TableOfServises = new DataTable();
            TableOfWorker = new DataTable();
            ListOfMateriaslId = new List<Tuple<int, Guid, Guid>>();
            ListOfServisesId = new List<Tuple<int, Guid, Guid>>();
            ListOfWorkerId = new List<Tuple<int, Guid, Guid>>();
            ListOfDeleteMat = new List<Guid>();
            ListOfDeleteServ = new List<Guid>();
            ListOfDeleteWorker = new List<Guid>();
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

        private bool MakeCheckData()
        {
            if (!DateStart.SelectedDate.HasValue)
            {
                MakeSomeHelp.MSG("Необходимо указать дату начала", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (!DateEnd.SelectedDate.HasValue)
            {
                MakeSomeHelp.MSG("Необходимо указать планируемую дату окончания", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (DateStart.SelectedDate > DateEnd.SelectedDate)
            {
                MakeSomeHelp.MSG("Дата начала не может быть больше даты окончания", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (TableOfServises.Rows.Count == 0)
            {
                if (TableOfWorker.Rows.Count == 0)
                {
                    MakeSomeHelp.MSG("Для создания задания необходимо иметь какие либо услуги и работников, которые должны выполнить", MsgBoxImage: MessageBoxImage.Hand);
                    return false;
                }
                else
                {
                    MakeSomeHelp.MSG("Для создания задания необходимо иметь какие либо услуги", MsgBoxImage: MessageBoxImage.Hand);
                    return false;
                }
            }
            if (TableOfWorker.Rows.Count == 0)
            {
                MakeSomeHelp.MSG("Укажите работников для выполнения задания", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            return true;
        }

        private void MakeSumma()
        {
            double summa = 0;
            for (int i = 0; i < TableOfMaterials.Rows.Count; i++)
            {
                summa = summa + Convert.ToDouble(TableOfMaterials.Rows[i][4].ToString());
            }
            for (int i = 0; i < TableOfServises.Rows.Count; i++)
            {
                summa = summa + Convert.ToDouble(TableOfServises.Rows[i][4].ToString());
            }
            AllSumma.Text = summa.ToString();
        }

        #endregion
    }
}
