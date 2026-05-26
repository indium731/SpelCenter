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
                MessageBox.Show("StartDatum måste vara före slutdatum");
                throw new ArgumentException();
            }
            if (value < DateTime.Now)
            {
                MessageBox.Show("Bokningen får inte påbörjas tillbaka i tiden");
                throw new ArgumentException();
            }
            field = value;
        }
    }
    public DateTime slutDatum {get => field; set
        {
            if (startDatum > value)
            {
                MessageBox.Show("StartDatum måste vara före slutdatum");
                throw new ArgumentException();
            }
            if (value < DateTime.Now)
            {
                MessageBox.Show("Bokningen får inte påbörjas tillbaka i tiden");
            }
            field = value;
        }
    }
    
    public string plats { get; set; }
    public int maxAntal {get => field; set
        {
            if (value < 0)
            {
                MessageBox.Show("Maxantal måste vara ett positivt tal");
                throw new ArgumentException();
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
        if (bokadeSpel.Contains(spel))
        {
            MessageBox.Show("Spelet är redan bokat");
            return;
        } 
        bokadeSpel.Add(spel);
    }
    public void AvBokaSpel(Spel spel)
    {
        if (!bokadeSpel.Contains(spel))
        {
            MessageBox.Show("Spelet är inte bokat");
            return;
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

