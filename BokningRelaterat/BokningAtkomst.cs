
using System.Windows.Controls;

namespace Labb1_OOP;

public class BokningAtkomst : IAtkomst
{

    public Button Knapp()
    {
        Button knapp = new Button();
        knapp.Content = "BokningHanterare";
        knapp.Tag = new BokningVy();
        return knapp;
    }
}
