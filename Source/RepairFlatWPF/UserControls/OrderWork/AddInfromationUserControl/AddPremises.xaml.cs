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
using static RepairFlatWPF.Model.MeasuModel;

namespace RepairFlatWPF.UserControls.OrderWork.AddInfromationUserControl
{
    /// <summary>
    /// Interaction logic for AddPremises.xaml
    /// </summary>
    public partial class AddPremises : UserControl
    {
        #region Переменные
        bool NewData = true;
        int LastRedButton = 0;
        Guid idOrder;
        Guid idPremises;
        List<Guid> ListOfId;
        List<string> ListOfTooltlip;
        DataTable DataAboutElement;
        List<Tuple<int, Guid>> DataAboutID;
        bool NotElement = false;
        double LenghtData, HeightData, WidthData, Pwalls, SWall, Pfloor, Sfloor;
        List<Guid> DeletedGuid = new List<Guid>();

        BaseWindow window;
        #endregion

        #region Обработка событий и конструктор
        public AddPremises(Guid idOrder, ref BaseWindow baseWindow, Guid idPremises = new Guid())
        {
            InitializeComponent();
            window = baseWindow;
            this.idOrder = idOrder;
            MakeTable();

            if (idPremises != new Guid())
            {
                NewData = false;
                this.idPremises = idPremises;
                MakeDataAboutElement();
                AddBtn.Content = "Редактировать";
                

            }
            else
            {
                this.idPremises = Guid.NewGuid();
            }
            var ListOfPremisesType = MakeListOfTypeOfPremises();
            if (ListOfPremisesType.Count != 0)
            {
                for (int i = 0; i < ListOfPremisesType.Count; i++)
                {
                    ComboBoxItem NewItem = new ComboBoxItem();
                    NewItem.Content = ListOfPremisesType[i];
                    NewItem.ToolTip = ListOfTooltlip[i];
                    TypeOfPremises.Items.Add(NewItem);
                }
            }
            else
            {
                ComboBoxItem NewItem = new ComboBoxItem();
                NewItem.Content = "Нет данных";
                NewItem.ToolTip = "Необходимо задать данные о типах помещений";
                TypeOfPremises.Items.Add(NewItem);
                NotElement = true;
            }


        }

        private void MakeTable()
        {
            DataAboutElement = new DataTable("ElementOf");
            foreach (string Name in SomeEnums.DataAboutElement)
            {
                DataAboutElement.Columns.Add(Name);
            }
            DataGrid.ItemsSource = DataAboutElement.DefaultView;
            DataAboutID = new List<Tuple<int, Guid>>();
        }


        private async void MakeDataAboutElement()
        {
            MakeTable();
            DataAboutID = new List<Tuple<int, Guid>>();
            var InformFromserver = await Task.Run(() => MakeDownloadByLink($"api/measurment/infbyid?idMeas={idPremises}"));
            var ListOfElement = JsonConvert.DeserializeObject<Model.MeasuModel.DataAboutMeassFromDB>(InformFromserver.ToString());

            if (ListOfElement.success)
            {
                TypeOfPremises.SelectedIndex = ListOfId.IndexOf(ListOfId.Where(ee => ee == ListOfElement.idPremisesType).First());
                Description.Text = ListOfElement.Description;
                Lenght.Text = ListOfElement.Lenght.ToString();
                Width.Text = ListOfElement.Width.ToString();
                Height.Text = ListOfElement.Height.ToString();
                PWalls.Text = ListOfElement.Pwalls.ToString();
                PCelling.Text = ListOfElement.PCelling.ToString();
                SWalls.Text = ListOfElement.Swalls.ToString();
                SFloor.Text = ListOfElement.Sfloor.ToString();

                int number = 1;
                foreach (var elementInf in ListOfElement.elementOfMeasurments)
                {
                    DataRow newEmentRow = DataAboutElement.NewRow();
                    newEmentRow[0] = number;
                    newEmentRow[1] = elementInf.TypeOfElement;
                    newEmentRow[2] = elementInf.Lenght;
                    newEmentRow[3] = elementInf.Width;
                    newEmentRow[4] = elementInf.Height;
                    newEmentRow[5] = elementInf.WidthOfSlope;
                    newEmentRow[6] = elementInf.POfElement;
                    newEmentRow[7] = elementInf.Description;

                    DataAboutElement.Rows.Add(newEmentRow);
                    DataAboutID.Add(new Tuple<int, Guid>(number, elementInf.idElement));
                    number++;
                }
            }

        }

        public object MakeDownloadByLink(string UrlOfDownload)
        {
            return BaseWorkWithServer.CatchErrorWithGet(UrlOfDownload, "GET", nameof(MakeLoading), nameof(MakeDownloadByLink));
        }

        private void RedactElement_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {

                    int indeInTable= 0 ;
                    Guid idRedElement = DataAboutID.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                    for(int i=0;i< DataAboutElement.Rows.Count; i++)
                    {
                        if(Convert.ToInt32(DataAboutElement.Rows[i][0].ToString())== numberOfRows)
                        {
                            indeInTable = i;
                        }
                    }
                    
                    var modelForRedact = new ElementOfMeasurment
                    {
                        
                        idElement = idRedElement,
                        idMeasurements = idPremises,
                        Description = DataAboutElement.Rows[indeInTable][7].ToString(),
                        Height = Convert.ToDouble(DataAboutElement.Rows[indeInTable][4].ToString()),
                        Lenght = Convert.ToDouble(DataAboutElement.Rows[indeInTable][2].ToString()),
                        POfElement = Convert.ToDouble(DataAboutElement.Rows[indeInTable][6].ToString()),
                        Width = Convert.ToDouble(DataAboutElement.Rows[indeInTable][3].ToString()),
                        TypeOfElement = DataAboutElement.Rows[indeInTable][1].ToString(),
                        WidthOfSlope = Convert.ToDouble(DataAboutElement.Rows[indeInTable][5].ToString())
                    };

                    BaseWindow baseWindow = new BaseWindow("Редактирование данных об элементах помещений");
                    baseWindow.MakeOpen(new AddElementOfPremises(idPremises, ref baseWindow, modelForRedact));
                    baseWindow.ShowDialog();
                    if (SaveSomeData.MakeSomeOperation)
                    {
                        DataAboutElement.Rows[indeInTable].Delete();
                        DataAboutID.RemoveAll(ee => ee.Item1 == numberOfRows);
                        SaveSomeData.MakeSomeOperation = false;
                        if (SaveSomeData.SomeObject != null)
                        {
                            var dataAbPomes = SaveSomeData.SomeObject as ElementOfMeasurment;
                            SaveSomeData.SomeObject = null;
                            DataRow newEmentRow = DataAboutElement.NewRow();
                            newEmentRow[0] = numberOfRows;
                            newEmentRow[1] = dataAbPomes.TypeOfElement;
                            newEmentRow[2] = dataAbPomes.Lenght;
                            newEmentRow[3] = dataAbPomes.Width;
                            newEmentRow[4] = dataAbPomes.Height;
                            newEmentRow[5] = dataAbPomes.WidthOfSlope;
                            newEmentRow[6] = dataAbPomes.POfElement;
                            newEmentRow[7] = dataAbPomes.Description;
                            DataAboutElement.Rows.Add(newEmentRow);
                            DataAboutID.Add(new Tuple<int, Guid>(numberOfRows, dataAbPomes.idElement));
                        }
                    }

                }
            }
            else
            {
                MakeSomeHelp.MSG("Выберите элемент для редактирования!", MsgBoxImage: MessageBoxImage.Error);
            }
        }

        private void MakeSomeCalc()
        {
            if (double.TryParse(Lenght.Text.Trim(), out LenghtData) && double.TryParse(Height.Text.Trim(), out HeightData))
            {
                Pwalls = ((HeightData + LenghtData) * 2) * 4;
                PWalls.Text = Pwalls.ToString();
                SWall = (HeightData * LenghtData) * 4;
                SWalls.Text = SWall.ToString();
            }
            if (double.TryParse(Height.Text.Trim(), out HeightData) && double.TryParse(Width.Text.Trim(), out WidthData))
            {
                Pfloor = ((HeightData + WidthData) * 2) * 4;
                PCelling.Text = Pfloor.ToString();
                Sfloor = (HeightData * WidthData) * 4;
                SFloor.Text = SWall.ToString();
                double SElement = 0;
                if (DataAboutElement.Rows.Count != 0)
                {
                    for (int i = 0; i < DataAboutElement.Rows.Count; i++)
                    {
                        SElement += Convert.ToDouble(DataAboutElement.Rows[i][6]);
                    }
                }
                Sfloor = ((HeightData * WidthData) * 4) - SElement;
                SFloor.Text = SWall.ToString();
            }
        }

        private void SomeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            MakeSomeCalc();
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
                    if (NewData)
                    {
                        DataAboutID.RemoveAll(ee => ee.Item1 == numberOfRows);
                        for (int i = 0; i < DataAboutElement.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(DataAboutElement.Rows[i][0].ToString()) == numberOfRows)
                            {
                                DataAboutElement.Rows[i].Delete();
                                MakeSomeCalc();
                            }
                        }
                    }
                    else
                    {
                       
                        DeletedGuid.Add(DataAboutID.Where(ee => ee.Item1 == numberOfRows).Select(ee => ee.Item2).First());
                        DataAboutID.RemoveAll(ee => ee.Item1 == numberOfRows);
                        for (int i = 0; i < DataAboutElement.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(DataAboutElement.Rows[i][0].ToString()) == numberOfRows)
                            {
                                DataAboutElement.Rows[i].Delete();
                                MakeSomeCalc();
                            }
                        }
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Выберите элемент для удаления!", MsgBoxImage: MessageBoxImage.Error);
            }
        }

        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void AddElementof_Click(object sender, RoutedEventArgs e)
        {

            BaseWindow baseWindow = new BaseWindow("Добавление данных об элементах помещений");
            baseWindow.MakeOpen(new AddElementOfPremises(idPremises, ref baseWindow));
            baseWindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {
                SaveSomeData.MakeSomeOperation = false;
                if (SaveSomeData.SomeObject != null)
                {
                    var dataAbPomes = SaveSomeData.SomeObject as ElementOfMeasurment;
                    SaveSomeData.SomeObject = null;
                    int numb = 1;
                    for (int i = 0; i < DataAboutElement.Rows.Count; i++)
                    {
                        int so = Convert.ToInt32(DataAboutElement.Rows[i][0].ToString());
                        if (so > numb)
                        {
                            numb = so;
                        }
                    }
                    DataRow newEmentRow = DataAboutElement.NewRow();
                    newEmentRow[0] = numb;
                    newEmentRow[1] = dataAbPomes.TypeOfElement;
                    newEmentRow[2] = dataAbPomes.Lenght;
                    newEmentRow[3] = dataAbPomes.Width;
                    newEmentRow[4] = dataAbPomes.Height;
                    newEmentRow[5] = dataAbPomes.WidthOfSlope;
                    newEmentRow[6] = dataAbPomes.POfElement;
                    newEmentRow[7] = dataAbPomes.Description;
                    DataAboutElement.Rows.Add(newEmentRow);
                    DataAboutID.Add(new Tuple<int, Guid>(numb, dataAbPomes.idElement));
                }
            }

        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            if (CheckFields())
            {
                if (NewData)
                {
                    List<ElementOfMeasurment> listOfElement = new List<ElementOfMeasurment>();
                    for (int i = 0; i < DataAboutElement.Rows.Count; i++)
                    {
                        listOfElement.Add(new ElementOfMeasurment
                        {
                            Description = DataAboutElement.Rows[i][7].ToString(),
                            Height = Convert.ToDouble(DataAboutElement.Rows[i][3].ToString()),
                            idMeasurements = idPremises,
                            Lenght = Convert.ToDouble(DataAboutElement.Rows[i][2].ToString()),
                            idElement = DataAboutID.Where(ee => ee.Item1 == Convert.ToInt32(DataAboutElement.Rows[i][0].ToString())).Select(ee => ee.Item2).First(),
                            TypeOfElement = DataAboutElement.Rows[i][1].ToString(),
                            POfElement = Convert.ToDouble(DataAboutElement.Rows[i][6].ToString()),
                            Width = Convert.ToDouble(DataAboutElement.Rows[i][3].ToString()),
                            WidthOfSlope = Convert.ToDouble(DataAboutElement.Rows[i][5].ToString()),
                        });

                    }
                    DataAboutMeassFromDB meas = new DataAboutMeassFromDB
                    {
                        Description = Description.Text.Trim(),
                        Width = WidthData,
                        Height = HeightData,
                        idMeasurements = idPremises,
                        elementOfMeasurments = listOfElement,
                        Lenght = LenghtData,
                        IdOrder = idOrder,
                        idPremisesType = ListOfId[TypeOfPremises.SelectedIndex],
                        PCelling = Pfloor,
                        Pwalls = Pwalls,
                        Sfloor = Sfloor,
                        Swalls = SWall,
                        

                    };
                    string Json = JsonConvert.SerializeObject(meas);
                    string urlSend = "api/measurment/create";
                    AddBtn.Content = "Ожидайте...";
                    RetutnBTN.Content = "Ожидайте...";
                    AddBtn.IsEnabled = false;
                    RetutnBTN.IsEnabled = false;

                    var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(AddBtn_Click)));
                    var deserializedProduct = JsonConvert.DeserializeObject<BaseResult>(task.ToString());

                    if (!deserializedProduct.success)
                    {
                        MakeSomeHelp.MSG($"Произошла ошикбка при создании пользователя {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
                    }
                    else
                    {
                        MakeSomeHelp.MSG("Данные добавлены!", MsgBoxImage: MessageBoxImage.Information);
                    }
                    window.Close();
                }
                else
                {
                    List<ElementOfMeasurment> listOfElement = new List<ElementOfMeasurment>();
                    for (int i = 0; i < DataAboutElement.Rows.Count; i++)
                    {
                        listOfElement.Add(new ElementOfMeasurment
                        {
                            Description = DataAboutElement.Rows[i][7].ToString(),
                            Height = Convert.ToDouble(DataAboutElement.Rows[i][3].ToString()),
                            idMeasurements = idPremises,
                            Lenght = Convert.ToDouble(DataAboutElement.Rows[i][2].ToString()),
                            idElement = DataAboutID.Where(ee => ee.Item1 == Convert.ToInt32(DataAboutElement.Rows[i][0].ToString())).Select(ee => ee.Item2).First(),
                            TypeOfElement = DataAboutElement.Rows[i][1].ToString(),
                            POfElement = Convert.ToDouble(DataAboutElement.Rows[i][6].ToString()),
                            Width = Convert.ToDouble(DataAboutElement.Rows[i][3].ToString()),
                            WidthOfSlope = Convert.ToDouble(DataAboutElement.Rows[i][5].ToString()),
                        });

                    }
                    DataAboutMeassFromDB meas = new DataAboutMeassFromDB
                    {
                        Description = Description.Text.Trim(),
                        Width = WidthData,
                        Height = HeightData,
                        idMeasurements = idPremises,
                        elementOfMeasurments = listOfElement,
                        Lenght = LenghtData,
                        IdOrder = idOrder,
                        idPremisesType = ListOfId[TypeOfPremises.SelectedIndex],
                        PCelling = Pfloor,
                        Pwalls = Pwalls,
                        Sfloor = Sfloor,
                        Swalls = SWall,
                        DeletedElement = DeletedGuid
                    };
                    string Json = JsonConvert.SerializeObject(meas);
                    string urlSend = "api/measurment/update";
                    AddBtn.Content = "Ожидайте...";
                    RetutnBTN.Content = "Ожидайте...";
                    AddBtn.IsEnabled = false;
                    RetutnBTN.IsEnabled = false;

                    var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(AddBtn_Click)));
                    var deserializedProduct = JsonConvert.DeserializeObject<BaseResult>(task.ToString());

                    if (!deserializedProduct.success)
                    {
                        MakeSomeHelp.MSG($"Произошла ошибка при обновлении данных о помщениях {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
                    }
                    else
                    {
                        MakeSomeHelp.MSG("Данные обновлены!", MsgBoxImage: MessageBoxImage.Information);
                    }
                    window.Close();
                }
            }

        }
        #endregion

        #region Дополнительно
        private bool CheckFields()
        {
            if (TypeOfPremises.SelectedIndex == -1)
            {
                MakeSomeHelp.MSG("Необходимо выбрать тип помещения", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (!double.TryParse(Lenght.Text.Trim(), out LenghtData))
            {
                MakeSomeHelp.MSG("Необходимо указать длине", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (!double.TryParse(Width.Text.Trim(), out WidthData))
            {
                MakeSomeHelp.MSG("Необходимо указать ширину", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (!double.TryParse(Height.Text.Trim(), out HeightData))
            {
                MakeSomeHelp.MSG("Необходимо указать высоту", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }

            if (DataAboutElement.Rows.Count == 0)
            {
                if (MakeSomeHelp.MSG("Вы действительно хотите не добавлять данные об элементах помешения?", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }

        private List<string> MakeListOfTypeOfPremises()
        {
            List<string> TypeOfContact = new List<string>();
            ListOfId = new List<Guid>();
            ListOfTooltlip = new List<string>();
            string query = "Select * from PremisesType";
            var TablesOfTypeOfContact = Controller.MakeWorkWirthDataBase.MakeSomeQueryWork(query, WorkWithTables: true);
            if (TablesOfTypeOfContact != null)
            {
                DataTable ContactType = TablesOfTypeOfContact as DataTable;
                for (int i = 0; i < ContactType.Rows.Count; i++)
                {
                    try
                    {
                        string desc = !string.IsNullOrEmpty(ContactType.Rows[i]["Descriprtion"].ToString()) ? ContactType.Rows[i]["Descriprtion"].ToString() : "Не данных";
                        ListOfTooltlip.Add(desc);
                        TypeOfContact.Add(ContactType.Rows[i]["NameOfPremises"].ToString());
                        Guid id;
                        if (Guid.TryParse(ContactType.Rows[i]["idPremises"].ToString(), out id))
                        {
                            ListOfId.Add(id);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch
                    {
                        return null;
                    }

                }
            }
            return TypeOfContact;
        }
        #endregion
    }
}
