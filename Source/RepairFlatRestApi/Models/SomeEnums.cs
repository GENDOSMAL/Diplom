using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairFlatRestApi.Models
{
    public class SomeEnums
    {
        public enum TypeOfAction
        {
            AddOrUpdate,
            Update,
            Delete
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

        public enum TypeOfOperate
        {
            Adoption,
            Permutation,
            Firing
        }

    }
}