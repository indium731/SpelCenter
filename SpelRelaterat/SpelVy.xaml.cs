

using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class SpelVy : UserControl
    {
        public SpelVy()
        {
            InitializeComponent();
            InitieraSvarighetsgradLada();
            UppdateraUI();
        }

        private void UppdateraUI()
        {
            SpelListaLada.ItemsSource = null;
            SpelListaLada.ItemsSource = SpelLista.HamtaSpelLista().spel;
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

            SpelLista.HamtaSpelLista().spel.Add(new Spel(NamnTextLada.Text.Trim(),
                                                                      KategoriTextLada.Text.Trim(),
                                                                      minAntal,
                                                                      maxAntal,
                                                                      svarighetsgrad,
                                                                      BeskrivningTextLada.Text.Trim()));
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

        private void UppdateraValdSpelKlick(Object sender, RoutedEventArgs e)
        {
            if (SpelListaLada.SelectedItem is not Spel valdSpel) return;
            if (NamnTextLada.Text.Trim().Count() != 0) valdSpel.namn = NamnTextLada.Text.Trim();
            if (KategoriTextLada.Text.Trim().Count() != 0) valdSpel.kategori= KategoriTextLada.Text.Trim();
            if (!int.TryParse(MinSpelareTextLada.Text.Trim(), out valdSpel.minAntalSpelare)) ;
            if (!int.TryParse(MaxSpelareTextLada.Text.Trim(), out valdSpel.maxAntalSpelare)) ;
            if (SvarighetsgradLada.SelectedItem != null) valdSpel.svarighetsgrad = (Svarighetsgrad)SvarighetsgradLada.SelectedItem;
            UppdateraUI();

        }

        private void InitieraSvarighetsgradLada()
        {
            SvarighetsgradLada.ItemsSource = Enum.GetNames(typeof(Svarighetsgrad));
        }

    }
}