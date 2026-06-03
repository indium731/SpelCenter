using System.Collections.ObjectModel;
using Labb1_OOP.Modeller;
using Labb1_OOP.Tjanster;
using Microsoft.Extensions.DependencyInjection;

namespace Labb1_OOP.VyModeller;

public class BokningListaVM
{
    public ObservableCollection<BokningEntitetVM> bokningar { get; private set; } = new();
    private List<Sorterare<BokningEntitetVM>> metoder;
    private int metodIndex;
    //använder denna bool för att veta om medlemmar måste laddas om från databasen
    private bool sokningVisas = false;
    public BokningListaVM()
    {
        metoder = new List<Sorterare<BokningEntitetVM>>
        {
            new Sorterare<BokningEntitetVM>(b=>b.beskrivning, "Beskrivning"),
            new Sorterare<BokningEntitetVM>(b=>b.ansvarig.namn, "Ansvarig"),
            new Sorterare<BokningEntitetVM>(b=>b.startDatum.Date, "Startdatum"),
            new Sorterare<BokningEntitetVM>(b=>b.slutDatum.Date, "Slutdatum"),
            new Sorterare<BokningEntitetVM>(b=>b.plats, "Plats"),
            new Sorterare<BokningEntitetVM>(b=>b.maxAntal, "Maxantal"),
        };
        metodIndex = 0;
        UppdateraBokningar();
    }

    private void UppdateraBokningar()
    {
        bokningar.Clear();
        App.tjanstLeverantor.GetRequiredService<BokningLista>().bokningar.ForEach(b => bokningar.Add(new BokningEntitetVM(b)));
        sokningVisas = false;
    }
    public async Task LaggTillAsync(string n, DateTime d, DateTime s, string p, int m, Medlem a, string b)
    {
        Bokning bokning = await App.tjanstLeverantor.GetRequiredService<BokningLista>().LaggTillAsync(n,d,s,p,m,a,b);
        bokningar.Add(new BokningEntitetVM(bokning));
        if (sokningVisas) UppdateraBokningar();
    }
    public async Task TaBortAsync(BokningEntitetVM bokning)
    {
        await App.tjanstLeverantor.GetRequiredService<BokningLista>().TaBortAsync(bokning.TillBokning());
        bokningar.Remove(bokning);
        if (sokningVisas) UppdateraBokningar();
    }
    public async Task SparaBokningAsync(BokningEntitetVM bokning)
    {
        await App.tjanstLeverantor.GetRequiredService<BokningLista>().SparaBokningAsync(bokning.TillBokning());
    }
    public void GaTillNastaMetod()
    {
        metodIndex = (metodIndex + 1) % metoder.Count();
        if (sokningVisas) UppdateraBokningar();
    }
    public void Sok(string sokOrd)
    {
        sokningVisas = true;
        //Gjort sökfunktionen på detta sätt för att kunna
        // bibehålla samma pekare för den observablecollection som finns så den uppdateras korrekt
        List<BokningEntitetVM> ickeMatchningar = bokningar.Where(b => !metoder[metodIndex].Matchar(b, sokOrd.ToLower())).ToList();
        ickeMatchningar.ForEach(b => bokningar.Remove(b));
    }
    public string NuvarandeSortering()
    {
        return metoder[metodIndex].sortering;
    }
    public void AnsvaradeBokningar(Medlem medlem)
    {
        sokningVisas = true;
        bokningar.Clear();
        App.tjanstLeverantor.GetRequiredService<BokningLista>().AnsvaradeBokningar(medlem)
            .ForEach(b => bokningar.Add(new BokningEntitetVM(b)));
    }
}
