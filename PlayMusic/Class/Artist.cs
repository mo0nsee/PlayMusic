using PlayMusic.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlayMusic.Class
{
    /// <summary>
    /// Артист
    /// </summary>
    public class Artist : IOperationBD
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// имя артиста
        /// </summary>
        private string name = "";

        /// <summary>
        /// жанр артиста
        /// </summary>
        private int genre;

        /// <summary>
        /// имя артиста
        /// </summary>
        public string Name {
            get { return name; }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// жанр артиста
        /// </summary>
        public int Genre {
            get { return genre; }
            set
            {
                genre = value;
            }
        }

        /// <summary>
        /// Добавление элемента в бд
        /// </summary>
        public void AddElement(Object obj, ApplicationContext db)
        {
            if (CheckElement(db) == false)
            {
                if (obj is Artist artist)
                {
                    // добавляем в бд
                    db.Artists.Add(artist);
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Данный артист с таким жанром уже имеется");
            }
        }

        /// <summary>
        /// Определение id жанра
        /// </summary>
        public void GenreId(string genreText, ApplicationContext db)
        {
            var genre = db.Genres.Where(genre => genre.Title.ToLower().Trim() == genreText.ToLower().Trim()).ToList();
            if(genre.Count != 0)
            {
                Genre = genre[0].Id;
            }
        }

        public bool CheckElement(ApplicationContext db)
        {
            bool check = false;
            var countGenre = db.Artists.Where(artist => artist.Name.ToLower().Contains(Name.ToLower())).ToList();
            if (countGenre.Count > 0)
            {
                countGenre = countGenre.Where(artist => artist.Genre == Genre).ToList();
                if (countGenre.Count > 0) //Если жанр и имя совпадает
                {
                    check = true;
                }
            }
            return check;
        }

        public List<Object> OutputList(ApplicationContext db)
        {
            List<Object> listObject = new List<Object>();
            var artistList = db.Artists.ToList();
            listObject.AddRange(artistList.OfType<Artist>());
            return listObject;
        }

        public void DeleteElement(ApplicationContext db)
        {
            var artistDelete = db.Artists.Where(artist => artist.Name.Trim().ToLower() == Name.Trim().ToLower())
                                         .Where(artist => artist.Genre == Genre).ToList();

            var songCheck = db.Songs.Where(song => song.Name_artist == artistDelete[0].Id).ToList();

            if(songCheck.Count == 0) // Удаление артиста, если песен не осталось
                db.Artists.Remove(artistDelete[0]);
            db.SaveChanges();
        }
    }
}
