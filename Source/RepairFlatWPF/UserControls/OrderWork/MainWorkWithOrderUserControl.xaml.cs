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
    /// Interaction logic for MainWorkWithOrderUserControl.xaml
    /// </summary>
    public partial class MainWorkWithOrderUserControl : UserControl
    {
        Guid idOrder;
        public MainWorkWithOrderUserControl(Guid idOrder)
        {
            InitializeComponent();
            this.idOrder = idOrder;
            //При загрузке полностью данные о заказе
            ForPermisent.Children.Clear();
            ForPermisent.Children.Add(new WorkWithMeasurment(idOrder));

            ForServises.Children.Clear();
            ForServises.Children.Add(new WorkWithOrderServiseUserControl(idOrder));

            ForMaterials.Children.Clear();
            ForMaterials.Children.Add(new WorkWithOrderMaterialsUserControl(idOrder));

            ForPayment.Children.Clear();
            ForPayment.Children.Add(new InformationAboutOrderPay(idOrder));


        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new MainOrderUserControler());
        }

        private void SelectTabsClick(object sender, RoutedEventArgs e)
        {

            int index = int.Parse(((Button)e.Source).Uid);

            switch (index)
            {
                case 0:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowInformationAboutPresunt();
                    break;
                case 1:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowInformationAboutServises();
                    break;
                case 2:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowInformationAboutMaterials();
                    break;
                case 3:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowInformationAboutPayment();
                    break;
                case 4:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowInformationAboutPrint();
                    break;
            }
        }
        #region Показать соответсвующую "вкладку"
        private void ShowInformationAboutPresunt()
        {
            ForPermisent.Visibility = Visibility.Visible;
            ForServises.Visibility = Visibility.Collapsed;
            ForMaterials.Visibility = Visibility.Collapsed;
            ForPayment.Visibility = Visibility.Collapsed;
            ForPrint.Visibility = Visibility.Collapsed;
        }
        private void ShowInformationAboutServises()
        {
            ForServises.Visibility = Visibility.Visible;
            ForPermisent.Visibility = Visibility.Collapsed;
            ForMaterials.Visibility = Visibility.Collapsed;
            ForPayment.Visibility = Visibility.Collapsed;
            ForPrint.Visibility = Visibility.Collapsed;
        }
        private void ShowInformationAboutMaterials()
        {
            ForMaterials.Visibility = Visibility.Visible;
            ForServises.Visibility = Visibility.Collapsed;
            ForPermisent.Visibility = Visibility.Collapsed;
            ForPayment.Visibility = Visibility.Collapsed;
            ForPrint.Visibility = Visibility.Collapsed;
        }
        private void ShowInformationAboutPayment()
        {
            ForPayment.Visibility = Visibility.Visible;
            ForServises.Visibility = Visibility.Collapsed;
            ForPermisent.Visibility = Visibility.Collapsed;
            ForMaterials.Visibility = Visibility.Collapsed;
            ForPrint.Visibility = Visibility.Collapsed;
        }

        private void ShowInformationAboutPrint()
        {
            ForPrint.Visibility = Visibility.Visible;
            ForServises.Visibility = Visibility.Collapsed;
            ForPermisent.Visibility = Visibility.Collapsed;
            ForMaterials.Visibility = Visibility.Collapsed;
            ForPayment.Visibility = Visibility.Collapsed;
        }
        #endregion

    }
}
