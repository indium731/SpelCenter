using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Labb1_OOP.Data;
using Labb1_OOP.Modeller;
using Microsoft.EntityFrameworkCore;


namespace Labb1_OOP.Tjanster;

public class MedlemLista
{
    private IDbContextFactory<SpelCenterDbContext> _context;
    public MedlemLista(IDbContextFactory<SpelCenterDbContext> context)
    {
        _context = context;
    }
    public List<Medlem> medlemmar  {
        get
        {
            if (field == null)
            {
                using var context = _context.CreateDbContext();
                field = context.Medlem.Include(m => m.medlemSkap).ToList();
            }
            return field;
        } private set; } = null;

    private async Task UppdateraMedlemmarAsync()
    {
        await using var context = await _context.CreateDbContextAsync();
        medlemmar = await context.Medlem.Include(m => m.medlemSkap).ToListAsync();
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
    public Medlem? TestaInlogg(string inlogg)
    {
        Medlem medlem = medlemmar.FirstOrDefault(m => m.medlemsNummer == inlogg);
        if (medlem == null) throw new Exception("Felaktigt inlogg");
        if (!medlem.medlemSkap.medlemStatus) throw new Exception("medlemskap är ej aktivt");
        Session.HamtaSession().inloggadMedlem = medlem;
        return medlem;
    }
}
