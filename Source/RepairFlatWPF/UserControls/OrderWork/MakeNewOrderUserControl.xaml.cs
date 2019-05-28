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

namespace RepairFlatWPF.UserControls.OrderWork
{

    public partial class MakeNewOrderUserControl : UserControl
    {
        public bool NewOrder = true;
        Guid? IdUser;
        Guid idAdress;
        Guid idContact;
        Guid idOrder;
        List<Guid> ContactId=new List<Guid>();
        BaseWindow Window;
        public MakeNewOrderUserControl(ref BaseWindow baseWindow, bool NewOrder = true)
        {
            InitializeComponent();
            Window = baseWindow;
            this.NewOrder = NewOrder;
            if (!NewOrder)
            {
                SelectClient.IsEnabled = false;
            }
            foreach (var StatOfOrder in SomeEnums.StatusOfOrder)
            {
                StatusOfOrders.Items.Add(StatOfOrder);
            }
        }

        private void CreateNewOrder_Click(object sender, RoutedEventArgs e)
        {
            idOrder = Guid.NewGuid();
            //TODO Создание нового заказа на сервере
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
                if (listOfUserContactInf.listOfContact != null )
                {
                    foreach (var contact in listOfUserContactInf.listOfContact)
                    {
                        ComboBoxItem NewItem = new ComboBoxItem();
                        NewItem.Content = $"{contact.ValueTypeOfContact.Trim()} : {contact.Value.Trim()}";
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
            catch(Exception ex)
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
                if (dataAboutAdress!=null)
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

        private void AddContactData_Click(object sender, RoutedEventArgs e)
        {
            //TODO Выбор контакта
            idContact = Guid.NewGuid();
            BaseWindow baseWindow = new BaseWindow("Добавление контакной информации");
            baseWindow.MakeOpen(new AddContactUserConrol(IdUser, ref baseWindow, idContact));
            baseWindow.ShowDialog();

        }

        private void ChangeClient_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Обновление данных");
            baseWindow.MakeOpen(new AddUserControl(IdUser,ref baseWindow));
            baseWindow.ShowDialog();

        }

        private void RedactContactData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeAdress_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MakeLoadingContact()
        {

        }
    }
}
