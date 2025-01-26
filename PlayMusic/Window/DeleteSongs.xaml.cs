using PlayMusic.Class;
using PlayMusic.Interface;
using System;
using System.Collections.Generic;
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

namespace PlayMusic
{
    /// <summary>
    /// Логика взаимодействия для DeleteSongs.xaml
    /// </summary>
    public partial class DeleteSongs
    {
        /// <summary>
        /// Выбранная песня
        /// </summary>
        private Song song;

        /// <summary>
        /// Выбранный артист
        /// </summary>
        private Artist artist;

        /// <summary>
        /// Выбранный жанр
        /// </summary>
        private Genre genre;
        private List<Object> listGenre;
        private List<Object> listArtist;

        public DeleteSongs()
        {
            InitializeComponent();
            song = new Song();
            artist = new Artist();
            genre = new Genre();
            IOperationWindow.ShowGenre(genre, comboBoxGenre, ref listGenre);
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
            genre = new Genre();
            genre.Title = comboBoxGenre.Text;

            //Выбор артиста
            artist = new Artist();
            artist.GenreId(comboBoxGenre.Text, PlayWindow.db);
            artist.Name = comboBoxArtist.Text;

            //Выбор жанра
            song.NameArtistId(comboBoxArtist.Text, PlayWindow.db);
            song.Genre_song = artist.Genre;
            song.SearchTitle(comboBoxSong.Text, PlayWindow.db);

            song.DeleteElement(PlayWindow.db);
            artist.DeleteElement(PlayWindow.db);

            comboBoxArtist.Items.Clear();
            comboBoxSong.Items.Clear();
            //comboBoxGenre.Items.Clear();
            comboBoxGenre.SelectedValue = "";
        }

        /// <summary>
        /// Выбор жанра
        /// </summary>
        private void comboBoxGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxArtist.Items.Clear(); //Очистка артиста
            comboBoxSong.Items.Clear(); //Очистка песни
            if (comboBoxGenre.SelectedValue != null)
                IOperationWindow.ShowArtist(artist, listGenre,comboBoxArtist,comboBoxGenre.SelectedValue.ToString(), ref listArtist);
        }

        /// <summary>
        /// Выбор артиста
        /// </summary>
        private void comboBoxArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxSong.Items.Clear(); //Очистка песни
            if (comboBoxArtist.SelectedValue != null)
                IOperationWindow.ShowSong(song,listGenre,listArtist,comboBoxSong,comboBoxArtist.SelectedValue.ToString(), comboBoxGenre.SelectedValue.ToString());
        }
    }
}
