using CommunityToolkit.Mvvm.ComponentModel;
using Labb1_OOP.Modeller;

namespace Labb1_OOP.VyModeller;

public partial class MedlemSkapEntitetVM : ObservableObject
{
    private MedlemSkap _model;

    public MedlemSkapEntitetVM(MedlemSkap m)
    {
        _model = m;
    }

    public DateOnly startDatum 
    {
        get => _model.startDatum;
        set
        {
            _model.startDatum = value;
            OnPropertyChanged();
        }
    }
    public DateOnly slutDatum 
    {
        get => _model.slutDatum;
        set
        {
            _model.slutDatum = value;
            OnPropertyChanged();
        }
    }
    public bool medlemStatus {get => _model.medlemStatus; private set;}

    public string ToString()
    {
        return _model.ToString();
    }
    public MedlemSkap TillMedlemSkap()
    {
        return _model;
        
    }
}
