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
public partial class MinaBokningarVyVM : ObservableObject
{
    [ObservableProperty]
    private Bokning valdBokning;
    [ObservableProperty]
    private ObservableCollection<Bokning> bokningar = new ObservableCollection<Bokning>(BokningLista.HamtaBokningLista().bokningar.Where(b => b.ansvarig.Id == Session.HamtaSession().inloggadMedlem.Id));
    [ObservableProperty]
    private string detaljText = "Ingen bokning vald.";
    [ObservableProperty]

    private Navigator _navigator;
    public MinaBokningarVyVM(Navigator n)
    {
        _navigator = n;
    }

    [RelayCommand]
    private void GaTillMeny()
    {
        _navigator.NavigeraTill(new MedlemMenyVyVM(_navigator));
    }

    [RelayCommand]
    private void TaBortValdBokning()
    {
        if (ValdBokning is not Bokning valdBokning)
        {
            DetaljText = "Välj en bokning att ta bort";
            return;
        }
        
        BokningLista.HamtaBokningLista().TaBortAsync(valdBokning);
        DetaljText = "Ingen bokning vald";

    }

    [RelayCommand]
    partial void OnValdBokningChanged(Bokning b)
    {
        if (ValdBokning is not Bokning valdBokning)
        {
            DetaljText = "Ingen bokning vald";
            return;
        }
        
        DetaljText = new BokningEntitetVM(valdBokning).Detaljer();
    }

    [RelayCommand]
    private void GaTillOversikt()
    {
        if (ValdBokning is not Bokning valdBokning) return;
        _navigator.NavigeraTill(new OversiktVyVM(valdBokning, _navigator));
    }
    [RelayCommand]
    private void GaTillAndraBokning()
    {
        if (ValdBokning is not Bokning valdBokning) return;
        _navigator.NavigeraTill(new AndraBokningVyVM(valdBokning, _navigator));
    }
}