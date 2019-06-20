namespace RepairFlatWPF
{
    public class SomeEnums
    {
        #region Место описани пунктов Combobox

        public static string[] StatusOfOrder = new string[] { "Принят", "В исполнении", "Выполнен", "Отмен" };

        public static string[] RypeOfSearchOrder = new string[] { "Все данные", "Дата начала", "Фамилия клиента" };

        public static string[] TypeOfElementOfPremises = new string[] { "Оконо", "Дверь" };

        public static string[] FemaleType = new string[] { "Мужчина", "Женщина" };

        public static string[] TypeOfElement = new string[] { "Окно", "Дверь" };

        public static string[] RoleOfWorker = new string[] { "Прораб", "Рабочий" };



        #endregion

        #region Описание DataTable

        public static string[] ContactTableDesc = new string[] { "Номер", "Тип", "Значение", "Описание" };

        public static string[] ClientTables = new string[] { "Номер", "Имя", "Фамилия", "Отчество", "Пол", "Описание" };

        public static string[] OrderMainTable = new string[] { "Номер", "Дата начала", "Статус", "ФИО Клиента", "Контакная информация", "Данные об адресе", "Сумма", "Описание" };

        public static string[] MeasurmentMainTable = new string[] { "Номер", "Наименование", "Описание", "Длина", "Ширина", "Высота", "P стен", "P пола", "S стен", "S пола" };

        public static string[] DataAboutElement = new string[] { "Номер", "Тип элемента", "Длина", "Ширина", "Толщина", "P элемента", "Описание" };

        public static string[] WorkerTables = new string[] { "Номер", "Фамилия", "Имя", "Отчество", "Пол", "Дата рождения" };

        public static string[] WorkerTablesRedact = new string[] { "Номер", "Фамилия", "Имя", "Отчество", "Пол", "Дата рождения", "Должность", "Оклад" };

        public static string[] PostSubs = new string[] { "Номер", "Название должности", "Базовый оклад", "Выполняет заказы" };

        public static string[] MaterialSubs = new string[] { "Номер", "Название материала", "Еденица измерения", "Тип материала", "Стоимость", "Описание" };

        public static string[] ServisesSubs = new string[] { "Номер", "Название услуги", "Тип сервиса", "Цена", "Описание" };



        public static string[] ContactSubs = new string[] { "Номер", "Название", "Описание", "Регулярное выражение" };

        public static string[] PremisesSubs = new string[] { "Номер", "Название", "Описание" };


        public static string[] WorkerTask = new string[] { "Номер", "ФИО работника", "Роль" };

        public static string[] ServisesTask = new string[] { "Номер", "Наименование услуги", "Количество", "Стоимость", "Описание" };

        public static string[] ServisesMaterials = new string[] { "Номер", "Наименование материала", "Количество", "Стоимость", "Описание" };


        public static string[] PayInf = new string[] { "Номер", "Дата создания", "Сумма оплаты", "Кто создал", "Описание" };


        #region Описание статистики
        public static string[] InformationAboutSalary = new string[] { "№", "ФИО работника", "Должность", "Размер", "Дата" };

        public static string[] InformationAboutOrderPay = new string[] { "№", "ФИО клиента", "Сумма", "Описание", "Дата", "ФИО работника" };

        public static string[] InformationAboutServStat = new string[] { "№", "Наименование", "Тип услуги", "Стоимость", "Количество", "Сумма" };

        public static string[] InformationAboutMatStat = new string[] { "№", "Наименование", "Еденицы измерения", "Стоимость", "Количество", "Сумма" };


        public enum TypeOfReport
        {
            AboutSalary,
            AboutOrderPayment
        }

        #endregion



        #region Описание задания
        public static string[] TaskTable = new string[] { "Номер", "Цена", "Описание", "Дата начала", "Планируемое время завершения", };

        public static string[] TaskMaterialTable = new string[] { "Номер", "Название", "Количество", "Стоимость", "Сумма" };

        public static string[] TaskServisTable = new string[] { "Номер", "Название", "Количество", "Стоимость", "Сумма" };

        public static string[] TaskWorkerTable = new string[] { "Номер", "ФИО работника", "Должность", "Роль работника" };


        #endregion
        #endregion


        public enum TypeOfAction
        {
            AddOrUpdate,
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
            SW,
            /// <summary>
            /// Кандидат либо уволенный
            /// </summary>
            KD

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
        public enum TypeOfUserNeed
        {
            KD,
            All,
            ForOrder,
            ForRedact,
            forpayment

        }
        public enum TypeOfOperate
        {
            Adoption,
            Permutation,
            Firing
        }
    }
}