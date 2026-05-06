
using System.Windows.Controls;

namespace Labb1_OOP;

public class BokningAtkomst : IAtkomst
{

    public UserControl Atkom()
    {
        return new BokningVy();
    }
    public string Namn()
    {
        return "BokningHanterare";
    }
}
