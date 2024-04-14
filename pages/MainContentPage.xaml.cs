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
    /// Interaction logic for MainContentPage.xaml
    /// </summary>
    public partial class MainContentPage : Page
    {
        private ByWoggiEntities _context;
        public MainContentPage()
        {
            InitializeComponent();
            _context = new ByWoggiEntities();
            GameListView.ItemsSource = _context.Games.ToList();
            }

    }
}
