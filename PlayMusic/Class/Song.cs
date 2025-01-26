using Microsoft.Win32;
using PlayMusic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace PlayMusic.Class
{
    /// <summary>
    /// Песня
    /// </summary>
    public class Song : IOperationWithSongs,IOperationBD
    {
        private ApplicationContext db;

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// название песни
        /// </summary>
        private string title = "";

        /// <summary>
        /// Имя артиста
        /// </summary>
        private int name_artist;

        /// <summary>
        /// Жанр песни
        /// </summary>
        private int genre_song;

        /// <summary>
        /// название песни
        /// </summary>
        public string Title 
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// Имя артиста
        /// </summary>
        public int Name_artist
        {
            get { return name_artist; }
            set
            {
                name_artist = value;
            }
        }

        /// <summary>
        /// Жанр песни
        /// </summary>
        public int Genre_song
        {
            get { return genre_song; }
            set
            {
                genre_song = value;
            }
        }

        public string AbbreviationOfName() 
        {
            string[] strings = title.Split('\\');
            return strings[strings.Length - 1];
        }

        public string OpenSong()
        {
            StringBuilder fullPath = new StringBuilder();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                fullPath.Append(openFileDialog.FileName);
            return fullPath.ToString();
        }

        /// <summary>
        /// Получение id Артиста
        /// </summary>
        public void NameArtistId(string nameText, ApplicationContext db)
        {
            var artist = db.Artists.Where(genre => genre.Name.ToLower().Contains(nameText.ToLower())).ToList();
            if (artist.Count != 0)
            {
                Name_artist = artist[0].Id;
            }
        }

        /// <summary>
        /// Поиск песни
        /// </summary>
        public void SearchTitle(string songText, ApplicationContext db)
        {
            var song = db.Songs.Where(song => song.Title.ToLower().Contains(songText.ToLower()))
                               .Where(song => song.Name_artist == Name_artist)
                               .Where(song => song.Genre_song == Genre_song).ToList();
            if (song.Count != 0)
            {
                Title = song[0].Title;
            }
        }

        public bool CheckElement(ApplicationContext db)
        {
            bool check = false;
            var countSong = db.Songs.Where(song => song.Title.ToLower().Contains(Title.ToLower())).ToList();
            if (countSong.Count > 0)
            {
                countSong = countSong.Where(song => song.Genre_song == Genre_song)
                                     .Where(song => song.Name_artist == Name_artist).ToList();
                if (countSong.Count > 0)
                {
                    check = true;
                }
            }
            return check;
        }

        public void AddElement(Object obj, ApplicationContext db)
        {
            if (CheckElement(db) == false)
            {
                if (obj is Song song)
                {
                    // добавляем в бд
                    db.Songs.Add(song);
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Данная песня с таким исполнителем и жанром уже имеется");
            }
        }

        public void DeleteElement(ApplicationContext db)
        {
            var songDelete = db.Songs.Where(song => song.Title.Trim().ToLower() == Title.Trim().ToLower())
                                     .Where(song => song.Name_artist == Name_artist)
                                     .Where(song => song.Genre_song == Genre_song).ToList();
            db.Songs.Remove(songDelete[0]);
            db.SaveChanges();
        }

        public List<Object> OutputList(ApplicationContext db)
        {
            List<Object> listObject = new List<Object>();
            var songList = db.Songs.ToList();
            listObject.AddRange(songList.OfType<Song>());
            return listObject;
        }
    }
}
