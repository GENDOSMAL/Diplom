using Newtonsoft.Json;
using RepairFlat.Model;
using RepairFlatWPF.Model;
using System;
using System.Collections.Generic;
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
using static RepairFlatWPF.WorkWithOrder;

namespace RepairFlatWPF.UserControls.OrderWork
{

    public partial class CreateNewOrder : UserControl
    {
        #region Переменные
        public bool NewOrder = true;
        Guid? IdUser;
        Guid idAdress;
        Guid idContact;
        Guid TempidContact;
        Guid idOrder;
        List<Tuple<int, Guid>> ContactId = new List<Tuple<int, Guid>>();
        BaseWindow Window;
        #endregion

        #region Конструктор

        public CreateNewOrder(ref BaseWindow baseWindow, bool NewOrder = true,Guid idOrder=new Guid())
        {
            InitializeComponent();
            DateOfOrder.SelectedDate = DateTime.Now;
            Window = baseWindow;
            this.NewOrder = NewOrder;

            if (!NewOrder)
            {
                SelectClient.IsEnabled = false;
                OperationBTN.Content = "Редактировать";
                MakeOperationWithAdress.Content = "Редактировать";
                LoadingDataAboutContact(idOrder);
                this.idOrder = idOrder;
            }
            else
            {
                AllSumma.Text = "0";
            }
            foreach (var StatOfOrder in SomeEnums.StatusOfOrder)
            {
                StatusOfOrders.Items.Add(StatOfOrder);

            }
        }

        #endregion

        #region Обработка событий 

        private void SelectClient_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Выберите клиента");
            baseWindow.MakeOpen(new ClientWork.SelectClientUserControl(SomeEnums.TypeOfConrols.Window, ref baseWindow));
            baseWindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {
                SaveSomeData.MakeSomeOperation = false;
                if (SaveSomeData.SomeObject != null)
                {
                    var DataAboutUser = SaveSomeData.SomeObject as DescriptionOfUser;
                    SaveSomeData.SomeObject = null;
                    IdUser = DataAboutUser.idUser;
                    string female = DataAboutUser.Female == 1 ? "МУЖ" : "Жен";
                    ClientFIO.Text = $"{DataAboutUser.Lastname?.Trim()} {DataAboutUser.Name?.Substring(0, 1).ToUpper()}.{DataAboutUser.Patronymic?.Substring(0, 1).ToUpper()}. {DataAboutUser.Birstday.Value.ToString("dd.MM.yyyy") } {female}";
                    List<Guid> ContactId = new List<Guid>();
                    ConctactType.Items.Clear();
                    makeloadingListOfContact(IdUser);
                }
            }
        }

        public async void makeloadingListOfContact(Guid? idUser, Guid idTempContact = new Guid())
        {
            try
            {
                var InformationFromServer = await Task.Run(() => MakeDownloadByLink($"api/contact/getusercontact?idUser={idUser}"));
                ContactModel.ListOfUserContactInf listOfUserContactInf = JsonConvert.DeserializeObject<ContactModel.ListOfUserContactInf>(InformationFromServer.ToString());
                if (listOfUserContactInf.listOfContact != null)
                {
                    int num = 0;
                    foreach (var contact in listOfUserContactInf.listOfContact)
                    {
                        ComboBoxItem NewItem = new ComboBoxItem();
                        NewItem.Content = $"{contact.ValueTypeOfContact.Trim()} : {contact.Value.Trim()}";
                        NewItem.ToolTip = contact.Desctription;
                        ConctactType.Items.Add(NewItem);
                        ContactId.Add(Tuple.Create(num, contact.idContact));
                        num++;
                    }
                    if (idTempContact != null)
                    {
                        ConctactType.SelectedIndex = ContactId.Where(ee => ee.Item2 == idTempContact).Select(ee => ee.Item1).FirstOrDefault();
                    }
                }
                if (ConctactType.Items.Count == 0)
                {
                    ComboBoxItem NewItem = new ComboBoxItem();
                    NewItem.Content = $"Необходимо добавить данные о контатах";
                    ConctactType.Items.Add(NewItem);
                }
            }
            catch (Exception ex)
            {
                MakeSomeHelp.MSG(ex.ToString());
            }
        }

        public object MakeDownloadByLink(string UrlOfDownload)
        {
            return BaseWorkWithServer.CatchErrorWithGet(UrlOfDownload, "GET", nameof(MakeLoading), nameof(MakeDownloadByLink));
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            Window.Close();
        }

        private void ConctactType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ind = ConctactType.SelectedIndex;
            if (ind != -1)
            {
                idContact = ContactId.Where(ww => ww.Item1 == ind).Select(ee => ee.Item2).FirstOrDefault();
            }
        }

        private async void AddContactData_Click(object sender, RoutedEventArgs e)
        {
            if (IdUser != null)
            {
                BaseWindow baseWindow = new BaseWindow("Добавление контактной информации");
                baseWindow.MakeOpen(new AddContactUserConrol(IdUser, ref baseWindow));
                baseWindow.ShowDialog();
                if (SaveSomeData.MakeSomeOperation)
                {
                    SaveSomeData.MakeSomeOperation = false;
                    var dataAboutNewContact = SaveSomeData.SomeObject as ContactModel.InformationAboutContact;
                    SaveSomeData.SomeObject = null;
                    string Json = JsonConvert.SerializeObject(dataAboutNewContact);
                    string urlSend = "api/contact/create";
                    var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(OperationBTN_Click)));
                    var deserializedProduct = JsonConvert.DeserializeObject<BaseResult>(task.ToString());
                    ContactId.Clear();
                    ConctactType.Items.Clear();
                    if (!deserializedProduct.success)
                    {
                        MakeSomeHelp.MSG($"Произошла ошибка при работе {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
                    }
                    else
                    {
                        MakeSomeHelp.MSG("Данные о контакте добавлены!", MsgBoxImage: MessageBoxImage.Information);
                    }
                    makeloadingListOfContact(IdUser);
                }
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать клиента для добавления данных о контакте!", MsgBoxImage: MessageBoxImage.Error);
            }
        }

        private async void RedactContactData_Click(object sender, RoutedEventArgs e)
        {
            if (IdUser != null)
            {
                if (ConctactType.SelectedIndex != -1)
                {
                    var InformationFromServer = await Task.Run(() => MakeDownloadByLink($"api/contact/getusercontact?idUser={IdUser}"));
                    ContactModel.ListOfUserContactInf listOfUserContactInf = JsonConvert.DeserializeObject<ContactModel.ListOfUserContactInf>(InformationFromServer.ToString());
                    ContactModel.InformationAboutContact aboutContact = new ContactModel.InformationAboutContact();

                    if (listOfUserContactInf.listOfContact != null)
                    {
                        foreach (var contact in listOfUserContactInf.listOfContact)
                        {
                            if (contact.idContact == idContact)
                            {
                                aboutContact.idContact = contact.idContact;
                                aboutContact.idTypeOfContact = contact.idTypeOfContact;
                                aboutContact.idUser = contact.idUser;
                                aboutContact.Value = contact.Value.Trim();
                                aboutContact.Desctription = contact.Desctription.Trim();
                            }
                        }
                    }

                    BaseWindow baseWindow = new BaseWindow("Редактирование контактной информации");
                    baseWindow.MakeOpen(new AddContactUserConrol(IdUser, ref baseWindow, aboutContact));
                    baseWindow.ShowDialog();
                    if (SaveSomeData.MakeSomeOperation)
                    {
                        SaveSomeData.MakeSomeOperation = false;
                        var dataAboutNewContact = SaveSomeData.SomeObject as ContactModel.InformationAboutContact;
                        SaveSomeData.SomeObject = null;
                        string Json = JsonConvert.SerializeObject(dataAboutNewContact);
                        string urlSend = "api/contact/update";
                        var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(OperationBTN_Click)));
                        var deserializedProduct = JsonConvert.DeserializeObject<BaseResult>(task.ToString());

                        ContactId.Clear();
                        ConctactType.Items.Clear();
                        if (!deserializedProduct.success)
                        {
                            MakeSomeHelp.MSG($"Произошла ошикбка при работе {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
                        }
                        else
                        {

                            MakeSomeHelp.MSG("Данные о контакте обновлены!", MsgBoxImage: MessageBoxImage.Information);
                        }

                        makeloadingListOfContact(IdUser);
                    }
                }
                else
                {
                    MakeSomeHelp.MSG("Необходимо выбрать контакт для редактирования!", MsgBoxImage: MessageBoxImage.Error);
                }
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать клиента для редактирования данных о номере контакте!", MsgBoxImage: MessageBoxImage.Error);
            }
        }

        private async void OperationBTN_Click(object sender, RoutedEventArgs e)
        {
            if (CheckField())
            {
                if (NewOrder)
                {
                    idOrder = Guid.NewGuid();
                    BaseOrderInformation infOrder = new BaseOrderInformation
                    {
                        idAdress = idAdress,
                        Allsumma = 0,
                        DataStart = DateTime.Now,
                        Desc = Description.Text.Trim(),
                        idClient = IdUser,
                        idOrder = idOrder,
                        idWorkerMake = SaveSomeData.IdUser,
                        Status = StatusOfOrders.SelectedIndex,
                        MainContactID = idContact,

                    };
                    string Json = JsonConvert.SerializeObject(infOrder);
                    string urlSend = "api/order/create";
                    OperationBTN.Content = "Ожидайте...";
                    ReturnBtn.Content = "Ожидайте...";
                    ReturnBtn.IsEnabled = false;
                    OperationBTN.IsEnabled = false;
                    var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(OperationBTN_Click)));
                    var deserializedProduct = JsonConvert.DeserializeObject<BaseResult>(task.ToString());

                    if (!deserializedProduct.success)
                    {
                        MakeSomeHelp.MSG($"Произошла ошикбка при работе {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
                    }
                    else
                    {
                        MakeSomeHelp.MSG("Данные добавлены!", MsgBoxImage: MessageBoxImage.Information);
                    }
                    Window.Close();

                }
                else
                {
                    BaseOrderInformation infOrder = new BaseOrderInformation
                    {
                        idAdress = idAdress,
                        Allsumma = 0,
                        Desc = Description.Text.Trim(),
                        idClient = IdUser,
                        idOrder = idOrder,
                        idWorkerMake = SaveSomeData.IdUser,
                        Status = StatusOfOrders.SelectedIndex,
                        MainContactID = idContact,

                    };
                    string Json = JsonConvert.SerializeObject(infOrder);
                    string urlSend = "api/order/update";
                    OperationBTN.Content = "Ожидайте...";
                    ReturnBtn.Content = "Ожидайте...";
                    var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(OperationBTN_Click)));
                    var deserializedProduct = JsonConvert.DeserializeObject<BaseResult>(task.ToString());

                    if (!deserializedProduct.success)
                    {
                        MakeSomeHelp.MSG($"Произошла ошикбка при работе {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
                    }
                    else
                    {
                        MakeSomeHelp.MSG("Данные отредактированы!", MsgBoxImage: MessageBoxImage.Information);
                    }
                    Window.Close();
                }
            }
        }

        private void MakeOperationWithAdress_Click(object sender, RoutedEventArgs e)
        {
            if (NewOrder && idAdress == new Guid())
            {//Тут добавление данных об адресе
                BaseWindow baseWindow = new BaseWindow("Создание адресса");
                baseWindow.MakeOpen(new AditinalControl.AddInformationAboutAdress(Guid.NewGuid(), ref baseWindow));
                baseWindow.ShowDialog();
                if (SaveSomeData.MakeSomeOperation)
                {
                    MakeOperationWithAdress.Content = "Редактировать";
                    SaveSomeData.MakeSomeOperation = false;
                    var dataAboutAdress = SaveSomeData.SomeObject as ModelAdress.DataAboutAdress;
                    SaveSomeData.SomeObject = null;
                    if (dataAboutAdress != null)
                    {
                        idAdress = dataAboutAdress.idAdress;
                        FullAdress.Text = $"{dataAboutAdress.CityName} {dataAboutAdress.Street} {dataAboutAdress.House} {dataAboutAdress.Entrance} {dataAboutAdress.NumberOfDelen}";
                    }
                }
            }
            else
            {//Тут редактирование данных об адресе
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
                        FullAdress.Text = $"{dataAboutAdress.CityName} {dataAboutAdress.Street} {dataAboutAdress.House} {dataAboutAdress.Entrance} {dataAboutAdress.NumberOfDelen}";
                    }
                }
            }
        }

        #endregion

        #region  Прочие обработчики 
        private async void LoadingDataAboutContact(Guid idOrder)
        {
            var InformationFromServer = await Task.Run(() => MakeDownloadByLink($"api/order/dataforupdate?idOrder={idOrder}"));
            OrderDesc.DataForUpdate DataAboutOrder = JsonConvert.DeserializeObject<OrderDesc.DataForUpdate>(InformationFromServer.ToString());
            IdUser= DataAboutOrder.idUser;
            idAdress= DataAboutOrder.idAdress ?? default(Guid);
            TempidContact= DataAboutOrder.idContact ?? default(Guid);
            idContact= DataAboutOrder.idContact ?? default(Guid);
            DateOfOrder.SelectedDate = DataAboutOrder.DataStart;
            StatusOfOrders.SelectedIndex = DataAboutOrder.Status ?? default(int);
            AllSumma.Text = DataAboutOrder.AllSumma.ToString();
            Description.Text = DataAboutOrder.Desc;
            FullAdress.Text = DataAboutOrder.DataAboutAdress;
            ClientFIO.Text = DataAboutOrder.FIOClient;
            makeloadingListOfContact(IdUser, TempidContact);
        }


        private bool CheckField()
        {
            if (StatusOfOrders.SelectedIndex == -1)
            {
                MakeSomeHelp.MSG("Необходимо указать статус заказа!", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }

            if (IdUser == new Guid())
            {
                MakeSomeHelp.MSG("Необходимо указать данные о клиенте!", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (idAdress == new Guid())
            {
                MakeSomeHelp.MSG("Необходимо указать данные об адресе!", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (ConctactType.SelectedIndex == -1)
            {
                MakeSomeHelp.MSG("Необходимо выбрать основной способ связи!", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        #endregion
    }
}
