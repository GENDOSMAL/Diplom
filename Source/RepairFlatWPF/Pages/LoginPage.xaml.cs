﻿using Newtonsoft.Json;
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
using static RepairFlatWPF.LoginModel;

namespace RepairFlatWPF
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public void CheckLogin_Click(object sender, RoutedEventArgs e)
        {
            //Ip();
            var dd = new LoginWork();
            string base64Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(PasswordText.Password));

            string UrlSend = "http://repairflat.somee.com/api/main/auth";
            string Json = JsonConvert.SerializeObject(new LoginModel.MakeAuth() { login = Login.Text, password = base64Password });

            var d = BaseWorkWithServer.CatchErrorWithPost(UrlSend, "POST", Json, nameof(LoginWork), nameof(MakeAuth));
            MessageBox.Show(d.ToString());
            //Task<string> t = Task<string>.Run(() =>
            //{
            //string ss = "";
            //ss = dd.MakeAuth(Login.Text, base64Password).ToString();
            // return ss;
            //});
            //t.Wait();
            //MessageBox.Show(ss);


        }
    }
}
