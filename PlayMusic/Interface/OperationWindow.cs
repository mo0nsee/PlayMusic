using PlayMusic.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PlayMusic.Interface
{
    interface IOperationWindow
    {
        /// <summary>
        /// Вывод жанров
        /// </summary>
        static void ShowGenre(Genre genre, ComboBox comboBoxGenre, ref List<Object> listGenre)
        {
            listGenre = genre.OutputList(PlayWindow.db);
            foreach (Genre genr in listGenre)
            {
                comboBoxGenre.Items.Add(genr.Title);
            }
        }

        /// <summary>
        /// Вывод артистов с определённым жанром
        /// </summary>
        static void ShowArtist(Artist artist, List<Object> listGenre,ComboBox comboBoxArtist, string textGenre, ref List<Object> listArtist)
        {
            listArtist = artist.OutputList(PlayWindow.db);

            foreach (Genre genre in listGenre)
            {
                if (genre.Title == textGenre)
                {
                    foreach (Artist artis in listArtist)
                    {
                        if (artis.Genre == genre.Id)
                            comboBoxArtist.Items.Add(artis.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Вывод песен с определённым жанром и артиста
        /// </summary>
        static void ShowSong(Song song, List<Object> listGenre, List<Object> listArtist,ComboBox comboBoxSong, string textArtistName, string textGenre)
        {
            List<Object> listSong = song.OutputList(PlayWindow.db);

            foreach (Genre genre in listGenre)
            {
                if (genre.Title == textGenre)
                {
                    foreach (Artist artist in listArtist)
                    {
                        if (artist.Name == textArtistName)
                        {
                            foreach (Song son in listSong)
                            {
                                if (son.Name_artist == artist.Id && son.Genre_song == genre.Id)
                                    comboBoxSong.Items.Add(son.AbbreviationOfName());
                            }
                        }
                    }
                }
            }
        }

    }
}
