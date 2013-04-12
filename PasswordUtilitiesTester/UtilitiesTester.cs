// Copyright (c) 2013 Mark Pearce
// http://opensource.org/licenses/MIT

using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

using PasswordUtilities;

namespace PasswordUtilitiesTester
{
    public partial class UtilitiesTester : Form
    {
        private const string COLUMN_NAME_KEY = "Key";
        private const string COLUMN_NAME_ALLOWED = "Allowed";
        private const string COLUMN_NAME_MINIMUM_NUMBER_OF_CHARACTERS = "Minimum";
        private const string COLUMN_NAME_TITLE = "Title";
        private const string COLUMN_NAME_CHARACTERS = "Characters";

        private const string SECONDS_WITH_FOUR_DECIMAL_PLACES = "0:ss\\.ffff";
        private const string COMMAS_AND_ZERO_DECIMAL_PLACES = "0:n0";
        private const string COMMAS_AND_TWO_DECIMAL_PLACES = "0:n2";

        public UtilitiesTester()
        {
            InitializeComponent();
        }

        private PasswordGenerator PasswordGen { get; set; }
        private HashGenerator HashGen { get; set; }

        // Setup policies and control values.
        private void UtilitiesTester_Load(object sender, EventArgs e)
        {
            // Programmatic value changes to controls are starting.
            this.UserEditing = false;

            // Use default password and hash policies.
            this.PasswordGen = new PasswordGenerator();
            this.HashGen = new HashGenerator();

            this.SetupPasswordPolicyControlValues();
            this.SetupHashPolicyControlValues();

            // Programmatic value changes to controls are finished.
            this.UserEditing = true; 
            this.GenerateNewPassword();
        }

        // Used to control behavior of controls and password/hash generation during user/program data changes.
        private bool UserEditing { get; set; }

        // Set control values based on the current password policy.
        private void SetupPasswordPolicyControlValues()
        {
            // Set controls to show password policy values
            this.udnPasswordLengthMinimum.Value = this.PasswordGen.Policy.LengthMinimum;
            this.udnPasswordLengthMinimum.Maximum = this.PasswordGen.Policy.LengthMaximum;
            this.udnPasswordLengthMinimum.Minimum = this.CalculateMinimumPasswordLength();

            this.udnPasswordLengthMaximum.Value = this.PasswordGen.Policy.LengthMaximum;
            this.udnPasswordLengthMaximum.Maximum = PasswordPolicy.AbsoluteLengthMaximum;
            this.udnPasswordLengthMaximum.Minimum = this.udnPasswordLengthMinimum.Value;

            this.txtPasswordSymbols.Text = this.PasswordGen.Policy.AllowedSymbols;

            // Setup grid showing character sets.
            this.SetupCharacterSetView();
            this.PopulateCharacterSetView(this.PasswordGen.Policy.AllowedCharacterSets);
        }

        private Int32 CalculateMinimumPasswordLength()
        {
            if (PasswordPolicy.AbsoluteLengthMinimum > this.PasswordGen.Policy.MinimumNumberOfSymbols)
            {
                return PasswordPolicy.AbsoluteLengthMinimum;
            }
            else
            {
                return this.PasswordGen.Policy.MinimumNumberOfSymbols;
            }
        }

        // Show character sets belong to current password policy.
        private void SetupCharacterSetView()
        {
            this.dgvCharacterSets.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            this.dgvCharacterSets.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvCharacterSets.ColumnHeadersDefaultCellStyle.Font = new Font(dgvCharacterSets.Font, FontStyle.Bold);
            this.dgvCharacterSets.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvCharacterSets.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.dgvCharacterSets.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            this.dgvCharacterSets.GridColor = Color.Black;
            this.dgvCharacterSets.RowHeadersVisible = true;

            this.dgvCharacterSets.ColumnCount = 4;

            this.dgvCharacterSets.Columns[0].DataPropertyName = "Key";
            this.dgvCharacterSets.Columns[0].Name = COLUMN_NAME_KEY;
            this.dgvCharacterSets.Columns[0].HeaderText = COLUMN_NAME_KEY;

            this.dgvCharacterSets.Columns[1].DataPropertyName = "Title";
            this.dgvCharacterSets.Columns[1].Name = COLUMN_NAME_TITLE;
            this.dgvCharacterSets.Columns[1].HeaderText = COLUMN_NAME_TITLE;

            this.dgvCharacterSets.Columns[2].DataPropertyName = "Characters";
            this.dgvCharacterSets.Columns[2].Name = COLUMN_NAME_CHARACTERS;
            this.dgvCharacterSets.Columns[2].HeaderText = COLUMN_NAME_CHARACTERS;

            this.dgvCharacterSets.Columns[3].DataPropertyName = "MinimumNumberOfCharacters";
            this.dgvCharacterSets.Columns[3].Name = COLUMN_NAME_MINIMUM_NUMBER_OF_CHARACTERS;
            this.dgvCharacterSets.Columns[3].HeaderText = COLUMN_NAME_MINIMUM_NUMBER_OF_CHARACTERS;

            //this.dgvCharacterSets.AutoSize = true; 
            this.dgvCharacterSets.AllowUserToDeleteRows = false;
            this.dgvCharacterSets.MultiSelect = false;
            this.dgvCharacterSets.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }

        // Populate view of character sets.
        private void PopulateCharacterSetView(IEnumerable<CharacterSet> characterSets)
        {
            // Add character sets to control.
            bool exceptionThrown = true;
            BindingSource newBinding = new BindingSource(); 
            try
            {
                foreach (CharacterSet characterSet in characterSets)
                {
                    newBinding.Add(characterSet);
                }
                this.dgvCharacterSets.DataSource = newBinding;
                exceptionThrown = false;
            }
            finally
            {
                // Just in case the .Add throws an exception.
                if (exceptionThrown)
                {
                    newBinding.Dispose();
                }
            }

            // Add extra column to control.
            exceptionThrown = true; 
            var columnCheck = new DataGridViewCheckBoxColumn();
            try
            {
                columnCheck.Name = COLUMN_NAME_ALLOWED;
                columnCheck.HeaderText = COLUMN_NAME_ALLOWED + "?";
                this.dgvCharacterSets.Columns.Add(columnCheck);
                exceptionThrown = false;
            }
            finally
            {
                // Just in case the .Add throws an exception.
                if (exceptionThrown)
                {
                    columnCheck.Dispose();
                }
            }

            // Set flag for every allowed character set.
            foreach (DataGridViewRow row in this.dgvCharacterSets.Rows)
            {
                row.Cells[COLUMN_NAME_ALLOWED].Value = true;
            }
            // If only one allowed character set, make sure it isn't removed!
            this.AtLeastOneCharacterSetAllowed();

            this.dgvCharacterSets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCharacterSets.AutoResizeColumns();
        }

        // Set control values based on the current hash policy.
        private void SetupHashPolicyControlValues()
        {
            this.udnSaltBytes.Value = this.HashGen.Policy.NumberOfSaltBytes;
            this.udnWorkFactor.Value = this.HashGen.Policy.WorkFactor;
            this.cmbHashMethod.DataSource = Enum.GetValues(typeof(HashAlgorithm));
            this.cmbHashMethod.SelectedItem = this.HashGen.Policy.HashMethod;
            this.cmbStorageFormat.DataSource = Enum.GetValues(typeof(StorageFormat));
            this.cmbStorageFormat.SelectedItem = this.HashGen.Policy.HashStorageFormat;
        }

        // Set new values based on change in password minimum length.
        private void PasswordLengthMinimum_ValueChanged(object sender, EventArgs e)
        {
            this.PasswordGen.Policy.LengthMinimum = (Int32)this.udnPasswordLengthMinimum.Value;
            this.udnPasswordLengthMaximum.Minimum = this.udnPasswordLengthMinimum.Value; 
            this.GenerateNewPassword();
        }

        // Set new values based on change in password maximum length.
        private void PasswordLengthMaximum_ValueChanged(object sender, EventArgs e)
        {
            this.PasswordGen.Policy.LengthMaximum = (Int32)this.udnPasswordLengthMaximum.Value;
            this.udnPasswordLengthMinimum.Maximum = this.udnPasswordLengthMaximum.Value;
            this.GenerateNewPassword();
        }

        // Recalculate hash based on new number of hash iterations.
        private void HashIterations_ValueChanged(object sender, EventArgs e)
        {
            this.HashGen.Policy.WorkFactor = (Int32)this.udnWorkFactor.Value;
            this.GenerateNewHash();
        }

        // Recalculate hash based on new number of salt bytes.
        private void SaltBytes_ValueChanged(object sender, EventArgs e)
        {
            this.HashGen.Policy.NumberOfSaltBytes = (Int32)this.udnSaltBytes.Value;
            this.GenerateNewHash();
        }

        // Recalculate hash based on new hash algorithm.
        private void HashAlgo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.HashGen.Policy.HashMethod = (HashAlgorithm)Enum.Parse(typeof(HashAlgorithm), this.cmbHashMethod.SelectedItem.ToString());
            this.GenerateNewHash();
        }

        // Reformat hash based on selected storage format.
        private void StorageFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.HashGen.Policy.HashStorageFormat = (StorageFormat)Enum.Parse(typeof(StorageFormat), this.cmbStorageFormat.SelectedItem.ToString());
            this.RegenerateHashStorage();
        }

        // Don't allow minimum number of symbols to exceed password min length.

        // User wants to generate a new password manually.
        private void PasswordGenerate_Click(object sender, EventArgs e)
        {
            this.GenerateNewPassword(); 
        }

        // User wants to verify password against stored hash.
        private void PasswordVerify_Click(object sender, EventArgs e)
        {
            if (PasswordVerifier.PasswordVerify(this.txtPassword.Text, this.txtFullHash.Text))
            {
                this.lblVerify.Text = "Verified successfully";
            }
            else
            {
                this.lblVerify.Text = "Verification failed!";
            }
        }

        // Event triggered when DataGridView cell value has been changed (before being committed).
        private void dgvCharacterSets_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Immediately commit any end-user click on an "Allowed" checkbox.
            if ( (this.UserEditing) && (this.dgvCharacterSets.IsCurrentCellDirty) && 
                 (this.dgvCharacterSets.Columns[this.dgvCharacterSets.CurrentCell.ColumnIndex].Name == COLUMN_NAME_ALLOWED) )
            {
                dgvCharacterSets.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        // Event triggered when DataGridView cell value has been changed and committed.
        private void dgvCharacterSets_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.UserEditing) 
            {
                // Add or remove allowed character set as specified by the end-user.
                if (this.dgvCharacterSets.Columns[e.ColumnIndex].Name == COLUMN_NAME_ALLOWED) 
                {
                    if ((bool)this.dgvCharacterSets.CurrentRow.Cells[e.ColumnIndex].Value == true)
                    {
                        CharacterSet charSet = this.dgvCharacterSets.CurrentRow.DataBoundItem as CharacterSet;
                        this.PasswordGen.Policy.CharacterSetAdd(charSet);
                        this.dgvCharacterSets.CurrentRow.Cells[COLUMN_NAME_MINIMUM_NUMBER_OF_CHARACTERS].ReadOnly = false;
                    }
                    else
                    {
                        string key = this.dgvCharacterSets.CurrentRow.Cells[COLUMN_NAME_KEY].Value.ToString();
                        this.PasswordGen.Policy.CharacterSetRemove(key);
                        this.dgvCharacterSets.CurrentRow.Cells[COLUMN_NAME_MINIMUM_NUMBER_OF_CHARACTERS].ReadOnly = true;
                    }
                    this.AtLeastOneCharacterSetAllowed();
                }

                // Update password minimum length based on minumum number of symbols in allowed character sets.
                if (this.dgvCharacterSets.Columns[e.ColumnIndex].Name == COLUMN_NAME_MINIMUM_NUMBER_OF_CHARACTERS)
                {
                    string key = this.dgvCharacterSets.CurrentRow.Cells[COLUMN_NAME_KEY].Value.ToString();
                    Int32 symbolCount = (Int32)this.dgvCharacterSets.CurrentRow.Cells[COLUMN_NAME_MINIMUM_NUMBER_OF_CHARACTERS].Value;
                    this.PasswordGen.Policy.CharacterSetMinimumCharacters(key, symbolCount);
                    this.udnPasswordLengthMinimum.Minimum = this.CalculateMinimumPasswordLength();
                }

                this.GenerateNewPassword();
            }
        }

        // Make sure that at least one character set is allowed!
        private void AtLeastOneCharacterSetAllowed()
        {
            foreach (DataGridViewRow row in this.dgvCharacterSets.Rows)
            {
                if ((bool)row.Cells[COLUMN_NAME_ALLOWED].Value == true)
                {
                    row.Cells[COLUMN_NAME_ALLOWED].ReadOnly = (this.PasswordGen.Policy.Count == 1);
                }
            }
        }

        private void GenerateNewPassword()
        {
            if (this.UserEditing)
            {
                this.lblVerify.Text = String.Empty;
                this.txtPasswordSymbols.Text = this.PasswordGen.Policy.AllowedSymbols;
                this.txtPassword.Text = this.PasswordGen.GeneratePassword();
                this.lblAttempts.Text = String.Format("Rejected passwords: {" + COMMAS_AND_ZERO_DECIMAL_PLACES + "}", this.PasswordGen.PasswordRejectedCount);
                this.lblPasswordTime.Text = String.Format("Time: {" + SECONDS_WITH_FOUR_DECIMAL_PLACES + "}", this.PasswordGen.GenerationTime);
                this.lblPasswordEntropy.Text = String.Format("Strength: {" + COMMAS_AND_TWO_DECIMAL_PLACES + "} bits", this.PasswordGen.PasswordEntropy);

                this.GenerateNewHash();
            }
        }

        private void GenerateNewHash()
        {
            if (this.UserEditing)
            {
                this.HashGen.CreatePasswordSaltAndHash(this.txtPassword.Text);
                this.txtPasswordHash.Text = this.HashGen.HashEncoded;
                this.txtFullHash.Text = this.HashGen.StoredHash;
                this.lblHashTime.Text = String.Format("Time: {" + SECONDS_WITH_FOUR_DECIMAL_PLACES + "}", this.HashGen.GenerationTime);
                this.lblHashEntropy.Text = String.Format("Strength: {" + COMMAS_AND_TWO_DECIMAL_PLACES + "} bits", this.HashGen.HashEntropy);
            }
        }

        private void RegenerateHashStorage()
        {
            if (this.UserEditing)
            {
                this.txtPasswordHash.Text = this.HashGen.HashEncoded;
                this.txtFullHash.Text = this.HashGen.StoredHash;
            }
        }
    }
}