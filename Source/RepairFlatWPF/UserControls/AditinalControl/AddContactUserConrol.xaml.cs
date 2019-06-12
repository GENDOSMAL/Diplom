using RepairFlat.Model;
using RepairFlatWPF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace RepairFlatWPF.UserControls
{
    public partial class AddContactUserConrol : UserControl
    {
        ContactModel.InformationAboutContact informationAboutContact;
        List<Tuple<Guid, string>> idContactType;
        List<string> Tooltip;
        Guid? idUser;
        Guid idContact;
        bool NewContact = true;
        BaseWindow window;
        public AddContactUserConrol(Guid? idUser, ref BaseWindow baseWindow, object InformationAboutContact = null)
        {
            InitializeComponent();
            window = baseWindow;

            this.idUser = idUser;
            var ListOfType = MakeListOfTypeOfContact();
            if (ListOfType == null)
            {
                window.Close();
                MakeSomeHelp.MSG("Необходимо обратиться к администратору либо перезагрузить систему", MsgBoxImage: MessageBoxImage.Error);
            }
            else
            {
                for (int i = 0; i < ListOfType.Count; i++)
                {
                    ComboBoxItem NewItem = new ComboBoxItem();
                    NewItem.Content = ListOfType[i];
                    NewItem.ToolTip = Tooltip[i];
                    TypeOFContact.Items.Add(NewItem);
                }
            }

            if (InformationAboutContact == null)
            {
                this.idContact = Guid.NewGuid();
            }
            else
            {
                CreateContact.Content = "Редактировать";
                this.NewContact = false;
                this.informationAboutContact = InformationAboutContact as ContactModel.InformationAboutContact;
                idContact = informationAboutContact.idContact;
                Description.Text = informationAboutContact.Desctription;
                int index = 0;
                for (int i = 0; i < idContactType.Count; i++)
                {
                    if (idContactType[i].Item1 == informationAboutContact.idTypeOfContact)
                    {
                        index = i;
                    }
                }
                TypeOFContact.SelectedIndex = index;
                Value.Text = informationAboutContact.Value;
            }

        }
        private void CreateContact_Click(object sender, RoutedEventArgs e)
        {
            if (ResultIsHave())
            {
                if (NewContact)
                {
                    ContactModel.InformationAboutContact contactModel = new ContactModel.InformationAboutContact
                    {
                        DateAdd = DateTime.Now,
                        Desctription = Description.Text.Trim(),
                        idContact = idContact,
                        idTypeOfContact = idContactType[TypeOFContact.SelectedIndex].Item1,
                        Value = Value.Text.Trim(),
                        idUser = idUser,
                        NameOfValue = TypeOFContact.Text.Trim()
                    };
                    SaveSomeData.SomeObject = contactModel;
                    SaveSomeData.MakeSomeOperation = true;
                    window.Close();
                }
                else
                {
                    ContactModel.InformationAboutContact contactModel = new ContactModel.InformationAboutContact
                    {
                        Desctription = Description.Text.Trim(),
                        idContact = idContact,
                        idTypeOfContact = idContactType[TypeOFContact.SelectedIndex].Item1,
                        Value = Value.Text.Trim(),
                        idUser = idUser,
                        NameOfValue = TypeOFContact.Text.Trim(),
                        Number = informationAboutContact.Number
                    };
                    SaveSomeData.SomeObject = contactModel;
                    SaveSomeData.MakeSomeOperation = true;
                    window.Close();
                }
            }
        }

        private bool ResultIsHave()
        {
            if (string.IsNullOrEmpty(Value.Text.Trim()))
            {
                MakeSomeHelp.MSG("Неоходимо указать значение контакта", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (TypeOFContact.SelectedIndex == -1)
            {
                MakeSomeHelp.MSG("Неоходимо выбрать тип контакта", MsgBoxImage: MessageBoxImage.Error);
                return false;

            }
            else
            {
                if (!string.IsNullOrEmpty(idContactType[TypeOFContact.SelectedIndex].Item2.Trim()))
                {
                    if (!Regex.IsMatch(Value.Text.Trim(), idContactType[TypeOFContact.SelectedIndex].Item2.Trim()))
                    {
                        MakeSomeHelp.MSG("Не соответсвует требуемому значению", MsgBoxImage: MessageBoxImage.Error);
                        return false;
                    }
                }
            }

            return true;
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveSomeData.MakeSomeOperation = false;
            window.Close();
        }

        private List<string> MakeListOfTypeOfContact()
        {
            List<string> TypeOfContact = new List<string>();
            idContactType = new List<Tuple<Guid, string>>();
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
                            idContactType.Add(new Tuple<Guid, string>(id, ContactType.Rows[i]["Regex"].ToString()));
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
