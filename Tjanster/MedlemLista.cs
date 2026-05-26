using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Labb1_OOP.Data;
using Microsoft.EntityFrameworkCore;


namespace Labb1_OOP.Modeller;

public sealed class MedlemLista
{
    private IDbContextFactory<SpelCenterDbContext> _context;
    private MedlemLista(IDbContextFactory<SpelCenterDbContext> context)
    {
        _context = context;
        metoder = new List<Sorterare<Medlem>>
        {
            new Sorterare<Medlem>(m=>m.namn, "Namn"),
            new Sorterare<Medlem>(m=>m.medlemSkap.medlemStatus, "MedlemStatus"),
            new Sorterare<Medlem>(m=>m.medlemSkap.startDatum, "Startdatum"),
            new Sorterare<Medlem>(m=>m.medlemSkap.slutDatum, "Slutdatum"),
            new Sorterare<Medlem>(m=>m.admin, "Admin"),
        };
        metodIndex = 0;
    }
    private static MedlemLista _instans;
    public static MedlemLista HamtaMedlemLista()
    {
        if (_instans == null)
        {
            throw new Exception("MedlemLista har inte initierats");
        }
        return _instans;
    }
    public static void InitieraMedlemLista(IDbContextFactory<SpelCenterDbContext> context)
    {
        if (_instans == null)
        {
            _instans = new MedlemLista(context);
        }
    }
    public List<Medlem> medlemmar  { get; private set; }
    private List<Sorterare<Medlem>> metoder;
    private int metodIndex;

    private async Task UppdateraMedlemmarAsync()
    {
        await using var context = await _context.CreateDbContextAsync();
        medlemmar = await context.Medlem.Include(m => m.medlemSkap).ToListAsync();
        medlemmar = metoder[metodIndex].Sortera(medlemmar);
    }
    public async Task<Medlem> LaggTillAsync(string n, string t, string m, bool a)
    {
        await using var context = await _context.CreateDbContextAsync();
        Medlem nyMedlem = new Medlem(n, t, m, a);
        context.Medlem.Add(nyMedlem);
        await context.SaveChangesAsync();
        await UppdateraMedlemmarAsync();
        return nyMedlem;
    }

    public async Task TaBortAsync(Medlem medlem)
    {
        await using var context = await _context.CreateDbContextAsync();
        context.Medlem.Remove(medlem);
        context.SaveChanges();
        await UppdateraMedlemmarAsync();
    }
    public async Task<Medlem> SparaMedlemAsync(Medlem medlem)
    {
        await using var context = await _context.CreateDbContextAsync();
        context.Medlem.Update(medlem);
        context.SaveChanges();
        await UppdateraMedlemmarAsync();
        return medlem;

    }
    public void GaTillNastaMetod()
    {
        metodIndex = (metodIndex + 1) % metoder.Count();
        UppdateraMedlemmarAsync();
    }
    public List<Medlem> Sok(string sokOrd)
    {
        return medlemmar.Where(m => metoder[metodIndex].Matchar(m, sokOrd.ToLower())).ToList();
    }
    public string NuvarandeSortering()
    {
        return metoder[metodIndex].sortering;
    }
}
