
using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class AndraBokningVy : UserControl
    {
        public AndraBokningVy(Bokning bokning)
        {
            InitializeComponent();
            InitieraSchemaTider();
            NuvarandeBokningTextLada.Text = bokning.ToString();
            NuvarandeBokningTextLada.Tag = bokning;
        }

        private void GaTillMeny(Object sender, RoutedEventArgs e)
        {
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new MedlemMenyVy();
        }

        private void TestaAndraBokningKlick(Object sender, RoutedEventArgs e)
        {
            int antal = 0;
            if (!int.TryParse(MaxAntalTextLada.Text.Trim(), out antal)) return;

            Bokning bokning = (Bokning)NuvarandeBokningTextLada.Tag;

            if (StartDatumValjare != null) bokning.startDatum = (DateTime)StartDatumValjare.SelectedDate;
            if (SlutDatumValjare != null) bokning.slutDatum = (DateTime)SlutDatumValjare.SelectedDate;
            if (PlatsTextLada.Text.Trim() != "") bokning.plats = PlatsTextLada.Text.Trim();
            if (antal != 0) bokning.maxAntal = antal;
            if (BeskrivningTextLada.Text.Trim() != "") bokning.beskrivning = BeskrivningTextLada.Text.Trim();

            var mainWin = (MainWindow)MainWindow.GetWindow(this);
            mainWin.Vy.Content = new BokaSpelVy(bokning);
        }

        private void InitieraSchemaTider()
        {
            DateTime datum = new DateTime();
            datum = DateTime.Today;
            datum = datum.AddHours(8);
            List<TextBlock> tider = new List<TextBlock>();

            for (int i = 0; i<4; i++)
            {
                TextBlock tid = new TextBlock();
                tid.Text = datum.TimeOfDay.ToString();
                tid.Tag = datum;
                tider.Add(tid);
                SchemaTiderLada.ItemsSource = tider;
                datum = datum.AddHours(4);
            }
            UppdateraUI();
        }
        private void InitieraSchemaTider(DateTime datum)
        {
            List<TextBlock> tider = new List<TextBlock>();

            for (int i = 0; i<4; i++)
            {
                TextBlock tid = new TextBlock();
                tid.Text = datum.TimeOfDay.ToString();
                tid.Tag = datum;
                SchemaTiderLada.ItemsSource = tider;
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
