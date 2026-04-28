namespace Labb1_OOP;

public class Medlem
{
    public Medlem()
    {
        namn = "Alfred";
        telefonNummer = "0707715633";
        medlemsNummer = "S2507580";
        medlemSkap = new MedlemSkap{};
        atkomster.Add(new MedlemHanterarAtkomst());
        atkomster.Add(new SpelAtkomst());
        atkomster.Add(new BokningAtkomst());
        atkomster.Add(new AnmalAtkomst());
        atkomster.Add(new MinaBokningarAtkomst());
    }

    public Medlem(string n, string t, string m, bool? a)
    {
        namn = n;
        telefonNummer = t;
        medlemsNummer = m;
        medlemSkap = new MedlemSkap{};
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
}
