namespace digitalFinger
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMessageData = new System.Windows.Forms.TextBox();
            this.btnResetUI = new System.Windows.Forms.Button();
            this.btnCopyFingerData = new System.Windows.Forms.Button();
            this.lblStatusText = new System.Windows.Forms.Label();
            this.txtFingerData = new System.Windows.Forms.TextBox();
            this.picFingerPicture = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDateData = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFingerPicture)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(805, 500);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.txtMessageData);
            this.panel1.Controls.Add(this.btnResetUI);
            this.panel1.Controls.Add(this.btnCopyFingerData);
            this.panel1.Controls.Add(this.lblStatusText);
            this.panel1.Controls.Add(this.txtFingerData);
            this.panel1.Controls.Add(this.picFingerPicture);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(799, 494);
            this.panel1.TabIndex = 0;
            // 
            // txtMessageData
            // 
            this.txtMessageData.Location = new System.Drawing.Point(19, 367);
            this.txtMessageData.Multiline = true;
            this.txtMessageData.Name = "txtMessageData";
            this.txtMessageData.Size = new System.Drawing.Size(605, 76);
            this.txtMessageData.TabIndex = 7;
            // 
            // btnResetUI
            // 
            this.btnResetUI.Location = new System.Drawing.Point(644, 408);
            this.btnResetUI.Name = "btnResetUI";
            this.btnResetUI.Size = new System.Drawing.Size(138, 35);
            this.btnResetUI.TabIndex = 6;
            this.btnResetUI.Text = "Reset";
            this.btnResetUI.UseVisualStyleBackColor = true;
            this.btnResetUI.Click += new System.EventHandler(this.BtnResetUI_Click);
            // 
            // btnCopyFingerData
            // 
            this.btnCopyFingerData.Location = new System.Drawing.Point(644, 367);
            this.btnCopyFingerData.Name = "btnCopyFingerData";
            this.btnCopyFingerData.Size = new System.Drawing.Size(138, 35);
            this.btnCopyFingerData.TabIndex = 5;
            this.btnCopyFingerData.Text = "Хуулах";
            this.btnCopyFingerData.UseVisualStyleBackColor = true;
            this.btnCopyFingerData.Click += new System.EventHandler(this.BtnCopyFingerData_Click);
            // 
            // lblStatusText
            // 
            this.lblStatusText.AutoSize = true;
            this.lblStatusText.Location = new System.Drawing.Point(16, 347);
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(75, 17);
            this.lblStatusText.TabIndex = 4;
            this.lblStatusText.Text = "Мэдээлэл:";
            // 
            // txtFingerData
            // 
            this.txtFingerData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFingerData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtFingerData.Location = new System.Drawing.Point(340, 19);
            this.txtFingerData.Multiline = true;
            this.txtFingerData.Name = "txtFingerData";
            this.txtFingerData.Size = new System.Drawing.Size(442, 310);
            this.txtFingerData.TabIndex = 2;
            // 
            // picFingerPicture
            // 
            this.picFingerPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFingerPicture.Location = new System.Drawing.Point(19, 19);
            this.picFingerPicture.Name = "picFingerPicture";
            this.picFingerPicture.Size = new System.Drawing.Size(310, 310);
            this.picFingerPicture.TabIndex = 1;
            this.picFingerPicture.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblDateData);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 459);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(799, 35);
            this.panel2.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label4.Location = new System.Drawing.Point(642, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "EXIT(Press to ESC)";
            // 
            // lblDateData
            // 
            this.lblDateData.AutoSize = true;
            this.lblDateData.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblDateData.Location = new System.Drawing.Point(82, 10);
            this.lblDateData.Name = "lblDateData";
            this.lblDateData.Size = new System.Drawing.Size(191, 17);
            this.lblDateData.TabIndex = 1;
            this.lblDateData.Text = "YYYY-MM-DD HH24:MI:SS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label2.Location = new System.Drawing.Point(9, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "ОГНОО:";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 500);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Хурууны хээ уншигч";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFingerPicture)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtFingerData;
        private System.Windows.Forms.PictureBox picFingerPicture;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtMessageData;
        private System.Windows.Forms.Button btnResetUI;
        private System.Windows.Forms.Button btnCopyFingerData;
        private System.Windows.Forms.Label lblStatusText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDateData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
    }
}

