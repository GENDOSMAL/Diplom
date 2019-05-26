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
        public static BaseWindow baseWindow1;
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
            BaseWindow redactwindow = new BaseWindow("Редактирование сущестующего задания");
            redactwindow.MakeOpen(new AddInfromationUserControl.AddNewTaskInOrderUserControl(idOrder, ref redactwindow, idOrder));
            redactwindow.ShowDialog();

            //baseWindow1 = new BaseWindow(new AddInfromationUserControl.AddNewTaskInOrderUserControl(idOrder,ref baseWindow1, idOrder), "Редактирование сущестующего задания");
            //baseWindow1.ShowDialog();


        }

        private void AddTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            baseWindow1 = new BaseWindow(new AddInfromationUserControl.AddNewTaskInOrderUserControl(idOrder, ref baseWindow1), "Создание нового задания");
            try
            {
                baseWindow1.ShowDialog();
            }
            catch
            {
                baseWindow1.Close();
            }
        }

        private void BtnSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
