using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using PlayMusic.Class;
using PlayMusic.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PlayMusic.Window
{
    /// <summary>
    /// Логика взаимодействия для AddSongs.xaml
    /// </summary>
    public partial class AddSongs
    {
        /// <summary>
        /// Песня
        /// </summary>
        private Song song;

        public AddSongs()
        {
            InitializeComponent();
            song = new Song();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PlayWindow playWindow = new PlayWindow();
            playWindow.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Выбранный жанр
            Genre genre = new Genre();
            genre.Title = textTitleGenre.Text;
            genre.AddElement(genre,PlayWindow.db);

            //Выбранный артист
            Artist artist = new Artist();
            artist.GenreId(textTitleGenre.Text, PlayWindow.db);
            artist.Name = textNameArtist.Text;
            artist.AddElement(artist, PlayWindow.db);

            //Выбранная песня
            song.NameArtistId(textNameArtist.Text, PlayWindow.db);
            song.Genre_song = artist.Genre;
            song.AddElement(song, PlayWindow.db);
            song = new Song();
        }

        /// <summary>
        /// Открытие диалогового окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StringBuilder fullPath = new StringBuilder();
            fullPath.Append(song.OpenSong());
            song.Title = fullPath.ToString();
            fullPath.Replace(fullPath.ToString(), song.AbbreviationOfName());
            textTitleSong.Text = fullPath.ToString();
        }
    }
}
