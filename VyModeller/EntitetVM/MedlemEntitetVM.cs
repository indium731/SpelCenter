using CommunityToolkit.Mvvm.ComponentModel;
using Labb1_OOP.Modeller;

namespace Labb1_OOP.VyModeller;

public partial class MedlemEntitetVM : ObservableObject
{
    private Medlem _model;

    public MedlemEntitetVM(Medlem m)
    {
        _model = m;
        medlemSkap = new MedlemSkapEntitetVM(_model.medlemSkap);
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
    public string telefonNummer 
    {
        get => _model.telefonNummer;
        set
        {
            _model.telefonNummer = value;
            OnPropertyChanged();
        }
    }
    public string medlemsNummer 
    {
        get => _model.medlemsNummer;
        set
        {
            _model.medlemsNummer = value;
            OnPropertyChanged();
        }
    }
    public MedlemSkapEntitetVM medlemSkap { get; private set; } 
    public bool admin 
    {
        get => _model.admin;
        set
        {
            _model.admin = value;
            OnPropertyChanged();
        }
    }

    public Medlem TillMedlem()
    {
        return _model;
    }

}
