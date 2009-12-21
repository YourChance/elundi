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
                    slovoNode.NodeFont = new Font("ELUNDI1", 8.25f);

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

        private void NormalizeBtn_Click(object sender, EventArgs e)
        {
            ETEnTranslator.ETEnCore core = new ETEnTranslator.ETEnCore();
            ArrayList wordList = core.GetMorphology(elundiText.Text);
            string resultText = "";
            for (int i = 0; i < wordList.Count; i++)
            {
                Predlozhenie curPred = (Predlozhenie)wordList[i];
                /**
                 * Что считать нормальным порядком слов в предложении?
                 * Подлежащее сказуемое а дальше.. хз что дальше..
                 * Хотя для английского наиболее важны только подлежаще и сказуемое.
                 * Лан. Забиваем на остальные части речи пока
                 */
                /**
                 * Ищем подлежащее и сказуемое
                 * Что считать подлежащим?
                 * Первое найденное существительное или местоимение, перед которым не стоит предлог
                 * Если перед существительным стоит прилагательное (определяющее слово), то переносим
                 * его вместе с подлежащим в начало
                 * Что считать сказуемым?
                 * Ближайший к подлежащему глагол
                 */
                Slovo predSlovo = null;
                Slovo podlejashee = null;
                int podlejashee_index = -1;
                Slovo skazuemoe = null;
                int skazuemoe_index = -1;
                Slovo adjective = null;
                int adjective_index = -1;
                for (int j = 0; j < curPred.Count; j++)
                {
                    Slovo curSlovo = curPred[j];
                    if (podlejashee_index < 0 && (curSlovo.chastRechi == ChastRechi.Suschestvitelnoe || curSlovo.chastRechi == ChastRechi.Mestoimenie) &&
                        (predSlovo == null || predSlovo.chastRechi != ChastRechi.Predlog))
                    {
                        podlejashee = curSlovo;
                        podlejashee_index = j;
                        if (predSlovo != null && predSlovo.chastRechi == ChastRechi.Prilagatelnoe)
                        {
                            adjective = predSlovo;
                            adjective_index = j - 1;
                        }
                    }
                    if (curSlovo.chastRechi == ChastRechi.Glagol)
                    {
                        skazuemoe = curSlovo;
                        skazuemoe_index = j;
                        if (podlejashee_index >= 0)
                            break; //мы все нашли
                    }
                    predSlovo = curSlovo;
                }
                int hasAdjective = adjective_index >= 0 ? 1 : 0;
                if (adjective_index >= 0 && curPred.Count > 1)
                {
                    for (int t = adjective_index; t > 0; t--)
                        curPred[t] = curPred[t - 1];
                    curPred[0] = adjective;
                }
                if (podlejashee_index >= 0 && curPred.Count > 1 + hasAdjective)
                {
                    for (int t = podlejashee_index; t > hasAdjective; t--)
                        curPred[t] = curPred[t - 1];
                    curPred[hasAdjective] = podlejashee;
                }
                if (skazuemoe_index >= 0 && curPred.Count > 2 + hasAdjective)
                {
                    if (skazuemoe_index < podlejashee_index)
                        skazuemoe_index += 1 + hasAdjective;
                    for (int t = skazuemoe_index; t > 1 + hasAdjective; t--)
                        curPred[t] = curPred[t - 1];
                    curPred[hasAdjective + 1] = skazuemoe;
                }
                resultText += curPred.ToEString() + " ";
            }
            elundiText.Text = resultText.Trim();
        }
    }
}
