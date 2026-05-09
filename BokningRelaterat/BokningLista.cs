
using System.ComponentModel.DataAnnotations;

namespace Labb1_OOP;

public sealed class BokningLista
{
    private BokningLista()
    {
        _bokningar = new List<Bokning>();
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
    private List<Bokning> _bokningar;
    public List<Bokning> bokningar 
    {
        get
        {
            return metoder[metodIndex].Sortera(_bokningar);
        } set;}
    private List<Sorterare<Bokning>> metoder;
    private int metodIndex;

    public void LaggTill(Bokning bokning)
    {
        _bokningar.Add(bokning);
    }
    public void TaBort(Bokning bokning)
    {
        _bokningar.Remove(bokning);
    }
    public void GaTillNastaMetod()
    {
        metodIndex = (metodIndex + 1) % metoder.Count();
    }
    public List<Bokning> Sok(string sokOrd)
    {
        return bokningar.Where(m => metoder[metodIndex].Matchar(m, sokOrd.ToLower())).ToList();
    }
    public string NuvarandeSortering()
    {
        return metoder[metodIndex].sortering;
    }
}
