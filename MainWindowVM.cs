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
		Navigator.NavigeraTill(new InloggVyVM(Navigator));
	}

	private async void Seed()
	{


		Medlem Alexander = await MedlemLista.HamtaMedlemLista().LaggTillAsync("Alexander", "0701234567", "S1", true);   
		Medlem Lisa = await MedlemLista.HamtaMedlemLista().LaggTillAsync("Lisa", "0739876543", "S2", false);
		Medlem Pelle = await  MedlemLista.HamtaMedlemLista().LaggTillAsync("Pelle", "0725551234", "S3", false);
		Medlem Emma = await MedlemLista.HamtaMedlemLista().LaggTillAsync("Emma", "0761112233", "S4", true);
		Medlem Rednaxela = await MedlemLista.HamtaMedlemLista().LaggTillAsync("Rednaxela", "070123333", "S5", true);

		Spel uno = await SpelLista.HamtaSpelLista().LaggTillAsync("Uno", "Sällskap", 2, 8, Svarighetsgrad.barnvänligt , "kortspel");
		Spel fyraIRad = await SpelLista.HamtaSpelLista().LaggTillAsync("Fyra i rad", "Sällskap", 2, 2, Svarighetsgrad.barnvänligt, "Få fyra i rad");
		Spel cod = await SpelLista.HamtaSpelLista().LaggTillAsync("Call of duty", "Strategi", 2, 8, Svarighetsgrad.barnvänligt, "Actionfyllt strategispel");
		Spel guitarHero = await SpelLista.HamtaSpelLista().LaggTillAsync("Guitarherokortspelet", "Sällskap", 1, 2, Svarighetsgrad.barnvänligt, "kortspel");
		Spel schack = await SpelLista.HamtaSpelLista().LaggTillAsync("Schack", "Sällskap", 2, 2, Svarighetsgrad.utmanande, "The ROOK!");
		Spel ticketToRide = await SpelLista.HamtaSpelLista().LaggTillAsync("Ticket To Ride", "Sällskap", 2, 5, Svarighetsgrad.mittemellan, "Klassisk brädspel");

		Bokning unoTraff = await BokningLista.HamtaBokningLista().LaggTillAsync("unoTräff",new DateTime(DateTime.Now.Year+1, 01, 1), new DateTime(DateTime.Now.Year+1, 01, 2), "Majorna", 5, Alexander, "Unospelträff");
		Bokning codTraff = await BokningLista.HamtaBokningLista().LaggTillAsync("codTräff",new DateTime(DateTime.Now.Year+1, 01, 1), new DateTime(DateTime.Now.Year+1, 01, 2), "Majorna", 5, Emma, "Codträff");
		Bokning gottOBlandatTraff = await BokningLista.HamtaBokningLista().LaggTillAsync("gottOBlandatTräff",new DateTime(DateTime.Now.Year+1, 06, 1), new DateTime(DateTime.Now.Year+1, 06, 2), "Majorna", 5, Alexander, "Gott O Blandat träff");

  
		BokningLista.HamtaBokningLista().AnmalMedlemAsync(unoTraff, Rednaxela);
		BokningLista.HamtaBokningLista().AnmalMedlemAsync(unoTraff, Lisa);
		BokningLista.HamtaBokningLista().AnmalMedlemAsync(codTraff, Pelle);
		BokningLista.HamtaBokningLista().AnmalMedlemAsync(codTraff, Emma);
		BokningLista.HamtaBokningLista().AnmalMedlemAsync(gottOBlandatTraff, Lisa);
		BokningLista.HamtaBokningLista().AnmalMedlemAsync(gottOBlandatTraff, Pelle);
		BokningLista.HamtaBokningLista().AnmalMedlemAsync(gottOBlandatTraff, Rednaxela);

		BokningLista.HamtaBokningLista().BokaSpelAsync(unoTraff, uno);
		BokningLista.HamtaBokningLista().BokaSpelAsync(codTraff, cod);
		BokningLista.HamtaBokningLista().BokaSpelAsync(gottOBlandatTraff, uno);
		BokningLista.HamtaBokningLista().BokaSpelAsync(gottOBlandatTraff, cod);
		BokningLista.HamtaBokningLista().BokaSpelAsync(gottOBlandatTraff, guitarHero);



	}
}
}
