using Labb1_OOP.Modeller;
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
            var overlappandeBokningar = BokningLista.HamtaBokningLista().OverlappandeBokningar(bokning);

            var tillgangligaSpel = SpelLista.HamtaSpelLista().spel
                .Where(spel => !overlappandeBokningar.Any(b => b.bokadeSpel.Contains(spel)))
                .ToList();

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
            //var mainWin = (MainWindow)Window.GetWindow(this);
            //mainWin.Vy.Content = new MedlemMenyVy();
        }

        private void TestaBokaSpelKlick(Object sender, RoutedEventArgs e)
        {
            if (SpelListaLada.SelectedItem is not Spel spel) return;
            bokning.BokaSpel(spel);

            UppdateraUI();
        }

        private void AvbokaValdSpelKlick(Object sender, RoutedEventArgs e)
        {
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

