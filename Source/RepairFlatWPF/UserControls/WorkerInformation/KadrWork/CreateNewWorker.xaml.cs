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

namespace RepairFlatWPF.UserControls.WorkerInformation.KadrWork
{
    public partial class CreateNewWorker : UserControl
    {
        BaseWindow window;
        bool NewWorker = true;
        Guid idUser = Guid.NewGuid();
        Guid idAdress;
        DataTable DataAboutContact = new DataTable("ContactTable");
        List<Tuple<int, Guid, Guid>> ContactDataGuidByNamber;

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
                AddAdress.IsEnabled = false;
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
                        //Если обновление данных о клиенте
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать строку для удаления", MsgBoxImage: MessageBoxImage.Error);
            }
        }

        //Загрузка данных о контактах
        public async void makeloadingListOfContact()
        {

            var InformationFromServer = await Task.Run(() => MakeDownloadByLink($"api/contact/getusercontact?idUser={idUser}"));
            ContactModel.ListOfUserContactInf listOfUserContactInf = JsonConvert.DeserializeObject<ContactModel.ListOfUserContactInf>(InformationFromServer.ToString());
            if (listOfUserContactInf != null)
            {
                int number = 0;
                foreach (var contact in listOfUserContactInf.listOfContact)
                {
                    DataRow NewContactData = DataAboutContact.NewRow();
                    NewContactData[0] = number;
                    NewContactData[1] = contact.ValueTypeOfContact;
                    NewContactData[2] = contact.Value;
                    NewContactData[3] = contact.Desctription;
                    ContactDataGuidByNamber.Add(new Tuple<int, Guid, Guid>(number, contact.idContact, contact.idTypeOfContact ?? default(Guid)));
                    number++;
                }
            }

        }

        #endregion

        //Добавление работника
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NewWorker)
            {//Если добавление

            }
            else
            {//Если редактирование

            }
        }

        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }


        #region Работа с адресом
        private void RedactAdress_Click(object sender, RoutedEventArgs e)
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

        private void AddAdress_Click(object sender, RoutedEventArgs e)
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

        #endregion

        public object MakeDownloadByLink(string UrlOfDownload)
        {
            return BaseWorkWithServer.CatchErrorWithGet(UrlOfDownload, "GET", nameof(MakeLoading), nameof(MakeDownloadByLink));
        }
    }
}
