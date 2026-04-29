namespace Labb1_OOP;

public sealed class SpelLista
{
    private SpelLista()
    {
        spel = new List<Spel>();
        spel.Add(new Spel());
    }

    private static SpelLista _instans;
 
    public static SpelLista HamtaSpelLista()
    {
        if (_instans == null)
        {
            _instans = new SpelLista();
        }
        return _instans;
    }
    public List<Spel> spel;
}