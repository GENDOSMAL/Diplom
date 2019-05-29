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

    public partial class MakeNewOrderUserControl : UserControl
    {
        public bool NewOrder = true;
        Guid? IdUser;
        Guid idAdress;
        Guid idContact;
        Guid idOrder;
        List<Guid> ContactId = new List<Guid>();
        BaseWindow Window;
        public MakeNewOrderUserControl(ref BaseWindow baseWindow, bool NewOrder = true)
        {
            InitializeComponent();
            DateOfOrder.SelectedDate = DateTime.Now;
            Window = baseWindow;
            this.NewOrder = NewOrder;
            
            if (!NewOrder)
            {
                if (SaveSomeData.MakeSomeOperation)
                {
                    SaveSomeData.MakeSomeOperation = false;
                    var DataAboutOrder = SaveSomeData.SomeObject as BaseOrderInformation;
                    SaveSomeData.SomeObject = false;
                    if (DataAboutOrder != null)
                    {
                        IdUser = DataAboutOrder.idClient;
                        idAdress = DataAboutOrder.idAdress;
                        idOrder = DataAboutOrder.idOrder;
                        idContact = DataAboutOrder.MainContactID;
                        AllSumma.Text = DataAboutOrder.Allsumma.ToString();
                        StatusOfOrders.SelectedIndex = DataAboutOrder.Status;
                        Description.Text = DataAboutOrder.description;
                    }
                }
                SelectClient.IsEnabled = false;
                CreateNewOrder.Content = "Редактировать данные о заказе";
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

        private async void CreateNewOrder_Click(object sender, RoutedEventArgs e)
        {
            idOrder = Guid.NewGuid();
            if (CheckField())
            {
                if (NewOrder)
                {
                    //Тут добавление
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
                    CreateNewOrder.Content= "Ожидайте...";
                    ReturnBtn.Content= "Ожидайте...";
                    var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(CreateNewOrder_Click)));
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
                        DataStart = DateTime.Now,
                        Desc = Description.Text.Trim(),
                        idClient = IdUser,
                        idOrder = idOrder,
                        idWorkerMake = SaveSomeData.IdUser,
                        Status = StatusOfOrders.SelectedIndex,
                        MainContactID = idContact,

                    };
                    string Json = JsonConvert.SerializeObject(infOrder);
                    string urlSend = "api/order/update";
                    CreateNewOrder.Content = "Ожидайте...";
                    ReturnBtn.Content = "Ожидайте...";
                    var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(CreateNewOrder_Click)));
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
            }



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

        private void SelectClient_Click(object sender, RoutedEventArgs e)
        {
            //TODO Выбор клиента
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
                    ClientFIO.Text = $"{DataAboutUser.Lastname.Trim()} {DataAboutUser.Name.Substring(0, 1).ToUpper()}.{DataAboutUser.Patronymic.Substring(0, 1).ToUpper()}. {DataAboutUser.Birstday.Value.ToString("dd.MM.yyyy") } {female}";
                    makeloadingListOfContact(IdUser);
                }
            }
        }

        public async void makeloadingListOfContact(Guid? idUser)
        {
            try
            {
                var InformationFromServer = await Task.Run(() => MakeDownloadByLink($"api/contact/getusercontact?idUser={idUser}"));
                ContactModel.ListOfUserContactInf listOfUserContactInf = JsonConvert.DeserializeObject<ContactModel.ListOfUserContactInf>(InformationFromServer.ToString());
                if (listOfUserContactInf.listOfContact != null)
                {
                    foreach (var contact in listOfUserContactInf.listOfContact)
                    {
                        ComboBoxItem NewItem = new ComboBoxItem();
                        NewItem.Content = $"{contact.ValueTypeOfContact.Trim()} : {contact.Value.Trim()}:{contact.idContact.ToString()}";
                        NewItem.ToolTip = contact.Desctription;
                        ConctactType.Items.Add(NewItem);
                        ContactId.Add(contact.idContact);
                    }
                }
                else
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

        private void AddAdress_Click(object sender, RoutedEventArgs e)
        {
            //TODO Добавление Адреса
            this.idAdress = Guid.NewGuid();
            BaseWindow baseWindow = new BaseWindow("Создание адресса");
            baseWindow.MakeOpen(new AditinalControl.AddInformationAboutAdress(idAdress, ref baseWindow));
            baseWindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {
                SaveSomeData.MakeSomeOperation = false;
                var dataAboutAdress = SaveSomeData.SomeObject as ModelAdress.DataAboutAdress;
                if (dataAboutAdress != null)
                {
                    idAdress = dataAboutAdress.idAdress;
                    FullAdress.Text = $"{dataAboutAdress.CiryName} {dataAboutAdress.Street} {dataAboutAdress.House} {dataAboutAdress.Entrance} {dataAboutAdress.NumberOfDelen}";
                }
            }
        }


        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            //Возврат назад
            Window.Close();
        }


        private void ChangeClient_Click(object sender, RoutedEventArgs e)
        {
            //BaseWindow baseWindow = new BaseWindow("Обновление данных");
            //baseWindow.MakeOpen(new AddUserControl(IdUser, ref baseWindow));
            //baseWindow.ShowDialog();
            MakeSomeHelp.MSG("Не реализовано!", MsgBoxImage: MessageBoxImage.Error);

        }

        private void ChangeAdress_Click(object sender, RoutedEventArgs e)
        {

            MakeSomeHelp.MSG("Не реализовано!", MsgBoxImage: MessageBoxImage.Error);
        }

        private void ConctactType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ind = ConctactType.SelectedIndex;
            if (ind != -1)
            {
                idContact = ContactId[ind];
                MessageBox.Show(ContactId[ind].ToString());
            }
           
        }

        private void AddContactData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RedactContactData_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
