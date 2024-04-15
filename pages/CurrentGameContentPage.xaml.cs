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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ByWoggi.pages
{
    /// <summary>
    /// Interaction logic for CurrentGameContentPage.xaml
    /// </summary>
    public partial class CurrentGameContentPage : Page
    {
        public CurrentGameContentPage(string imagePath, string gameName, string description, DateTime? release_date )
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
    }
}
