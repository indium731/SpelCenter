using System.Windows.Controls;

namespace Labb1_OOP;

public class AnmalAtkomst : IAtkomst
{

    public UserControl Atkom()
    {
        return new AnmalVy();
    }
    public string Namn()
    {
        return "Anmäl dig";
    }
}