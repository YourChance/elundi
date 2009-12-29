using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace ETEnTranslator
{
    class ETEnGlagol : IModule
    {
        string logOsnovaEl;
        Predlozhenie predl = null;
        int pozition_predl = 0;
        //Slovo slovo;

        public ETEnGlagol()
        {}

        public Slovo Analyze(Predlozhenie pr, int place)
        {
            Slovo analyzed = pr[place];

            PreAnalyze(pr, place, ref analyzed);

            return analyzed;

        }

        protected void PreAnalyze(Predlozhenie pr, int place, ref Slovo slovo)
        {
            AnalyzeGlagol(pr, place, ref slovo);
            GetTranslate(ref slovo);
            SetExtraData(ref slovo);
        }

        private void GetTranslate(ref Slovo slovo)
        {
            SQLiteConnection connection = new SQLiteConnection(@"Data Source=dict.sqlitedb;Version=3;");
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
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
            else
            {
                slovo.enSlovo.slovo = "[Нет перевода]";
            }
            reader.Close();
            connection.Close();
        }

        private void SetExtraData(ref Slovo slovo)
        {

            Glagol glagol = new Glagol();
            glagol.vremya = slovo.vremya;
            glagol.zalog = slovo.zalog;
            glagol.naklonenie = slovo.naklonenie;
            glagol.vid = slovo.vid;
            glagol.sostoyanie = slovo.sostoynie; 
            glagol.english = slovo.enSlovo.slovo;
            slovo.ExtraData = glagol;
        }

        protected void AnalyzeGlagol(Predlozhenie pr, int place, ref Slovo slovo)
        {
           // if (c.eSlovo.Length < 2) return c;
            bool Zaver = false;
           /* slovo.zalog = Zalog.Dejstvitekniy;//настройки по умолчанию
            slovo.chastRechi = ChastRechi.Glagol;//настройки по умолчанию
            slovo.sostoynie = Sostoynie.Обычное;//настройки по умолчанию
            slovo.chislo = Chislo.Edinstvennoe;//настройки по умолчанию
            slovo.rod = Rod.Obshij;//настройки по умолчанию            
            if (slovo.eSlovo[1] == 'A') slovo.zalog = Zalog.Vozvratniy;*/
            if (slovo.eSlovo.Length > 3) if (slovo.eSlovo[2] == '-' && slovo.eSlovo[1] == 'S')
                {
                    slovo.vid = Vid.Zavershennost;
                    Zaver = true;
                }
           // NaklSoslag();
            string[] temp = slovo.eSlovo.Split('-');
            if (temp[0].Length == 1 && temp.Length > 1)
            {
                switch (temp[1])
                {
                    //совершенный вид
                    case "RBY": //[туко]
                        slovo.vid = Vid.Mgnovennost;
                        break;
                    case "SCY"://[роко]
                        slovo.vid = Vid.NachaloDejstviya;
                        break;
                    case "PVI"://[пэсо]:
                        slovo.vid = Vid.OgranichenieDlitelnosti;
                        break;
                    //несовершенный вид
                    case "PVS"://[пэро]
                        slovo.vid = Vid.NeopredelennayaDlitelnost;
                        break;
                    case "RBA"://[тудо]
                        slovo.vid = Vid.PostoyannayaDlitelnost;
                        break;
                    case "UCS"://[шоро]
                        slovo.vid = Vid.NezavershennostDejstviya;
                        break;
                    default: break;
                }
            }
            for (int i = 1; i < temp.Length; i++)
            {
                switch (temp[i])
                {
                    //определение дополнительных времён
                    case "RBA"://постоянное(длительное)
                        switch (temp[0][0])
                        {
                            case 'T':
                                slovo.vremya = Vremya.NastoyascheeDlitelnoe;
                                break;
                            case 'Y':
                                slovo.vremya = Vremya.ProshedsheeDlitelnoe;
                                break;
                            case 'U':
                                slovo.vremya = Vremya.BuduscheeDlitelnoe;
                                break;
                            default: break;
                        }
                        break;
                    case "TBA"://наст
                        switch (temp[0][0])
                        {
                            case 'T':
                                slovo.vremya = Vremya.NastoyascheeVNastoyaschem;
                                break;
                            case 'Y':
                                slovo.vremya = Vremya.ProshedsheeSNastoyaschim;
                                break;
                            case 'U':
                                slovo.vremya = Vremya.BuduscheeVNastoyaschem;
                                break;
                            default: break;
                        }
                        break;
                    case "YBA"://прош
                        switch (temp[0][0])
                        {
                            case 'T':
                                slovo.vremya = Vremya.NastoyascheeSProshedshim;
                                break;
                            case 'Y':
                                slovo.vremya = Vremya.Davnoproshedshee;
                                break;
                            case 'U':
                                slovo.vremya = Vremya.BuduscheeSProshedshim;
                                break;
                            default: break;
                        }
                        break;
                    case "UBA"://буд
                        switch (temp[0][0])
                        {
                            case 'T':
                                slovo.vremya = Vremya.NastoyascheeSBuduschim;
                                break;
                            case 'Y':
                                slovo.vremya = Vremya.ProshedsheeBuduscheeBezNastoyaschego;
                                break;
                            case 'U':
                                slovo.vremya = Vremya.BuduscheeDalekoe;
                                break;
                            default: break;
                        }
                        break;
                    //определение залога
                    case "A":
                        slovo.zalog = Zalog.Vozvratniy;
                        break;
                    case "F":
                        slovo.zalog = Zalog.Stradatelniy;
                        break;
                    case "SBA":
                        slovo.zalog = Zalog.Vzaimniy;
                        break;
                    //определение наклонения
                    case "J":
                        slovo.naklonenie = Naklonenie.Povelitelnoe;
                        break;
                    //определение вида
                    //совершенный вид
                    case "S"://[ро]
                        if (!Zaver) slovo.vid = Vid.Zavershennost;
                        else slovo.vid = Vid.NevozvratnayaZavershennost;
                        break;
                    //состояние
                    case "TVY"://[чэко]                                        
                        slovo.sostoynie = Sostoynie.Безличное;
                        break;
                    default: break;
                }
            }
            logOsnovaEl += "\n***Характеристики*** " + "\nЧисло: " + slovo.chislo.ToString() + "\nНаклонение: " + slovo.naklonenie.ToString() + "\nРод: " + slovo.rod.ToString() + "\nСостояние: " + slovo.sostoynie.ToString() + "\nВид: " + slovo.vid.ToString() + "\nВремя: " + slovo.vremya.ToString() + "\nЗалог: " + slovo.zalog.ToString();
        }
       /* 
        private void NaklSoslag()//
        {
            int i = pozition_predl - 1;
            //пока смотрим только предыдущие слова
            while (i > 0)
            {
                if (predl[i].chastRechi != ChastRechi.Znak)
                {
                    if (predl[i].eSlovo == "J-OBP" || predl[i].eSlovo == "J-TVQ" || predl[i].eSlovo == "G-OBP" || predl[i].eSlovo == "S-OBP")
                    {
                        // "J-OBP": //бы                          
                        // "J-TVQ": //бы...ни                           
                        // "G-OBP": //бы (вот бы)                           
                        // "S-OBP": //чтобы                           
                        slovo.naklonenie = Naklonenie.Soslagatelnoe;
                        break;
                    }
                }
                else break;
                i--;
            }
        }*/
    }
}
