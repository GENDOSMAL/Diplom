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
    /// Interaction logic for WorkWithMeasurment.xaml
    /// </summary>
    public partial class WorkWithMeasurment : UserControl
    {
        Guid idOrder;
        public WorkWithMeasurment(Guid IdOrder)
        {
            InitializeComponent();
            this.idOrder = IdOrder;
        }

        private void AddMeasurmant_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow(new UserControls.OrderWork.AddInfromationUserControl.AddPremises(idOrder), "Добавление данных о помещениях");
            try
            {
                baseWindow.ShowDialog();
            }
            catch
            {
                baseWindow.Close();
            }
        }

        private void EditMeasurment_Click(object sender, RoutedEventArgs e)
        {
            object f = new object();
            BaseWindow baseWindow = new BaseWindow(new UserControls.OrderWork.AddInfromationUserControl.AddPremises(idOrder,f), "Добавление данных о помещениях");
            try
            {
                baseWindow.ShowDialog();
            }
            catch
            {
                baseWindow.Close();
            }
        }

        private void DeleteMeasurment_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.MSG("Не реализовано");
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.MSG("Не реализовано");
        }
    }
}
