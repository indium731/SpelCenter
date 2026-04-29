namespace Labb1_OOP;

public class MedlemSkap
{
    public MedlemSkap()
    {
        startDatum = DateOnly.MinValue;
        slutDatum = DateOnly.MaxValue;
    }
    public DateOnly startDatum;
    public DateOnly slutDatum;
    public bool medlemStatus;
    public override string ToString()
    {
        return "Medlem";
    }

}
