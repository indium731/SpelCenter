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

public partial class MedlemMenyVM : ObservableObject
{
    private Navigator _navigator;
    public MedlemMenyVM(Navigator n)
    {
        _navigator = n;
    }

    [RelayCommand]
    private void LoggaUt()
    {
        _navigator.NavigeraTill(new InloggVM(_navigator));
    }
}
