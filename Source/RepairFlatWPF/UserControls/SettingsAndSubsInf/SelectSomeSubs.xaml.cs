﻿using System.Windows;
using System.Windows.Controls;


namespace RepairFlatWPF.UserControls.SettingsAndSubsInf
{
    /// <summary>
    /// Interaction logic for SelectSomeSubs.xaml
    /// </summary>
    public partial class SelectSomeSubs : UserControl
    {
        #region Переменные 

        Model.SomeEnums.TypeOfSubs typeOfSubs;
        BaseWindow BaseWindow;
        #endregion

        #region Конструкторы

        public SelectSomeSubs(Model.SomeEnums.TypeOfSubs typeOfSubs)
        {
            InitializeComponent();
            this.typeOfSubs = typeOfSubs;
            MakeLoadFromLocalBD();
        }

        public SelectSomeSubs(ref BaseWindow newWindow, Model.SomeEnums.TypeOfSubs typeOfSubs)
        {
            InitializeComponent();
            this.typeOfSubs = typeOfSubs;
            this.BaseWindow = newWindow;
            MakeLoadFromLocalBD();
        }
        #endregion

        #region Для подстановки данных
        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow.Close();
        }

        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {
            if (typeOfSubs == Model.SomeEnums.TypeOfSubs.Materials)
            {
                //Тут про материалы
            }
            else if (typeOfSubs == Model.SomeEnums.TypeOfSubs.Servises)
            {
                //Тут про услуги
            }
            else if (typeOfSubs == Model.SomeEnums.TypeOfSubs.Post)
            {
                //Тут про должности
            }
        }
        #endregion

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (typeOfSubs == Model.SomeEnums.TypeOfSubs.Materials)
            {
                //Тут про материалы
            }
            else if (typeOfSubs == Model.SomeEnums.TypeOfSubs.Servises)
            {
                //Тут про услуги
            }
            else if (typeOfSubs == Model.SomeEnums.TypeOfSubs.Post)
            {
                //Тут про должности
            }
        }

        #region Для добавления данных
        private void RetunBtnFromAddNew(object sender, RoutedEventArgs e)
        {

        }

        private void AddElement_Click(object sender, RoutedEventArgs e)
        {
            if (typeOfSubs == Model.SomeEnums.TypeOfSubs.Materials)
            {
                //Тут про материалы
            }
            else if (typeOfSubs == Model.SomeEnums.TypeOfSubs.Servises)
            {
                //Тут про услуги
            }
            else if (typeOfSubs == Model.SomeEnums.TypeOfSubs.Post)
            {
                //Тут про должности
            }
        }

        private void EditElement_Click(object sender, RoutedEventArgs e)
        {
            if (typeOfSubs == Model.SomeEnums.TypeOfSubs.Materials)
            {
                //Тут про материалы
            }
            else if (typeOfSubs == Model.SomeEnums.TypeOfSubs.Servises)
            {
                //Тут про услуги
            }
            else if (typeOfSubs == Model.SomeEnums.TypeOfSubs.Post)
            {
                //Тут про должности
            }
        }

        private void DeleteElement_Click(object sender, RoutedEventArgs e)
        {
            if (typeOfSubs == Model.SomeEnums.TypeOfSubs.Materials)
            {
                //Тут про материалы
            }
            else if (typeOfSubs == Model.SomeEnums.TypeOfSubs.Servises)
            {
                //Тут про услуги
            }
            else if (typeOfSubs == Model.SomeEnums.TypeOfSubs.Post)
            {
                //Тут про должности
            }
        }

        #endregion

        #region Дополнительные данные
        private void MakeLoadFromLocalBD()
        {
            if(typeOfSubs == Model.SomeEnums.TypeOfSubs.Materials)
            {
                //Тут про материалы
            }
            else if(typeOfSubs == Model.SomeEnums.TypeOfSubs.Servises)
            {
                //Тут про услуги
            }
            else if(typeOfSubs == Model.SomeEnums.TypeOfSubs.Post)
            {
                //Тут про должности
            }

        }

        #endregion
    }
}
