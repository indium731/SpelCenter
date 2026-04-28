namespace Labb1_OOP;

public class Bokning 
{
    public Bokning()
    {
        datum = new DateTime(2030, 4, 20);
        plats  = "Sandgärdet";
        maxAntal = "1";
        ansvarig = new Medlem();
        beskrivning = "spela schack ensam";
        anmalda = new List<Medlem>();
    }

    public Bokning(DateTime d, string p, string m, Medlem a, string b)
    {
        datum = d;
        plats  = p;
        maxAntal = m;
        ansvarig = a;
        beskrivning = b;
        anmalda = new List<Medlem>();
    }
    public DateTime datum;
    public string plats;
    public string maxAntal;
    public Medlem ansvarig;
    public string beskrivning;
    public List<Medlem> anmalda;

    public override string ToString()
    {
        return ansvarig.ToString();
    }
}

