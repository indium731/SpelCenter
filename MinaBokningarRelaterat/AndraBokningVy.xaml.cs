
using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class AndraBokningVy : UserControl
    {
        Bokning bokning;
        public AndraBokningVy(Bokning b)
        {
            InitializeComponent();
            InitieraSchemaTider(StartSchemaTiderLada);
            InitieraSchemaTider(SlutSchemaTiderLada);
            NuvarandeBokningTextLada.Text = b.ToString();
            bokning = b;
        }

        private void GaTillMeny(Object sender, RoutedEventArgs e)
        {
            //var mainWin = (MainWindow)Window.GetWindow(this);
            //mainWin.Vy.Content = new MedlemMenyVy();
        }

        private void TestaAndraBokningKlick(Object sender, RoutedEventArgs e)
        {
            int antal = 0;
            if (!int.TryParse(MaxAntalTextLada.Text.Trim(), out antal));

            DateTime? startDatumTid = null;
            DateTime? slutDatumTid = null;

            if (((Button)sender).Tag is TimeOnly tid)
            {
                if (StartDatumValjare.SelectedDate is DateTime startDatum)
                {
                startDatumTid = startDatum.Date + tid.ToTimeSpan();
                }
                if (SlutDatumValjare.SelectedDate is DateTime slutDatum)
                {
                slutDatumTid = slutDatum.Date + tid.ToTimeSpan();
                }
            }
            try
            {
                if (startDatumTid != null) bokning.startDatum = (DateTime)startDatumTid;
                if (slutDatumTid != null) bokning.slutDatum = (DateTime)slutDatumTid;
                if (PlatsTextLada.Text.Trim() != "") bokning.plats = PlatsTextLada.Text.Trim();
                if (antal != 0) bokning.maxAntal = antal;
                if (BeskrivningTextLada.Text.Trim() != "") bokning.beskrivning = BeskrivningTextLada.Text.Trim();

                //var mainWin = (MainWindow)MainWindow.GetWindow(this);
                //mainWin.Vy.Content = new BokaSpelVy(bokning);
            }
            catch
            {
                
            }
        }
        private void InitieraSchemaTider(ListBox ListaLada)
        {
            TimeSpan inkrement;
            if (Installningar.antalBokningTider == 1)
            {
                inkrement = new TimeSpan();
            }else
            {
            inkrement = (Installningar.sistaBokbaraTid - Installningar.forstaBokbaraTid)/(Installningar.antalBokningTider-1);
            }

            TimeOnly tid = Installningar.forstaBokbaraTid;

            List<Button> knappar = new List<Button>();

            for (int i = 0; i<Installningar.antalBokningTider; i++)
            {
                Button knapp = new Button();
                knapp.Content = tid.ToString();
                knapp.Click += TestaAndraBokningKlick;
                knapp.Tag = tid;
                knappar.Add(knapp);
                tid = tid.Add(inkrement);
            }
            ListaLada.ItemsSource = knappar;
            UppdateraUI();
        } 

        
        private void UppdateraUI()
        {
            var startLista = StartSchemaTiderLada.ItemsSource;
            StartSchemaTiderLada.ItemsSource = null;
            StartSchemaTiderLada.ItemsSource = startLista;
            var slutLista = SlutSchemaTiderLada.ItemsSource;
            SlutSchemaTiderLada.ItemsSource = null;
            SlutSchemaTiderLada.ItemsSource = slutLista;
            var startDatum = StartDatumTextLada.Text;
            StartDatumTextLada.Text = null;
            StartDatumTextLada.Text = startDatum;

        }
    }
}
