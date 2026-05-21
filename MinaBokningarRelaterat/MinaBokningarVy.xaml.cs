using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP.Vyer
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
            MinaBokningarListaLada.ItemsSource = BokningLista.HamtaBokningLista().bokningar.Where(bokning => bokning.ansvarig.medlemsNummer == Session.HamtaSession().inloggadMedlem.medlemsNummer);
        }

        private void InitieraMedlemLista()
        {
            MinaBokningarListaLada.ItemsSource = BokningLista.HamtaBokningLista().bokningar.Where(bokning => bokning.ansvarig.medlemsNummer == Session.HamtaSession().inloggadMedlem.medlemsNummer);
        }

        private void GaTillMeny(Object sender, RoutedEventArgs e)
        {
            //var mainWin = (MainWindow)Window.GetWindow(this);
            //mainWin.Vy.Content = new MedlemMenyVy();
        }

        private void TaBortValdBokningKlick(Object sender, RoutedEventArgs e)
        {
            if (MinaBokningarListaLada.SelectedItem is not Bokning valdBokning)
            {
                DetaljTextLada.Text = "Välj en bokning att ta bort";
                return;
            }
            
            BokningLista.HamtaBokningLista().bokningar.Remove(valdBokning);
            DetaljTextLada.Text = "Ingen bokning vald";
            UppdateraUI();

        }

        private void AndraValdBokning(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (MinaBokningarListaLada.SelectedItem is not Bokning valdBokning)
            {
                DetaljTextLada.Text = "Ingen bokning vald";
                return;
            }
            
            DetaljTextLada.Text = valdBokning.Detaljer();
        }

        private void GaTillOversiktKlick(Object sender, RoutedEventArgs e)
        {
            if (MinaBokningarListaLada.SelectedItem is not Bokning valdBokning) return;
            //var mainWin = (MainWindow)Window.GetWindow(this);
            //mainWin.Vy.Content = new OversiktVy(valdBokning);
        }
        private void GaTillAndraBokningKlick(Object sender, RoutedEventArgs e)
        {
            if (MinaBokningarListaLada.SelectedItem is not Bokning valdBokning) return;
            //var mainWin = (MainWindow)Window.GetWindow(this);
            //mainWin.Vy.Content = new AndraBokningVy(valdBokning);
        }


    }
}

