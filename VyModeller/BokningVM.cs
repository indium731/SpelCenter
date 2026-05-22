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

public partial class BokningVM : ObservableObject
{
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

    private ObservableCollection<TimeOnly> StartTider { get; } = new();
    private ObservableCollection<TimeOnly> SlutTider { get; } = new();
    private TimeOnly valdStartTid;

    private Navigator _navigator;

    public BokningVM(Navigator n)
    {
        _navigator = n;
        InitieraStartSchemaTider();
        InitieraSlutSchemaTider();
    }

    public void GaTillMeny()
    {
        _navigator.NavigeraTill(new MedlemMenyVM(_navigator));
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
            StartTider.Add(tid);
            tid = tid.Add(inkrement);
        }
    }


    [RelayCommand]
    private void ValjSlutSchemaTid(TimeOnly slutTid)
    {

        if (StartDatum == null) return;
        if (SlutDatum == null) return;
        
        if (valdStartTid == null)
        {
            MessageBox.Show("Välj ett startdatum först");
            return;
        }

        DateTime startDatum = ((DateTime)StartDatum).Date + valdStartTid.ToTimeSpan();
        DateTime slutDatum = ((DateTime)SlutDatum).Date + slutTid.ToTimeSpan();


        try {
        int antal = 0;
        if (!int.TryParse(MaxAntal.Trim(), out antal)) return;


        Bokning bokning = BokningLista.HamtaBokningLista().LaggTill(startDatum,
                                        slutDatum,
                                        Plats.Trim(),
                                        antal,
                                        Session.HamtaSession().inloggadMedlem,
                                        Beskrivning.Trim());
        

        //var mainWin = (MainWindow)MainWindow.GetWindow(this);
        //mainWin.Vy.Content = new BokaSpelVy(bokning);
        
        } 
        catch (ArgumentException ex)
        {
                return;
        }
    }
    private void ValjStartTid(TimeOnly tid)
    {
        valdStartTid = tid;
    }
}