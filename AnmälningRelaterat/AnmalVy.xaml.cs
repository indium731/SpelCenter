

using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class AnmalVy : UserControl
    {
        public AnmalVy()
        {
            InitializeComponent();
            SorteringTextLada.Text = MedlemsLista.HamtaMedlemsLista().NuvarandeSortering();
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
            if (Bokningar.HamtaBokningar().NuvarandeSortering() == "Startdatum") SokDatumLada.Visibility = Visibility.Visible;
            if (Bokningar.HamtaBokningar().NuvarandeSortering() == "Slutdatum") SokDatumLada.Visibility = Visibility.Visible;
            if (Bokningar.HamtaBokningar().NuvarandeSortering() == "Beskrivning") SokTextLada.Visibility = Visibility.Visible;
            if (Bokningar.HamtaBokningar().NuvarandeSortering() == "Ansvarig") SokTextLada.Visibility = Visibility.Visible;
            if (Bokningar.HamtaBokningar().NuvarandeSortering() == "Maxantal") SokTextLada.Visibility = Visibility.Visible;
            if (Bokningar.HamtaBokningar().NuvarandeSortering() == "Plats") SokTextLada.Visibility = Visibility.Visible;
        }

        private void InitieraBokningar()
        {
            BokningarListaLada.ItemsSource = Bokningar.HamtaBokningar().bokningar;
        }

        private void GaTillMeny(Object sender, RoutedEventArgs e)
        {
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new MedlemMenyVy();
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
            Bokningar.HamtaBokningar().GaTillNastaMetod();
            SorteringTextLada.Text = Bokningar.HamtaBokningar().NuvarandeSortering();
            UppdateraUI();
            
        }
        private void SokKlick(Object sender, RoutedEventArgs e)
        {

            if (SokTextLada.Visibility != Visibility.Collapsed)
            {
                BokningarListaLada.ItemsSource = MedlemsLista.HamtaMedlemsLista().Sok(SokTextLada.Text.Trim());
            }
            if (SokDatumLada.Visibility != Visibility.Collapsed)
            {
                var datum = SokDatumLada.SelectedDate ?? DateTime.Now;
                BokningarListaLada.ItemsSource = MedlemsLista.HamtaMedlemsLista().Sok(datum.ToShortDateString());
            }
            
        }

    }
}

