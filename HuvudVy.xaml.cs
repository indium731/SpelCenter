using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP
{
    public partial class HuvudVy : UserControl
    {
        public HuvudVy()
        {
            InitializeComponent();
        }

        private void TestaUtloggKlick(Object sender, RoutedEventArgs e)
        {

            var mainWin = (MainWindow)Window.GetWindow(this);
            mainWin.Vy.Content = new InloggVy();
        }
    

    }
}

