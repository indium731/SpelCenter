

using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP.Vyer
{
    public partial class AnmalVy : UserControl
    {
        public AnmalVy()
        {
            InitializeComponent();
            SorteringTextLada.Text = BokningLista.HamtaBokningLista().NuvarandeSortering();
            InitieraBokningar();
            UppdateraUI();
        }
        private void UppdateraUI()
        {
            var b = BokningarListaLada.ItemsSource;
            BokningarListaLada.ItemsSource = null;
            BokningarListaLada.ItemsSource = b;
            SokTextLada.Visibility = Visibility.Collapsed;
            SokDatumLada.Visibility = Visibility.Collapsed;
            if (BokningLista.HamtaBokningLista().NuvarandeSortering() == "Startdatum") SokDatumLada.Visibility = Visibility.Visible;
            if (BokningLista.HamtaBokningLista().NuvarandeSortering() == "Slutdatum") SokDatumLada.Visibility = Visibility.Visible;
            if (BokningLista.HamtaBokningLista().NuvarandeSortering() == "Beskrivning") SokTextLada.Visibility = Visibility.Visible;
            if (BokningLista.HamtaBokningLista().NuvarandeSortering() == "Ansvarig") SokTextLada.Visibility = Visibility.Visible;
            if (BokningLista.HamtaBokningLista().NuvarandeSortering() == "Maxantal") SokTextLada.Visibility = Visibility.Visible;
            if (BokningLista.HamtaBokningLista().NuvarandeSortering() == "Plats") SokTextLada.Visibility = Visibility.Visible;
        }

        private void InitieraBokningar()
        {
            BokningarListaLada.ItemsSource = BokningLista.HamtaBokningLista().bokningar;
        }

        private void GaTillMeny(Object sender, RoutedEventArgs e)
        {
            //var mainWin = (MainWindow)Window.GetWindow(this);
            //mainWin.Vy.Content = new MedlemMenyVy();
        }

        private void TestaAnmalKlick(Object sender, RoutedEventArgs e)
        {
        if (BokningarListaLada.SelectedItem is not Bokning valdBokning)
            {
                return;
            }
            valdBokning.Anmal(Session.HamtaSession().inloggadMedlem);
        }

        private void AndraValdBokning(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (BokningarListaLada.SelectedItem is not Bokning valdBokning)
            {
                DetaljTextLada.Text = "Ingen Bokning vald";
                return;
            }


            DetaljTextLada.Text = valdBokning.Detaljer();
        }
        private void AndraSorteringKlick(Object sender, RoutedEventArgs e)
        {
            BokningLista.HamtaBokningLista().GaTillNastaMetod();
            SorteringTextLada.Text = BokningLista.HamtaBokningLista().NuvarandeSortering();
            UppdateraUI();
            
        }
        private void SokKlick(Object sender, RoutedEventArgs e)
        {

            if (SokTextLada.Visibility != Visibility.Collapsed)
            {
                BokningarListaLada.ItemsSource = BokningLista.HamtaBokningLista().Sok(SokTextLada.Text.Trim());
            }
            if (SokDatumLada.Visibility != Visibility.Collapsed)
            {
                var datum = SokDatumLada.SelectedDate ?? DateTime.Now;
                BokningarListaLada.ItemsSource = BokningLista.HamtaBokningLista().Sok(datum.ToShortDateString());
            }
            
        }

    }
}

