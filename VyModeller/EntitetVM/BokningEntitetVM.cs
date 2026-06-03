using CommunityToolkit.Mvvm.ComponentModel;
using Labb1_OOP.Modeller;

namespace Labb1_OOP.VyModeller;

public partial class BokningEntitetVM : ObservableObject
{
    private Bokning _model;
    
    public BokningEntitetVM(Bokning b)
    {
        _model = b;
    }
    public string namn
    {
        get => _model.namn;
        set
        {
            _model.namn = value;
            OnPropertyChanged();
        }
    }
    public MedlemEntitetVM ansvarig
    {
        get => new MedlemEntitetVM(_model.ansvarig);
        set
        {
            _model.ansvarig = value.TillMedlem();
            OnPropertyChanged();
        }
    }
    public string beskrivning
    {
        get => _model.beskrivning;
        set
        {
            _model.beskrivning = value;
            OnPropertyChanged();
        }
    }
    public List<MedlemEntitetVM> anmalda
    {
        get => _model.anmalda.Select(m => new MedlemEntitetVM(m)).ToList();
        set
        {
            _model.anmalda = value.Select(m => m.TillMedlem()).ToList();
            OnPropertyChanged();
        }
    }
    public List<SpelEntitetVM> bokadeSpel
    {
        get => _model.bokadeSpel.Select(s => new SpelEntitetVM(s)).ToList();
        set
        {
            _model.bokadeSpel = value.Select(s => s.TillSpel()).ToList();
            OnPropertyChanged();
        }
    }
    public DateTime startDatum
    {
        get => _model.startDatum;
        set
        {
            _model.startDatum = value;
            OnPropertyChanged();
        }
    }
    public DateTime slutDatum 
    {
        get => _model.slutDatum;
        set
        {
            _model.slutDatum = value;
            OnPropertyChanged();
        }
    }
    
    public string plats 
    {
        get => _model.plats;
        set
        {
            _model.plats = value;
            OnPropertyChanged();
        }
    }
    public int maxAntal 
    {
        get => _model.maxAntal;
        set
        {
            _model.maxAntal = value;
            OnPropertyChanged();
        }
    }
    public override string ToString()
    {
        return namn;
    }
    public string Detaljer()
    {
        string bokadeSpelString = "\n";
        foreach (SpelEntitetVM spel in bokadeSpel)
        {
            bokadeSpelString += spel.ToString() + '\n';
        }

        return $"Namn: {namn}\n" +
               $"Tid: {startDatum.ToString()} - {slutDatum.ToString()}\n" +
               $"Plats: {plats}\n" +
               $"Maxantal: {maxAntal}\n" +
               $"Ansvarig: {ansvarig.ToString()}\n" +
               $"Beskriving: {beskrivning}\n" +
               $"Bokade spel: {bokadeSpelString}";
    }
    public string UtokadeDetaljer()
    {
        string bokadeSpelString = "\n";
        foreach (SpelEntitetVM spel in bokadeSpel)
        {
            bokadeSpelString += spel.ToString() + '\n';
        }

        return $"Namn: {namn}\n" +
               $"Tid: {startDatum.ToString()} - {slutDatum.ToString()}\n" +
               $"Plats: {plats}\n" +
               $"Maxantal: {maxAntal}\n" +
               $"Ansvarig: {ansvarig.ToString()}\n" +
               $"Beskriving: {beskrivning}\n" +
               $"Bokade spel: {bokadeSpelString}";
    }
    public Bokning TillBokning()
    {
        return _model;
    }

}
