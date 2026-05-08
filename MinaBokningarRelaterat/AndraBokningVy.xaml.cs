
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
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new MedlemMenyVy();
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

                var mainWin = (MainWindow)MainWindow.GetWindow(this);
                mainWin.Vy.Content = new BokaSpelVy(bokning);
            }
            catch
            {
                
            }
        }

        
        private void InitieraSchemaTider(ListBox ListaLada)
        {
            int startTid = Installningar.forstaBokbaraTid;
            int bokningTider = Installningar.antalBokningTider;
            double inkrement = (Installningar.sistaBokbaraTid - startTid);
            inkrement /= (bokningTider-1);
            List<Button> tider = new List<Button>();
            TimeOnly tid = TimeOnly.MinValue;
            tid = tid.AddHours(startTid);

            for (int i = 0; i<bokningTider; i++)
            {
                Button tidKnapp = new Button();
                tidKnapp.Content = tid.ToString();
                tidKnapp.Tag = tid;
                tidKnapp.Click += TestaAndraBokningKlick;
                tider.Add(tidKnapp);
                ListaLada.ItemsSource = tider;
                tid = tid.AddHours(inkrement);
            }
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
