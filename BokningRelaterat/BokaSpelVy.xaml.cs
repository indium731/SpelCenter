


using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class BokaSpelVy : UserControl
    {
        Bokning bokning;
        public BokaSpelVy(Bokning b)
        {
            InitializeComponent();
            bokning = b;
            InitieraLista();
            UppdateraUI();
        }

        private void InitieraLista()
        {
            //Jag ber om ursäkt för denna synd
            //  men vad koden gör är att den separerar ut om några tidigare bokningar överlappar
            //  tidsmässigt med en man försöker lägga.
            //  Sedan tar den reda på om någon av dessa tidigare,
            //  överlappande bokningarna har bokat upp ett givet spel.
            //  Om spelet inte är bokat under denna tid så läggs den till i listan av spel som kan bokas.

            // 2. Find all bookings that clash with the selected dates
            var overlappandeBokningar = BokningLista.HamtaBokningLista().bokningar
                .Where(b => b.startDatum < bokning.slutDatum && bokning.startDatum < b.slutDatum)
                .ToList();

            // 3. Filter the games: Select games NOT found in the list of overlapping games
            var tillgangligaSpel = SpelLista.HamtaSpelLista().spel
                .Where(spel => !overlappandeBokningar.Any(b => b.bokadeSpel.Contains(spel)))
                .ToList();

            // 4. Update the UI
            SpelListaLada.ItemsSource = tillgangligaSpel;
        }

        private void UppdateraUI()
        {
            var lista = SpelListaLada.ItemsSource;
            SpelListaLada.ItemsSource = null;
            SpelListaLada.ItemsSource = lista;

        }

        private void GaTillMeny(Object sender, RoutedEventArgs e)
        {
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new MedlemMenyVy();
        }

        private void TestaBokaSpelKlick(Object sender, RoutedEventArgs e)
        {
            if (BokaSpelKnapp.Tag is not Bokning bokning) return;
            if (SpelListaLada.SelectedItem is not Spel spel) return;
            bokning.BokaSpel(spel);

            UppdateraUI();
        }

        private void AvbokaValdSpelKlick(Object sender, RoutedEventArgs e)
        {
            if (BokaSpelKnapp.Tag is not Bokning bokning) return;
            if (SpelListaLada.SelectedItem is not Spel spel) return;
            bokning.AvBokaSpel(spel);

            UppdateraUI();
        }

        private void AndraValdSpel(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SpelListaLada.SelectedItem is not Spel valdSpel)
            {
                DetaljTextLada.Text = "Inget spel vald";
                return;
            }


            DetaljTextLada.Text = valdSpel.Detaljer(); 
        }

    }
}

