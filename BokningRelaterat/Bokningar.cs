
using System.ComponentModel.DataAnnotations;

namespace Labb1_OOP;

public sealed class Bokningar
{
    private Bokningar()
    {
        _bokningar = new List<Bokning>();
        metoder = new List<Sorterare<Bokning>>
        {
            new Sorterare<Bokning>(b=>b.beskrivning, "Beskrivning"),
            new Sorterare<Bokning>(b=>b.ansvarig.namn, "Ansvarig"),
            new Sorterare<Bokning>(b=>b.startDatum, "Börjar"),
            new Sorterare<Bokning>(b=>b.slutDatum, "Avslutas"),
            new Sorterare<Bokning>(b=>b.plats, "Plats"),
            new Sorterare<Bokning>(b=>b.maxAntal, "maxantal"),
        };
        metodIndex = 0;
    }

    private static Bokningar _instans;
 
    public static Bokningar HamtaBokningar()
    {
        if (_instans == null)
        {
            _instans = new Bokningar();
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
