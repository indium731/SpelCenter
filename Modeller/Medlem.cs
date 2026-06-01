using System.Windows;

namespace Labb1_OOP.Modeller;

public class Medlem 
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
                throw new ArgumentException("Telefonnummer får enbart innehålla siffror");
            }
            field = value;
        }
    }
    public string medlemsNummer { get; set; }
    public MedlemSkap medlemSkap { get; private set; } = new MedlemSkap();
    public bool admin { get; set; }
    public List<Bokning> bokningar { get; set; } = new();
    public List<Bokning> ansvaradeBokningar { get; set; } = new();

}
