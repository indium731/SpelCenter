using System.Windows;

namespace Labb1_OOP;

public sealed class SpelLista
{
    private SpelLista()
    {
        _spel = new List<Spel>();
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
            _instans = new SpelLista();
        }
        return _instans;
    }
    private List<Spel> _spel;
    public List<Spel> spel 
    {
        get
        {
            return metoder[metodIndex].Sortera(_spel);
        } set;}
    private List<Sorterare<Spel>> metoder;
    private int metodIndex;

    public Spel LaggTill(string n, string k, int a, int m, Svarighetsgrad s, string b)
    {
        Spel spel = new Spel(n, k, a, m, s, b);
        _spel.Add(spel);
        MessageBox.Show("Nytt spel har nu lagts till");
        return spel;
    }
    public void TaBort(Spel nyttSpel)
    {
        _spel.Remove(nyttSpel);
        MessageBox.Show("Spel har nu tagits bort");
    }
    public void GaTillNastaMetod()
    {
        metodIndex = (metodIndex + 1) % metoder.Count();
    }
    public List<Spel> Sok(string sokOrd)
    {
        return spel.Where(m => metoder[metodIndex].Matchar(m, sokOrd.ToLower())).ToList();
    }
    public string NuvarandeSortering()
    {
        return metoder[metodIndex].sortering;
    }
    public List<Spel> RekommenderadeSpel(Bokning bokning)
    {
        return _spel.Where(spel => spel.minAntalSpelare <= bokning.anmalda.Count && spel.maxAntalSpelare >= bokning.anmalda.Count).ToList();
    }
}