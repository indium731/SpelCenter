using System.ComponentModel.DataAnnotations;

namespace Labb1_OOP;

public sealed class MedlemLista
{
    private MedlemLista()
    {
        _medlemmar = new List<Medlem>();
        metoder = new List<Sorterare<Medlem>>
        {
            new Sorterare<Medlem>(m=>m.namn, "Namn"),
            new Sorterare<Medlem>(m=>m.medlemSkap.medlemStatus, "MedlemStatus"),
            new Sorterare<Medlem>(m=>m.medlemSkap.startDatum, "Startdatum"),
            new Sorterare<Medlem>(m=>m.medlemSkap.slutDatum, "Slutdatum"),
            new Sorterare<Medlem>(m=>m.admin, "Admin"),
        };
        metodIndex = 0;
    }

    private static MedlemLista _instans;
 
    public static MedlemLista HamtaMedlemLista()
    {
        if (_instans == null)
        {
            _instans = new MedlemLista();
        }
        return _instans;
    }
    private List<Medlem> _medlemmar;
    public List<Medlem> medlemmar
    {
        get
        {
            return metoder[metodIndex].Sortera(_medlemmar);
        } set;}
    private List<Sorterare<Medlem>> metoder;
    private int metodIndex;

    public Medlem LaggTill(string n, string t, string m, bool a)
    {
        Medlem medlem = new Medlem(n, t, m, a);
        _medlemmar.Add(medlem);
        return medlem;
    }
    public void TaBort(Medlem medlem)
    {
        _medlemmar.Remove(medlem);
    }
    public void GaTillNastaMetod()
    {
        metodIndex = (metodIndex + 1) % metoder.Count();
    }
    public List<Medlem> Sok(string sokOrd)
    {
        return medlemmar.Where(m => metoder[metodIndex].Matchar(m, sokOrd.ToLower())).ToList();
    }
    public string NuvarandeSortering()
    {
        return metoder[metodIndex].sortering;
    }
}
