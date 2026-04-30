namespace Labb1_OOP;

public class Bokning 
{
    public Bokning()
    {
        datum = new DateTime(2030, 4, 20);
        plats  = "Sandgärdet";
        maxAntal = 1;
        ansvarig = new Medlem();
        beskrivning = "spela schack ensam";
        anmalda = new List<Medlem>();
    }

    public Bokning(DateTime d, string p, int m, Medlem a, string b)
    {
        datum = d;
        plats  = p;
        maxAntal = m;
        ansvarig = a;
        beskrivning = b;
        anmalda = new List<Medlem>();
        bokadeSpel = new List<Spel>();
    }
    public DateTime datum;
    public string plats;
    public int maxAntal;
    public Medlem ansvarig;
    public string beskrivning;
    public List<Medlem> anmalda;
    public List<Spel> bokadeSpel;

    public void Anmal(Medlem medlem)
    {
        if (anmalda.Count <= maxAntal) return;
        if (anmalda.Contains(medlem)) return;
        anmalda.Add(medlem);
    }
    public void BokaSpel(Spel spel)
    {
        if (bokadeSpel.Contains(spel)) return;
        bokadeSpel.Add(spel);

    }

    public override string ToString()
    {
        return ansvarig.ToString();
    }
}

