using System.Windows;

namespace Labb1_OOP;

public class Medlem
{


    public Medlem(string n, string t, string m, bool a)
    {
        namn = n;
        telefonNummer = t;
        medlemsNummer = m;
        medlemSkap = new MedlemSkap();
        admin = a;
    }
    public string namn;
    private string _telefonNummer;
    public string telefonNummer {get => _telefonNummer; set
        {
            if (!value.All(char.IsDigit))
            {
                MessageBox.Show("Telefonnummer få enbart innehålla siffror");
                throw new ArgumentException();
            }
            _telefonNummer = value;
        }
    }
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
