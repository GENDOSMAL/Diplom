using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairFlatWPF.Model
{
    public class SomeEnums
    {
        #region Место описани пунктов Combobox
        public static string[] StatusOfOrder = new string[] { "Принят", "В исполнении", "Выполнен", "Отмен" };

        public static string[] RypeOfSearch = new string[] { "Номер", "Статус", "Дата начала", "Фамилия клиента" };

        #endregion

        public enum TypeOfAction
        {
            Add,
            Update,
            Delete
        }

        public enum TypeOfUser
        {
            Cl,
            AD,
            MG,
            KW,
            BW,
            BB
        }

        public enum TypeOfSubs
        {
            Servises,
            Premises,
            Materials,
            Contact
        }

    }
}