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


public partial class InloggVM : ObservableObject
{
    [ObservableProperty]
    private string inlogg;
    private Navigator _navigator;
    public InloggVM(Navigator n)
    {
        _navigator = n;
    }

    [RelayCommand]
    private void TestaInlogg()
    {

        if (string.IsNullOrWhiteSpace(Inlogg))
        {
            return;
        }

        MessageBox.Show(MedlemLista.HamtaMedlemLista().medlemmar.Count().ToString());
        foreach (Medlem medlem in MedlemLista.HamtaMedlemLista().medlemmar)
        {
            MessageBox.Show(medlem.medlemsNummer);
            if (Inlogg == medlem.medlemsNummer)
                {
                    if (!medlem.medlemSkap.medlemStatus)
                {
                    MessageBox.Show("medlemSkap ej aktivt");
                    return;
                }
                    Session.HamtaSession().inloggadMedlem = medlem;
                    _navigator.NavigeraTill(new MedlemMenyVM(_navigator));

                }
        }
    }
}