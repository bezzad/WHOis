namespace WHOis
{
    sealed partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.btnLookUp = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAccecptDigitLetteral = new System.Windows.Forms.CheckBox();
            this.btnReTryErrorCells = new System.Windows.Forms.Button();
            this.txtHostName = new System.Windows.Forms.RichTextBox();
            this.lblNamesCounter = new System.Windows.Forms.Label();
            this.btnPreCompile = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grbExtensions = new System.Windows.Forms.GroupBox();
            this.chkCc = new System.Windows.Forms.CheckBox();
            this.chkCom = new System.Windows.Forms.CheckBox();
            this.chkInfo = new System.Windows.Forms.CheckBox();
            this.chkIr = new System.Windows.Forms.CheckBox();
            this.chkNet = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.progResult = new System.Windows.Forms.ProgressBar();
            this.grbResult = new System.Windows.Forms.GroupBox();
            this.lblProcessPercent = new System.Windows.Forms.Label();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.colDomain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.grbExtensions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grbResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Host Names:";
            // 
            // cmbServer
            // 
            this.cmbServer.FormattingEnabled = true;
            this.cmbServer.Items.AddRange(new object[] {
            "whois.internic.net",
            "whois.ripe.net",
            "whois.arin.net",
            "whois.nic.ir"});
            this.cmbServer.Location = new System.Drawing.Point(53, 22);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(204, 21);
            this.cmbServer.TabIndex = 3;
            // 
            // btnLookUp
            // 
            this.btnLookUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLookUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLookUp.Location = new System.Drawing.Point(188, 384);
            this.btnLookUp.Name = "btnLookUp";
            this.btnLookUp.Size = new System.Drawing.Size(138, 27);
            this.btnLookUp.TabIndex = 5;
            this.btnLookUp.Text = "Look Up";
            this.btnLookUp.UseVisualStyleBackColor = true;
            this.btnLookUp.Click += new System.EventHandler(this.btnLookUp_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.chkAccecptDigitLetteral);
            this.groupBox1.Controls.Add(this.btnReTryErrorCells);
            this.groupBox1.Controls.Add(this.txtHostName);
            this.groupBox1.Controls.Add(this.lblNamesCounter);
            this.groupBox1.Controls.Add(this.btnPreCompile);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.grbExtensions);
            this.groupBox1.Controls.Add(this.cmbServer);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnLookUp);
            this.groupBox1.Location = new System.Drawing.Point(12, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 418);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "I Want To Know About";
            // 
            // chkAccecptDigitLetteral
            // 
            this.chkAccecptDigitLetteral.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAccecptDigitLetteral.AutoSize = true;
            this.chkAccecptDigitLetteral.Checked = true;
            this.chkAccecptDigitLetteral.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAccecptDigitLetteral.Location = new System.Drawing.Point(9, 353);
            this.chkAccecptDigitLetteral.Name = "chkAccecptDigitLetteral";
            this.chkAccecptDigitLetteral.Size = new System.Drawing.Size(122, 17);
            this.chkAccecptDigitLetteral.TabIndex = 13;
            this.chkAccecptDigitLetteral.Text = "Accept Digit Letteral";
            this.chkAccecptDigitLetteral.UseVisualStyleBackColor = true;
            this.chkAccecptDigitLetteral.CheckedChanged += new System.EventHandler(this.chkAccecptDigitLetteral_CheckedChanged);
            // 
            // btnReTryErrorCells
            // 
            this.btnReTryErrorCells.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReTryErrorCells.Location = new System.Drawing.Point(188, 312);
            this.btnReTryErrorCells.Name = "btnReTryErrorCells";
            this.btnReTryErrorCells.Size = new System.Drawing.Size(117, 27);
            this.btnReTryErrorCells.TabIndex = 20;
            this.btnReTryErrorCells.Text = "Retry Error Cells";
            this.btnReTryErrorCells.UseVisualStyleBackColor = true;
            this.btnReTryErrorCells.Click += new System.EventHandler(this.btnReTryErrorCells_Click);
            // 
            // txtHostName
            // 
            this.txtHostName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHostName.Location = new System.Drawing.Point(5, 72);
            this.txtHostName.Margin = new System.Windows.Forms.Padding(2);
            this.txtHostName.Name = "txtHostName";
            this.txtHostName.Size = new System.Drawing.Size(179, 276);
            this.txtHostName.TabIndex = 19;
            this.txtHostName.Text = "";
            // 
            // lblNamesCounter
            // 
            this.lblNamesCounter.AutoSize = true;
            this.lblNamesCounter.ForeColor = System.Drawing.Color.Green;
            this.lblNamesCounter.Location = new System.Drawing.Point(80, 56);
            this.lblNamesCounter.Name = "lblNamesCounter";
            this.lblNamesCounter.Size = new System.Drawing.Size(13, 13);
            this.lblNamesCounter.TabIndex = 18;
            this.lblNamesCounter.Text = "0";
            // 
            // btnPreCompile
            // 
            this.btnPreCompile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreCompile.Location = new System.Drawing.Point(188, 279);
            this.btnPreCompile.Name = "btnPreCompile";
            this.btnPreCompile.Size = new System.Drawing.Size(117, 27);
            this.btnPreCompile.TabIndex = 17;
            this.btnPreCompile.Text = "Parse Host Names";
            this.btnPreCompile.UseVisualStyleBackColor = true;
            this.btnPreCompile.Click += new System.EventHandler(this.btnPreCompile_Click);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(188, 246);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(117, 27);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save WHOis Result";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(9, 384);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(138, 27);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grbExtensions
            // 
            this.grbExtensions.Controls.Add(this.chkCc);
            this.grbExtensions.Controls.Add(this.chkCom);
            this.grbExtensions.Controls.Add(this.chkInfo);
            this.grbExtensions.Controls.Add(this.chkIr);
            this.grbExtensions.Controls.Add(this.chkNet);
            this.grbExtensions.Location = new System.Drawing.Point(188, 72);
            this.grbExtensions.Name = "grbExtensions";
            this.grbExtensions.Size = new System.Drawing.Size(117, 168);
            this.grbExtensions.TabIndex = 14;
            this.grbExtensions.TabStop = false;
            this.grbExtensions.Text = "Domain Extension";
            // 
            // chkCc
            // 
            this.chkCc.AutoSize = true;
            this.chkCc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.chkCc.Location = new System.Drawing.Point(32, 132);
            this.chkCc.Name = "chkCc";
            this.chkCc.Size = new System.Drawing.Size(44, 20);
            this.chkCc.TabIndex = 13;
            this.chkCc.Text = ".cc";
            this.chkCc.UseVisualStyleBackColor = true;
            // 
            // chkCom
            // 
            this.chkCom.AutoSize = true;
            this.chkCom.Checked = true;
            this.chkCom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.chkCom.Location = new System.Drawing.Point(32, 28);
            this.chkCom.Name = "chkCom";
            this.chkCom.Size = new System.Drawing.Size(59, 20);
            this.chkCom.TabIndex = 9;
            this.chkCom.Text = ". com";
            this.chkCom.UseVisualStyleBackColor = true;
            // 
            // chkInfo
            // 
            this.chkInfo.AutoSize = true;
            this.chkInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.chkInfo.Location = new System.Drawing.Point(32, 106);
            this.chkInfo.Name = "chkInfo";
            this.chkInfo.Size = new System.Drawing.Size(51, 20);
            this.chkInfo.TabIndex = 12;
            this.chkInfo.Text = ".info";
            this.chkInfo.UseVisualStyleBackColor = true;
            // 
            // chkIr
            // 
            this.chkIr.AutoSize = true;
            this.chkIr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.chkIr.Location = new System.Drawing.Point(32, 54);
            this.chkIr.Name = "chkIr";
            this.chkIr.Size = new System.Drawing.Size(40, 20);
            this.chkIr.TabIndex = 10;
            this.chkIr.Text = ". ir";
            this.chkIr.UseVisualStyleBackColor = true;
            // 
            // chkNet
            // 
            this.chkNet.AutoSize = true;
            this.chkNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.chkNet.Location = new System.Drawing.Point(32, 80);
            this.chkNet.Name = "chkNet";
            this.chkNet.Size = new System.Drawing.Size(51, 20);
            this.chkNet.TabIndex = 11;
            this.chkNet.Text = ". net";
            this.chkNet.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WHOis.Properties.Resources._1377639655_Connected;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 68);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(98, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 42);
            this.label4.TabIndex = 10;
            this.label4.Text = "WHOis";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Green;
            this.label5.Location = new System.Drawing.Point(117, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Get Your Domain Info";
            // 
            // progResult
            // 
            this.progResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progResult.Location = new System.Drawing.Point(6, 468);
            this.progResult.Name = "progResult";
            this.progResult.Size = new System.Drawing.Size(434, 23);
            this.progResult.TabIndex = 7;
            // 
            // grbResult
            // 
            this.grbResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbResult.Controls.Add(this.lblProcessPercent);
            this.grbResult.Controls.Add(this.dgvResult);
            this.grbResult.Controls.Add(this.progResult);
            this.grbResult.Location = new System.Drawing.Point(350, 35);
            this.grbResult.Name = "grbResult";
            this.grbResult.Size = new System.Drawing.Size(564, 496);
            this.grbResult.TabIndex = 12;
            this.grbResult.TabStop = false;
            this.grbResult.Text = "WHOis Details";
            // 
            // lblProcessPercent
            // 
            this.lblProcessPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProcessPercent.Location = new System.Drawing.Point(446, 468);
            this.lblProcessPercent.Name = "lblProcessPercent";
            this.lblProcessPercent.Size = new System.Drawing.Size(112, 23);
            this.lblProcessPercent.TabIndex = 9;
            this.lblProcessPercent.Text = "0 / 0";
            this.lblProcessPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvResult.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResult.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvResult.ColumnHeadersHeight = 40;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDomain});
            this.dgvResult.Location = new System.Drawing.Point(6, 19);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.RowHeadersWidth = 40;
            this.dgvResult.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvResult.RowTemplate.Height = 30;
            this.dgvResult.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvResult.Size = new System.Drawing.Size(552, 443);
            this.dgvResult.TabIndex = 8;
            this.dgvResult.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResult_CellDoubleClick);
            // 
            // colDomain
            // 
            this.colDomain.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calisto MT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDomain.DefaultCellStyle = dataGridViewCellStyle3;
            this.colDomain.FillWeight = 200F;
            this.colDomain.Frozen = true;
            this.colDomain.HeaderText = "Domain";
            this.colDomain.Name = "colDomain";
            this.colDomain.ReadOnly = true;
            this.colDomain.Width = 85;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 544);
            this.Controls.Add(this.grbResult);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "WHOis  Onlin Domain Database";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbExtensions.ResumeLayout(false);
            this.grbExtensions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grbResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbServer;
        private System.Windows.Forms.Button btnLookUp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar progResult;
        private System.Windows.Forms.GroupBox grbResult;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.CheckBox chkCc;
        private System.Windows.Forms.CheckBox chkInfo;
        private System.Windows.Forms.CheckBox chkNet;
        private System.Windows.Forms.CheckBox chkIr;
        private System.Windows.Forms.CheckBox chkCom;
        private System.Windows.Forms.GroupBox grbExtensions;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPreCompile;
        private System.Windows.Forms.Label lblProcessPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDomain;
        private System.Windows.Forms.Label lblNamesCounter;
        private System.Windows.Forms.RichTextBox txtHostName;
        private System.Windows.Forms.Button btnReTryErrorCells;
        private System.Windows.Forms.CheckBox chkAccecptDigitLetteral;
    }
}

