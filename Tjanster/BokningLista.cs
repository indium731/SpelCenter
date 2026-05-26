
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
    public List<Bokning> bokningar { get; private set; }

    private List<Sorterare<Bokning>> metoder;
    private int metodIndex;

    private void UppdateraBokningar()
    {
        using var context = _context.CreateDbContext();
        bokningar = context.Bokning.Include(b => b.ansvarig)
                                   .Include(b => b.anmalda)
                                   .Include(b => b.bokadeSpel)
                                   .ToList();
        
        bokningar = metoder[metodIndex].Sortera(bokningar);
    }
    public Bokning LaggTill(string n, DateTime d, DateTime s, string p, int m, Medlem a, string b)
    {
        using var context = _context.CreateDbContext();
        Medlem medlem = context.Medlem.Find(a.Id) ?? throw new Exception();
        Bokning nyBokning = new Bokning(n, d, s, p, m, medlem, b);
        context.Bokning.Add(nyBokning);
        context.SaveChanges();
        UppdateraBokningar();
        return nyBokning;
    }
    public void TaBort(Bokning bokning)
    {
        using var context = _context.CreateDbContext();
        context.Attach(bokning);
        context.Bokning.Remove(bokning);
        context.SaveChanges();
        UppdateraBokningar();
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
    public List<Bokning> OverlappandeBokningar(Bokning bokning)
    {
        return _context.CreateDbContext().Bokning.Where(b => b != bokning && b.startDatum <bokning.slutDatum && b.slutDatum > bokning.startDatum).ToList();
        
    }
    public void AnmalMedlem (Bokning bokning, Medlem medlem)
    {
        using var context = _context.CreateDbContext();
        var dbBokning = context.Bokning.Include(b => b.anmalda).First(b => b.Id == bokning.Id);
        var dbMedlem = context.Medlem.First(m => m.Id == medlem.Id);
        dbBokning.Anmal(dbMedlem);
        context.SaveChanges();
        UppdateraBokningar();
    }
    public void BokaSpel(Bokning bokning, Spel spel)
    {
        using var context = _context.CreateDbContext();
        var dbBokning = context.Bokning.Include(b => b.bokadeSpel).First(b => b.Id == bokning.Id);
        var dbSpel = context.Spel.First(s => s.Id == spel.Id);
        dbBokning.BokaSpel(dbSpel);
        context.SaveChanges();
        UppdateraBokningar();
    }
    public void AvbokaSpel(Bokning bokning, Spel spel)
    {
        using var context = _context.CreateDbContext();
        bokning.AvBokaSpel(spel);
        context.Bokning.Update(bokning);
        UppdateraBokningar();
        context.SaveChanges();
    }
}
