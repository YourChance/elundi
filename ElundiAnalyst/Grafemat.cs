/*
 * Created by SharpDevelop.
 * User: dao
 * Date: 20.02.2009
 * Time: 20:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections;

namespace ETEnTranslator
{
	/// <summary>
	/// Description of Grafemat.
	/// </summary>
	public class Grafemat
	{
		public Grafemat()
		{
		}
		
		public ArrayList AnalyzeText(string text)
		{
			ArrayList ar = new ArrayList();
			FirstParse(text,0,ar);
			ArrayList ret = SecondParse(ar);
			return ret;
		}
		
		public ArrayList AnalyzeTextEl(string text)
		{
			ArrayList ar = new ArrayList();
			FirstParseEl(text,0,ar);
			ArrayList ret = SecondParseEl(ar);
			return ret;
		}
		
		protected void FirstParse(string text,int position,ArrayList ar)
		{
			if(position < text.Length)
			{
				if(text[position]==' ')
					FirstParse(text,position+1,ar);
				else
				{
					if(IsSlovo(text,position)!=-1)
					{
						ar.Add(new Slovo(text.Substring(position,IsSlovo(text,position)-position), ChastRechi.Neopredelennaya));
						FirstParse(text,IsSlovo(text,position),ar);
					}					
					else 
					{
						if(position+3<text.Length&&text.Substring(position,3)=="...")
						{
							ar.Add(new Slovo("...", ChastRechi.Znak));
							FirstParse(text,position+3,ar);
						}
						else
						{
							ar.Add(new Slovo(text.Substring(position,1), ChastRechi.Znak));
							FirstParse(text,position+1,ar);
						}
					}
				}
			}
			else return;			
		}
		
		protected void FirstParseEl(string text,int position,ArrayList ar)
		{
			if(position < text.Length)
			{
				if(text[position]==' ')
					FirstParseEl(text,position+1,ar);
				else
				{
					if(IsSlovo(text,position)!=-1)
					{
						ar.Add(new Slovo(text.Substring(position,IsSlovo(text,position)-position), ChastRechi.Neopredelennaya,false));
						FirstParseEl(text,IsSlovo(text,position),ar);
					}					
					else 
					{
						if(position+3<text.Length&&text.Substring(position,3)=="...")
						{
							ar.Add(new Slovo("...", ChastRechi.Znak,false));
							FirstParseEl(text,position+3,ar);
						}
						else
						{
							ar.Add(new Slovo(text.Substring(position,1), ChastRechi.Znak,false));
							FirstParseEl(text,position+1,ar);
						}
					}
				}
			}
			else return;			
		}
		
		protected ArrayList SecondParse(ArrayList slova)
		{
			ArrayList ar = new ArrayList();
			Predlozhenie pr = new Predlozhenie();
			
			for(int i=0;i<slova.Count;i++)
			{
				Slovo current = (Slovo)slova[i];
				if(current.chastRechi == ChastRechi.Znak)
				{
					if(current.rSlovo == "."||current.rSlovo == "?"||current.rSlovo == "!"||current.rSlovo == "..."||current.rSlovo == "\n")
					{
						pr.AddSlovo(current);
						if(i+1<slova.Count)
						{
							current = (Slovo)slova[i+1];
							while(current.chastRechi == ChastRechi.Znak&&i+1<slova.Count)
							{
								pr.AddSlovo(current);
								i++;
								if(i+1<slova.Count)
								current = (Slovo)slova[i+1];														
							}
							//i--;
						}
						ar.Add(pr);
						pr = new Predlozhenie();
						
					}
					else
					{
						pr.AddSlovo(current);
					}
				}
				else 
				{
					pr.AddSlovo(current);
				}
			}
			
			ar.Add(pr);
			
			return ar;
		}
		
		protected ArrayList SecondParseEl(ArrayList slova)
		{
			ArrayList ar = new ArrayList();
			Predlozhenie pr = new Predlozhenie();
			
			for(int i=0;i<slova.Count;i++)
			{
				Slovo current = (Slovo)slova[i];
				if(current.chastRechi == ChastRechi.Znak)
				{
					if(current.eSlovo == "."||current.eSlovo == "?"||current.eSlovo == "!"||current.eSlovo == "..."||current.eSlovo == "\n")
					{
						pr.AddSlovo(current);
						if(i+1<slova.Count)
						{
							current = (Slovo)slova[i+1];
							while(current.chastRechi == ChastRechi.Znak&&i+1<slova.Count)
							{
								pr.AddSlovo(current);
								i++;
								if(i+1<slova.Count)
								current = (Slovo)slova[i+1];														
							}
							//i--;
						}
						ar.Add(pr);
						pr = new Predlozhenie();
						
					}
					else
					{
						pr.AddSlovo(current);
					}
				}
				else 
				{
					pr.AddSlovo(current);
				}
			}
			
			ar.Add(pr);
			
			return ar;
		}
		
		
		public static int IsSlovo(string s,int position)
		{
			int i = position;
			string razr = "ёйцукенгшщзхъфывапролджэячсмитьбюЁЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮqwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM0123456789-_";
			while(true)
			{
				if(i==s.Length||razr.IndexOf(s[i])==-1)
					break;
				i++;
			}
			if(i==position)
				return -1;
			else return i;
		}
	}
}
