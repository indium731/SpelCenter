using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Labb1_OOP.Data;
using Labb1_OOP.Modeller;
using Microsoft.EntityFrameworkCore;

namespace Labb1_OOP.Tjanster;

public class SpelLista
{
    private IDbContextFactory<SpelCenterDbContext> _context;
    public SpelLista(IDbContextFactory<SpelCenterDbContext> contextFactory)
    {
        _context = contextFactory;
    }
    public List<Spel> spel  {
        get
        {
            if (field == null)
            {
                using var context = _context.CreateDbContext();
                field = context.Spel.ToList();
            }
            return field;
        } private set; } = null;

    private async Task UppdateraSpelAsync()
    {
        await using var context = await _context.CreateDbContextAsync();
        spel = await context.Spel.ToListAsync();
    }
    public async Task<Spel> LaggTillAsync(string n, string k, int a, int m, Svarighetsgrad s, string b)
    {
        await using var context = await _context.CreateDbContextAsync();
        Spel nySpel = new Spel(n, k, a, m, s, b);
        context.Spel.Add(nySpel);
        await context.SaveChangesAsync();
        await UppdateraSpelAsync();
        return nySpel;
    }
    public async Task TaBortAsync(Spel nyttSpel)
    {
        await using var context = await _context.CreateDbContextAsync();
        context.Spel.Remove(nyttSpel);
        await context.SaveChangesAsync();
        await UppdateraSpelAsync();
    }
    public async Task<Spel> SparaSpelAsync(Spel spel)
    {
        await using var context = await _context.CreateDbContextAsync();
        context.Spel.Update(spel);
        context.SaveChanges();
        await UppdateraSpelAsync();
        return spel;
    }
    public List<Spel> RekommenderadeSpel(Bokning bokning)
    {
        return spel.Where(spel => spel.minAntalSpelare <= bokning.anmalda.Count && spel.maxAntalSpelare >= bokning.anmalda.Count).ToList();
    }
}