namespace AddInGeometricNetwork
{
    partial class DockableWindow
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listBoxJunction = new System.Windows.Forms.ListBox();
            this.listBoxEdge = new System.Windows.Forms.ListBox();
            this.buttonTrace = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxZoom = new System.Windows.Forms.CheckBox();
            this.listBoxJunctionFinale = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxJunctionIniziale = new System.Windows.Forms.ListBox();
            this.listBoxTutteJunction = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Tab1 = new System.Windows.Forms.TabControl();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Tab1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.listBoxJunction);
            this.tabPage2.Controls.Add(this.listBoxEdge);
            this.tabPage2.Controls.Add(this.buttonTrace);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(602, 569);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Calcolo Trace";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Khaki;
            this.button4.Location = new System.Drawing.Point(130, 488);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(130, 34);
            this.button4.TabIndex = 5;
            this.button4.Text = "Pulisci";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.label5.Location = new System.Drawing.Point(14, 294);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "EID Simple Junction";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "EID Edge Lineari";
            // 
            // listBoxJunction
            // 
            this.listBoxJunction.FormattingEnabled = true;
            this.listBoxJunction.ItemHeight = 16;
            this.listBoxJunction.Location = new System.Drawing.Point(17, 327);
            this.listBoxJunction.Name = "listBoxJunction";
            this.listBoxJunction.Size = new System.Drawing.Size(324, 132);
            this.listBoxJunction.TabIndex = 2;
            // 
            // listBoxEdge
            // 
            this.listBoxEdge.FormattingEnabled = true;
            this.listBoxEdge.ItemHeight = 16;
            this.listBoxEdge.Location = new System.Drawing.Point(17, 155);
            this.listBoxEdge.Name = "listBoxEdge";
            this.listBoxEdge.Size = new System.Drawing.Size(324, 100);
            this.listBoxEdge.TabIndex = 1;
            // 
            // buttonTrace
            // 
            this.buttonTrace.BackColor = System.Drawing.Color.SandyBrown;
            this.buttonTrace.Location = new System.Drawing.Point(111, 27);
            this.buttonTrace.Name = "buttonTrace";
            this.buttonTrace.Size = new System.Drawing.Size(184, 48);
            this.buttonTrace.TabIndex = 0;
            this.buttonTrace.Text = "Calcola Trace";
            this.buttonTrace.UseVisualStyleBackColor = false;
            this.buttonTrace.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.listBoxTutteJunction);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(602, 569);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Analisi Junction";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.PaleTurquoise;
            this.button3.Location = new System.Drawing.Point(25, 481);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(144, 32);
            this.button3.TabIndex = 5;
            this.button3.Text = "Rimuovi Graphics";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkBoxZoom);
            this.groupBox1.Controls.Add(this.listBoxJunctionFinale);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.listBoxJunctionIniziale);
            this.groupBox1.Location = new System.Drawing.Point(6, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(518, 236);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Analisi Per Simple / Complex Edge";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Junction Finale";
            // 
            // checkBoxZoom
            // 
            this.checkBoxZoom.AutoSize = true;
            this.checkBoxZoom.Location = new System.Drawing.Point(265, 45);
            this.checkBoxZoom.Name = "checkBoxZoom";
            this.checkBoxZoom.Size = new System.Drawing.Size(169, 21);
            this.checkBoxZoom.TabIndex = 8;
            this.checkBoxZoom.Text = "zooma su tratta scelta";
            this.checkBoxZoom.UseVisualStyleBackColor = true;
            this.checkBoxZoom.CheckedChanged += new System.EventHandler(this.checkBoxZoom_CheckedChanged);
            // 
            // listBoxJunctionFinale
            // 
            this.listBoxJunctionFinale.FormattingEnabled = true;
            this.listBoxJunctionFinale.ItemHeight = 16;
            this.listBoxJunctionFinale.Location = new System.Drawing.Point(0, 188);
            this.listBoxJunctionFinale.Name = "listBoxJunctionFinale";
            this.listBoxJunctionFinale.Size = new System.Drawing.Size(434, 36);
            this.listBoxJunctionFinale.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Junction Iniziale";
            // 
            // listBoxJunctionIniziale
            // 
            this.listBoxJunctionIniziale.FormattingEnabled = true;
            this.listBoxJunctionIniziale.ItemHeight = 16;
            this.listBoxJunctionIniziale.Location = new System.Drawing.Point(0, 85);
            this.listBoxJunctionIniziale.Name = "listBoxJunctionIniziale";
            this.listBoxJunctionIniziale.Size = new System.Drawing.Size(434, 36);
            this.listBoxJunctionIniziale.TabIndex = 1;
            // 
            // listBoxTutteJunction
            // 
            this.listBoxTutteJunction.FormattingEnabled = true;
            this.listBoxTutteJunction.ItemHeight = 16;
            this.listBoxTutteJunction.Location = new System.Drawing.Point(6, 299);
            this.listBoxTutteJunction.Name = "listBoxTutteJunction";
            this.listBoxTutteJunction.Size = new System.Drawing.Size(434, 148);
            this.listBoxTutteJunction.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.RosyBrown;
            this.button1.Location = new System.Drawing.Point(214, 477);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Pulisci";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Junction Connesse";
            // 
            // Tab1
            // 
            this.Tab1.Controls.Add(this.tabPage1);
            this.Tab1.Controls.Add(this.tabPage2);
            this.Tab1.Location = new System.Drawing.Point(12, 3);
            this.Tab1.Name = "Tab1";
            this.Tab1.SelectedIndex = 0;
            this.Tab1.Size = new System.Drawing.Size(610, 598);
            this.Tab1.TabIndex = 0;
            // 
            // DockableWindow
            // 
            this.Controls.Add(this.Tab1);
            this.Name = "DockableWindow";
            this.Size = new System.Drawing.Size(625, 601);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Tab1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxJunctionFinale;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxJunctionIniziale;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl Tab1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxTutteJunction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonTrace;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBoxJunction;
        private System.Windows.Forms.ListBox listBoxEdge;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox checkBoxZoom;
    }
}
