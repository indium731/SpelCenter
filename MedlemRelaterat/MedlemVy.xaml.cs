
using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class MedlemVy : UserControl
    {
        public MedlemVy()
        {
            InitializeComponent();
            InitieraMedlemLista();
            UppdateraUI();
        }

        private void UppdateraUI()
        {
            MedlemListaLada.ItemsSource = null;
            MedlemListaLada.ItemsSource = MedlemsLista.HamtaMedlemsLista().medlemmar;
            SokTextLada.Visibility = Visibility.Collapsed;
            SokCheckLada.Visibility = Visibility.Collapsed;
            SokDatumLada.Visibility = Visibility.Collapsed;
            if (MedlemsLista.HamtaMedlemsLista().NuvarandeSortering() == "Startdatum") SokDatumLada.Visibility = Visibility.Visible;
            if (MedlemsLista.HamtaMedlemsLista().NuvarandeSortering() == "Slutdatum") SokDatumLada.Visibility = Visibility.Visible;
            if (MedlemsLista.HamtaMedlemsLista().NuvarandeSortering() == "MedlemStatus") SokCheckLada.Visibility = Visibility.Visible;
            if (MedlemsLista.HamtaMedlemsLista().NuvarandeSortering() == "Admin") SokCheckLada.Visibility = Visibility.Visible;
            if (MedlemsLista.HamtaMedlemsLista().NuvarandeSortering() == "Namn") SokTextLada.Visibility = Visibility.Visible;

        }

        private void InitieraMedlemLista()
        {
            SorteringTextLada.Text = MedlemsLista.HamtaMedlemsLista().NuvarandeSortering();
            MedlemListaLada.ItemsSource = MedlemsLista.HamtaMedlemsLista().medlemmar;
        }

        private void GaTillMeny(Object sender, RoutedEventArgs e)
        {
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new MedlemMenyVy();
        }

        private void TestaLaggTillMedlemKlick(Object sender, RoutedEventArgs e)
        {
            try{
            MedlemsLista.HamtaMedlemsLista().LaggTill(new Medlem(NamnTextLada.Text.Trim(),
                                                                      TelefonNummerTextLada.Text.Trim(),
                                                                      MedlemsNummerTextLada.Text.Trim(),
                                                                      (bool)Administratör.IsChecked));
            

            } catch (ArgumentException ex)
            {
                return;
            }
            UppdateraUI();
        }

        private void TaBortValdMedlemKlick(Object sender, RoutedEventArgs e)
        {
            if (MedlemListaLada.SelectedItem is not Medlem valdMedlem)
            {
                DetaljTextLada.Text = "Välj en medlem att ta bort";
                return;
            }
            
            MedlemsLista.HamtaMedlemsLista().TaBort(valdMedlem);
            UppdateraUI();
            DetaljTextLada.Text = "Ingen medlem vald";

        }

        private void AndraValdMedlem(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (MedlemListaLada.SelectedItem is not Medlem valdMedlem)
            {
                DetaljTextLada.Text = "Ingen medlem vald";
                return;
            }


            DetaljTextLada.Text = valdMedlem.UtokadeDetaljer();
        }

        private void UppdateraValdMedlemKlick(Object sender, RoutedEventArgs e)
        {
            try
            {
                if (MedlemListaLada.SelectedItem is not Medlem valdMedlem) return;
                if (NamnTextLada.Text.Trim().Count() != 0) valdMedlem.namn = NamnTextLada.Text.Trim();
                if (TelefonNummerTextLada.Text.Trim().Count() != 0) valdMedlem.telefonNummer = TelefonNummerTextLada.Text.Trim();
                if (MedlemsNummerTextLada.Text.Trim().Count() != 0) valdMedlem.medlemsNummer = MedlemsNummerTextLada.Text.Trim();
            }
            catch
            {
            }
            UppdateraUI();
            
        }

        private void OkaMedlemSkapAr(Object sender, RoutedEventArgs e)
        {
            if (MedlemListaLada.SelectedItem is not Medlem valdMedlem) return;
            valdMedlem.medlemSkap.slutDatum = valdMedlem.medlemSkap.slutDatum.AddYears(1);
            UppdateraUI();
        }
        private void AndraSorteringKlick(Object sender, RoutedEventArgs e)
        {
            MedlemsLista.HamtaMedlemsLista().GaTillNastaMetod();
            SorteringTextLada.Text = MedlemsLista.HamtaMedlemsLista().NuvarandeSortering();
            UppdateraUI();
            
        }
        private void SokKlick(Object sender, RoutedEventArgs e)
        {
            if (SokTextLada.Visibility != Visibility.Collapsed)
            {
                MedlemListaLada.ItemsSource = MedlemsLista.HamtaMedlemsLista().Sok(SokTextLada.Text.Trim());
            }
            if (SokCheckLada.Visibility != Visibility.Collapsed)
            {
                var checkad = SokCheckLada.IsChecked ?? true;
                MedlemListaLada.ItemsSource = MedlemsLista.HamtaMedlemsLista().Sok(checkad.ToString());
            }
            if (SokDatumLada.Visibility != Visibility.Collapsed)
            {
                var datum = SokDatumLada.SelectedDate ?? DateTime.Now;
                MedlemListaLada.ItemsSource = MedlemsLista.HamtaMedlemsLista().Sok(datum.ToShortDateString());
            }
            
        }

    }
}

