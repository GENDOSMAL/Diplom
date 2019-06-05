using RepairFlatWPF.Controller;
using RepairFlatWPF.Model;
using RepairFlatWPF.UserControls.SettingsAndSubsInf.ControlForRedact;
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
    /// Interaction logic for RedactSomeSubs.xaml
    /// </summary>
    public partial class RedactSomeSubs : UserControl
    {
        SomeEnums.TypeOfSubs typeOfSubs;
        DataTable DataAboutSomeSubInf;
        List<Tuple<int, Guid>> ListofId;
        public RedactSomeSubs(SomeEnums.TypeOfSubs typeOfSubs)
        {
            InitializeComponent();
            this.typeOfSubs = typeOfSubs;
            makeSelectFromDB();
        }

        private void MakePreparateData()
        {
            DataAboutSomeSubInf = new DataTable();
            ListofId = new List<Tuple<int, Guid>>();
        }

        private void makeSelectFromDB()
        {
            MakePreparateData();
            TypeOfWork.Text = "Работа над информации о:";
            DataAboutSomeSubInf = MakeSomeHelp.DataTableFromDataBase(typeOfSubs);
            switch (typeOfSubs)
            {
                case SomeEnums.TypeOfSubs.Contact: { TypeOfWork.Text += " типе контактов."; break; }
                case SomeEnums.TypeOfSubs.Materials: { TypeOfWork.Text += " материалах."; break; }
                case SomeEnums.TypeOfSubs.Post: { TypeOfWork.Text += " должностях."; break; }
                case SomeEnums.TypeOfSubs.Premises: { TypeOfWork.Text += " типах помещений."; break; }
                case SomeEnums.TypeOfSubs.Servises: { TypeOfWork.Text += "б услугах"; break; }
            }
            ListofId = MakeSomeHelp.ListofId;
            DataGrid.ItemsSource = DataAboutSomeSubInf.DefaultView;
        }

        private void RetunBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.SelectDataForRedactUC());
        }

        private void AddElement_Click(object sender, RoutedEventArgs e)
        {
            switch (typeOfSubs)
            {
                case SomeEnums.TypeOfSubs.Contact:
                    {//Работа с контакной информацией
                        BaseWindow baseWindow = new BaseWindow("Добавление типа контакной информации");
                        baseWindow.MakeOpen(new ContactTypeRedactUC(ref baseWindow));
                        baseWindow.ShowDialog();
                        makeSelectFromDB();
                        break;
                    }
                case SomeEnums.TypeOfSubs.Materials:
                    {//Работа с материалами
                        BaseWindow baseWindow = new BaseWindow("Добавление материала");
                        baseWindow.MakeOpen(new MaterialsRedactUC(ref baseWindow));
                        baseWindow.ShowDialog();
                        makeSelectFromDB();
                        break;
                    }
                case SomeEnums.TypeOfSubs.Post:
                    {//Работа с должностями
                        BaseWindow baseWindow = new BaseWindow("Добавление должностей");
                        baseWindow.MakeOpen(new PostRedactUC(ref baseWindow));
                        baseWindow.ShowDialog();
                        makeSelectFromDB();
                        break;
                    }
                case SomeEnums.TypeOfSubs.Premises:
                    {//Работа с типами помещений
                        BaseWindow baseWindow = new BaseWindow("Добавление типов помещений");
                        baseWindow.MakeOpen(new PremisesRedactUC(ref baseWindow));
                        baseWindow.ShowDialog();
                        makeSelectFromDB();
                        break;
                    }
                case SomeEnums.TypeOfSubs.Servises:
                    {//Работа с услугами
                        BaseWindow baseWindow = new BaseWindow("Добавление новых услуг");
                        baseWindow.MakeOpen(new ServisesRedactUC(ref baseWindow));
                        baseWindow.ShowDialog();
                        makeSelectFromDB();
                        break;
                    }
            }
        }

        private void EditElement_Click(object sender, RoutedEventArgs e)
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
                            SaveSomeData.MakeSomeOperation = false;
                            SaveSomeData.SomeObject = null;

                            switch (typeOfSubs)
                            {
                                case SomeEnums.TypeOfSubs.Contact:
                                    {//Работа с контакной информацией
                                        string value = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index, 1).ToString();
                                        string desc = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index, 2).ToString();
                                        BaseWindow baseWindow = new BaseWindow("Редактирование контакной информации");
                                        baseWindow.MakeOpen(new ContactTypeRedactUC(ref baseWindow, idSubs, value, desc));
                                        baseWindow.ShowDialog();
                                        makeSelectFromDB();
                                        break;
                                    }
                                case SomeEnums.TypeOfSubs.Materials:
                                    {//Работа с материалами
                                        RepairFlat.Model.MakeSubs.ListOfMaterials listOfMaterials = new RepairFlat.Model.MakeSubs.ListOfMaterials();
                                        listOfMaterials.NameOfMaterial = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index, 1).ToString();
                                        listOfMaterials.UnitOfMeasue = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index, 2).ToString();
                                        listOfMaterials.TypeOfMaterial = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index, 3).ToString();
                                        listOfMaterials.Cost = Decimal.Parse(MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index, 4).ToString());
                                        listOfMaterials.Description = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index, 5).ToString();
                                        BaseWindow baseWindow = new BaseWindow("Редактирование материала");
                                        baseWindow.MakeOpen(new MaterialsRedactUC(ref baseWindow, listOfMaterials));
                                        baseWindow.ShowDialog();
                                        makeSelectFromDB();
                                        break;
                                    }
                                case SomeEnums.TypeOfSubs.Post:
                                    {//Работа с должностями
                                        string dd = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index, 3).ToString();
                                        bool? MakeWoe = default;
                                        if (dd == "Да")
                                        {
                                            MakeWoe = true;
                                        }
                                        else if (dd == "Нет")
                                        {
                                            MakeWoe = false;
                                        }
                                        else
                                        {
                                            MakeWoe = null;
                                        }

                                        RepairFlat.Model.MakeSubs.ListOfPost listOfPost = new RepairFlat.Model.MakeSubs.ListOfPost();
                                        listOfPost.NameOfPost = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index, 1).ToString();
                                        listOfPost.MakeWork = MakeWoe;
                                        listOfPost.idPost = idSubs;
                                        listOfPost.BaseWage = Convert.ToDecimal(MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index, 2).ToString());
                                        BaseWindow baseWindow = new BaseWindow("Редактирование данных о должностях");
                                        baseWindow.MakeOpen(new PostRedactUC(ref baseWindow, listOfPost));
                                        baseWindow.ShowDialog();
                                        makeSelectFromDB();
                                        break;
                                    }
                                case SomeEnums.TypeOfSubs.Premises:
                                    {//Работа с типами помещений
                                        RepairFlat.Model.MakeSubs.ListOfPremises listOfPremises = new RepairFlat.Model.MakeSubs.ListOfPremises();
                                        listOfPremises.idPremises = idSubs;
                                        listOfPremises.Name = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index, 1).ToString();
                                        listOfPremises.Description = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index, 2).ToString();
                                        BaseWindow baseWindow = new BaseWindow("Обновление данных о типах помещений");
                                        baseWindow.MakeOpen(new PremisesRedactUC(ref baseWindow, listOfPremises));
                                        baseWindow.ShowDialog();
                                        makeSelectFromDB();
                                        break;
                                    }
                                case SomeEnums.TypeOfSubs.Servises:
                                    {//Работа с услугами
                                        RepairFlat.Model.MakeSubs.ListOfServises listOfServises = new RepairFlat.Model.MakeSubs.ListOfServises();
                                        listOfServises.idServises = idSubs;
                                        listOfServises.Nomination = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index, 1).ToString();
                                        listOfServises.TypeOfServises = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index, 2).ToString();
                                        listOfServises.Cost = Convert.ToDecimal(MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index, 3).ToString());
                                        listOfServises.Description = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index, 4).ToString();
                                        BaseWindow baseWindow = new BaseWindow("Обновление данных об услугах");
                                        baseWindow.MakeOpen(new ServisesRedactUC(ref baseWindow, listOfServises));
                                        baseWindow.ShowDialog();
                                        makeSelectFromDB();
                                        break;
                                    }
                            }

                        }
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать данные для работы!", MsgBoxImage: MessageBoxImage.Hand);
            }


        }

        private void DeleteElement_Click(object sender, RoutedEventArgs e)
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
                            SaveSomeData.SomeObject = idSubs;

                            switch (typeOfSubs)
                            {
                                case SomeEnums.TypeOfSubs.Contact:
                                    {//Работа с контакной информацией

                                        break;
                                    }
                                case SomeEnums.TypeOfSubs.Materials:
                                    {//Работа с материалами

                                        break;
                                    }
                                case SomeEnums.TypeOfSubs.Post:
                                    {//Работа с должностями

                                        break;
                                    }
                                case SomeEnums.TypeOfSubs.Premises:
                                    {//Работа с типами помещений

                                        break;
                                    }
                                case SomeEnums.TypeOfSubs.Servises:
                                    {//Работа с услугами

                                        break;
                                    }
                            }
                        }
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать данные для работы!", MsgBoxImage: MessageBoxImage.Hand);
            }
        }
    }
}
