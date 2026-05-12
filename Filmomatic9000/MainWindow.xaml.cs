using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;

namespace Filmomatic9000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetFilms();
        }

        private void GetFilms()
        {
            FilmList.Items.Clear();
            List<string> results = new Film().where(UserQuery.Text.ToString());
            foreach (string result in results)
            {
                FilmList.Items.Add(result);
            }
        }

        private void CreateFilm()
        {
            Film film = new Film();
            film.title = ""; // Go get this from a form
            film.description = ""; // Go get *this* from a form
            try
            {
                film.save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetFilms();   
        }

        private void Get_Single_Film_Click(object sender, RoutedEventArgs e)
        {
            Film twentiethFilm = new Film();
            twentiethFilm.Id = 20;

            twentiethFilm.get();
            FilmSingle.Items.Add($"{twentiethFilm.title}, {twentiethFilm.description}");
        }

        //private void UserQuery_KeyUp(object sender, KeyEventArgs e)
        //{   
        //    GetFilms();
        //}
    }
}