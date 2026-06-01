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
    private BokningListaVM bokningListaLada = new BokningListaVM();
    [ObservableProperty]
    private BokningEntitetVM? valdBokning;
    [ObservableProperty]
    private string detaljText;
    [ObservableProperty]
    private string sorteringText;
    [ObservableProperty]
    private bool sokTextVisas;
    [ObservableProperty]
    private bool sokDatumVisas;
    [ObservableProperty]
    private string sokText;
    [ObservableProperty]
    private DateTime? sokDatum;

    private Navigator navigator;
    public AnmalVyVM(Navigator n)
    {
        SorteringText = bokningListaLada.NuvarandeSortering();
        navigator = n;
        UppdateraUI();
    }
    private void UppdateraUI()
    {
        SokTextVisas = false;
        SokDatumVisas = false;
        if (BokningListaLada.NuvarandeSortering() == "Startdatum") SokDatumVisas = true;
        if (BokningListaLada.NuvarandeSortering() == "Slutdatum") SokDatumVisas = true;
        if (BokningListaLada.NuvarandeSortering() == "Beskrivning") SokTextVisas = true;
        if (BokningListaLada.NuvarandeSortering() == "Ansvarig") SokTextVisas = true;
        if (BokningListaLada.NuvarandeSortering() == "Maxantal") SokTextVisas = true;
        if (BokningListaLada.NuvarandeSortering() == "Plats") SokTextVisas = true;
    }

    [RelayCommand]
    private void GaTillMeny()
    {
        navigator.NavigeraTill(new MedlemMenyVyVM(navigator));
    }

    [RelayCommand]
    private void TestaAnmal()
    {
    if (ValdBokning is not BokningEntitetVM valdBokning)
        {
            return;
        }
        try{
            valdBokning.TillBokning().Anmal(Session.HamtaSession().inloggadMedlem);
            MessageBox.Show("Du är nu anmäld");

        } catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    [RelayCommand]
    partial void OnValdBokningChanged(BokningEntitetVM bokning)
    {
        DetaljText = bokning.Detaljer();
    }
    [RelayCommand]
    private void AndraSortering()
    {
        BokningListaLada.GaTillNastaMetod();
        SorteringText = BokningListaLada.NuvarandeSortering();
        UppdateraUI();
        
    }
    [RelayCommand]
    private void Sok()
    {

        if (SokTextVisas)
        {
            BokningListaLada.Sok(SokText.Trim());
        }
        if (SokDatumVisas)
        {
            SokDatum ??= DateTime.Now;
            string SokDatumText = SokDatum.ToString() ?? DateTime.Now.ToString();
            BokningListaLada.Sok(SokDatumText);
        }
    }
}
