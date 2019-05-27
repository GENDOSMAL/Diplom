using System;
using System.Windows;
using System.Windows.Controls;


namespace RepairFlatWPF.UserControls.OrderWork.AddInfromationUserControl
{
    /// <summary>
    /// Interaction logic for AddElementOfPremises.xaml
    /// </summary>
    public partial class AddElementOfPremises : UserControl
    {
        Guid idPremises;
        bool NewElement = true;
        double lenghtData, WidthData, heightData;
        public AddElementOfPremises(Guid idOfPremises, object InformationAbouElement=null)
        {
            InitializeComponent();
            TypeOfElement.Items.Add("Окно");
            TypeOfElement.Items.Add("Дверь");
            if (InformationAbouElement != null)
            {
                NewElement = false;
                AddBtn.Content = "Редактировать";
            }
            else
            {
                idPremises = Guid.NewGuid();
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                if (NewElement)
                {
                    //Добавление
                }
                else
                {
                    //Редактирование
                }
            }
        }

        
        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.CloseBaseWindow();
        }

        private bool CheckFields()
        {
            if (TypeOfElement.SelectedIndex == -1)
            {
                MakeSomeHelp.MSG("Необходимо указать тип элемента",MsgBoxImage:MessageBoxImage.Error);
                return false;
            }
            if(!double.TryParse(Lenght.Text.Trim(),out lenghtData))
            {
                MakeSomeHelp.MSG("Необходимо указать длину", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (!double.TryParse(Height.Text.Trim(),out heightData))
            {
                MakeSomeHelp.MSG("Необходимо указать высоту", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (!double.TryParse(Width.Text.Trim(),out WidthData))
            {
                MakeSomeHelp.MSG("Необходимо указать ширину", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
