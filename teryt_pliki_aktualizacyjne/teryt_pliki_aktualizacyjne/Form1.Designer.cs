namespace teryt_pliki_aktualizacyjne
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDataOd = new System.Windows.Forms.MaskedTextBox();
            this.txtDataDo = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(185, 15);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 63);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Data Od\r\n(YYYY-MM-DD)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 26);
            this.label2.TabIndex = 4;
            this.label2.Text = "Data Do\r\n(YYYY-MM-DD)";
            // 
            // txtDataOd
            // 
            this.txtDataOd.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDataOd.Location = new System.Drawing.Point(105, 15);
            this.txtDataOd.Mask = "0000-00-00";
            this.txtDataOd.Name = "txtDataOd";
            this.txtDataOd.Size = new System.Drawing.Size(74, 20);
            this.txtDataOd.TabIndex = 5;
            // 
            // txtDataDo
            // 
            this.txtDataDo.Location = new System.Drawing.Point(105, 52);
            this.txtDataDo.Mask = "0000-00-00";
            this.txtDataDo.Name = "txtDataDo";
            this.txtDataDo.Size = new System.Drawing.Size(74, 20);
            this.txtDataDo.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 94);
            this.Controls.Add(this.txtDataDo);
            this.Controls.Add(this.txtDataOd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox txtDataOd;
        private System.Windows.Forms.MaskedTextBox txtDataDo;
    }
}

