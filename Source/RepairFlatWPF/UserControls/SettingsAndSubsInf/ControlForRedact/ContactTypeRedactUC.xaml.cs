﻿using Newtonsoft.Json;
using RepairFlat.Model;
using RepairFlatWPF.Controller;
using RepairFlatWPF.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
using static RepairFlat.Model.MakeSubs;

namespace RepairFlatWPF.UserControls.SettingsAndSubsInf.ControlForRedact
{
    /// <summary>
    /// Interaction logic for ContactTypeRedactUC.xaml
    /// </summary>
    public partial class ContactTypeRedactUC : UserControl
    {
        BaseWindow window;
        Guid idContact;
        bool Redact = false;
        public ContactTypeRedactUC(ref BaseWindow baseWindow, Guid idContact = new Guid(),string value="",string description="")
        {
            InitializeComponent();
            this.window = baseWindow;
            if (idContact == new Guid())
            {
                this.idContact = Guid.NewGuid();
            }
            else
            {
                Redact = true;
                this.idContact = idContact;
                Value.Text = value?.Trim();
                Description.Text = description?.Trim();
                AddBtn.Content = "Редактировать";
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Check())
            {
                string query = "";
                if (!Redact)
                {
                    query = "Insert into ContactType (idContact,Value,Description) values (@idContact,@Value,@Description)";
                }
                else
                {
                    query = "Update ContactType set  Value=@Value , Description=@Description where idContact=@idContact;";
                }
                SQLiteParameter[] sQLiteParameter = new SQLiteParameter[3];
                sQLiteParameter[0] = new SQLiteParameter("@idContact", idContact.ToString());
                sQLiteParameter[1] = new SQLiteParameter("@Value", Value.Text.Trim());
                sQLiteParameter[2] = new SQLiteParameter("@Description", Description.Text.Trim());
                MakeWorkWirthDataBase.MakeSomeQueryWork(query, parameters: sQLiteParameter);
                MakeUpdateServer();
            }
        }

        private  void MakeUpdateServer()
        {
            MakeUpdOrInsContacts makeUpdOrInsContacts = new MakeUpdOrInsContacts();
            makeUpdOrInsContacts.idUser = SaveSomeData.IdUser ?? default;
            makeUpdOrInsContacts.DateOfMake = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            makeUpdOrInsContacts.ListOfContacts = new List<ListOfContacts>();
            ListOfContacts listOfContacts = new ListOfContacts { Value = Value.Text.Trim(), idContact = idContact, Description = Description.Text.Trim() };
            makeUpdOrInsContacts.ListOfContacts.Add(listOfContacts);
            string Json = JsonConvert.SerializeObject(makeUpdOrInsContacts);
            string urlSend = "api/substring/contact/update";
            MakeSomeHelp.UpdloadDataToServer(urlSend, Json);
            window.Close();
        }

        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }
        private bool Check()
        {
            if (string.IsNullOrEmpty(Value.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать значение для типа контакной информации", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (string.IsNullOrEmpty(Value.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать описание для типа контакной информации", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            return true;
        }
    }
}