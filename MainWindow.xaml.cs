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

		Medlem Alexander = MedlemLista.HamtaMedlemLista().LaggTill("Alexander", "0701234567", "S1", true);   
		Medlem Lisa = MedlemLista.HamtaMedlemLista().LaggTill("Lisa", "0739876543", "S2", false);
		Medlem Pelle = MedlemLista.HamtaMedlemLista().LaggTill("Pelle", "0725551234", "S3", false);
		Medlem Emma = MedlemLista.HamtaMedlemLista().LaggTill("Emma", "0761112233", "S4", true);
		Medlem Rednaxela = MedlemLista.HamtaMedlemLista().LaggTill("Rednaxela", "070123333", "S5", true);



		SpelLista.HamtaSpelLista().LaggTill("Uno", "Sällskap", 2, 8, Svarighetsgrad.barnvänligt , "kortspel");
		SpelLista.HamtaSpelLista().LaggTill("Fyra i rad", "Sällskap", 2, 2, Svarighetsgrad.barnvänligt, "Få fyra i rad");
		SpelLista.HamtaSpelLista().LaggTill("Call of duty", "Strategi", 2, 8, Svarighetsgrad.barnvänligt, "Actionfyllt strategispel");
		SpelLista.HamtaSpelLista().LaggTill("Guitarherokortspelet", "Sällskap", 1, 2, Svarighetsgrad.barnvänligt, "kortspel");





		Bokning unotraff = BokningLista.HamtaBokningLista().LaggTill(new DateTime(2027, 03, 12), new DateTime(2027, 03, 12), "Majorna", 5, Alexander, "Unospelträff");
		Bokning Codtraff = BokningLista.HamtaBokningLista().LaggTill(new DateTime(2027, 03, 15), new DateTime(2027, 03, 15), "Majorna", 5, Emma, "Unospelträff");

		unotraff.Anmal(Rednaxela);
		Codtraff.Anmal(Pelle);
	}

}
