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
    /// Interaction logic for WorkWithOrderServiseUserControl.xaml
    /// </summary>
    public partial class WorkWithOrderServiseUserControl : UserControl
    {
        Guid idOrder;
        public WorkWithOrderServiseUserControl( Guid idOrder)
        {
            InitializeComponent();
            this.idOrder = idOrder;
        }

        private void DeleteServises_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.MSG("Не реализовано");
        }

        private void EditServises_Click(object sender, RoutedEventArgs e)
        {
            object f = new object();
            BaseWindow baseWindow = new BaseWindow(new UserControls.OrderWork.AddInfromationUserControl.AddServisesInOrder(idOrder,f), "Обвновление данных об услугах");
            try
            {
                baseWindow.ShowDialog();
            }
            catch
            {
                baseWindow.Close();
            }
        }

        private void AddServises_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow(new UserControls.OrderWork.AddInfromationUserControl.AddServisesInOrder(idOrder), "Добавление данных об услугах");
            try
            {
                baseWindow.ShowDialog();
            }
            catch
            {
                baseWindow.Close();
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.MSG("Не реализовано");
        }
    }
}
