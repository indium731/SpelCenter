
using System.Windows;

namespace Labb1_OOP;

public class Spel 
{
    public Spel()
    {
        namn = "Schack";
        kategori= "Strategi";
        minAntalSpelare = 2;
        maxAntalSpelare = 2;
        svarighetsgrad = Svarighetsgrad.mittemellan;
        beskrivning = "The ROOK!";
    }

    public Spel(string n, string k, int a, int m, Svarighetsgrad s, string b)
    {
        if (a > m)
        {
            MessageBox.Show("minimum antal spelare måste vara lägre än max antal spelare");
            throw new ArgumentException();
        }
        namn = n;
        kategori = k;
        minAntalSpelare = a;
        maxAntalSpelare = m;
        svarighetsgrad = s;
        beskrivning = b;

    }
    public string namn;
    public string kategori;
    public int minAntalSpelare;
    public int maxAntalSpelare;
    public Svarighetsgrad svarighetsgrad;
    public string beskrivning;

    public override string ToString()
    {
        return namn;
    }
}
