using System;
using System.Windows;
using System.Windows.Controls;
using static RepairFlatWPF.Model.MeasuModel;

namespace RepairFlatWPF.UserControls.OrderWork.AddInfromationUserControl
{
    /// <summary>
    /// Interaction logic for AddElementOfPremises.xaml
    /// </summary>
    public partial class AddElementOfPremises : UserControl
    {
        Guid idPremises;
        Guid idElement;
        double lenghtData, WidthData, heightData, pofelement, withOfSlopData;
        BaseWindow window;
        public AddElementOfPremises(Guid idOfPremises, ref BaseWindow baseWindow, object elementOf =null)
        {
            InitializeComponent();
            window = baseWindow;
            foreach (string TypeOfele in SomeEnums.TypeOfElement)
            {
                TypeOfElement.Items.Add(TypeOfele);
            }
            this.idPremises = idOfPremises;
            if (elementOf!=null)
            {
                var object1 = elementOf as Model.MeasuModel.ElementOfMeasurment;
                this.idElement = object1.idElement;
                TypeOfElement.SelectedValue = object1.TypeOfElement.Trim();
                Lenght.Text = object1.Lenght.ToString();
                Width.Text = object1.Width.ToString();
                Height.Text = object1.Height.ToString();
                POfElement.Text = object1.POfElement.ToString();
                lenghtData = object1.Lenght?? default(double);
                WidthData = object1.Width?? default(double);
                heightData = object1.Height?? default(double);
                pofelement = object1.POfElement?? default(double);
                withOfSlopData = object1.WidthOfSlope?? default(double);
                Description.Text = object1.Description;
                AddBtn.Content = "Редактировать";
            }
            else
            {
                this.idElement = Guid.NewGuid();
            }
        }


        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                Model.SaveSomeData.MakeSomeOperation = true;
                Model.SaveSomeData.SomeObject = new ElementOfMeasurment
                {
                    idElement = idElement,
                    idMeasurements = idPremises,
                    Description = Description.Text?.Trim(),
                    Height = heightData,
                    Lenght = lenghtData,
                    POfElement = pofelement,
                    Width = WidthData,
                    TypeOfElement = TypeOfElement.Text,
                    WidthOfSlope = withOfSlopData
                };
                window.Close();

            }
        }

        private void Width_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(Lenght.Text.Trim(), out lenghtData) && double.TryParse(Width.Text.Trim(), out WidthData))
            {
                pofelement = 2 * (lenghtData + WidthData);
                POfElement.Text = pofelement.ToString();
            }
        }

        private void Height_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(Lenght.Text.Trim(), out lenghtData) && double.TryParse(Width.Text.Trim(), out WidthData))
            {
                pofelement = (lenghtData * WidthData);
                POfElement.Text = pofelement.ToString();
            }
        }

        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private bool CheckFields()
        {
            if (TypeOfElement.SelectedIndex == -1)
            {
                MakeSomeHelp.MSG("Необходимо указать тип элемента", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (!double.TryParse(Lenght.Text.Trim(), out lenghtData))
            {
                MakeSomeHelp.MSG("Необходимо указать длину", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (!double.TryParse(Height.Text.Trim(), out heightData))
            {
                MakeSomeHelp.MSG("Необходимо указать высоту", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (!double.TryParse(Width.Text.Trim(), out WidthData))
            {
                MakeSomeHelp.MSG("Необходимо указать ширину", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
