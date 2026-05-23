using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Labb1_OOP.Data;
using Microsoft.EntityFrameworkCore;


namespace Labb1_OOP.Modeller;

public sealed class MedlemLista
{
    private SpelCenterDbContext _context;
    private MedlemLista(SpelCenterDbContext context)
    {
        _context = context;
        metoder = new List<Sorterare<Medlem>>
        {
            new Sorterare<Medlem>(m=>m.namn, "Namn"),
            new Sorterare<Medlem>(m=>m.medlemSkap.medlemStatus, "MedlemStatus"),
            new Sorterare<Medlem>(m=>m.medlemSkap.startDatum, "Startdatum"),
            new Sorterare<Medlem>(m=>m.medlemSkap.slutDatum, "Slutdatum"),
            new Sorterare<Medlem>(m=>m.admin, "Admin"),
        };
        metodIndex = 0;
    }
    public ObservableCollection<Medlem> medlemmar 
    {
        get
        {
            ObservableCollection<Medlem> medlemmar = new ObservableCollection<Medlem>(_context.Medlem);
            medlemmar = metoder[metodIndex].Sortera(medlemmar);

            return medlemmar;
        } 
        private set; 
    }

    private List<Sorterare<Medlem>> metoder;
    private int metodIndex;

    public Medlem LaggTill(string n, string t, string m, bool a)
    {
        Medlem nyMedlem = new Medlem(n, t, m, a);
        _context.Medlem.Add(nyMedlem);
        _context.SaveChanges();
        MessageBox.Show("Ny medlem har nu lagts till");
        return nyMedlem;
    }

    public void TaBort(Medlem medlem)
    {
        _context.Medlem.Remove(medlem);
        _context.SaveChanges();
        MessageBox.Show("Medlem har nu tagits bort");
    }
    public void GaTillNastaMetod()
    {
        metodIndex = (metodIndex + 1) % metoder.Count();
    }
    public ObservableCollection<Medlem> Sok(string sokOrd)
    {
        return new ObservableCollection<Medlem>(medlemmar.Where(m => metoder[metodIndex].Matchar(m, sokOrd.ToLower())));
    }
    public string NuvarandeSortering()
    {
        return metoder[metodIndex].sortering;
    }
    public Medlem Seed(string n, string t, string m, bool a)
    {
        Medlem nyMedlem = new Medlem(n, t, m, a);
        _context.Medlem.Add(nyMedlem);
        _context.SaveChanges();
        return nyMedlem;
    }
}
