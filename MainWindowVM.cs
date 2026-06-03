using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb1_OOP.Modeller;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using Labb1_OOP.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Labb1_OOP.Tjanster;
using Microsoft.Extensions.DependencyInjection;

namespace Labb1_OOP.VyModeller
{


public partial class MainWindowVM : ObservableObject
{
    [ObservableProperty]
    public partial ObservableObject Vy { get; set; }

	public Navigator Navigator {get; set;} 
    public MainWindowVM()
    {
		Navigator = App.tjanstLeverantor.GetRequiredService<Navigator>();
		Navigator.NavigeraTill(new InloggVyVM(Navigator));
    }
	}



}
