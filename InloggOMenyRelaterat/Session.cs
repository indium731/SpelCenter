namespace Labb1_OOP;

public sealed class Session
{


    private static Session _instans;

    public static Session HamtaSession()
    {
        if (_instans == null)
        {
            _instans = new Session();
            return _instans;
        }
        return _instans;
    }
    public Medlem inloggadMedlem;

    
}
