namespace Labb1_OOP;
using Labb1_OOP.Modeller;

public sealed class Session
{
    private static Session _instans;

    public static Session HamtaSession()
    {
        if (_instans == null)
        {
            _instans = new Session();
        }
        return _instans;
    }
    public Medlem inloggadMedlem;

    
}
