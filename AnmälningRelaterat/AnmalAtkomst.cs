using System.Windows.Controls;

namespace Labb1_OOP;

public class AnmalAtkomst : IAtkomst
{

    public Button Knapp()
    {
        Button knapp = new Button();
        knapp.Content = "Anmäl dig";
        knapp.Tag = new AnmalVy();
        return knapp;
    }
}