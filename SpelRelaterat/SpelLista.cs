namespace Labb1_OOP;

public sealed class SpelLista
{
    private SpelLista()
    {
        spel = new List<Spel>();
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

    public void LaggTill(Spel nyttSpel)
    {
        spel.Add(nyttSpel);
    }
    public void TaBort(Spel nyttSpel)
    {
        spel.Remove(nyttSpel);
    }
}