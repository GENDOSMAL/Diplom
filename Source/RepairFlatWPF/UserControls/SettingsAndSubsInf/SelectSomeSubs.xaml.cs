using System.Windows;
using System.Windows.Controls;


namespace RepairFlatWPF.UserControls.SettingsAndSubsInf
{
    /// <summary>
    /// Interaction logic for SelectSomeSubs.xaml
    /// </summary>
    public partial class SelectSomeSubs : UserControl
    {
        #region Переменные 

        SomeEnums.TypeOfSubs typeOfSubs;
        BaseWindow BaseWindow;
        #endregion

        #region Конструкторы

        public SelectSomeSubs(ref BaseWindow newWindow, SomeEnums.TypeOfSubs typeOfSubs)
        {
            InitializeComponent();
            this.typeOfSubs = typeOfSubs;
            this.BaseWindow = newWindow;
            MakeLoadFromLocalBD();
        }
        #endregion

        #region Для подстановки данных
        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow.Close();
        }

        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {
            if (typeOfSubs == SomeEnums.TypeOfSubs.Materials)
            {
                //Тут про материалы
            }
            else if (typeOfSubs == SomeEnums.TypeOfSubs.Servises)
            {
                //Тут про услуги
            }
            else if (typeOfSubs == SomeEnums.TypeOfSubs.Post)
            {
                //Тут про должности
            }
        }
        #endregion

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (typeOfSubs == SomeEnums.TypeOfSubs.Materials)
            {
                //Тут про материалы
            }
            else if (typeOfSubs == SomeEnums.TypeOfSubs.Servises)
            {
                //Тут про услуги
            }
            else if (typeOfSubs == SomeEnums.TypeOfSubs.Post)
            {
                //Тут про должности
            }
        }


        #region Дополнительные данные
        private void MakeLoadFromLocalBD()
        {
            if(typeOfSubs == SomeEnums.TypeOfSubs.Materials)
            {
                //Тут про материалы
            }
            else if(typeOfSubs == SomeEnums.TypeOfSubs.Servises)
            {
                //Тут про услуги
            }
            else if(typeOfSubs == SomeEnums.TypeOfSubs.Post)
            {
                //Тут про должности
            }

        }

        #endregion
    }
}
