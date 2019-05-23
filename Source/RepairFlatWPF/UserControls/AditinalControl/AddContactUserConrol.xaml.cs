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

namespace RepairFlatWPF.UserControls
{
    /// <summary>
    /// Interaction logic for AddContactUserConrol.xaml
    /// </summary>
    public partial class AddContactUserConrol : UserControl
    {
        Guid idUser;
        Guid idContact;
        bool NewContact = true;
        public AddContactUserConrol(Guid idUser, object InformationAboutContact=null)
        {
            InitializeComponent();
            
            if (InformationAboutContact == null)
            {
                this.idContact = Guid.NewGuid();
            }
            else
            {
                this.NewContact = false;
            }         
        }

        private void CreateContact_Click(object sender, RoutedEventArgs e)
        {
            if (NewContact)
            {
                //Если новый
            }
            else
            {
                //Если редактирование
            }
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.CloseBaseWindow();
        }
    }
}
