
namespace Labb1_OOP;

public class Spel 
{
    public Spel()
    {
        namn = "Schack";
        kategori= "Strategi";
        antalSpelare = "2";
        svårighetsgrad = "Beror på motståndet";
        beskrivning = "The ROOK!";
    }

    public Spel(string n, string k, string a, string s, string b)
    {
        namn = n;
        kategori = k;
        antalSpelare = a;
        svårighetsgrad = s;
        beskrivning = b;

    }
    public string namn;
    public string kategori;
    public string antalSpelare;
    public string svårighetsgrad;
    public string beskrivning;

    public override string ToString()
    {
        return namn;
    }
}
