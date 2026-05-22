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
public partial class MinaBokningarVM : ObservableObject
{
    [ObservableProperty]
    private Bokning valdBokning;
    [ObservableProperty]
    private ObservableCollection<Bokning> bokningar = new ObservableCollection<Bokning>(BokningLista.HamtaBokningLista().bokningar.Where(bokning => bokning.ansvarig.medlemsNummer == Session.HamtaSession().inloggadMedlem.medlemsNummer));
    [ObservableProperty]
    private string detaljText = "Ingen bokning vald.";
    [ObservableProperty]

    private Navigator _navigator;
    public MinaBokningarVM(Navigator n)
    {
        _navigator = n;
    }

    [RelayCommand]
    private void GaTillMeny()
    {
        _navigator.NavigeraTill(new MedlemMenyVM(_navigator));
    }

    [RelayCommand]
    private void TaBortValdBokning()
    {
        if (ValdBokning is not Bokning valdBokning)
        {
            DetaljText = "Välj en bokning att ta bort";
            return;
        }
        
        BokningLista.HamtaBokningLista().TaBort(valdBokning);
        DetaljText = "Ingen bokning vald";

    }

    [RelayCommand]
    private void AndraValdBokning()
    {
        if (ValdBokning is not Bokning valdBokning)
        {
            DetaljText = "Ingen bokning vald";
            return;
        }
        
        DetaljText = valdBokning.Detaljer();
    }

    [RelayCommand]
    private void GaTillOversikt()
    {
        if (ValdBokning is not Bokning valdBokning) return;
        //var mainWin = (MainWindow)Window.GetWindow(this);
        //mainWin.Vy.Content = new OversiktVy(valdBokning);
    }
    [RelayCommand]
    private void GaTillAndraBokning()
    {
        if (ValdBokning is not Bokning valdBokning) return;
        //var mainWin = (MainWindow)Window.GetWindow(this);
        //mainWin.Vy.Content = new AndraBokningVy(valdBokning);
    }

}