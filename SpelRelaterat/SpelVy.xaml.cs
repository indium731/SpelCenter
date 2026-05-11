

using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class SpelVy : UserControl
    {
        public SpelVy()
        {
            InitializeComponent();
            SorteringTextLada.Text = SpelLista.HamtaSpelLista().NuvarandeSortering();
            InitieraSvarighetsgradLada();
            UppdateraUI();
        }

        private void UppdateraUI()
        {
            SpelListaLada.ItemsSource = null;
            SpelListaLada.ItemsSource = SpelLista.HamtaSpelLista().spel;
            SokTextLada.Visibility = Visibility.Collapsed;
            SokComboLada.Visibility = Visibility.Collapsed;
            if (SpelLista.HamtaSpelLista().NuvarandeSortering() == "Namn") SokTextLada.Visibility = Visibility.Visible;
            if (SpelLista.HamtaSpelLista().NuvarandeSortering() == "Kategori") SokTextLada.Visibility = Visibility.Visible;
            if (SpelLista.HamtaSpelLista().NuvarandeSortering() == "Minantal spelare") SokTextLada.Visibility = Visibility.Visible;
            if (SpelLista.HamtaSpelLista().NuvarandeSortering() == "Maxantal spelare") SokTextLada.Visibility = Visibility.Visible;
            if (SpelLista.HamtaSpelLista().NuvarandeSortering() == "Svarighetsgrad") SokComboLada.Visibility = Visibility.Visible;
        }

        private void GaTillMeny(Object sender, RoutedEventArgs e)
        {
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new MedlemMenyVy();
        }

        private void TestaLaggTillSpelKlick(Object sender, RoutedEventArgs e)
        {

            int minAntal; 
            int maxAntal;

            if (!int.TryParse(MinSpelareTextLada.Text.Trim(), out minAntal)) return;
            if (!int.TryParse(MaxSpelareTextLada.Text.Trim(), out maxAntal)) return;
            if (SvarighetsgradLada.SelectedItem == null) return;

            Svarighetsgrad svarighetsgrad = (Svarighetsgrad)Enum.Parse(typeof(Svarighetsgrad), SvarighetsgradLada.SelectedItem.ToString());

            try {

            SpelLista.HamtaSpelLista().LaggTill(NamnTextLada.Text.Trim(),
                                                 KategoriTextLada.Text.Trim(),
                                                 minAntal,
                                                 maxAntal,
                                                 svarighetsgrad,
                                                 BeskrivningTextLada.Text.Trim());
            } catch (ArgumentException ex)
            {
                return;
            }
            UppdateraUI();
        }

        private void TaBortValdSpelKlick(Object sender, RoutedEventArgs e)
        {
            if (SpelListaLada.SelectedItem is not Spel valdSpel)
            {
                DetaljTextLada.Text = "Välj ett spel att ta bort";
                return;
            }
            
            SpelLista.HamtaSpelLista().TaBort(valdSpel);
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
            DetaljTextLada.Text = valdSpel.Detaljer(); 
        }

        private void UppdateraValdSpelKlick(Object sender, RoutedEventArgs e)
        {
            try
            {
                int tempInt;
                if (SpelListaLada.SelectedItem is not Spel valdSpel) return;
                if (NamnTextLada.Text.Trim().Count() != 0) valdSpel.namn = NamnTextLada.Text.Trim();
                if (KategoriTextLada.Text.Trim().Count() != 0) valdSpel.kategori= KategoriTextLada.Text.Trim();
                if (!int.TryParse(MinSpelareTextLada.Text.Trim(), out tempInt)) ;
                else valdSpel.minAntalSpelare = tempInt;
                if (!int.TryParse(MaxSpelareTextLada.Text.Trim(), out tempInt)) ;
                else valdSpel.maxAntalSpelare = tempInt;
                if (SvarighetsgradLada.SelectedItem != null) valdSpel.svarighetsgrad = (Svarighetsgrad)SvarighetsgradLada.SelectedItem;
            }
            catch
            {
            }
            UppdateraUI();
        }

        private void InitieraSvarighetsgradLada()
        {
            SvarighetsgradLada.ItemsSource = Enum.GetNames(typeof(Svarighetsgrad));
            SokComboLada.ItemsSource = Enum.GetNames(typeof(Svarighetsgrad));
        }
        private void AndraSorteringKlick(Object sender, RoutedEventArgs e)
        {
            SpelLista.HamtaSpelLista().GaTillNastaMetod();
            SorteringTextLada.Text = SpelLista.HamtaSpelLista().NuvarandeSortering();
            UppdateraUI();
            
        }
        private void SokKlick(Object sender, RoutedEventArgs e)
        {
            if (SokTextLada.Visibility != Visibility.Collapsed)
            {
                SpelListaLada.ItemsSource = SpelLista.HamtaSpelLista().Sok(SokTextLada.Text.Trim());
            }
            if (SokComboLada.Visibility != Visibility.Collapsed)
            {
                if (SvarighetsgradLada.SelectedItem == null)
                {
                    return;
                }
                
                Svarighetsgrad svarighetsgrad = (Svarighetsgrad)SvarighetsgradLada.SelectedItem;
                SpelListaLada.ItemsSource = SpelLista.HamtaSpelLista().Sok(svarighetsgrad.ToString());
            }
            
        }

    }
}