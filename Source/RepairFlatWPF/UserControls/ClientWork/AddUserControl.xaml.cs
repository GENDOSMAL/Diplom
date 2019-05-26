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

namespace RepairFlatWPF.UserControls
{
    /// <summary>
    /// Interaction logic for AddUserControl.xaml
    /// </summary>
    public partial class AddUserControl : UserControl
    {
        Guid idClient;
        bool NewData = true;
        #region Констурктор и обработчик
        public AddUserControl(object InformationAboutClient = null)
        {
            InitializeComponent();
            if (InformationAboutClient != null)
            {
                AddBtn.Content = "Редактировать";
                NewData = false;
            }
            else
            {
                idClient = Guid.NewGuid();
            }
        }

        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.CloseBaseWindow();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                if (NewData)
                {
                    //Тут добавление
                }
                else
                {
                    //Тут редактирование
                }
            }
        }

        private void AddElement_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow(new AddContactUserConrol(idClient), "Добавление контактной информации");
            try
            {
                baseWindow.ShowDialog();
            }
            catch
            {
                baseWindow.Close();
            }
        }

        private void RedactElement_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                object some = new object();
                BaseWindow baseWindow = new BaseWindow(new AddContactUserConrol(idClient, some), "Редактирование контактной информации");
                try
                {
                    baseWindow.ShowDialog();
                }
                catch
                {
                    baseWindow.Close();
                }
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать строку для редактирования", MsgBoxImage: MessageBoxImage.Error);
            }
        }

        private void DeleteElement_Click(object sender, RoutedEventArgs e)
        {

            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать строку для редактирования", MsgBoxImage: MessageBoxImage.Error);
            }

        }

        #endregion

        #region Прочие обработки

        private bool CheckFields()
        {
            if (string.IsNullOrEmpty(Name.Text.Trim()))
            {
                MakeSomeHelp.MSG("Укажите данные о имени клиента", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(Famil.Text.Trim()))
            {
                MakeSomeHelp.MSG("Укажите данные о фамилии клиента", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(Patronymic.Text.Trim()))
            {
                MakeSomeHelp.MSG("Укажите данные об отчестве клиента", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(Pasport.Text.Trim()))
            {
                MakeSomeHelp.MSG("Укажите данные об паспорте клиента", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (Female.SelectedIndex == -1)
            {
                MakeSomeHelp.MSG("Укажите данные об половой принаждежности клиента", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (!DateOfBirsd.SelectedDate.HasValue)
            {
                MakeSomeHelp.MSG("Укажите данные об дате рождения", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        #endregion


    }
}
