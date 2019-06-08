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
using static RepairFlatWPF.Model.OrderDesc;

namespace RepairFlatWPF.UserControls.OrderWork.AddInfromationUserControl
{
    /// <summary>
    /// Interaction logic for AddServisesInOrder.xaml
    /// </summary>
    public partial class AddServisesInOrder : UserControl
    {
        #region Переменные

        Guid idServis;
        bool IsSelect = false;
        int Count;
        decimal cost;
        BaseWindow window;
        #endregion

        #region Обработка событий
        public AddServisesInOrder(ref BaseWindow baseWindow, object InfromationAboutServis = null)
        {
            InitializeComponent();
            window = baseWindow;
            if (InfromationAboutServis != null)
            {
                AddServis.Content = "Редактировать";
                var dataAbout = InfromationAboutServis as TaskServises;
                idServis = dataAbout.idServis;
                NameOfServis.Text = dataAbout.NameOfServises;
                cost = dataAbout.cost ?? default;
                Cost.Text = cost.ToString();
                Count = Convert.ToInt32(dataAbout.count);
                CountOfServis.Text = Count.ToString();
                IsSelect = true;
            }
        }

        private void SelectServis_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Выбор услуги");
            baseWindow.MakeOpen(new SettingsAndSubsInf.SelectSomeSubs(ref baseWindow, SomeEnums.TypeOfSubs.Servises));
            baseWindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {
                IsSelect = true;
                idServis = SaveSomeData.idSubs;
                SaveSomeData.idSubs = new Guid();
                var row = SaveSomeData.SomeObject as DataRow;
                SaveSomeData.SomeObject = null;
                cost = decimal.Parse(row[3].ToString());
                NameOfServis.Text = row[1].ToString()?.Trim();
                Cost.Text = cost.ToString();
            }

        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void AddServis_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                TaskServises taskServises = new TaskServises
                {
                    NameOfServises = NameOfServis.Text,
                    cost = cost,
                    count = Count,
                    idServis = idServis
                };
                SaveSomeData.SomeObject = taskServises;
                SaveSomeData.MakeSomeOperation = true;
                window.Close();
            }
        }
        #endregion

        #region Дополнительно
        private bool CheckFields()
        {
            bool result = true;
            if (!IsSelect)
            {
                MakeSomeHelp.MSG("Необходимо выбрать услугу");
                result = false;
            }
            if (!int.TryParse(CountOfServis.Text.Trim(), out Count) && IsSelect)
            {
                MakeSomeHelp.MSG("Необходимо указать количество и оно должно быть целым числом",MsgBoxImage:MessageBoxImage.Hand);
                result = false;
            }
            return result;
        }
        #endregion

    }
}
