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

namespace RepairFlatWPF.UserControls.SettingsAndSubsInf
{
    /// <summary>
    /// Interaction logic for RedactSomeSubs.xaml
    /// </summary>
    public partial class RedactSomeSubs : UserControl
    {
        SomeEnums.TypeOfSubs typeOfSubs;
        DataTable DataAboutSomeSubInf;
        List<Tuple<int, Guid>> ListofId;
        public RedactSomeSubs(SomeEnums.TypeOfSubs typeOfSubs)
        {
            InitializeComponent();
            this.typeOfSubs= typeOfSubs;
            
        }

        private void MakePreparateData()
        {
            DataAboutSomeSubInf=new DataTable();
            ListofId = new List<Tuple<int, Guid>>();
        }

        private void MakeDataGrid()
        {
            MakePreparateData();
            if (typeOfSubs == SomeEnums.TypeOfSubs.Materials)
            {//Материалы

            }
            else if (typeOfSubs == SomeEnums.TypeOfSubs.Post)
            {//Дожности

            }
            else if (typeOfSubs == SomeEnums.TypeOfSubs.Servises)
            {//Услуги

            }
        }

        private void RetunBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddElement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditElement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteElement_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
