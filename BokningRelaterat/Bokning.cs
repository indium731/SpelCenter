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
    public string plats;
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
        MessageBox.Show("Du är nu anmäld");
    }
    public void BokaSpel(Spel spel)
    {
        if (bokadeSpel.Contains(spel)) return;
        bokadeSpel.Add(spel);
        MessageBox.Show("Spel är nu bokat");

    }
    public void AvBokaSpel(Spel spel)
    {
        if (!bokadeSpel.Contains(spel)) return;
        bokadeSpel.Remove(spel);
        MessageBox.Show("Spel är nu avbokat");

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
    public void SeedAnmal(Medlem medlem)
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
    public void SeedBokaSpel(Spel spel)
    {
        if (bokadeSpel.Contains(spel)) return;
        bokadeSpel.Add(spel);

    }
}

