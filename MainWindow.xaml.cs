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


		MedlemsLista.HamtaMedlemsLista().medlemmar.Add(new Medlem("Alexander", "0701234567", "S1", true));   // admin
		MedlemsLista.HamtaMedlemsLista().medlemmar.Add(new Medlem("Lisa", "0739876543", "S2", false));      // vanlig
		MedlemsLista.HamtaMedlemsLista().medlemmar.Add(new Medlem("Pelle", "0725551234", "S3", false));
		MedlemsLista.HamtaMedlemsLista().medlemmar.Add(new Medlem("Emma", "0761112233", "S4", true));       // admin

		SpelLista.HamtaSpelLista().spel.Add(new Spel("Uno", "Sällskap", 2, 8, Svarighetsgrad.barnvänligt , "kortspel"));
		SpelLista.HamtaSpelLista().spel.Add(new Spel("Fyra i rad", "Sällskap", 2, 2, Svarighetsgrad.barnvänligt, "Få fyra i rad"));
		SpelLista.HamtaSpelLista().spel.Add(new Spel("Call of duty", "Strategi", 2, 8, Svarighetsgrad.barnvänligt, "Actionfyllt strategispel"));
		SpelLista.HamtaSpelLista().spel.Add(new Spel("Guitarherokortspelet", "Sällskap", 1, 2, Svarighetsgrad.barnvänligt, "kortspel"));


		Bokningar().HamtaBokningar().bokningar.Add(new Bokning(new DateTime (2024, 03, 12),new  DateTime (2024, 03, 12), "Majorna", 5, Medlem a, string b));
	}

}
