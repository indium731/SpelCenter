
using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class OversiktVy : UserControl
    {
        public OversiktVy()
        {
            return;
            /*
            InitializeComponent();
            InitieraOversiktLista();
            UppdateraUI();
            */
        }
        public OversiktVy(Bokning bokning)
        {
            InitializeComponent();
            InitieraOversiktLista(bokning);
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
                DetaljTextLada.Text = 
                    $"Namn: {valdMedlem.namn}\n" +
                    $"TelefonNummer: {valdMedlem.telefonNummer}\n";
            }
            if (OversiktListaLada.SelectedItem is Spel valdSpel)
            {
                return;
            }
        }

        private void AndraVisadListaKlick(Object sender, RoutedEventArgs e)
        {
            return;
        }
    }
}

