using System;
using System.Text;
using System.Collections;
using System.Data.SQLite;

namespace ETEnTranslator
{
   public class ETRPredlog : IModule
    {
        public ETRPredlog()
        {
        }
        public Slovo Analyze(Predlozhenie pr, int place)
        {
            Slovo analyzed = pr[place];

            PreAnalyze(pr, place, ref analyzed);

            return analyzed;
        }
        protected void PreAnalyze(Predlozhenie pr, int place, ref Slovo slovo)
        {
            GetTranslate(ref slovo);
            SetExtraData(ref slovo);
        }
        private void SetExtraData(ref Slovo slovo)
        {
           
            Predlog predlog = new Predlog();

            predlog.english = slovo.enSlovo.slovo;
            slovo.ExtraData = predlog;
        }
        private void GetTranslate(ref Slovo slovo)
        {
            SQLiteConnection connection = new SQLiteConnection(@"Data Source=dict.sqlitedb;Version=3;");
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            //command.CommandText = "SELECT n, rus FROM dict";
            command.CommandText = "SELECT rus FROM dict WHERE el=@el";
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
    }
}
