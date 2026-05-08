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

		Medlem Alexander = new Medlem("Alexander", "0701234567", "S1", true);
		Medlem Rednaxela = new Medlem("Rednaxela", "070123333", "S5", true);
		Medlem Lisa = new Medlem("Lisa", "0739876543", "S2", false);
		Medlem Pelle = new Medlem("Pelle", "0725551234", "S3", false);
		Medlem Emma = new Medlem("Emma", "0761112233", "S4", true);

		MedlemsLista.HamtaMedlemsLista().LaggTill(Alexander);   
		MedlemsLista.HamtaMedlemsLista().LaggTill(Rednaxela);
		MedlemsLista.HamtaMedlemsLista().LaggTill(Lisa);
		MedlemsLista.HamtaMedlemsLista().LaggTill(Pelle);
		MedlemsLista.HamtaMedlemsLista().LaggTill(Emma);



		SpelLista.HamtaSpelLista().spel.Add(new Spel("Uno", "Sällskap", 2, 8, Svarighetsgrad.barnvänligt , "kortspel"));
		SpelLista.HamtaSpelLista().spel.Add(new Spel("Fyra i rad", "Sällskap", 2, 2, Svarighetsgrad.barnvänligt, "Få fyra i rad"));
		SpelLista.HamtaSpelLista().spel.Add(new Spel("Call of duty", "Strategi", 2, 8, Svarighetsgrad.barnvänligt, "Actionfyllt strategispel"));
		SpelLista.HamtaSpelLista().spel.Add(new Spel("Guitarherokortspelet", "Sällskap", 1, 2, Svarighetsgrad.barnvänligt, "kortspel"));



		Bokning unotraff = new Bokning(new DateTime(2027, 03, 12), new DateTime(2027, 03, 12), "Majorna", 5, Alexander, "Unospelträff");

		Bokning Codtraff = new Bokning(new DateTime(2027, 03, 15), new DateTime(2027, 03, 15), "Majorna", 5, Emma, "Unospelträff");


		Bokningar.HamtaBokningar().bokningar.Add(unotraff);
		Bokningar.HamtaBokningar().bokningar.Add(Codtraff);

		unotraff.Anmal(Rednaxela);
		Codtraff.Anmal(Pelle);
	}

}
