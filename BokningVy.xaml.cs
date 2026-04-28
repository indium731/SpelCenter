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
            Button knapp = (Button) sender;
            DateTime datum = (DateTime)knapp.Tag;
            Bokningar.HamtaBokningar().bokningar.Add(new Bokning(datum,
                                                                      PlatsTextLada.Text.Trim(),
                                                                      MaxAntalTextLada.Text.Trim(),
                                                                      Session.HamtaSession().inloggadMedlem,
                                                                      BeskrivningTextLada.Text.Trim()));
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
        private void NastaVeckaKlick(Object sender, RoutedEventArgs e)
        {
            DateTime datum = new DateTime();
            datum = DateTime.Parse(DatumTextLada.Text);
            datum = datum.AddDays(7);
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

        }
    }
}
