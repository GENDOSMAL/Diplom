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
using static RepairFlat.Model.PersonDesctiption;

namespace RepairFlatWPF.UserControls
{
    /// <summary>
    /// Interaction logic for AddUserControl.xaml
    /// </summary>
    public partial class AddUserControl : UserControl
    {
        #region Переменные 
        Guid? idUser;
        bool NewData = true;
        DataTable TableOfContactInformation;
        BaseWindow window;
        /// <summary>
        /// 1- номер в таблице
        /// 2- уникальный номер контакта
        /// 3- уникальный номер типа контакта
        /// </summary>
        List<Tuple<int, Guid, Guid?>> IdContactAndTypeInTable = new List<Tuple<int, Guid, Guid?>>();

        #endregion

        #region Констурктор и обработчик
        public AddUserControl(Guid? idUser, ref BaseWindow baseWindow, object InformationAboutClient = null)
        {
            InitializeComponent();
            this.idUser = idUser;
            window = baseWindow;
            foreach (string Type in SomeEnums.FemaleType)
            {
                Female.Items.Add(Type);
            }

            TableOfContactInformation = new DataTable();

            foreach (string NameOfColumn in SomeEnums.ContactTableDesc)
            {
                TableOfContactInformation.Columns.Add(NameOfColumn);
            }
            DataGrid.ItemsSource = TableOfContactInformation.DefaultView;

            if (InformationAboutClient != null)
            {
                AddBtn.Content = "Редактировать";
                NewData = false;
                makeloadingListOfContact();
            }
            else
            {
                idUser = Guid.NewGuid();
            }
        }

        public async void makeloadingListOfContact()
        {
            var InformationFromServer = await Task.Run(() => MakeDownloadByLink($"api/contact/getusercontact?idUser={idUser}"));
            ContactModel.ListOfUserContactInf listOfUserContactInf = JsonConvert.DeserializeObject<ContactModel.ListOfUserContactInf>(InformationFromServer.ToString());
            if (listOfUserContactInf != null)
            {
                int number = 0;
                foreach (var contact in listOfUserContactInf.listOfContact)
                {
                    DataRow NewContactData = TableOfContactInformation.NewRow();
                    NewContactData[0] = number;
                    NewContactData[1] = contact.ValueTypeOfContact;
                    NewContactData[2] = contact.Value;
                    NewContactData[3] = contact.Desctription;
                    IdContactAndTypeInTable.Add(new Tuple<int, Guid, Guid?>(number, contact.idContact, contact.idTypeOfContact));
                    number++;
                }
            }

        }

        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                if (NewData)
                {
                    //Тут добавление
                    List<ContactModel.InformationAboutContact> ContactInformatio = new List<ContactModel.InformationAboutContact>();
                    if (TableOfContactInformation.Rows.Count != 0)
                    {
                        for (int i = 0; i < TableOfContactInformation.Rows.Count; i++)
                        {
                            Guid idContact = IdContactAndTypeInTable.Where(e2 => e2.Item1 == Convert.ToInt32(TableOfContactInformation.Rows[i][0])).Select(e1 => e1.Item2).First();
                            Guid? idType = IdContactAndTypeInTable.Where(e2 => e2.Item1 == Convert.ToInt32(TableOfContactInformation.Rows[i][0])).Select(e1 => e1.Item3).First();
                            ContactModel.InformationAboutContact contact = new ContactModel.InformationAboutContact()
                            {
                                DateAdd = DateTime.Now,
                                Desctription = TableOfContactInformation.Rows[i][3].ToString(),
                                idContact = idContact,
                                idTypeOfContact = idType,
                                idUser = idUser,
                                Value = TableOfContactInformation.Rows[i][2].ToString()
                            };
                            ContactInformatio.Add(contact);
                        }
                    }
                    else
                    {
                        ContactInformatio = null;
                    }
                    CreateNewClient createNewClient = new CreateNewClient
                    {
                        idUser = idUser,
                        Birstday = DateOfBirsd.SelectedDate.Value,
                        Desc = Description.Text.Trim(),
                        Female = Female.SelectedIndex + 1,
                        Lastname = Famil.Text.Trim(),
                        Name = Name.Text.Trim(),
                        Pasport = Pasport.Text.Trim(),
                        Patronymic = Patronymic.Text.Trim(),
                        TypeOfUser = SomeEnums.TypeOfUser.Cl.ToString(),
                        ListOfContact = ContactInformatio
                    };
                    string Json = JsonConvert.SerializeObject(createNewClient);
                    string urlSend = "api/user/create";
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
                    //Тут обновление
                }
            }
        }

        private void AddElement_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Добавление контактной информации");
            baseWindow.MakeOpen(new AddContactUserConrol(idUser, ref baseWindow));
            baseWindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {
                
                var contactInformation = SaveSomeData.SomeObject as ContactModel.InformationAboutContact;
                SaveSomeData.SomeObject = null;
                SaveSomeData.MakeSomeOperation = false;
                int Number = TableOfContactInformation.Rows.Count == 0 ? 1 : MakeSomeHelp.SelectMaxValueinColumn(ref TableOfContactInformation, SomeEnums.ContactTableDesc[0]);
                DataRow newContact = TableOfContactInformation.NewRow();
                newContact[0] = Number;
                newContact[1] = contactInformation.NameOfValue;
                newContact[2] = contactInformation.Value;
                newContact[3] = contactInformation.Desctription;
                TableOfContactInformation.Rows.Add(newContact);
                IdContactAndTypeInTable.Add(new Tuple<int, Guid, Guid?>(Number, contactInformation.idContact, contactInformation.idTypeOfContact));
            }
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
                    if (NewData)
                    {
                        ContactModel.InformationAboutContact aboutContact = new ContactModel.InformationAboutContact();
                        for (int i = 0; i < TableOfContactInformation.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(TableOfContactInformation.Rows[i][0].ToString()) == numberOfRows)
                            {
                                Guid idContact = IdContactAndTypeInTable.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                                Guid? idType = IdContactAndTypeInTable.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item3).First();

                                aboutContact = new ContactModel.InformationAboutContact
                                {
                                    idContact = idContact,
                                    idTypeOfContact = idType,
                                    idUser = idUser,
                                    Value = TableOfContactInformation.Rows[i][2].ToString(),
                                    Desctription = TableOfContactInformation.Rows[i][3].ToString(),
                                    NameOfValue = TableOfContactInformation.Rows[i][1].ToString(),
                                    Number = numberOfRows
                                };

                            }
                        }

                        BaseWindow baseWindow = new BaseWindow("Редактирование контактной информации");
                        baseWindow.MakeOpen(new AddContactUserConrol(idUser, ref baseWindow, aboutContact));
                        baseWindow.ShowDialog();
                        if (SaveSomeData.MakeSomeOperation)
                        {
                            SaveSomeData.MakeSomeOperation = false;
                            ContactModel.InformationAboutContact dataUpdated = SaveSomeData.SomeObject as ContactModel.InformationAboutContact;
                            SaveSomeData.SomeObject = null;
                            for (int i = 0; i < TableOfContactInformation.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(TableOfContactInformation.Rows[i][0].ToString()) == dataUpdated.Number)
                                {
                                    TableOfContactInformation.Rows[i][1] = dataUpdated.NameOfValue;
                                    TableOfContactInformation.Rows[i][2] = dataUpdated.Value;
                                    TableOfContactInformation.Rows[i][3] = dataUpdated.Desctription;
                                    var data = IdContactAndTypeInTable.Single(e1 => e1.Item1 == dataUpdated.Number);
                                    IdContactAndTypeInTable.Remove(data);
                                    IdContactAndTypeInTable.Add(new Tuple<int, Guid, Guid?>(dataUpdated.Number, dataUpdated.idContact, dataUpdated.idTypeOfContact));
                                }
                            }
                        }
                    }
                    else
                    {
                        //Если обновление данных о клиенте
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать строку для редактирования", MsgBoxImage: MessageBoxImage.Error);
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
                    if (NewData)
                    {
                        for (int i = 0; i < TableOfContactInformation.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(TableOfContactInformation.Rows[i][0].ToString()) == numberOfRows)
                            {
                                TableOfContactInformation.Rows[i].Delete();
                                var data = IdContactAndTypeInTable.Single(e1 => e1.Item1 == numberOfRows);
                                IdContactAndTypeInTable.Remove(data);
                            }
                        }
                    }
                    else
                    {
                        //Если обновление данных о клиенте
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать строку для редактирования", MsgBoxImage: MessageBoxImage.Error);
            }

        }

        #endregion

        #region Прочие обработки

        private bool CheckFields()
        {
            if (string.IsNullOrEmpty(Name.Text.Trim()))
            {
                MakeSomeHelp.MSG("Укажите данные о имени клиента", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(Famil.Text.Trim()))
            {
                MakeSomeHelp.MSG("Укажите данные о фамилии клиента", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(Patronymic.Text.Trim()))
            {
                MakeSomeHelp.MSG("Укажите данные об отчестве клиента", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(Pasport.Text.Trim()))
            {
                MakeSomeHelp.MSG("Укажите данные об паспорте клиента", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (Female.SelectedIndex == -1)
            {
                MakeSomeHelp.MSG("Укажите данные об половой принаждежности клиента", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (!DateOfBirsd.SelectedDate.HasValue)
            {
                MakeSomeHelp.MSG("Укажите данные об дате рождения", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        #endregion

        public object MakeDownloadByLink(string UrlOfDownload)
        {
            return BaseWorkWithServer.CatchErrorWithGet(UrlOfDownload, "GET", nameof(MakeLoading), nameof(MakeDownloadByLink));
        }

    }
}
