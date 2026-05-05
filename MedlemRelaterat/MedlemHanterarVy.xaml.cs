
using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class MedlemHanterarVy : UserControl
    {
        public MedlemHanterarVy()
        {
            InitializeComponent();
            InitializeMedlemLista();
            UppdateraUI();
        }

        private void UppdateraUI()
        {
            MedlemListaLada.ItemsSource = null;
            MedlemListaLada.ItemsSource = MedlemsLista.HamtaMedlemsLista().medlemmar;
        }

        private void InitializeMedlemLista()
        {
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
            MedlemsLista.HamtaMedlemsLista().medlemmar.Add(new Medlem(NamnTextLada.Text.Trim(),
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
            
            MedlemsLista.HamtaMedlemsLista().medlemmar.Remove(valdMedlem);
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
            if (MedlemListaLada.SelectedItem is not Medlem valdMedlem) return;
            if (NamnTextLada.Text.Trim().Count() != 0) valdMedlem.namn = NamnTextLada.Text.Trim();
            if (TelefonNummerTextLada.Text.Trim().Count() != 0) valdMedlem.telefonNummer= TelefonNummerTextLada.Text.Trim();
            if (MedlemsNummerTextLada.Text.Trim().Count() != 0) valdMedlem.medlemsNummer = MedlemsNummerTextLada.Text.Trim();
            UppdateraUI();
        }

        private void OkaMedlemSkapManad(Object sender, RoutedEventArgs e)
        {
            if (MedlemListaLada.SelectedItem is not Medlem valdMedlem) return;
            valdMedlem.medlemSkap.slutDatum = valdMedlem.medlemSkap.slutDatum.AddMonths(1);
            UppdateraUI();
        }

    }
}

