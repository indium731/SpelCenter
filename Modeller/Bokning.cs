using System.Windows;
using Labb1_OOP.Modeller;

namespace Labb1_OOP;

public class Bokning : IListBar
{


    public Bokning()
    {
    }
    public Bokning(string n, DateTime d, DateTime s, string p, int m, Medlem a, string b)
    {
        namn = n;
        slutDatum = s;
        startDatum = d;
        plats  = p;
        maxAntal = m;
        ansvarig = a;
        beskrivning = b;
    }
    public int Id { get; set; }
    public DateTime startDatum {get => field; set
        {
            if (value > slutDatum)
            {
                throw new ArgumentException("StartDatum måste vara före slutdatum");
            }
            if (value < DateTime.Now)
            {
                throw new ArgumentException("Bokningen får inte påbörjas tillbaka i tiden");
            }
            field = value;
        }
    }
    public DateTime slutDatum {get => field; set
        {
            if (startDatum > value)
            {
                throw new ArgumentException("StartDatum måste vara före slutdatum");
            }
            if (value < DateTime.Now)
            {
                throw new ArgumentException("Bokningen får inte påbörjas tillbaka i tiden");
            }
            field = value;
        }
    }
    
    public string plats { get; set; }
    public int maxAntal {get => field; set
        {
            if (value < 0)
            {
                throw new ArgumentException("Maxantal måste vara ett positivt tal");
            }
            field = value;
        }
    }
    public string namn { get; set; }
    public Medlem ansvarig { get; set; }
    public string beskrivning { get; set; }
    public List<Medlem> anmalda { get; set; } = new List<Medlem>();
    public List<Spel> bokadeSpel { get; set; } = new List<Spel>();

    public void Anmal(Medlem medlem)
    {
        if (anmalda.Count >= maxAntal)
        {
            throw new Exception("Bokningen är fullbokad");
        } 
        if (anmalda.Contains(medlem))
        {
            anmalda.Remove(medlem);
            throw new Exception("Du är nu frånanmäld från denna bokning");
        }
        anmalda.Add(medlem);
    }
    public void BokaSpel(Spel spel)
    {
        if (bokadeSpel.Contains(spel))
        {
            throw new Exception("Spelet är redan bokat");
        } 
        bokadeSpel.Add(spel);
    }
    public void AvBokaSpel(Spel spel)
    {
        if (!bokadeSpel.Contains(spel))
        {
            throw new Exception("Spelet är inte bokat");
        }
        bokadeSpel.Remove(spel);

    }

    public override string ToString()
    {
        return namn;
    }
    public string Detaljer()
    {
        string bokadeSpelString = "\n";
        foreach (Spel spel in bokadeSpel)
        {
            bokadeSpelString += spel.ToString() + '\n';
        }

        return $"Namn: {namn}\n" +
               $"Tid: {startDatum.ToString()} - {slutDatum.ToString()}\n" +
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

        return $"Namn: {namn}\n" +
               $"Tid: {startDatum.ToString()} - {slutDatum.ToString()}\n" +
               $"Plats: {plats}\n" +
               $"Maxantal: {maxAntal}\n" +
               $"Ansvarig: {ansvarig.ToString()}\n" +
               $"Beskriving: {beskrivning}\n" +
               $"Bokade spel: {bokadeSpelString}";
    }
}

