namespace PasswordUtilitiesTester
{
    partial class UtilitiesTester
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
            this.grpPasswordPolicy = new System.Windows.Forms.GroupBox();
            this.lblVerify = new System.Windows.Forms.Label();
            this.btnVerify = new System.Windows.Forms.Button();
            this.dgvCharacterSets = new System.Windows.Forms.DataGridView();
            this.lblPasswordEntropy = new System.Windows.Forms.Label();
            this.lblPasswordTime = new System.Windows.Forms.Label();
            this.lblAttempts = new System.Windows.Forms.Label();
            this.lblAllowedSymbols = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtPasswordSymbols = new System.Windows.Forms.TextBox();
            this.labelLengthMax = new System.Windows.Forms.Label();
            this.labelLengthMin = new System.Windows.Forms.Label();
            this.udnPasswordLengthMaximum = new System.Windows.Forms.NumericUpDown();
            this.udnPasswordLengthMinimum = new System.Windows.Forms.NumericUpDown();
            this.grpHashPolicy = new System.Windows.Forms.GroupBox();
            this.lblWholeHash = new System.Windows.Forms.Label();
            this.lblHash = new System.Windows.Forms.Label();
            this.lblHashAlgo = new System.Windows.Forms.Label();
            this.cmbHashMethod = new System.Windows.Forms.ComboBox();
            this.lblHashEntropy = new System.Windows.Forms.Label();
            this.lblHashTime = new System.Windows.Forms.Label();
            this.lblSaltBytes = new System.Windows.Forms.Label();
            this.txtFullHash = new System.Windows.Forms.TextBox();
            this.udnSaltBytes = new System.Windows.Forms.NumericUpDown();
            this.cmbStorageFormat = new System.Windows.Forms.ComboBox();
            this.lblStorageFormat = new System.Windows.Forms.Label();
            this.lblWorkFactor = new System.Windows.Forms.Label();
            this.udnWorkFactor = new System.Windows.Forms.NumericUpDown();
            this.txtPasswordHash = new System.Windows.Forms.TextBox();
            this.lblIterations = new System.Windows.Forms.Label();
            this.grpPasswordPolicy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCharacterSets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udnPasswordLengthMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udnPasswordLengthMinimum)).BeginInit();
            this.grpHashPolicy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udnSaltBytes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udnWorkFactor)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPasswordPolicy
            // 
            this.grpPasswordPolicy.Controls.Add(this.lblVerify);
            this.grpPasswordPolicy.Controls.Add(this.btnVerify);
            this.grpPasswordPolicy.Controls.Add(this.dgvCharacterSets);
            this.grpPasswordPolicy.Controls.Add(this.lblPasswordEntropy);
            this.grpPasswordPolicy.Controls.Add(this.lblPasswordTime);
            this.grpPasswordPolicy.Controls.Add(this.lblAttempts);
            this.grpPasswordPolicy.Controls.Add(this.lblAllowedSymbols);
            this.grpPasswordPolicy.Controls.Add(this.btnGenerate);
            this.grpPasswordPolicy.Controls.Add(this.txtPassword);
            this.grpPasswordPolicy.Controls.Add(this.txtPasswordSymbols);
            this.grpPasswordPolicy.Controls.Add(this.labelLengthMax);
            this.grpPasswordPolicy.Controls.Add(this.labelLengthMin);
            this.grpPasswordPolicy.Controls.Add(this.udnPasswordLengthMaximum);
            this.grpPasswordPolicy.Controls.Add(this.udnPasswordLengthMinimum);
            this.grpPasswordPolicy.Location = new System.Drawing.Point(12, 12);
            this.grpPasswordPolicy.Name = "grpPasswordPolicy";
            this.grpPasswordPolicy.Size = new System.Drawing.Size(565, 457);
            this.grpPasswordPolicy.TabIndex = 0;
            this.grpPasswordPolicy.TabStop = false;
            this.grpPasswordPolicy.Text = "Password Policy";
            // 
            // lblVerify
            // 
            this.lblVerify.AutoSize = true;
            this.lblVerify.Location = new System.Drawing.Point(299, 308);
            this.lblVerify.Name = "lblVerify";
            this.lblVerify.Size = new System.Drawing.Size(0, 13);
            this.lblVerify.TabIndex = 23;
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(153, 303);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(123, 23);
            this.btnVerify.TabIndex = 22;
            this.btnVerify.Text = "Verify password";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.PasswordVerify_Click);
            // 
            // dgvCharacterSets
            // 
            this.dgvCharacterSets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCharacterSets.Location = new System.Drawing.Point(13, 53);
            this.dgvCharacterSets.Name = "dgvCharacterSets";
            this.dgvCharacterSets.Size = new System.Drawing.Size(534, 147);
            this.dgvCharacterSets.TabIndex = 16;
            this.dgvCharacterSets.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCharacterSets_CellValueChanged);
            this.dgvCharacterSets.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvCharacterSets_CurrentCellDirtyStateChanged);
            // 
            // lblPasswordEntropy
            // 
            this.lblPasswordEntropy.AutoSize = true;
            this.lblPasswordEntropy.Location = new System.Drawing.Point(263, 403);
            this.lblPasswordEntropy.Name = "lblPasswordEntropy";
            this.lblPasswordEntropy.Size = new System.Drawing.Size(89, 13);
            this.lblPasswordEntropy.TabIndex = 15;
            this.lblPasswordEntropy.Text = "Entropy: ### bits";
            // 
            // lblPasswordTime
            // 
            this.lblPasswordTime.AutoSize = true;
            this.lblPasswordTime.Location = new System.Drawing.Point(160, 403);
            this.lblPasswordTime.Name = "lblPasswordTime";
            this.lblPasswordTime.Size = new System.Drawing.Size(67, 13);
            this.lblPasswordTime.TabIndex = 14;
            this.lblPasswordTime.Text = "Time: ##.##";
            // 
            // lblAttempts
            // 
            this.lblAttempts.AutoSize = true;
            this.lblAttempts.Location = new System.Drawing.Point(21, 403);
            this.lblAttempts.Name = "lblAttempts";
            this.lblAttempts.Size = new System.Drawing.Size(122, 13);
            this.lblAttempts.TabIndex = 13;
            this.lblAttempts.Text = "### rejected passwords";
            // 
            // lblAllowedSymbols
            // 
            this.lblAllowedSymbols.AutoSize = true;
            this.lblAllowedSymbols.Location = new System.Drawing.Point(13, 217);
            this.lblAllowedSymbols.Name = "lblAllowedSymbols";
            this.lblAllowedSymbols.Size = new System.Drawing.Size(86, 13);
            this.lblAllowedSymbols.TabIndex = 12;
            this.lblAllowedSymbols.Text = "Allowed Symbols";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(13, 303);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(123, 23);
            this.btnGenerate.TabIndex = 11;
            this.btnGenerate.Text = "Generate password";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.PasswordGenerate_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtPassword.Font = new System.Drawing.Font("Lucida Console", 9F);
            this.txtPassword.Location = new System.Drawing.Point(15, 334);
            this.txtPassword.Multiline = true;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.ReadOnly = true;
            this.txtPassword.Size = new System.Drawing.Size(532, 66);
            this.txtPassword.TabIndex = 10;
            this.txtPassword.TabStop = false;
            // 
            // txtPasswordSymbols
            // 
            this.txtPasswordSymbols.CausesValidation = false;
            this.txtPasswordSymbols.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasswordSymbols.Location = new System.Drawing.Point(15, 240);
            this.txtPasswordSymbols.Multiline = true;
            this.txtPasswordSymbols.Name = "txtPasswordSymbols";
            this.txtPasswordSymbols.ReadOnly = true;
            this.txtPasswordSymbols.Size = new System.Drawing.Size(528, 49);
            this.txtPasswordSymbols.TabIndex = 9;
            this.txtPasswordSymbols.TabStop = false;
            // 
            // labelLengthMax
            // 
            this.labelLengthMax.AutoSize = true;
            this.labelLengthMax.Location = new System.Drawing.Point(159, 29);
            this.labelLengthMax.Name = "labelLengthMax";
            this.labelLengthMax.Size = new System.Drawing.Size(83, 13);
            this.labelLengthMax.TabIndex = 2;
            this.labelLengthMax.Text = "Maximum length";
            // 
            // labelLengthMin
            // 
            this.labelLengthMin.AutoSize = true;
            this.labelLengthMin.Location = new System.Drawing.Point(8, 29);
            this.labelLengthMin.Name = "labelLengthMin";
            this.labelLengthMin.Size = new System.Drawing.Size(80, 13);
            this.labelLengthMin.TabIndex = 2;
            this.labelLengthMin.Text = "Minimum length";
            // 
            // udnPasswordLengthMaximum
            // 
            this.udnPasswordLengthMaximum.Location = new System.Drawing.Point(248, 27);
            this.udnPasswordLengthMaximum.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.udnPasswordLengthMaximum.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.udnPasswordLengthMaximum.Name = "udnPasswordLengthMaximum";
            this.udnPasswordLengthMaximum.Size = new System.Drawing.Size(40, 20);
            this.udnPasswordLengthMaximum.TabIndex = 1;
            this.udnPasswordLengthMaximum.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udnPasswordLengthMaximum.ValueChanged += new System.EventHandler(this.PasswordLengthMaximum_ValueChanged);
            // 
            // udnPasswordLengthMinimum
            // 
            this.udnPasswordLengthMinimum.Location = new System.Drawing.Point(95, 26);
            this.udnPasswordLengthMinimum.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.udnPasswordLengthMinimum.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.udnPasswordLengthMinimum.Name = "udnPasswordLengthMinimum";
            this.udnPasswordLengthMinimum.Size = new System.Drawing.Size(41, 20);
            this.udnPasswordLengthMinimum.TabIndex = 0;
            this.udnPasswordLengthMinimum.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udnPasswordLengthMinimum.ValueChanged += new System.EventHandler(this.PasswordLengthMinimum_ValueChanged);
            // 
            // grpHashPolicy
            // 
            this.grpHashPolicy.Controls.Add(this.lblIterations);
            this.grpHashPolicy.Controls.Add(this.lblWholeHash);
            this.grpHashPolicy.Controls.Add(this.lblHash);
            this.grpHashPolicy.Controls.Add(this.lblHashAlgo);
            this.grpHashPolicy.Controls.Add(this.cmbHashMethod);
            this.grpHashPolicy.Controls.Add(this.lblHashEntropy);
            this.grpHashPolicy.Controls.Add(this.lblHashTime);
            this.grpHashPolicy.Controls.Add(this.lblSaltBytes);
            this.grpHashPolicy.Controls.Add(this.txtFullHash);
            this.grpHashPolicy.Controls.Add(this.udnSaltBytes);
            this.grpHashPolicy.Controls.Add(this.cmbStorageFormat);
            this.grpHashPolicy.Controls.Add(this.lblStorageFormat);
            this.grpHashPolicy.Controls.Add(this.lblWorkFactor);
            this.grpHashPolicy.Controls.Add(this.udnWorkFactor);
            this.grpHashPolicy.Location = new System.Drawing.Point(599, 13);
            this.grpHashPolicy.Name = "grpHashPolicy";
            this.grpHashPolicy.Size = new System.Drawing.Size(324, 456);
            this.grpHashPolicy.TabIndex = 1;
            this.grpHashPolicy.TabStop = false;
            this.grpHashPolicy.Text = "Hash Policy";
            // 
            // lblWholeHash
            // 
            this.lblWholeHash.AutoSize = true;
            this.lblWholeHash.Location = new System.Drawing.Point(10, 310);
            this.lblWholeHash.Name = "lblWholeHash";
            this.lblWholeHash.Size = new System.Drawing.Size(284, 13);
            this.lblWholeHash.TabIndex = 21;
            this.lblWholeHash.Text = "Algorithm.Storage.WorkFactor.EncodedSalt.EncodedHash";
            // 
            // lblHash
            // 
            this.lblHash.AutoSize = true;
            this.lblHash.Location = new System.Drawing.Point(10, 216);
            this.lblHash.Name = "lblHash";
            this.lblHash.Size = new System.Drawing.Size(124, 13);
            this.lblHash.TabIndex = 17;
            this.lblHash.Text = "Encoded password hash";
            // 
            // lblHashAlgo
            // 
            this.lblHashAlgo.AutoSize = true;
            this.lblHashAlgo.Location = new System.Drawing.Point(6, 23);
            this.lblHashAlgo.Name = "lblHashAlgo";
            this.lblHashAlgo.Size = new System.Drawing.Size(77, 13);
            this.lblHashAlgo.TabIndex = 20;
            this.lblHashAlgo.Text = "Hash algorithm";
            // 
            // cmbHashMethod
            // 
            this.cmbHashMethod.FormattingEnabled = true;
            this.cmbHashMethod.Location = new System.Drawing.Point(94, 20);
            this.cmbHashMethod.Name = "cmbHashMethod";
            this.cmbHashMethod.Size = new System.Drawing.Size(121, 21);
            this.cmbHashMethod.TabIndex = 19;
            this.cmbHashMethod.SelectedIndexChanged += new System.EventHandler(this.HashAlgo_SelectedIndexChanged);
            // 
            // lblHashEntropy
            // 
            this.lblHashEntropy.AutoSize = true;
            this.lblHashEntropy.Location = new System.Drawing.Point(212, 402);
            this.lblHashEntropy.Name = "lblHashEntropy";
            this.lblHashEntropy.Size = new System.Drawing.Size(89, 13);
            this.lblHashEntropy.TabIndex = 17;
            this.lblHashEntropy.Text = "Entropy: ### bits";
            // 
            // lblHashTime
            // 
            this.lblHashTime.AutoSize = true;
            this.lblHashTime.Location = new System.Drawing.Point(124, 402);
            this.lblHashTime.Name = "lblHashTime";
            this.lblHashTime.Size = new System.Drawing.Size(67, 13);
            this.lblHashTime.TabIndex = 17;
            this.lblHashTime.Text = "Time: ##.##";
            // 
            // lblSaltBytes
            // 
            this.lblSaltBytes.AutoSize = true;
            this.lblSaltBytes.Location = new System.Drawing.Point(10, 132);
            this.lblSaltBytes.Name = "lblSaltBytes";
            this.lblSaltBytes.Size = new System.Drawing.Size(53, 13);
            this.lblSaltBytes.TabIndex = 18;
            this.lblSaltBytes.Text = "Salt bytes";
            // 
            // txtFullHash
            // 
            this.txtFullHash.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtFullHash.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFullHash.Location = new System.Drawing.Point(13, 333);
            this.txtFullHash.Multiline = true;
            this.txtFullHash.Name = "txtFullHash";
            this.txtFullHash.ReadOnly = true;
            this.txtFullHash.Size = new System.Drawing.Size(295, 66);
            this.txtFullHash.TabIndex = 17;
            this.txtFullHash.TabStop = false;
            // 
            // udnSaltBytes
            // 
            this.udnSaltBytes.Location = new System.Drawing.Point(95, 130);
            this.udnSaltBytes.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.udnSaltBytes.Name = "udnSaltBytes";
            this.udnSaltBytes.Size = new System.Drawing.Size(61, 20);
            this.udnSaltBytes.TabIndex = 6;
            this.udnSaltBytes.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udnSaltBytes.ValueChanged += new System.EventHandler(this.SaltBytes_ValueChanged);
            // 
            // cmbStorageFormat
            // 
            this.cmbStorageFormat.FormattingEnabled = true;
            this.cmbStorageFormat.Location = new System.Drawing.Point(94, 57);
            this.cmbStorageFormat.Name = "cmbStorageFormat";
            this.cmbStorageFormat.Size = new System.Drawing.Size(121, 21);
            this.cmbStorageFormat.TabIndex = 3;
            this.cmbStorageFormat.SelectedIndexChanged += new System.EventHandler(this.StorageFormat_SelectedIndexChanged);
            // 
            // lblStorageFormat
            // 
            this.lblStorageFormat.AutoSize = true;
            this.lblStorageFormat.Location = new System.Drawing.Point(8, 59);
            this.lblStorageFormat.Name = "lblStorageFormat";
            this.lblStorageFormat.Size = new System.Drawing.Size(76, 13);
            this.lblStorageFormat.TabIndex = 2;
            this.lblStorageFormat.Text = "Storage format";
            // 
            // lblWorkFactor
            // 
            this.lblWorkFactor.AutoSize = true;
            this.lblWorkFactor.Location = new System.Drawing.Point(9, 95);
            this.lblWorkFactor.Name = "lblWorkFactor";
            this.lblWorkFactor.Size = new System.Drawing.Size(63, 13);
            this.lblWorkFactor.TabIndex = 1;
            this.lblWorkFactor.Text = "Work factor";
            // 
            // udnWorkFactor
            // 
            this.udnWorkFactor.Location = new System.Drawing.Point(95, 93);
            this.udnWorkFactor.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.udnWorkFactor.Name = "udnWorkFactor";
            this.udnWorkFactor.Size = new System.Drawing.Size(61, 20);
            this.udnWorkFactor.TabIndex = 0;
            this.udnWorkFactor.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udnWorkFactor.ValueChanged += new System.EventHandler(this.HashIterations_ValueChanged);
            // 
            // txtPasswordHash
            // 
            this.txtPasswordHash.CausesValidation = false;
            this.txtPasswordHash.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasswordHash.Location = new System.Drawing.Point(612, 252);
            this.txtPasswordHash.Multiline = true;
            this.txtPasswordHash.Name = "txtPasswordHash";
            this.txtPasswordHash.ReadOnly = true;
            this.txtPasswordHash.Size = new System.Drawing.Size(295, 49);
            this.txtPasswordHash.TabIndex = 17;
            this.txtPasswordHash.TabStop = false;
            // 
            // lblIterations
            // 
            this.lblIterations.AutoSize = true;
            this.lblIterations.Location = new System.Drawing.Point(166, 97);
            this.lblIterations.Name = "lblIterations";
            this.lblIterations.Size = new System.Drawing.Size(80, 13);
            this.lblIterations.TabIndex = 22;
            this.lblIterations.Text = "#### iterations";
            // 
            // UtilitiesTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 481);
            this.Controls.Add(this.txtPasswordHash);
            this.Controls.Add(this.grpHashPolicy);
            this.Controls.Add(this.grpPasswordPolicy);
            this.Name = "UtilitiesTester";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password Utilities Tester";
            this.Load += new System.EventHandler(this.UtilitiesTester_Load);
            this.grpPasswordPolicy.ResumeLayout(false);
            this.grpPasswordPolicy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCharacterSets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udnPasswordLengthMaximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udnPasswordLengthMinimum)).EndInit();
            this.grpHashPolicy.ResumeLayout(false);
            this.grpHashPolicy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udnSaltBytes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udnWorkFactor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPasswordPolicy;
        private System.Windows.Forms.NumericUpDown udnPasswordLengthMinimum;
        private System.Windows.Forms.NumericUpDown udnPasswordLengthMaximum;
        private System.Windows.Forms.Label labelLengthMax;
        private System.Windows.Forms.Label labelLengthMin;
        private System.Windows.Forms.TextBox txtPasswordSymbols;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label lblAllowedSymbols;
        private System.Windows.Forms.Label lblAttempts;
        private System.Windows.Forms.Label lblPasswordTime;
        private System.Windows.Forms.Label lblPasswordEntropy;
        private System.Windows.Forms.GroupBox grpHashPolicy;
        private System.Windows.Forms.NumericUpDown udnWorkFactor;
        //private PasswordUtilitiesTester.UpDownPowerOfTwo udnWorkFactor;
        private System.Windows.Forms.DataGridView dgvCharacterSets;
        private System.Windows.Forms.Label lblStorageFormat;
        private System.Windows.Forms.Label lblWorkFactor;
        private System.Windows.Forms.ComboBox cmbStorageFormat;
        private System.Windows.Forms.NumericUpDown udnSaltBytes;
        private System.Windows.Forms.TextBox txtFullHash;
        private System.Windows.Forms.TextBox txtPasswordHash;
        private System.Windows.Forms.Label lblSaltBytes;
        private System.Windows.Forms.Label lblHashTime;
        private System.Windows.Forms.Label lblHashEntropy;
        private System.Windows.Forms.Label lblHashAlgo;
        private System.Windows.Forms.ComboBox cmbHashMethod;
        private System.Windows.Forms.Label lblWholeHash;
        private System.Windows.Forms.Label lblHash;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Label lblVerify;
        private System.Windows.Forms.Label lblIterations;
    }
}