using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class BokningVy : UserControl
    {
        public BokningVy()
        {
            InitializeComponent();
            InitieraStartSchemaTider();
            InitieraSlutSchemaTider();
        }

        public void GaTillMeny(Object sender, RoutedEventArgs e)
        {
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new MedlemMenyVy();
        }

        private void TestaLaggTillBokningKlick(Object sender, RoutedEventArgs e)
        {

            if (StartDatumValjare.SelectedDate == null) return;
            if (SlutDatumValjare.SelectedDate == null) return;
            
            if (StartDatumLada.Tag is not TimeOnly startTid)
            {
                MessageBox.Show("Välj ett startdatum först");
                return;
            }

            Button knapp = (Button)sender;
            TimeOnly slutTid = (TimeOnly)((Button)sender).Tag;
            DateTime startDatum = ((DateTime)StartDatumValjare.SelectedDate).Date + startTid.ToTimeSpan();
            DateTime slutDatum = ((DateTime)SlutDatumValjare.SelectedDate).Date + slutTid.ToTimeSpan();


            try {
            int antal = 0;
            if (!int.TryParse(MaxAntalTextLada.Text.Trim(), out antal)) return;

            Bokning bokning = new Bokning(startDatum,
                                          slutDatum,
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

        private void ValjStartTid(Object sender, RoutedEventArgs e)
        {
            StartDatumLada.Tag = (TimeOnly)((Button)sender).Tag;
            StartDatumLada.Text = "StartDatum vald";
        }
        private void InitieraStartSchemaTider()
        {
            int startTid = Installningar.forstaBokbaraTid;
            int bokningTider = Installningar.antalBokningTider;
            double inkrement = (Installningar.sistaBokbaraTid - startTid);
            inkrement /= (bokningTider-1);
            TimeOnly tid = new TimeOnly();
            tid = TimeOnly.MinValue;
            //8 == tidigaste bokbara tid
            tid = tid.AddHours(startTid);
            List<Button> knappar = new List<Button>();

            //4 == mängd olika bokbara tider per dag
            for (int i = 0; i<bokningTider; i++)
            {
                Button knapp = new Button();
                knapp.Content = tid.ToString();
                knapp.Click += ValjStartTid;
                knapp.Tag = tid;
                knappar.Add(knapp);
                StartSchemaTiderLada.ItemsSource = knappar;
                //4 == tid mellan de bokbara tiderna utöver en dag
                tid = tid.AddHours(inkrement);
            }
            UppdateraUI();
        } 
        private void InitieraSlutSchemaTider()
        {
            int startTid = Installningar.forstaBokbaraTid;
            int bokningTider = Installningar.antalBokningTider;

            double inkrement = (Installningar.sistaBokbaraTid - startTid);
            inkrement /= (bokningTider-1);

            TimeOnly tid = new TimeOnly();
            tid = TimeOnly.MinValue;
            //8 == tidigaste bokbara tid
            tid = tid.AddHours(startTid);
            List<Button> knappar = new List<Button>();

            //4 == mängd olika bokbara tider per dag
            for (int i = 0; i<bokningTider; i++)
            {
                Button knapp = new Button();
                knapp.Content = tid.ToString();
                knapp.Click += TestaLaggTillBokningKlick;
                knapp.Tag = tid;
                knappar.Add(knapp);
                SlutSchemaTiderLada.ItemsSource = knappar;
                //4 == tid mellan de bokbara tiderna utöver en dag
                tid = tid.AddHours(inkrement);
            }
            UppdateraUI();
        }

        private void UppdateraUI()
        {
            var lista = StartSchemaTiderLada.ItemsSource;
            StartSchemaTiderLada.ItemsSource = null;
            StartSchemaTiderLada.ItemsSource = lista;
            var startDatum = StartDatumTextLada.Text;
            StartDatumTextLada.Text = null;
            StartDatumTextLada.Text = startDatum;

        }

    }
}
