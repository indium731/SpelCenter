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
        private MedlemListaVM medlemListaLada = new MedlemListaVM();
        [ObservableProperty]
        private MedlemEntitetVM? valdMedlem;
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
            SorteringText = medlemListaLada.NuvarandeSortering();
        }


        [RelayCommand]
        private void GaTillMeny()
        {
            _navigator.NavigeraTill(new MedlemMenyVyVM(_navigator));
        }

        [RelayCommand]
        private async void TestaLaggTillMedlem()
        {
            try
            {
                await MedlemListaLada.LaggTillAsync(Namn.Trim(),
                                                        TelefonNummer.Trim(),
                                                        MedlemsNummer.Trim(),
                                                        Administrator);
                
            

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        [RelayCommand]
        private async void TaBortValdMedlem()
        {
            if (ValdMedlem is not MedlemEntitetVM valdMedlem)
            {
                MessageBox.Show("Välj en medlem att ta bort");
                return;
            }
            
            await MedlemListaLada.TaBortAsync(valdMedlem);
            DetaljText = "Ingen medlem vald";
        }

        [RelayCommand]
        partial void OnValdMedlemChanged(MedlemEntitetVM medlem)
        {
            DetaljText = medlem.UtokadeDetaljer();
        }

        [RelayCommand]
        private async Task UppdateraValdMedlem()
        {
            try
            {
                if (ValdMedlem is not MedlemEntitetVM valdMedlem) return;
                if (!string.IsNullOrWhiteSpace(Namn.Trim())) valdMedlem.namn = Namn.Trim();
                if (!string.IsNullOrWhiteSpace(TelefonNummer.Trim())) valdMedlem.telefonNummer = TelefonNummer.Trim();
                if (!string.IsNullOrWhiteSpace(MedlemsNummer.Trim())) valdMedlem.medlemsNummer = MedlemsNummer.Trim();

                await MedlemListaLada.SparaMedlemAsync(valdMedlem);
                DetaljText = "Ingen medlem vald";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        [RelayCommand]
        private void OkaMedlemSkapAr()
        {
            if (ValdMedlem is not MedlemEntitetVM valdMedlem) return;
            valdMedlem.medlemSkap.slutDatum = valdMedlem.medlemSkap.slutDatum.AddYears(1);
        }
        [RelayCommand]
        private void AndraSortering()
        {
            MedlemListaLada.GaTillNastaMetod();
            SorteringText = MedlemListaLada.NuvarandeSortering();
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
                MedlemListaLada.Sok(SokText.Trim());
            }
            if (SokCheckVisas)
            {
                MedlemListaLada.Sok(SokCheck.ToString());
            }
            if (SokDatumVisas)
            {
                SokDatum ??= DateTime.Now;
                string SokDatumText = SokDatum.ToString() ?? DateTime.Now.ToString();
                MedlemListaLada.Sok(SokDatumText);
            }
        }
    }
}