using System;
using System.Collections.Generic;
using System.Data;
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
        List<Guid> idContactType;
        List<string> Tooltip;
        Guid idUser;
        Guid idContact;
        bool NewContact = true;
        public AddContactUserConrol(Guid idUser, object InformationAboutContact = null)
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
            var ListOfType = MakeListOfTypeOfContact();
            if (ListOfType == null)
            {
                //Закрыть окно
                MakeSomeHelp.MSG("Необходимо обратиться к администратору либо перезагрузить систему", MsgBoxImage: MessageBoxImage.Error);
            }
            else
            {
                for(int i = 0; i < ListOfType.Count; i++)
                {
                    ComboBoxItem NewItem= new ComboBoxItem();
                    NewItem.Content = ListOfType[i];
                    NewItem.ToolTip = Tooltip[i];
                    TypeOFContact.Items.Add(NewItem);
                }
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
            //TODO Переделать
            MakeSomeHelp.CloseBaseWindow();
        }

        private List<string> MakeListOfTypeOfContact()
        {
            List<string> TypeOfContact = new List<string>();
            idContactType = new List<Guid>();
            Tooltip = new List<string>();
            string query = "Select * from ContactType";
            var TablesOfTypeOfContact = Controller.MakeWorkWirthDataBase.MakeSomeQueryWork(query, WorkWithTables: true);
            if (TablesOfTypeOfContact != null)
            {
                DataTable ContactType = TablesOfTypeOfContact as DataTable;
                for (int i = 0; i < ContactType.Rows.Count; i++)
                {
                    try
                    {
                        string desc = !string.IsNullOrEmpty(ContactType.Rows[i]["Description"].ToString()) ? ContactType.Rows[i]["Description"].ToString() : "Не данных";
                        Tooltip.Add(desc);
                        TypeOfContact.Add(ContactType.Rows[i]["Value"].ToString());
                        Guid id;
                        if (Guid.TryParse(ContactType.Rows[i]["idContact"].ToString(), out id))
                        {
                            idContactType.Add(id);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch
                    {
                        return null;
                    }

                }
            }
            return TypeOfContact;
        }

    }
}
