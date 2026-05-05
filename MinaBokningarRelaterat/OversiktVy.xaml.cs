
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class OversiktVy : UserControl
    {
        public OversiktVy(Bokning bokning)
        {
            InitializeComponent();
            InitieraOversiktLista(bokning);
            OversiktListaLada.Tag = bokning;
            UppdateraUI();
        }
        private void UppdateraUI()
        {
            var temporar = OversiktListaLada.ItemsSource;
            OversiktListaLada.ItemsSource = null;
            OversiktListaLada.ItemsSource = temporar;
        }

        private void InitieraOversiktLista(Bokning bokning)
        {
            OversiktListaLada.ItemsSource = bokning.anmalda;
            AndraVisadListaKnapp.Tag = "personer";
        }

        private void GaTillMinaBokningarKlick(Object sender, RoutedEventArgs e)
        {
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new MinaBokningarVy();
        }

        private void AndraValdObjekt(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (OversiktListaLada.SelectedItem is Medlem valdMedlem)
            {
                DetaljTextLada.Text = valdMedlem.Detaljer();
            }
            if (OversiktListaLada.SelectedItem is Spel valdSpel)
            {
                DetaljTextLada.Text = valdSpel.Detaljer();
            }
        }

        private void AndraVisadListaKlick(Object sender, RoutedEventArgs e)
        {
            if (AndraVisadListaKnapp.Tag == "personer")
            {
                AndraVisadListaKnapp.Tag = "spel";
            
                Bokning bokning = (Bokning)OversiktListaLada.Tag;
                OversiktListaLada.ItemsSource = SpelLista.HamtaSpelLista().spel.Where(spel => spel.minAntalSpelare <= bokning.anmalda.Count() && spel.maxAntalSpelare >= bokning.anmalda.Count());
                return;
            }
            if (AndraVisadListaKnapp.Tag == "spel")
            {
                AndraVisadListaKnapp.Tag = "personer";

                OversiktListaLada.ItemsSource = ((Bokning)OversiktListaLada.Tag).anmalda;
            }
            UppdateraUI();
        }
    }
}

