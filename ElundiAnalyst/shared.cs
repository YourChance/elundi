using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Collections;

namespace ETEnTranslator
{
    public struct Noun
    {
        public Chislo chislo;
        public Rod rod;
        public Padezh padezh;
        public string osnova;
        public string english;
        public override string ToString()
        {
            return
            "\r\nОснова: " + this.osnova +
            "\r\nПеревод: " + this.english +
            "\r\nРод: " + this.rod +
            "\r\nПадеж: " + this.padezh +
            "\r\nЧисло: " + this.chislo + "\r\n";
        }
    }

    public struct Glagol
    {
        public string english;
        public Vremya vremya;
        public Zalog zalog;
        public Naklonenie naklonenie;
        public Vid vid;
        public Sostoynie sostoyanie;
        public override string ToString()
        {
            return "\r\nПеревод: " + this.english +
                   "\r\nВремя: " + this.vremya +
                   "\r\nЗалог: " + this.zalog +
                   "\r\nНаклонение: " + this.naklonenie +
                   "\r\nВид: " + this.vid +
                   "\r\nСостояние: " + this.sostoyanie;
        }
    }

    public struct Mestoimenie
    {
        public string english;
        public override string ToString()
        {
            return 
                "\r\nПеревод: " + this.english;
        }
    }

    /*public struct Mestoimenie
    {
        public string english;
        public override string ToString()
        {
            return 
                "\r\nПеревод: " + this.english + "\r\n";
        }
    }*/

    public struct Prilagatelnoe
    {
        public string osnova;
        public string english;
        public Rod rod;
        public Padezh padezh;
        public StepenSravneniya stepenSravneniya;
        public Chislo chislo;      
        public override string ToString()
        {
            return 
                   "\r\nОснова: " + this.osnova +
                   "\r\nПеревод: " + this.english +
                   "\r\nРод: " + this.rod +
                   "\r\nПадеж: " + this.padezh +
                   "\r\nСтепень сравнения: " + this.stepenSravneniya +
                   "\r\nЧисло: " + this.chislo + "\r\n";
        }
    }
    public struct Predlog
    {
        public string english;
        public override string ToString()
        {
            return
            
            "\r\nПеревод: " + this.english +"\r\n";
        }
    }

    public enum ChastRechi
	{
		Neopredelennaya,
		Suschestvitelnoe,
		Chislitelnoe,
		ChislitelnoePoryadkovoe,
		Prilagatelnoe,
		Glagol,
		Prichastie,
		Deeprichastie,
		Narechie,
		Mestoimenie,
		Predlog,
		Souz,
		Chastica,
		Sluzhebnoe,
		Mezhdometie,
		Znak
	}
	
	//
	// Добавлено Ромой
	//
	public enum Sostoynie
    {
        Обычное,
        Безличное
    }
	
	public enum Lico
	{
		Первое,
		Второе,
		Третье
	}

	public enum Rod
	{
		Muzhskoj,
		Zhenskij,
		Obshij
	}
	
	public enum Odushevlennost
	{
		Odushevlennoe,
		Neodushevlennoe
	}
	
	public enum Chislo
	{
		Edinstvennoe,
		Mnozhestvennoe,
		Odinochnoe,
		Malochislennoe,
		Neopredelennoe,
		Mnogoobraznoe
	}
	
	public enum Padezh
	{
		Imenitelnij,
		Vinitelnij,
		Prityazhatelnij,
		Datelnij,
		Tvoritelnij,
		Instrumentalnij,
		Prisvyazochnij,
		Dejstviya,
		Predmeta,
		Napravleniya,
		Mestoprebivaniya,
		Obrasheniya,
		Avtora,
		Nazvaniya,
		Celi,
		Prichini,
		Sledstviya
	}
	
	public enum Vid
	{
		Zavershennost,
		NevozvratnayaZavershennost,
		Mgnovennost,
		NachaloDejstviya,
		OgranichenieDlitelnosti,
		NeopredelennayaDlitelnost,
		PostoyannayaDlitelnost,
		NezavershennostDejstviya
	}
	
	public enum Zalog
	{
		Vzaimniy,
		Vozvratniy,
		Stradatelniy,
		Dejstvitekniy
	}
	
	public enum Vremya
	{
		Nastoyaschee,
		Proshedshee,
		Buduschee,
		Postoyannoe,
		NastoyascheeDlitelnoe,
		NastoyascheeVNastoyaschem,
		NastoyascheeSProshedshim,
		NastoyascheeSBuduschim,
		ProshedsheeDlitelnoe,
		ProshedsheeSNastoyaschim,
		Davnoproshedshee,
		ProshedsheeBuduscheeBezNastoyaschego,
		BuduscheeDlitelnoe,
		BuduscheeVNastoyaschem,
		BuduscheeSProshedshim,
		BuduscheeDalekoe
	}
	
	public enum Naklonenie
	{
		Izjavitelnoe,
		Povelitelnoe,
		Soslagatelnoe,		
		Zaochnoe,
		Infinitiv
	}
	
	public enum StepenSravneniya
	{
		Polozhitelnaya,
		Bolee,
		Menee,
		Naibolee,
		Naimenee
	}
	
	/// <summary>
	/// Description of Slovo.
	/// </summary>
	public class Slovo
	{
		public string eSlovo;
		public string rSlovo;	
		public ChastRechi chastRechi;
		public Rod rod;
		public Odushevlennost odushevlennost;
		public Chislo chislo;
		public Padezh padezh;
		public Vid vid;
		public Zalog zalog;
		public Vremya vremya;
		public Naklonenie naklonenie;
		public StepenSravneniya stepenSravneniya;
		public Sostoynie sostoynie; // Добавлено Ромой
		public Lico lico;
		public EnSlovo enSlovo;

        public object ExtraData;

		public Slovo()
		{
			enSlovo = new EnSlovo();
		}
		public Slovo(string eSlovo)
		{
			this.eSlovo = eSlovo;
			enSlovo = new EnSlovo();
		}
		public Slovo(string rSlovo,ChastRechi chastRechi)
		{
			this.rSlovo = rSlovo;
			this.chastRechi = chastRechi;
			enSlovo = new EnSlovo();
		}
		public Slovo(string eSlovo,ChastRechi chastRechi,bool b)
		{
			this.eSlovo = eSlovo;
			this.chastRechi = chastRechi;
			enSlovo = new EnSlovo();
		}

        public string ToString()
        {
            return
                eSlovo + ":\r\n"
                + "\t\tЧасть речи: " + this.chastRechi + "\r\n"
                + "\t\tРод: " + this.rod + "\r\n"
                + "\t\tОдушевленность: " + this.odushevlennost + "\r\n"
                + "\t\tЧисло: " + this.chislo + "\r\n"
                + "\t\tПадеж: " + this.padezh + "\r\n"
                + "\t\tВид: " + this.vid + "\r\n"
                + "\t\tЗалог: " + this.zalog + "\r\n"
                + "\t\tВремя: " + this.vremya + "\r\n"
                + "\t\tНаклонение: " + this.naklonenie + "\r\n"
                + "\t\tСтепень сравнения: " + this.stepenSravneniya + "\r\n"
                + "\t\tСостояние: " + this.sostoynie + "\r\n"
                + "\t\tЛицо: " + this.lico + "\r\n"
                ;
        }
	}
	
	public class EnSlovo
	{
        public string slovo;
	}
	
	public enum RuPadezh
	{
		Imenitelniy,
		Roditelniy,
		Vinitelniy,
		Datelniy,
		Tvoritelniy,
		Predlozhniy
	}
	
	public enum RuRod
	{
		Muzhskoj,
		Zhenskij,
		Srednij
	}
	
	public enum RuChislo
	{
		Edinstvennoe,
		Mnozhestvennoe
	}
	
	public enum RuForma
	{
		Polnaya,
		Kratkaya
	}
	
	public enum RuVid
	{
		Sovershenniy,
		Nesovershenniy
	}
	
	public enum RuPerehodnost
	{
		Perehodniy,
		Neperehodniy
	}
	
	public enum RuLico
	{
		Pervoe,
		Vtoroe,
		Tretie,
		Bezlichnoe
	}
	
	public class Predlozhenie
	{
		ArrayList slova;
		public Predlozhenie()
		{
			slova = new ArrayList();
		}
		public Slovo this[int n]
		{
			get
			{
				if(n>=0&&n<slova.Count)
					return (Slovo)slova[n];
				else return null;
			}
            set
            {
                slova[n] = value;
            }
		}
		public void SetSlovo(Slovo s,int i)
		{
			slova[i] = s;
		}
		public void AddSlovo(Slovo s)
		{
			slova.Add(s);
		}
		public int Count
		{
			get
			{
				return slova.Count;
			}
		}
		public string ToRString()
		{
			StringBuilder sb = new StringBuilder();
			foreach(Slovo s in slova)
				sb.Append(s.rSlovo+" "/*+"("+s.chastRechi.ToString()+") "*/);
			return sb.ToString();
		}
		public string ToEString()
		{
			StringBuilder sb = new StringBuilder();
			foreach(Slovo s in slova)
				if(s.chastRechi != ChastRechi.Znak)
					sb.Append(" "+s.eSlovo);
			else {
				try
				{
					if(s.eSlovo.IndexOf(' ')==-1)
						int.Parse(s.eSlovo);
					else int.Parse(s.eSlovo.Substring(0,s.eSlovo.IndexOf(' ')));
					sb.Append(" "+s.eSlovo);
				}
				catch(Exception e)
				{
					/*System.Windows.Forms.MessageBox.Show(e.ToString());
					System.Windows.Forms.MessageBox.Show(s.eSlovo);*/
					sb.Append(s.eSlovo);
				}
			}
			return sb.ToString();
		}
	}
	
	public interface IModule
	{
		Slovo Analyze(Predlozhenie p, int poziciya);
        // Пока это не делаем
		//Slovo Translate(Predlozhenie p, int poziciya);
	}
	
	public struct DictSlovo
	{
		public string Rus;
		public string El;
		public string Ex;
		public string Proizn;
		public DictSlovo(string rus,string el,string ex,string proizn)
		{
			Rus = rus;
			El = el;
			Ex = ex;
			Proizn = proizn;
		}
	}
}
