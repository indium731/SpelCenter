
using System.Windows;

namespace Labb1_OOP.Modeller;

public class Spel
{


    public Spel()
    {
    }
    public Spel(string n, string k, int a, int m, Svarighetsgrad s, string b)
    {
        namn = n;
        kategori = k;
        maxAntalSpelare = m;
        minAntalSpelare = a;
        svarighetsgrad = s;
        beskrivning = b;

    }
    public int Id { get; set; }
    public string namn { get; set; }
    public string kategori { get; set; }
    public int minAntalSpelare {get => field; set
        {
            if (value > maxAntalSpelare)
            {
                throw new ArgumentException("Minimum antal spelare måste vara färre än maximum");
            }
            if (value < 0)
            {
                throw new ArgumentException("Minimum antal spelare måste vara mer än 0");
            }
            field = value;
        }
    } = 0;
    public int maxAntalSpelare {get => field; set
        {
            if (value < maxAntalSpelare)
            {
                throw new ArgumentException("Max antal spelare måste vara fler än minimum antal");
            }
            if (value < 0)
            {
                throw new ArgumentException("Maximum antal spelare måste vara mer än 0");
            }
            field = value;
        } 
    } = 0;
    public Svarighetsgrad svarighetsgrad { get; set; }
    public string beskrivning { get; set; }
    public List<Bokning> bokningar { get; set; } = new();

}
