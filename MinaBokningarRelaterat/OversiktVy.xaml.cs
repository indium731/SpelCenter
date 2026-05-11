using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;


namespace Labb1_OOP
{
    public partial class OversiktVy : UserControl
    {
        Bokning bokning;
        public OversiktVy(Bokning b)
        {
            bokning = b;
            InitializeComponent();
            InitieraOversiktLista(bokning);
            UppdateraUI();
        }
        private void UppdateraUI()
        {
            var temporar = OversiktListaLada.ItemsSource;
            OversiktListaLada.ItemsSource = null;
            OversiktListaLada.ItemsSource = temporar;
        }


        private void InitieraOversiktLista(Bokning bokning)
        {
            OversiktListaLada.ItemsSource = bokning.anmalda;
        }


        private void GaTillMinaBokningarKlick(Object sender, RoutedEventArgs e)
        {
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new MinaBokningarVy();
        }


        private void AndraValdObjekt(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ((IListBar)OversiktListaLada.SelectedItem).Detaljer();
        }


        private void AndraVisadListaKlick(Object sender, RoutedEventArgs e)
        {
            if (OversiktListaLada.ItemsSource is IEnumerable<Medlem> medlem)
            {

                List<Spel> rekommenderadeSpel = SpelLista.HamtaSpelLista().RekommenderadeSpel(bokning);
                List<Bokning> overlappandeBokningar = BokningLista.HamtaBokningLista().OverlappandeBokningar(bokning);

                rekommenderadeSpel.RemoveAll(spel => 
                    overlappandeBokningar.Any(b => b.bokadeSpel.Contains(spel)));

                OversiktListaLada.ItemsSource = rekommenderadeSpel;
                UppdateraUI();
                return;
            }

            if (OversiktListaLada.ItemsSource is IEnumerable<Spel> spel)
            {
                OversiktListaLada.ItemsSource = bokning.anmalda;
                UppdateraUI();
                return;
            }
        }
    }
}