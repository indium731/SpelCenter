using CommunityToolkit.Mvvm.ComponentModel;
using Labb1_OOP.Modeller;

namespace Labb1_OOP.VyModeller;

public partial class SpelEntitetVM : ObservableObject
{
    private Spel _model;

    public SpelEntitetVM(Spel s)
    {
        _model = s;
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
    public string kategori
    {
        get => _model.kategori;
        set
        {
            _model.kategori = value;
            OnPropertyChanged();
        }
    }
    public int minAntalSpelare
    {
        get => _model.minAntalSpelare;
        set
        {
            _model.minAntalSpelare = value;
            OnPropertyChanged();
        }
    }
    public int maxAntalSpelare 
    {
        get => _model.maxAntalSpelare;
        set
        {
            _model.maxAntalSpelare = value;
            OnPropertyChanged();
        }
    }
    public Svarighetsgrad svarighetsgrad 
    {
        get => _model.svarighetsgrad;
        set
        {
            _model.svarighetsgrad = value;
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
    public Spel TillSpel()
    {
        return _model;
    }

}
