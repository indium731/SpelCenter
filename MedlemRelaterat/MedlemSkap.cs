namespace Labb1_OOP;

public class MedlemSkap
{
    public MedlemSkap()
    {
        startDatum = DateOnly.FromDateTime(DateTime.Today);
        slutDatum = DateOnly.FromDateTime(DateTime.Today);
    }
    public DateOnly startDatum;
    public DateOnly slutDatum;
    public bool medlemStatus;
    public override string ToString()
    {
        return "Medlem";
    }

}
