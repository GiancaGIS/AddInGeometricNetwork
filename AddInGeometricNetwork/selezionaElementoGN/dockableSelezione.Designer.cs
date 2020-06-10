namespace AddInGeometricNetwork
{
    partial class dockableSelezione
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
            this.tabPage1ComplexEdge = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxSceltaDownload = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonSvuotaSimpleJunction = new System.Windows.Forms.Button();
            this.buttonSvuotaSimpleEdge = new System.Windows.Forms.Button();
            this.listViewSimpleJunction = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewSimpleEdge = new System.Windows.Forms.ListView();
            this.OID_SimpleEdge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FID_SimpleEdge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EID_SimpleEdge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxNomeFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.listViewComplexEdge = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonSvuotaComplexEdge = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage1ComplexEdge.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1ComplexEdge
            // 
            this.tabPage1ComplexEdge.AutoScroll = true;
            this.tabPage1ComplexEdge.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage1ComplexEdge.Controls.Add(this.label1);
            this.tabPage1ComplexEdge.Controls.Add(this.comboBoxSceltaDownload);
            this.tabPage1ComplexEdge.Controls.Add(this.label6);
            this.tabPage1ComplexEdge.Controls.Add(this.label5);
            this.tabPage1ComplexEdge.Controls.Add(this.buttonSvuotaSimpleJunction);
            this.tabPage1ComplexEdge.Controls.Add(this.buttonSvuotaSimpleEdge);
            this.tabPage1ComplexEdge.Controls.Add(this.listViewSimpleJunction);
            this.tabPage1ComplexEdge.Controls.Add(this.listViewSimpleEdge);
            this.tabPage1ComplexEdge.Controls.Add(this.textBoxNomeFile);
            this.tabPage1ComplexEdge.Controls.Add(this.label4);
            this.tabPage1ComplexEdge.Controls.Add(this.label2);
            this.tabPage1ComplexEdge.Controls.Add(this.button1);
            this.tabPage1ComplexEdge.Controls.Add(this.listViewComplexEdge);
            this.tabPage1ComplexEdge.Controls.Add(this.buttonSvuotaComplexEdge);
            this.tabPage1ComplexEdge.Location = new System.Drawing.Point(4, 25);
            this.tabPage1ComplexEdge.Name = "tabPage1ComplexEdge";
            this.tabPage1ComplexEdge.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1ComplexEdge.Size = new System.Drawing.Size(1916, 543);
            this.tabPage1ComplexEdge.TabIndex = 0;
            this.tabPage1ComplexEdge.Text = "Analisi oggetti Geometric Network";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(917, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 17);
            this.label1.TabIndex = 19;
            this.label1.Text = "Seleziona Tabella da esportare in CSV";
            // 
            // comboBoxSceltaDownload
            // 
            this.comboBoxSceltaDownload.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxSceltaDownload.FormattingEnabled = true;
            this.comboBoxSceltaDownload.Items.AddRange(new object[] {
            "Complex Edge",
            "Simple Edge",
            "Simple Junction"});
            this.comboBoxSceltaDownload.Location = new System.Drawing.Point(1212, 307);
            this.comboBoxSceltaDownload.Name = "comboBoxSceltaDownload";
            this.comboBoxSceltaDownload.Size = new System.Drawing.Size(185, 24);
            this.comboBoxSceltaDownload.TabIndex = 18;
            this.comboBoxSceltaDownload.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(916, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Simple Junction";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(6, 281);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Simple Edge";
            // 
            // buttonSvuotaSimpleJunction
            // 
            this.buttonSvuotaSimpleJunction.BackColor = System.Drawing.Color.Sienna;
            this.buttonSvuotaSimpleJunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSvuotaSimpleJunction.Location = new System.Drawing.Point(919, 230);
            this.buttonSvuotaSimpleJunction.Name = "buttonSvuotaSimpleJunction";
            this.buttonSvuotaSimpleJunction.Size = new System.Drawing.Size(229, 35);
            this.buttonSvuotaSimpleJunction.TabIndex = 15;
            this.buttonSvuotaSimpleJunction.Text = "Svuota tutto";
            this.buttonSvuotaSimpleJunction.UseVisualStyleBackColor = false;
            this.buttonSvuotaSimpleJunction.Click += new System.EventHandler(this.buttonSvuotaSimpleJunction_Click_1);
            // 
            // buttonSvuotaSimpleEdge
            // 
            this.buttonSvuotaSimpleEdge.BackColor = System.Drawing.Color.Sienna;
            this.buttonSvuotaSimpleEdge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSvuotaSimpleEdge.Location = new System.Drawing.Point(6, 494);
            this.buttonSvuotaSimpleEdge.Name = "buttonSvuotaSimpleEdge";
            this.buttonSvuotaSimpleEdge.Size = new System.Drawing.Size(229, 35);
            this.buttonSvuotaSimpleEdge.TabIndex = 14;
            this.buttonSvuotaSimpleEdge.Text = "Svuota tutto";
            this.buttonSvuotaSimpleEdge.UseVisualStyleBackColor = false;
            this.buttonSvuotaSimpleEdge.Click += new System.EventHandler(this.buttonSvuotaSimpleEdge_Click_1);
            // 
            // listViewSimpleJunction
            // 
            this.listViewSimpleJunction.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.listViewSimpleJunction.FullRowSelect = true;
            this.listViewSimpleJunction.GridLines = true;
            this.listViewSimpleJunction.HideSelection = false;
            this.listViewSimpleJunction.Location = new System.Drawing.Point(919, 46);
            this.listViewSimpleJunction.MultiSelect = false;
            this.listViewSimpleJunction.Name = "listViewSimpleJunction";
            this.listViewSimpleJunction.Size = new System.Drawing.Size(905, 178);
            this.listViewSimpleJunction.TabIndex = 13;
            this.listViewSimpleJunction.UseCompatibleStateImageBehavior = false;
            this.listViewSimpleJunction.View = System.Windows.Forms.View.Details;
            this.listViewSimpleJunction.SelectedIndexChanged += new System.EventHandler(this.listViewSimpleJunction_SelectedIndexChanged_1);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ObjectID";
            this.columnHeader1.Width = 125;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Feature Class ID";
            this.columnHeader2.Width = 125;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Element ID - Geometric Network";
            this.columnHeader3.Width = 250;
            // 
            // listViewSimpleEdge
            // 
            this.listViewSimpleEdge.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.OID_SimpleEdge,
            this.FID_SimpleEdge,
            this.EID_SimpleEdge,
            this.columnHeader10,
            this.columnHeader11});
            this.listViewSimpleEdge.FullRowSelect = true;
            this.listViewSimpleEdge.GridLines = true;
            this.listViewSimpleEdge.HideSelection = false;
            this.listViewSimpleEdge.Location = new System.Drawing.Point(6, 310);
            this.listViewSimpleEdge.MultiSelect = false;
            this.listViewSimpleEdge.Name = "listViewSimpleEdge";
            this.listViewSimpleEdge.Size = new System.Drawing.Size(805, 178);
            this.listViewSimpleEdge.TabIndex = 12;
            this.listViewSimpleEdge.UseCompatibleStateImageBehavior = false;
            this.listViewSimpleEdge.View = System.Windows.Forms.View.Details;
            this.listViewSimpleEdge.SelectedIndexChanged += new System.EventHandler(this.listViewSimpleEdge_SelectedIndexChanged_1);
            // 
            // OID_SimpleEdge
            // 
            this.OID_SimpleEdge.Text = "ObjectID";
            this.OID_SimpleEdge.Width = 125;
            // 
            // FID_SimpleEdge
            // 
            this.FID_SimpleEdge.Text = "Feature Class ID";
            this.FID_SimpleEdge.Width = 125;
            // 
            // EID_SimpleEdge
            // 
            this.EID_SimpleEdge.Text = "Element ID - Geometric Network";
            this.EID_SimpleEdge.Width = 250;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "OID Junction Iniziale";
            this.columnHeader10.Width = 150;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "OID Junction Finale";
            this.columnHeader11.Width = 150;
            // 
            // textBoxNomeFile
            // 
            this.textBoxNomeFile.Location = new System.Drawing.Point(1213, 352);
            this.textBoxNomeFile.Name = "textBoxNomeFile";
            this.textBoxNomeFile.Size = new System.Drawing.Size(184, 22);
            this.textBoxNomeFile.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(6, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Complex Edge";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(917, 357);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Scegliere nome file CSV:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MediumAquamarine;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1213, 419);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(185, 62);
            this.button1.TabIndex = 8;
            this.button1.Text = "Download CSV";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listViewComplexEdge
            // 
            this.listViewComplexEdge.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader6,
            this.columnHeader8,
            this.columnHeader9});
            this.listViewComplexEdge.FullRowSelect = true;
            this.listViewComplexEdge.GridLines = true;
            this.listViewComplexEdge.HideSelection = false;
            this.listViewComplexEdge.Location = new System.Drawing.Point(6, 46);
            this.listViewComplexEdge.MultiSelect = false;
            this.listViewComplexEdge.Name = "listViewComplexEdge";
            this.listViewComplexEdge.Size = new System.Drawing.Size(867, 178);
            this.listViewComplexEdge.TabIndex = 5;
            this.listViewComplexEdge.UseCompatibleStateImageBehavior = false;
            this.listViewComplexEdge.View = System.Windows.Forms.View.Details;
            this.listViewComplexEdge.SelectedIndexChanged += new System.EventHandler(this.listViewComplexEdge_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ObjectID";
            this.columnHeader4.Width = 125;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Feature Class ID";
            this.columnHeader5.Width = 125;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Sub - ID";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Element ID - Geometric Network";
            this.columnHeader6.Width = 250;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "OID Junction Iniziale";
            this.columnHeader8.Width = 150;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "OID Junction Finale";
            this.columnHeader9.Width = 150;
            // 
            // buttonSvuotaComplexEdge
            // 
            this.buttonSvuotaComplexEdge.BackColor = System.Drawing.Color.Sienna;
            this.buttonSvuotaComplexEdge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSvuotaComplexEdge.Location = new System.Drawing.Point(6, 230);
            this.buttonSvuotaComplexEdge.Name = "buttonSvuotaComplexEdge";
            this.buttonSvuotaComplexEdge.Size = new System.Drawing.Size(229, 35);
            this.buttonSvuotaComplexEdge.TabIndex = 1;
            this.buttonSvuotaComplexEdge.Text = "Svuota tutto";
            this.buttonSvuotaComplexEdge.UseVisualStyleBackColor = false;
            this.buttonSvuotaComplexEdge.Click += new System.EventHandler(this.buttonSvuotaComplexEdge_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage1ComplexEdge);
            this.tabControl.Location = new System.Drawing.Point(16, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1924, 572);
            this.tabControl.TabIndex = 2;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Coord X";
            this.columnHeader12.Width = 100;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Coord Y";
            this.columnHeader13.Width = 100;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Longitudine";
            this.columnHeader14.Width = 100;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Latitudine";
            this.columnHeader15.Width = 100;
            // 
            // dockableSelezione
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.Controls.Add(this.tabControl);
            this.Name = "dockableSelezione";
            this.Size = new System.Drawing.Size(1962, 606);
            this.tabPage1ComplexEdge.ResumeLayout(false);
            this.tabPage1ComplexEdge.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1ComplexEdge;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSvuotaSimpleJunction;
        private System.Windows.Forms.Button buttonSvuotaSimpleEdge;
        private System.Windows.Forms.ListView listViewSimpleJunction;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView listViewSimpleEdge;
        private System.Windows.Forms.ColumnHeader OID_SimpleEdge;
        private System.Windows.Forms.ColumnHeader FID_SimpleEdge;
        private System.Windows.Forms.ColumnHeader EID_SimpleEdge;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.TextBox textBoxNomeFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listViewComplexEdge;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Button buttonSvuotaComplexEdge;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxSceltaDownload;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
    }
}
