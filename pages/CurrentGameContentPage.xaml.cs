using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ByWoggi.pages
{
    /// <summary>
    /// Interaction logic for CurrentGameContentPage.xaml
    /// </summary>
    public partial class CurrentGameContentPage : Page
    {
        public CurrentGameContentPage(string imagePath, string gameName, string description, DateTime? release_date)
        {
            InitializeComponent();

            DataContext = new Game
            {
                imagePath = imagePath,
                name = gameName,
                description = description,
                release_date = release_date
            };
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            var game = DataContext as Game;
            string fileName = game.name + ".torrent"; 
            string sourcePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\game_torrent", fileName); // Путь к файлу в папке проекта

            var dialog = new Microsoft.Win32.SaveFileDialog()
            {
                FileName = fileName,
                Filter = "All files (*.*)|*.*" 
            };
                
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string destPath = dialog.FileName;
                File.Copy(sourcePath, destPath, true); 
                MessageBox.Show("Файл успешно сохранён!");
            }
        }
    }
}
