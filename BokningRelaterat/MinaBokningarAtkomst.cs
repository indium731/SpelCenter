
using System.Windows.Controls;

namespace Labb1_OOP;

public class MinaBokningarAtkomst : IAtkomst
{
    public ContentControl Atkom()
    {
        return new MinaBokningarVy();
    }

    public Button Knapp()
    {
        Button knapp = new Button();
        knapp.Content = "Mina bokningar";
        return knapp;
    }
}
