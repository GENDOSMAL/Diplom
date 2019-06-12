using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static RepairFlatWPF.Model.OrderDesc;

namespace RepairFlatWPF.UserControls.OrderWork
{
    /// <summary>
    /// Interaction logic for AditionalTablesUserControl.xaml
    /// </summary>
    public partial class AditionalTablesUserControl : UserControl
    {
        Guid idOrder;
        public AditionalTablesUserControl(Guid idOrder)
        {
            InitializeComponent();
            this.idOrder = idOrder;
            WhatPrint.Items.Add("Отдельно каждое задание");
            WhatPrint.Items.Add("Вместе все данные ");
        }

        private async void MakeSmeta_Click(object sender, RoutedEventArgs e)
        {
            if (WhatPrint.SelectedIndex != -1)
            {
                if (WhatPrint.SelectedIndex == 0)
                {//Отдельно по заданиям

                }
                else if (WhatPrint.SelectedIndex == 1)
                {//Вместе по заданиям
                    var DataDle = await Task.Run(() => MakeSomeHelp.MakeDownloadByLink($"api/Order/doc/smeta1?idOrder={idOrder}"));
                    var InfForSmeta = JsonConvert.DeserializeObject<MakeSmetaAll>(DataDle.ToString());

                    var Director = System.IO.Path.Combine(Environment.CurrentDirectory, "Temp");
                    if (!Directory.Exists(Director))
                    {
                        Directory.CreateDirectory(Director);
                    }

                    string NameOfFile = System.IO.Path.Combine(Director, "smeta1.dotx");
                    if (!File.Exists(NameOfFile))
                        File.WriteAllBytes(NameOfFile, RepairFlatWPF.Properties.Resources.Smeta);
                    using (var application = new NetOffice.WordApi.Application { Visible = true })
                    {
                        using (var document = application.Documents.Add(NameOfFile))
                        {
                            var AdressOfWork = document.Bookmarks["AdressOfWork"].Range;
                            AdressOfWork.Text = $"{InfForSmeta.AdressOfWork}";
                            var Contact = document.Bookmarks["Contact"].Range;
                            Contact.Text = $"{InfForSmeta.Contact}";
                            var FIO = document.Bookmarks["FIO"].Range;
                            FIO.Text = $"{InfForSmeta.FIO}";
                            var SummaMat = document.Bookmarks["SummaMat"].Range;
                            SummaMat.Text = $"{InfForSmeta.SummaMat}";
                            var SummaServ = document.Bookmarks["SummaServ"].Range;
                            SummaServ.Text = $"{InfForSmeta.SummaServ}";
                            if (InfForSmeta.materialsInf != null)
                            {
                                var InfAbMat = document.Bookmarks["InfAbMat"].Range.Tables[1];
                                InfAbMat.Rows[2].Cells.Delete();
                                foreach (var Mat in InfForSmeta.materialsInf)
                                {
                                    var rows = InfAbMat.Rows.Add();
                                    rows.Cells[1].Range.Text = Mat.numb.ToString();
                                    rows.Cells[2].Range.Text = Mat.NameOfMaterials.ToString();
                                    rows.Cells[3].Range.Text = Mat.count.ToString();
                                    rows.Cells[4].Range.Text = Mat.cost.ToString();
                                    rows.Cells[5].Range.Text = Mat.summa.ToString();

                                }
                            }
                            if (InfForSmeta.ServisInf != null)
                            {
                                var InfAbServ = document.Bookmarks["InfAbServ"].Range.Tables[1];
                                InfAbServ.Rows[2].Cells.Delete();
                                foreach (var Mat in InfForSmeta.ServisInf)
                                {
                                    var rows = InfAbServ.Rows.Add();
                                    rows.Cells[1].Range.Text = Mat.numb.ToString();
                                    rows.Cells[2].Range.Text = Mat.NameOfServises.ToString();
                                    rows.Cells[3].Range.Text = Mat.count.ToString();
                                    rows.Cells[4].Range.Text = Mat.cost.ToString();
                                    rows.Cells[5].Range.Text = Mat.summa.ToString();
                                }
                            }
                        }
                        application.Activate();
                    }

                }
            }
            else
            {
                MakeSomeHelp.MSG("Выберите тип печати сметы", MsgBoxImage: MessageBoxImage.Hand);
            }
        }

        private async void MakeDogovor_Click(object sender, RoutedEventArgs e)
        {
            if (MakeSomeHelp.MSG("Вы дейсвительно хотите напечатать договор для текущего заказа?", MsgBoxImage: MessageBoxImage.Question, MsgBoxButton: MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                var DataDle = await Task.Run(() => MakeSomeHelp.MakeDownloadByLink($"api/Order/doc/dogovor?idOrder={idOrder}"));
                var InfForDog = JsonConvert.DeserializeObject<MakeDogovor>(DataDle.ToString());

                var Director = System.IO.Path.Combine(Environment.CurrentDirectory, "Temp");
                if (!Directory.Exists(Director))
                {
                    Directory.CreateDirectory(Director);
                }

                string NameOfFile = System.IO.Path.Combine(Director, "dogovor.dotx");
                if (!File.Exists(NameOfFile))
                    File.WriteAllBytes(NameOfFile, RepairFlatWPF.Properties.Resources.Dogovor);
                using (var application = new NetOffice.WordApi.Application { Visible = true })
                {
                    using (var document = application.Documents.Add(NameOfFile))
                    {
                        var FIOSmall = document.Bookmarks["FIOSmall"].Range;
                        FIOSmall.Text = $"{InfForDog.FIOSmall}";
                        var Adress = document.Bookmarks["Adress"].Range;
                        Adress.Text = $"{InfForDog.Adress}";
                        var ContactInf = document.Bookmarks["ContactInf"].Range;
                        ContactInf.Text = $"{InfForDog.ContactInf}";
                        var FullFIO = document.Bookmarks["FullFIO"].Range;
                        FullFIO.Text = $"{InfForDog.FullFIO}";
                        var FullFIO1 = document.Bookmarks["FullFIO1"].Range;
                        FullFIO1.Text = $"{InfForDog.FullFIO}";
                        var Inn = document.Bookmarks["Inn"].Range;
                        Inn.Text = $"{InfForDog.Inn}";
                        var KPP = document.Bookmarks["KPP"].Range;
                        KPP.Text = $"{InfForDog.KPP}";
                        var NameOfOrganization = document.Bookmarks["NameOfOrganization"].Range;
                        NameOfOrganization.Text = $"{InfForDog.NameOfOrganization}";
                        var NameOfOrganization1 = document.Bookmarks["NameOfOrganization1"].Range;
                        NameOfOrganization1.Text = $"{InfForDog.NameOfOrganization}";
                        var Summa = document.Bookmarks["Summa"].Range;
                        Summa.Text = $"{InfForDog.Summa}";
                        var Summa1 = document.Bookmarks["Summa1"].Range;
                        Summa1.Text = $"{InfForDog.Summa}";
                    }
                    application.Activate();
                }
            }
        }

        private async void MakeSpavk_Click(object sender, RoutedEventArgs e)
        {
            if (MakeSomeHelp.MSG("Вы дейсвительно хотите напечатать справочную информацию о текущем заказе?", MsgBoxImage: MessageBoxImage.Question, MsgBoxButton: MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                var DataDle = await Task.Run(() => MakeSomeHelp.MakeDownloadByLink($"api/Order/doc/sprav?idOrder={idOrder}"));
                var InfForSpr = JsonConvert.DeserializeObject<MakeDataForSpravka>(DataDle.ToString());

                var Director = System.IO.Path.Combine(Environment.CurrentDirectory, "Temp");
                if (!Directory.Exists(Director))
                {
                    Directory.CreateDirectory(Director);
                }

                string NameOfFile = System.IO.Path.Combine(Director, "SpavkaAboutOrder.dotx");
                if (!File.Exists(NameOfFile))
                    File.WriteAllBytes(NameOfFile, RepairFlatWPF.Properties.Resources.SpavkaAboutOrder);
                using (var application = new NetOffice.WordApi.Application { Visible = true })
                {
                    using (var document = application.Documents.Add(NameOfFile))
                    {
                        if (!String.IsNullOrEmpty(InfForSpr.description?.Trim()))
                        {
                            var Description = document.Bookmarks["Description"].Range;
                            Description.Text = $"Описание: {InfForSpr.Description?.Trim()}{Environment.NewLine}";
                        }
                        if (!String.IsNullOrEmpty(InfForSpr.NumberOfDelen?.Trim()))
                        {
                            var NumberOfDelen = document.Bookmarks["NumberOfDelen"].Range;
                            NumberOfDelen.Text = $"Квартира/офис: {InfForSpr.NumberOfDelen?.Trim()}{Environment.NewLine}";
                        }
                        if (!String.IsNullOrEmpty(InfForSpr.Entrance?.Trim()))
                        {
                            var Entrance = document.Bookmarks["Entrance"].Range;
                            Entrance.Text = $"Подъезд: {InfForSpr.Entrance?.Trim()}{Environment.NewLine}";
                        }
                        if (!String.IsNullOrEmpty(InfForSpr.House?.Trim()))
                        {
                            var House = document.Bookmarks["House"].Range;
                            House.Text = $"Дом: {InfForSpr.House?.Trim()}{Environment.NewLine}";
                        }
                        if (!String.IsNullOrEmpty(InfForSpr.Street?.Trim()))
                        {
                            var Street = document.Bookmarks["Street"].Range;
                            Street.Text = $"Улица: {InfForSpr.Street?.Trim()}{Environment.NewLine}";
                        }
                        if (!String.IsNullOrEmpty(InfForSpr.MicroAreaName?.Trim()))
                        {
                            var MicroAreaName = document.Bookmarks["MicroAreaName"].Range;
                            MicroAreaName.Text = $"Микрорайон: {InfForSpr.MicroAreaName?.Trim()}{Environment.NewLine}";
                        }
                        if (!String.IsNullOrEmpty(InfForSpr.CityName?.Trim()))
                        {
                            var CityName = document.Bookmarks["CityName"].Range;
                            CityName.Text = $"Город: {InfForSpr.CityName?.Trim()}{Environment.NewLine}";
                        }
                        if (!String.IsNullOrEmpty(InfForSpr.AreaName?.Trim()))
                        {
                            var AreaName = document.Bookmarks["AreaName"].Range;
                            AreaName.Text = $"Область: {InfForSpr.AreaName?.Trim()}{Environment.NewLine}";

                        }
                        if (!String.IsNullOrEmpty(InfForSpr.RegionName?.Trim()))
                        {
                            var RegionName = document.Bookmarks["RegionName"].Range;
                            RegionName.Text = $"Страна: {InfForSpr.RegionName?.Trim()}{Environment.NewLine}";
                        }
                        var DateMake = document.Bookmarks["DateMake"].Range;
                        DateMake.Text = $"{DateTime.Now.ToString("dd.MM.yyyy")}";
                        var DateMakeOrder = document.Bookmarks["DateMakeOrder"].Range;
                        DateMakeOrder.Text = $"{InfForSpr.DateMakeOrder.Value.ToString("dd.MM.yyyy")}";
                        var DateRozd = document.Bookmarks["DateRozd"].Range;
                        DateRozd.Text = $"{InfForSpr.DateRozd.Value.ToString("dd.MM.yyyy")}";
                        var DescContact = document.Bookmarks["DescContact"].Range;
                        DescContact.Text = $"{InfForSpr.DescContact}";
                        var LastName = document.Bookmarks["LastName"].Range;
                        LastName.Text = $"{InfForSpr.LastName}";
                        var Name = document.Bookmarks["Name"].Range;
                        Name.Text = $" {InfForSpr.Name}";
                        var Patronymic = document.Bookmarks["Patronymic"].Range;
                        Patronymic.Text = $"{InfForSpr.Patronymic}";
                        var StatusOfOrder = document.Bookmarks["StatusOfOrder"].Range;
                        StatusOfOrder.Text = $" <{SomeEnums.StatusOfOrder[InfForSpr.StatusOfOrder ?? default]}>";
                        var SummaOfOrder = document.Bookmarks["SummaOfOrder"].Range;
                        SummaOfOrder.Text = $"{InfForSpr.SummaOfOrder}";
                        var TypeOfcontact = document.Bookmarks["TypeOfcontact"].Range;
                        TypeOfcontact.Text = $"{InfForSpr.TypeOfcontact}";
                        var Value = document.Bookmarks["Value"].Range;
                        Value.Text = $"{InfForSpr.Value}";

                        if (InfForSpr.Premises != null)
                        {
                            var TablePremis = document.Bookmarks["PremisesInf"].Range.Tables[1];
                            TablePremis.Rows[2].Cells.Delete();
                            foreach (var conact in InfForSpr.Premises)
                            {
                                var rows = TablePremis.Rows.Add();

                                rows.Cells[1].Range.Text = conact.number.ToString();
                                rows.Cells[2].Range.Text = conact.NameOf.ToString();
                                rows.Cells[3].Range.Text = conact.Desc.ToString();
                                rows.Cells[4].Range.Text = conact.lenght.ToString();
                                rows.Cells[5].Range.Text = conact.Width.ToString();
                                rows.Cells[6].Range.Text = conact.Height.ToString();
                                rows.Cells[7].Range.Text = conact.PWalls.ToString();
                                rows.Cells[8].Range.Text = conact.PCelling.ToString();
                                rows.Cells[9].Range.Text = conact.SWalls.ToString();
                                rows.Cells[10].Range.Text = conact.SFloor.ToString();
                            }
                        }
                    }
                    application.Activate();
                }
            }
        }
    }
}
