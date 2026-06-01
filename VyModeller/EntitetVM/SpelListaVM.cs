using System.Collections.ObjectModel;
using Labb1_OOP.Modeller;

namespace Labb1_OOP.VyModeller;

public class SpelListaVM
{    
    public ObservableCollection<SpelEntitetVM> spel  { get; private set; } = new();
    private List<Sorterare<SpelEntitetVM>> metoder;
    private int metodIndex;
    //använder denna bool för att veta om medlemmar måste laddas om från databasen
    private bool sokningVisas = false;
    public SpelListaVM()
    {
        metoder = new List<Sorterare<SpelEntitetVM>>
        {
            new Sorterare<SpelEntitetVM>(s=>s.namn, "Namn"),
            new Sorterare<SpelEntitetVM>(s=>s.kategori, "Kategori"),
            new Sorterare<SpelEntitetVM>(s=>s.minAntalSpelare, "Minantal spelare"),
            new Sorterare<SpelEntitetVM>(s=>s.maxAntalSpelare, "Maxantal spelare"),
            new Sorterare<SpelEntitetVM>(s=>s.svarighetsgrad, "Svarighetsgrad")
        };
        metodIndex = 0;
        UppdateraSpel();
    }

    private void UppdateraSpel()
    {
        spel.Clear();
        SpelLista.HamtaSpelLista().spel.ForEach(s => spel.Add(new SpelEntitetVM(s)));
        sokningVisas = false;
    }
    public async Task LaggTillAsync(string n, string k, int a, int m, Svarighetsgrad s, string b)
    {
        Spel nySpel = await SpelLista.HamtaSpelLista().LaggTillAsync(n, k, a, m, s, b);
        spel.Add(new SpelEntitetVM(nySpel));
        if (sokningVisas) UppdateraSpel();

    }
    public async Task TaBortAsync(SpelEntitetVM spelAttTaBort)
    {
        await SpelLista.HamtaSpelLista().TaBortAsync(spelAttTaBort.TillSpel());
        spel.Remove(spelAttTaBort);
        if (sokningVisas) UppdateraSpel();
    }
    public async Task SparaSpelAsync(SpelEntitetVM spel)
    {
        await SpelLista.HamtaSpelLista().SparaSpelAsync(spel.TillSpel());
    }
    public void GaTillNastaMetod()
    {
        metodIndex = (metodIndex + 1) % metoder.Count();
        UppdateraSpel();
    }
    public void Sok(string sokOrd)
    {
        //Gjort sökfunktionen på detta sätt för att kunna 
        // bibehålla samma pekare för den observablecollection som finns så den uppdateras korrekt
        sokningVisas = true;
        List<SpelEntitetVM> ickeMatchningar = spel.Where(s => !metoder[metodIndex].Matchar(s, sokOrd.ToLower())).ToList();
        ickeMatchningar.ForEach(s => spel.Remove(s));
    }
    public string NuvarandeSortering()
    {
        return metoder[metodIndex].sortering;
    }
}
