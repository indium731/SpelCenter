using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class BokningVy : UserControl
    {
        public BokningVy()
        {
            InitializeComponent();
        }

        private void GaTillMeny(Object sender, RoutedEventArgs e)
        {
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new MedlemMenyVy();
        }

        private void TestaLaggTillBokningKlick(Object sender, RoutedEventArgs e)
        {
            Bokningar.HamtaBokningar().bokningar.Add(new Bokning(DatumTextLada.Text.Trim(),
                                                                      TidTextLada.Text.Trim(),
                                                                      PlatsTextLada.Text.Trim(),
                                                                      MaxAntalTextLada.Text.Trim(),
                                                                      AnsvarigTextLada.Text.Trim(),
                                                                      BeskrivningTextLada.Text.Trim()));
        }
    }
}
