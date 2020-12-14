using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace EasySave_graphical
{
    public partial class graphical_interface : Form
    {
        Controller controller;

        // Initialize ---------------------------------------------------------------------------------------------------------------------------------------------------
        public graphical_interface()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("");
            InitializeComponent();
            //reloadListView();
        }
        public void SetController(Controller controller)
        {
            this.controller = controller;
        }
        // Homepage ---------------------------------------------------------------------------------------------------------------------------------------------------
        private void homepageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void easySaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetStripColor();
            TabControl.SelectedIndex = 0;
        }

        private void strip_about_Click(object sender, EventArgs e)
        {
            MessageBox.Show("EasySave 2.0 - MiProSoft", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Add Section ---------------------------------------------------------------------------------------------------------------------------------------------------
        String add_sourceFolder = "";
        String add_destinationFolder = "";
        String backupType = "";

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabControl.SelectedIndex = 1;
            resetStripColor();
            strip_add.BackColor = ColorTranslator.FromHtml("#CCCAC8");
        }

        private void add_source_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog add_browser_source = new FolderBrowserDialog();
            if (add_browser_source.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // We get the selected source path of the user
                add_sourceFolder = add_browser_source.SelectedPath;
                add_source.Text = "Source : " + add_sourceFolder;
            }

        }

        private void add_destination_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog add_browser_destination = new FolderBrowserDialog();
            if (add_browser_destination.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // We get the selected source path of the user
                add_destinationFolder = add_browser_destination.SelectedPath;
                add_destination.Text = "Destination : " + add_destinationFolder;
            }
        }

        private Boolean addIsValid = true;
        private String errorMessage_add = "";

        private void button1_Click(object sender, EventArgs e)
        {
            addIsValid = true;
            errorMessage_add = "";

            if (add_full.Checked == false && add_differential.Checked == false)
            {
                addIsValid = false;
                errorMessage_add = Properties.Resources.error_message1;
            }
            else if (add_full.Checked == true)
            {
                backupType = "full";
            }
            else
            {
                backupType = "differential";
            }

            if (add_name.Text == "")
            {
                addIsValid = false;
                errorMessage_add = Properties.Resources.error_message2;
            }

            if (add_sourceFolder.Length < 2)
            {
                addIsValid = false;
                errorMessage_add = Properties.Resources.error_message3;
            }

            if (add_destinationFolder.Length < 2)
            {
                addIsValid = false;
                errorMessage_add = Properties.Resources.error_message4;
            }

            if (addIsValid)
            {
                // add the backup
                BackupJob backupJob = new BackupJob(add_name.Text, add_sourceFolder, add_destinationFolder, (backupType == "full"), parseUserInputAsList(add_extension.Text));
                if (this.controller.Model.createBackupJob(backupJob))
                {
                    MessageBox.Show(Properties.Resources.success, Properties.Resources.information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reloadListView();
                }
                else
                {
                    MessageBox.Show(errorMessage_add, Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(errorMessage_add, Properties.Resources.information, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Edit section ---------------------------------------------------------------------------------------------------------------------------------------------------
        String edit_sourceFolderValue = "";
        String edit_destinationFolderValue = "";
        String edit_backupType = "";
        private void edit_backup_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When the user has chosen a backup we put every information we have in the form
            edit_name.Text = this.controller.Model.BackupJobList[this.edit_backup_list.SelectedIndex].Name;
            edit_name.ReadOnly = false;

            edit_sourceFolder.Enabled = true;
            edit_destinationFolder.Enabled = true;
            edit_sourceFolderValue = this.controller.Model.BackupJobList[this.edit_backup_list.SelectedIndex].Source;
            edit_destinationFolderValue = this.controller.Model.BackupJobList[this.edit_backup_list.SelectedIndex].Destination;
            this.edit_sourceFolder.Text = this.controller.Model.BackupJobList[this.edit_backup_list.SelectedIndex].Source;
            this.edit_destinationFolder.Text = this.controller.Model.BackupJobList[this.edit_backup_list.SelectedIndex].Destination;

            edit_full.Checked = this.controller.Model.BackupJobList[this.edit_backup_list.SelectedIndex].IsFull;
            edit_full.Enabled = true;
            edit_differential.Enabled = true;
            edit_differential.Checked = !this.controller.Model.BackupJobList[this.edit_backup_list.SelectedIndex].IsFull;

            edit_extension.ReadOnly = false;
            edit_extension.Text = string.Join(", ", this.controller.Model.BackupJobList[this.edit_backup_list.SelectedIndex].ToBeEncryptedFileExtensions);
            edit_label.Enabled = true;
        }

        private Boolean editIsValid = true;
        private String errorMessage = "";
        private void edit_label_Click(object sender, EventArgs e)
        {
            editIsValid = true;
            errorMessage = "";

            if (edit_full.Checked == false && edit_differential.Checked == false)
            {
                editIsValid = false;
                errorMessage = Properties.Resources.error_message1;
            }
            else if (edit_full.Checked == true)
            {
                edit_backupType = "full";
            }
            else
            {
                edit_backupType = "differential";
            }

            if (edit_name.Text == "")
            {
                editIsValid = false;
                errorMessage = Properties.Resources.error_message2;
            }

            if (edit_sourceFolderValue.Length < 2)
            {
                editIsValid = false;
                errorMessage = Properties.Resources.error_message3;
            }

            if (edit_destinationFolderValue.Length < 2)
            {
                editIsValid = false;
                errorMessage = Properties.Resources.error_message4;
            }

            if (editIsValid)
            {
                // Edit the backup
                this.controller.Model.editBackupJob(this.edit_backup_list.SelectedIndex, edit_name.Text, edit_sourceFolderValue, edit_destinationFolderValue, (edit_backupType == "full"), parseUserInputAsList(edit_extension.Text));
                MessageBox.Show(Properties.Resources.all_good, Properties.Resources.information,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(errorMessage, Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void edit_sourceFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog edit_browser_source = new FolderBrowserDialog();
            if (edit_browser_source.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // We get the selected source path of the user
                edit_sourceFolderValue = edit_browser_source.SelectedPath;
                edit_sourceFolder.Text = Properties.Resources.source + " : " + edit_sourceFolderValue;
            }
        }
        private void edit_destinationFolder_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog edit_browser_destination = new FolderBrowserDialog();
            if (edit_browser_destination.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // We get the selected source path of the user
                edit_destinationFolderValue = edit_browser_destination.SelectedPath;
                edit_sourceFolder.Text = Properties.Resources.destination + " : " + edit_sourceFolderValue;
            }
        }

        // Delete ---------------------------------------------------------------------------------------------------------------------------------------------------
        private void delete_validation_Click(object sender, EventArgs e)
        {
            if (delete_backup_list.SelectedItems.Count == 1)
            {
                var result = MessageBox.Show(Properties.Resources.delete_check, Properties.Resources.information,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Exclamation);

                if (result == DialogResult.Yes)
                {
                    // Call the model to delete the backup
                    this.controller.Model.deleteBackupJob(delete_backup_list.SelectedIndex);
                    MessageBox.Show(Properties.Resources.delete_confirm + " : " + delete_backup_list.SelectedItem);
                    reloadListView();
                }
            }
            else
            {
                delete_warning.Visible = true;
            }
        }

        private void delete_backup_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            delete_warning.Visible = false;
        }

        // Execute ---------------------------------------------------------------------------------------------------------------------------------------------------
        private void execute_play_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName(this.controller.Model.processNameToWatch);
            //Console.WriteLine(processes.Length);
            if (processes.Length >= 1)
            {
                MessageBox.Show(Properties.Resources.business_software_error, Properties.Resources.business_software, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (processes.Length == 0)
            {
                execute_stop.Enabled = true;
                execute_pause.Enabled = true;
                if (execute_backup_list.SelectedItems.Count > 0) // This is an array containing every backup the user want's to select.
                {
                    List<int> slectedBUJ = new List<int>();
                    List<String> dirFullForDiff = new List<string>();
                    foreach (var i in execute_backup_list.SelectedItems)
                    {
                        slectedBUJ.Add(execute_backup_list.Items.IndexOf(i));// Add selected indexes to the List<int>
                        if (!this.controller.Model.BackupJobList[slectedBUJ[slectedBUJ.Count - 1]].IsFull)
                        {
                            using (var fbd = new FolderBrowserDialog())
                            {

                                MessageBox.Show(Properties.Resources.reference + this.controller.Model.BackupJobList[slectedBUJ[slectedBUJ.Count - 1]].Name, Properties.Resources.information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DialogResult result = fbd.ShowDialog();
                                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                                {
                                    dirFullForDiff.Add(fbd.SelectedPath);
                                }
                                else
                                {
                                    while (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                                    {
                                        MessageBox.Show(Properties.Resources.please + this.controller.Model.BackupJobList[slectedBUJ[slectedBUJ.Count - 1]].Name, Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        result = fbd.ShowDialog();
                                    }
                                }
                            }
                        }
                    }
                    this.controller.Model.executeBUJList(slectedBUJ, dirFullForDiff);
                }
                else
                {
                    execute_warning.Visible = true;
                }
            }

        }

        private void execute_backup_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            execute_warning.Visible = false;
        }

        // Progress bar for each backup thread
        readonly List<ProgressBar> threadsTracking = new List<ProgressBar>();

        public delegate void addProgressBarDelegate();
        public addProgressBarDelegate trackingDelegate;

        public void addProgressBar(int id)
        {
            trackingDelegate = new addProgressBarDelegate(() => { newProgressBar(id); });
            Invoke(trackingDelegate);
        }

        public void newProgressBar(int id)
        {
            ProgressBar myProgressBar = new ProgressBar();
            myProgressBar.Name = "ProgressBar" + id; // Should add the ID here
            myProgressBar.Width = 190;
            threadsTracking.Add(myProgressBar);
            trackingPanel.Controls.Add(myProgressBar);
        }

        public delegate void removeProgressBarDelegate();
        public removeProgressBarDelegate removeTrackingDelegate;

        public void removeProgressBar()
        {
            removeTrackingDelegate = new removeProgressBarDelegate(deleteProgressBar);
            Invoke(removeTrackingDelegate);
        }
        public void deleteProgressBar()
        {
            trackingPanel.Controls.Clear();
        }


        public delegate void updateProgressBarDelegate();
        public updateProgressBarDelegate refreshTrackingDelegate;

        public void updateTracking(int id, int percent, string bname, string status = "")
        {
            refreshTrackingDelegate = new updateProgressBarDelegate(() => { refreshTracking(id, percent, bname, status); });
            Invoke(refreshTrackingDelegate);
        }
        public void refreshTracking(int id, int percent, string bname, string status)
        {
            // Update the value
            ProgressBar item = this.trackingPanel.Controls.Find("ProgressBar" + id.ToString(), false)[0] as ProgressBar;

            if (status == "end" || percent == 100)
            {
                //TODO: add persistent "complete"
                item.CreateGraphics().DrawString(bname + " : " + percent.ToString() + "COMPLETE", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(item.Width / 2 - 40, item.Height / 2 - 7));
                item.Value = 100;

            }
            else
            {
                item.CreateGraphics().DrawString(bname + " : " + percent.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(item.Width / 2 - 20, item.Height / 2 - 7));
                item.Value = percent;
            }
        }

        // Pause the backup
        public void UpdateBackupPauseList(string name)
        {
            //Add a backup to the collection -> execute_backup_pause
            execute_backup_pause.Items.Add(name);
        }

        public delegate void removePauseBackupList();
        public removePauseBackupList removePause;

        public void clearBackupPauseListDelegate()
        {
            removePause = new removePauseBackupList(ClearBackupPauseList);
            Invoke(removePause);
        }
        public void ClearBackupPauseList()
        {
            execute_backup_pause.Items.Clear();
        }

        private void execute_pause_Click(object sender, EventArgs e)
        {
            execute_pause.Enabled = false;
            execute_resume.Visible = true;
            execute_resume.Enabled = true;

            foreach (var item in execute_backup_pause.SelectedItems)
            {
                this.controller.Model.backupToPause.Add(item.ToString());
            }

            this.controller.Model.suspendThread();
        }

        private void execute_resume_Click(object sender, EventArgs e)
        {
            this.controller.Model.resumeThread();
            execute_pause.Enabled = true;
            execute_resume.Visible = false;
        }
        private void execute_stop_Click(object sender, EventArgs e)
        {
            this.controller.Model.abortThread();
            deleteProgressBar();
            MessageBox.Show(Properties.Resources.thread_stop, Properties.Resources.information,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Exclamation);
            execute_stop.Enabled = false;
            execute_pause.Enabled = false;
            execute_resume.Visible = false;
        }

        // Settings
        private void strip_settings_Click(object sender, EventArgs e)
        {
            TabControl.SelectedIndex = 5;
            sizeLimit.Value = this.controller.Model.maxFileSize;
            settings_priority.Text = string.Join(", ", this.controller.Model.extensionPrioritized);
            file_size.Text = Properties.Resources.size_limit + " : " + sizeLimit.Value + " " + Properties.Resources.size_unit;
        }
        private void sizeLimit_Scroll(object sender, EventArgs e)
        {
            file_size.Text = Properties.Resources.size_limit + " : " + sizeLimit.Value + " " + Properties.Resources.size_unit;
        }

        private void settings_save_Click(object sender, EventArgs e)
        {
            this.controller.Model.maxFileSize = sizeLimit.Value;
            this.controller.Model.extensionPrioritized = parseUserInputAsList(settings_priority.Text);
            this.controller.Model.saveToSettingFile(sizeLimit.Value.ToString(), settings_priority.Text);
        }

        // Color ---------------------------------------------------------------------------------------------------------------------------------------------------


        private void stripe_execute_Click(object sender, EventArgs e)
        {
            TabControl.SelectedIndex = 4;
            resetStripColor();
            strip_execute.BackColor = ColorTranslator.FromHtml("#CCCAC8");
            execute_backup_pause.Items.Clear();
        }

        private void strip_edit_Click(object sender, EventArgs e)
        {
            TabControl.SelectedIndex = 2;
            resetStripColor();
            strip_edit.BackColor = ColorTranslator.FromHtml("#CCCAC8");
        }

        private void strip_delete_Click(object sender, EventArgs e)
        {
            TabControl.SelectedIndex = 3;
            resetStripColor();
            strip_delete.BackColor = ColorTranslator.FromHtml("#CCCAC8");
        }

        private void resetStripColor()
        {
            strip_add.BackColor = ColorTranslator.FromHtml("#00FFFFFFF");
            strip_edit.BackColor = ColorTranslator.FromHtml("#00FFFFFF");
            strip_delete.BackColor = ColorTranslator.FromHtml("#00FFFFFF");
            strip_execute.BackColor = ColorTranslator.FromHtml("#00FFFFFF");
        }

        // Language ---------------------------------------------------------------------------------------------------------------------------------------------------

        private void changeLanguage()
        {
            // stripe
            strip_home.Text = Properties.Resources.home;
            strip_about.Text = Properties.Resources.about;
            strip_add.Text = Properties.Resources.add;
            strip_edit.Text = Properties.Resources.edit;
            strip_delete.Text = Properties.Resources.delete;
            strip_execute.Text = Properties.Resources.execute;
            strip_state.Text = Properties.Resources.state;
            strip_log.Text = Properties.Resources.log;

            // add
            add_name_label.Text = Properties.Resources.name;
            add_full.Text = Properties.Resources.full;
            add_source.Text = Properties.Resources.source;
            add_destination.Text = Properties.Resources.destination;
            add_differential.Text = Properties.Resources.differential;
            add_extension_label.Text = Properties.Resources.extension;
            add_save.Text = Properties.Resources.save;

            // edit
            edit_select_label.Text = Properties.Resources.select_label;
            edit_name_label.Text = Properties.Resources.name;
            edit_sourceFolder.Text = Properties.Resources.source;
            edit_destinationFolder.Text = Properties.Resources.destination;
            edit_full.Text = Properties.Resources.full;
            edit_differential.Text = Properties.Resources.differential;
            edit_extension_label.Text = Properties.Resources.extension;
            edit_label.Text = Properties.Resources.edit;

            // delete
            delete_label.Text = Properties.Resources.delete_label;
            delete_validation.Text = Properties.Resources.delete;

            // execute
            execute_label.Text = Properties.Resources.execute_label;
            execute_play.Text = Properties.Resources.execute;
            execute_warning.Text = Properties.Resources.execute_warning;
            delete_warning.Text = Properties.Resources.delete_warning;
            execute_pause.Text = Properties.Resources.pause;
            execute_stop.Text = Properties.Resources.stop;

            // settings
            strip_settings.Text = Properties.Resources.settings;
            settings_save.Text = Properties.Resources.save;
            priority_label.Text = Properties.Resources.file_priority;
            file_size.Text = Properties.Resources.size_limit;
        }

        private void spanish_CheckedChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-ES");
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            changeLanguage();
        }

        private void english_CheckedChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("");
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            changeLanguage();
        }

        private void french_CheckedChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-FR");
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            changeLanguage();
        }

        private void chinese_CheckedChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            changeLanguage();
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("pt-PT");
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            changeLanguage();
        }

        private void arabic_CheckedChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar-DZ");
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            changeLanguage();
        }

        private void deutsch_CheckedChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de-DE");
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            changeLanguage();
        }

        private void russian_CheckedChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            changeLanguage();
        }

        private void hebrew_CheckedChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("he-IL");
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            changeLanguage();
        }

        // Other : 
        private List<string> parseUserInputAsList(string userInput)
        {
            //List <string> listParsed = new List<string>();
            List<string> listParsed = new List<string>(Regex.Replace(userInput, @"\s+", "").Split(','));
            return listParsed;
        }

        public void reloadListView()
        {
            this.edit_backup_list.Items.Clear();
            this.delete_backup_list.Items.Clear();
            this.execute_backup_list.Items.Clear();
            for (int i = 0; i < this.controller.Model.BackupJobList.Count; i++)
            {
                this.edit_backup_list.Items.Add("[" + i + "] " + this.controller.Model.BackupJobList[i].Name);
                this.delete_backup_list.Items.Add("[" + i + "] " + this.controller.Model.BackupJobList[i].Name);
                this.execute_backup_list.Items.Add("[" + i + "] " + this.controller.Model.BackupJobList[i].Name);
            }
            /*this.edit_backup_list.Refresh();
            this.delete_backup_list.Refresh();
            this.execute_backup_list.Refresh();*/
            this.edit_backup_list.Update();
            this.delete_backup_list.Update();
            this.execute_backup_list.Update();

        }

        private void strip_state_Click(object sender, EventArgs e)
        {
            this.controller.Model.openStateFile();
        }

        private void strip_log_Click(object sender, EventArgs e)
        {
            this.controller.Model.openLogFile();
        }

        private void trackingPanel_Paint(object sender, PaintEventArgs e)
        {

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog2_HelpRequest(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void add_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void add_new_extension_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

    }
}
