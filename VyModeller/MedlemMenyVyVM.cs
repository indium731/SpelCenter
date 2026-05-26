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

public partial class MedlemMenyVyVM : ObservableObject
{
    [ObservableProperty]
    private bool bokningHanterareVisas = true;
    [ObservableProperty]
    private bool anmalHanterareVisas = true;
    [ObservableProperty]
    private bool minaBokningarVisas = true;
    [ObservableProperty]
    private bool spelHanterareVisas = true;
    [ObservableProperty]
    private bool medlemHanterareVisas = true;

    private Navigator _navigator;
    public MedlemMenyVyVM(Navigator n)
    {
        _navigator = n;
        KontrolleraAtkomster();
    }
    [RelayCommand]
    private void GaTillValdVy(Type vmTyp)
    {
            
        
        ObservableObject vm =
            (ObservableObject)Activator.CreateInstance(vmTyp, _navigator);

        _navigator.NavigeraTill(vm);
        
    }
    [RelayCommand]
    private void LoggaUt()
    {
        Session.HamtaSession().inloggadMedlem = null;
        _navigator.NavigeraTill(new InloggVyVM(_navigator));
    }
    private void KontrolleraAtkomster()
    {

        if (!Session.HamtaSession().inloggadMedlem.admin)
        {
            MedlemHanterareVisas = false;
            SpelHanterareVisas = false;
        }
        if (!BokningLista.HamtaBokningLista().bokningar.Any(bokning => bokning.ansvarig.medlemsNummer == Session.HamtaSession().inloggadMedlem.medlemsNummer))
        {
            MinaBokningarVisas = false;
        }
    }
}