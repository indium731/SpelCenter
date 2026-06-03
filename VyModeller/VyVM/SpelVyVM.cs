using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb1_OOP.Modeller;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq.Expressions;
using Labb1_OOP.Tjanster;

namespace Labb1_OOP.VyModeller;

public partial class SpelVyVM : ObservableObject
{
    private Navigator _navigator;
    [ObservableProperty]
    private SpelListaVM spelListaLada = new SpelListaVM();
    [ObservableProperty]
    private SpelEntitetVM valdSpel;
    [ObservableProperty]
    private string namn;
    [ObservableProperty]
    private string kategori;
    [ObservableProperty]
    private string minAntal;
    [ObservableProperty]
    private string maxAntal;
    public Svarighetsgrad[] Svarighetsgrader => Enum.GetValues<Svarighetsgrad>();
    [ObservableProperty]
    private Svarighetsgrad valdSvarighetsgrad;
    [ObservableProperty]
    private string beskrivning;
    [ObservableProperty]
    private string sorteringText;
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
    [ObservableProperty]
    private Svarighetsgrad comboValdSvarighetsgrad;
    public Svarighetsgrad[] ComboSvarighetsgrader => Enum.GetValues<Svarighetsgrad>();

    public SpelVyVM(Navigator navigator)
    {
        _navigator = navigator;
    }


    [RelayCommand]
    private void GaTillMeny()
    {
        _navigator.NavigeraTill(new MedlemMenyVyVM(_navigator));
    }

    [RelayCommand]
    private async void TestaLaggTillSpel()
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
        if (ValdSvarighetsgrad == null) return;

        try {

        await SpelListaLada.LaggTillAsync(Namn,
                                                Kategori,
                                                min,
                                                max,
                                                ValdSvarighetsgrad,
                                                Beskrivning);
        } catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return;
        }
    }

    [RelayCommand]
    private async void TaBortValdSpel()
    {
        if (ValdSpel is not SpelEntitetVM valdSpel)
        {
            DetaljText = "Välj ett spel att ta bort";
            return;
        }
        
        await SpelListaLada.TaBortAsync(valdSpel);
        DetaljText = "Inget Spel vald";

    }

    [RelayCommand]
    partial void OnValdSpelChanged(SpelEntitetVM spel)
    {
        DetaljText = valdSpel.Detaljer(); 
    }

    [RelayCommand]
    private async void UppdateraValdSpel()
    {
        try
        {
            int tempInt;
            if (ValdSpel is not SpelEntitetVM valdSpel) return;
            if (!string.IsNullOrWhiteSpace(Namn)) valdSpel.namn = Namn.Trim();
            if (!string.IsNullOrWhiteSpace(Kategori)) valdSpel.kategori= Kategori.Trim();
            if (!int.TryParse(MinAntal, out tempInt)) ;
            else valdSpel.minAntalSpelare = tempInt;
            if (!int.TryParse(MaxAntal, out tempInt)) ;
            else valdSpel.maxAntalSpelare = tempInt;
            if (ValdSvarighetsgrad != null) valdSpel.svarighetsgrad = (Svarighetsgrad)Enum.Parse(typeof(Svarighetsgrad), ValdSvarighetsgrad.ToString());
            SpelListaLada.SparaSpelAsync(valdSpel);
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    [RelayCommand]
    private void AndraSortering()
    {
        SpelListaLada.GaTillNastaMetod();
        SorteringText = SpelListaLada.NuvarandeSortering();
            SokTextVisas = false;
            SokComboVisas = false;
            if (SorteringText == "Namn"
             || SorteringText == "Kategori" 
             || SorteringText == "Minantal spelare"
             || SorteringText == "Maxantal spelare") SokTextVisas = true;
            if (SorteringText == "Svarighetsgrad") SokComboVisas = true;
        
    }
    [RelayCommand]
    private void Sok()
    {
        if (SokTextVisas)
        {
            SpelListaLada.Sok(SokText.Trim());
        }
        if (SokComboVisas)
        {
            SpelListaLada.Sok(ComboValdSvarighetsgrad.ToString());
        }
    }

}
