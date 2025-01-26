using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PlayMusic.Interface
{
    interface IOperationWithSongs
    {
        /// <summary>
        /// Сокращение пути, до получения имени
        /// </summary>
        string AbbreviationOfName();
        
        /// <summary>
        /// Выбор песни в файловой системы для её добавление
        /// </summary>
        /// <returns></returns>
        string OpenSong();
    }
}
