using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb1_OOP.Modeller;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Labb1_OOP.VyModeller
{
    public partial class BokaSpelVM : ObservableObject
    {
        [ObservableProperty]
        private string detaljText = "Ingen spel vald.";
        [ObservableProperty]
        private Spel valdSpel;
        [ObservableProperty]
        private ObservableCollection<Spel> spelListaLada;
        private Bokning bokning;
        private Navigator _navigator;
        public BokaSpelVM(Bokning b, Navigator n)
        {
            bokning = b;
            _navigator = n;
            InitieraLista();
        }

        private void InitieraLista()
        {
            ObservableCollection<Bokning> overlappandeBokningar = BokningLista.HamtaBokningLista().OverlappandeBokningar(bokning);

            ObservableCollection<Spel> tillgangligaSpel = new ObservableCollection<Spel>(SpelLista.HamtaSpelLista().spel
                .Where(spel => !overlappandeBokningar.Any(b => b.bokadeSpel.Contains(spel))));
                

            SpelListaLada = tillgangligaSpel;
        }


        [RelayCommand]
        private void GaTillMeny()
        {
            _navigator.NavigeraTill(new MedlemMenyVM(_navigator));
        }

        [RelayCommand]
        private void TestaBokaSpel()
        {
            if (ValdSpel is not Spel spel) return;
            BokningLista.HamtaBokningLista().BokaSpel(bokning, spel);

        }

        [RelayCommand]
        private void AvbokaValdSpel()
        {
            if (ValdSpel is not Spel spel) return;
            BokningLista.HamtaBokningLista().AvbokaSpel(bokning, spel);

        }


    }
    
}

