using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb1_OOP.Modeller;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Labb1_OOP.Tjanster;
using Microsoft.Extensions.DependencyInjection;


namespace Labb1_OOP.VyModeller
{
    public partial class OversiktVyVM : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<MedlemEntitetVM> medlemListaLada;
        [ObservableProperty]
        private ObservableCollection<SpelEntitetVM> spelListaLada;
        [ObservableProperty]
        private SpelEntitetVM valdSpel;
        [ObservableProperty]
        private MedlemEntitetVM valdMedlem;
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
            MedlemListaLada = new ObservableCollection<MedlemEntitetVM>(bokning.anmalda.Select(m => new MedlemEntitetVM(m)));
            SpelListaLada = new ObservableCollection<SpelEntitetVM>(App.tjanstLeverantor.GetRequiredService<SpelLista>().RekommenderadeSpel(bokning).Select(s => new SpelEntitetVM(s)));
        }


        [RelayCommand]
        private void GaTillMinaBokningar()
        {
            _navigator.NavigeraTill(new MinaBokningarVyVM(_navigator));
        }


        [RelayCommand]
        partial void OnValdSpelChanged(SpelEntitetVM spel)
        {
            DetaljText = spel.Detaljer();
        }

        [RelayCommand]
        partial void OnValdMedlemChanged(MedlemEntitetVM medlem)
        {
            DetaljText = medlem.Detaljer();
        }


        [RelayCommand]
        private void AndraVisadLista()
        {
            MedlemListaVisas = !MedlemListaVisas;
            SpelListaVisas = !SpelListaVisas;

            DetaljText = "";
        }
    }
}