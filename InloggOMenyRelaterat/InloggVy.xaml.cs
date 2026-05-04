using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class InloggVy : UserControl
    {
        public InloggVy()
        {
            InitializeComponent();
        }
    

        private void TestaInloggKlick(object sender, RoutedEventArgs e)
        {
           string inlogg = InloggTextLada.Text.Trim();
 

           if (string.IsNullOrWhiteSpace(inlogg))
               {
             return;
            }

            foreach (Medlem medlem in MedlemsLista.HamtaMedlemsLista().medlemmar)
            {
                if (inlogg == medlem.medlemsNummer)
                    {
                        if (!medlem.medlemSkap.medlemStatus)
                    {
                        MessageBox.Show("medlemSkap ej aktivt");
                        return;
                    }
                        Session.HamtaSession().inloggadMedlem = medlem;
                        var mainWin = (MainWindow)Window.GetWindow(this);
                        mainWin.Vy.Content = new MedlemMenyVy();
                    }
            }
        }
    }
}

