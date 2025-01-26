using Microsoft.Win32;
using PlayMusic.Class;
using PlayMusic.Interface;
using PlayMusic.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Numerics;
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
using System.Windows.Threading;

namespace PlayMusic
{
    /// <summary>
    /// Логика взаимодействия для PlayWindow.xaml
    /// </summary>
    public partial class PlayWindow
    {
        /// <summary>
        /// Взаимодействие с бд
        /// </summary>
        static public ApplicationContext db;
        
        /// <summary>
        /// Выбранная песня
        /// </summary>
        private Song song;

        /// <summary>
        /// Выбранная артист
        /// </summary>
        private Artist artist;

        /// <summary>
        /// Выбранный жанр
        /// </summary>
        private Genre genre;

        /// <summary>
        /// Список жанров
        /// </summary>
        private List<Object> listGenre;

        /// <summary>
        /// Список артистов
        /// </summary>
        private List<Object> listArtist;

        /// <summary>
        /// Произведение песни
        /// </summary>
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private DispatcherTimer timer;

        public PlayWindow()
        {
            db = new ApplicationContext();
            InitializeComponent();
            song = new Song();
            artist = new Artist();
            genre = new Genre();
            IOperationWindow.ShowGenre(genre, comboBoxGenre, ref listGenre);

        }

        /// <summary>
        /// Открытие окна для удаления
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DeleteSongs deleteSongs = new DeleteSongs();
            deleteSongs.Show();
            Close();
        }

        /// <summary>
        /// Открытие окна для удаления
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddSongs addSongs = new AddSongs();
            addSongs.Show();
            Close();
        }

        /// <summary>
        /// Выбор жанра
        /// </summary>
        private void comboBoxGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxArtist.Items.Clear();//Очистка артиста
            comboBoxSong.Items.Clear();//Очистка песни
            if (comboBoxGenre.SelectedValue != null)
                IOperationWindow.ShowArtist(artist, listGenre, comboBoxArtist, comboBoxGenre.SelectedValue.ToString(), ref listArtist);
        }

        /// <summary>
        /// Выбор артиста
        /// </summary>
        private void comboBoxArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxSong.Items.Clear();//Очистка песни
            if (comboBoxArtist.SelectedValue != null)
                IOperationWindow.ShowSong(song, listGenre, listArtist, comboBoxSong, comboBoxArtist.SelectedValue.ToString(), comboBoxGenre.SelectedValue.ToString());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            genre = new Genre();
            genre.Title = comboBoxGenre.Text;

            artist = new Artist();
            artist.GenreId(comboBoxGenre.Text, PlayWindow.db);
            artist.Name = comboBoxArtist.Text;


            song.NameArtistId(comboBoxArtist.Text, PlayWindow.db);
            song.Genre_song = artist.Genre;
            song.SearchTitle(comboBoxSong.Text, PlayWindow.db);

            //Открытие песни, если стоял стоп или выбрана та же самая песня
            if (mediaPlayer.CanPause == false || mediaPlayer.NaturalDuration.TimeSpan == mediaPlayer.Position)
            {
                mediaPlayer.Open(new Uri(song.Title));
            }
            else
            {//Открытие новой песни
                if (mediaPlayer.Source != new Uri(song.Title))
                {
                    sliderTime.Value = 0;
                    timer.Stop();
                    mediaPlayer.Stop();
                    mediaPlayer.Open(new Uri(song.Title));
                }
            }
            mediaPlayer.Play();

            //Настройка ползунка
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        /// <summary>
        /// Присвоение ползунку время песни
        /// </summary>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
                sliderTime.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            labelTime.Content = mediaPlayer.Position.ToString("mm\\:ss");
            sliderTime.Value += 1;
        }

        /// <summary>
        /// Пауза песни
        /// </summary>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            sliderTime.Value = 0;
            timer.Stop();
            mediaPlayer.Pause();
        }

        /// <summary>
        /// Остановка песни
        /// </summary>
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            sliderTime.Value = 0;
            timer.Stop();
            mediaPlayer.Stop();
        }

        /// <summary>
        /// Установка громкости
        /// </summary>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = sliderVolume.Value;
        }

        /// <summary>
        /// Перемотка громкости
        /// </summary>
        private void sliderTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sliderTime.Focusable == true)
            {
                TimeSpan timeSongPrev = new TimeSpan(0, 0, Convert.ToInt16(Math.Floor(sliderTime.Value)));
                mediaPlayer.Position = timeSongPrev;
                mediaPlayer.Pause();
                mediaPlayer.Play();
            }
        }
    }
}
