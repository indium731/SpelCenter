namespace Labb1_OOP;

public sealed class Session
{

    private Session(Medlem medlem)
    {
        inloggadMedlem = medlem;
    }

    private static Session _instans;

    public static Session HamtaSession(Medlem medlem)
    {
        if (_instans == null)
        {
            _instans = new Session(medlem);
        }
        return _instans;
    }
    public static Session HamtaSession()
    {
        if (_instans == null)
        {
            //TODO lägg till fungerande exceptions då detta inte ska gå
            return _instans;
        }
        return _instans;
    }
    public Medlem inloggadMedlem;

    
}
