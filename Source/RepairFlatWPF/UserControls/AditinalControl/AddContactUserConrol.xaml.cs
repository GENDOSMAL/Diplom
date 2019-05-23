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
        public AddContactUserConrol(Guid idUser, Guid idContact, bool NewContact=true)
        {
            InitializeComponent();
            this.idContact = idContact;
            this.idUser = idUser;
            this.NewContact = NewContact;
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
