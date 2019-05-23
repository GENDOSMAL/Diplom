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
        public bool NewOrder=true;
        Guid IdUser;
        Guid IdBrigade;
        Guid idAdress;
        Guid idContact;
        Guid idOrder;
        public MakeNewOrderUserControl(bool NewOrder=true)
        {
            InitializeComponent();
            this.NewOrder = NewOrder;
            if (!NewOrder)
            {
                SelectClient.IsEnabled = false;
            }
            foreach (var StatOfOrder in Model.SomeEnums.StatusOfOrder)
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
            
            UserControls.BaseWindow baseWindow = new BaseWindow( new ClientWork.SelectWorkerUserControl(Model.SomeEnums.TypeOfConrols.Window), "Выберите клиента");
            try
            {
                baseWindow.ShowDialog();
            }
            catch
            {
                baseWindow.Close();
            }
        }

        private void AddAdress_Click(object sender, RoutedEventArgs e)
        {
            //TODO Добавление Адреса
            this.idAdress = Guid.NewGuid();
            UserControls.BaseWindow baseWindow = new BaseWindow(new AditinalControl.AddInformationAboutAdress(idAdress), "Укажите адресс");
            try
            {
                baseWindow.ShowDialog();
            }
            catch
            {
                baseWindow.Close();
            }

        }

        private void SelectBrigade_Click(object sender, RoutedEventArgs e)
        {
            //TODO Выбор бригады
            UserControls.BaseWindow baseWindow = new BaseWindow( new WorkerInformation.SelectBrigadeTable(Model.SomeEnums.TypeOfConrols.Window), "Выберите бригаду");
            try
            {
                baseWindow.ShowDialog();
            }
            catch
            {
                baseWindow.Close();
            }
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            //Возврат назад
            MakeSomeHelp.CloseBaseWindow();
        }

        private void AddContactData_Click(object sender, RoutedEventArgs e)
        {
            //TODO Выбор контакта
            idContact = Guid.NewGuid();
            UserControls.BaseWindow baseWindow = new BaseWindow( new AddContactUserConrol(IdUser,idContact), "Добавление контакной информации");
            try
            {
                baseWindow.ShowDialog();
            }
            catch
            {
                baseWindow.Close();
            }
        }
    }
}
