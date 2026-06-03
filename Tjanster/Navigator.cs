using CommunityToolkit.Mvvm.ComponentModel;

namespace Labb1_OOP.Tjanster;

public partial class Navigator : ObservableObject
{
    [ObservableProperty]
    private ObservableObject vy;

    public Navigator()
    {
    }
    public void NavigeraTill(ObservableObject nyVy)
    {
        Vy = nyVy;
    }

}
