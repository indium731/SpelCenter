

using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class AnmalVy : UserControl
    {
        public AnmalVy()
        {
            InitializeComponent();
            InitieraBokningar();
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


            DetaljTextLada.Text = 
                $"startdatum: {valdBokning.startDatum.ToString()}\n" +
                $"slutdatum: {valdBokning.slutDatum.ToString()}\n" +
                $"plats: {valdBokning.plats}\n" +
                $"ansvarig: {valdBokning.ansvarig.ToString()}\n" +
                $"max antal: {valdBokning.maxAntal}\n" +
                $"antal anmälda: {valdBokning.anmalda.Count}\n" +
                $"beskrivning: {valdBokning.beskrivning}";
        }

    }
}

