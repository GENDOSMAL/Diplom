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

namespace RepairFlatWPF.UserControls.OrderWork
{
    /// <summary>
    /// Interaction logic for MakeNewOrderUserControl.xaml
    /// </summary>
    public partial class MakeNewOrderUserControl : UserControl
    {
        public bool NewOrder = true;
        Guid IdUser;
        Guid IdBrigade;
        Guid idAdress;
        Guid idContact;
        Guid idOrder;
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
        }

        private void AddAdress_Click(object sender, RoutedEventArgs e)
        {
            //TODO Добавление Адреса
            this.idAdress = Guid.NewGuid();
            BaseWindow baseWindow = new BaseWindow("Создание адресса");
            baseWindow.MakeOpen(new AditinalControl.AddInformationAboutAdress(idAdress, ref baseWindow));
            baseWindow.ShowDialog();
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
            baseWindow.MakeOpen(new AddUserControl(ref baseWindow));
            baseWindow.ShowDialog();
        }

        private void RedactContactData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeAdress_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
