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

public partial class SpelVM : ObservableObject
{
    private Navigator _navigator;
    [ObservableProperty]
    private ObservableCollection<Spel> spelListaLada = SpelLista.HamtaSpelLista().spel;
    [ObservableProperty]
    private Spel valdSpel;
    [ObservableProperty]
    private string namn;
    [ObservableProperty]
    private string kategori;
    [ObservableProperty]
    private string minAntal;
    [ObservableProperty]
    private string maxAntal;
    [ObservableProperty]
    private string[] svarighetsgrad = Enum.GetNames(typeof(Svarighetsgrad));
    [ObservableProperty]
    private string valdSvarighetsgrad;
    [ObservableProperty]
    private string beskrivning;
    [ObservableProperty]
    private string sortering;
    [ObservableProperty]
    private string sokText;
    [ObservableProperty]
    private string sokCombo;
    [ObservableProperty]
    private string detaljText;
    [ObservableProperty]
    private bool sokTextVisas;
    [ObservableProperty]
    private bool sokComboVisas;

    public SpelVM(Navigator navigator)
    {
        _navigator = navigator;
    }

    private void UppdateraUI()
    {
        SpelListaLada = null;
        SpelListaLada = SpelLista.HamtaSpelLista().spel;
        SokTextVisas = false;
        SokComboVisas = false;
        if (SpelLista.HamtaSpelLista().NuvarandeSortering() == "Svarighetsgrad") SokComboVisas = true;
        else SokTextVisas = true;
    }

    [RelayCommand]
    private void GaTillMeny()
    {
        _navigator.NavigeraTill(new MedlemMenyVM(_navigator));
    }

    [RelayCommand]
    private void TestaLaggTillSpel()
    {

        if (!int.TryParse(MinAntal, out int min))
        {
            MessageBox.Show("Minimum antal spelare måste vara ett heltal");
            return;
        }
        if (!int.TryParse(MaxAntal, out int max))
        {
            MessageBox.Show("Maximum antal spelare måste vara ett heltal");
            return;
        }
        if (Svarighetsgrad == null) return;

        Svarighetsgrad svarighetsgrad = (Svarighetsgrad)Enum.Parse(typeof(Svarighetsgrad), Svarighetsgrad.ToString());

        try {

        SpelLista.HamtaSpelLista().LaggTill(Namn,
                                                Kategori,
                                                min,
                                                max,
                                                svarighetsgrad,
                                                Beskrivning);
        } catch (ArgumentException ex)
        {
            return;
        }
    }

    [RelayCommand]
    private void TaBortValdSpel()
    {
        if (ValdSpel is not Spel valdSpel)
        {
            DetaljText = "Välj ett spel att ta bort";
            return;
        }
        
        SpelLista.HamtaSpelLista().TaBort(valdSpel);
        DetaljText = "Inget Spel vald";

    }

    [RelayCommand]
    private void AndraValdSpel()
    {
        if (ValdSpel is not Spel valdSpel)
        {
            DetaljText = "Inget spel vald";
            return;
        }
        DetaljText = valdSpel.Detaljer(); 
    }

    [RelayCommand]
    private void UppdateraValdSpel()
    {
        try
        {
            int tempInt;
            if (ValdSpel is not Spel valdSpel) return;
            if (Namn.Trim().Count() != 0) valdSpel.namn = Namn.Trim();
            if (Kategori.Trim().Count() != 0) valdSpel.kategori= Kategori.Trim();
            if (!int.TryParse(MinAntal.Trim(), out tempInt)) ;
            else valdSpel.minAntalSpelare = tempInt;
            if (!int.TryParse(MaxAntal.Trim(), out tempInt)) ;
            else valdSpel.maxAntalSpelare = tempInt;
            if (ValdSvarighetsgrad != null) valdSpel.svarighetsgrad = (Svarighetsgrad)Enum.Parse(typeof(Svarighetsgrad), ValdSvarighetsgrad.ToString());
        }
        catch
        {
        }
    }

    [RelayCommand]
    private void AndraSortering()
    {
        SpelLista.HamtaSpelLista().GaTillNastaMetod();
        Sortering = SpelLista.HamtaSpelLista().NuvarandeSortering();
        
        UppdateraUI();
    }
    [RelayCommand]
    private void Sok()
    {
        if (SokTextVisas)
        {
            SpelListaLada = SpelLista.HamtaSpelLista().Sok(SokText.Trim());
        }
        if (SokComboVisas)
        {
            if (ValdSvarighetsgrad is not string valdSvarighetsgrad)
            {
                return;
            }
            
            SpelListaLada = SpelLista.HamtaSpelLista().Sok(valdSvarighetsgrad);
        }
        
    }

}
