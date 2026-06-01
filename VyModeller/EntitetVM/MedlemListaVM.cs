using System.Collections.ObjectModel;
using System.Windows;
using Labb1_OOP.Modeller;

namespace Labb1_OOP.VyModeller;

public class MedlemListaVM
{
    public ObservableCollection<MedlemEntitetVM> medlemmar { get; private set; } = new();
    private List<Sorterare<MedlemEntitetVM>> metoder;
    private int metodIndex;
    //använder denna bool för att veta om medlemmar måste laddas om från databasen
    private bool SokningVisas = false;

    public MedlemListaVM()
    {
        metoder = new List<Sorterare<MedlemEntitetVM>>
        {
            new Sorterare<MedlemEntitetVM>(m=>m.namn, "Namn"),
            new Sorterare<MedlemEntitetVM>(m=>m.medlemSkap.medlemStatus, "MedlemStatus"),
            new Sorterare<MedlemEntitetVM>(m=>m.medlemSkap.startDatum, "Startdatum"),
            new Sorterare<MedlemEntitetVM>(m=>m.medlemSkap.slutDatum, "Slutdatum"),
            new Sorterare<MedlemEntitetVM>(m=>m.admin, "Admin"),
        };
        metodIndex = 0;
        UppdateraMedlemmar();
    }

    private void UppdateraMedlemmar()
    {
        medlemmar.Clear();
        MedlemLista.HamtaMedlemLista().medlemmar.ForEach(m => medlemmar.Add(new MedlemEntitetVM(m)));
    }
    public async Task LaggTillAsync(string n, string t, string m, bool a)
    {
        Medlem medlem = await MedlemLista.HamtaMedlemLista().LaggTillAsync(n, t, m, a);
        medlemmar.Add(new MedlemEntitetVM(medlem));
        if (SokningVisas)UppdateraMedlemmar();
    }

    public async Task TaBortAsync(MedlemEntitetVM medlem)
    {
        await MedlemLista.HamtaMedlemLista().TaBortAsync(medlem.TillMedlem());
        medlemmar.Remove(medlem);
        if (SokningVisas)UppdateraMedlemmar();
    }
    public async Task SparaMedlemAsync(MedlemEntitetVM medlem)
    {
        await MedlemLista.HamtaMedlemLista().SparaMedlemAsync(medlem.TillMedlem());
    }
    public void GaTillNastaMetod()
    {
        metodIndex = (metodIndex + 1) % metoder.Count();
        if (SokningVisas)UppdateraMedlemmar();
    }
    public void Sok(string sokOrd)
    {
        //Gjort sökfunktionen på detta sätt för att kunna 
        // bibehålla samma pekare för den observablecollection som finns så den uppdateras korrekt
        SokningVisas = true;
        List<MedlemEntitetVM> ickeMatchningar = medlemmar.Where(m => !metoder[metodIndex].Matchar(m, sokOrd.ToLower())).ToList();
        ickeMatchningar.ForEach(m => medlemmar.Remove(m));
    }
    public string NuvarandeSortering()
    {
        return metoder[metodIndex].sortering;
    }
}
