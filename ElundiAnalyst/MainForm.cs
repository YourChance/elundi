using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ETEnTranslator;

namespace ElundiAnalyst
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void StartAnalysisBtn_Click(object sender, EventArgs e)
        {
            wordView.Nodes.Clear();
            ETEnTranslator.ETEnCore core = new ETEnTranslator.ETEnCore();
            ArrayList wordList = core.Analyze(elundiText.Text);
            for (int i = 0; i < wordList.Count; i++)
            {
                Predlozhenie curPred = (Predlozhenie)wordList[i];

                TreeNode tn = null;
                if(curPred.Count > 0)
                    tn = wordView.Nodes.Add("Предложение " + (i + 1).ToString());
                
                for (int j = 0; j < curPred.Count; j++)
                {
                    Slovo curSlovo = curPred[j];

                    TreeNode slovoNode = tn.Nodes.Add(curSlovo.eSlovo);

                    /**
                     * Тут вы для своих частей речи добавляете свой вывод
                     */

                    if (curSlovo.chastRechi == ChastRechi.Suschestvitelnoe)
                    {
                        Noun myNoun = (Noun)curSlovo.ExtraData;
                        slovoNode.Nodes.Add("Часть речи: Существительное");
                        slovoNode.Nodes.Add("Основа: " + myNoun.osnova.ToString());
                        slovoNode.Nodes.Add("Перевод: " + myNoun.english.ToString());
                        slovoNode.Nodes.Add("Род: " + myNoun.rod.ToString());
                        slovoNode.Nodes.Add("Число: " + myNoun.chislo.ToString());
                        slovoNode.Nodes.Add("Падеж: " + myNoun.padezh.ToString());
                    }

                    /*
            Prilagatelnoe prilag= new Prilagatelnoe();
            prilag.chislo = slovo.chislo;
            prilag.padezh = slovo.padezh;
            prilag.rod = slovo.rod;
            prilag.osnova = slovo.eSlovo;
            prilag.english = slovo.enSlovo.slovo;
            slovo.ExtraData = prilag;  
                     */
                    if (curSlovo.chastRechi == ChastRechi.Prilagatelnoe)
                    {
                        Prilagatelnoe prilag = (Prilagatelnoe)curSlovo.ExtraData;
                        slovoNode.Nodes.Add("Часть речи: Прилагательное");
                        slovoNode.Nodes.Add("Основа: " + prilag.osnova.ToString());
                        slovoNode.Nodes.Add("Перевод: " + prilag.english.ToString());
                        slovoNode.Nodes.Add("Род: " + prilag.rod.ToString());
                        slovoNode.Nodes.Add("Число: " + prilag.chislo.ToString());
                        slovoNode.Nodes.Add("Падеж: " + prilag.padezh.ToString());
                        slovoNode.Nodes.Add("Cтепень сравнения: " + prilag.stepenSravneniya.ToString());
                    }
                    if (curSlovo.chastRechi == ChastRechi.Predlog)
                    {
                        Predlog myPredlog = (Predlog)curSlovo.ExtraData;
                        slovoNode.Nodes.Add("Часть речи: Предлог");
                        slovoNode.Nodes.Add("Перевод: " + myPredlog.english.ToString());
                      
                    }

                    if (curSlovo.chastRechi == ChastRechi.Glagol)
                    {
                        Glagol myGlagol = (Glagol)curSlovo.ExtraData;
                        slovoNode.Nodes.Add("Часть речи: Глагол");
                        slovoNode.Nodes.Add("Перевод: " + myGlagol.english);
                        slovoNode.Nodes.Add("Время: " + myGlagol.vremya.ToString());
                        slovoNode.Nodes.Add("Залог: " + myGlagol.zalog.ToString());
                        slovoNode.Nodes.Add("Наклонение: " + myGlagol.naklonenie.ToString());
                        slovoNode.Nodes.Add("Вид: " + myGlagol.vid.ToString());
                        slovoNode.Nodes.Add("Состояние: " + myGlagol.sostoyanie.ToString());
                    }

                    if (curSlovo.chastRechi == ChastRechi.Mestoimenie)
                    {
                        Mestoimenie myMest = (Mestoimenie)curSlovo.ExtraData;
                        slovoNode.Nodes.Add("Часть речи: Местоимение");
                        slovoNode.Nodes.Add("Перевод: " + myMest.english);
                    }
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
