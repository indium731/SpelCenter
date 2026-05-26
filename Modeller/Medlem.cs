using System.Windows;

namespace Labb1_OOP.Modeller;

public class Medlem : IListBar
{
    public Medlem()
    {
    }
    public Medlem(string n, string t, string m, bool a)
    {
        namn = n;
        telefonNummer = t;
        medlemsNummer = m;
        admin = a;
    }
    public int Id { get; set; }
    public string namn { get; set; }
    public string telefonNummer {get => field; set
        {
            if (!value.All(char.IsDigit))
            {
                MessageBox.Show("Telefonnummer få enbart innehålla siffror");
                throw new ArgumentException();
            }
            field = value;
        }
    }
    public string medlemsNummer { get; set; }
    public MedlemSkap medlemSkap { get; private set; } = new MedlemSkap();
    public bool admin { get; set; }
    public List<Bokning> bokningar { get; set; } = new();
    public List<Bokning> ansvaradeBokningar { get; set; } = new();

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
