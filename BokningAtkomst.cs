
using System.Windows.Controls;

namespace Labb1_OOP;

public class BokningAtkomst : IAtkomst
{
    public ContentControl Atkom()
    {
        return new BokningVy();
    }

    public Button Knapp()
    {
        Button knapp = new Button();
        knapp.Content = "BokningHanterare";
        return knapp;
    }
}
