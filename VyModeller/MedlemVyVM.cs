using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb1_OOP.Modeller;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Labb1_OOP.VyModeller
{
    public partial class MedlemVyVM : ObservableObject
    {
        [ObservableProperty]
        private string namn;

        [ObservableProperty]
        private string telefonNummer;

        [ObservableProperty]
        private string medlemsNummer;

        [ObservableProperty]
        private bool administrator;
        [ObservableProperty]
        private string sorteringText;
        [ObservableProperty]
        private ObservableCollection<Medlem> medlemListaLada;
        [ObservableProperty]
        private Medlem? valdMedlem;
        [ObservableProperty]
        private string detaljText;
        [ObservableProperty]
        private string sokText;
        [ObservableProperty]
        private bool sokCheck;
        [ObservableProperty]
        private DateTime? sokDatum;
        [ObservableProperty]
        private bool sokDatumVisas;
        [ObservableProperty]
        private bool sokCheckVisas;
        [ObservableProperty]
        private bool sokTextVisas;

        private Navigator _navigator;
        public MedlemVyVM(Navigator n)
        {
            _navigator = n;
            InitieraMedlemLista();
        }

        private void InitieraMedlemLista()
        {
            SorteringText = MedlemLista.HamtaMedlemLista().NuvarandeSortering();
            MedlemListaLada = MedlemLista.HamtaMedlemLista().medlemmar;
        }

        [RelayCommand]
        private void GaTillMeny()
        {
            _navigator.NavigeraTill(new MedlemMenyVM(_navigator));
        }

        [RelayCommand]
        private void TestaLaggTillMedlem()
        {
            try
            {
                MedlemLista.HamtaMedlemLista().LaggTill(Namn.Trim(),
                                                        TelefonNummer.Trim(),
                                                        MedlemsNummer.Trim(),
                                                        Administrator);
            

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        [RelayCommand]
        private void TaBortValdMedlem()
        {
            if (ValdMedlem is not Medlem valdMedlem)
            {
                DetaljText = "Välj en medlem att ta bort";
                return;
            }
            
            MedlemLista.HamtaMedlemLista().TaBort(valdMedlem);
            DetaljText = "Ingen medlem vald";

        }

        [RelayCommand]
        partial void OnValdMedlemChanged(Medlem medlem)
        {
            DetaljText = medlem.UtokadeDetaljer();
        }

        [RelayCommand]
        private void UppdateraValdMedlem()
        {
            try
            {
                if (ValdMedlem is not Medlem valdMedlem) return;
                if (Namn.Trim().Count() != 0) valdMedlem.namn = Namn.Trim();
                if (TelefonNummer.Trim().Count() != 0) valdMedlem.telefonNummer = TelefonNummer.Trim();
                if (MedlemsNummer.Trim().Count() != 0) valdMedlem.medlemsNummer = MedlemsNummer.Trim();
            }
            catch
            {
            }
        }

        [RelayCommand]
        private void OkaMedlemSkapAr()
        {
            if (ValdMedlem is not Medlem valdMedlem) return;
            valdMedlem.medlemSkap.slutDatum = valdMedlem.medlemSkap.slutDatum.AddYears(1);
        }
        [RelayCommand]
        private void AndraSortering()
        {
            MedlemLista.HamtaMedlemLista().GaTillNastaMetod();
            SorteringText = MedlemLista.HamtaMedlemLista().NuvarandeSortering();
            SokTextVisas = false;
            SokCheckVisas = false;
            SokDatumVisas = false;
            if (SorteringText == "Namn") SokTextVisas = true;
            if (SorteringText == "Admin") SokCheckVisas = true;
            if (SorteringText == "MedlemStatus") SokCheckVisas = true;
            if (SorteringText == "Startdatum" || SorteringText == "Slutdatum") SokDatumVisas = true;
        }
        [RelayCommand]
        private void Sok()
        {
            if (SokTextVisas)
            {
                MedlemListaLada = MedlemLista.HamtaMedlemLista().Sok(SokText.Trim());
            }
            if (SokCheckVisas)
            {
                MedlemListaLada = MedlemLista.HamtaMedlemLista().Sok(SokCheck.ToString());
            }
            if (SokDatumVisas)
            {
                if (SokDatum == null) return;
                MedlemListaLada = MedlemLista.HamtaMedlemLista().Sok(SokDatum.ToString());
            }
        }
    }
}