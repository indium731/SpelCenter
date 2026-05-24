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
    public ObservableCollection<Medlem> medlemmar  { get; private set; }
    private List<Sorterare<Medlem>> metoder;
    private int metodIndex;

    private void UppdateraMedlemmar()
    {
        medlemmar = new ObservableCollection<Medlem>(_context.CreateDbContext().Medlem.Include(m => m.medlemSkap).ToList());
        medlemmar = metoder[metodIndex].Sortera(medlemmar);
    }
    public Medlem LaggTill(string n, string t, string m, bool a)
    {
        var context = _context.CreateDbContext();
        Medlem nyMedlem = new Medlem(n, t, m, a);
        context.Medlem.Add(nyMedlem);
        context.SaveChanges();
        UppdateraMedlemmar();
        return nyMedlem;
    }

    public void TaBort(Medlem medlem)
    {
        var context = _context.CreateDbContext();
        context.Medlem.Remove(medlem);
        context.SaveChanges();
        UppdateraMedlemmar();
    }
    public void GaTillNastaMetod()
    {
        metodIndex = (metodIndex + 1) % metoder.Count();
        UppdateraMedlemmar();
    }
    public ObservableCollection<Medlem> Sok(string sokOrd)
    {
        return new ObservableCollection<Medlem>(medlemmar.Where(m => metoder[metodIndex].Matchar(m, sokOrd.ToLower())));
    }
    public string NuvarandeSortering()
    {
        return metoder[metodIndex].sortering;
    }
    public Medlem Seed(string n, string t, string m, bool a)
    {
        using var context = _context.CreateDbContext();
        Medlem nyMedlem = new Medlem(n, t, m, a);
        context.Medlem.Add(nyMedlem);
        context.SaveChanges();
        UppdateraMedlemmar();
        return nyMedlem;
    }
}
