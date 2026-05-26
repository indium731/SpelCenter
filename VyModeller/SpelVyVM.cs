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

namespace Labb1_OOP.VyModeller;

public partial class SpelVyVM : ObservableObject
{
    private Navigator _navigator;
    [ObservableProperty]
    private ObservableCollection<SpelEntitetVM> spelListaLada = new();
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

    public SpelVyVM(Navigator navigator)
    {
        _navigator = navigator;
        LaddaSpel();
    }

    private void LaddaSpel()
    {
        var spelLista = SpelLista.HamtaSpelLista().spel;
        SpelListaLada.Clear();
        foreach(Spel spel in spelLista)
        {
            SpelListaLada.Add(new SpelEntitetVM(spel));
        }
    }

    [RelayCommand]
    private void GaTillMeny()
    {
        _navigator.NavigeraTill(new MedlemMenyVyVM(_navigator));
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

        Spel spel = SpelLista.HamtaSpelLista().LaggTill(Namn,
                                                Kategori,
                                                min,
                                                max,
                                                svarighetsgrad,
                                                Beskrivning);
        SpelListaLada.Add(new SpelEntitetVM(spel));
        } catch (ArgumentException ex)
        {
            return;
        }
    }

    [RelayCommand]
    private void TaBortValdSpel()
    {
        if (ValdSpel is not SpelEntitetVM valdSpel)
        {
            DetaljText = "Välj ett spel att ta bort";
            return;
        }
        
        SpelLista.HamtaSpelLista().TaBort(valdSpel.TillSpel());
        DetaljText = "Inget Spel vald";
        SpelListaLada.Remove(valdSpel);

    }

    [RelayCommand]
    private void AndraValdSpel()
    {
        if (ValdSpel is not SpelEntitetVM valdSpel)
        {
            DetaljText = "Inget spel vald";
            return;
        }
        DetaljText = valdSpel.TillSpel().Detaljer(); 
    }

    [RelayCommand]
    private void UppdateraValdSpel()
    {
        try
        {
            int tempInt;
            if (ValdSpel is not SpelEntitetVM valdSpel) return;
            if (Namn.Trim().Count() != 0) valdSpel.namn = Namn.Trim();
            if (Kategori.Trim().Count() != 0) valdSpel.kategori= Kategori.Trim();
            if (!int.TryParse(MinAntal.Trim(), out tempInt)) ;
            else valdSpel.minAntalSpelare = tempInt;
            if (!int.TryParse(MaxAntal.Trim(), out tempInt)) ;
            else valdSpel.maxAntalSpelare = tempInt;
            if (ValdSvarighetsgrad != null) valdSpel.svarighetsgrad = (Svarighetsgrad)Enum.Parse(typeof(Svarighetsgrad), ValdSvarighetsgrad.ToString());
            SpelLista.HamtaSpelLista().SparaSpel(valdSpel.TillSpel());
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
        
    }
    [RelayCommand]
    private void Sok()
    {
        if (SokTextVisas)
        {
            var spelLista = SpelLista.HamtaSpelLista().Sok(SokText.Trim());
            SpelListaLada.Clear();
            foreach (Spel spel in spelLista)
            {
                SpelListaLada.Add(new SpelEntitetVM(spel));
            }
        }
        if (SokComboVisas)
        {
            if (ValdSvarighetsgrad is not string valdSvarighetsgrad)
            {
                return;
            }
            var spelLista = SpelLista.HamtaSpelLista().Sok(valdSvarighetsgrad);
            SpelListaLada.Clear();
            foreach(Spel spel in spelLista)
            {
                SpelListaLada.Add(new SpelEntitetVM(spel));
            }
        }
        
    }

}
