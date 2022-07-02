namespace BitirmeProjesi
{
    partial class GitapYorumlari
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
            this.components = new System.ComponentModel.Container();
            this.gbYorumlar = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbYorumYap = new System.Windows.Forms.GroupBox();
            this.txtYorum = new System.Windows.Forms.TextBox();
            this.btnYorumPaylas = new System.Windows.Forms.Button();
            this.btnGeri = new System.Windows.Forms.Button();
            this.lblAciklama = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gbYorumlar.SuspendLayout();
            this.gbYorumYap.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbYorumlar
            // 
            this.gbYorumlar.Controls.Add(this.label4);
            this.gbYorumlar.ForeColor = System.Drawing.Color.White;
            this.gbYorumlar.Location = new System.Drawing.Point(25, 259);
            this.gbYorumlar.Margin = new System.Windows.Forms.Padding(2);
            this.gbYorumlar.Name = "gbYorumlar";
            this.gbYorumlar.Padding = new System.Windows.Forms.Padding(2);
            this.gbYorumlar.Size = new System.Drawing.Size(570, 54);
            this.gbYorumlar.TabIndex = 48;
            this.gbYorumlar.TabStop = false;
            this.gbYorumlar.Text = "Yorumlar";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(17, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Henüz bir yorum bulunmamakta";
            // 
            // gbYorumYap
            // 
            this.gbYorumYap.Controls.Add(this.txtYorum);
            this.gbYorumYap.Controls.Add(this.btnYorumPaylas);
            this.gbYorumYap.ForeColor = System.Drawing.Color.White;
            this.gbYorumYap.Location = new System.Drawing.Point(25, 107);
            this.gbYorumYap.Margin = new System.Windows.Forms.Padding(2);
            this.gbYorumYap.Name = "gbYorumYap";
            this.gbYorumYap.Padding = new System.Windows.Forms.Padding(2);
            this.gbYorumYap.Size = new System.Drawing.Size(570, 148);
            this.gbYorumYap.TabIndex = 49;
            this.gbYorumYap.TabStop = false;
            this.gbYorumYap.Text = "Yorum Yap";
            // 
            // txtYorum
            // 
            this.txtYorum.BackColor = System.Drawing.Color.Black;
            this.txtYorum.ForeColor = System.Drawing.Color.White;
            this.txtYorum.Location = new System.Drawing.Point(16, 29);
            this.txtYorum.Multiline = true;
            this.txtYorum.Name = "txtYorum";
            this.txtYorum.Size = new System.Drawing.Size(536, 77);
            this.txtYorum.TabIndex = 51;
            // 
            // btnYorumPaylas
            // 
            this.btnYorumPaylas.BackColor = System.Drawing.Color.Black;
            this.btnYorumPaylas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYorumPaylas.ForeColor = System.Drawing.Color.White;
            this.btnYorumPaylas.Location = new System.Drawing.Point(493, 112);
            this.btnYorumPaylas.Name = "btnYorumPaylas";
            this.btnYorumPaylas.Size = new System.Drawing.Size(59, 23);
            this.btnYorumPaylas.TabIndex = 53;
            this.btnYorumPaylas.Text = "Paylaş";
            this.btnYorumPaylas.UseVisualStyleBackColor = false;
            this.btnYorumPaylas.Click += new System.EventHandler(this.btnYorumPaylas_Click);
            // 
            // btnGeri
            // 
            this.btnGeri.BackColor = System.Drawing.Color.Black;
            this.btnGeri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeri.ForeColor = System.Drawing.Color.White;
            this.btnGeri.Location = new System.Drawing.Point(11, 11);
            this.btnGeri.Margin = new System.Windows.Forms.Padding(2);
            this.btnGeri.Name = "btnGeri";
            this.btnGeri.Size = new System.Drawing.Size(27, 23);
            this.btnGeri.TabIndex = 30;
            this.btnGeri.Text = "<-";
            this.btnGeri.UseVisualStyleBackColor = false;
            this.btnGeri.Click += new System.EventHandler(this.btnGeri_Click);
            // 
            // lblAciklama
            // 
            this.lblAciklama.AutoSize = true;
            this.lblAciklama.ForeColor = System.Drawing.Color.White;
            this.lblAciklama.Location = new System.Drawing.Point(256, 79);
            this.lblAciklama.Name = "lblAciklama";
            this.lblAciklama.Size = new System.Drawing.Size(92, 13);
            this.lblAciklama.TabIndex = 51;
            this.lblAciklama.Text = "Gitabının yorumları";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(280, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 37);
            this.label1.TabIndex = 50;
            this.label1.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GitapYorumlari
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(620, 633);
            this.Controls.Add(this.lblAciklama);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGeri);
            this.Controls.Add(this.gbYorumlar);
            this.Controls.Add(this.gbYorumYap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GitapYorumlari";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "GitapYorumlari";
            this.Load += new System.EventHandler(this.GitapYorumlari_Load);
            this.gbYorumlar.ResumeLayout(false);
            this.gbYorumlar.PerformLayout();
            this.gbYorumYap.ResumeLayout(false);
            this.gbYorumYap.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbYorumlar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbYorumYap;
        private System.Windows.Forms.TextBox txtYorum;
        private System.Windows.Forms.Button btnYorumPaylas;
        private System.Windows.Forms.Button btnGeri;
        private System.Windows.Forms.Label lblAciklama;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
    }
}