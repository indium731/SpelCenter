

using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class SpelVy : UserControl
    {
        public SpelVy()
        {
            InitializeComponent();
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
            SpelLista.HamtaSpelLista().spel.Add(new Spel(NamnTextLada.Text.Trim(),
                                                                      KategoriTextLada.Text.Trim(),
                                                                      SpelareTextLada.Text.Trim(),
                                                                      SvarighetsgradTextLada.Text.Trim(),
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
            if (SpelareTextLada.Text.Trim().Count() != 0) valdSpel.antalSpelare = SpelareTextLada.Text.Trim();
            if (SvarighetsgradTextLada.Text.Trim().Count() != 0) valdSpel.svarighetsgrad= SvarighetsgradTextLada.Text.Trim();
            if (BeskrivningTextLada.Text.Trim().Count() != 0) valdSpel.beskrivning = BeskrivningTextLada.Text.Trim();
            UppdateraUI();

        }

    }
}