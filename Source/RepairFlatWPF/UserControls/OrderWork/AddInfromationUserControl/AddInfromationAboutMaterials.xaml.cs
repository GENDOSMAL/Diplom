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
    /// Interaction logic for AddInfromationAboutMaterials.xaml
    /// </summary>
    public partial class AddInfromationAboutMaterials : UserControl
    {
        Guid idOrder;
        Guid idMaterial;
        bool NewInformation = true;
        bool InformationSelect = false;
        double Count;
        public AddInfromationAboutMaterials(Guid IdOrder,object InfromationAboutMaterial=null)
        {
            InitializeComponent();
            this.idOrder = IdOrder;
            if (InfromationAboutMaterial != null)
            {
                AddMaterial.Content = "Редактировать";
                NewInformation = false;
            }
            else
            {
                idMaterial = Guid.NewGuid();
            }
        }

        private void SelectMaterial_Click(object sender, RoutedEventArgs e)
        {
            //TODO Окно с выбором материала
            if (true)//Если материал был выбран
            {
                InformationSelect = true;
            }
            else
            {
                InformationSelect = false;
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.CloseBaseWindow();
        }

        private void AddMaterial_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                if (NewInformation)
                {
                    //Добавляем материал
                }
                else
                {
                    //Редактируем
                }
            }
        }

        private bool CheckFields()
        {
            bool result = true;
            if (!InformationSelect)
            {
                result = false;
                MakeSomeHelp.MSG("Необходимо выбрать материал!", MsgBoxImage: MessageBoxImage.Error);
            }
            if(!Double.TryParse(CountOfMaterial.Text.Trim(), out Count) && InformationSelect)
            {
                result = false;
                MakeSomeHelp.MSG("Необходимо указать данные о количестве!", MsgBoxImage: MessageBoxImage.Error);
            }
            return result;
        }
    }
}
