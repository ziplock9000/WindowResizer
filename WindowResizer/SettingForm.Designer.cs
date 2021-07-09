﻿namespace WindowResizer
{
    partial class SettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.SaveKeysBox = new System.Windows.Forms.TextBox();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.RestoreKeysBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.WindowsGrid = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.ConfigExportBtn = new System.Windows.Forms.Button();
            this.ConfigImportBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.WindowsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveKeysBox
            // 
            this.SaveKeysBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SaveKeysBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SaveKeysBox.Location = new System.Drawing.Point(78, 11);
            this.SaveKeysBox.Margin = new System.Windows.Forms.Padding(2);
            this.SaveKeysBox.Name = "SaveKeysBox";
            this.SaveKeysBox.Size = new System.Drawing.Size(128, 28);
            this.SaveKeysBox.TabIndex = 0;
            this.SaveKeysBox.TabStop = false;
            // 
            // SaveBtn
            // 
            this.SaveBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SaveBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SaveBtn.Location = new System.Drawing.Point(614, 458);
            this.SaveBtn.Margin = new System.Windows.Forms.Padding(2);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(116, 54);
            this.SaveBtn.TabIndex = 1;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(29, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Save";
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CancelBtn.Location = new System.Drawing.Point(485, 458);
            this.CancelBtn.Margin = new System.Windows.Forms.Padding(2);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(116, 54);
            this.CancelBtn.TabIndex = 0;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // RestoreKeysBox
            // 
            this.RestoreKeysBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RestoreKeysBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RestoreKeysBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.RestoreKeysBox.Location = new System.Drawing.Point(306, 11);
            this.RestoreKeysBox.Margin = new System.Windows.Forms.Padding(2);
            this.RestoreKeysBox.Name = "RestoreKeysBox";
            this.RestoreKeysBox.Size = new System.Drawing.Size(128, 28);
            this.RestoreKeysBox.TabIndex = 4;
            this.RestoreKeysBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(235, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Restore";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(468, 12);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(236, 25);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.TabStop = false;
            this.checkBox1.Text = "Disable in FullScreen Mode";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // WindowsGrid
            // 
            this.WindowsGrid.AllowUserToResizeRows = false;
            this.WindowsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.WindowsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.WindowsGrid.Location = new System.Drawing.Point(12, 44);
            this.WindowsGrid.MultiSelect = false;
            this.WindowsGrid.Name = "WindowsGrid";
            this.WindowsGrid.RowHeadersVisible = false;
            this.WindowsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.WindowsGrid.ShowCellToolTips = false;
            this.WindowsGrid.Size = new System.Drawing.Size(728, 409);
            this.WindowsGrid.TabIndex = 7;
            this.WindowsGrid.TabStop = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(8, 455);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 69);
            this.label3.TabIndex = 8;
            this.label3.Text = "ExeName is an exact match.  Title can be exact, or a start or end match. Start wi" +
    "th a * for an end match, end with a * for a start match.";
            // 
            // ConfigExportBtn
            // 
            this.ConfigExportBtn.Location = new System.Drawing.Point(258, 459);
            this.ConfigExportBtn.Name = "ConfigExportBtn";
            this.ConfigExportBtn.Size = new System.Drawing.Size(119, 31);
            this.ConfigExportBtn.TabIndex = 9;
            this.ConfigExportBtn.Text = "Export Config";
            this.ConfigExportBtn.UseVisualStyleBackColor = true;
            this.ConfigExportBtn.Click += new System.EventHandler(this.ConfigExportBtn_Click);
            // 
            // ConfigImportBtn
            // 
            this.ConfigImportBtn.Location = new System.Drawing.Point(258, 493);
            this.ConfigImportBtn.Name = "ConfigImportBtn";
            this.ConfigImportBtn.Size = new System.Drawing.Size(119, 31);
            this.ConfigImportBtn.TabIndex = 10;
            this.ConfigImportBtn.Text = "Import Config";
            this.ConfigImportBtn.UseVisualStyleBackColor = true;
            this.ConfigImportBtn.Click += new System.EventHandler(this.ConfigImportBtn_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(752, 523);
            this.Controls.Add(this.ConfigImportBtn);
            this.Controls.Add(this.ConfigExportBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.WindowsGrid);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RestoreKeysBox);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.SaveKeysBox);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SettingForm";
            this.Text = "SettingForm";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WindowsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SaveKeysBox;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.TextBox RestoreKeysBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridView WindowsGrid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ConfigExportBtn;
        private System.Windows.Forms.Button ConfigImportBtn;
    }
}