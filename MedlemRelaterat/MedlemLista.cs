using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace Labb1_OOP;

public sealed class MedlemLista
{
    private MedlemLista()
    {
        medlemmar = new List<Medlem>();
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
    public List<Medlem> medlemmar
    {
        get => field;
        private set;
        }
    private List<Sorterare<Medlem>> metoder;
    private int metodIndex;

    public Medlem LaggTill(string n, string t, string m, bool a)
    {
        Medlem nyttMedlem = new Medlem(n, t, m, a);
        medlemmar.Add(nyttMedlem);
        MessageBox.Show("Ny medlem har nu lagts till");
        return nyttMedlem;
    }

    public void TaBort(Medlem medlem)
    {
        medlemmar.Remove(medlem);
        MessageBox.Show("Ny medlem har nu tagits bort");
    }
    public void GaTillNastaMetod()
    {
        metodIndex = (metodIndex + 1) % metoder.Count();
       medlemmar = metoder[metodIndex].Sortera(medlemmar);
    }
    public List<Medlem> Sok(string sokOrd)
    {
        return medlemmar.Where(m => metoder[metodIndex].Matchar(m, sokOrd.ToLower())).ToList();
    }
    public string NuvarandeSortering()
    {
        return metoder[metodIndex].sortering;
    }
    public Medlem Seed(string n, string t, string m, bool a)
    {
        Medlem nyttMedlem = new Medlem(n, t, m, a);
        medlemmar.Add(nyttMedlem);
        return nyttMedlem;
    }
}
