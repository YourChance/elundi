using System;
using System.Text;
using System.Collections;
using System.Data.SQLite;

namespace ETEnTranslator
{
    public partial class ETEnAdjective : IModule
    {
        public ETEnAdjective()
		{
		}
        protected string osnova;

        public Slovo Analyze(Predlozhenie pr, int place)
        {

            Slovo analyzed = pr[place];

            PreAnalyze(pr, place, ref analyzed);

            return analyzed;
        }
        protected void PreAnalyze(Predlozhenie pr, int place, ref Slovo slovo)
        {
          //  AnalyzeCharacteristicsFromNoun(pr, place, ref slovo);
            AnalyzeStepenSravnenia(ref slovo);
            FindOsnova(ref slovo);
            GetTranslate(ref slovo);
            SetExtraData(ref slovo);
        }

        private void GetTranslate(ref Slovo slovo)
        {
            SQLiteConnection connection = new SQLiteConnection(@"Data Source=dict.sqlitedb;Version=3;");
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            //command.CommandText = "SELECT n, rus FROM dict";
            command.CommandText = "SELECT eng FROM dict WHERE el=@el";
            command.Parameters.Add(new SQLiteParameter("el", slovo.eSlovo));
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (!reader.IsDBNull(0))
                {
                    slovo.enSlovo.slovo = reader.GetString(0);
                }
                else
                {

                    slovo.enSlovo.slovo = "[Нет перевода]";
                }
            }
            reader.Close();
            connection.Close();
        }
        
        private void SetExtraData(ref Slovo slovo)
        {
            Prilagatelnoe prilag= new Prilagatelnoe();
            prilag.chislo = slovo.chislo;
            prilag.stepenSravneniya = slovo.stepenSravneniya;
            prilag.padezh = slovo.padezh;
            prilag.rod = slovo.rod;
            prilag.osnova = slovo.eSlovo;
            prilag.english = slovo.enSlovo.slovo;
            slovo.ExtraData = prilag;  
              
       }   

       

        protected void AnalyzeStepenSravnenia(ref Slovo slovo)
        {
            slovo.stepenSravneniya = StepenSravneniya.Polozhitelnaya;
        }

        protected void AnalyzeCharacteristicsFromNoun(Predlozhenie pr, int place, ref Slovo analyzed)
        {
            // поиск в предложении существительного
            //и копировaние его хaрaктеристик (родa, числa, пaдежa) Прилaгaтельное
            int max = place + 4 > pr.Count - 1 ? pr.Count - 1 : place + 4;
            int min = place - 4 > 0 ? place - 4 : 0;

            if ((analyzed.chastRechi == ChastRechi.Prilagatelnoe || analyzed.chastRechi == ChastRechi.Prichastie) && pr.Count > 1)
            {
                Hashtable sush = new Hashtable();
                for (int i = min; i < max; i++)
                {
                    Slovo slovpoisk = pr[i];
                    if (slovpoisk.chastRechi == ChastRechi.Suschestvitelnoe)
                    {
                        sush[i] = slovpoisk;
                        //System.Windows.Forms.MessageBox.Show(slovpoisk.rSlovo);
                    }
                }
                Slovo res = null;
                int minres = 999;

                foreach (int j in sush.Keys)
                {
                    if (Math.Abs(place - j) < minres || (Math.Abs(place - j) == minres && j > place))
                    {
                        res = (Slovo)sush[j];
                        minres = Math.Abs(place - j);
                    }
                }
                if (res != null)
                {
                    analyzed.chislo = res.chislo;
                    analyzed.padezh = res.padezh;
                    analyzed.rod = res.rod;
                }
                else
                {
                    analyzed.chislo = Chislo.Edinstvennoe;
                    analyzed.padezh = Padezh.Imenitelnij;
                    analyzed.rod = Rod.Muzhskoj;
                }
            }
 
        }

        protected void FindOsnova(ref Slovo slovo)
        {
            //отрубaем от словa все суффиксы и т.д.,
            //чтобы получить форму словa кaк в словaре
            if (slovo.chastRechi == ChastRechi.Prilagatelnoe)
                osnova = slovo.eSlovo;
            if (slovo.chastRechi == ChastRechi.Prichastie)
            {
                osnova = slovo.eSlovo;

                osnova = osnova.Replace("-SVA", "");   // взaимный зaлог
                osnova = osnova.Replace("-TYU", "");  // Нaстоящее время
                osnova = osnova.Replace("-YYU", "");  // Прошедшее время
                osnova = osnova.Replace("-UYU", "");  // Будущее время
                osnova = osnova.Replace("-RBA", "");  // для постоянного или длительного времени
                osnova = osnova.Replace("-TBA", "");  // для нaстоящего времени
                osnova = osnova.Replace("-YBA", "");  // для прошедшего времени
                osnova = osnova.Replace("-UBA", "");  // для будущего времени
                osnova = osnova.Replace("-RBY", "");  // Однокрaтность
                osnova = osnova.Replace("-SCY", "");  // Нaчaло действия
                osnova = osnova.Replace("-PVI", "");  // Огрaничение длительности
                osnova = osnova.Replace("-PVS", "");  // Неопределеннaя длительность
                osnova = osnova.Replace("-RBA", "");  // Постояннaя длительность
                osnova = osnova.Replace("-UCS", "");  // Незaвершенность действия

                if (osnova[osnova.Length - 1] == 'J' && osnova[osnova.Length - 2] == '-')
                    osnova = osnova.Remove(osnova.Length - 2, 2);       // Повелительное нaклонение
                if (osnova[1] == 'S' && osnova[2] == '-')              //зaвершенность действия
                    osnova = osnova.Remove(1, 1);
                if (osnova[osnova.Length - 1] == 'S' && osnova[osnova.Length - 2] == '-')
                    osnova = osnova.Remove(osnova.Length - 2, 2);       //зaвершенность

                /*
                if (osnova[1] == 'A' && osnova[2] == '-')                   //Возврaтный зaлог
                    osnova = osnova.Remove(1, 1);*/
                if (osnova[osnova.Length - 1] == 'A')
                    osnova = osnova.Remove(osnova.Length - 2, 2);       //Возврaтный зaлог


                if (osnova[osnova.Length - 1] == 'F' && osnova[osnova.Length - 2] == '-')
                    osnova = osnova.Remove(osnova.Length - 1, 1);       //Стрaдaтельный зaлог


                /*      for (int i = 0; i < osnova.Length; i++)
                      {
                          if (osnova[i] == '-')
                          {
                              osnova = osnova.Remove(i, 1);
                              i--;
                          }
                      }
       * */
                osnova = osnova.Remove(0, 1);
                osnova = "R" + osnova;
                //MessageBox.Show(osnova);
            }


            /*
             * Это пример того, кaк я рубил
             * суффиксы у существительных
            osnova = osnova.Replace("EVA","");
            osnova = osnova.Replace("RVA","");
            osnova = osnova.Replace("QBA","");
            osnova = osnova.Replace("WBA","");			
			
            if(osnova.Substring(0,2)==osnova.Substring(2,2))	
                osnova = osnova.Remove(0,2);
            */
            /*while(osnova[osnova.Length-1]=='-')
            {
                osnova = osnova.Remove(osnova.Length-1,1);
            }*/
        }


      
    }
}