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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RepairFlatWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool open = true;

        /// <summary>
        /// Конструктор главного окна, которы при открытии приложения процесс логирования
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(this);
            //TODO Для логирования убрать
            //MakeSomeHelp.MakeShowLoading();
        }


        /// <summary>
        /// Событие открытие бокового меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            open = true;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            Bluring.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Событие закрытие бокового меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMenuClose_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            open = false;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            Bluring.Visibility = Visibility.Collapsed;
        }


        /// <summary>
        /// Отлавливание события перехода по определенным пунктам верхнего меню 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        MakeSomeHelp.DataGridMakeWork(new UserControls.MainOrderUserControler());
                        break;
                    case 1:
                        //Работа с клиентами
                        CloseMenu();
                        MakeSomeHelp.DataGridMakeWork(new UserControls.ClientWork.SelectWorkerUserControl(Model.SomeEnums.TypeOfConrols.UserControl));
                        break;
                    
                    case 2:
                        //Справочные данные
                        CloseMenu();
                        MakeSomeHelp.MakeMessageBox("Не реализовано");
                        break;
                    case 3:
                        //Работа с кадрами
                        CloseMenu();
                        MakeSomeHelp.MakeMessageBox("Не реализовано");
                        break;
                    case 4:
                        //Работа с финансами
                        CloseMenu();
                        MakeSomeHelp.MakeMessageBox("Не реализовано");

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
        /// <summary>
        /// Отлавливание событий выбора нижней части меню 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BottomListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Проверка на то, что окно открыто или нет, сделано, для того, что бы не работа при закрытом
            if (open)
            {
                var animation = (Storyboard)FindResource("CloseMenu");
                int index = BottomListView.SelectedIndex;
                switch (index)
                {
                    case 0:
                        //Переход на отображение настроек
                        break;
                    case 1:
                        //Смена профиля
                        //bright.Visibility = Visibility.Collapsed;
                        if (MessageBox.Show("Вы действительно хотите сменить пользователя?", "АИС Фирмы по ремонту квартир", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                        {
                            animation.Begin();
                            GridMenu.Width = 0;
                            //ChildGrid.Children.Clear();
                            //LoginChild.Children.Clear();
                            //LoginChild.Children.Add(new Views.LoginUsersControl());
                            //Classes.ClassWitSettings.Prava = 0;
                            //Classes.ClassWitSettings.idPolzovat = 0;
                            open = false;
                        }
                        break;
                        
                    case 2:
                        //Выход из приложения
                        //bright.Visibility = Visibility.Collapsed;
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
    }
}
