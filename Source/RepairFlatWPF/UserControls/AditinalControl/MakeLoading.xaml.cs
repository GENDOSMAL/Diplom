using RepairFlatWPF.Controller;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Data.SQLite;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using RepairFlatWPF.Model;
using System.Threading;
using RepairFlat.Model;

namespace RepairFlatWPF.UserControls
{
    /// <summary>
    /// Interaction logic for MakeLoading.xaml
    /// </summary>
    public partial class MakeLoading : UserControl
    {
        public MakeLoading()
        {
            InitializeComponent();
            ((Storyboard)FindResource("WaitStoryboard")).Begin();
            DescriptionOfWork.Content = "Определение даты последнего обновления ...";
            MakeDownload();
            
        }

        public async void MakeDownload()
        {
            await Task.Run(() => MakeWorkWirthDataBase.MakeFilePathAndCheck());
            string CheckLastDate = "Select Max(idUpdate), DateOfUpdate from DateOfLastUpdate;";
            var MaxDate = Task.Run(() => MakeWorkWirthDataBase.MakeSomeQueryWork(CheckLastDate, WorkWithTables: true));
            object ServisesDownload = null;
            object ContaktsDownload = null;
            object PremisesDownload = null;
            object MaterialDownload = null;
            bool withOutData = false;
            if (MaxDate.Result != null)
            {
                DataTable infAbDate = MaxDate.Result as DataTable;
                if (infAbDate.Rows.Count != 0)
                {//Берем с датой
                    DescriptionOfWork.Content = "Получение данных об услугах с сервера ...";
                    string someDate = infAbDate.Rows[0]["DateOfUpdate"].ToString();
                    //Дата получена

                    #region Получение данных от сервера
                    DescriptionOfWork.Content = "Получение данных об услугах с сервера ...";
                    ServisesDownload = await Task.Run(() => MakeDownloadByLink($"api/substring/servises/get?dateofclientlastupdate={someDate}")); //Скачивание данных о услугах
                    ContaktsDownload = await Task.Run(() => MakeDownloadByLink($"api/substring/contact/get?dateofclientlastupdate={someDate}")); //Скачивание данных о контактах
                    DescriptionOfWork.Content = "Получение данных о типах помещений с сервера ...";
                    PremisesDownload = await Task.Run(() => MakeDownloadByLink($"api/substring/premises/get?dateofclientlastupdate={someDate}")); //Скачивание данных о типах помещений
                    DescriptionOfWork.Content = "Получение данных о материалах с сервера ...";
                    MaterialDownload = await Task.Run(() => MakeDownloadByLink($"api/substring/material/get?dateofclientlastupdate={someDate}")); //Скачивание данных о материалах
                    #endregion
                }
                else
                {
                    withOutData = true;
                }
            }
            else
            {
                withOutData = true;
            }
            if (withOutData)
            {
                #region Получение данных от сервера
                DescriptionOfWork.Content = "Получение данных об услугах с сервера ...";
                ServisesDownload = await Task.Run(() => MakeDownloadByLink($"api/substring/servises/get")); //Скачивание данных о услугах
                ContaktsDownload = await Task.Run(() => MakeDownloadByLink($"api/substring/contact/get")); //Скачивание данных о контактах
                DescriptionOfWork.Content = "Получение данных о типах помещений с сервера ...";
                PremisesDownload = await Task.Run(() => MakeDownloadByLink($"api/substring/premises/get")); //Скачивание данных о типах помещений
                DescriptionOfWork.Content = "Получение данных о материалах с сервера ...";
                MaterialDownload = await Task.Run(() => MakeDownloadByLink($"api/substring/material/get")); //Скачивание данных о материалах
                #endregion
            }
            #region Преобразование данных в читаемое состояние
            DescriptionOfWork.Content = "Преобразование над данными ...";
            MakeSubs.ServisesMake ServisesMake = JsonConvert.DeserializeObject<MakeSubs.ServisesMake>(ServisesDownload.ToString());
            MakeSubs.ContactsMake ContactsMake = JsonConvert.DeserializeObject<MakeSubs.ContactsMake>(ContaktsDownload.ToString());
            MakeSubs.PremisesMake PremisesMake = JsonConvert.DeserializeObject<MakeSubs.PremisesMake>(PremisesDownload.ToString());
            MakeSubs.MaterialsMake MaterialsMake = JsonConvert.DeserializeObject<MakeSubs.MaterialsMake>(MaterialDownload.ToString());
            #endregion
            DescriptionOfWork.Content = "Запись данных в локальную базу данных ...";
            #region Загрузка данных в локальную БД
            #region Заделки под рациональность
            //List<string> NameOfColumn = new List<string>();
            //string NameOfTable = "";
            //string NameOfPk = "";
            //var ListOFTablesDescription = MakeSomeHelp.ReturnJsonOfTable();
            //if (ServisesMake.success && ServisesMake.kol != 0)
            //{
            //    if (ServisesMake.ListOfServises != null)
            //    {
            //        NameOfPk = "idServis";
            //        NameOfTable = "NameOfPk";
            //        foreach (var TableColumn in ListOFTablesDescription.Tables.Single(s => s.NameOfTable.ToLower() == "OurServices".ToLower()).ColumnOfTable)
            //        {
            //            NameOfColumn.Add(TableColumn.NameOfCol);
            //        }

            //        List<Tuple<Guid, string, string, string, decimal?, string>> ListOfServises = new List<Tuple<Guid, string, string, string, decimal?, string>>();

            //        foreach (var LstSer in ServisesMake.ListOfServises)
            //        {
            //            ListOfServises.Add(Tuple.Create(LstSer.idServises, LstSer.Nomination, LstSer.TypeOfServises, LstSer.UnitOfMeasue, LstSer.Cost, LstSer.Description));
            //        }
            //    }
            //}
            #endregion
            if (ServisesMake.success)
                await Task.Run(() => ServisesUpdlocToDB(ServisesMake));

            if (MaterialsMake.success)
                await Task.Run(() => MaterialsUpdlocToDB(MaterialsMake));

            if (PremisesMake.success)
                await Task.Run(() => PremisesUpdlocToDB(PremisesMake));

            if (ContactsMake.success)
                await Task.Run(() => ContactUpdlocToDB(ContactsMake));
            #endregion
            ((Storyboard)FindResource("WaitStoryboard")).Stop();
            DescriptionOfWork.Content = "Данные обновлены";            
            MakeSomeHelp.ShowMainGrid();
            MakeSomeHelp.DataGridMakeWork(new MainOrderUserControler());
        }

        public object MakeDownloadByLink(string UrlOfDownload)
        {
            return BaseWorkWithServer.CatchErrorWithGet(UrlOfDownload, "GET", nameof(MakeLoading), nameof(MakeDownloadByLink));
        }

        public void ServisesUpdlocToDB(MakeSubs.ServisesMake InformationAboutServises)
        {
            string MakeQuery = "Insert into OurServices (idServis,Nomination,TypeOfServices,UnitOfMeasue,Cost,Descriprtion) Values (@idServis,@Nomination,@TypeOfServices,@UnitOfMeasue,@Cost,@Descriprtion) On CONFLICT(idServis) DO UPDATE SET  Nomination=@Nomination,TypeOfServices=@TypeOfServices,UnitOfMeasue=@UnitOfMeasue,Cost=@Cost,Descriprtion=@Descriprtion;";
            MakeWorkWirthDataBase.Run((command) =>
            {
                bool MakeSmF = false;
                if (InformationAboutServises.ListOfServises != null)
                {
                    MakeSmF = true;
                    foreach (var ServisUpdate in InformationAboutServises.ListOfServises)
                    {
                        SQLiteParameter[] parameters = new SQLiteParameter[6];
                        parameters[0] = new SQLiteParameter("@idServis", ServisUpdate.idServises.ToString());
                        parameters[1] = new SQLiteParameter("@Nomination", ServisUpdate.Nomination);
                        parameters[2] = new SQLiteParameter("@TypeOfServices", ServisUpdate.TypeOfServises);
                        parameters[3] = new SQLiteParameter("@UnitOfMeasue", ServisUpdate.UnitOfMeasue);
                        parameters[4] = new SQLiteParameter("@Cost", ServisUpdate.Cost);
                        parameters[5] = new SQLiteParameter("@Descriprtion", ServisUpdate.Description);
                        command.Parameters.AddRange(parameters);
                        command.CommandText = MakeQuery;
                        command.ExecuteNonQuery();
                    }
                }
                if (InformationAboutServises.ListOfDeleteServises != null)
                {
                    MakeSmF = true;
                    string QueryForDelete = "Delete From OurServices where idServis=@idServis;";
                    foreach (var DeleteThings in InformationAboutServises.ListOfDeleteServises)
                    {
                        SQLiteParameter[] parameters = new SQLiteParameter[1];
                        parameters[0] = new SQLiteParameter("@idServis", DeleteThings.idGuid.ToString());
                        command.Parameters.AddRange(parameters);
                        command.CommandText = QueryForDelete;
                        command.ExecuteNonQuery();
                    }
                }
                if (MakeSmF)
                {
                    string QueryForHistory = "Insert into DateOfLastUpdate (TypeOfSubs,DateOfUpdate) Values (@TypeOfSubs,@DateOfUpdate)";
                    SQLiteParameter[] parameters = new SQLiteParameter[2];
                    parameters[0] = new SQLiteParameter("@TypeOfSubs", SomeEnums.TypeOfSubs.Servises.ToString());
                    parameters[1] = new SQLiteParameter("@DateOfUpdate", InformationAboutServises.DateOfMakeAnswer.ToString("dd.MM.yyyy HH:mm"));
                    command.Parameters.AddRange(parameters);
                    command.CommandText = QueryForHistory;
                    command.ExecuteNonQuery();
                }
            });
        }

        public void MaterialsUpdlocToDB(MakeSubs.MaterialsMake InformationMaterials)
        {
            string MakeQuery = "Insert into OurMaterials (idMaterials,NameOfMaterial,UnitOfMeasue,Cost,Descriprtion) Values (@idMaterials,@NameOfMaterial,@UnitOfMeasue,@Cost,@Descriprtion) On CONFLICT(idMaterials) DO UPDATE SET  NameOfMaterial=@NameOfMaterial, UnitOfMeasue=UnitOfMeasue, Cost=@Cost, Descriprtion=@Descriprtion;";
            MakeWorkWirthDataBase.Run((command) =>
            {
                bool MakeSomethink = false;
                if (InformationMaterials.listOfMaterials != null)
                {
                    MakeSomethink = true;
                    foreach (var MaterialsUpdate in InformationMaterials.listOfMaterials)
                    {
                        SQLiteParameter[] parameters = new SQLiteParameter[5];
                        parameters[0] = new SQLiteParameter("@idMaterials", MaterialsUpdate.idMaterials.ToString());
                        parameters[1] = new SQLiteParameter("@NameOfMaterial", MaterialsUpdate.NameOfMaterial);
                        parameters[2] = new SQLiteParameter("@UnitOfMeasue", MaterialsUpdate.UnitOfMeasue);
                        parameters[3] = new SQLiteParameter("@Cost", MaterialsUpdate.Cost);
                        parameters[4] = new SQLiteParameter("@Descriprtion", MaterialsUpdate.Description);
                        command.Parameters.AddRange(parameters);
                        command.CommandText = MakeQuery;
                        command.ExecuteNonQuery();
                    }
                }
                if (InformationMaterials.ListOfDeleteMaterials != null)
                {
                    MakeSomethink = true;
                    string QueryForDelete = "Delete From OurMaterials where idMaterials=@idMaterials;";
                    foreach (var DeleteThings in InformationMaterials.ListOfDeleteMaterials)
                    {
                        SQLiteParameter[] parameters = new SQLiteParameter[1];
                        parameters[0] = new SQLiteParameter("@idMaterials", DeleteThings.idGuid.ToString());
                        command.Parameters.AddRange(parameters);
                        command.CommandText = QueryForDelete;
                        command.ExecuteNonQuery();
                    }
                }
                if (MakeSomethink)
                {
                    string QueryForHistory = "Insert into DateOfLastUpdate (TypeOfSubs,DateOfUpdate) Values (@TypeOfSubs,@DateOfUpdate)";
                    SQLiteParameter[] parameters = new SQLiteParameter[2];
                    parameters[0] = new SQLiteParameter("@TypeOfSubs", SomeEnums.TypeOfSubs.Materials.ToString());
                    parameters[1] = new SQLiteParameter("@DateOfUpdate", InformationMaterials.DateOfMakeAnswer.ToString("dd.MM.yyyy HH:mm"));
                    command.Parameters.AddRange(parameters);
                    command.CommandText = QueryForHistory;
                    command.ExecuteNonQuery();
                }
            });
        }

        public void PremisesUpdlocToDB(MakeSubs.PremisesMake InformationPremises)
        {
            string MakeQuery = "Insert into PremisesType (idPremises,NameOfPremises,Descriprtion) Values (@idPremises,@NameOfPremises,@Descriprtion) On CONFLICT(idPremises) DO UPDATE SET  NameOfPremises=@NameOfPremises, Descriprtion=@Descriprtion;";
            MakeWorkWirthDataBase.Run((command) =>
            {
                bool MakeSmF = false;
                if (InformationPremises.listOfPremises != null)
                {
                    MakeSmF = true;
                    foreach (var PremisesUpdate in InformationPremises.listOfPremises)
                    {
                        SQLiteParameter[] parameters = new SQLiteParameter[3];
                        parameters[0] = new SQLiteParameter("@idPremises", PremisesUpdate.idPremises.ToString());
                        parameters[1] = new SQLiteParameter("@NameOfPremises", PremisesUpdate.Name);
                        parameters[2] = new SQLiteParameter("@Descriprtion", PremisesUpdate.Description);
                        command.Parameters.AddRange(parameters);
                        command.CommandText = MakeQuery;
                        command.ExecuteNonQuery();
                    }
                }
                if (InformationPremises.ListOfDeletePremises != null)
                {
                    MakeSmF = true;
                    string QueryForDelete = "Delete From PremisesType where idPremises=@idPremises;";
                    foreach (var DeleteThings in InformationPremises.ListOfDeletePremises)
                    {
                        SQLiteParameter[] parameters = new SQLiteParameter[1];
                        parameters[0] = new SQLiteParameter("@idPremises", DeleteThings.idGuid.ToString());
                        command.Parameters.AddRange(parameters);
                        command.CommandText = QueryForDelete;
                        command.ExecuteNonQuery();
                    }
                }
                if (MakeSmF)
                {
                    string QueryForHistory = "Insert into DateOfLastUpdate (TypeOfSubs,DateOfUpdate) Values (@TypeOfSubs,@DateOfUpdate)";
                    SQLiteParameter[] parameters = new SQLiteParameter[2];
                    parameters[0] = new SQLiteParameter("@TypeOfSubs", SomeEnums.TypeOfSubs.Premises.ToString());
                    parameters[1] = new SQLiteParameter("@DateOfUpdate", InformationPremises.DateOfMakeAnswer.ToString("dd.MM.yyyy HH:mm"));
                    command.Parameters.AddRange(parameters);
                    command.CommandText = QueryForHistory;
                    command.ExecuteNonQuery();
                }
            });
        }

        public void ContactUpdlocToDB(MakeSubs.ContactsMake InformationContact)
        {
            string MakeQuery = "Insert into ContactType (idContact,Value,Description) Values (@idContact,@Value,@Description) On CONFLICT(idContact) DO UPDATE SET  Value=@Value, Description=@Description;";
            MakeWorkWirthDataBase.Run((command) =>
            {
                bool MakeSmF = false;
                if (InformationContact.listOfContacts != null)
                {
                    MakeSmF = true;
                    foreach (var ContactUpdate in InformationContact.listOfContacts)
                    {
                        SQLiteParameter[] parameters = new SQLiteParameter[3];
                        parameters[0] = new SQLiteParameter("@idContact", ContactUpdate.idContact.ToString());
                        parameters[1] = new SQLiteParameter("@Value", ContactUpdate.Value);
                        parameters[2] = new SQLiteParameter("@Description", ContactUpdate.Description);
                        command.Parameters.AddRange(parameters);
                        command.CommandText = MakeQuery;
                        command.ExecuteNonQuery();
                    }
                }
                if (InformationContact.ListOfDeleteContacts != null)
                {
                    MakeSmF = true;
                    string QueryForDelete = "Delete From ContactType where idContact=@idContact;";
                    foreach (var DeleteThings in InformationContact.ListOfDeleteContacts)
                    {
                        SQLiteParameter[] parameters = new SQLiteParameter[1];
                        parameters[0] = new SQLiteParameter("@idContact", DeleteThings.idGuid.ToString());
                        command.Parameters.AddRange(parameters);
                        command.CommandText = QueryForDelete;
                        command.ExecuteNonQuery();
                    }
                }
                if (MakeSmF)
                {
                    string QueryForHistory = "Insert into DateOfLastUpdate (TypeOfSubs,DateOfUpdate) Values (@TypeOfSubs,@DateOfUpdate)";
                    SQLiteParameter[] parameters = new SQLiteParameter[2];
                    parameters[0] = new SQLiteParameter("@TypeOfSubs", SomeEnums.TypeOfSubs.Contact.ToString());
                    parameters[1] = new SQLiteParameter("@DateOfUpdate", InformationContact.DateOfMakeAnswer.ToString("dd.MM.yyyy HH:mm"));
                    command.Parameters.AddRange(parameters);
                    command.CommandText = QueryForHistory;
                    command.ExecuteNonQuery();
                }
            });
        }

    }
}
