using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Labb1_OOP.Data;  
using Labb1_OOP.Modeller;
using Labb1_OOP.Tjanster;
using Labb1_OOP.VyModeller;
using Labb1_OOP.Vyer;

namespace Labb1_OOP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider tjanstLeverantor { get; private set; }

        public App()
        {
            var tjanster = new ServiceCollection();

            ConfigureServices(tjanster);

            tjanstLeverantor = tjanster.BuildServiceProvider();
        }

        /// <summary>
        /// Registers all application dependencies, services, and view models.
        /// </summary>
        private void ConfigureServices(IServiceCollection tjanster)
        {
            tjanster.AddDbContextFactory<SpelCenterDbContext>(options =>
                options.UseSqlServer("Server=.;Database=SpelCenterDb;Trusted_Connection=True;TrustServerCertificate=True;"));

            tjanster.AddTransient<MedlemLista>();
            tjanster.AddTransient<SpelLista>();
            tjanster.AddTransient<BokningLista>();

            tjanster.AddSingleton<Navigator>();


        }

        /// <summary>
        /// The main entry point override called right when the WPF window system boots up.
        /// </summary>
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                var fabrik = tjanstLeverantor.GetRequiredService<IDbContextFactory<SpelCenterDbContext>>();
                using (var kontext = fabrik.CreateDbContext())
                {
                    kontext.Database.EnsureCreated();
                    
                    if (kontext.Medlem.Any())
                    {
                        KorMainWindow();
                        return; 
                    }
                }

                var medlemService = tjanstLeverantor.GetRequiredService<MedlemLista>();
                var spelService = tjanstLeverantor.GetRequiredService<SpelLista>();
                var bokningService = tjanstLeverantor.GetRequiredService<BokningLista>();

                await SeedAsync(medlemService, spelService, bokningService);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett fel uppstod vid uppstart av databasen:\n{ex.Message}");
            }

            KorMainWindow();
        }

        /// <summary>
        /// Instantiates and displays the MainWindow with its dependencies wired up.
        /// </summary>
        private void KorMainWindow()
        {
            var mainWindow = new MainWindow();
            
            mainWindow.DataContext = new MainWindowVM();
            
            mainWindow.Show();
        }

        /// <summary>
        /// Seeds the database utilizing our newly transient business layers.
        /// </summary>
        private async Task SeedAsync(MedlemLista medlemService, SpelLista spelService, BokningLista bokningService)
    {
        Medlem alexander = await medlemService.LaggTillAsync("Alexander", "0701234567", "S1", true);   
        Medlem lisa = await medlemService.LaggTillAsync("Lisa", "0739876543", "S2", false);
        Medlem pelle = await medlemService.LaggTillAsync("Pelle", "0725551234", "S3", false);
        Medlem emma = await medlemService.LaggTillAsync("Emma", "0761112233", "S4", true);
        Medlem rednaxela = await medlemService.LaggTillAsync("Rednaxela", "070123333", "S5", true);

        Spel uno = await spelService.LaggTillAsync("Uno", "Sällskap", 2, 8, Svarighetsgrad.barnvänligt, "kortspel");
        Spel fyraIRad = await spelService.LaggTillAsync("Fyra i rad", "Sällskap", 2, 2, Svarighetsgrad.barnvänligt, "Få fyra i rad");
        Spel cod = await spelService.LaggTillAsync("Call of duty", "Strategi", 2, 8, Svarighetsgrad.barnvänligt, "Actionfyllt strategispel");
        Spel guitarHero = await spelService.LaggTillAsync("Guitarherokortspelet", "Sällskap", 1, 2, Svarighetsgrad.barnvänligt, "kortspel");
        Spel schack = await spelService.LaggTillAsync("Schack", "Sällskap", 2, 2, Svarighetsgrad.utmanande, "The ROOK!");
        Spel ticketToRide = await spelService.LaggTillAsync("Ticket To Ride", "Sällskap", 2, 5, Svarighetsgrad.mittemellan, "Klassisk brädspel");

        Bokning unoTraff = await bokningService.LaggTillAsync("unoTräff", new DateTime(DateTime.Now.Year + 1, 01, 1), new DateTime(DateTime.Now.Year + 1, 01, 2), "Majorna", 5, alexander, "Unospelträff");
        Bokning codTraff = await bokningService.LaggTillAsync("codTräff", new DateTime(DateTime.Now.Year + 1, 01, 1), new DateTime(DateTime.Now.Year + 1, 01, 2), "Majorna", 5, emma, "Codträff");
        Bokning gottOBlandatTraff = await bokningService.LaggTillAsync("gottOBlandatTräff", new DateTime(DateTime.Now.Year + 1, 06, 1), new DateTime(DateTime.Now.Year + 1, 06, 2), "Majorna", 5, alexander, "Gott O Blandat träff");

        await bokningService.AnmalMedlemAsync(unoTraff, rednaxela);
        await bokningService.AnmalMedlemAsync(unoTraff, lisa);
        await bokningService.AnmalMedlemAsync(codTraff, pelle);
        await bokningService.AnmalMedlemAsync(codTraff, emma);
        await bokningService.AnmalMedlemAsync(gottOBlandatTraff, lisa);
        await bokningService.AnmalMedlemAsync(gottOBlandatTraff, pelle);
        await bokningService.AnmalMedlemAsync(gottOBlandatTraff, rednaxela);

        await bokningService.BokaSpelAsync(unoTraff, uno);
        await bokningService.BokaSpelAsync(codTraff, cod);
        await bokningService.BokaSpelAsync(gottOBlandatTraff, uno);
        await bokningService.BokaSpelAsync(gottOBlandatTraff, cod);
        await bokningService.BokaSpelAsync(gottOBlandatTraff, guitarHero);
    }
    }
}