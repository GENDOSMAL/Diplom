using RepairFlatWPF.Model;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using static RepairFlatWPF.Model.OrderDesc;

namespace RepairFlatWPF.UserControls.OrderWork.AddInfromationUserControl
{
    /// <summary>
    /// Interaction logic for AddInfromationAboutMaterials.xaml
    /// </summary>
    public partial class AddInfromationAboutMaterials : UserControl
    {
        Guid idMaterial;
        bool NewInformation = true;
        bool InformationSelect = false;
        double Count;
        decimal cost;
        BaseWindow window;
        public AddInfromationAboutMaterials(ref BaseWindow baseWindow, object InfromationAboutMaterial = null)
        {
            InitializeComponent();
            window = baseWindow;
            if (InfromationAboutMaterial != null)
            {
                AddMaterial.Content = "Редактировать";
                NewInformation = false;
                var data = InfromationAboutMaterial as TaskMaterial;
                NameOfMaterial.Text = data.NameOfMaterials;
                CountOfMaterial.Text = data.count.ToString();
                cost = Convert.ToDecimal(data.cost);
                Cost.Text = data.cost.ToString();
                SelectMaterial.IsEnabled = false;
                InformationSelect = true;
            }

        }

        private void SelectMaterial_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Выбор материала");
            baseWindow.MakeOpen(new SettingsAndSubsInf.SelectSomeSubs(ref baseWindow, SomeEnums.TypeOfSubs.Materials));
            baseWindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {
                InformationSelect = true;
                idMaterial = SaveSomeData.idSubs;
                SaveSomeData.idSubs = new Guid();
                var row = SaveSomeData.SomeObject as DataRow;
                SaveSomeData.SomeObject = null;
                cost = decimal.Parse(row[4].ToString());
                NameOfMaterial.Text = row[1].ToString()?.Trim();
                Cost.Text = cost.ToString();
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void AddMaterial_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                TaskMaterial taskMat = new TaskMaterial
                {
                    NameOfMaterials = NameOfMaterial.Text,
                    cost = Convert.ToDouble(cost),
                    count = Count,
                    idMaterial = idMaterial,
                };
                SaveSomeData.SomeObject = taskMat;
                SaveSomeData.MakeSomeOperation = true;
                window.Close();
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
            if (!Double.TryParse(CountOfMaterial.Text.Trim(), out Count) && InformationSelect)
            {
                result = false;
                MakeSomeHelp.MSG("Необходимо указать данные о количестве!", MsgBoxImage: MessageBoxImage.Error);
            }
            return result;
        }
    }
}
