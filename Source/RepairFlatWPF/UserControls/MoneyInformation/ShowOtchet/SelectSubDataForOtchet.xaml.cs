﻿using System;
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

namespace RepairFlatWPF.UserControls.MoneyInformation.ShowOtchet
{
    /// <summary>
    /// Interaction logic for SelectSubDataForOtchet.xaml
    /// </summary>
    public partial class SelectSubDataForOtchet : UserControl
    {
        BaseWindow window;
        public SelectSubDataForOtchet(ref BaseWindow baseWindow)
        {
            InitializeComponent();
            this.window = baseWindow;
        }

        private void RetunBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void GetData_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
