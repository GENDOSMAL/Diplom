using Newtonsoft.Json;
using RepairFlat.Model;
using RepairFlatWPF.Model;
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
using static RepairFlat.Model.PersonDesctiption;

namespace RepairFlatWPF.UserControls
{
    /// <summary>
    /// Interaction logic for AddUserControl.xaml
    /// </summary>
    public partial class AddUserControl : UserControl
    {
        #region Переменные 
        Guid idUser;
        bool NewData = true;
        DataTable TableOfContactInformation;
        BaseWindow window;
        /// <summary>
        /// 1- номер в таблице
        /// 2- уникальный номер контакта
        /// 3- уникальный номер типа контакта
        /// </summary>
        List<Tuple<int, Guid, Guid>> IdContactAndTypeInTable = new List<Tuple<int, Guid, Guid>>();

        #endregion

        #region Констурктор и обработчик
        public AddUserControl(ref BaseWindow baseWindow, object InformationAboutClient = null)
        {
            InitializeComponent();
            window = baseWindow;
            foreach (string Type in SomeEnums.FemaleType)
            {
                Female.Items.Add(Type);
            }

            TableOfContactInformation = new DataTable();

            foreach (string NameOfColumn in SomeEnums.ContactTableDesc)
            {
                TableOfContactInformation.Columns.Add(NameOfColumn);
            }

            if (InformationAboutClient != null)
            {
                AddBtn.Content = "Редактировать";
                NewData = false;
            }
            else
            {
                idUser = Guid.NewGuid();
            }
        }

        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                if (NewData)
                {
                    //Тут добавление
                    List<ContactModel.InformationAboutContact> ContactInformatio = new List<ContactModel.InformationAboutContact>();
                    if (TableOfContactInformation.Rows.Count != 0)
                    {
                        for (int i = 0; i < TableOfContactInformation.Rows.Count; i++)
                        {
                            Guid idContact = IdContactAndTypeInTable.Where(e2 => e2.Item1 == Convert.ToInt32(TableOfContactInformation.Rows[i][0])).Select(e1 => e1.Item2).First();
                            Guid idType = IdContactAndTypeInTable.Where(e2 => e2.Item1 == Convert.ToInt32(TableOfContactInformation.Rows[i][0])).Select(e1 => e1.Item3).First();
                            ContactModel.InformationAboutContact contact = new ContactModel.InformationAboutContact()
                            {
                                DateAdd = DateTime.Now,
                                Desctription = TableOfContactInformation.Rows[i][3].ToString(),
                                idContact = idContact,
                                idTypeOfContact = idType,
                                idUser = idUser,
                                Value = TableOfContactInformation.Rows[i][2].ToString()
                            };
                            ContactInformatio.Add(contact);
                        }
                    }
                    else
                    {
                        ContactInformatio = null;
                    }
                    CreateNewClient createNewClient = new CreateNewClient
                    {
                        idUser = idUser,
                        Birstday = DateOfBirsd.SelectedDate.Value,
                        Desc = Description.Text.Trim(),
                        Female = Female.SelectedIndex + 1,
                        Lastname = Famil.Text.Trim(),
                        Name = Name.Text.Trim(),
                        Pasport = Pasport.Text.Trim(),
                        Patronymic = Patronymic.Text.Trim(),
                        TypeOfUser = SomeEnums.TypeOfUser.Cl.ToString(),
                        ListOfContact = ContactInformatio
                    };
                    string Json = JsonConvert.SerializeObject(createNewClient);
                    string urlSend = "api/user/create";
                    AddBtn.Content = "Ожидайте...";
                    RetutnBTN.Content = "Ожидайте...";
                    AddBtn.IsEnabled = false;
                    RetutnBTN.IsEnabled = false;

                    var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(AddBtn_Click)));
                    var deserializedProduct = JsonConvert.DeserializeObject<BaseResult>(task.ToString());

                    if (!deserializedProduct.success)
                    {
                        MakeSomeHelp.MSG($"Произошла ошикбка при создании пользователя {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
                    }
                    else
                    {
                        MakeSomeHelp.MSG("Данные добавлены!", MsgBoxImage: MessageBoxImage.Information);
                    }
                    window.Close();
                }
                else
                {
                    //Тут обновление
                }
            }
        }

        private void AddElement_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Добавление контактной информации");
            baseWindow.MakeOpen(new AddContactUserConrol(idUser, ref baseWindow));
            baseWindow.ShowDialog();

        }

        private void RedactElement_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                BaseWindow baseWindow = new BaseWindow("Редактирование контактной информации");
                baseWindow.MakeOpen(new AddContactUserConrol(idUser, ref baseWindow, idUser));
                baseWindow.ShowDialog();
            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать строку для редактирования", MsgBoxImage: MessageBoxImage.Error);
            }
        }

        private void DeleteElement_Click(object sender, RoutedEventArgs e)
        {

            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {

            }
            else
            {
                MakeSomeHelp.MSG("Необходимо выбрать строку для редактирования", MsgBoxImage: MessageBoxImage.Error);
            }

        }

        #endregion

        #region Прочие обработки

        private bool CheckFields()
        {
            if (string.IsNullOrEmpty(Name.Text.Trim()))
            {
                MakeSomeHelp.MSG("Укажите данные о имени клиента", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(Famil.Text.Trim()))
            {
                MakeSomeHelp.MSG("Укажите данные о фамилии клиента", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(Patronymic.Text.Trim()))
            {
                MakeSomeHelp.MSG("Укажите данные об отчестве клиента", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(Pasport.Text.Trim()))
            {
                MakeSomeHelp.MSG("Укажите данные об паспорте клиента", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (Female.SelectedIndex == -1)
            {
                MakeSomeHelp.MSG("Укажите данные об половой принаждежности клиента", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (!DateOfBirsd.SelectedDate.HasValue)
            {
                MakeSomeHelp.MSG("Укажите данные об дате рождения", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        #endregion


    }
}
