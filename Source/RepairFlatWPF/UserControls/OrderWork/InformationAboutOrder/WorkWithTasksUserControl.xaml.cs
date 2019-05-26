using System;
using System.Windows.Controls;

namespace RepairFlatWPF.UserControls.OrderWork
{
    /// <summary>
    /// Interaction logic for WorkWithTasksUserControl.xaml
    /// </summary>
    public partial class WorkWithTasksUserControl : UserControl
    {
        Guid idOrder;

        public WorkWithTasksUserControl(Guid idOrder, object DataAboutTasks=null)
        {
            InitializeComponent();
            this.idOrder = idOrder;
        }

        private void DeleteTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void EditTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
            BaseWindow baseWindow = new BaseWindow(new AddInfromationUserControl.AddNewTaskInOrderUserControl(idOrder,idOrder), "Редактирование сущестующего задания");
            try
            {
                baseWindow.ShowDialog();
            }
            catch
            {
                baseWindow.Close();
            }
        }

        private void AddTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow(new AddInfromationUserControl.AddNewTaskInOrderUserControl(idOrder), "Создание нового задания");
            try
            {
                baseWindow.ShowDialog();
            }
            catch
            {
                baseWindow.Close();
            }
        }

        private void BtnSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
