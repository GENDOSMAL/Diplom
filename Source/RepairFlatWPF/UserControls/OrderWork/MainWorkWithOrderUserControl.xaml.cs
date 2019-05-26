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
        public MainWorkWithOrderUserControl(Guid idOrder, object AllDataAboutOrder=null)
        {
            InitializeComponent();
            this.idOrder = idOrder;
            #region Загружаем данные обо всем заказе на соответсвующие элементы управления
            //Данные о помещениях
            ForPermisent.Children.Clear();
            ForPermisent.Children.Add(new WorkWithMeasurment(idOrder));
            //данные о заданиях
            ForTasks.Children.Clear();
            ForTasks.Children.Add(new WorkWithTasksUserControl(idOrder));                      
            //данные об услугах
            ForServises.Children.Clear();
            ForServises.Children.Add(new WorkWithOrderServiseUserControl(idOrder));
            //Данные о материалах
            ForMaterials.Children.Clear();
            ForMaterials.Children.Add(new WorkWithOrderMaterialsUserControl(idOrder));
            //Данные об оплате 
            ForPayment.Children.Clear();
            ForPayment.Children.Add(new InformationAboutOrderPay(idOrder));
            #endregion
            //При загрузке полностью данные о заказе



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
                    ShowInformationAboutTask();
                    break;
                case 2:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowInformationAboutServises();
                    break;
                case 3:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowInformationAboutMaterials();
                    break;
                case 4:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowInformationAboutPayment();
                    break;
                case 5:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowInformationAboutPrint();
                    break;
            }
        }
        #region Показать соответсвующую "вкладку" (Говно код)
        private void ShowInformationAboutPresunt()
        {
            ForTasks.Visibility = Visibility.Collapsed;
            ForPermisent.Visibility = Visibility.Visible;
            ForServises.Visibility = Visibility.Collapsed;
            ForMaterials.Visibility = Visibility.Collapsed;
            ForPayment.Visibility = Visibility.Collapsed;
            ForPrint.Visibility = Visibility.Collapsed;
        }
        private void ShowInformationAboutServises()
        {
            ForTasks.Visibility = Visibility.Collapsed;
            ForServises.Visibility = Visibility.Visible;
            ForPermisent.Visibility = Visibility.Collapsed;
            ForMaterials.Visibility = Visibility.Collapsed;
            ForPayment.Visibility = Visibility.Collapsed;
            ForPrint.Visibility = Visibility.Collapsed;
        }
        private void ShowInformationAboutMaterials()
        {
            ForTasks.Visibility = Visibility.Collapsed;
            ForMaterials.Visibility = Visibility.Visible;
            ForServises.Visibility = Visibility.Collapsed;
            ForPermisent.Visibility = Visibility.Collapsed;
            ForPayment.Visibility = Visibility.Collapsed;
            ForPrint.Visibility = Visibility.Collapsed;
        }
        private void ShowInformationAboutPayment()
        {
            ForTasks.Visibility = Visibility.Collapsed;
            ForPayment.Visibility = Visibility.Visible;
            ForServises.Visibility = Visibility.Collapsed;
            ForPermisent.Visibility = Visibility.Collapsed;
            ForMaterials.Visibility = Visibility.Collapsed;
            ForPrint.Visibility = Visibility.Collapsed;
        }

        private void ShowInformationAboutPrint()
        {
            ForTasks.Visibility = Visibility.Collapsed;
            ForPrint.Visibility = Visibility.Visible;
            ForServises.Visibility = Visibility.Collapsed;
            ForPermisent.Visibility = Visibility.Collapsed;
            ForMaterials.Visibility = Visibility.Collapsed;
            ForPayment.Visibility = Visibility.Collapsed;
        }
        private void ShowInformationAboutTask()
        {
            ForTasks.Visibility = Visibility.Visible;
            ForPrint.Visibility = Visibility.Collapsed;
            ForServises.Visibility = Visibility.Collapsed;
            ForPermisent.Visibility = Visibility.Collapsed;
            ForMaterials.Visibility = Visibility.Collapsed;
            ForPayment.Visibility = Visibility.Collapsed;
        }
        #endregion

    }
}
