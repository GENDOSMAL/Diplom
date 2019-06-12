using Newtonsoft.Json;
using RepairFlat.Model;
using RepairFlatWPF.Controller;
using RepairFlatWPF.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static RepairFlat.Model.MakeSubs;

namespace RepairFlatWPF.UserControls.SettingsAndSubsInf.ControlForRedact
{
    /// <summary>
    /// Interaction logic for MaterialsRedactUC.xaml
    /// </summary>
    public partial class MaterialsRedactUC : UserControl
    {
        BaseWindow window;
        Guid idMaterial;
        bool Redact = false;
        decimal cost;
        public MaterialsRedactUC(ref BaseWindow baseWindow, object DataAboutMaterial = null)
        {
            InitializeComponent();
            window = baseWindow;
            if (DataAboutMaterial != null)
            {
                var obf = DataAboutMaterial as RepairFlat.Model.MakeSubs.ListOfMaterials;
                NameOfMaterial.Text = obf.NameOfMaterial?.Trim();
                UnitOfMeasue.Text = obf.UnitOfMeasue?.Trim();
                Cost.Text = obf.Cost.ToString()?.Trim();
                TypeOfMaterial.Text = obf.TypeOfMaterial?.Trim();
                Descriprtion.Text = obf.Description?.Trim();
                Redact = true;
                idMaterial = obf.idMaterials;
                AddBtn.Content = "Редактировать";
            }
            else
            {
                idMaterial = Guid.NewGuid();
            }

        }

        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                string query = "";
                if (!Redact)
                {
                    query = "Insert into ContactType (idMaterials,NameOfMaterial,UnitOfMeasue,Cost,TypeOfMaterial,Descriprtion) values (@idMaterials,@NameOfMaterial,@UnitOfMeasue,@Cost,@TypeOfMaterial,@Descriprtion)";
                }
                else
                {
                    query = "Update OurMaterials set   NameOfMaterial=@NameOfMaterial, UnitOfMeasue=@UnitOfMeasue, Cost=@Cost, TypeOfMaterial=@TypeOfMaterial, Descriprtion=@Descriprtion where idMaterials=@idMaterials;";
                }
                SQLiteParameter[] sQLiteParameter = new SQLiteParameter[6];
                sQLiteParameter[0] = new SQLiteParameter("@idMaterials", idMaterial.ToString());
                sQLiteParameter[1] = new SQLiteParameter("@NameOfMaterial", NameOfMaterial.Text.Trim());
                sQLiteParameter[2] = new SQLiteParameter("@UnitOfMeasue", UnitOfMeasue.Text.Trim());
                sQLiteParameter[3] = new SQLiteParameter("@Cost", cost);
                sQLiteParameter[4] = new SQLiteParameter("@TypeOfMaterial", TypeOfMaterial.Text.Trim());
                sQLiteParameter[5] = new SQLiteParameter("@Descriprtion", Descriprtion.Text.Trim());
                MakeWorkWirthDataBase.MakeSomeQueryWork(query, parameters: sQLiteParameter);
                MakeUpdateServer();
            }
        }

        private async void MakeUpdateServer()
        {
            MakeUpdOrInsMaterials makeUpdOrInsMaterials = new MakeUpdOrInsMaterials();
            makeUpdOrInsMaterials.idUser = SaveSomeData.IdUser ?? default;
            makeUpdOrInsMaterials.DateOfMake = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            makeUpdOrInsMaterials.ListOfMaterials = new List<ListOfMaterials>();
            ListOfMaterials listOfContacts = new ListOfMaterials { Cost = cost, Description = Descriprtion.Text.Trim(), idMaterials = idMaterial, NameOfMaterial = NameOfMaterial.Text.Trim(), TypeOfMaterial = TypeOfMaterial.Text.Trim(), UnitOfMeasue = UnitOfMeasue.Text.Trim() };
            makeUpdOrInsMaterials.ListOfMaterials.Add(listOfContacts);
            string Json = JsonConvert.SerializeObject(makeUpdOrInsMaterials);
            string urlSend = "api/substring/material/update";
            var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(MakeUpdateServer)));
            var deserializedProduct = JsonConvert.DeserializeObject<BaseResult>(task.ToString());
            if (!deserializedProduct.success)
            {
                MakeSomeHelp.MSG($"Произошла ошикбка при работе {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
            }
            else
            {
                MakeSomeHelp.MSG("Операции над данными были произведены!", MsgBoxImage: MessageBoxImage.Information);
            }
            window.Close();
        }
        bool CheckData()
        {
            if (string.IsNullOrEmpty(NameOfMaterial.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать значение для материала", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (string.IsNullOrEmpty(UnitOfMeasue.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать еденицу измерения для материала", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (string.IsNullOrEmpty(TypeOfMaterial.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать тип материала", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (string.IsNullOrEmpty(Descriprtion.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать описание для материала", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (!decimal.TryParse(Cost.Text.Trim(), out cost) || string.IsNullOrEmpty(Cost.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать стоимость материала", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            return true;
        }
    }
}
