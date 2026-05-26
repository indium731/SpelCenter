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
		Navigator.NavigeraTill(new InloggVyVM(Navigator));
		}catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void Seed()
	{


		Medlem Alexander = MedlemLista.HamtaMedlemLista().LaggTill("Alexander", "0701234567", "S1", true);   
		Medlem Lisa = MedlemLista.HamtaMedlemLista().LaggTill("Lisa", "0739876543", "S2", false);
		Medlem Pelle = MedlemLista.HamtaMedlemLista().LaggTill("Pelle", "0725551234", "S3", false);
		Medlem Emma = MedlemLista.HamtaMedlemLista().LaggTill("Emma", "0761112233", "S4", true);
		Medlem Rednaxela = MedlemLista.HamtaMedlemLista().LaggTill("Rednaxela", "070123333", "S5", true);

		Spel uno = SpelLista.HamtaSpelLista().LaggTill("Uno", "Sällskap", 2, 8, Svarighetsgrad.barnvänligt , "kortspel");
		Spel fyraIRad = SpelLista.HamtaSpelLista().LaggTill("Fyra i rad", "Sällskap", 2, 2, Svarighetsgrad.barnvänligt, "Få fyra i rad");
		Spel cod = SpelLista.HamtaSpelLista().LaggTill("Call of duty", "Strategi", 2, 8, Svarighetsgrad.barnvänligt, "Actionfyllt strategispel");
		Spel guitarHero = SpelLista.HamtaSpelLista().LaggTill("Guitarherokortspelet", "Sällskap", 1, 2, Svarighetsgrad.barnvänligt, "kortspel");
		Spel schack = SpelLista.HamtaSpelLista().LaggTill("Schack", "Sällskap", 2, 2, Svarighetsgrad.utmanande, "The ROOK!");
		Spel ticketToRide = SpelLista.HamtaSpelLista().LaggTill("Ticket To Ride", "Sällskap", 2, 5, Svarighetsgrad.mittemellan, "Klassisk brädspel");

		Bokning unoTraff = BokningLista.HamtaBokningLista().LaggTill("unoTräff",new DateTime(DateTime.Now.Year+1, 01, 1), new DateTime(DateTime.Now.Year+1, 01, 2), "Majorna", 5, Alexander, "Unospelträff");
		Bokning codTraff = BokningLista.HamtaBokningLista().LaggTill("codTräff",new DateTime(DateTime.Now.Year+1, 01, 1), new DateTime(DateTime.Now.Year+1, 01, 2), "Majorna", 5, Emma, "Codträff");
		Bokning gottOBlandatTraff = BokningLista.HamtaBokningLista().LaggTill("gottOBlandatTräff",new DateTime(DateTime.Now.Year+1, 06, 1), new DateTime(DateTime.Now.Year+1, 06, 2), "Majorna", 5, Alexander, "Gott O Blandat träff");

  
		BokningLista.HamtaBokningLista().AnmalMedlem(unoTraff, Rednaxela);
		BokningLista.HamtaBokningLista().AnmalMedlem(unoTraff, Lisa);
		BokningLista.HamtaBokningLista().AnmalMedlem(codTraff, Pelle);
		BokningLista.HamtaBokningLista().AnmalMedlem(codTraff, Emma);
		BokningLista.HamtaBokningLista().AnmalMedlem(gottOBlandatTraff, Lisa);
		BokningLista.HamtaBokningLista().AnmalMedlem(gottOBlandatTraff, Pelle);
		BokningLista.HamtaBokningLista().AnmalMedlem(gottOBlandatTraff, Rednaxela);

		BokningLista.HamtaBokningLista().BokaSpel(unoTraff, uno);
		BokningLista.HamtaBokningLista().BokaSpel(codTraff, cod);
		BokningLista.HamtaBokningLista().BokaSpel(gottOBlandatTraff, uno);
		BokningLista.HamtaBokningLista().BokaSpel(gottOBlandatTraff, cod);
		BokningLista.HamtaBokningLista().BokaSpel(gottOBlandatTraff, guitarHero);



	}
}
}
