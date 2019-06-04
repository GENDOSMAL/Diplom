using Newtonsoft.Json;
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
    /// Interaction logic for PostRedactUC.xaml
    /// </summary>
    public partial class PostRedactUC : UserControl
    {
        BaseWindow window;
        Guid idPost;
        bool Redact = false;
        decimal baseWage;

        public PostRedactUC(ref BaseWindow baseWindow, object DataAboutPost = null)
        {
            InitializeComponent();            
            CanMakeWork.Items.Add("Нет");
            CanMakeWork.Items.Add("Да");
            window = baseWindow;
            if (DataAboutPost != null)
            {
                Redact = true;
                var dat = DataAboutPost as RepairFlat.Model.MakeSubs.ListOfPost;
                idPost = dat.idPost;
                NameOfPost.Text = dat.NameOfPost?.Trim();
                BaseWage.Text = dat.BaseWage.Value.ToString()?.Trim();
                if (dat.MakeWork == false)
                {
                    CanMakeWork.SelectedIndex = 0;
                }
                else
                {
                    CanMakeWork.SelectedIndex = 1;
                }
                AddBtn.Content = "Редактировать";
            }
            else
            {
                idPost = Guid.NewGuid();
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                string query = "";
                if (!Redact)
                {
                    query = "Insert into PostData (idPost,NameOfPost,BaseWage,MakeWork) values (@idPost,@NameOfPost,@BaseWage,@MakeWork)";
                }
                else
                {
                    query = "Update PostData set NameOfPost=@NameOfPost, BaseWage=@BaseWage, MakeWork=@MakeWork where idPost=@idPost;";
                }
                int index = CanMakeWork.SelectedIndex;
                SQLiteParameter[] sQLiteParameter = new SQLiteParameter[4];
                sQLiteParameter[0] = new SQLiteParameter("@idPost", idPost.ToString());
                sQLiteParameter[1] = new SQLiteParameter("@NameOfPost", NameOfPost.Text.Trim());
                sQLiteParameter[2] = new SQLiteParameter("@BaseWage", baseWage);
                sQLiteParameter[3] = new SQLiteParameter("@MakeWork", index);
                MakeWorkWirthDataBase.MakeSomeQueryWork(query, parameters: sQLiteParameter);
                MakeUpdateServer();

            }
        }
        private void MakeUpdateServer()
        {
            MakeUpdOrInsPost makeUpdOrInsPost = new MakeUpdOrInsPost();
            makeUpdOrInsPost.idUser = SaveSomeData.IdUser ?? default;
            makeUpdOrInsPost.DateOfMake = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            bool ss = Convert.ToBoolean(CanMakeWork.SelectedIndex);
            ListOfPost listOfPost = new ListOfPost { idPost = idPost, BaseWage = baseWage, MakeWork = ss,NameOfPost= NameOfPost.Text.Trim() };
            if (Redact)
            {
                makeUpdOrInsPost.listOfPostUpdate = new List<ListOfPost>();
                makeUpdOrInsPost.listOfPostUpdate.Add(listOfPost);
            }
            else
            {
                makeUpdOrInsPost.ListOfPostInsert = new List<ListOfPost>();
                makeUpdOrInsPost.ListOfPostInsert.Add(listOfPost);
            }            
            string Json = JsonConvert.SerializeObject(makeUpdOrInsPost);
            string urlSend = "api/substring/post/update";
            MakeSomeHelp.UpdloadDataToServer(urlSend, Json);
            window.Close();
        }


        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        bool CheckData()
        {
            if (string.IsNullOrEmpty(NameOfPost.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать название должности", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (CanMakeWork.SelectedIndex == -1)
            {
                MakeSomeHelp.MSG("Необходимо указать возможность работы данного типа работника", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (!decimal.TryParse(BaseWage.Text.Trim(), out baseWage) || string.IsNullOrEmpty(BaseWage.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо базовую стоимость", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            return true;
        }
    }
}
