using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class BokningVy : UserControl
    {
        public BokningVy()
        {
            InitializeComponent();
            InitieraSchemaTider();
        }

        private void GaTillMeny(Object sender, RoutedEventArgs e)
        {
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new MedlemMenyVy();
        }

        private void TestaLaggTillBokningKlick(Object sender, RoutedEventArgs e)
        {
            try {
            int antal = 0;
            if (!int.TryParse(MaxAntalTextLada.Text.Trim(), out antal)) return;

            Bokning bokning = new Bokning(StartDatumValjare.SelectedDate,
                                          SlutDatumValjare.SelectedDate,
                                          PlatsTextLada.Text.Trim(),
                                          antal,
                                          Session.HamtaSession().inloggadMedlem,
                                          BeskrivningTextLada.Text.Trim());

            Bokningar.HamtaBokningar().bokningar.Add(bokning);
            

            var mainWin = (MainWindow)MainWindow.GetWindow(this);
            mainWin.Vy.Content = new BokaSpelVy(bokning);
            
            } catch (ArgumentException ex)
                {
                    return;
                }
                
        }

        private void InitieraSchemaTider()
        {
            DateTime datum = new DateTime();
            datum = DateTime.Today;
            datum = datum.AddHours(8);
            List<Button> knappar = new List<Button>();

            for (int i = 0; i<4; i++)
            {
                Button knapp = new Button();
                knapp.Content = datum.TimeOfDay.ToString();
                knapp.Click += TestaLaggTillBokningKlick;
                knapp.Tag = datum;
                knappar.Add(knapp);
                SchemaTiderLada.ItemsSource = knappar;
                datum = datum.AddHours(4);
            }
            UppdateraUI();
        }
        private void InitieraSchemaTider(DateTime datum)
        {
            List<Button> knappar = new List<Button>();

            for (int i = 0; i<4; i++)
            {
                Button knapp = new Button();
                knapp.Content = datum.TimeOfDay.ToString();
                knapp.Click += TestaLaggTillBokningKlick;
                knapp.Tag = datum;
                knappar.Add(knapp);
                SchemaTiderLada.ItemsSource = knappar;
                datum = datum.AddHours(4);
            }
            UppdateraUI();
        }
        private void UppdateraUI()
        {
            var lista = SchemaTiderLada.ItemsSource;
            SchemaTiderLada.ItemsSource = null;
            SchemaTiderLada.ItemsSource = lista;
            var startDatum = StartDatumTextLada.Text;
            StartDatumTextLada.Text = null;
            StartDatumTextLada.Text = startDatum;

        }

    }
}
