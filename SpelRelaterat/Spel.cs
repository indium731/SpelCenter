
using System.Windows;

namespace Labb1_OOP;

public class Spel : IListBar
{


    public Spel(string n, string k, int a, int m, Svarighetsgrad s, string b)
    {
        namn = n;
        kategori = k;
        maxAntalSpelare = m;
        minAntalSpelare = a;
        svarighetsgrad = s;
        beskrivning = b;

    }
    public string namn;
    public string kategori;
    private int _minAntalSpelare = 0;
    public int minAntalSpelare {get => _minAntalSpelare; set
        {
            if (value > _maxAntalSpelare)
            {
                MessageBox.Show("Minimum antal spelare måste vara färre än maximum");
                throw new ArgumentException();
            }
            if (value < 0)
            {
                MessageBox.Show("Minimum antal spelare måste vara mer än 0");
                throw new ArgumentException();
            }
            _minAntalSpelare = value;
        }
    }
    private int _maxAntalSpelare = 0;
    public int maxAntalSpelare {get => _minAntalSpelare; set
        {
            if (value < _maxAntalSpelare)
            {
                MessageBox.Show("Max antal spelare måste vara fler än minimum antal");
                throw new ArgumentException();
            }
            if (value < 0)
            {
                MessageBox.Show("Maximum antal spelare måste vara mer än 0");
                throw new ArgumentException();
            }
            _maxAntalSpelare = value;
        }
    }
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
    public string UtokadeDetaljer()
    {
        return $"Namn: {namn}\n" +
               $"Kategori: {kategori}\n" +
               $"Antal Spelare: {minAntalSpelare} - {maxAntalSpelare}\n" +
               $"Svårighetsgrad: {svarighetsgrad}\n" +
               $"Beskrivning: {beskrivning}";
    }
}
