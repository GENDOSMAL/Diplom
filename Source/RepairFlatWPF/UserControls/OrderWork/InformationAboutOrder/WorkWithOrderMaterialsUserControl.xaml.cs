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

namespace RepairFlatWPF.UserControls.OrderWork
{
    /// <summary>
    /// Interaction logic for WorkWithOrderMaterialsUserControl.xaml
    /// </summary>
    public partial class WorkWithOrderMaterialsUserControl : UserControl
    {
        Guid idOrder;
        public WorkWithOrderMaterialsUserControl(Guid idOrder)
        {
            InitializeComponent();
            this.idOrder = idOrder;
        }

        private void DeleteMaterials_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.MSG("Не реализовано");
        }

        private void EditMaterials_Click(object sender, RoutedEventArgs e)
        {
            object f = new object();
            BaseWindow baseWindow = new BaseWindow(new UserControls.OrderWork.AddInfromationUserControl.AddInfromationAboutMaterials(idOrder,f),"Обвновление данных о материалах");
            try
            {
                baseWindow.ShowDialog();
            }
            catch
            {
                baseWindow.Close();
            }
        }

        private void AddMaterials_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow(new UserControls.OrderWork.AddInfromationUserControl.AddInfromationAboutMaterials(idOrder), "Добавление данных о материалах");
            try
            {
                baseWindow.ShowDialog();
            }
            catch
            {
                baseWindow.Close();
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.MSG("Не реализовано");
        }
    }
}