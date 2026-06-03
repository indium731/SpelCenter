using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb1_OOP.Modeller;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Navigation;
using Labb1_OOP.Tjanster;
using Microsoft.Extensions.DependencyInjection;

namespace Labb1_OOP.VyModeller;


public partial class InloggVyVM : ObservableObject
{
    [ObservableProperty]
    private string inlogg;
    private Navigator _navigator;
    public InloggVyVM(Navigator n)
    {
        _navigator = n;
    }

    [RelayCommand]
    private async Task TestaInlogg()
    {
        try{
        var medlemTjanst = App.tjanstLeverantor.GetRequiredService<MedlemLista>();
        medlemTjanst.TestaInlogg(inlogg);
        _navigator.NavigeraTill(new MedlemMenyVyVM(_navigator));
        }catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }
}