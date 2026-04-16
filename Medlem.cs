namespace Labb1_OOP;

public class Medlem
{
    public string namn;
    public string telefonNummer;
    public string medlemsNummer;
    public MedlemSkap medlemSkap;

    public override string ToString()
    {
        return namn;
    }
}
