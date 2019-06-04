using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairFlatWPF
{
    public class SomeEnums
    {
        #region Место описани пунктов Combobox

        public static string[] StatusOfOrder = new string[] { "Принят", "В исполнении", "Выполнен", "Отмен" };

        public static string[] RypeOfSearchOrder = new string[] {"Все данные", "Статус", "Дата начала", "Фамилия клиента" };

        public static string[] TypeOfElementOfPremises= new string[] { "Оконо", "Дверь" };

        public static string[] FemaleType = new string[] { "Мужчина", "Женщина" };

        public static string[] TypeOfElement = new string[] { "Окно", "Дверь" };



        #endregion

        #region Описание DataTable

        public static string[] ContactTableDesc = new string[] { "Номер", "Тип", "Значение", "Описание" };

        public static string[] ClientTables = new string[] { "Номер", "Имя", "Фамилия", "Отчество", "Пол", "Описание" };

        public static string[] OrderMainTable = new string[] { "Номер", "Дата начала", "Статус","ФИО Клиента", "Контакная информация", "Данные об адресе","Сумма", "Описание" };

        public static string[] MeasurmentMainTable = new string[] { "Номер", "Наименование", "Описание","Длина", "Ширина", "Высота","P стен", "P пола", "S стен", "S пола" };

        public static string[] DataAboutElement = new string[] { "Номер", "Тип элемента", "Длина","Ширина", "Высота","Ширина откоса","S элемента", "Описание" };

        public static string[] WorkerTables = new string[] { "Номер", "Фамилия", "Имя",  "Отчество", "Пол", "Дата рождения" };

        public static string[] PostSubs= new string[] { "Номер", "Название должности", "Базовый оклад",  "Выполняет заказы" };

        public static string[] MaterialSubs= new string[] { "Номер", "Название материала", "Еденица измерения","Тип материала",  "Стоимость",  "Описание" };

        public static string[] ServisesSubs= new string[] { "Номер", "Название услуги", "Тип сервиса","Цена",  "Описание" };

        public static string[] ContactSubs= new string[] { "Номер", "Название",  "Описание" };

        public static string[] PremisesSubs = new string[] { "Номер", "Название",  "Описание" };


        #endregion


        public enum TypeOfAction
        {
            Add,
            Update,
            Delete
        }

        public enum TypeOfConrols
        {
            /// <summary>
            /// Работа с окном
            /// </summary>
            Window,
            /// <summary>
            /// Работа в пределах главного окна
            /// </summary>
            UserControl
        }

        public enum TypeOfUser
        {
            /// <summary>
            /// Клиент. Необходимо для мобильных приложений
            /// </summary>
            Cl,
            /// <summary>
            /// Администратор. Доступ ко всем только просмотр
            /// </summary>
            AD,
            /// <summary>
            /// Менеджер лезет везде кроме работы с финансами иди кадрами
            /// </summary>
            MG,
            /// <summary>
            /// Работник отдела кадров доступ только к работе с кадрами и все
            /// </summary>
            KW,
            /// <summary>
            /// Доступ к финансам и справочным данным
            /// </summary>
            BW,
            /// <summary>
            /// Видит все и делает все типо бог
            /// </summary>
            BB,
            /// <summary>
            /// Необходимо для мобилки если она будет но это не точно
            /// </summary>
            SW
        }

        public enum TypeOfSubs
        {
            Servises,
            Premises,
            Materials,
            Contact,
            Post
        }


        public enum TypeOfSelect
        {
            Servises,
            Premises
        }
    }
}