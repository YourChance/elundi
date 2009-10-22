using System;
using System.Text;
using System.Collections;

namespace ETEnTranslator
{
	/// <summary>
	/// Description of ETRNoun.
	/// </summary>
	public partial class ETEnNoun : IModule
	{
		public ETEnNoun()
		{
		}
		
		protected string osnova;
		
		public Slovo Analyze(Predlozhenie pr,int place)
		{
			Slovo analyzed = pr[place];
			
			PreAnalyze(pr,place,ref analyzed);
			
			return analyzed;
		}

		protected void ShiftPadezh(ref Slovo slovo)
		{
			switch(slovo.padezh)
			{
				case Padezh.Vinitelnij:
					slovo.ruSlovo.ruPadezh = RuPadezh.Vinitelniy;
					break;
				case Padezh.Prityazhatelnij:
					slovo.ruSlovo.ruPadezh = RuPadezh.Roditelniy;
					break;
				case Padezh.Datelnij:
					slovo.ruSlovo.ruPadezh = RuPadezh.Datelniy;
					break;
				case Padezh.Tvoritelnij:
					slovo.ruSlovo.ruPadezh = RuPadezh.Tvoritelniy;
					break;
				case Padezh.Imenitelnij:
					slovo.ruSlovo.ruPadezh = RuPadezh.Imenitelniy;
					break;
				default:
					slovo.ruSlovo.ruPadezh = RuPadezh.Predlozhniy;
					break;
			}		
		}
		
		protected void PreAnalyze(Predlozhenie pr,int place,ref Slovo slovo)
		{
			AnalyzePadezh(pr,place,ref slovo);
			AnalyzeChislo(ref slovo);
			AnalyzeRod(ref slovo);
			FindOsnova(ref slovo);
		}

		protected void AnalyzePadezh(Predlozhenie pr,int place,ref Slovo slovo)
		{
			int i = place-1;
			
			while(i>0 && (pr[i].chastRechi == ChastRechi.Prilagatelnoe || pr[i].chastRechi == ChastRechi.Prichastie || pr[i].chastRechi == ChastRechi.Mestoimenie))
			{
				i = i-1;
			}
			
			if(pr[i]!=null && pr[i].chastRechi == ChastRechi.Predlog)
			{
				Slovo predlog = pr[i];
				switch(predlog.eSlovo)
				{
					case "FF":
						slovo.padezh = Padezh.Vinitelnij;
						break;
					case "FQV":
					case "FJB":
						slovo.padezh = Padezh.Prityazhatelnij;
						break;
					case "FZP":
						slovo.padezh = Padezh.Datelnij;
						break;
					case "FT":
						slovo.padezh = Padezh.Tvoritelnij;						
						break;
					case "FR":
						slovo.padezh = Padezh.Instrumentalnij;
						break;
					case "FZJ":
						slovo.padezh = Padezh.Prisvyazochnij;
						break;
					case "FOB":
						slovo.padezh = Padezh.Dejstviya;
						break;
					case "FZO":
						slovo.padezh = Padezh.Predmeta;
						break;
					case "FZE":
						slovo.padezh = Padezh.Napravleniya;
						break;
					case "FA":
						slovo.padezh = Padezh.Mestoprebivaniya;
						break;
					case "FWV":
						slovo.padezh = Padezh.Obrasheniya;
						break;
					case "FSVE":
						slovo.padezh = Padezh.Avtora;
						break;
					case "FRZF":
						slovo.padezh = Padezh.Nazvaniya;
						break;
					case "FVA":
						slovo.padezh = Padezh.Celi;
						break;
					case "FFVY":
						slovo.padezh = Padezh.Prichini;
						break;
					case "FFV":
						slovo.padezh = Padezh.Sledstviya;
						break;					
				}
			}
			else
			{
				slovo.padezh = Padezh.Imenitelnij;
			}				
		}
		
		protected void AnalyzeChislo(ref Slovo slovo)
		{
			switch(slovo.eSlovo[0])
			{
				case 'Q':
					slovo.chislo = Chislo.Edinstvennoe;
					slovo.ruSlovo.ruChislo = RuChislo.Edinstvennoe;
					if(slovo.eSlovo.IndexOf("QBA")!=-1)
						slovo.chislo = Chislo.Odinochnoe;
					if(slovo.eSlovo.IndexOf("WBA")!=-1)
						slovo.chislo = Chislo.Malochislennoe;
					break;
				case 'W':
					slovo.chislo = Chislo.Mnozhestvennoe;
					slovo.ruSlovo.ruChislo = RuChislo.Mnozhestvennoe;
					if(slovo.eSlovo.IndexOf("QBA")!=-1)
						slovo.chislo = Chislo.Neopredelennoe;
					if(slovo.eSlovo.IndexOf("WBA")!=-1)
						slovo.chislo = Chislo.Mnogoobraznoe;
					break;
				default:
					// здесь будет обработка ошибок
					break;
			}
		}
		
		protected void AnalyzeRod(ref Slovo slovo)
		{
			slovo.rod = Rod.Obshij;
			if(slovo.eSlovo.IndexOf("]") != -1)
			{
				slovo.rod = Rod.Muzhskoj;
				slovo.ruSlovo.ruRod = RuRod.Muzhskoj;
			}
			if(slovo.eSlovo.IndexOf("[") != -1)
			{
				slovo.rod = Rod.Zhenskij;
				slovo.ruSlovo.ruRod = RuRod.Zhenskij;
			}
			if(slovo.eSlovo.IndexOf("EVA") != -1)
			{
				slovo.rod = Rod.Muzhskoj;
				slovo.ruSlovo.ruRod = RuRod.Muzhskoj;
			}
			if(slovo.eSlovo.IndexOf("RVA") != -1)
			{
				slovo.rod = Rod.Zhenskij;
				slovo.ruSlovo.ruRod = RuRod.Zhenskij;
			}
		}
		
		protected void FindOsnova(ref Slovo slovo)
		{
			osnova = slovo.eSlovo;
			osnova = osnova.Replace("EVA","");
			osnova = osnova.Replace("RVA","");
			osnova = osnova.Replace("QBA","");
			osnova = osnova.Replace("WBA","");
			
			if(osnova.Length>3 && osnova.Substring(0,2)==osnova.Substring(2,2))	
				osnova = osnova.Remove(0,2);
			if(osnova[0] == 'W')
				osnova = "Q"+osnova.Substring(1);
			while(osnova[osnova.Length-1]=='-')
			{
				osnova = osnova.Remove(osnova.Length-1,1);
			}
		}
	}
}
