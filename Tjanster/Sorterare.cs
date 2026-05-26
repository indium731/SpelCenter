using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Labb1_OOP;

public class Sorterare<T>
{
    public Sorterare(Func<T,Object> m, string s)
    {
        metod = m;
        sortering = s;

    }
    Func<T,Object> metod;
    public string sortering;
    public List<T> Sortera(List<T> lista)
    {
        return lista.OrderBy(metod).ToList();
    }
    public bool Matchar(T sokTema, string sokOrd)
    {
        string ord = metod(sokTema).ToString().ToLower();
        if (ord.Contains(sokOrd)) return true;
        return false;
    }
}
