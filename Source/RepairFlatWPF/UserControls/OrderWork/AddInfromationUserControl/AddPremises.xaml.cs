using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for AddPremises.xaml
    /// </summary>
    public partial class AddPremises : UserControl
    {
        #region Переменные
        bool NewData = true;
        Guid idOrder;

        DataTable DataAboutElement;
        double LenghtData, HeightData, WidthData;

        #endregion
        #region Обработка событий и конструктор
        public AddPremises(Guid idOrder, object InformatioAboutPremises = null)
        {
            InitializeComponent();
            this.idOrder = idOrder;
            if (InformatioAboutPremises != null)
            {
                NewData = false;
                AddBtn.Content = "Редактировать";
            }
        }

        private void AddElement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RedactElement_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {

            }
            else
            {
                MakeSomeHelp.MSG("Выберите элемент для редактирования!", MsgBoxImage: MessageBoxImage.Error);
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
                MakeSomeHelp.MSG("Выберите элемент для удаления!", MsgBoxImage: MessageBoxImage.Error);
            }
        }

        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                if (NewData)
                {
                    //Тут если данные новые
                }
                else
                {
                    //Тут если данные обновляются
                }
            }
        }
        #endregion

        #region Дополнительно
        private bool CheckFields()
        {
            if (TypeOfPremises.SelectedIndex == -1)
            {
                MakeSomeHelp.MSG("Необходимо выбрать тип помещения", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (!double.TryParse(Lenght.Text.Trim(), out LenghtData))
            {
                MakeSomeHelp.MSG("Необходимо указать длну", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (!double.TryParse(Width.Text.Trim(), out WidthData))
            {
                MakeSomeHelp.MSG("Необходимо указать ширину", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (!double.TryParse(Height.Text.Trim(), out HeightData))
            {
                MakeSomeHelp.MSG("Необходимо указать высоту", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }

            if (DataAboutElement.Rows.Count == 0)
            {
                if(MakeSomeHelp.MSG("Вы действительно хотите не добавлять данные об элементах помешения?", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
