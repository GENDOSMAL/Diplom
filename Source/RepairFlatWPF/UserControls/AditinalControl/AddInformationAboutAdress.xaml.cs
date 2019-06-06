using Newtonsoft.Json;
using RepairFlat.Model;
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

namespace RepairFlatWPF.UserControls.AditinalControl
{
    /// <summary>
    /// Interaction logic for AddInformationAboutAdress.xaml
    /// </summary>
    public partial class AddInformationAboutAdress : UserControl
    {
        Guid idAdress;
        bool Redact = false;
        BaseWindow window;
        public AddInformationAboutAdress(Guid idAdress, ref BaseWindow baseWindow, bool Redact = false)
        {
            InitializeComponent();
            window = baseWindow;
            this.Redact = Redact;
            this.idAdress = idAdress;
            if (Redact)
            {
                CreateNewAdress.Content = "Обновить данные";
                MakeDataAboutAdress();
            }
        }


        private async void MakeDataAboutAdress()
        {
            var InformationFromServer = await Task.Run(() => MakeDownloadByLink($"api/adress/data?idAdress={idAdress}"));
            string ss = InformationFromServer.ToString();
            ModelAdress.DataAboutAdress listOfUserAdressInf = JsonConvert.DeserializeObject<ModelAdress.DataAboutAdress>(InformationFromServer.ToString());
            AreaName.Text = listOfUserAdressInf.AreaName?.Trim();
            CiryName.Text = listOfUserAdressInf.CityName?.Trim();
            Description.Text = listOfUserAdressInf.Desc?.Trim();
            Entrance.Text = listOfUserAdressInf.Entrance?.Trim();
            House.Text = listOfUserAdressInf.House?.Trim();
            MicroAreaName.Text = listOfUserAdressInf.MicroAreaName?.Trim();
            NumberOfDelen.Text = listOfUserAdressInf.NumberOfDelen?.Trim();
            RegionName.Text = listOfUserAdressInf.RegionName?.Trim();
            Street.Text = listOfUserAdressInf.Street?.Trim();
        }

        private async void CreateNewAdress_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                if (Redact)
                {
                    ModelAdress.DataAboutAdress modelAdress = new ModelAdress.DataAboutAdress
                    {
                        AreaName = AreaName.Text.Trim(),
                        CityName = CiryName.Text.Trim(),
                        Desc = Description.Text.Trim(),
                        Entrance = Entrance.Text.Trim(),
                        House = House.Text.Trim(),
                        idAdress = idAdress,
                        MicroAreaName = MicroAreaName.Text.Trim(),
                        NumberOfDelen = NumberOfDelen.Text.Trim(),
                        RegionName = RegionName.Text.Trim(),
                        Street = Street.Text.Trim()
                    };
                    string Json = JsonConvert.SerializeObject(modelAdress);
                    string urlSend = "api/adress/update";
                    CreateNewAdress.Content = "Ожидайте...";
                    ReturnBtn.Content = "Ожидайте...";
                    CreateNewAdress.IsEnabled = false;
                    ReturnBtn.IsEnabled = false;
                    var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(CreateNewAdress_Click)));
                    var deserializedProduct = JsonConvert.DeserializeObject<BaseResult>(task.ToString());

                    if (!deserializedProduct.success)
                    {
                        MakeSomeHelp.MSG($"Произошла ошикбка при создании пользователя {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
                    }
                    else
                    {
                        Model.SaveSomeData.MakeSomeOperation = true;
                        Model.SaveSomeData.SomeObject = modelAdress;
                        MakeSomeHelp.MSG("Данные обновлены!", MsgBoxImage: MessageBoxImage.Information);
                    }
                    window.Close();
                }
                else
                {
                    ModelAdress.DataAboutAdress modelAdress = new ModelAdress.DataAboutAdress
                    {
                        AreaName = AreaName.Text.Trim(),
                        CityName = CiryName.Text.Trim(),
                        Desc = Description.Text.Trim(),
                        Entrance = Entrance.Text.Trim(),
                        House = House.Text.Trim(),
                        idAdress = idAdress,
                        MicroAreaName = MicroAreaName.Text.Trim(),
                        NumberOfDelen = NumberOfDelen.Text.Trim(),
                        RegionName = RegionName.Text.Trim(),
                        Street = Street.Text.Trim()
                    };
                    string Json = JsonConvert.SerializeObject(modelAdress);
                    string urlSend = "api/adress/create";
                    CreateNewAdress.Content = "Ожидайте...";
                    ReturnBtn.Content = "Ожидайте...";
                    CreateNewAdress.IsEnabled = false;
                    ReturnBtn.IsEnabled = false;

                    var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(CreateNewAdress_Click)));
                    var deserializedProduct = JsonConvert.DeserializeObject<BaseResult>(task.ToString());

                    if (!deserializedProduct.success)
                    {
                        MakeSomeHelp.MSG($"Произошла ошикбка при создании пользователя {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
                    }
                    else
                    {
                        Model.SaveSomeData.MakeSomeOperation = true;
                        Model.SaveSomeData.SomeObject = modelAdress;
                        MakeSomeHelp.MSG("Данные добавлены!", MsgBoxImage: MessageBoxImage.Information);
                    }
                    window.Close();
                }
            }
        }

        public object MakeDownloadByLink(string UrlOfDownload)
        {
            return BaseWorkWithServer.CatchErrorWithGet(UrlOfDownload, "GET", nameof(MakeLoading), nameof(MakeDownloadByLink));
        }


        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private bool CheckData()
        {
            if (string.IsNullOrEmpty(RegionName.Text.Trim()))
            {
                MakeSomeHelp.MSG("Не заполнена информация о стране", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(AreaName.Text.Trim()))
            {
                MakeSomeHelp.MSG("Не заполнена информация об области", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(CiryName.Text.Trim()))
            {
                MakeSomeHelp.MSG("Не заполнена информация об городе/области", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(Street.Text.Trim()))
            {
                MakeSomeHelp.MSG("Не заполнена информация об улице", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(House.Text.Trim()))
            {
                MakeSomeHelp.MSG("Не заполнена информация о доме", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
