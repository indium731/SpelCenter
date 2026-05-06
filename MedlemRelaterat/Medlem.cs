using System.Windows;

namespace Labb1_OOP;

public class Medlem
{


    public Medlem(string n, string t, string m, bool a)
    {
        if (!t.All(char.IsDigit)){
            MessageBox.Show("Telefonnummer får enbart innehålla siffror");
            throw new ArgumentException();
        }
        namn = n;
        telefonNummer = t;
        medlemsNummer = m;
        medlemSkap = new MedlemSkap{};
        atkomster.Add(new BokningAtkomst());
        atkomster.Add(new AnmalAtkomst());
        if (a)
        {
            atkomster.Add(new MedlemHanterarAtkomst());
            atkomster.Add(new SpelAtkomst());
        }
    }
    public string namn;
    public string telefonNummer;
    public string medlemsNummer;
    public MedlemSkap medlemSkap;
    public List<IAtkomst> atkomster = new List<IAtkomst>();

    public override string ToString()
    {
        return namn;
    }
    public string UtokadeDetaljer()
    {
        string admin;
        admin = atkomster.Count() == 5 ? "admin" : "ej admin";

        return $"Namn: {this.namn}\n" + 
               $"TelefonNummer: {this.telefonNummer}\n" +
               $"medlemsNummer: {this.medlemsNummer}\n" +
               $"Blev medlem: {this.medlemSkap.startDatum}" +
               $"Medlemskap upphör: {this.medlemSkap.slutDatum}" +
               admin;
    }
    public string Detaljer()
    {
        return $"Namn: {this.namn}\n" + 
               $"TelefonNummer: {this.telefonNummer}\n";
    }
}
