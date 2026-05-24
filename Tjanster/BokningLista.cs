
using System.ComponentModel.DataAnnotations;
using System.Windows;
using Labb1_OOP.Modeller;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Labb1_OOP.Data;
using Microsoft.EntityFrameworkCore;

namespace Labb1_OOP;

public sealed class BokningLista
{
    private IDbContextFactory<SpelCenterDbContext> _context;
    private BokningLista(IDbContextFactory<SpelCenterDbContext> contextFactory)
    {
        _context = contextFactory;
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
            throw new Exception("BokningLista har inte initierats");
        }
        return _instans;
    }
    public static void InitieraBokningLista(IDbContextFactory<SpelCenterDbContext> contextFactory)
    {
        if (_instans == null)
        {
            _instans = new BokningLista(contextFactory);
        }
    }
    public ObservableCollection<Bokning> bokningar { get; private set; }

    private List<Sorterare<Bokning>> metoder;
    private int metodIndex;

    private void UppdateraBokningar()
    {
        var context = _context.CreateDbContext();
        var lista = context.Bokning.Include(b => b.ansvarig).ToList();
        bokningar = new ObservableCollection<Bokning>(lista);
        bokningar = metoder[metodIndex].Sortera(bokningar);
    }
    public Bokning LaggTill(DateTime d, DateTime s, string p, int m, Medlem a, string b)
    {
        var context = _context.CreateDbContext();
        Bokning nyBokning = new Bokning(d, s, p, m, a, b);
        context.Bokning.Add(nyBokning);
        context.SaveChanges();
        UppdateraBokningar();
        return nyBokning;
    }
    public void TaBort(Bokning bokning)
    {
        var context = _context.CreateDbContext();
        context.Bokning.Remove(bokning);
        context.SaveChanges();
        UppdateraBokningar();
    }
    public void GaTillNastaMetod()
    {
        metodIndex = (metodIndex + 1) % metoder.Count();
    }
    public ObservableCollection<Bokning> Sok(string sokOrd)
    {
        return new ObservableCollection<Bokning>(bokningar.Where(m => metoder[metodIndex].Matchar(m, sokOrd.ToLower())));
    }
    public string NuvarandeSortering()
    {
        return metoder[metodIndex].sortering;
    }
    public ObservableCollection<Bokning> OverlappandeBokningar(Bokning bokning)
    {
        return new ObservableCollection<Bokning>(_context.CreateDbContext().Bokning.Where(b => b != bokning && b.startDatum <bokning.slutDatum && b.slutDatum > bokning.startDatum));
        
    }
    public Bokning Seed(DateTime d, DateTime s, string p, int m, Medlem a, string b)
    {
        var context = _context.CreateDbContext();
        context.Attach(a);
        Bokning nyBokning = new Bokning(d, s, p, m, a, b);
        context.Bokning.Add(nyBokning);
        context.SaveChanges();
        UppdateraBokningar();
        return nyBokning;
    }
    public void AnmalMedlem (Bokning bokning, Medlem medlem)
    {
        var context = _context.CreateDbContext();
        context.Attach(bokning);
        context.Attach(medlem);
        bokning.Anmal(medlem);
        UppdateraBokningar();
        context.SaveChanges();
    }
    public void BokaSpel(Bokning bokning, Spel spel)
    {
        var context = _context.CreateDbContext();
        context.Attach(bokning);
        context.Attach(spel);
        bokning.BokaSpel(spel);
        UppdateraBokningar();
        context.SaveChanges();
    }
    public void AvbokaSpel(Bokning bokning, Spel spel)
    {
        bokning.AvBokaSpel(spel);
        var context = _context.CreateDbContext();
        context.Bokning.Update(bokning);
        UppdateraBokningar();
        context.SaveChanges();
    }
}
