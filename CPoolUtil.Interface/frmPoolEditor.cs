﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CPoolUtil.Core;

namespace CPoolUtil.Interface
{
    public partial class frmPoolEditor : Form
    {
        private IOutputter _outputter;
        private string[] filesToLoadOnLaunch; // Only used when constructing

        private string _currentFile;
        private CPool _appendingPool; // Keep track of each pool so we can handle it after the load worker finishes
        private bool _ignoreDataChanges; // Prevents edits to soldiers while datasources are being modified and values change
        private Character _lastSelectedCharacter = null; // From the listbox
        private int numCharactersAdded = 0;

        private Character _selectedCharacter => (lstCharacters.SelectedItems.Count == 1 ? lstCharacters.SelectedItem : null) as Character;
        private Character[] _selectedCharacters => lstCharacters.SelectedItems.Cast<Character>().ToArray();

        public frmPoolEditor(IOutputter outputter)
        {
            InitializeComponent();

            _outputter = outputter;
            Overlord.ClearPools();
            Overlord.AppendPool(CPool.Create(_outputter));
            Overlord.CharacterPool.DuplicateCharactersIgnoredEvent += DuplicateCharactersIgnored_EventHandler;
            UpdateDirtyStatus();
            ToggleCommonControls(true);
        }

        public frmPoolEditor(string[] poolFiles, IOutputter outputter)
        {
            InitializeComponent();

            _outputter = outputter;
            Overlord.ClearPools();
            filesToLoadOnLaunch = poolFiles;
        }

        private void LoadPools(string[] fileNames)
        {
            lblFormStatus.Text = "Loading... (0%)";
            ToggleCommonControls(false);

            // Manually load files so we can handle potential duplicate soldiers
            for (int i = 0; i < fileNames.Length; i++)
            {
                _currentFile = Path.GetFileName(fileNames[i]);
                _appendingPool = new CPool(new Parser(fileNames[i], _outputter), _outputter);
                _appendingPool.SendProgressUpdateEvent += (progress) =>
                {
                    // Progress update event handler
                    if (progress < 100)
                        lblFormStatus.Text = $"Loading {_currentFile}... ({progress}%)";
                    else
                    {
                        Overlord.AppendPool(_appendingPool);

                        // Subscribe to our duplicate characters ignored event handler if this is the first time we load a pool
                        if (Overlord.CharacterPool == _appendingPool)
                            Overlord.CharacterPool.DuplicateCharactersIgnoredEvent += DuplicateCharactersIgnored_EventHandler;

                        // Update our GUI components
                        DetectDlcsAndMods(Program.Settings.ShowWarnings);
                        UpdateDirtyStatus();
                        RefreshCharacterListDataSource();
                        ToggleCommonControls(true);
                    }
                };

                try
                {
                    _appendingPool.Load();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while loading character pool! {ex}", "Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
        }

        private List<string> DetectDlcsAndMods(bool displayMessage = false)
        {
            var detectedStuff = new List<string>();
            var vanillaTemplateNames = Overlord.Templates.Where(t => t.Origin == "Vanilla").Select(t => t.Name).ToList();
            var nonVanillaTemplates = Overlord.Templates.Where(t => t.Origin != "Vanilla").ToList();

            // Manually check template names against existing character list
            var soldierTypes = Overlord.CharacterPool.Characters.Select(c => c.SoldierType.Data).Distinct().ToList();
            var nameTypes = (Overlord.CharacterPool.Characters.Select(c => c.Appearance.Helmet.Data)
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.Haircut.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.Beard.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.FacePropUpper.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.FacePropLower.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.FacePaint.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.Scars.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.ArmorPatterns.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.Torso.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.TorsoDeco.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.Arms.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.LeftArm.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.RightArm.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.LeftForearm.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.RightForearm.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.LeftArmDeco.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.RightArmDeco.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.Tattoo_LeftArm.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.Tattoo_RightArm.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.Legs.Data))
                .Concat(Overlord.CharacterPool.Characters.Select(c => c.Appearance.Thighs.Data))
                ).Distinct().Where(nt => nt != null).ToList();
            detectedStuff.AddRange(nonVanillaTemplates.Where(t => ((t.PartType == "Pawn" && t.CharacterTemplate != "Soldier" && soldierTypes.Any(st => st == t.CharacterTemplate))
                || nameTypes.Any(nt => nt == t.Name))
                && !vanillaTemplateNames.Contains(t.Name)).Select(t => t.Origin).Distinct()); // If the name exists in vanilla as well, ignore it

            // Hair and armor color indexes (just detect if the index is outside of the vanilla range. It's not guaranteed that it is THESE specific mods that have expanded that selection)
            var maxHairSelection = Overlord.CharacterPool.Characters.Count > 0 ? Overlord.CharacterPool.Characters.Max(c => c.Appearance.HairColor.DataVal) : 0;
            var maxArmorSelection = Overlord.CharacterPool.Characters.Count > 0 ? Overlord.CharacterPool.Characters.Max(c => Math.Max(c.Appearance.ArmorTint1.DataVal, c.Appearance.ArmorTint2.DataVal)) : 0;
            var maxTattooColor = Overlord.CharacterPool.Characters.Count > 0 ? Overlord.CharacterPool.Characters.Max(c => c.Appearance.TattooTint.DataVal) : 0;
            var maxWeaponColor = Overlord.CharacterPool.Characters.Count > 0 ? Overlord.CharacterPool.Characters.Max(c => c.Appearance.WeaponTint.DataVal) : 0;
            var maxVanillaHairSelection = Overlord.Palettes.Where(p => p.PaletteType == Palette.ePaletteType.HairColor).Max(p => p.Index);
            var maxVanillaArmorSelection = Overlord.Palettes.Where(p => p.PaletteType == Palette.ePaletteType.ArmorColor).Max(p => p.Index);
            if (!Overlord.DlcAndModOptions.Contains("More Hair Colors") && maxHairSelection > maxVanillaHairSelection)
                detectedStuff.Add("More Hair Colors");
            // Tattoos and weapons use the same palette as armor
            if (!Overlord.DlcAndModOptions.Contains("More Armor Colors") && (new[] { maxArmorSelection, maxTattooColor, maxWeaponColor }.Max() > maxVanillaArmorSelection))
                detectedStuff.Add("More Armor Colors");

            // Update options with newly detected options to our list
            var newDetectedStuff = detectedStuff.Distinct().Where(d => !Overlord.DlcAndModOptions.Contains(d)).ToList();
            var oldStuff = Overlord.DlcAndModOptions.ToList(); // Clone for reference later
            Overlord.DlcAndModOptions = Overlord.DlcAndModOptions.Concat(newDetectedStuff).Distinct().ToList();
            UpdateDlcAndModInfo(oldStuff);

            // Special case - force WotC if TLE was detected
            if (Overlord.DlcAndModOptions.Contains("Tactical Legacy Pack") && !Overlord.DlcAndModOptions.Contains("WotC"))
                Overlord.DlcAndModOptions.Add("WotC");

            if (displayMessage && newDetectedStuff.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendLine();
                sb.AppendLine();
                sb.Append(string.Join(Environment.NewLine, newDetectedStuff.Select(d => $"* {d}")));
                MessageBox.Show($"The following DLCs and Mods were detected and have been enabled:{sb}", "Detected DLCs/Mods", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Notify sepearately about unknown templates
                var unknownTypes = nameTypes.Where(nt => !Overlord.Templates.Any(t => t.Name == nt)).ToList();
                if (unknownTypes.Any())
                {
                    var characters = Overlord.CharacterPool.Characters.Where(c => c.Appearance.Properties.Any(p => unknownTypes.Contains(p.Data))).ToList();

                    sb = new StringBuilder();
                    sb.AppendLine("Unknown templates detected! This means one or more characters are using parts NOT yet supported by this program, So if you choose a DIFFERENT part, you will lose the original modded one.");
                    sb.AppendLine();
                    sb.AppendLine("The affected parts will still save ok unless you replace them manually.");
                    sb.AppendLine();
                    sb.AppendLine("The following characters are affected:");
                    sb.AppendLine();
                    sb.Append(string.Join(Environment.NewLine, characters.Take(10).Select(c => $"* {c.FullName}")));
                    if (characters.Count > 10)
                    {
                        sb.AppendLine();
                        sb.AppendLine($"And {characters.Count - 10} more...");
                    }
                    MessageBox.Show(sb.ToString(), "Unsupported Templates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            return detectedStuff;
        }

        private void DuplicateCharactersIgnored_EventHandler(List<Character> characters)
        {
            var sb = new StringBuilder();
            sb.AppendLine("The following characters already exist in the current pool and were ignored:");
            sb.AppendLine();
            sb.AppendLine(string.Join(Environment.NewLine, characters.OrderBy(c => c.FullName).Take(10).Select(c => $"* {c.FullName}")));
            if (characters.Count > 10)
            {
                sb.AppendLine();
                sb.AppendLine($"And {characters.Count - 10} more...");
            }

            if (Program.Settings.ShowWarnings)
                MessageBox.Show(sb.ToString(), "Duplicate Characters Ignored", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateDlcAndModInfo(List<string> oldOptions = null)
        {
            bool usedToHaveMoreColors = oldOptions != null && (oldOptions.Contains("More Hair Colors") || oldOptions.Contains("More Armor Colors"));
            bool nowHasMoreColors = Overlord.DlcAndModOptions.Contains("More Hair Colors") || Overlord.DlcAndModOptions.Contains("More Armor Colors");

            // Reload the palettes if we detected a change
            if (usedToHaveMoreColors != nowHasMoreColors)
                Overlord.LoadOrReloadPalettes();

            UpdateSoldierSelectables();
        }

        private void ToggleCommonControls(bool enable)
        {
            // These get disabled/enabled as a pool loads
            btnMergePool.Enabled = enable;
            btnAddCharacter.Enabled = enable;
            btnDlcMods.Enabled = enable;

            // Character controls
            ToggleCharacterControls(enable && _selectedCharacter != null);
        }

        private void ToggleCharacterControls(bool enable)
        {
            txtFirstName.Enabled = enable;
            txtNickName.Enabled = enable;
            txtLastName.Enabled = enable;
            cboGender.Enabled = enable && ((IEnumerable<object>)cboGender.DataSource).Count() > 1;
            cboRace.Enabled = enable && ((IEnumerable<object>)cboRace.DataSource).Count() > 1;
            cboAttitude.Enabled = enable && ((IEnumerable<object>)cboAttitude.DataSource).Count() > 1;
            cboSoldierType.Enabled = enable && ((IEnumerable<object>)cboSoldierType.DataSource).Count() > 1;
            cboPreferredClass.Enabled = enable && ((IEnumerable<object>)cboPreferredClass.DataSource).Count() > 1;
            cboCountry.Enabled = enable && ((IEnumerable<object>)cboCountry.DataSource).Count() > 1;
            cboVoice.Enabled = enable && ((IEnumerable<object>)cboVoice.DataSource).Count() > 1;
            chkCanBeSoldier.Enabled = enable;
            chkCanBeVIP.Enabled = enable;
            chkCanBeDarkVIP.Enabled = enable;
            txtBiography.Enabled = enable;

            // Enable/disable specific character buttons
            btnResetCharacter.Enabled = lstCharacters.SelectedIndex >= 0;
            btnDeleteSelected.Enabled = lstCharacters.SelectedIndex >= 0;
        }

        private void MarkCharactersModified(bool reset, params Character[] characters)
        {
            if (characters == null)
                return;

            foreach (var character in characters)
                character.IsDirty = !reset;
            Overlord.CharacterPool.IsDirty = !reset;
            RefreshCharacterListDataSource();
            UpdateDirtyStatus();
        }

        private void UpdateDirtyStatus()
        {
            lblFormStatus.Text = $"{Overlord.CharacterPool.PoolFileName?.Data ?? "(Unnamed Pool)"}{(Overlord.CharacterPool.IsDirty ? "*" : "")}";
            Text = $"Character Pool Editor{(Overlord.CharacterPool.IsDirty ? "*" : "")}";
            btnSavePool.Enabled = Overlord.CharacterPool.IsDirty;
        }

        #region Form and Control Events

        private void frmPoolEditor_Load(object sender, EventArgs e)
        {
            Overlord.LoadCustomizationTemplates();
            Overlord.LoadChoicesTemplates();
            Overlord.LoadOrReloadPalettes();

            if (filesToLoadOnLaunch != null)
                LoadPools(filesToLoadOnLaunch);

            // Initial dropdown populations
            UpdateSoldierSelectables();
        }

        private void btnMergePool_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog() { Filter = "bin files (*.bin)|*.bin", Multiselect = true, RestoreDirectory = true };
            if (ofd.ShowDialog() == DialogResult.OK)
                LoadPools(ofd.FileNames);
        }

        private void lstCharacters_SelectedChanged(object sender, EventArgs e)
        {
            if (lstCharacters.SelectedItem == _lastSelectedCharacter || _ignoreDataChanges)
                return;

            // Populate character information
            _ignoreDataChanges = true;
            txtFirstName.Text = _selectedCharacter?.FirstName.Data;
            txtNickName.Text = string.IsNullOrWhiteSpace(_selectedCharacter?.NickName.Data) ? string.Empty : _selectedCharacter.NickName.Data.Substring(1, _selectedCharacter.NickName.Data.Length - 2); // Strip apostrophes
            txtLastName.Text = _selectedCharacter?.LastName.Data;
            chkCanBeSoldier.Checked = _selectedCharacter?.CanBeSoldier.DataVal ?? false;
            chkCanBeVIP.Checked = _selectedCharacter?.CanBeVIP.DataVal ?? false;
            chkCanBeDarkVIP.Checked = _selectedCharacter?.CanBeDarkVIP.DataVal ?? false;
            txtBiography.Text = _selectedCharacter?.Biography.Data;
            lblCreatedOn.Text = $"Created On {_selectedCharacter?.CreatedOn.Data}";
            ResetDropdowns(); // Reset all dropdowns so nulls don't get populated with what we had selected from the last character
            _ignoreDataChanges = false;
            UpdateSoldierSelectables();

            ToggleCharacterControls(_selectedCharacter != null);
            _lastSelectedCharacter = lstCharacters.SelectedItem as Character;
        }

        private void btnResetCharacter_Click(object sender, EventArgs e)
        {
            Overlord.CharacterPool.ResetCharacters(_selectedCharacters);
            RefreshCharacterListDataSource();
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            Overlord.CharacterPool.RemoveCharacters(_selectedCharacters);
            MarkCharactersModified(false, _selectedCharacters);
            RefreshCharacterListDataSource();
        }

        private void btnAddCharacter_Click(object sender, EventArgs e)
        {
            Overlord.CharacterPool.AppendCharacters(Character.Create(_outputter, (++numCharactersAdded).ToString()));
            RefreshCharacterListDataSource();
        }

        private void btnDlcMods_Click(object sender, EventArgs e)
        {
            var currentOptions = Overlord.DlcAndModOptions.ToList(); // Clone for checking later
            var forcedOptions = DetectDlcsAndMods();
            var optionsForm = new frmPoolDlcModOptions(forcedOptions);
            optionsForm.ShowDialog();

            if (optionsForm.DialogResult == DialogResult.OK)
                UpdateDlcAndModInfo(currentOptions);
        }

        private void btnSavePool_Click(object sender, EventArgs e)
        {
            var dir1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"My Games\XCOM2 War of the Chosen\XComGame\CharacterPool\Importable");
            var dir2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"My Games\XCOM2\XComGame\CharacterPool\Importable");

            var dirInfo = new DirectoryInfo(dir1);
            if (!dirInfo.Exists) dirInfo = new DirectoryInfo(dir2);

            var sfd = new SaveFileDialog { DefaultExt = "bin", Filter = "bin files (*.bin)|*.bin", InitialDirectory = dirInfo.Exists ? dirInfo.FullName : string.Empty };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // Update PoolFileName first
                var fileInfo = new FileInfo(sfd.FileName);
                Overlord.CharacterPool.PoolFileName.Data = @$"CharacterPool\Importable\{fileInfo.Name}";
                Overlord.CharacterPool.Save(sfd.FileName);

                // Reset the pool - it has been saved!
                MarkCharactersModified(true, Overlord.CharacterPool.Characters.ToArray());

                MessageBox.Show("Pool saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ColorPicker_OnDrawItem(object sender, DrawItemEventArgs e)
        {
            var cbx = sender as ComboBox;
            if (e.Index >= 0 && e.Index < cbx.Items.Count && cbx.Items[e.Index] is Palette colorItem)
            {
                var focuedOrSelected = DrawItemState.Focus | DrawItemState.Selected;

                e.Graphics.FillRectangle(new SolidBrush(cbx.DisplayMember == "Primary" ? colorItem.Primary : colorItem.Secondary), e.Bounds);
                e.Graphics.DrawRectangle(new Pen((e.State & focuedOrSelected) == focuedOrSelected ? Color.Black : Color.White, 2f), e.Bounds);
            }
        }

        private void frmPoolEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((Overlord.CharacterPool?.IsDirty ?? false)
            && MessageBox.Show("You will lose your unsaved changes if you close this window. Are you sure you want to close it?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) != DialogResult.Yes)
                e.Cancel = true;
        }

        #endregion

        #region Data Source Refereshers

        private void RefreshCharacterListDataSource()
        {
            _ignoreDataChanges = true;
            var oldIndex = Math.Clamp(lstCharacters.SelectedIndex, -1, Overlord.CharacterPool.Characters.Count - 1);
            lstCharacters.SelectionMode = SelectionMode.One;
            lstCharacters.ValueMember = "ID";
            lstCharacters.DisplayMember = "FullName";
            lstCharacters.DataSource = Overlord.CharacterPool.Characters.OrderBy(c => c.FullName).ToList();
            _ignoreDataChanges = false;
            if (oldIndex != -1 || lstCharacters.Items.Count == 0)
            {
                // Force a selected refresh call here
                if (lstCharacters.SelectedIndex == oldIndex)
                    lstCharacters_SelectedChanged(null, null);
                else
                    lstCharacters.SelectedIndex = oldIndex;
            }
            lstCharacters.SelectionMode = SelectionMode.MultiExtended;

            lblNumCharacters.Text = $"Number of Characters: {Overlord.CharacterPool.NumCharacters}";
        }

        private void ResetDropdowns()
        {
            var allCombos = tbpCharInfo.Controls.OfType<ComboBox>().Concat(tbpHeadAppearance.Controls.OfType<ComboBox>()).Concat(tbpBodyAppearance.Controls.OfType<ComboBox>()).Concat(tbpWeaponAppearance.Controls.OfType<ComboBox>()).ToList();
            foreach (var cbx in allCombos)
                cbx.SelectedIndex = cbx.Items.Count > 0 ? 0 : -1;
        }

        #endregion

        #region Character Properties

        private void CharacterTextProperty_Validated(object sender, EventArgs e)
        {
            if (_selectedCharacter != null && !_ignoreDataChanges &&
            EditSoldier(_selectedCharacter.FirstName.Data, txtFirstName.Text.Trim(), d => _selectedCharacter.FirstName.Data = d)
            | EditSoldier(_selectedCharacter.NickName.Data, $"'{txtNickName.Text.Trim()}'", d => _selectedCharacter.NickName.Data = d)
            | EditSoldier(_selectedCharacter.LastName.Data, txtLastName.Text.Trim(), d => _selectedCharacter.LastName.Data = d)
            | EditSoldier(_selectedCharacter.Biography.Data, txtBiography.Text.Trim(), d => _selectedCharacter.Biography.Data = d))
                MarkCharactersModified(false, _selectedCharacters);
        }

        private void CharacterBoolProperty_Changed(object sender, EventArgs e)
        {
            if (_selectedCharacter != null && !_ignoreDataChanges &&
            EditSoldier(_selectedCharacter.CanBeSoldier.DataVal, chkCanBeSoldier.Checked, d => _selectedCharacter.CanBeSoldier.DataVal = d)
            | EditSoldier(_selectedCharacter.CanBeVIP.DataVal, chkCanBeVIP.Checked, d => _selectedCharacter.CanBeVIP.DataVal = d)
            | EditSoldier(_selectedCharacter.CanBeDarkVIP.DataVal, chkCanBeDarkVIP.Checked, d => _selectedCharacter.CanBeDarkVIP.DataVal = d))
                MarkCharactersModified(false, _selectedCharacters);
        }

        private void CharacterDropDownProperty_Changed(object sender, EventArgs e)
        {
            UpdateSoldierSelectables((ComboBox)sender);
        }

        /// <summary>
        /// This rather large function first refreshes the datasource of every applicable combobox based to enforce selection restrictions, then it applies any and all changes to the selected soldier
        /// </summary>
        /// <param name="sourceCbx">Which dropdown triggered this function so we know what value to persist. If null, everything possible will be written from the selected character</param>
        private void UpdateSoldierSelectables(ComboBox sourceCbx = null)
        {
            if (_ignoreDataChanges)
                return;
            _ignoreDataChanges = true;

            // Store the target values here for readability. Comboboxes should always try to reset to the character's old value unless the trigger WAS the combobox, in which case we set it to the selected value
            var soldierType = (sourceCbx == cboSoldierType ? cboSoldierType.SelectedValue as string : _selectedCharacter?.SoldierType.Data) ?? "Soldier";
            var gender = sourceCbx == cboGender && cboGender.SelectedValue is int ? (int)cboGender.SelectedValue : _selectedCharacter?.Appearance.Gender.DataVal ?? 1;
            var race = sourceCbx == cboRace && cboRace.SelectedValue is int ? (int)cboRace.SelectedValue : _selectedCharacter?.Appearance.Race.DataVal ?? 0;
            var attitude = sourceCbx == cboAttitude && cboAttitude.SelectedValue is int ? (int)cboAttitude.SelectedValue : _selectedCharacter?.Appearance.Attitude.DataVal ?? 0;
            var preferredClass = sourceCbx == cboPreferredClass ? cboPreferredClass.SelectedValue as string : _selectedCharacter?.PreferredClass.Data;
            var country = sourceCbx == cboCountry ? cboCountry.SelectedValue as string : _selectedCharacter?.Country.Data;
            var eyeColor = sourceCbx == cboEyeColor && cboEyeColor.SelectedValue is int ? (int)cboEyeColor.SelectedValue : _selectedCharacter?.Appearance.EyeColor.DataVal ?? 0;
            var voice = sourceCbx == cboVoice ? cboVoice.SelectedValue as string : _selectedCharacter?.Appearance.Voice.Data;
            var skinColor = sourceCbx == cboSkinColor && cboSkinColor.SelectedValue is int ? (int)cboSkinColor.SelectedValue : _selectedCharacter?.Appearance.SkinColor.DataVal ?? 0;
            var face = sourceCbx == cboFace ? cboFace.SelectedValue as string : _selectedCharacter?.Appearance.Head.Data;
            var helmet = sourceCbx == cboHelmet ? cboHelmet.SelectedValue as string : _selectedCharacter?.Appearance.Helmet.Data;
            var hair = sourceCbx == cboHair ? cboHair.SelectedValue as string : _selectedCharacter?.Appearance.Haircut.Data;
            var hairColor = sourceCbx == cboHairColor && cboHairColor.SelectedValue is int ? (int)cboHairColor.SelectedValue : _selectedCharacter?.Appearance.HairColor.DataVal ?? 0;
            var facialHair = sourceCbx == cboFacialHair ? cboFacialHair.SelectedValue as string : _selectedCharacter?.Appearance.Beard.Data;
            var upperFaceProps = sourceCbx == cboUpperFaceProps ? cboUpperFaceProps.SelectedValue as string : _selectedCharacter?.Appearance.FacePropUpper.Data;
            var lowerFaceProps = sourceCbx == cboLowerFaceProps ? cboLowerFaceProps.SelectedValue as string : _selectedCharacter?.Appearance.FacePropLower.Data;
            var facePaint = sourceCbx == cboFacePaint ? cboFacePaint.SelectedValue as string : _selectedCharacter?.Appearance.FacePaint.Data;
            var scars = sourceCbx == cboScars ? cboScars.SelectedValue as string : _selectedCharacter?.Appearance.Scars.Data;
            var armorColor = sourceCbx == cboArmorColor && cboArmorColor.SelectedValue is int ? (int)cboArmorColor.SelectedValue : _selectedCharacter?.Appearance.ArmorTint1.DataVal ?? 0;
            var secondArmorColor = sourceCbx == cboSecondArmorColor && cboSecondArmorColor.SelectedValue is int ? (int)cboSecondArmorColor.SelectedValue : _selectedCharacter?.Appearance.ArmorTint2.DataVal ?? 0;
            var armorPattern = sourceCbx == cboArmorPattern ? cboArmorPattern.SelectedValue as string : _selectedCharacter?.Appearance.ArmorPatterns.Data;
            var torso = sourceCbx == cboTorso ? cboTorso.SelectedValue as string : _selectedCharacter?.Appearance.Torso.Data;
            var torsoGear = sourceCbx == cboTorsoGear ? cboTorsoGear.SelectedValue as string : _selectedCharacter?.Appearance.TorsoDeco.Data;
            var arms = sourceCbx == cboArms ? cboArms.SelectedValue as string : _selectedCharacter?.Appearance.Arms.Data;
            var leftArm = sourceCbx == cboLeftArm ? cboLeftArm.SelectedValue as string : arms == "None" ? _selectedCharacter?.Appearance.LeftArm.Data : "None";
            var rightArm = sourceCbx == cboRightArm ? cboRightArm.SelectedValue as string : arms == "None" ? _selectedCharacter?.Appearance.RightArm.Data : "None";
            var leftForearm = sourceCbx == cboLeftForearm ? cboLeftForearm.SelectedValue as string : _selectedCharacter?.Appearance.LeftForearm.Data;
            var rightForearm = sourceCbx == cboRightForearm ? cboRightForearm.SelectedValue as string : _selectedCharacter?.Appearance.RightForearm.Data;
            var leftShoulder = sourceCbx == cboLeftShoulder ? cboLeftShoulder.SelectedValue as string : arms == "None" ? _selectedCharacter?.Appearance.LeftArmDeco.Data : "None";
            var rightShoulder = sourceCbx == cboRightShoulder ? cboRightShoulder.SelectedValue as string : arms == "None" ? _selectedCharacter?.Appearance.RightArmDeco.Data : "None";
            var leftArmTattoo = sourceCbx == cboLeftArmTattoo ? cboLeftArmTattoo.SelectedValue as string : _selectedCharacter?.Appearance.Tattoo_LeftArm.Data;
            var rightArmTattoo = sourceCbx == cboRightArmTattoo ? cboRightArmTattoo.SelectedValue as string : _selectedCharacter?.Appearance.Tattoo_RightArm.Data;
            var legs = sourceCbx == cboLegs ? cboLegs.SelectedValue as string : _selectedCharacter?.Appearance.Legs.Data;
            var thighs = sourceCbx == cboThighs ? cboThighs.SelectedValue as string : _selectedCharacter?.Appearance.Thighs.Data;
            var tattooColor = sourceCbx == cboTattooColor && cboTattooColor.SelectedValue is int ? (int)cboTattooColor.SelectedValue : _selectedCharacter?.Appearance.TattooTint.DataVal ?? 0;
            var weaponColor = sourceCbx == cboWeaponColor && cboWeaponColor.SelectedValue is int ? (int)cboWeaponColor.SelectedValue : _selectedCharacter?.Appearance.WeaponTint.DataVal ?? 0;
            var weaponPattern = sourceCbx == cboWeaponPattern ? cboWeaponPattern.SelectedValue as string : _selectedCharacter?.Appearance.WeaponPattern.Data;

            // Set every dropdown's datasource and value (from the character - if the old value no longer exists in the dropdown, it will default to the first selection), ensuring correct order for filters to work.
            SetComboBoxDataSourceAndValue(cboSoldierType, Overlord.FilteredTemplates.Where(c => c.PartType == "Pawn" && c.CharacterTemplate.Contains("Soldier") && !string.IsNullOrWhiteSpace(c.Display)).GroupBy(g => g.CharacterTemplate).Select(g => g.First()), soldierType);

            // Miscellaneous helper variables
            bool isSoldier = soldierType == "Soldier";
            bool isSpark = soldierType == "SparkSoldier";
            bool isReaper = soldierType == "ReaperSoldier";
            bool isSkirmisher = soldierType == "SkirmisherSoldier";
            bool isTemplar = soldierType == "TemplarSoldier";
            var armorTemplate = $"{(isSpark ? "Spark" : isReaper ? "Reaper" : isSkirmisher ? "Skirmisher" : isTemplar ? "Templar" : "Kevlar")}Armor";

            // Disable a couple non-dropdown controls if the soldier is a SPARK
            chkCanBeVIP.Enabled = _selectedCharacter != null && !isSpark;
            chkCanBeVIP.Checked &= !isSpark;
            chkCanBeDarkVIP.Enabled = _selectedCharacter != null && !isSpark;
            chkCanBeDarkVIP.Checked &= !isSpark;

            // Tier 2 - Soldier type affects these options
            SetComboBoxDataSourceAndValue(cboGender, Template.GetIndexes(Overlord.Choices.Where(c => c.PartType == "Gender").ToList()).Skip(isSpark ? 0 : 1).Take(isSpark ? 1 : 2), gender);
            SetComboBoxDataSourceAndValue(cboRace, Template.GetIndexes(isSpark ? new List<Template> { Overlord.Choices.First(c => c.Name == "eRace_None") } : Overlord.Choices.Where(c => c.PartType == "Race" && c.Name != "eRace_None").ToList()), race);
            SetComboBoxDataSourceAndValue(cboAttitude, Template.GetIndexes(isSpark || isTemplar ? new List<Template> { Overlord.Choices.First(c => c.Name == "Personality_None") } : Overlord.Choices.Where(c => c.PartType == "Attitude" && c.Name != "Personality_None").ToList()), attitude);
            SetComboBoxDataSourceAndValue(cboPreferredClass, Overlord.Choices.Where(c => c.PartType == "Class" && c.CharacterTemplate == soldierType), preferredClass);
            SetComboBoxDataSourceAndValue(cboCountry, Overlord.Choices.Where(c => c.PartType == "Country" && c.CharacterTemplate == soldierType), country);

            // Helpers that may have not been set until now
            var genderStr = Overlord.Choices.Where(c => c.PartType == "Gender").ElementAt((int)(cboGender.SelectedValue ?? 0)).Name;
            var raceStr = Overlord.Choices.Where(c => c.PartType == "Race").ElementAt((int)(cboRace.SelectedValue ?? 0) + 1).Name; // Skip "None"
            var torsoUnderlay = isSpark ? torso : Overlord.FilteredTemplates.First(t => t.PartType == "Torso" && t.ArmorTemplate == "Underlay" && t.IsGender(genderStr)).Name;
            var armsUnderlay = isSpark ? arms : Overlord.FilteredTemplates.First(t => t.PartType == "Arms" && t.ArmorTemplate == "Underlay" && t.IsGender(genderStr)).Name;
            var legsUnderlay = isSpark ? legs : Overlord.FilteredTemplates.First(t => t.PartType == "Legs" && t.ArmorTemplate == "Underlay" && t.IsGender(genderStr)).Name;

            // Some of these next properties are tier 3 - multiple sources above can affect them
            SetComboBoxDataSourceAndValue(cboVoice, Overlord.FilteredTemplates.Where(t => t.PartType == "Voice" && !string.IsNullOrEmpty(t.Display) && t.IsGender(genderStr) && (soldierType == "Soldier" || t.CharacterTemplate == soldierType)).GroupBy(g => g.Name).Select(g => g.First()), voice);
            SetComboBoxDataSourceAndValue(cboSkinColor, Overlord.Palettes.Where(p => p.PaletteType == Palette.ePaletteType.SkinColor && p.PaletteSubType == race), skinColor, !isSpark);
            SetComboBoxDataSourceAndValue(cboEyeColor, Overlord.Palettes.Where(p => p.PaletteType == Palette.ePaletteType.EyeColor), eyeColor, !isSpark);
            SetComboBoxDataSourceAndValue(cboFace, Overlord.FilteredTemplates.Where(t => t.PartType == "Head" && string.IsNullOrEmpty(t.Tech) && !string.IsNullOrEmpty(t.Display) && t.IsGender(genderStr)
                && (isSpark || t.IsRace(raceStr)) && (string.IsNullOrEmpty(t.CharacterTemplate) == (!isSpark && !isSkirmisher) || t.CharacterTemplate == soldierType)).GroupBy(g => g.Name).Select(g => g.First()), face);
            SetComboBoxDataSourceAndValue(cboHelmet, Overlord.FilteredTemplates.Where(t => t.PartType == "Helmets" && string.IsNullOrEmpty(t.CharacterTemplate) && !string.IsNullOrEmpty(t.Display) && t.IsGender(genderStr)).GroupBy(g => g.Name).Select(g => g.First()), helmet, !isSpark);
            SetComboBoxDataSourceAndValue(cboTorso, Template.GetIndexes(Overlord.FilteredTemplates.Where(t => t.PartType == "Torso" && t.CharacterTemplate == soldierType && t.ArmorTemplate == armorTemplate && t.IsGender(genderStr)).GroupBy(g => g.Name).Select(g => g.First()).ToList()), torso);
            SetComboBoxDataSourceAndValue(cboTorsoGear, Overlord.FilteredTemplates.Where(t => t.PartType == "TorsoDeco" && t.CharacterTemplate == soldierType && t.IsGender(genderStr)), torsoGear == "None" ? null : torsoGear); // Force a selection if possible

            // More helpers that may have not been set until now
            var helmetInfo = Overlord.HelmetInfos.FirstOrDefault(h => h.Name == cboHelmet.SelectedValue as string);
            bool hideHair = helmetInfo?.HideHair ?? false;
            bool hideUpperFaceProps = helmetInfo?.HideUpperFaceProps ?? false;
            bool hideLowerFaceProps = helmetInfo?.HideLowerFaceProps ?? false;
            bool hideFacialHair = helmetInfo?.HideFacialHair ?? false;
            bool hasAnyArmGear = new[] { leftArm, rightArm, leftForearm, rightForearm }.Any(s => !string.IsNullOrWhiteSpace(s) && s != "None");
            bool hideArms = (cboTorso.SelectedItem as Template)?.Origin == "Anarchy's Children";

            // And on to tier 4 & misc properties. Tiers 2 and 3 can sometimes affect these
            SetComboBoxDataSourceAndValue(cboHair, Overlord.FilteredTemplates.Where(t => t.PartType == "Hair" && !string.IsNullOrEmpty(t.Display) && t.IsGender(genderStr)).GroupBy(g => g.Name).Select(g => g.First()), hair, !isSpark && !isSkirmisher && !hideHair);
            SetComboBoxDataSourceAndValue(cboHairColor, Overlord.Palettes.Where(p => p.PaletteType == Palette.ePaletteType.HairColor), hairColor, cboHair.Enabled);
            SetComboBoxDataSourceAndValue(cboFacialHair, Overlord.FilteredTemplates.Where(t => t.PartType == "Beards" && !t.SpecializedType).GroupBy(g => g.Name).Select(g => g.First()), facialHair, gender == 1 && !isSpark && !isSkirmisher && !hideFacialHair);
            SetComboBoxDataSourceAndValue(cboUpperFaceProps, Overlord.FilteredTemplates.Where(t => t.PartType == "FacePropsUpper" && !t.SpecializedType && t.IsGender(genderStr, true)).GroupBy(g => g.Name).Select(g => g.First()), upperFaceProps, !isSpark && !isSkirmisher && !hideUpperFaceProps);
            SetComboBoxDataSourceAndValue(cboLowerFaceProps, Overlord.FilteredTemplates.Where(t => t.PartType == "FacePropsLower" && !t.SpecializedType && t.IsGender(genderStr, true)).GroupBy(g => g.Name).Select(g => g.First()), lowerFaceProps, !isSpark && !hideLowerFaceProps);
            SetComboBoxDataSourceAndValue(cboFacePaint, Overlord.FilteredTemplates.Where(t => t.PartType == "Facepaint").GroupBy(g => g.Name).Select(g => g.First()), facePaint, !isSpark);
            SetComboBoxDataSourceAndValue(cboScars, Overlord.FilteredTemplates.Where(t => (string.IsNullOrEmpty(t.CharacterTemplate) == !isSkirmisher || t.CharacterTemplate == soldierType) && t.PartType == "Scars").GroupBy(g => g.Name).Select(g => g.First()), scars, !isSpark);
            SetComboBoxDataSourceAndValue(cboArmorColor, Overlord.Palettes.Where(p => p.PaletteType == Palette.ePaletteType.ArmorColor), armorColor);
            SetComboBoxDataSourceAndValue(cboSecondArmorColor, Overlord.Palettes.Where(p => p.PaletteType == Palette.ePaletteType.ArmorColor), secondArmorColor);
            SetComboBoxDataSourceAndValue(cboArmorPattern, Overlord.FilteredTemplates.Where(t => t.PartType == "Patterns").GroupBy(g => g.Name).Select(g => g.First()), armorPattern);
            SetComboBoxDataSourceAndValue(cboArms, Template.GetIndexes(Overlord.FilteredTemplates.Where(t => !hideArms && t.PartType == "Arms" && t.IsGender(genderStr) && t.ArmorTemplate == armorTemplate).GroupBy(g => g.Name).Select(g => g.First()).ToList()), hasAnyArmGear ? "None" : arms);

            // And MORE helpers that may not have been set until now
            bool forceArmGearActive = hasAnyArmGear || !cboArms.Enabled;
            var armInfos = new[] { Overlord.ArmInfos.FirstOrDefault(a => a.Name == cboLeftArm.SelectedValue as string), Overlord.ArmInfos.FirstOrDefault(a => a.Name == cboRightArm.SelectedValue as string) };
            bool[] hideForearms = new[] { armInfos[0]?.HideForearms ?? false, armInfos[1]?.HideForearms ?? false };

            // Tier 5. Arms are weird, and I'm too lazy to explain it
            SetComboBoxDataSourceAndValue(cboLeftArm, Overlord.FilteredTemplates.Where(t => t.PartType == "LeftArm" && t.IsGender(genderStr) && t.CharacterTemplate == soldierType && (t.ArmorTemplate == null || t.ArmorTemplate == armorTemplate)), forceArmGearActive && leftArm == "None" ? null : leftArm);
            SetComboBoxDataSourceAndValue(cboRightArm, Overlord.FilteredTemplates.Where(t => t.PartType == "RightArm" && t.IsGender(genderStr) && t.CharacterTemplate == soldierType && (t.ArmorTemplate == null || t.ArmorTemplate == armorTemplate)), forceArmGearActive && rightArm == "None" ? null : rightArm);
            SetComboBoxDataSourceAndValue(cboLeftForearm, Overlord.FilteredTemplates.Where(t => t.PartType == "LeftForearm" && t.IsGender(genderStr) && t.CharacterTemplate == soldierType && (t.ArmorTemplate == null || t.ArmorTemplate == armorTemplate)), forceArmGearActive && leftForearm == "None" ? null : leftForearm, !hideForearms[0]);
            SetComboBoxDataSourceAndValue(cboRightForearm, Overlord.FilteredTemplates.Where(t => t.PartType == "RightForearm" && t.IsGender(genderStr) && t.CharacterTemplate == soldierType && (t.ArmorTemplate == null || t.ArmorTemplate == armorTemplate)), forceArmGearActive && rightForearm == "None" ? null : rightForearm, !hideForearms[1]);
            SetComboBoxDataSourceAndValue(cboLeftShoulder, Overlord.FilteredTemplates.Where(t => t.PartType == "LeftArmDeco" && t.IsGender(genderStr) && t.CharacterTemplate == soldierType && (t.ArmorTemplate == null || t.ArmorTemplate == armorTemplate)), forceArmGearActive && leftShoulder == "None" ? null : leftShoulder, forceArmGearActive);
            SetComboBoxDataSourceAndValue(cboRightShoulder, Overlord.FilteredTemplates.Where(t => t.PartType == "RightArmDeco" && t.IsGender(genderStr) && t.CharacterTemplate == soldierType && (t.ArmorTemplate == null || t.ArmorTemplate == armorTemplate)), forceArmGearActive && rightShoulder == "None" ? null : rightShoulder, forceArmGearActive);
            SetComboBoxDataSourceAndValue(cboLeftArmTattoo, Overlord.FilteredTemplates.Where(t => t.PartType == "Tattoos").GroupBy(g => g.Name).Select(t => t.First()), leftArmTattoo, !isSpark);
            SetComboBoxDataSourceAndValue(cboRightArmTattoo, Overlord.FilteredTemplates.Where(t => t.PartType == "Tattoos").GroupBy(g => g.Name).Select(t => t.First()), rightArmTattoo, !isSpark);
            SetComboBoxDataSourceAndValue(cboLegs, Template.GetIndexes(Overlord.FilteredTemplates.Where(t => t.PartType == "Legs" && t.IsGender(genderStr) && t.CharacterTemplate == soldierType && (t.ArmorTemplate == armorTemplate || (!isSpark && !isSoldier))).GroupBy(g => g.Name).Select(t => t.First()).ToList()), legs);
            SetComboBoxDataSourceAndValue(cboThighs, Overlord.FilteredTemplates.Where(t => t.PartType == "Thighs" && t.IsGender(genderStr) && t.CharacterTemplate == soldierType && (t.ArmorTemplate == null || t.ArmorTemplate == armorTemplate)), thighs == "None" ? null : thighs); // These manual "None" checks are getting kind of sloppy...
            SetComboBoxDataSourceAndValue(cboTattooColor, Overlord.Palettes.Where(p => p.PaletteType == Palette.ePaletteType.ArmorColor), tattooColor);
            SetComboBoxDataSourceAndValue(cboWeaponColor, Overlord.Palettes.Where(p => p.PaletteType == Palette.ePaletteType.ArmorColor), weaponColor);
            SetComboBoxDataSourceAndValue(cboWeaponPattern, Overlord.FilteredTemplates.Where(t => t.PartType == "Patterns").GroupBy(g => g.Name).Select(g => g.First()), weaponPattern);

            // If any of the following edits are successful (using non-short-circuiting operators), mark the character as modified and refresh the list
            if (_selectedCharacter != null &&
            // Character Info tab
            EditSoldier(_selectedCharacter.SoldierType.Data, soldierType, (d) => _selectedCharacter.SoldierType.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.Gender.DataVal, cboGender.SelectedValue as int? ?? 1, (d) => _selectedCharacter.Appearance.Gender.DataVal = d)
            | EditSoldier(_selectedCharacter.Appearance.Race.DataVal, cboRace.SelectedValue as int? ?? 0, (d) => _selectedCharacter.Appearance.Race.DataVal = d)
            | EditSoldier(_selectedCharacter.Appearance.Attitude.DataVal, cboAttitude.SelectedValue as int? ?? 0, (d) => _selectedCharacter.Appearance.Attitude.DataVal = d)
            | EditSoldier(_selectedCharacter.PreferredClass.Data, cboPreferredClass.SelectedValue as string, (d) => _selectedCharacter.PreferredClass.Data = d)
            | EditSoldier(_selectedCharacter.Country.Data, cboCountry.SelectedValue as string, (d) => _selectedCharacter.Country.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.Flag.Data, cboCountry.SelectedValue as string, (d) => _selectedCharacter.Appearance.Flag.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.Voice.Data, cboVoice.SelectedValue as string, (d) => _selectedCharacter.Appearance.Voice.Data = d)

            // Head Appearance tab
            | EditSoldier(_selectedCharacter.Appearance.SkinColor.DataVal, cboSkinColor.SelectedValue as int? ?? 0, (d) => _selectedCharacter.Appearance.SkinColor.DataVal = d)
            | EditSoldier(_selectedCharacter.Appearance.EyeColor.DataVal, cboEyeColor.SelectedValue as int? ?? 0, (d) => _selectedCharacter.Appearance.EyeColor.DataVal = d)
            | EditSoldier(_selectedCharacter.Appearance.Head.Data, cboFace.SelectedValue as string, (d) => _selectedCharacter.Appearance.Head.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.Helmet.Data, cboHelmet.SelectedValue as string, (d) => _selectedCharacter.Appearance.Helmet.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.Haircut.Data, cboHair.SelectedValue as string, (d) => _selectedCharacter.Appearance.Haircut.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.HairColor.DataVal, cboHairColor.SelectedValue as int? ?? 0, (d) => _selectedCharacter.Appearance.HairColor.DataVal = d)
            | EditSoldier(_selectedCharacter.Appearance.Beard.Data, cboFacialHair.SelectedValue as string, (d) => _selectedCharacter.Appearance.Beard.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.FacePropUpper.Data, cboUpperFaceProps.SelectedValue as string, (d) => _selectedCharacter.Appearance.FacePropUpper.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.FacePropLower.Data, cboLowerFaceProps.SelectedValue as string, (d) => _selectedCharacter.Appearance.FacePropLower.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.FacePaint.Data, cboFacePaint.SelectedValue as string, (d) => _selectedCharacter.Appearance.FacePaint.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.Scars.Data, cboScars.SelectedValue as string, (d) => _selectedCharacter.Appearance.Scars.Data = d)

            // Body Appearance tab
            | EditSoldier(_selectedCharacter.Appearance.ArmorTint1.DataVal, cboArmorColor.SelectedValue as int? ?? 0, (d) => _selectedCharacter.Appearance.ArmorTint1.DataVal = d)
            | EditSoldier(_selectedCharacter.Appearance.ArmorTint2.DataVal, cboSecondArmorColor.SelectedValue as int? ?? 0, (d) => _selectedCharacter.Appearance.ArmorTint2.DataVal = d)
            | EditSoldier(_selectedCharacter.Appearance.ArmorPatterns.Data, cboArmorPattern.SelectedValue as string, (d) => _selectedCharacter.Appearance.ArmorPatterns.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.Torso.Data, cboTorso.SelectedValue as string, (d) => _selectedCharacter.Appearance.Torso.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.TorsoUnderlay.Data, torsoUnderlay, (d) => _selectedCharacter.Appearance.TorsoUnderlay.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.TorsoDeco.Data, cboTorsoGear.SelectedValue as string, (d) => _selectedCharacter.Appearance.TorsoDeco.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.Arms.Data, cboArms.SelectedValue as string, (d) => _selectedCharacter.Appearance.Arms.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.ArmsUnderlay.Data, armsUnderlay, (d) => _selectedCharacter.Appearance.ArmsUnderlay.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.LeftArm.Data, cboLeftArm.SelectedValue as string, (d) => _selectedCharacter.Appearance.LeftArm.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.RightArm.Data, cboRightArm.SelectedValue as string, (d) => _selectedCharacter.Appearance.RightArm.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.LeftForearm.Data, cboLeftForearm.SelectedValue as string, (d) => _selectedCharacter.Appearance.LeftForearm.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.RightForearm.Data, cboRightForearm.SelectedValue as string, (d) => _selectedCharacter.Appearance.RightForearm.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.LeftArmDeco.Data, cboLeftShoulder.SelectedValue as string, (d) => _selectedCharacter.Appearance.LeftArmDeco.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.RightArmDeco.Data, cboRightShoulder.SelectedValue as string, (d) => _selectedCharacter.Appearance.RightArmDeco.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.Tattoo_LeftArm.Data, cboLeftArmTattoo.SelectedValue as string, (d) => _selectedCharacter.Appearance.Tattoo_LeftArm.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.Tattoo_RightArm.Data, cboRightArmTattoo.SelectedValue as string, (d) => _selectedCharacter.Appearance.Tattoo_RightArm.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.Legs.Data, cboLegs.SelectedValue as string, (d) => _selectedCharacter.Appearance.Legs.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.LegsUnderlay.Data, legsUnderlay, (d) => _selectedCharacter.Appearance.LegsUnderlay.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.Thighs.Data, cboThighs.SelectedValue as string, (d) => _selectedCharacter.Appearance.Thighs.Data = d)
            | EditSoldier(_selectedCharacter.Appearance.TattooTint.DataVal, cboTattooColor.SelectedValue as int? ?? 0, (d) => _selectedCharacter.Appearance.TattooTint.DataVal = d)

            // Weapon Appearance tab
            | EditSoldier(_selectedCharacter.Appearance.WeaponTint.DataVal, cboWeaponColor.SelectedValue as int? ?? 0, (d) => _selectedCharacter.Appearance.WeaponTint.DataVal = d)
            | EditSoldier(_selectedCharacter.Appearance.WeaponPattern.Data, cboWeaponPattern.SelectedValue as string, (d) => _selectedCharacter.Appearance.WeaponPattern.Data = d))
                MarkCharactersModified(false, _selectedCharacter);
            _ignoreDataChanges = false;
        }

        private void SetComboBoxDataSourceAndValue<TS, TV>(ComboBox cbx, IEnumerable<TS> ds, TV targetValue, bool enabled = true)
        {
            // TODO: Should we dynamically add unknown values to our global templates list? If so, we should probably highlight them red or something to show they are not supported, with a tooltip on hover?
            var newDs = ds.ToList();
            if (newDs is List<Template> templateList)
            {
                // Automatically add "None" if we need it (if the type is correct, our value member is "Name", we don't already have a "None" in the list, and either (target value is "None" or we have no items))
                if (cbx.ValueMember == "Name" && !templateList.Any(t => t.Name == "None") && (targetValue as string == "None" || !templateList.Any()))
                    (newDs as List<Template>).Insert(0, Overlord.Templates.First(t => t.Name == "None"));

                // Get the values of each of the target template property using reflection. If our target value isn't in that list, create it
                if (targetValue != null)
                {
                    var values = newDs.Select(t => t.GetType().GetProperty(cbx.ValueMember).GetValue(t) as string).ToList();
                    if (!values.Contains(targetValue as string))
                    {
                        string parseLine = cbx.ValueMember == "Name" ? $"TemplateName={targetValue}" : $"TemplateName=TEMP_UNKNOWN,{cbx.ValueMember}={targetValue}";
                        var tempTemplate = Template.Parse(parseLine, new Dictionary<string, string>(), null);
                        (newDs as List<Template>).Insert(0, tempTemplate);
                    }
                }
            }

            // Only set datasource if it's different
            var existingDs = cbx.DataSource as IList<TS>;
            if (existingDs == null || !newDs.SequenceEqual(existingDs))
                cbx.DataSource = newDs;

            // Only set the selected value if it's not null and not already equal to the existing value
            if (targetValue != null && !targetValue.Equals(cbx.SelectedValue))
                cbx.SelectedValue = targetValue;

            cbx.Enabled = enabled && _selectedCharacter != null && cbx.Items.Count > 1;

            // If we still don't have a value at this point, select the top item
            if (cbx.SelectedValue == null && newDs.Count > 0)
                cbx.SelectedIndex = 0;
        }

        private bool EditSoldier<T>(T oldVal, T newVal, Action<T> setData)
        {
            // Don't edit anything if the values are matching
            if ((oldVal == null && newVal == null) || (oldVal != null && oldVal.Equals(newVal)))
                return false;

            setData(newVal);

            // Don't mark edited for replacing -1s with 0s or nulls with "None"s
            return !(oldVal is int oInt && oInt == -1 && newVal is int nInt && nInt == 0)
                && !(oldVal == null && newVal is string nStr && nStr == "None");
        }

        #endregion
    }
}