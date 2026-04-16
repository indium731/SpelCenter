using System.ComponentModel.DataAnnotations;

namespace Labb1_OOP;

public sealed class MedlemsLista
{
    private MedlemsLista()
    {
        medlemmar = new List<string> {"Alexander"};
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
    public List<string> medlemmar;
}
