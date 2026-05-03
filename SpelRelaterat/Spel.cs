
namespace Labb1_OOP;

public class Spel 
{
    public Spel()
    {
        namn = "Schack";
        kategori= "Strategi";
        minAntalSpelare = 2;
        maxAntalSpelare = 2;
        svarighetsgrad = "Beror på motståndet";
        beskrivning = "The ROOK!";
    }

    public Spel(string n, string k, int a, int m, string s, string b)
    {
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
    public string svarighetsgrad;
    public string beskrivning;

    public override string ToString()
    {
        return namn;
    }
}
