using System.Windows;

namespace Labb1_OOP;

public class Bokning : IListBar
{


    public Bokning(DateTime d, DateTime s, string p, int m, Medlem a, string b)
    {

        slutDatum = s;
        startDatum = d;
        plats  = p;
        maxAntal = m;
        ansvarig = a;
        beskrivning = b;
        anmalda = new List<Medlem>();
        bokadeSpel = new List<Spel>();
    }
    private DateTime _startDatum;
    public DateTime startDatum {get => _startDatum; set
        {
            if (value > _slutDatum)
            {
                MessageBox.Show("StartDatum måste vara före slutdatum");
                throw new ArgumentException();
            }
            if (value < DateTime.Now)
            {
                MessageBox.Show("Bokningen får inte påbörjas tillbaka i tiden");
            }
            _startDatum = value;
        }
    }
    private DateTime _slutDatum;
    public DateTime slutDatum {get => _slutDatum; set
        {
            if (_startDatum > value)
            {
                MessageBox.Show("StartDatum måste vara före slutdatum");
                throw new ArgumentException();
            }
            if (value < DateTime.Now)
            {
                MessageBox.Show("Bokningen får inte påbörjas tillbaka i tiden");
            }
            _slutDatum = value;
        }
    }
    public string plats;
    private int _maxAntal;
    public int maxAntal {get => _maxAntal; set
        {
            if (value < 0)
            {
                MessageBox.Show("Maxantal måste vara ett positivt tal");
                throw new ArgumentException();
            }
            _maxAntal = value;
        }
    }
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
    public string UtokadeDetaljer()
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

