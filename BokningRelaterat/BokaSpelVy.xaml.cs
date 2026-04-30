


using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class BokaSpelVy : UserControl
    {
        public BokaSpelVy(Bokning bokning)
        {
            InitializeComponent();
            BokaSpelKnapp.Tag = bokning;
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

            SpelListaLada.ItemsSource = null;
            List<Bokning> overlappandeBokningar = (Bokningar.HamtaBokningar().bokningar.Where(bokning => bokning.startDatum < ((Bokning)BokaSpelKnapp.Tag).slutDatum && ((Bokning)BokaSpelKnapp.Tag).startDatum < bokning.slutDatum)).ToList();
            List<Spel> tillgangligaSpel = new List<Spel>();
            foreach (Spel spel in SpelLista.HamtaSpelLista().spel)
            {
                if (overlappandeBokningar.Where(bokning => bokning.bokadeSpel.Contains(spel)).Count() == 0)
                {
                    tillgangligaSpel.Add(spel);
                }
            }
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
            Bokning bokning = (Bokning)BokaSpelKnapp.Tag;
            bokning.BokaSpel((Spel)SpelListaLada.SelectedItem);

            UppdateraUI();
        }

        private void TaBortValdSpelKlick(Object sender, RoutedEventArgs e)
        {
            if (SpelListaLada.SelectedItem is not Spel valdSpel)
            {
                DetaljTextLada.Text = "Välj ett spel att ta bort";
                return;
            }
            
            SpelLista.HamtaSpelLista().spel.Remove(valdSpel);
            UppdateraUI();
            DetaljTextLada.Text = "Inget Spel vald";

        }

        private void AndraValdSpel(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SpelListaLada.SelectedItem is not Spel valdSpel)
            {
                DetaljTextLada.Text = "Inget spel vald";
                return;
            }


            DetaljTextLada.Text = 
                $"Namn: {valdSpel.namn}\n" +
                $"Kategori: {valdSpel.kategori}\n" +
                $"Beskrivning: {valdSpel.beskrivning}";
        }

    }
}

