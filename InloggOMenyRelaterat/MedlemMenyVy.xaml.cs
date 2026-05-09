using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class MedlemMenyVy : UserControl
    {
        public MedlemMenyVy()
        {
            InitializeComponent();
            KontrolleraAtkomster();
        }

        private void TestaUtloggKlick(Object sender, RoutedEventArgs e)
        {
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new InloggVy();
        }

        private void GaTillValdVy(Object sender, RoutedEventArgs e)
        {
            Button knapp = (Button)sender;
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = knapp.Tag;
        }
        
        private void KontrolleraAtkomster()
        {
            if (!Session.HamtaSession().inloggadMedlem.admin)
            {
                MedlemHanterare.Visibility = Visibility.Collapsed;
                SpelHanterare.Visibility = Visibility.Collapsed;
            }
            if (BokningLista.HamtaBokningLista().bokningar.Any(bokning => bokning.ansvarig == Session.HamtaSession().inloggadMedlem))
            {
                MinaBokningar.Visibility = Visibility.Visible;
            }
        }
    }
}

