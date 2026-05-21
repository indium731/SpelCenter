using System.Windows;
using System.Windows.Controls;

namespace Labb1_OOP.Vyer
{
    public partial class MedlemVy : UserControl
    {
        public MedlemVy()
        {
            InitializeComponent();
            DataContext = new VyModeller.MedlemVyVM();
        }
    }
}
