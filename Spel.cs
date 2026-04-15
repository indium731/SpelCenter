using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace Labb1_OOP
{
	// En Enum för kategorier gör det mycket enklare att använda LINQ (filtrering/gruppering)
	public enum Spelkategori
	{
		Strategi,
		Familjespel,
		Samarbetsspel,
		Festspel,
		Kortspel
	}

	internal class Spel
	{
		// --- Egenskaper (Properties) ---
		public string Titel { get; set; }
		public Spelkategori Kategori { get; set; }
		public int MinAntalSpelare { get; set; }
		public int MaxAntalSpelare { get; set; }
		public int SpeltidIMinuter { get; set; }
		public int Svarighetsgrad { get; set; } // T.ex. skala 1-5
		public string Beskrivning { get; set; }

		// Status för att hantera utlåning/reservation (viktigt enligt verksamhetsbeskrivningen)
		public bool ArTillgangligt { get; set; }

		// --- Konstruktor ---
		public Spel(string titel, Spelkategori kategori, int minSpelare, int maxSpelare, int speltid, int svarighet, string beskrivning)
		{
			Titel = titel;
			Kategori = kategori;
			MinAntalSpelare = minSpelare;
			MaxAntalSpelare = maxSpelare;
			SpeltidIMinuter = speltid;
			Svarighetsgrad = svarighet;
			Beskrivning = beskrivning;
			ArTillgangligt = true; // Som standard är spelet tillgängligt när det registreras
		}

		// --- Domänlogik (Metoder) ---

		/// <summary>
		/// Kontrollerar om spelet fungerar för en specifik gruppstorlek.
		/// </summary>
		public bool PassarFörAntalSpelare(int antal)
		{
			return antal >= MinAntalSpelare && antal <= MaxAntalSpelare;
		}

		/// <summary>
		/// Formaterar hur spelet visas i t.ex. en ListBox i WPF.
		/// </summary>
		public override string ToString()
		{
			string status = ArTillgangligt ? "Tillgängligt" : "Reserverat";
			return $"{Titel} ({Kategori}) | {MinAntalSpelare}-{MaxAntalSpelare} pers | {status}";
		}
	}
}