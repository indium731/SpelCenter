using System.Windows;

namespace Labb1_OOP;

public class Bokning 
{
    public Bokning()
    {
        startDatum = new DateTime(2030, 4, 20);
        slutDatum = new DateTime(2030, 4, 21);
        plats  = "Sandgärdet";
        maxAntal = 1;
        ansvarig = Session.HamtaSession().inloggadMedlem;
        beskrivning = "spela schack ensam";
        anmalda = new List<Medlem>();
    }

    public Bokning(DateTime d, DateTime s, string p, int m, Medlem a, string b)
    {

        if (d > s)
        {
           MessageBox.Show($"Startdatum måste vara före slutdatum \n  startdatum: {d}\n slutdatum: {s}"); 
           throw new ArgumentException();
        }

        startDatum = (DateTime)d;
        slutDatum = (DateTime)s;
        plats  = (string)p;
        maxAntal = (int)m;
        ansvarig = (Medlem)a;
        beskrivning = (string)b;
        anmalda = new List<Medlem>();
        bokadeSpel = new List<Spel>();
    }
    public DateTime startDatum;
    public DateTime slutDatum;
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
        return beskrivning.ToString();
    }
}

