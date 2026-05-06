using System.Windows.Controls;

namespace Labb1_OOP;

public class SpelAtkomst : IAtkomst
{

    public UserControl Atkom()
    {
        return new SpelVy();
    }
    public string Namn()
    {
        return "SpelHanterare";
    }
}
