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
            UserControls.BaseWindow baseWindow = new BaseWindow(false, new ClientWork.SelectWorkerUserControl(Model.SomeEnums.TypeOfConrols.Window), "Выберите клиента");
            baseWindow.ShowDialog();
        }

        private void AddAdress_Click(object sender, RoutedEventArgs e)
        {
            //TODO Добавление Адреса

        }

        private void SelectBrigade_Click(object sender, RoutedEventArgs e)
        {
            //TODO Выбор бригады
            UserControls.BaseWindow baseWindow = new BaseWindow(false, new WorkerInformation.SelectBrigadeTable(Model.SomeEnums.TypeOfConrols.Window), "Выберите бригаду");
            baseWindow.ShowDialog();
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            UserControls.BaseWindow baseWindow = new BaseWindow(false,new MainOrderUserControler(),"Тестирование");
            baseWindow.ShowDialog();
        }

        private void AddContactData_Click(object sender, RoutedEventArgs e)
        {
            //TODO Выбор контакта
            UserControls.BaseWindow baseWindow = new BaseWindow(false, new ClientWork.AddContactUserConrol(IdUser), "Добавление контакной информации");
            baseWindow.ShowDialog();
        }
    }
}
