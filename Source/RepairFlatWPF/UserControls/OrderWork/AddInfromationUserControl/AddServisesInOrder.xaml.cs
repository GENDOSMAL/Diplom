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

namespace RepairFlatWPF.UserControls.OrderWork.AddInfromationUserControl
{
    /// <summary>
    /// Interaction logic for AddServisesInOrder.xaml
    /// </summary>
    public partial class AddServisesInOrder : UserControl
    {
        #region Переменные

        Guid idOrder;
        Guid idServis;
        bool NewInfromation = true;
        bool IsSelect = false;
        double Count;

        #endregion
        #region Обработка событий
        public AddServisesInOrder(Guid IdOrder, object InfromationAboutServis = null)
        {
            InitializeComponent();
            if (InfromationAboutServis != null)
            {
                NewInfromation = false;
                AddServis.Content = "Редактировать";
            }
            else
            {
                NewInfromation = true;
                idServis = Guid.NewGuid();
            }
        }

        private void SelectServis_Click(object sender, RoutedEventArgs e)
        {
            if (true)//Если выбран
            {
                IsSelect = true;
            }
            else
            {

            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.CloseBaseWindow();
        }

        private void AddServis_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                if (NewInfromation)
                {

                }
                else
                {
                    //Редактирование
                }
            }
        }
        #endregion

        #region Дополнительно
        private bool CheckFields()
        {
            bool result = true;
            if (!IsSelect)
            {
                MakeSomeHelp.MSG("Необходимо выбрать услугу");
                result = false;
            }
            if (!double.TryParse(CountOfServis.Text.Trim(),out Count) && IsSelect)
            {
                MakeSomeHelp.MSG("Необходимо указать количество");
                result = false;
            }
            return result;
        }
        #endregion

    }
}
