using RepairFlatWPF.Model;
using RepairFlatWPF.UserControls;
using RepairFlatWPF.UserControls.KadrWork;
using RepairFlatWPF.UserControls.MoneyInformation;
using RepairFlatWPF.UserControls.SettingsAndSubsInf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace RepairFlatWPF
{

    public partial class MainWindow : Window
    {
        bool open = true;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(this, "");
            MakeSomeHelp.MakeShowLogining();
        }

        public void Makecheck()
        {
            WorkWithOrder.Visibility = Visibility.Collapsed;
            ClientWork.Visibility = Visibility.Collapsed;
            Spravoch.Visibility = Visibility.Collapsed;
            KadrWork.Visibility = Visibility.Collapsed;
            Finans.Visibility = Visibility.Collapsed;
            Settings.Visibility = Visibility.Collapsed;
            NameOfPolz.Text = SaveSomeData.LastNameAndIni;
            if (SaveSomeData.TypeOfUser == SomeEnums.TypeOfUser.AD.ToString())
            {//Администратор
                Settings.Visibility = Visibility.Visible;
                Spravoch.Visibility = Visibility.Visible;
                MakeSomeHelp.DataGridMakeWork(new SettingsUserControls());
            }
            else if (SaveSomeData.TypeOfUser == SomeEnums.TypeOfUser.BW.ToString())
            {//Работник бухгалтерии
                Finans.Visibility = Visibility.Visible;
                Spravoch.Visibility = Visibility.Visible;
                MakeSomeHelp.DataGridMakeWork(new FinansInformation());
            }
            else if (SaveSomeData.TypeOfUser == SomeEnums.TypeOfUser.KW.ToString())
            {//Работник отдела кадров
                KadrWork.Visibility = Visibility.Visible;
                MakeSomeHelp.DataGridMakeWork(new MenuKadrWork());
            }
            else if (SaveSomeData.TypeOfUser == SomeEnums.TypeOfUser.MG.ToString())
            {//Менеджер
                WorkWithOrder.Visibility = Visibility.Visible;
                ClientWork.Visibility = Visibility.Visible;
                Spravoch.Visibility = Visibility.Visible;
                MakeSomeHelp.DataGridMakeWork(new SelectOrderToWork());
            }
            else if (SaveSomeData.TypeOfUser == SomeEnums.TypeOfUser.BB.ToString())
            {//Босс
                WorkWithOrder.Visibility = Visibility.Visible;
                ClientWork.Visibility = Visibility.Visible;
                Spravoch.Visibility = Visibility.Visible;
                KadrWork.Visibility = Visibility.Visible;
                Finans.Visibility = Visibility.Visible;
                Settings.Visibility = Visibility.Visible;
                MakeSomeHelp.DataGridMakeWork(new SelectOrderToWork());
            }
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            open = true;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            Bluring.Visibility = Visibility.Visible;
        }

        private void ButtonMenuClose_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            open = false;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            Bluring.Visibility = Visibility.Collapsed;
        }
        private void ListViewMenu_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (open)
            {

                int index = ListViewMenu.SelectedIndex;
                switch (index)
                {
                    case 0:
                        //Работа с заказами
                        CloseMenu();

                        MakeSomeHelp.DataGridMakeWork(new UserControls.SelectOrderToWork());
                        break;
                    case 1:
                        //Работа с клиентами
                        CloseMenu();
                        BaseWindow baseWindow = new BaseWindow("sa");
                        MakeSomeHelp.DataGridMakeWork(new UserControls.ClientWork.SelectClientUserControl(SomeEnums.TypeOfConrols.UserControl, ref baseWindow));
                        break;

                    case 2:
                        //Справочные данные
                        CloseMenu();
                        MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.WorkWithSubInfromation());
                        break;
                    case 3:
                        //Работа с кадрами
                        CloseMenu();
                        MakeSomeHelp.DataGridMakeWork(new UserControls.KadrWork.MenuKadrWork());
                        break;
                    case 4:
                        //Работа с финансами
                        CloseMenu();
                        MakeSomeHelp.DataGridMakeWork(new UserControls.MoneyInformation.FinansInformation());
                        break;

                }
            }
        }

        void CloseMenu()
        {
            var animation = (Storyboard)FindResource("CloseMenu");
            animation.Begin();
            ButtonCloseMenu.Visibility = Visibility.Visible;
            open = false;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            Bluring.Visibility = Visibility.Collapsed;
        }

        private void BottomListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Проверка на то, что окно открыто или нет, сделано, для того, что бы не работа при закрытом
            if (open)
            {
                int index = BottomListView.SelectedIndex;
                switch (index)
                {
                    case 0:
                        //Переход на отображение настроек
                        CloseMenu();
                        MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.SettingsUserControls());
                        break;
                    case 1:
                        //Смена профиля
                        //bright.Visibility = Visibility.Collapsed;
                        CloseMenu();
                        if (MessageBox.Show("Вы действительно хотите сменить пользователя?", "АИС Фирмы по ремонту квартир", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                        {
                            GridMenu.Width = 0;
                            MakeSomeHelp.MakeShowLogining();
                            SaveSomeData.LastNameAndIni = "";
                            SaveSomeData.IdUser = new Guid();
                            SaveSomeData.TypeOfUser = "";
                            open = false;
                        }
                        break;

                    case 2:
                        //Выход из приложения
                        if (MessageBox.Show("Вы действительно хотите выйти из программы?", "АИС Ремонт квартир", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                        {
                            Application.Current.Shutdown();
                        }
                        break;
                }
            }
        }
        public void setChildren(UserControl ss1)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(ss1);
        }
        private void Bluring_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CloseMenu();
        }
    }
}
