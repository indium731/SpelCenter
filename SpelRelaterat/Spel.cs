
using System.Windows;

namespace Labb1_OOP;

public class Spel 
{


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
    public string Detaljer()
    {
        return $"Namn: {namn}\n" +
               $"Kategori: {kategori}\n" +
               $"Antal Spelare: {minAntalSpelare} - {maxAntalSpelare}\n" +
               $"Svårighetsgrad: {svarighetsgrad}\n" +
               $"Beskrivning: {beskrivning}";
    }
}
