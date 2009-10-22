namespace ElundiAnalyst
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AnalysProgress = new System.Windows.Forms.ProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.StartAnalysisBtn = new System.Windows.Forms.Button();
            this.elundiText = new System.Windows.Forms.RichTextBox();
            this.analysedText = new System.Windows.Forms.RichTextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.elundiText);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.analysedText);
            this.splitContainer1.Size = new System.Drawing.Size(607, 395);
            this.splitContainer1.SplitterDistance = 202;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.AnalysProgress);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 170);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(607, 32);
            this.panel1.TabIndex = 1;
            // 
            // AnalysProgress
            // 
            this.AnalysProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnalysProgress.Location = new System.Drawing.Point(0, 0);
            this.AnalysProgress.Name = "AnalysProgress";
            this.AnalysProgress.Size = new System.Drawing.Size(453, 32);
            this.AnalysProgress.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.StartAnalysisBtn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(453, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(154, 32);
            this.panel2.TabIndex = 2;
            // 
            // StartAnalysisBtn
            // 
            this.StartAnalysisBtn.Location = new System.Drawing.Point(6, 0);
            this.StartAnalysisBtn.Name = "StartAnalysisBtn";
            this.StartAnalysisBtn.Size = new System.Drawing.Size(145, 32);
            this.StartAnalysisBtn.TabIndex = 0;
            this.StartAnalysisBtn.Text = "Начать анализ";
            this.StartAnalysisBtn.UseVisualStyleBackColor = true;
            this.StartAnalysisBtn.Click += new System.EventHandler(this.StartAnalysisBtn_Click);
            // 
            // elundiText
            // 
            this.elundiText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elundiText.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.elundiText.Location = new System.Drawing.Point(0, 0);
            this.elundiText.Name = "elundiText";
            this.elundiText.Size = new System.Drawing.Size(607, 170);
            this.elundiText.TabIndex = 0;
            this.elundiText.Text = " Q-RRQ FQV Q-IBS FQV A-ZJ FQV E-MKYM Q-ABO-E S-I FQV Q-MLYM.";
            // 
            // analysedText
            // 
            this.analysedText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analysedText.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.analysedText.Location = new System.Drawing.Point(0, 0);
            this.analysedText.Name = "analysedText";
            this.analysedText.ReadOnly = true;
            this.analysedText.Size = new System.Drawing.Size(607, 189);
            this.analysedText.TabIndex = 0;
            this.analysedText.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 395);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "Анализатор текста Эльюнди";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button StartAnalysisBtn;
        private System.Windows.Forms.RichTextBox elundiText;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ProgressBar AnalysProgress;
        private System.Windows.Forms.RichTextBox analysedText;
    }
}

