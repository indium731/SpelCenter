namespace Labb1_OOP;

public sealed class Installningar
{
    private Installningar()
    {
        forstaBokbaraTid = 8;
        antalBokningTider = 4;
        sistaBokbaraTid = 20;
    }
    private static Installningar _instans;

    public static Installningar HamtaInstallningar()
    {
        if (_instans == null)
        {
            _instans = new Installningar();
            return _instans;
        }
        return _instans;
            
    }

    public int forstaBokbaraTid;
    public int antalBokningTider;
    public int sistaBokbaraTid;
}
