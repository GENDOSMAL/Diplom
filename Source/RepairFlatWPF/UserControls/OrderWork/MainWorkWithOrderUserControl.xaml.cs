﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace RepairFlatWPF.UserControls.OrderWork
{
    /// <summary>
    /// Interaction logic for MainWorkWithOrderUserControl.xaml
    /// </summary>
    public partial class MainWorkWithOrderUserControl : UserControl
    {
        Guid idOrder;

        public MainWorkWithOrderUserControl(Guid idOrder, object AllDataAboutOrder = null, string FioClient = "", string Adress = "", string AllSumma = "")
        {
            InitializeComponent();
            this.idOrder = idOrder;
            FIOClient.Text += FioClient;
            this.Adress.Text += Adress;
            SummaOfOrder.Text += Convert.ToDecimal(AllSumma);


            #region Загружаем данные обо всем заказе на соответсвующие элементы управления
            //Данные о помещениях
            ForPermisent.Children.Clear();
            ForPermisent.Children.Add(new WorkWithMeasurment(idOrder));
            //данные о заданиях
            ForTasks.Children.Clear();
            ForTasks.Children.Add(new WorkWithTasksUserControl(idOrder, ref SummaOfOrder, ref Ostatok));
            //Данные об оплате 
            ForPayment.Children.Clear();
            ForPayment.Children.Add(new InformationAboutOrderPay(idOrder, ref Ostatok, FioClient, Adress));
            //При загрузке полностью данные о заказе
            ForPrint.Children.Clear();
            ForPrint.Children.Add(new AditionalTablesUserControl(idOrder));
            #endregion
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new SelectOrderToWork());
        }

        private void SelectTabsClick(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            switch (index)
            {
                case 0:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowInformationAboutPresunt();
                    break;
                case 1:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowInformationAboutTask();
                    break;
                case 2:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowInformationAboutPayment();
                    break;
                case 3:
                    GridCursor.SetValue(Grid.ColumnProperty, index);
                    ShowInformationAboutPrint();
                    break;
            }
        }
        #region Показать соответсвующую "вкладку" (Говно код)
        private void ShowInformationAboutPresunt()
        {
            ForTasks.Visibility = Visibility.Collapsed;
            ForPermisent.Visibility = Visibility.Visible;
            ForPayment.Visibility = Visibility.Collapsed;
            ForPrint.Visibility = Visibility.Collapsed;
        }

        private void ShowInformationAboutPayment()
        {
            ForTasks.Visibility = Visibility.Collapsed;
            ForPayment.Visibility = Visibility.Visible;
            ForPermisent.Visibility = Visibility.Collapsed;
            ForPrint.Visibility = Visibility.Collapsed;
        }

        private void ShowInformationAboutPrint()
        {
            ForTasks.Visibility = Visibility.Collapsed;
            ForPrint.Visibility = Visibility.Visible;
            ForPermisent.Visibility = Visibility.Collapsed;
            ForPayment.Visibility = Visibility.Collapsed;
        }
        private void ShowInformationAboutTask()
        {
            ForTasks.Visibility = Visibility.Visible;
            ForPrint.Visibility = Visibility.Collapsed;
            ForPermisent.Visibility = Visibility.Collapsed;
            ForPayment.Visibility = Visibility.Collapsed;
        }
        #endregion

    }
}
