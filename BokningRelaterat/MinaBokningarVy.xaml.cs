using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class MinaBokningarVy : UserControl
    {
        public MinaBokningarVy()
        {
            InitializeComponent();
            InitieraMedlemLista();
            UppdateraUI();
        }

        private void UppdateraUI()
        {
            MinaBokningarListaLada.ItemsSource = null;
            MinaBokningarListaLada.ItemsSource = Bokningar.HamtaBokningar().bokningar.Where(bokning => bokning.ansvarig.medlemsNummer == Session.HamtaSession().inloggadMedlem.medlemsNummer);
        }

        private void InitieraMedlemLista()
        {
            MinaBokningarListaLada.ItemsSource = Bokningar.HamtaBokningar().bokningar.Where(bokning => bokning.ansvarig.medlemsNummer == Session.HamtaSession().inloggadMedlem.medlemsNummer);
        }

        private void GaTillMeny(Object sender, RoutedEventArgs e)
        {
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new MedlemMenyVy();
        }

        private void TaBortValdBokningKlick(Object sender, RoutedEventArgs e)
        {
            if (MinaBokningarListaLada.SelectedItem is not Bokning valdBokning)
            {
                DetaljTextLada.Text = "Välj en bokning att ta bort";
                return;
            }
            
            Bokningar.HamtaBokningar().bokningar.Remove(valdBokning);
            UppdateraUI();
            DetaljTextLada.Text = "Ingen bokning vald";

        }

        private void AndraValdBokning(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (MinaBokningarListaLada.SelectedItem is not Bokning valdBokning)
            {
                DetaljTextLada.Text = "Ingen bokning vald";
                return;
            }
            
            DetaljTextLada.Text = 
                $"startdatum: {valdBokning.startDatum.ToString()}\n" +
                $"slutdatum: {valdBokning.slutDatum.ToString()}\n" +
                $"plats: {valdBokning.plats}\n" +
                $"ansvarig: {valdBokning.ansvarig.ToString()}\n" +
                $"max antal: {valdBokning.maxAntal}\n" +
                $"antal anmälda: {valdBokning.anmalda.Count}\n" +
                $"beskrivning: {valdBokning.beskrivning}";
        }

        private void GaTillOversiktKlick(Object sender, RoutedEventArgs e)
        {
            if (MinaBokningarListaLada.SelectedItem is not Bokning valdBokning) return;
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new OversiktVy(valdBokning);
        }

    }
}

