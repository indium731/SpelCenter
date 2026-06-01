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
    public partial class OversiktVyVM : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Medlem> medlemListaLada;
        [ObservableProperty]
        private ObservableCollection<Spel> spelListaLada;
        [ObservableProperty]
        private Spel valdSpel;
        [ObservableProperty]
        private Medlem valdMedlem;
        [ObservableProperty]
        private string detaljText;
        [ObservableProperty]
        private bool medlemListaVisas = true;
        [ObservableProperty]
        private bool spelListaVisas = false;
        
        Bokning bokning;
        Navigator _navigator;
        public OversiktVyVM(Bokning b, Navigator n)
        {
            bokning = b;
            _navigator = n;
            InitieraOversiktLista(bokning);
        }


        private void InitieraOversiktLista(Bokning bokning)
        {
            MedlemListaLada = new ObservableCollection<Medlem>(bokning.anmalda);
            SpelListaLada = new ObservableCollection<Spel>(SpelLista.HamtaSpelLista().RekommenderadeSpel(bokning));
        }


        [RelayCommand]
        private void GaTillMinaBokningar()
        {
            _navigator.NavigeraTill(new MinaBokningarVyVM(_navigator));
        }


        [RelayCommand]
        partial void OnValdSpelChanged(Spel spel)
        {
            DetaljText = new SpelEntitetVM(ValdSpel).Detaljer();
        }

        [RelayCommand]
        partial void OnValdMedlemChanged(Medlem medlem)
        {
            DetaljText = new MedlemEntitetVM(ValdMedlem).Detaljer();
        }


        [RelayCommand]
        private void AndraVisadLista()
        {

            MedlemListaVisas = !MedlemListaVisas;
            SpelListaVisas = !SpelListaVisas;
            
            DetaljText = "Inget valt";
        }
    }
}