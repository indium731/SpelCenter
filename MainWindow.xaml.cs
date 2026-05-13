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

		Medlem Alexander = MedlemLista.HamtaMedlemLista().Seed("Alexander", "0701234567", "S1", true);   
		Medlem Lisa = MedlemLista.HamtaMedlemLista().Seed("Lisa", "0739876543", "S2", false);
		Medlem Pelle = MedlemLista.HamtaMedlemLista().Seed("Pelle", "0725551234", "S3", false);
		Medlem Emma = MedlemLista.HamtaMedlemLista().Seed("Emma", "0761112233", "S4", true);
		Medlem Rednaxela = MedlemLista.HamtaMedlemLista().Seed("Rednaxela", "070123333", "S5", true);

		SpelLista.HamtaSpelLista().Seed("Uno", "Sällskap", 2, 8, Svarighetsgrad.barnvänligt , "kortspel");
		SpelLista.HamtaSpelLista().Seed("Fyra i rad", "Sällskap", 2, 2, Svarighetsgrad.barnvänligt, "Få fyra i rad");
		SpelLista.HamtaSpelLista().Seed("Call of duty", "Strategi", 2, 8, Svarighetsgrad.barnvänligt, "Actionfyllt strategispel");
		SpelLista.HamtaSpelLista().Seed("Guitarherokortspelet", "Sällskap", 1, 2, Svarighetsgrad.barnvänligt, "kortspel");

		Bokning unotraff = BokningLista.HamtaBokningLista().Seed(new DateTime(2027, 03, 12), new DateTime(2027, 03, 12), "Majorna", 5, Alexander, "Unospelträff");
		Bokning Codtraff = BokningLista.HamtaBokningLista().Seed(new DateTime(2027, 03, 15), new DateTime(2027, 03, 15), "Majorna", 5, Emma, "Unospelträff");

		unotraff.SeedAnmal(Rednaxela);
		Codtraff.SeedAnmal(Pelle);
	}

}
