
using System.ComponentModel.DataAnnotations;

namespace Labb1_OOP;

public sealed class Bokningar
{
    private Bokningar()
    {
        bokningar = new List<Bokning>();
        bokningar.Add(new Bokning());
    }

    private static Bokningar _instans;
 
    public static Bokningar HamtaBokningar()
    {
        if (_instans == null)
        {
            _instans = new Bokningar();
        }
        return _instans;
    }
    public List<Bokning> bokningar;
}
