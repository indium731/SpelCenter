using System.Windows;

namespace Labb1_OOP;

public class Bokning 
{


    public Bokning(DateTime d, DateTime s, string p, int m, Medlem a, string b)
    {

        if (d > s)
        {
           MessageBox.Show($"Startdatum måste vara före slutdatum \n startdatum: {d}\n slutdatum: {s}"); 
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
        if (anmalda.Count >= maxAntal)
        {
            MessageBox.Show("Bokningen är fullbokad");
            return;
        } 
        if (anmalda.Contains(medlem))
        {
            MessageBox.Show("Du har redan anmält dig till denna bokning");
            return;
        }
        anmalda.Add(medlem);
    }
    public void BokaSpel(Spel spel)
    {
        if (bokadeSpel.Contains(spel)) return;
        bokadeSpel.Add(spel);

    }
    public void AvBokaSpel(Spel spel)
    {
        if (!bokadeSpel.Contains(spel)) return;
        bokadeSpel.Remove(spel);

    }

    public override string ToString()
    {
        return beskrivning.ToString();
    }
    public string Detaljer()
    {
        string bokadeSpelString = "\n";
        foreach (Spel spel in bokadeSpel)
        {
            bokadeSpelString += spel.ToString() + '\n';
        }

        return $"Tid: {startDatum.ToString()} - {slutDatum.ToString()}\n" +
               $"Plats: {plats}\n" +
               $"Maxantal: {maxAntal}\n" +
               $"Ansvarig: {ansvarig.ToString()}\n" +
               $"Beskriving: {beskrivning}\n" +
               $"Bokade spel: {bokadeSpelString}";
    }
}

