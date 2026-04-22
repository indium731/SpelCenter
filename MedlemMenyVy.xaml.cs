using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class MedlemMenyVy : UserControl
    {
        public MedlemMenyVy()
        {
            InitializeComponent();
        }

        private void TestaUtloggKlick(Object sender, RoutedEventArgs e)
        {
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new InloggVy();
        }

        private void GaTillMedlemHanterarVy(Object sender, RoutedEventArgs e)
        {
            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new MedlemHanterarVy();
        }
    }
}

