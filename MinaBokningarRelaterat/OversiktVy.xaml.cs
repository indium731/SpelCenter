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


                //"oh vengance of god how you should be feared by all who read what i now see before my eyes" - Dante Alighieri
                //kommande kod tar först fram en lista på vilka spel som har en matchande mängd rekommenderade spelare i förhållande till mängden anmälda till den träffen man har översikt över
                //efter detta tar den fram en lista på bokningar som är bokade till samma tid som den bokningen man har översikt över
                //sedan räknas det ut, av de bokningar som överlappar, vilka har bokat ett spel som är med i listan av rekommenderade spel

                List<Spel> tillgangligaSpel = SpelLista.HamtaSpelLista().spel
                    .Where(spel => spel.minAntalSpelare <= bokning.anmalda.Count && 
                                spel.maxAntalSpelare >= bokning.anmalda.Count)
                    .ToList();

                List<Bokning> overlappandeBokningar = Bokningar.HamtaBokningar().bokningar
                    .Where(b => b != bokning && 
                                b.startDatum < bokning.slutDatum && 
                                b.slutDatum > bokning.startDatum)
                    .ToList();

                tillgangligaSpel.RemoveAll(spel => 
                    overlappandeBokningar.Any(b => b.bokadeSpel.Contains(spel)));

                OversiktListaLada.ItemsSource = tillgangligaSpel;
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