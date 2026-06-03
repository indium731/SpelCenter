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

namespace Labb1_OOP.VyModeller
{
    public partial class AndraBokningVyVM : ObservableObject
    {
        [ObservableProperty]
        private string nuvarandeBokning;
        [ObservableProperty]
        private string plats;
        [ObservableProperty]
        private string maxAntal;
        [ObservableProperty]
        private string beskrivning;
        [ObservableProperty]
        private ObservableCollection<TimeOnly> tider = new();
        [ObservableProperty]
        private DateTime? valdDatum;
        [ObservableProperty]
        private string? valdTid;
        public List<string> TidVal { get; } = [
            "StartTid",
            "SlutTid"
        ];

        Bokning bokning;
        Navigator _navigator;
        public AndraBokningVyVM(Bokning b, Navigator n)
        {
            NuvarandeBokning = b.ToString();
            bokning = b;
            _navigator = n;
            InitieraSchemaTider();
        }

        [RelayCommand]
        private void GaTillMeny()
        {
            _navigator.NavigeraTill(new MedlemMenyVyVM(_navigator));
        }

        [RelayCommand]
        private void TestaAndraBokning(TimeOnly? tid)
        {
            try{
            MaxAntal ??= "";
            int antal = 0;
            if (!int.TryParse(MaxAntal.Trim(), out antal));

            if (ValdTid == "StartTid" && ValdDatum != null && tid != null)
                {
                    bokning.startDatum = ((DateTime)ValdDatum).Date + ((TimeOnly)tid).ToTimeSpan();
                    MessageBox.Show("startdatum är nu "+bokning.startDatum);
                }
            else if (ValdTid == "SlutTid" && ValdDatum != null && tid != null)
                {
                    bokning.slutDatum = ((DateTime)ValdDatum).Date + ((TimeOnly)tid).ToTimeSpan();
                    MessageBox.Show("slutdatum är nu "+bokning.slutDatum);
                }

                if (!string.IsNullOrWhiteSpace(Plats)) bokning.plats = Plats.Trim();
                if (antal != 0) bokning.maxAntal = antal;
                if (!string.IsNullOrWhiteSpace(Beskrivning)) bokning.beskrivning = Beskrivning.Trim();

                _navigator.NavigeraTill(new BokaSpelVyVM(bokning, _navigator));

            }
            
            catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
        private void InitieraSchemaTider()
        {
            TimeSpan inkrement;

            if (Installningar.antalBokningTider == 1)
            {
                inkrement = TimeSpan.Zero;
            }
            else
            {
                inkrement =
                    (Installningar.sistaBokbaraTid - Installningar.forstaBokbaraTid)
                    / (Installningar.antalBokningTider - 1);
            }

            TimeOnly tid = Installningar.forstaBokbaraTid;

            for (int i = 0; i < Installningar.antalBokningTider; i++)
            {
                Tider.Add(tid);
                tid = tid.Add(inkrement);
            }
        }
        
    }
}
