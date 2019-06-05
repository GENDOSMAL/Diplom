using Newtonsoft.Json;
using RepairFlat.Model;
using RepairFlatWPF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static RepairFlatWPF.Model.WorkerDescriptiom;

namespace RepairFlatWPF.UserControls.WorkerInformation.KadrWork
{
    public partial class CreateNewWorker : UserControl
    {
        string TypeOfUser;
        BaseWindow window;
        bool NewWorker = true;
        Guid idUser = Guid.NewGuid();
        Guid idAdress;
        DataTable DataAboutContact = new DataTable("ContactTable");
        List<Tuple<int, Guid, Guid>> ContactDataGuidByNamber;
        List<Guid?> idOfDelete = new List<Guid?>();

        public CreateNewWorker(ref BaseWindow baseWindow, Guid idUser = new Guid())
        {
            InitializeComponent();
            window = baseWindow;
            MakeAllPreparationWithData();
            foreach (string Type in SomeEnums.FemaleType)
            {
                Female.Items.Add(Type);
            }

            if (idUser != new Guid())
            {
                this.NewWorker = false;
                AddBtn.Content = "Редактировать";
                this.idUser = idUser;
                makeloadingListOfContact();
                
                WorkWithAdress.Content = "Редактировать";
                makeotherSelectDataAsync();
            }
        }



        private void MakeAllPreparationWithData()
        {
            DataAboutContact = new DataTable("ContactTable");
            DataGrid.ItemsSource = DataAboutContact.DefaultView;
            foreach (string NameOfColumn in SomeEnums.ContactTableDesc)
            {
                DataAboutContact.Columns.Add(NameOfColumn);
            }
            
            ContactDataGuidByNamber = new List<Tuple<int, Guid, Guid>>();
        }

        #region Работа с контактами
        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Добавление контактной информации");
            baseWindow.MakeOpen(new AddContactUserConrol(idUser, ref baseWindow));
            baseWindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {

                var contactInformation = SaveSomeData.SomeObject as ContactModel.InformationAboutContact;
                SaveSomeData.SomeObject = null;
                SaveSomeData.MakeSomeOperation = false;
                int Number = DataAboutContact.Rows.Count == 0 ? 1 : MakeSomeHelp.SelectMaxValueinColumn(ref DataAboutContact, SomeEnums.ContactTableDesc[0]);
                DataRow newContact = DataAboutContact.NewRow();
                newContact[0] = Number;
                newContact[1] = contactInformation.NameOfValue;
                newContact[2] = contactInformation.Value;
                newContact[3] = contactInformation.Desctription;
                DataAboutContact.Rows.Add(newContact);
                ContactDataGuidByNamber.Add(new Tuple<int, Guid, Guid>(Number, contactInformation.idContact, contactInformation.idTypeOfContact ?? default(Guid)));
            }
        }

        private void RedactContact_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    ContactModel.InformationAboutContact aboutContact = new ContactModel.InformationAboutContact();
                    for (int i = 0; i < DataAboutContact.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(DataAboutContact.Rows[i][0].ToString()) == numberOfRows)
                        {
                            Guid idContact = ContactDataGuidByNamber.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                            Guid idType = ContactDataGuidByNamber.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item3).First();

                            aboutContact = new ContactModel.InformationAboutContact
                            {
                                idContact = idContact,
                                idTypeOfContact = idType,
                                idUser = idUser,
                                Value = DataAboutContact.Rows[i][2].ToString(),
                                Desctription = DataAboutContact.Rows[i][3].ToString(),
                                NameOfValue = DataAboutContact.Rows[i][1].ToString(),
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
                        for (int i = 0; i < DataAboutContact.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(DataAboutContact.Rows[i][0].ToString()) == dataUpdated.Number)
                            {
                                DataAboutContact.Rows[i][1] = dataUpdated.NameOfValue;
                                DataAboutContact.Rows[i][2] = dataUpdated.Value;
                                DataAboutContact.Rows[i][3] = dataUpdated.Desctription;
                                var data = ContactDataGuidByNamber.Single(e1 => e1.Item1 == dataUpdated.Number);
                                ContactDataGuidByNamber.Remove(data);
                                ContactDataGuidByNamber.Add(new Tuple<int, Guid, Guid>(dataUpdated.Number, dataUpdated.idContact, dataUpdated.idTypeOfContact ?? default));
                            }
                        }
                    }

                }
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать контакт для редактирования", MsgBoxImage: MessageBoxImage.Error);
            }
        }

        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    if (NewWorker)
                    {
                        for (int i = 0; i < DataAboutContact.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(DataAboutContact.Rows[i][0].ToString()) == numberOfRows)
                            {
                                DataAboutContact.Rows[i].Delete();
                                var data = ContactDataGuidByNamber.Single(e1 => e1.Item1 == numberOfRows);
                                ContactDataGuidByNamber.Remove(data);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < DataAboutContact.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(DataAboutContact.Rows[i][0].ToString()) == numberOfRows)
                            {
                                DataAboutContact.Rows[i].Delete();
                                var data = ContactDataGuidByNamber.Single(e1 => e1.Item1 == numberOfRows);
                                idOfDelete.Add(data.Item2);
                                ContactDataGuidByNamber.Remove(data);
                            }
                        }
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать строку для удаления", MsgBoxImage: MessageBoxImage.Error);
            }
        }

        private async void makeotherSelectDataAsync()
        {
            var InformationFromServer = await Task.Run(() => MakeDownloadByLink($"api/worker/getData?idWorker={idUser}"));
            var dataAbWorker= JsonConvert.DeserializeObject<MakeNewWorker>(InformationFromServer.ToString());
            if (dataAbWorker != null)
            {
                Name.Text = dataAbWorker.Name;
                Famil.Text = dataAbWorker.Lastname;
                Patronymic.Text = dataAbWorker.Patronymic;
                Pasport.Text = dataAbWorker.Pasport;
                Female.SelectedItem = SomeEnums.FemaleType[dataAbWorker.Female?? default];
                DateOfBirsd.SelectedDate = dataAbWorker.Birstday;
                Adress.Text = dataAbWorker.DescOfAdress;
                idAdress = dataAbWorker.idAdress;
                TypeOfUser = dataAbWorker.TypeOfUser;
            }

        }

        //Загрузка данных о контактах
        public async void makeloadingListOfContact()
        {

            var InformationFromServer = await Task.Run(() => MakeDownloadByLink($"api/contact/getusercontact?idUser={idUser}"));
            ContactModel.ListOfUserContactInf listOfUserContactInf = JsonConvert.DeserializeObject<ContactModel.ListOfUserContactInf>(InformationFromServer.ToString());
            if (listOfUserContactInf != null)
            {
                int number = 1;
                foreach (var contact in listOfUserContactInf.listOfContact)
                {
                    DataRow NewContactData = DataAboutContact.NewRow();
                    NewContactData[0] = number;
                    NewContactData[1] = contact.ValueTypeOfContact?.Trim();
                    NewContactData[2] = contact.Value?.Trim();
                    NewContactData[3] = contact.Desctription?.Trim();
                    DataAboutContact.Rows.Add(NewContactData);
                    ContactDataGuidByNamber.Add(new Tuple<int, Guid, Guid>(number, contact.idContact, contact.idTypeOfContact ?? default(Guid)));
                    number++;
                }

            }
            DataGrid.ItemsSource = DataAboutContact.DefaultView;
        }

        #endregion

        //Добавление работника
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                if (NewWorker)
                {//Если добавление
                    MakeNewWorker newWorker = new MakeNewWorker();
                    newWorker.InformatioAboutContact = new PersonDesctiption.InformationAboutContact();
                    newWorker.InformatioAboutContact.ListOfContact = new List<ContactModel.InformationAboutContact>();
                    for (int i = 0; i < DataAboutContact.Rows.Count; i++)
                    {
                        var idOfCont = ContactDataGuidByNamber.Where(ee => ee.Item1 == Convert.ToInt32(DataAboutContact.Rows[i][0].ToString()));
                        ContactModel.InformationAboutContact contact = new ContactModel.InformationAboutContact();
                        contact.Desctription = DataAboutContact.Rows[i][3].ToString()?.Trim();
                        contact.Value = DataAboutContact.Rows[i][2].ToString()?.Trim();
                        contact.idUser = idUser;
                        contact.idTypeOfContact = idOfCont.First().Item3;
                        contact.idContact = idOfCont.First().Item2;
                        contact.DateAdd = DateTime.Now;
                        newWorker.InformatioAboutContact.ListOfContact.Add(contact);
                    }
                    newWorker.Birstday = DateOfBirsd.SelectedDate.Value;
                    newWorker.Female = Female.SelectedIndex;
                    newWorker.idAdress = idAdress;
                    newWorker.idUser = idUser;
                    newWorker.Lastname = Famil.Text?.Trim();
                    newWorker.Name = Name.Text?.Trim();
                    newWorker.Patronymic = Patronymic.Text?.Trim();
                    newWorker.Pasport = Pasport.Text?.Trim();
                    newWorker.TypeOfUser = SomeEnums.TypeOfUser.KD.ToString();
                    string Json = JsonConvert.SerializeObject(newWorker);
                    string urlSend = "api/worker/createorupdate/worker";
                    MakeSomeHelp.UpdloadDataToServer(urlSend, Json);
                    window.Close();
                }
                else
                {//Если редактирование
                    MakeNewWorker newWorker = new MakeNewWorker();
                    newWorker.InformatioAboutContact = new PersonDesctiption.InformationAboutContact();
                    newWorker.InformatioAboutContact.ListOfContact = new List<ContactModel.InformationAboutContact>();
                    for (int i = 0; i < DataAboutContact.Rows.Count; i++)
                    {
                        var idOfCont = ContactDataGuidByNamber.Where(ee => ee.Item1 == Convert.ToInt32(DataAboutContact.Rows[i][0].ToString()));
                        ContactModel.InformationAboutContact contact = new ContactModel.InformationAboutContact();
                        contact.Desctription = DataAboutContact.Rows[i][3].ToString()?.Trim();
                        contact.Value = DataAboutContact.Rows[i][2].ToString()?.Trim();
                        contact.idUser = idUser;
                        contact.idTypeOfContact = idOfCont.First().Item3;
                        contact.idContact = idOfCont.First().Item2;
                        contact.DateAdd = DateTime.Now;
                        newWorker.InformatioAboutContact.ListOfContact.Add(contact);
                    }
                    newWorker.Birstday = DateOfBirsd.SelectedDate.Value;
                    newWorker.Female = Female.SelectedIndex;
                    newWorker.idAdress = idAdress;
                    newWorker.idUser = idUser;
                    newWorker.Lastname = Famil.Text?.Trim();
                    newWorker.Name = Name.Text?.Trim();
                    newWorker.Patronymic = Patronymic.Text?.Trim();
                    newWorker.Pasport = Pasport.Text?.Trim();
                    newWorker.TypeOfUser = TypeOfUser;
                    if (idOfDelete.Count != 0)
                    {
                        newWorker.InformatioAboutContact.ListForDelete = new List<Guid?>();
                        newWorker.InformatioAboutContact.ListForDelete = idOfDelete;
                    }

                    string Json = JsonConvert.SerializeObject(newWorker);
                    string urlSend = "api/worker/createorupdate/worker";
                    MakeSomeHelp.UpdloadDataToServer(urlSend, Json);
                    window.Close();
                }
            }
           
        }

        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }


        #region Работа с адресом
        private void WorkWithAdress_Click(object sender, RoutedEventArgs e)
        {
            if (NewWorker)
            {
                this.idAdress = Guid.NewGuid();
                BaseWindow baseWindow = new BaseWindow("Создание адресса");
                baseWindow.MakeOpen(new AditinalControl.AddInformationAboutAdress(idAdress, ref baseWindow));
                baseWindow.ShowDialog();
                if (SaveSomeData.MakeSomeOperation)
                {
                    SaveSomeData.MakeSomeOperation = false;
                    var dataAboutAdress = SaveSomeData.SomeObject as ModelAdress.DataAboutAdress;
                    SaveSomeData.SomeObject = null;
                    if (dataAboutAdress != null)
                    {
                        idAdress = dataAboutAdress.idAdress;
                        Adress.Text = $"{dataAboutAdress.CityName} {dataAboutAdress.Street} {dataAboutAdress.House} {dataAboutAdress.Entrance} {dataAboutAdress.NumberOfDelen}";
                    }
                }
            }
            else
            {
                BaseWindow baseWindow = new BaseWindow("Обновление адресса");
                baseWindow.MakeOpen(new AditinalControl.AddInformationAboutAdress(idAdress, ref baseWindow, Redact: true));
                baseWindow.ShowDialog();

                if (SaveSomeData.MakeSomeOperation)
                {
                    SaveSomeData.MakeSomeOperation = false;
                    var dataAboutAdress = SaveSomeData.SomeObject as ModelAdress.DataAboutAdress;
                    SaveSomeData.SomeObject = null;
                    if (dataAboutAdress != null)
                    {
                        Adress.Text = $"{dataAboutAdress.CityName} {dataAboutAdress.Street} {dataAboutAdress.House} {dataAboutAdress.Entrance} {dataAboutAdress.NumberOfDelen}";
                    }
                }
            }
        }

        #endregion

        public object MakeDownloadByLink(string UrlOfDownload)
        {
            return BaseWorkWithServer.CatchErrorWithGet(UrlOfDownload, "GET", nameof(MakeLoading), nameof(MakeDownloadByLink));
        }

        bool CheckData()
        {
            if (string.IsNullOrEmpty(Name.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать значение имени", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }

            if (string.IsNullOrEmpty(Famil.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать значение фамилии", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (string.IsNullOrEmpty(Patronymic.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать значение отчества", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (string.IsNullOrEmpty(Pasport.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать значение паспорта", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (Female.SelectedIndex==-1)
            {
                MakeSomeHelp.MSG("Необходимо указать значение пола работника", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (DateOfBirsd.SelectedDate == null) 
            {
                MakeSomeHelp.MSG("Необходимо указать дату рождения", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (idAdress == new Guid())
            {
                MakeSomeHelp.MSG("Необходимо место проживания", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (DataAboutContact.Rows.Count==0)
            {
                if (MakeSomeHelp.MSG("Не указана контакная информация для работника. Вы действительно не хотите ее указывать?", MsgBoxImage: MessageBoxImage.Question,MsgBoxButton:MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            return true;
        }

    }
}
