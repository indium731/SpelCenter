using System.ComponentModel.DataAnnotations;

namespace Labb1_OOP.Modeller;

public class MedlemSkap
{
    public MedlemSkap()
    {
        startDatum = DateOnly.FromDateTime(DateTime.Today);
        slutDatum = DateOnly.FromDateTime(DateTime.Today).AddYears(1);
    }
    public int Id { get; set; }

    public DateOnly startDatum { get; set; }
    public DateOnly slutDatum { get; set; }
    public bool medlemStatus {
        get
        {
            if (DateOnly.FromDateTime(DateTime.Today) < slutDatum) return true;
            return false;
        } private set;}
    public override string ToString()
    {
        return "Medlem";
    }

}
