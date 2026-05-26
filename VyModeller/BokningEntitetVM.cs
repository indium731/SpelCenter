using CommunityToolkit.Mvvm.ComponentModel;

namespace Labb1_OOP;

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
    public Bokning TillBokning()
    {
        return _model;
    }

}
