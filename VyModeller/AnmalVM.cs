using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb1_OOP.Modeller;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Labb1_OOP.VyModeller;

public partial class AnmalVM : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Bokning> bokningListaLada = BokningLista.HamtaBokningLista().bokningar;
    [ObservableProperty]
    private Bokning? valdBokning;
    [ObservableProperty]
    private string detaljText;
    [ObservableProperty]
    private string sorteringText = BokningLista.HamtaBokningLista().NuvarandeSortering();
    [ObservableProperty]
    private bool sokTextVisas;
    [ObservableProperty]
    private bool sokDatumVisas;
    [ObservableProperty]
    private string sokText;
    [ObservableProperty]
    private DateTime? sokDatum;

    private void InitieraBokningar()
    {
        BokningListaLada = BokningLista.HamtaBokningLista().bokningar;
    }
    private Navigator navigator;
    public AnmalVM(Navigator n)
    {
        navigator = n;
        UppdateraUI();
    }
    private void UppdateraUI()
    {
        SokTextVisas = false;
        SokDatumVisas = false;
        if (BokningLista.HamtaBokningLista().NuvarandeSortering() == "Startdatum") SokDatumVisas = true;
        if (BokningLista.HamtaBokningLista().NuvarandeSortering() == "Slutdatum") SokDatumVisas = true;
        if (BokningLista.HamtaBokningLista().NuvarandeSortering() == "Beskrivning") SokTextVisas = true;
        if (BokningLista.HamtaBokningLista().NuvarandeSortering() == "Ansvarig") SokTextVisas = true;
        if (BokningLista.HamtaBokningLista().NuvarandeSortering() == "Maxantal") SokTextVisas = true;
        if (BokningLista.HamtaBokningLista().NuvarandeSortering() == "Plats") SokTextVisas = true;
    }

    [RelayCommand]
    private void GaTillMeny()
    {
        navigator.NavigeraTill(new MedlemMenyVM(navigator));
    }

    [RelayCommand]
    private void TestaAnmalKlick()
    {
    if (ValdBokning is not Bokning valdBokning)
        {
            return;
        }
        valdBokning.Anmal(Session.HamtaSession().inloggadMedlem);
    }

    private void AndraValdBokning()
    {
        if (ValdBokning is not Bokning valdBokning)
        {
            DetaljText = "Ingen Bokning vald";
            return;
        }
        DetaljText = valdBokning.Detaljer();
    }
    [RelayCommand]
    private void AndraSorteringKlick()
    {
        BokningLista.HamtaBokningLista().GaTillNastaMetod();
        SorteringText = BokningLista.HamtaBokningLista().NuvarandeSortering();
        UppdateraUI();
        
    }
    [RelayCommand]
    private void SokKlick()
    {

        if (SokTextVisas)
        {
            BokningListaLada = BokningLista.HamtaBokningLista().Sok(SokText.Trim());
        }
        if (SokDatumVisas)
        {
            var datum = SokDatum ?? DateTime.Now;
            BokningListaLada = BokningLista.HamtaBokningLista().Sok(datum.ToShortDateString());
        }
    }
}
