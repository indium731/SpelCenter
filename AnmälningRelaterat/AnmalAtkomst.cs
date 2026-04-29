using System.Windows.Controls;

namespace Labb1_OOP;

public class AnmalAtkomst : IAtkomst
{
    public ContentControl Atkom()
    {
        return new AnmalVy();
    }

    public Button Knapp()
    {
        Button knapp = new Button();
        knapp.Content = "Anmäl dig";
        return knapp;
    }
}