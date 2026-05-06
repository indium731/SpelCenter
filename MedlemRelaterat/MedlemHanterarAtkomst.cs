using System.Windows.Controls;

namespace Labb1_OOP;

public class MedlemHanterarAtkomst : IAtkomst
{

    public UserControl Atkom()
    {
        return new MedlemHanterarVy();
    }
    public string Namn()
    {
        return "MedlemHanterare";
    }
}
