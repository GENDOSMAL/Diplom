using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RepairFlatWPF.UserControls.OrderWork.AddInfromationUserControl
{
    /// <summary>
    /// Interaction logic for AddNewTaskInOrderUserControl.xaml
    /// </summary>
    public partial class AddNewTaskInOrderUserControl : UserControl
    {
        #region Переменные

        Guid idOrder;
        Guid idTask;
        bool NewTask;
        BaseWindow Window;
        
        List<int> idOfPremisesSelected = new List<int>();

        #endregion

        #region Конструктор
        public AddNewTaskInOrderUserControl(Guid idOrder, ref BaseWindow baseWindow, object DataAboutUpdateTask = null)
        {
            InitializeComponent();           
            this.idOrder = idOrder;
            ShowFirstPage();
            Window = baseWindow;
            if (DataAboutUpdateTask != null)
            {
                NewTask = false;
            }
            else
            {
                idTask = Guid.NewGuid();
                NewTask = true;
            }

        }
        #endregion

        #region Обработки основной части формы
        private void SelectTabsClick(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            switch (index)
            {
                case 0:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowFirstPage();
                    break;
                case 1:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowSecondPage();
                    break;
                case 2:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowThirdPage();
                    break;
            }
        }


        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            string is1 = "";
            for (int i = 0; i < idOfPremisesSelected.Count; i++)
            {
                is1 += idOfPremisesSelected[i].ToString();
            }
            MakeSomeHelp.MSG(is1);
        }


        private void ExtionButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Обработки при работе с услугами
        private void AddServises_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditServises_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteSerises_Click(object sender, RoutedEventArgs e)
        {
            WorkWithTasksUserControl.baseWindow1.Close();
        }

        private void BtnSearchServis_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Обработки про материалы
        private void DeleteMaterials_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditMaterials_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddMaterials_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSearchMaterials_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Вспомогательные

        #region При работе с основной формой

        private void ShowFirstPage()
        {
            ForMainData.Visibility = Visibility.Visible;
            ForServises.Visibility = Visibility.Collapsed;
            ForMaterials.Visibility = Visibility.Collapsed;
        }

        private void ShowSecondPage()
        {
            ForServises.Visibility = Visibility.Visible;
            ForMainData.Visibility = Visibility.Collapsed;
            ForMaterials.Visibility = Visibility.Collapsed;
        }

        private void ShowThirdPage()
        {
            ForMaterials.Visibility = Visibility.Visible;
            ForMainData.Visibility = Visibility.Collapsed;
            ForServises.Visibility = Visibility.Collapsed;
        }

        #endregion



        #endregion


    }
}
