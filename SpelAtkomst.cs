using System.Windows.Controls;

namespace Labb1_OOP;

public class SpelAtkomst : IAtkomst
{
    public ContentControl Atkom()
    {
        return new SpelVy();
    }

    public Button Knapp()
    {
        Button knapp = new Button();
        knapp.Content = "SpelHanterare";
        return knapp;
    }
}
