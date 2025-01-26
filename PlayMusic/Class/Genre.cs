using PlayMusic.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PlayMusic.Class
{
    /// <summary>
    /// Жанр
    /// </summary>
    public class Genre : IOperationBD
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// название жанра
        /// </summary>
        public string Title { get; set; }

        private ApplicationContext db;

        public void AddElement(Object obj, ApplicationContext db)
        {
            if (CheckElement(db) == false)
            {
                if (obj is Genre genre)
                {
                    // добавляем в бд
                    db.Genres.Add(genre);
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Данный жанр уже имеется");
            }
        }

        public void DeleteElement(ApplicationContext db)
        {
        
        }

        public bool CheckElement(ApplicationContext db)
        {
            bool check = false;
            var countGenre = db.Genres.Where(genre => genre.Title.ToLower().Trim() == Title.ToLower().Trim()).ToList();
            if (countGenre.Count > 0)
            {
                check = true;
            }
            return check;
        }

        public List<Object> OutputList(ApplicationContext db)
        {
            List<Object> listObject = new List<Object>();
            var genreList = db.Genres.ToList();
            listObject.AddRange(genreList.OfType<Genre>());
            return listObject;
        }
    }
}
