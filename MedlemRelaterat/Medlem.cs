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
        medlemSkap = new MedlemSkap();
        admin = a;
    }
    public string namn;
    public string telefonNummer;
    public string medlemsNummer;
    public MedlemSkap medlemSkap;
    public bool admin;

    public override string ToString()
    {
        return namn;
    }
    public string UtokadeDetaljer()
    {

        string adminString = admin? "Admin" : "Ej admin";
        return $"Namn: {namn}\n" + 
               $"TelefonNummer: {telefonNummer}\n" +
               $"medlemsNummer: {medlemsNummer}\n" +
               $"Blev medlem: {medlemSkap.startDatum}\n" +
               $"Medlemskap upphör: {medlemSkap.slutDatum}\n" +
                 adminString;
    }
    public string Detaljer()
    {
        return $"Namn: {namn}\n" + 
               $"TelefonNummer: {telefonNummer}\n";
    }
}
