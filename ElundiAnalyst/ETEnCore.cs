using System;
using System.Collections;

namespace ETEnTranslator
{
	public class ETEnCore
	{
        IModule NounModule;
        IModule AdjectiveModule;
        IModule VerbModule;
        IModule PredlogModule;
        IModule DefaultModule;
        IModule OtherModule;

		public ETEnCore()
		{
            NounModule = new ETEnNoun();
            /**
             * Сюда будете добавлять
             * свои модули для анализа,
             * когда сделаете
             */
            AdjectiveModule = new ETEnEmpty(); //new ETEnAdjective();
            VerbModule = new ETEnEmpty(); //new GlagModule();
            PredlogModule = new ETEnEmpty(); //new ETEnPredlog();
            DefaultModule = new ETEnEmpty(); //new ETEnDefault();
            OtherModule = new ETEnEmpty(); //new ETEnOther();
		}

        public ArrayList Analyze(string inText)
		{
			Grafemat gr = new Grafemat();
			ArrayList prText = gr.AnalyzeTextEl(inText);
			
			for(int i=0;i<prText.Count;i++)
			{
				Predlozhenie curPred = (Predlozhenie)prText[i];
				
				//
				//	Первый этап - анализ
				//
				
				//Нулевой проход - определение частей речи
				
				for(int j=0;j<curPred.Count;j++)
				{
					Slovo curSlovo = curPred[j];
					if(curSlovo.chastRechi != ChastRechi.Znak)
					{
						string eSlovo = curSlovo.eSlovo;
						if(eSlovo.Length == 1)
							curSlovo.chastRechi = ChastRechi.Mestoimenie;
						else
						switch(eSlovo[0])
						{
							case 'Q':
							case 'W':
								curSlovo.chastRechi = ChastRechi.Suschestvitelnoe;
								break;
							case 'F':
								curSlovo.chastRechi = ChastRechi.Predlog;
								break;
							case 'E':
								curSlovo.chastRechi = ChastRechi.Prilagatelnoe;
								break;
							case 'I':
								curSlovo.chastRechi = ChastRechi.Prichastie;
								break;
							case 'A':
								curSlovo.chastRechi = ChastRechi.Mestoimenie;
								break;
							case 'R':
							case 'T':
							case 'Y':
							case 'U':
							case 'O':
								curSlovo.chastRechi = ChastRechi.Glagol;
								break;
							//etc...
							default:
								curSlovo.chastRechi = ChastRechi.Mezhdometie;
								break;
						}
					}
					
					curPred.SetSlovo(curSlovo,j);
				}
				
				// Первый проход - существительные
				
				for(int j=0;j<curPred.Count;j++)
				{
					Slovo curSlovo = curPred[j];
					if(curSlovo.chastRechi != ChastRechi.Znak)
					{
						string eSlovo = curSlovo.eSlovo;
						if(eSlovo[0]=='Q' || eSlovo[0]=='W')
						{
							curSlovo = NounModule.Analyze(curPred,j);	
						}
					}
					
					curPred.SetSlovo(curSlovo,j);
				}
				
				// Полуторный проход - глаголы
				
				for(int j=0;j<curPred.Count;j++)
				{
					Slovo curSlovo = curPred[j];
					if(curSlovo.chastRechi != ChastRechi.Znak)
					{
						string eSlovo = curSlovo.eSlovo;
						if(eSlovo[0]=='R' || eSlovo[0]=='T' || eSlovo[0]=='Y' || eSlovo[0]=='U')
						{
							curSlovo = VerbModule.Analyze(curPred,j);	
						}
					}
					
					curPred.SetSlovo(curSlovo,j);
				}
				
				//Второй проход - все остальное.
				for(int j=0;j<curPred.Count;j++)
				{
					Slovo curSlovo = curPred[j];
					if(curSlovo.chastRechi != ChastRechi.Znak)
					{
						string eSlovo = curSlovo.eSlovo;
						
						//
						// Заменить на соответствующее для данной части речи.
						//
						if(eSlovo[0]=='E' || eSlovo[0]=='I')
						{
							curSlovo = AdjectiveModule.Analyze(curPred,j);
						}
						if(eSlovo[0]=='F')
						{
							curSlovo = PredlogModule.Analyze(curPred,j);
						}
						
						if(eSlovo[0]=='A')
						{
							curSlovo = OtherModule.Analyze(curPred,j);
						}
					}
					
					curPred.SetSlovo(curSlovo,j);
				}
                prText[i] = curPred;
			}
            return prText;
		}
	}
}
