using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Labb1_OOP;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Vy.Content = new InloggVy();
        Seed();
    }

	private void Seed()
	{
		var lista = MedlemsLista.HamtaMedlemsLista().medlemmar;

		lista.Add(new Medlem("Alexander", "0701234567", "S1", true));   // admin
		lista.Add(new Medlem("Lisa", "0739876543", "S2", false));      // vanlig
		lista.Add(new Medlem("Johan", "0725551234", "S3", false));
		lista.Add(new Medlem("Emma", "0761112233", "S4", true));       // admin
	}

}
