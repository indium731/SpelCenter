
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class OversiktVy : UserControl
    {
        public OversiktVy(Bokning bokning)
        {
            InitializeComponent();
            InitieraOversiktLista(bokning);
            OversiktListaLada.Tag = bokning;
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
            AndraVisadListaKnapp.Tag = "personer";
        }

        private void GaTillMinaBokningarKlick(Object sender, RoutedEventArgs e)
        {
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new MinaBokningarVy();
        }

        private void AndraValdObjekt(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (OversiktListaLada.SelectedItem is Medlem valdMedlem)
            {
                DetaljTextLada.Text = valdMedlem.Detaljer();
            }
            if (OversiktListaLada.SelectedItem is Spel valdSpel)
            {
                DetaljTextLada.Text = valdSpel.Detaljer();
            }
        }

        private void AndraVisadListaKlick(Object sender, RoutedEventArgs e)
        {
            if ((string)AndraVisadListaKnapp.Tag == "personer")
            {
                AndraVisadListaKnapp.Tag = "spel";

                //"oh vengance of god how you should be feared by all who read what i now see before my eyes" - Dante Alighieri
                //kommande kod tar först fram en lista på vilka spel som har en matchande mängd rekommenderade spelare i förhållande till mängden anmälda till den träffen man har översikt över
                //efter detta tar den fram en lista på bokningar som är bokade till samma tid som den bokningen man har översikt över
                //sedan räknas det ut, av de bokningar som överlappar, vilka har bokat ett spel som är med i listan av rekommenderade spel
            
                Bokning bokning = (Bokning)OversiktListaLada.Tag;
                List<Spel> tillgangligaSpel = SpelLista.HamtaSpelLista().spel.Where(spel => spel.minAntalSpelare <= bokning.anmalda.Count() && spel.maxAntalSpelare >= bokning.anmalda.Count()).ToList();
                List<Bokning> overlappandeBokningar = Bokningar.HamtaBokningar().bokningar.Where(bokning => bokning.startDatum < bokning.slutDatum && bokning.startDatum < bokning.slutDatum).ToList();
                foreach (Spel spel in tillgangligaSpel)
                {
                    if (!(overlappandeBokningar.Where(bokning => bokning.bokadeSpel.Contains(spel)).Count() == 0))
                    {
                        tillgangligaSpel.Remove(spel);
                    }

                }
                OversiktListaLada.ItemsSource = tillgangligaSpel;
                UppdateraUI();
                return;
            }
            if ((string)AndraVisadListaKnapp.Tag == "spel")
            {
                AndraVisadListaKnapp.Tag = "personer";

                OversiktListaLada.ItemsSource = ((Bokning)OversiktListaLada.Tag).anmalda;
                UppdateraUI();
                return;
            }
        }
    }
}

