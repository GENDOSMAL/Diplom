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

namespace RepairFlatWPF.UserControls.AditinalControl
{
    /// <summary>
    /// Interaction logic for AddInformationAboutAdress.xaml
    /// </summary>
    public partial class AddInformationAboutAdress : UserControl
    {
        Guid idAdress;
        bool Redact = false;
        BaseWindow window;
        public AddInformationAboutAdress(Guid idAdress,ref BaseWindow baseWindow, bool Redact=false)
        {
            InitializeComponent();
            window = baseWindow;
            this.Redact = Redact;
            this.idAdress = idAdress;
            if (Redact)
            {
                CreateNewAdress.Content="Обновить данные";
            }
        }

        private void CreateNewAdress_Click(object sender, RoutedEventArgs e)
        {
            if (Redact)
            {

            }
            else
            {

            }
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }
    }
}
