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

public partial class BokningVyVM : ObservableObject
{
    [ObservableProperty]
    private string namn;
    [ObservableProperty]
    private DateTime? startDatum;
    [ObservableProperty]
    private DateTime? slutDatum;
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
    private TimeOnly valdStartTid;

    private Navigator _navigator;

    public BokningVyVM(Navigator n)
    {
        _navigator = n;
        InitieraStartSchemaTider();
        InitieraSlutSchemaTider();
    }

    [RelayCommand]
    public void GaTillMeny()
    {
        _navigator.NavigeraTill(new MedlemMenyVyVM(_navigator));
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


    [RelayCommand]
    private async void ValjSlutTid(TimeOnly slutTid)
    {
        try {

        if (StartDatum == null) return;
        if (SlutDatum == null) return;
        if (Namn == null) return;
        
        if (valdStartTid == null)
        {
            MessageBox.Show("Välj ett startdatum först");
            return;
        }

        DateTime startDatum = ((DateTime)StartDatum).Date + valdStartTid.ToTimeSpan();
        DateTime slutDatum = ((DateTime)SlutDatum).Date + slutTid.ToTimeSpan();


        int antal = 0;
        if (!int.TryParse(MaxAntal.Trim(), out antal)) return;


        Bokning bokning = await BokningLista.HamtaBokningLista().LaggTillAsync(Namn,
                                        startDatum,
                                        slutDatum,
                                        Plats.Trim(),
                                        antal,
                                        Session.HamtaSession().inloggadMedlem,
                                        Beskrivning.Trim());
        

        _navigator.NavigeraTill(new BokaSpelVyVM(bokning, _navigator));
        
        } 
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    [RelayCommand]
    private void ValjStartTid(TimeOnly tid)
    {
        valdStartTid = tid;
    }
}