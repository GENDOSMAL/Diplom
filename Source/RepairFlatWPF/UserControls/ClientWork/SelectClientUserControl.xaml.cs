using System;
using System.Windows;
using System.Windows.Controls;

namespace RepairFlatWPF.UserControls.ClientWork
{
    /// <summary>
    /// Interaction logic for SelectWorkerUserControl.xaml
    /// </summary>
    public partial class SelectClientUserControl : UserControl
    {
        SomeEnums.TypeOfConrols typeOfConrols;
        BaseWindow window;
        public SelectClientUserControl(SomeEnums.TypeOfConrols typeOfConrols, ref BaseWindow baseWindow)
        {
            InitializeComponent();
            window = baseWindow;
            this.typeOfConrols = typeOfConrols;
            if (this.typeOfConrols == SomeEnums.TypeOfConrols.UserControl)
            {
                ForUserControl.Visibility = Visibility.Visible;
            }
            else
            {
                ForWindow.Visibility = Visibility.Visible;
            }
        }

        private void SelectClient_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.MSG("Не реализовано");
        }

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Редактирование данных о клиенте");
            baseWindow.MakeOpen(new AddUserControl(ref baseWindow, typeOfConrols));
            baseWindow.ShowDialog();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Добавление данных о клиенте");
            baseWindow.MakeOpen(new AddUserControl(ref baseWindow));
            baseWindow.ShowDialog();
        }

        private void ReturnBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.MSG("Не реализовано");
        }

        private void GetLoginInformation_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Информация о логине и пароле");
            baseWindow.MakeOpen(new AditinalControl.ShowDataForAuth(Guid.NewGuid(),ref baseWindow));
            baseWindow.ShowDialog();
        }
    }
}
