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


            int antal = 0;
            if (!int.TryParse(MaxAntalTextLada.Text.Trim(), out antal)) return;

            Button knapp = (Button) sender;
            DateTime datum = (DateTime)knapp.Tag;

            if (StartDatumTextLada.Tag == null)
            {
                StartDatumTextLada.Text = datum.ToString(); 
                StartDatumTextLada.Tag = datum;
                UppdateraUI();
                return;
            }

            Bokning bokning = new Bokning((DateTime)StartDatumTextLada.Tag,
                                          datum,
                                          PlatsTextLada.Text.Trim(),
                                          antal,
                                          Session.HamtaSession().inloggadMedlem,
                                          BeskrivningTextLada.Text.Trim());


            Bokningar.HamtaBokningar().bokningar.Add(bokning);

            var mainWin = (MainWindow)MainWindow.GetWindow(this);
            mainWin.Vy.Content = new BokaSpelVy(bokning);
        }

        private void InitieraSchemaTider()
        {
            DateTime datum = new DateTime();
            datum = DateTime.Today;
            datum = datum.AddHours(8);
            DatumTextLada.Text = datum.Date.ToShortDateString();
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
            DatumTextLada.Text = datum.Date.ToShortDateString();
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
        private void NastaDagKlick(Object sender, RoutedEventArgs e)
        {
            DateTime datum = new DateTime();
            datum = DateTime.Parse(DatumTextLada.Text);
            datum = datum.AddDays(1);
            datum = datum.AddHours(8);

            InitieraSchemaTider(datum);
        }
        private void ForegaendeDagKlick(Object sender, RoutedEventArgs e)
        {
            DateTime datum = new DateTime();
            datum = DateTime.Parse(DatumTextLada.Text);
            datum = datum.AddDays(-1);
            datum = datum.AddHours(8);

            InitieraSchemaTider(datum);
        }
        private void NastaVeckaKlick(Object sender, RoutedEventArgs e)
        {
            DateTime datum = new DateTime();
            datum = DateTime.Parse(DatumTextLada.Text);
            datum = datum.AddDays(7);
            datum = datum.AddHours(8);

            InitieraSchemaTider(datum);
        }

        private void ForegaendeVeckaKlick(Object sender, RoutedEventArgs e)
        {
            DateTime datum = new DateTime();
            datum = DateTime.Parse(DatumTextLada.Text);
            datum = datum.AddDays(-7);
            datum = datum.AddHours(8);

            InitieraSchemaTider(datum);
        }
        private void UppdateraUI()
        {
            var text = DatumTextLada.Text;
            DatumTextLada.Text = null;
            DatumTextLada.Text = text;
            var lista = SchemaTiderLada.ItemsSource;
            SchemaTiderLada.ItemsSource = null;
            SchemaTiderLada.ItemsSource = lista;
            var startDatum = StartDatumTextLada.Text;
            StartDatumTextLada.Text = null;
            StartDatumTextLada.Text = startDatum;

        }

    }
}
