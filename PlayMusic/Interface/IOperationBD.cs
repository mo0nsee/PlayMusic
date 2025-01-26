using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PlayMusic.Class;

namespace PlayMusic.Interface
{
    interface IOperationBD
    {
        /// <summary>
        /// Добавление элемента в бд
        /// </summary>
        void AddElement(Object obj, ApplicationContext db);

        /// <summary>
        /// Содержит ли бд данный элемент
        /// </summary>
        bool CheckElement(ApplicationContext db);

        /// <summary>
        /// Вывод списка элементов из бд
        /// </summary>
        /// <param name="db"></param>
        List<Object> OutputList(ApplicationContext db);

        /// <summary>
        /// Удаление элементов из бд
        /// </summary>
        void DeleteElement(ApplicationContext db);
    }
}
