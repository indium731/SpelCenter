using System.Windows.Controls;

namespace Labb1_OOP;

public class MedlemHanterarAtkomst : IAtkomst
{

    public Button Knapp()
    {
        Button knapp = new Button();
        knapp.Content = "MedlemHanterare";
        knapp.Tag = new MedlemHanterarVy();
        return knapp;
    }
}
