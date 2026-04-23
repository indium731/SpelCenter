
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
            MedlemsLista.HamtaMedlemsLista().medlemmar.Add(new Medlem(NamnTextLada.Text.Trim(),
                                                                      TelefonNummerTextLada.Text.Trim(),
                                                                      MedlemsNummerTextLada.Text.Trim(),
                                                                      Administratör.IsChecked));
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
            DetaljTextLada.Text = "Ingen kaka vald";

        }

        private void AndraValdMedlem(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (MedlemListaLada.SelectedItem is not Medlem valdMedlem)
            {
                DetaljTextLada.Text = "Ingen medlem vald";
                return;
            }


            DetaljTextLada.Text = 
                $"Namn: {valdMedlem.namn}\n" +
                $"TelefonNummer: {valdMedlem.telefonNummer}\n" +
                $"MedlemsNummer: {valdMedlem.medlemsNummer}";
        }

    }
}

