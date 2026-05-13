
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace Labb1_OOP;

public sealed class BokningLista
{
    private BokningLista()
    {
        bokningar = new List<Bokning>();
        metoder = new List<Sorterare<Bokning>>
        {
            new Sorterare<Bokning>(b=>b.beskrivning, "Beskrivning"),
            new Sorterare<Bokning>(b=>b.ansvarig.namn, "Ansvarig"),
            new Sorterare<Bokning>(b=>b.startDatum.Date, "Startdatum"),
            new Sorterare<Bokning>(b=>b.slutDatum.Date, "Slutdatum"),
            new Sorterare<Bokning>(b=>b.plats, "Plats"),
            new Sorterare<Bokning>(b=>b.maxAntal, "Maxantal"),
        };
        metodIndex = 0;
    }

    private static BokningLista _instans;
 
    public static BokningLista HamtaBokningLista()
    {
        if (_instans == null)
        {
            _instans = new BokningLista();
        }
        return _instans;
    }
    public List<Bokning> bokningar 
    {
        get => field;
        private set;
        }
    private List<Sorterare<Bokning>> metoder;
    private int metodIndex;

    public Bokning LaggTill(DateTime d, DateTime s, string p, int m, Medlem a, string b)
    {
        Bokning nyttBokning = new Bokning(d, s, p, m, a, b);
        bokningar.Add(nyttBokning);
        return nyttBokning;
    }
    public void TaBort(Bokning bokning)
    {
        bokningar.Remove(bokning);
        MessageBox.Show("Bokning har nu tagits bort");

    }
    public void GaTillNastaMetod()
    {
        metodIndex = (metodIndex + 1) % metoder.Count();
        bokningar = metoder[metodIndex].Sortera(bokningar);
    }
    public List<Bokning> Sok(string sokOrd)
    {
        return bokningar.Where(m => metoder[metodIndex].Matchar(m, sokOrd.ToLower())).ToList();
    }
    public string NuvarandeSortering()
    {
        return metoder[metodIndex].sortering;
    }
    public List<Bokning> OverlappandeBokningar(Bokning bokning)
    {
        return bokningar.Where(b => b != bokning && b.startDatum <bokning.slutDatum && b.slutDatum > bokning.startDatum).ToList();
        
    }
    public Bokning Seed(DateTime d, DateTime s, string p, int m, Medlem a, string b)
    {
        Bokning nyttBokning = new Bokning(d, s, p, m, a, b);
        bokningar.Add(nyttBokning);
        return nyttBokning;
    }
}
