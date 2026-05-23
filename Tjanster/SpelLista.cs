using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Labb1_OOP.Data;
using Microsoft.EntityFrameworkCore;

namespace Labb1_OOP.Modeller;

public sealed class SpelLista
{
    private IDbContextFactory<SpelCenterDbContext> _context;
    private SpelLista(IDbContextFactory<SpelCenterDbContext> contextFactory)
    {
        _context = contextFactory;
        metoder = new List<Sorterare<Spel>>
        {
            new Sorterare<Spel>(s=>s.namn, "Namn"),
            new Sorterare<Spel>(s=>s.kategori, "Kategori"),
            new Sorterare<Spel>(s=>s.minAntalSpelare, "Minantal spelare"),
            new Sorterare<Spel>(s=>s.maxAntalSpelare, "Maxantal spelare"),
            new Sorterare<Spel>(s=>s.svarighetsgrad, "Svarighetsgrad")
        };
        metodIndex = 0;
    }
    private static SpelLista _instans;
    public static SpelLista HamtaSpelLista()
    {
        if (_instans == null)
        {
            throw new Exception("SpelLista har inte initierats");
        }
        return _instans;
    }
    public static void InitieraSpelLista(IDbContextFactory<SpelCenterDbContext> context)
    {
        if (_instans == null)
        {
            _instans = new SpelLista(context);
        }
    }
    public ObservableCollection<Spel> spel 
    {
        get {
            ObservableCollection<Spel> spel = new ObservableCollection<Spel>(_context.CreateDbContext().Spel.ToList());
            
            spel = metoder[metodIndex].Sortera(spel);

            return spel;
        }
        private set;
        }
    private List<Sorterare<Spel>> metoder;
    private int metodIndex;

    public Spel LaggTill(string n, string k, int a, int m, Svarighetsgrad s, string b)
    {
        Spel nySpel = new Spel(n, k, a, m, s, b);
        _context.CreateDbContext().Spel.Add(nySpel);
        MessageBox.Show("Nytt spel har nu lagts till");
        return nySpel;
    }
    public void TaBort(Spel nyttSpel)
    {
        _context.CreateDbContext().Spel.Remove(nyttSpel);
        MessageBox.Show("Spel har nu tagits bort");
    }
    public void GaTillNastaMetod()
    {
        metodIndex = (metodIndex + 1) % metoder.Count();
    }
    public ObservableCollection<Spel> Sok(string sokOrd)
    {
        return new ObservableCollection<Spel>(spel.Where(m => metoder[metodIndex].Matchar(m, sokOrd.ToLower())).ToList());
    }
    public string NuvarandeSortering()
    {
        return metoder[metodIndex].sortering;
    }
    public ObservableCollection<Spel> RekommenderadeSpel(Bokning bokning)
    {
        return new ObservableCollection<Spel>(spel.Where(spel => spel.minAntalSpelare <= bokning.anmalda.Count && spel.maxAntalSpelare >= bokning.anmalda.Count).ToList());
    }
    public Spel Seed(string n, string k, int a, int m, Svarighetsgrad s, string b)
    {
        Spel nyttSpel = new Spel(n, k, a, m, s, b);
        _context.CreateDbContext().Spel.Add(nyttSpel);
        return nyttSpel;
    }
}