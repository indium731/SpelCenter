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
    private BokningEntitetVM valdBokning;
    [ObservableProperty]
    private BokningListaVM bokningListaLada = new BokningListaVM();
    [ObservableProperty]
    private string detaljText = "Ingen bokning vald.";
    [ObservableProperty]

    private Navigator _navigator;
    public MinaBokningarVyVM(Navigator n)
    {
        _navigator = n;
        bokningListaLada.AnsvaradeBokningar(Session.HamtaSession().inloggadMedlem);
    }

    [RelayCommand]
    private void GaTillMeny()
    {
        _navigator.NavigeraTill(new MedlemMenyVyVM(_navigator));
    }

    [RelayCommand]
    private async void TaBortValdBokning()
    {
        if (ValdBokning is not BokningEntitetVM valdBokning)
        {
            DetaljText = "Välj en bokning att ta bort";
            return;
        }
        
        await BokningListaLada.TaBortAsync(valdBokning);
        DetaljText = "Ingen bokning vald";

    }

    [RelayCommand]
    partial void OnValdBokningChanged(BokningEntitetVM b)
    {
        if (ValdBokning is not BokningEntitetVM valdBokning)
        {
            DetaljText = "Ingen bokning vald";
            return;
        }
        
        DetaljText = valdBokning.Detaljer();
    }

    [RelayCommand]
    private void GaTillOversikt()
    {
        if (ValdBokning is not BokningEntitetVM valdBokning) return;
        _navigator.NavigeraTill(new OversiktVyVM(valdBokning.TillBokning(), _navigator));
    }
    [RelayCommand]
    private void GaTillAndraBokning()
    {
        if (ValdBokning is not BokningEntitetVM valdBokning) return;
        _navigator.NavigeraTill(new AndraBokningVyVM(valdBokning.TillBokning(), _navigator));
    }
}