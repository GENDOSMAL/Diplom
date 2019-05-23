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
    /// Interaction logic for WorkWithOrderServiseUserControl.xaml
    /// </summary>
    public partial class WorkWithOrderServiseUserControl : UserControl
    {
        Guid idOrder;
        public WorkWithOrderServiseUserControl( Guid idOrder)
        {
            InitializeComponent();
            this.idOrder = idOrder;
        }

        private void DeleteServises_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditServises_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddServises_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
