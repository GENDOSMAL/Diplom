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

namespace RepairFlatWPF.UserControls.WorkerInformation.KadrWork
{
    /// <summary>
    /// Interaction logic for CreateNewWorker.xaml
    /// </summary>
    public partial class CreateNewWorker : UserControl
    {
        BaseWindow window;
        bool NewWorker = true;
        Guid idWorker = Guid.NewGuid();
        public CreateNewWorker(ref BaseWindow baseWindow,Guid idWorker=new Guid())
        {
            InitializeComponent();
            window = baseWindow;
            if (idWorker!=new Guid())
            {
                this.NewWorker = false;
                AddBtn.Content = "Редактировать";
                this.idWorker = idWorker;
            }
        }

        #region Работа с контактами
        private void AddContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RedactContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        //Выбор должности
        private void SelectPost_Click(object sender, RoutedEventArgs e)
        {

        }
        //Добавление работника
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        #region Работа с адресом
        private void RedactAdress_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddAdress_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
