﻿namespace Retriever
{
    partial class FormCredentials
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
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new NickAc.ModernUIDoneRight.Controls.ModernButton();
            this.btnSave = new NickAc.ModernUIDoneRight.Controls.ModernButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxSave = new System.Windows.Forms.CheckBox();
            this.cmbProfile = new System.Windows.Forms.ComboBox();
            this.txtSecretAccessKey = new System.Windows.Forms.TextBox();
            this.txtAccessKeyID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.appBar1 = new NickAc.ModernUIDoneRight.Controls.AppBar();
            this.panelButtons.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(1, 278);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(4);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(664, 62);
            this.panelButtons.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.CustomColorScheme = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(451, 17);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 32);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.CustomColorScheme = false;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(555, 17);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 32);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "OK";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.modernButton1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxSave);
            this.panel1.Controls.Add(this.cmbProfile);
            this.panel1.Controls.Add(this.txtSecretAccessKey);
            this.panel1.Controls.Add(this.txtAccessKeyID);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 83);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(664, 195);
            this.panel1.TabIndex = 1;
            // 
            // checkBoxSave
            // 
            this.checkBoxSave.AutoSize = true;
            this.checkBoxSave.Location = new System.Drawing.Point(507, 141);
            this.checkBoxSave.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxSave.Name = "checkBoxSave";
            this.checkBoxSave.Size = new System.Drawing.Size(144, 25);
            this.checkBoxSave.TabIndex = 17;
            this.checkBoxSave.Text = "Save Credentials";
            this.checkBoxSave.UseVisualStyleBackColor = true;
            // 
            // cmbProfile
            // 
            this.cmbProfile.FormattingEnabled = true;
            this.cmbProfile.Location = new System.Drawing.Point(207, 142);
            this.cmbProfile.Margin = new System.Windows.Forms.Padding(4);
            this.cmbProfile.Name = "cmbProfile";
            this.cmbProfile.Size = new System.Drawing.Size(292, 29);
            this.cmbProfile.TabIndex = 16;
            // 
            // txtSecretAccessKey
            // 
            this.txtSecretAccessKey.Location = new System.Drawing.Point(207, 83);
            this.txtSecretAccessKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtSecretAccessKey.Name = "txtSecretAccessKey";
            this.txtSecretAccessKey.Size = new System.Drawing.Size(444, 29);
            this.txtSecretAccessKey.TabIndex = 15;
            this.txtSecretAccessKey.UseSystemPasswordChar = true;
            // 
            // txtAccessKeyID
            // 
            this.txtAccessKeyID.Location = new System.Drawing.Point(207, 24);
            this.txtAccessKeyID.Margin = new System.Windows.Forms.Padding(4);
            this.txtAccessKeyID.Name = "txtAccessKeyID";
            this.txtAccessKeyID.Size = new System.Drawing.Size(444, 29);
            this.txtAccessKeyID.TabIndex = 14;
            this.txtAccessKeyID.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 145);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 21);
            this.label3.TabIndex = 13;
            this.label3.Text = "or use exisitng user:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 21);
            this.label2.TabIndex = 12;
            this.label2.Text = "AWS Secret Access Key:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 21);
            this.label1.TabIndex = 11;
            this.label1.Text = "AWS Access Key ID: ";
            // 
            // appBar1
            // 
            this.appBar1.CastShadow = true;
            this.appBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.appBar1.HamburgerButtonSize = 32;
            this.appBar1.IconVisible = false;
            this.appBar1.Location = new System.Drawing.Point(1, 33);
            this.appBar1.Name = "appBar1";
            this.appBar1.OverrideParentText = false;
            this.appBar1.Size = new System.Drawing.Size(664, 50);
            this.appBar1.TabIndex = 2;
            this.appBar1.Text = "AWS Credentials";
            this.appBar1.TextFont = new System.Drawing.Font("Segoe UI", 14F);
            this.appBar1.ToolTip = null;
            // 
            // FormCredentials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 341);
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.appBar1);
            this.Controls.Add(this.panelButtons);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(123, 55);
            this.Name = "FormCredentials";
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AWS Credentials";
            this.TitlebarVisible = false;
            this.Load += new System.EventHandler(this.FormCredentials_Load);
            this.panelButtons.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelButtons;
        private NickAc.ModernUIDoneRight.Controls.ModernButton btnCancel;
        private NickAc.ModernUIDoneRight.Controls.ModernButton btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxSave;
        private System.Windows.Forms.ComboBox cmbProfile;
        private System.Windows.Forms.TextBox txtSecretAccessKey;
        private System.Windows.Forms.TextBox txtAccessKeyID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private NickAc.ModernUIDoneRight.Controls.AppBar appBar1;
    }
}