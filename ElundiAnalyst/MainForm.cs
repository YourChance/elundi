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
            analysedText.Text = "";
            ETEnTranslator.ETEnCore core = new ETEnTranslator.ETEnCore();
            ArrayList wordList = core.Analyze(elundiText.Text);
            for (int i = 0; i < wordList.Count; i++)
            {
                Predlozhenie curPred = (Predlozhenie)wordList[i];
                for (int j = 0; j < curPred.Count; j++)
                {
                    Slovo curSlovo = curPred[j];
                    if (curSlovo.chastRechi == ChastRechi.Suschestvitelnoe)
                    {
                        analysedText.AppendText(((Noun)curSlovo.ExtraData).ToString());
                    }
                }
            }
        }
    }
}
