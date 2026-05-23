using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Labb1_OOP.Data;

namespace Labb1_OOP.Modeller;

public sealed class SpelLista
{
    private SpelCenterDbContext _context;
    private SpelLista(SpelCenterDbContext context)
    {
        _context = context;
        spel = new ObservableCollection<Spel>();
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

    public ObservableCollection<Spel> spel 
    {
        get {
            ObservableCollection<Spel> spel = new ObservableCollection<Spel>(_context.Spel);
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
        _context.Spel.Add(nySpel);
        MessageBox.Show("Nytt spel har nu lagts till");
        return nySpel;
    }
    public void TaBort(Spel nyttSpel)
    {
        _context.Spel.Remove(nyttSpel);
        MessageBox.Show("Spel har nu tagits bort");
    }
    public void GaTillNastaMetod()
    {
        metodIndex = (metodIndex + 1) % metoder.Count();
    }
    public ObservableCollection<Spel> Sok(string sokOrd)
    {
        return new ObservableCollection<Spel>(spel.Where(m => metoder[metodIndex].Matchar(m, sokOrd.ToLower())));
    }
    public string NuvarandeSortering()
    {
        return metoder[metodIndex].sortering;
    }
    public ObservableCollection<Spel> RekommenderadeSpel(Bokning bokning)
    {
        return new ObservableCollection<Spel>(spel.Where(spel => spel.minAntalSpelare <= bokning.anmalda.Count && spel.maxAntalSpelare >= bokning.anmalda.Count));
    }
    public Spel Seed(string n, string k, int a, int m, Svarighetsgrad s, string b)
    {
        Spel nyttSpel = new Spel(n, k, a, m, s, b);
        spel.Add(nyttSpel);
        return nyttSpel;
    }
}