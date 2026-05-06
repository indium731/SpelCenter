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
            foreach (IAtkomst atkomst in Session.HamtaSession().inloggadMedlem.atkomster)
            {
                Button knapp = new Button();
                knapp.Click += GaTillValdVy;
                knapp.Tag = atkomst.Atkom();
                knapp.Content = atkomst.Namn();
                MenyListaLada.Items.Add(knapp);

            }
            if (Bokningar.HamtaBokningar().bokningar.Where(bokning => bokning.ansvarig == Session.HamtaSession().inloggadMedlem).Count() != 0)
            {
                Button knapp = new Button();
                knapp.Content = "Mina bokningar";
                knapp.Click += GaTillValdVy;
                knapp.Tag = new MinaBokningarVy();
                MenyListaLada.Items.Add(knapp);
            }
        }
    }
}

