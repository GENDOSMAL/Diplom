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

namespace RepairFlatWPF.UserControls.OrderWork
{
    /// <summary>
    /// Interaction logic for AditionalTablesUserControl.xaml
    /// </summary>
    public partial class AditionalTablesUserControl : UserControl
    {
        Guid idOrder;
        public AditionalTablesUserControl(Guid idOrder)
        {            
            InitializeComponent();
            this.idOrder = idOrder;
        }

        private void MakeSmeta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MakeDogovor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MakeSpavk_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
