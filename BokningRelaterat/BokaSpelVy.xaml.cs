


using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class BokaSpelVy : UserControl
    {
        public BokaSpelVy(Bokning bokning)
        {
            InitializeComponent();
            UppdateraUI();
            BokaSpelKnapp.Tag = bokning;
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

