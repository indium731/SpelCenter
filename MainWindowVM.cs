using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb1_OOP.Modeller;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using Labb1_OOP.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Labb1_OOP.VyModeller
{


public partial class MainWindowVM : ObservableObject
{
    [ObservableProperty]
    public partial ObservableObject Vy { get; set; }

	public Navigator Navigator {get; set;} 
    public MainWindowVM()
    {
		try{
		var options = new DbContextOptionsBuilder<SpelCenterDbContext>()
			.UseSqlServer("Server=.;Database=SpelCenterDb;Trusted_Connection=True;TrustServerCertificate=True;")
			.Options;

		IDbContextFactory<SpelCenterDbContext> fabrik =
			new PooledDbContextFactory<SpelCenterDbContext>(options);

		MedlemLista.InitieraMedlemLista(fabrik);
		SpelLista.InitieraSpelLista(fabrik);
		BokningLista.InitieraBokningLista(fabrik);
		
		fabrik.CreateDbContext().Database.EnsureDeleted();
		fabrik.CreateDbContext().Database.EnsureCreated();
		Seed();

		Navigator = new Navigator();
		Navigator.NavigeraTill(new InloggVM(Navigator));
		}catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void Seed()
	{

		Medlem Alexander = MedlemLista.HamtaMedlemLista().Seed("Alexander", "0701234567", "S1", true);   
		Medlem Lisa = MedlemLista.HamtaMedlemLista().Seed("Lisa", "0739876543", "S2", false);
		Medlem Pelle = MedlemLista.HamtaMedlemLista().Seed("Pelle", "0725551234", "S3", false);
		Medlem Emma = MedlemLista.HamtaMedlemLista().Seed("Emma", "0761112233", "S4", true);
		Medlem Rednaxela = MedlemLista.HamtaMedlemLista().Seed("Rednaxela", "070123333", "S5", true);

		Spel uno = SpelLista.HamtaSpelLista().Seed("Uno", "Sällskap", 2, 8, Svarighetsgrad.barnvänligt , "kortspel");
		Spel fyraIRad = SpelLista.HamtaSpelLista().Seed("Fyra i rad", "Sällskap", 2, 2, Svarighetsgrad.barnvänligt, "Få fyra i rad");
		Spel cod = SpelLista.HamtaSpelLista().Seed("Call of duty", "Strategi", 2, 8, Svarighetsgrad.barnvänligt, "Actionfyllt strategispel");
		Spel guitarHero = SpelLista.HamtaSpelLista().Seed("Guitarherokortspelet", "Sällskap", 1, 2, Svarighetsgrad.barnvänligt, "kortspel");
		Spel schack = SpelLista.HamtaSpelLista().Seed("Schack", "Sällskap", 2, 2, Svarighetsgrad.utmanande, "The ROOK!");
		Spel ticketToRide = SpelLista.HamtaSpelLista().Seed("Ticket To Ride", "Sällskap", 2, 5, Svarighetsgrad.mittemellan, "Klassisk brädspel");

		Bokning unoTraff = BokningLista.HamtaBokningLista().Seed(new DateTime(DateTime.Now.Year+1, 01, 1), new DateTime(DateTime.Now.Year+1, 01, 2), "Majorna", 5, Alexander, "Unospelträff");
		Bokning codTraff = BokningLista.HamtaBokningLista().Seed(new DateTime(DateTime.Now.Year+1, 01, 1), new DateTime(DateTime.Now.Year+1, 01, 2), "Majorna", 5, Emma, "Codträff");
		Bokning gottOBlandatTraff = BokningLista.HamtaBokningLista().Seed(new DateTime(DateTime.Now.Year+1, 01, 1), new DateTime(DateTime.Now.Year+1, 01, 2), "Majorna", 5, Alexander, "Gott O Blandat träff");

		unoTraff.SeedAnmal(Rednaxela);
		unoTraff.SeedAnmal(Lisa);
		codTraff.SeedAnmal(Pelle);
		codTraff.SeedAnmal(Emma);
		gottOBlandatTraff.SeedAnmal(Lisa);
		gottOBlandatTraff.SeedAnmal(Pelle);
		gottOBlandatTraff.SeedAnmal(Rednaxela);

		unoTraff.SeedBokaSpel(uno);
		codTraff.SeedBokaSpel(cod);
		gottOBlandatTraff.SeedBokaSpel(uno);
		gottOBlandatTraff.SeedBokaSpel(cod);
		gottOBlandatTraff.SeedBokaSpel(guitarHero);


	}
}
}
