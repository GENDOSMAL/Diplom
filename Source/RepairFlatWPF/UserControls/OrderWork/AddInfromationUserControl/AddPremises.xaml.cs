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

namespace RepairFlatWPF.UserControls.OrderWork.AddInfromationUserControl
{
    /// <summary>
    /// Interaction logic for AddPremises.xaml
    /// </summary>
    public partial class AddPremises : UserControl
    {
        #region Переменные
        bool NewData = true;
        Guid idOrder;
        Guid idPremises;
        List<Guid> ListOfId;
        List<string> ListOfTooltlip;
        DataTable DataAboutElement;
        bool NotElement = false;
        double LenghtData, HeightData, WidthData;

        #endregion

        #region Обработка событий и конструктор
        public AddPremises(Guid idOrder, object InformatioAboutPremises = null)
        {
            InitializeComponent();
            this.idOrder = idOrder;
            if (InformatioAboutPremises != null)
            {
                NewData = false;
                AddBtn.Content = "Редактировать";
            }
            var ListOfPremisesType = MakeListOfTypeOfPremises();
            if (ListOfPremisesType == null)
            {
                //Закрыть окно
                MakeSomeHelp.MSG("Необходимо обратиться к администратору либо перезагрузить систему", MsgBoxImage: MessageBoxImage.Error);
            }
            else
            {
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

        }

        private void AddElement_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void RedactElement_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                BaseWindow baseWindow = new BaseWindow(new AddElementOfPremises(idPremises,index), "Редактирование данных об элементах помщений");
                try
                {
                    baseWindow.ShowDialog();
                }
                catch
                {
                    baseWindow.Close();
                }
            }
            else
            {
                MakeSomeHelp.MSG("Выберите элемент для редактирования!", MsgBoxImage: MessageBoxImage.Error);
            }
        }

        private void DeleteElement_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {

            }
            else
            {
                MakeSomeHelp.MSG("Выберите элемент для удаления!", MsgBoxImage: MessageBoxImage.Error);
            }
        }

        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.CloseBaseWindow();
        }

        private void AddElementof_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow(new AddElementOfPremises(idPremises), "Добавление данных об элементах помщений");
            try
            {
                baseWindow.ShowDialog();
            }
            catch
            {
                baseWindow.Close();
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NotElement)
            {
                if (CheckFields())
                {
                    if (NewData)
                    {
                        //Тут если данные новые
                    }
                    else
                    {
                        //Тут если данные обновляются
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо загрузить данные о типах помщений в справочную информацию",MsgBoxImage:MessageBoxImage.Error);
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
                MakeSomeHelp.MSG("Необходимо указать длну", MsgBoxImage: MessageBoxImage.Error);
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
                if(MakeSomeHelp.MSG("Вы действительно хотите не добавлять данные об элементах помешения?", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
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
