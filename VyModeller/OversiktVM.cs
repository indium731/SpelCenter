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
    public partial class OversiktVM : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<IListBar> oversiktListaLada;
        [ObservableProperty]
        private IListBar valdObjekt;
        [ObservableProperty]
        private string detaljText;
        
        Bokning bokning;
        Navigator _navigator;
        int visadListaIndex = 0;
        public OversiktVM(Bokning b, Navigator n)
        {
            bokning = b;
            _navigator = n;
            InitieraOversiktLista(bokning);
        }


        private void InitieraOversiktLista(Bokning bokning)
        {
            OversiktListaLada = new ObservableCollection<IListBar>(bokning.anmalda);
        }


        [RelayCommand]
        private void GaTillMinaBokningar()
        {
            _navigator.NavigeraTill(new MinaBokningarVM(_navigator));
        }


        [RelayCommand]
        partial void OnValdObjektChanged(IListBar obj)
        {
            DetaljText = ValdObjekt.Detaljer();
        }


        [RelayCommand]
        private void AndraVisadLista()
        {

            if (visadListaIndex == 0)
            {

                ObservableCollection<Spel> rekommenderadeSpel = SpelLista.HamtaSpelLista().RekommenderadeSpel(bokning);
                ObservableCollection<Bokning> overlappandeBokningar = BokningLista.HamtaBokningLista().OverlappandeBokningar(bokning);

                rekommenderadeSpel.ToList().RemoveAll(spel => 
                    overlappandeBokningar.Any(b => b.bokadeSpel.Contains(spel)));

                OversiktListaLada = new ObservableCollection<IListBar>(rekommenderadeSpel);
            }

            else if (visadListaIndex == 1)

            {
                OversiktListaLada = new ObservableCollection<IListBar>(bokning.anmalda);
            }
            visadListaIndex += 1;
            visadListaIndex = visadListaIndex % 2;
            DetaljText = "Inget valt";
        }
    }
}