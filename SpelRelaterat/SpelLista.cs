using System.Windows;

namespace Labb1_OOP;

public sealed class SpelLista
{
    private SpelLista()
    {
        spel = new List<Spel>();
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
    public List<Spel> spel 
    {
        get => field;
        private set;
        }
    private List<Sorterare<Spel>> metoder;
    private int metodIndex;

    public Spel LaggTill(string n, string k, int a, int m, Svarighetsgrad s, string b)
    {
        Spel nyttSpel = new Spel(n, k, a, m, s, b);
        spel.Add(nyttSpel);
        MessageBox.Show("Nytt spel har nu lagts till");
        return nyttSpel;
    }
    public void TaBort(Spel nyttSpel)
    {
        spel.Remove(nyttSpel);
        MessageBox.Show("Spel har nu tagits bort");
    }
    public void GaTillNastaMetod()
    {
        metodIndex = (metodIndex + 1) % metoder.Count();
        spel = metoder[metodIndex].Sortera(spel);
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
        return spel.Where(spel => spel.minAntalSpelare <= bokning.anmalda.Count && spel.maxAntalSpelare >= bokning.anmalda.Count).ToList();
    }
    public Spel Seed(string n, string k, int a, int m, Svarighetsgrad s, string b)
    {
        Spel nyttSpel = new Spel(n, k, a, m, s, b);
        spel.Add(nyttSpel);
        return nyttSpel;
    }
}