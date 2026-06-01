
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
        UppdateraBokningarAsync();
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


    private async Task UppdateraBokningarAsync()
    {
        await using var context = _context.CreateDbContext();
        bokningar = await context.Bokning.Include(b => b.ansvarig)
                                   .Include(b => b.anmalda)
                                   .Include(b => b.bokadeSpel)
                                   .ToListAsync();
        
    }
    public async Task<Bokning> LaggTillAsync(string n, DateTime d, DateTime s, string p, int m, Medlem a, string b)
    {
        await using var context = await _context.CreateDbContextAsync ();
        Medlem medlem = await context.Medlem.FindAsync(a.Id) ?? throw new Exception();
        Bokning nyBokning = new Bokning(n, d, s, p, m, medlem, b);
        await context.Bokning.AddAsync(nyBokning);
        await context.SaveChangesAsync();
        await UppdateraBokningarAsync();
        await context.Entry(medlem).Collection(m => m.ansvaradeBokningar).LoadAsync();
        return nyBokning;
    }
    public async Task TaBortAsync(Bokning bokning)
    {
        await using var context = await _context.CreateDbContextAsync();
        context.Attach(bokning);
        context.Bokning.Remove(bokning);
        await context.SaveChangesAsync();
        await UppdateraBokningarAsync();
    }
    public async Task<Bokning> SparaBokningAsync(Bokning bokning)
    {
        await using var context = await _context.CreateDbContextAsync();
        context.Bokning.Update(bokning);
        context.SaveChanges();
        await UppdateraBokningarAsync();
        return bokning;

    }
    public List<Bokning> OverlappandeBokningar(Bokning bokning)
    {
        return _context.CreateDbContext().Bokning.Where(b => b != bokning && b.startDatum <bokning.slutDatum && b.slutDatum > bokning.startDatum).ToList();
        
    }
    public async Task AnmalMedlemAsync (Bokning bokning, Medlem medlem)
    {
        await using var context = await _context.CreateDbContextAsync();
        var dbBokning = await context.Bokning.Include(b => b.anmalda).FirstOrDefaultAsync(b => b.Id == bokning.Id);
        var dbMedlem = await context.Medlem.FirstOrDefaultAsync(m => m.Id == medlem.Id);
        dbBokning.Anmal(dbMedlem);
        dbMedlem.bokningar.Add(dbBokning);
        await context.SaveChangesAsync();
        await UppdateraBokningarAsync();
    }
    public async Task BokaSpelAsync(Bokning bokning, Spel spel)
    {
        await using var context = await _context.CreateDbContextAsync();
        var dbBokning = await context.Bokning.Include(b => b.bokadeSpel).FirstOrDefaultAsync(b => b.Id == bokning.Id);
        var dbSpel = await context.Spel.FirstOrDefaultAsync(s => s.Id == spel.Id);
        dbBokning.BokaSpel(dbSpel);
        dbSpel.bokningar.Add(dbBokning);
        await context.SaveChangesAsync();
        await UppdateraBokningarAsync();
    }
    public async Task AvbokaSpelAsync(Bokning bokning, Spel spel)
    {
        await using var context = await _context.CreateDbContextAsync();
        var dbBokning = await context.Bokning.Include(b => b.bokadeSpel)
                                             .FirstOrDefaultAsync(b => b.Id == bokning.Id);
        bokning.AvBokaSpel(spel);
        context.Bokning.Update(bokning);
        await context.SaveChangesAsync();
        await UppdateraBokningarAsync();
    }
    public List<Bokning> AnsvaradeBokningar(Medlem medlem)
    {
        return bokningar.Where(b => b.ansvarig.Id == medlem.Id).ToList();
    }
}
