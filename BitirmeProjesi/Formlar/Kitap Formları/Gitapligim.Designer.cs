namespace BitirmeProjesi
{
    partial class Gitapligim
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
            this.btnGeri = new System.Windows.Forms.Button();
            this.lblSayfaAciklama = new System.Windows.Forms.Label();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.gbBegenilenler = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gbBegenilenler.SuspendLayout();
            this.SuspendLayout();
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
            // lblSayfaAciklama
            // 
            this.lblSayfaAciklama.AutoSize = true;
            this.lblSayfaAciklama.ForeColor = System.Drawing.Color.White;
            this.lblSayfaAciklama.Location = new System.Drawing.Point(167, 70);
            this.lblSayfaAciklama.Name = "lblSayfaAciklama";
            this.lblSayfaAciklama.Size = new System.Drawing.Size(261, 13);
            this.lblSayfaAciklama.TabIndex = 29;
            this.lblSayfaAciklama.Text = "Son beğenmiş olduğunuz kitaplar bu sayfada listelenir.";
            // 
            // lblBaslik
            // 
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblBaslik.ForeColor = System.Drawing.Color.White;
            this.lblBaslik.Location = new System.Drawing.Point(169, 24);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(255, 37);
            this.lblBaslik.TabIndex = 28;
            this.lblBaslik.Text = "Beğenilen Gitaplar";
            // 
            // gbBegenilenler
            // 
            this.gbBegenilenler.Controls.Add(this.label1);
            this.gbBegenilenler.ForeColor = System.Drawing.Color.White;
            this.gbBegenilenler.Location = new System.Drawing.Point(26, 104);
            this.gbBegenilenler.Margin = new System.Windows.Forms.Padding(2);
            this.gbBegenilenler.Name = "gbBegenilenler";
            this.gbBegenilenler.Padding = new System.Windows.Forms.Padding(2);
            this.gbBegenilenler.Size = new System.Drawing.Size(570, 54);
            this.gbBegenilenler.TabIndex = 49;
            this.gbBegenilenler.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Henüz bir gitap bulunmamakta";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Gitapligim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(620, 545);
            this.Controls.Add(this.gbBegenilenler);
            this.Controls.Add(this.btnGeri);
            this.Controls.Add(this.lblSayfaAciklama);
            this.Controls.Add(this.lblBaslik);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Gitapligim";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Gitapligim";
            this.Load += new System.EventHandler(this.Gitapligim_Load);
            this.gbBegenilenler.ResumeLayout(false);
            this.gbBegenilenler.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGeri;
        private System.Windows.Forms.Label lblSayfaAciklama;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.GroupBox gbBegenilenler;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
    }
}