using System;
using System.Text;
using System.Collections;

namespace ETEnTranslator
{
    public partial class ETEnEmpty : IModule
    {
        public ETEnEmpty()
        {
        }

        public Slovo Analyze(Predlozhenie pr, int place)
        {
            Slovo analyzed = pr[place];
            return analyzed;
        }
    }
}