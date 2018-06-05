using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Runtime.InteropServices;

namespace MX100VCS
{
	/// <summary>
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button_Start;
		private System.Windows.Forms.Button button_Close;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button_Stop;
		private System.Windows.Forms.PictureBox pictureBox1;
		/// <summary>
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label2;

		private DrawWaveMX100 daq = null;

		public Form1()
		{
			//
			//
			InitializeComponent();

			//
			//
			daq = new DrawWaveMX100();
		}

		/// <summary>
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// </summary>
		private void InitializeComponent()
		{
            this.button_Start = new System.Windows.Forms.Button();
            this.button_Close = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Stop = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(624, 84);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(125, 28);
            this.button_Start.TabIndex = 0;
            this.button_Start.Text = "Start";
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(624, 345);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(125, 28);
            this.button_Close.TabIndex = 1;
            this.button_Close.Text = "Close";
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(624, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "192.168.1.12";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(624, 299);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 28);
            this.label1.TabIndex = 3;
            this.label1.Text = "0.0000";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_Stop
            // 
            this.button_Stop.Location = new System.Drawing.Point(624, 121);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(125, 28);
            this.button_Stop.TabIndex = 4;
            this.button_Stop.Text = "Stop";
            this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(10, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 350);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(624, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Host";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(797, 388);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_Stop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.button_Start);
            this.Name = "Form1";
            this.Text = "MX100 Sample (C#)";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void button_Start_Click(object sender, System.EventArgs e)
		{
			daq.RunDraw(textBox1, pictureBox1, label1);
		}

		private void button_Close_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void button_Stop_Click(object sender, System.EventArgs e)
		{
			daq.StopDraw();		
		}
	}
}

