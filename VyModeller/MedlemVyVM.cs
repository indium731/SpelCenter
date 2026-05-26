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
        private ObservableCollection<MedlemEntitetVM> medlemListaLada = new();
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
            SorteringText = MedlemLista.HamtaMedlemLista().NuvarandeSortering();
            LaddaMedlemmar();
        }

        private void LaddaMedlemmar()
        {
            var medlemmar = MedlemLista.HamtaMedlemLista().medlemmar;
            MedlemListaLada.Clear();
            foreach(Medlem medlem in medlemmar)
            {
                MedlemListaLada.Add(new MedlemEntitetVM(medlem));
            }

        }

        [RelayCommand]
        private void GaTillMeny()
        {
            _navigator.NavigeraTill(new MedlemMenyVyVM(_navigator));
        }

        [RelayCommand]
        private void TestaLaggTillMedlem()
        {
            try
            {
                Medlem medlem = MedlemLista.HamtaMedlemLista().LaggTill(Namn.Trim(),
                                                        TelefonNummer.Trim(),
                                                        MedlemsNummer.Trim(),
                                                        Administrator);
                
                MedlemListaLada.Add(new MedlemEntitetVM(medlem));
            

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        [RelayCommand]
        private void TaBortValdMedlem()
        {
            if (ValdMedlem is not MedlemEntitetVM valdMedlem)
            {
                DetaljText = "Välj en medlem att ta bort";
                return;
            }
            
            MedlemLista.HamtaMedlemLista().TaBort(valdMedlem.TillMedlem());
            MedlemListaLada.Remove(valdMedlem);
            DetaljText = "Ingen medlem vald";

        }

        [RelayCommand]
        partial void OnValdMedlemChanged(MedlemEntitetVM medlem)
        {
            DetaljText = medlem.TillMedlem().UtokadeDetaljer();
        }

        [RelayCommand]
        private void UppdateraValdMedlem()
        {
            try
            {
                if (ValdMedlem is not MedlemEntitetVM valdMedlem) return;
                if (Namn.Trim().Count() != 0) valdMedlem.namn = Namn.Trim();
                if (TelefonNummer.Trim().Count() != 0) valdMedlem.telefonNummer = TelefonNummer.Trim();
                if (MedlemsNummer.Trim().Count() != 0) valdMedlem.medlemsNummer = MedlemsNummer.Trim();

                MedlemLista.HamtaMedlemLista().SparaMedlem(valdMedlem.TillMedlem());

            }
            catch (Exception ex)
            {
            }
        }

        [RelayCommand]
        private void OkaMedlemSkapAr()
        {
            if (ValdMedlem is not MedlemEntitetVM valdMedlem) return;
            valdMedlem.medlemSkap.slutDatum = valdMedlem.medlemSkap.slutDatum.AddYears(1);
            LaddaMedlemmar();
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
            LaddaMedlemmar();
        }
        [RelayCommand]
        private void Sok()
        {
            if (SokTextVisas)
            {
                var medlemmar = MedlemLista.HamtaMedlemLista().Sok(SokText.Trim());
                MedlemListaLada.Clear();
                foreach (Medlem medlem in medlemmar)
                {
                    MedlemListaLada.Add(new MedlemEntitetVM(medlem));
                }

            }
            if (SokCheckVisas)
            {
                var medlemmar = MedlemLista.HamtaMedlemLista().Sok(SokCheck.ToString());
                MedlemListaLada.Clear();
                foreach (Medlem medlem in medlemmar)
                {
                    MedlemListaLada.Add(new MedlemEntitetVM(medlem));
                }
            }
            if (SokDatumVisas)
            {
                if (SokDatum == null) return;
                var medlemmar = MedlemLista.HamtaMedlemLista().Sok(SokDatum.ToString());
                MedlemListaLada.Clear();
                foreach (Medlem medlem in medlemmar)
                {
                    MedlemListaLada.Add(new MedlemEntitetVM(medlem));
                }
            }
        }
    }
}