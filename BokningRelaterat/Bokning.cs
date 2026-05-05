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
        bokadeSpel = new List<Spel>();
    }

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
        if (this.anmalda.Count >= maxAntal)
        {
            MessageBox.Show("Bokningen är fullbokad");
            return;
        } 
        if (this.anmalda.Contains(medlem))
        {
            MessageBox.Show("Du har redan anmält dig till denna bokning");
            return;
        }
        this.anmalda.Add(medlem);
    }
    public void BokaSpel(Spel spel)
    {
        if (this.bokadeSpel.Contains(spel)) return;
        this.bokadeSpel.Add(spel);

    }
    public void AvBokaSpel(Spel spel)
    {
        if (!this.bokadeSpel.Contains(spel)) return;
        this.bokadeSpel.Remove(spel);

    }

    public override string ToString()
    {
        return this.beskrivning.ToString();
    }
    public string Detaljer()
    {
        string bokadeSpelString = "\n";
        foreach (Spel spel in this.bokadeSpel)
        {
            bokadeSpelString += spel.ToString() + '\n';
        }

        return $"Tid: {this.startDatum.ToString()} - {this.slutDatum.ToString()}\n" +
               $"Plats: {this.plats}\n" +
               $"Maxantal: {this.maxAntal}\n" +
               $"Ansvarig: {this.ansvarig.ToString()}\n" +
               $"Beskriving: {this.beskrivning}\n" +
               $"Bokade spel: {bokadeSpelString}";
    }
}

