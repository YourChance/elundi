using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace ETEnTranslator
{
    class ETEnOther: IModule
    {
        public ETEnOther()
        { }

        public Slovo Analyze(Predlozhenie pr, int place)
        {
            Slovo analyzed = pr[place];

            PreAnalyze(pr, place, ref analyzed);

            return analyzed;
        }

        protected void PreAnalyze(Predlozhenie pr, int place, ref Slovo slovo)
        {
            //CopyCharact(pr, place, ref slovo);
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
            Mestoimenie mestoimenie = new Mestoimenie();
            mestoimenie.english = slovo.enSlovo.slovo;
            slovo.ExtraData = mestoimenie;
        }

        private void CopyCharact(Predlozhenie pr, int place, ref Slovo analyzed)
        {

            int i = (place - 4 >= 0 ? place - 4 : 0);
            int max = (place + 4 > pr.Count ? pr.Count : place + 4);
            while (i < max)
            {
                Slovo slovpoisk = pr[i];
                if (slovpoisk.chastRechi == ChastRechi.Suschestvitelnoe)
                {

                    break;
                }
                else i++;
            }
        }
    }
}
