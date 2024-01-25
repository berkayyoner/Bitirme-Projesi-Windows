namespace BitirmeProjesi.Formlar.Kullanıcı_Formları
{
	partial class AktifSurem
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
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.lblHosgeldin = new System.Windows.Forms.Label();
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
			this.btnGeri.TabIndex = 49;
			this.btnGeri.Text = "<-";
			this.btnGeri.UseVisualStyleBackColor = false;
			this.btnGeri.Click += new System.EventHandler(this.btnGeri_Click);
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// lblHosgeldin
			// 
			this.lblHosgeldin.AutoSize = true;
			this.lblHosgeldin.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
			this.lblHosgeldin.ForeColor = System.Drawing.Color.White;
			this.lblHosgeldin.Location = new System.Drawing.Point(209, 20);
			this.lblHosgeldin.Name = "lblHosgeldin";
			this.lblHosgeldin.Size = new System.Drawing.Size(155, 37);
			this.lblHosgeldin.TabIndex = 50;
			this.lblHosgeldin.Text = "Hoşgeldin,";
			// 
			// AktifSurem
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.ClientSize = new System.Drawing.Size(620, 512);
			this.Controls.Add(this.lblHosgeldin);
			this.Controls.Add(this.btnGeri);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AktifSurem";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "AktifSurem";
			this.Load += new System.EventHandler(this.AktifSurem_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnGeri;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label lblHosgeldin;
	}
}