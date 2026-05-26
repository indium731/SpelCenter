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
        private ObservableCollection<TimeOnly> startTider = new();
        [ObservableProperty]
        private ObservableCollection<TimeOnly> slutTider = new();
        [ObservableProperty]
        private DateTime startDatum;
        [ObservableProperty]
        private DateTime slutDatum;

        Bokning bokning;
        Navigator _navigator;
        public AndraBokningVyVM(Bokning b, Navigator n)
        {
            NuvarandeBokning = b.ToString();
            bokning = b;
            _navigator = n;
            InitieraSlutSchemaTider();
            InitieraStartSchemaTider();
        }

        [RelayCommand]
        private void GaTillMeny()
        {
            _navigator.NavigeraTill(new MedlemMenyVyVM(_navigator));
        }

        [RelayCommand]
        private void TestaAndraBokning(TimeOnly tid)
        {
            int antal = 0;
            if (!int.TryParse(MaxAntal.Trim(), out antal)) return;

            DateTime? startDatumTid = null;
            DateTime? slutDatumTid = null;

            if (StartDatum is DateTime startDatum)
            {
            startDatumTid = startDatum.Date + tid.ToTimeSpan();
            }
            if (SlutDatum is DateTime slutDatum)
            {
            slutDatumTid = slutDatum.Date + tid.ToTimeSpan();
            }
            try
            {
                if (startDatumTid != null) bokning.startDatum = (DateTime)startDatumTid;
                if (slutDatumTid != null) bokning.slutDatum = (DateTime)slutDatumTid;
                if (Plats.Trim() != "") bokning.plats = Plats.Trim();
                if (antal != 0) bokning.maxAntal = antal;
                if (Beskrivning.Trim() != "") bokning.beskrivning = Beskrivning.Trim();

                _navigator.NavigeraTill(new BokaSpelVyVM(bokning, _navigator));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    private void InitieraStartSchemaTider()
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
            StartTider.Add(tid);
            tid = tid.Add(inkrement);
        }
    }
    private void InitieraSlutSchemaTider()
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
            SlutTider.Add(tid);
            tid = tid.Add(inkrement);
        }
    }

    }
}
