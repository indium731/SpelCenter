using System.Windows.Controls;

namespace Labb1_OOP;

public class SpelAtkomst : IAtkomst
{

    public Button Knapp()
    {
        Button knapp = new Button();
        knapp.Content = "SpelHanterare";
        knapp.Tag = new SpelVy();
        return knapp;
    }
}
