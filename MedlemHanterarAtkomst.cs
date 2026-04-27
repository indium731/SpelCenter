using System.Windows.Controls;

namespace Labb1_OOP;

public class MedlemHanterarAtkomst : IAtkomst
{
    public ContentControl Atkom()
    {
        return new MedlemHanterarVy();
    }

    public Button Knapp()
    {
        Button knapp = new Button();
        knapp.Content = "MedlemHanterare";

        return knapp;
    }
}
