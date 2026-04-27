namespace Labb1_OOP;

public class Bokning 
{
    public Bokning()
    {
        datum = "Söndag";
        tid = "14:00";
        plats  = "Sandgärdet";
        maxAntal = "1";
        ansvarig = "Jag";
        beskrivning = "spela schack ensam";
    }

    public Bokning(string d, string t, string p, string m, string a, string b)
    {
        datum = d;
        tid = t;
        plats  = p;
        maxAntal = m;
        ansvarig = a;
        beskrivning = b;
    }
    public string datum;
    public string tid;
    public string plats;
    public string maxAntal;
    public string ansvarig;
    public string beskrivning;

    public override string ToString()
    {
        return ansvarig.ToString();
    }
}

