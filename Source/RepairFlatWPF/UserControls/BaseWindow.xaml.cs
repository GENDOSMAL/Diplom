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
using System.Windows.Shapes;

namespace RepairFlatWPF.UserControls
{
    /// <summary>
    /// Interaction logic for MakeNewOrder.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {
        bool NewOrder = true;
        Guid idSelectUser;

        public BaseWindow(string title)
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(this, Title);
            
        }
        //public BaseWindow(UserControl userControl, string Title)
        //{           
        //    InitializeComponent();
        //    this.DataContext = new MainWindowViewModel(this, Title);
        //    GridForContent.Children.Clear();
        //    GridForContent.Children.Add(userControl);
        //}

        public void MakeOpen(UserControl userControl)
        {
            GridForContent.Children.Clear();
            GridForContent.Children.Add(userControl);
        }

    }
}
