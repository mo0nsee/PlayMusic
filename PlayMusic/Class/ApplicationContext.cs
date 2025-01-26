using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;

namespace PlayMusic.Class
{
    /// <summary>
    /// Подключение к бд
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Список песен
        /// </summary>
        public DbSet<Song> Songs => Set<Song>();

        /// <summary>
        /// Список жанров
        /// </summary>
        public DbSet<Genre> Genres => Set<Genre>();

        /// <summary>
        /// Список артистов
        /// </summary>
        public DbSet<Artist> Artists => Set<Artist>();

        /// <summary>
        /// Метод подключение к бд
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-5DQNS6K\SQLEXPRESS;Database=dbFirst;Trusted_Connection=True; Encrypt=False");
        }
    }
}
