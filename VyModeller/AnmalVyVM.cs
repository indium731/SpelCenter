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

public partial class AnmalVyVM : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<BokningEntitetVM> bokningListaLada = new();
    [ObservableProperty]
    private BokningEntitetVM? valdBokning;
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

    private void LaddaBokningar()
    {
        var bokningLista = BokningLista.HamtaBokningLista().bokningar;
        BokningListaLada.Clear();
        foreach (Bokning bokning in bokningLista)
        {
            BokningListaLada.Add(new BokningEntitetVM(bokning));
        }
    }
    private Navigator navigator;
    public AnmalVyVM(Navigator n)
    {
        navigator = n;
        UppdateraUI();
        LaddaBokningar();
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
        navigator.NavigeraTill(new MedlemMenyVyVM(navigator));
    }

    [RelayCommand]
    private void TestaAnmalKlick()
    {
    if (ValdBokning is not BokningEntitetVM valdBokning)
        {
            return;
        }
        valdBokning.TillBokning().Anmal(Session.HamtaSession().inloggadMedlem);
    }

    [RelayCommand]
    partial void OnValdBokningChanged(BokningEntitetVM bokning)
    {
        DetaljText = bokning.TillBokning().Detaljer();
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
            var bokningLista = BokningLista.HamtaBokningLista().Sok(SokText.Trim());
            BokningListaLada.Clear();
            foreach (Bokning bokning in bokningLista)
            {
                BokningListaLada.Add(new BokningEntitetVM(bokning));
            }
        }
        if (SokDatumVisas)
        {
            var datum = SokDatum ?? DateTime.Now;
            var bokningLista = BokningLista.HamtaBokningLista().Sok(datum.ToShortDateString());
            BokningListaLada.Clear();
            foreach (Bokning bokning in bokningLista)
            {
                BokningListaLada.Add(new BokningEntitetVM(bokning));
            }
        }
    }
}
