using System.ComponentModel.DataAnnotations;

namespace Labb1_OOP;

public sealed class MedlemsLista
{
    private MedlemsLista()
    {
        medlemmar = new List<Medlem>();
    }

    private static MedlemsLista _instans;
 
    public static MedlemsLista HamtaMedlemsLista()
    {
        if (_instans == null)
        {
            _instans = new MedlemsLista();
        }
        return _instans;
    }
    public List<Medlem> medlemmar;

    public void LaggTill(Medlem medlem)
    {
        medlemmar.Add(medlem);
    }
    public void TaBort(Medlem medlem)
    {
        medlemmar.Remove(medlem);
    }
}
