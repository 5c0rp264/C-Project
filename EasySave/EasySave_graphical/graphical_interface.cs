using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasySave_graphical
{
    public partial class graphical_interface : Form
    {
        String language = "english";
        Controller controller;

        // Initialize ---------------------------------------------------------------------------------------------------------------------------------------------------
        public graphical_interface()
        {
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
            MessageBox.Show("EasySave 2.0 - MiProSoft", "Information", MessageBoxButtons.OK,MessageBoxIcon.Information);
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
                add_source.Text = "Source : "+add_sourceFolder;
            }

        }

        private void add_destination_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog add_browser_destination = new FolderBrowserDialog();
            if (add_browser_destination.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // We get the selected source path of the user
                add_destinationFolder = add_browser_destination.SelectedPath;
                add_destination.Text = "Destination : "+add_destinationFolder;
            }
        }

        private Boolean addIsValid = true;
        private String errorMessage_add = "";
        private String errorTitle_add = "";
        private void button1_Click(object sender, EventArgs e)
        {
            addIsValid = true;
            errorMessage_add = "";

            if (add_full.Checked == false && add_differential.Checked == false)
            {
                addIsValid = false;
                if (language == "spanish")
                {
                    errorMessage_add = "El tipo de trabajo no está establecido";
                }
                else if(language == "french")
                {
                    errorMessage_add = "Le type de la sauvegarde est manquant";
                }
                else
                {
                    errorMessage_add = "The type of the backup job is not set";
                }
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
                if (language == "spanish")
                {
                    errorMessage_add = "Un trabajo de copia necesita un nombre para ser identificado";
                }
                else if (language == "french")
                {
                    errorMessage_add = "Le nom du travail de sauvegarde est nécessaire pour être identifiée";
                }
                else
                {
                    errorMessage_add = "A backup work needs a name to be identified";
                }
            }

            if (add_sourceFolder.Length < 2)
            {
                addIsValid = false;
                if (language == "spanish")
                {
                    errorMessage_add = "Un trabajo de copia necesita un origen";
                }
                else if (language == "french")
                {
                    errorMessage_add = "Le dossier source est manquant";
                }
                else
                {
                    errorMessage_add = "The source folder is not set";
                }
            }

            if (add_destinationFolder.Length < 2)
            {
                addIsValid = false;
                if (language == "spanish")
                {
                    errorMessage_add = "Un trabajo de copia necesita una destinación";
                    errorTitle_add = "Error durante la edición";
                }
                else if (language == "french")
                {
                    errorMessage_add = "Le dossier de destination est manquant";
                    errorTitle_add = "Erreur lors de l'édition";
                }
                else
                {
                    errorMessage_add = "The destination folder is not set";
                    errorTitle_add = "Error while editing";
                }
            }

            if (addIsValid)
            {
                // add the backup
                BackupJob backupJob = new BackupJob(add_name.Text, add_sourceFolder, add_destinationFolder, (backupType == "full"),parseUserInputAsList(add_extension.Text));
                if (this.controller.Model.createBackupJob(backupJob))
                {
                    if (language == "spanish")
                    {
                        MessageBox.Show("Añadido con éxito", "Añadido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (language == "french")
                    {
                        MessageBox.Show("Ajouté avec succès", "Ajout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Added with success", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                    reloadListView();
                }
                else
                {
                    MessageBox.Show(errorMessage_add, errorTitle_add, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(errorMessage_add, errorTitle_add, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            edit_full.Checked = true;
            edit_full.Enabled = this.controller.Model.BackupJobList[this.edit_backup_list.SelectedIndex].IsFull;
            edit_differential.Enabled = true;
            edit_differential.Checked = !this.controller.Model.BackupJobList[this.edit_backup_list.SelectedIndex].IsFull;

            edit_extension.ReadOnly = false;
            edit_extension.Text = string.Join(", ",this.controller.Model.BackupJobList[this.edit_backup_list.SelectedIndex].ToBeEncryptedFileExtensions);
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
                if (language == "spanish")
                {
                    errorMessage = "El tipo de trabajo no está establecido";
                }
                else if (language == "french")
                {
                    errorMessage = "Le type de la sauvegarde est manquant";
                }
                else
                {
                    errorMessage = "The type of the backup job is not set";
                }
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
                if (language == "spanish")
                {
                    errorMessage = "Un trabajo de copia necesita un nombre para ser identificado";
                }
                else if (language == "french")
                {
                    errorMessage = "Le nom du travail de sauvegarde est nécessaire pour être identifiée";
                }
                else
                {
                    errorMessage = "A backup work needs a name to be identified";
                }
            }

            if (edit_sourceFolderValue.Length < 2)
            {
                editIsValid = false;
                if (language == "spanish")
                {
                    errorMessage = "Un trabajo de copia necesita un origen";
                }
                else if (language == "french")
                {
                    errorMessage = "Le dossier source est manquant";
                }
                else
                {
                    errorMessage = "The source folder is not set";
                }
            }

            if (edit_destinationFolderValue.Length < 2)
            {
                editIsValid = false;
                if (language == "spanish")
                {
                    errorMessage = "Un trabajo de copia necesita una destinación";
                }
                else if (language == "french")
                {
                    errorMessage = "Le dossier de destination est manquant";
                }
                else
                {
                    errorMessage = "The destination folder is not set";
                }
            }

            if (editIsValid)
            {
                // Edit the backup
                this.controller.Model.editBackupJob(this.edit_backup_list.SelectedIndex, edit_name.Text, edit_sourceFolderValue, edit_destinationFolderValue, (edit_backupType == "full"),parseUserInputAsList(edit_extension.Text));
                if (language == "spanish")
                {
                     MessageBox.Show("Todo fue bien", "información",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                }
                else if (language == "french")
                {
                     MessageBox.Show("Tout s'est bien passé", "Information",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                }
                else
                {
                     MessageBox.Show("Everything went well", "Information",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(errorMessage,"Error while editing", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void edit_sourceFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog edit_browser_source = new FolderBrowserDialog();
            if (edit_browser_source.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // We get the selected source path of the user
                edit_sourceFolderValue = edit_browser_source.SelectedPath;
                if (language == "spanish")
                {
                    edit_sourceFolder.Text = "Origen : " + edit_sourceFolderValue;
                }
                else
                {
                    edit_sourceFolder.Text = "Source : " + edit_sourceFolderValue;
                }
                
            }
        }
        private void edit_destinationFolder_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog edit_browser_destination = new FolderBrowserDialog();
            if (edit_browser_destination.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // We get the selected source path of the user
                edit_destinationFolderValue = edit_browser_destination.SelectedPath;
                if (language == "spanish")
                {
                    edit_destinationFolder.Text = "Destinación : " + edit_destinationFolderValue;
                }
                else
                {
                    edit_destinationFolder.Text = "Destination : " + edit_destinationFolderValue;
                }
            }
        }

        // Delete ---------------------------------------------------------------------------------------------------------------------------------------------------
        private void delete_validation_Click(object sender, EventArgs e)
        {
                if (delete_backup_list.SelectedItems.Count == 1)
                {
                    string delete_message = "Do you really want to delete the following backup job : " + delete_backup_list.SelectedItem;
                    string caption = "Delete backup jobs";

                    if (language == "french")
                    {
                        delete_message = "Êtes-vous sûr de vouloir supprimer le travail de sauvegarde suivant : " + delete_backup_list.SelectedItem;
                        caption = "Supprimer le travail de sauvegarde";
                    }
                    else if (language == "spanish")
                    {
                        delete_message = "¿Estás seguro de que quieres borrar el siguiente trabajo de copia? : " + delete_backup_list.SelectedItem;
                        caption = "Borrar el trabajo de copia";
                    }

                    var result = MessageBox.Show(delete_message, caption,
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Exclamation);

                    if (result == DialogResult.Yes)
                    {
                    // Call the model to delete the backup
                    this.controller.Model.deleteBackupJob(delete_backup_list.SelectedIndex);
                    if (language == "english")
                    {
                        MessageBox.Show("Deleted : " + delete_backup_list.SelectedItem);
                    }
                    else if (language == "french")
                    {
                        MessageBox.Show("Supprimé : " + delete_backup_list.SelectedItem);
                    }
                    else if (language == "spanish")
                    {
                        MessageBox.Show("Borrado : " + delete_backup_list.SelectedItem);
                    }

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
            if (execute_backup_list.SelectedItems.Count > 0) // This is an array containing every backup the user want's to select.
            {
                List<int> slectedBUJ = new List<int>();
                List<String> dirFullForDiff = new List<string>();
                foreach (var i in execute_backup_list.SelectedItems)
                {
                    slectedBUJ.Add(execute_backup_list.Items.IndexOf(i));// Add selected indexes to the List<int>
                    if (!this.controller.Model.BackupJobList[slectedBUJ[slectedBUJ.Count-1]].IsFull)
                    {
                        using (var fbd = new FolderBrowserDialog())
                        {
                            if (language == "spanish")
                            {
                                MessageBox.Show("Por favor, introduzca una copia de seguridad de referencia completa : " + this.controller.Model.BackupJobList[slectedBUJ[slectedBUJ.Count - 1]].Name, "Plz", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else if (language == "french")
                            {
                                MessageBox.Show("Merci d'entrer une sauvegarde complète de référence : " + this.controller.Model.BackupJobList[slectedBUJ[slectedBUJ.Count - 1]].Name, "Plz", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                            {
                                MessageBox.Show("Please enter a full backup of reference : " + this.controller.Model.BackupJobList[slectedBUJ[slectedBUJ.Count - 1]].Name, "Plz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            DialogResult result = fbd.ShowDialog();
                            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                            {
                                dirFullForDiff.Add(fbd.SelectedPath);
                            }
                            else
                            {
                                while (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                                {
                                    if (language == "spanish")
                                    {
                                        MessageBox.Show("Por favor : " + this.controller.Model.BackupJobList[slectedBUJ[slectedBUJ.Count - 1]].Name, "entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                    else if (language == "french")
                                    {
                                        MessageBox.Show("S'il vous plaît : " + this.controller.Model.BackupJobList[slectedBUJ[slectedBUJ.Count - 1]].Name, "Entrée invalide", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                    else
                                    {
                                        MessageBox.Show("Please :  " + this.controller.Model.BackupJobList[slectedBUJ[slectedBUJ.Count - 1]].Name, "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
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

        //TODO: LOAD AND UNLOAD THIS ANIMATION
        private void loadingAnimation(bool needsToBeVisible)
        {
            if (needsToBeVisible)
            {
                loading.Visible = true;
            }
            else
            {
                loading.Visible = false;
            }
        }

        private void execute_backup_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            execute_warning.Visible = false;
        }
        // Color ---------------------------------------------------------------------------------------------------------------------------------------------------


        private void stripe_execute_Click(object sender, EventArgs e)
        {
            loading.Visible = false;
            TabControl.SelectedIndex = 4;
            resetStripColor();
            strip_execute.BackColor = ColorTranslator.FromHtml("#CCCAC8");
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
        private void french_CheckedChanged(object sender, EventArgs e)
        {
            language = "french";

            // strip - french
            strip_home.Text = "Accueil";
            strip_about.Text = "A propos";
            strip_add.Text = "Ajouter";
            strip_edit.Text = "Éditer";
            strip_delete.Text = "Effacer";
            strip_execute.Text = "Exécuter";
            strip_state.Text = "État";
            strip_log.Text = "Journal";

            // add - french
            add_name_label.Text = "Nom : ";
            add_full.Text = "Complète";
            add_differential.Text = "Différentielle";
            add_extension_label.Text = "Extension à crypter : (.png,.txt,.mp3...)";
            add_save.Text = "Sauvegarder";

            // edit - french
            edit_select_label.Text = "Choisir un travail";
            edit_name_label.Text = "Nom";
            edit_full.Text = "Complète";
            edit_differential.Text = "Différentielle";
            edit_extension_label.Text = "Extension à crypter : (.png,.txt,.mp3...)";
            edit_label.Text = "Éditer";

            // delete - french
            delete_label.Text = "Sélectionner un travail à supprimer :";
            delete_validation.Text = "Supprimer";

            // execute - french
            execute_label.Text = "Sélectionner les sauvegardes à exécuter";
            execute_play.Text = "Exécuter";
            execute_warning.Text = "Vous devez sélectionner au moins un travail";
            delete_warning.Text = "Vous devez sélectionner un travail";
        }

        private void spanish_CheckedChanged(object sender, EventArgs e)
        {
            language = "spanish";

            // strip - spanish
            strip_home.Text = "Home";
            strip_about.Text = "Sobre nosotros";
            strip_add.Text = "Añadir";
            strip_edit.Text = "Editar";
            strip_delete.Text = "Borrar";
            strip_execute.Text = "Ejecutar";
            strip_state.Text = "Estado";
            strip_log.Text = "Registro";

            // add - spanish
            add_name_label.Text = "Label : ";
            add_full.Text = "Completa";
            add_source.Text = "Origen";
            add_destination.Text = "Destinación";
            add_differential.Text = "Diferencial";
            add_extension_label.Text = "Extensión para cifrar : (.png,.txt,.mp3...)";
            add_save.Text = "Guardar";

            // edit - spanish
            edit_select_label.Text = "Elegir un trabajo";
            edit_name_label.Text = "Label : ";
            edit_sourceFolder.Text = "Origen";
            edit_destinationFolder.Text = "Destinación";
            edit_full.Text = "Completa";
            edit_differential.Text = "Diferencial";
            edit_extension_label.Text = "Extensión para cifrar : (.png,.txt,.mp3...)";
            edit_label.Text = "Editar";

            // delete - spanish
            delete_label.Text = "Seleccione el trabajo que desea eliminar:";
            delete_validation.Text = "Eliminar";

            // execute - spanish
            execute_label.Text = "Seleccione el trabajo(s) de copia a ejecutar :";
            execute_play.Text = "Ejecutar";
            execute_warning.Text = "Necesitas seleccionar al menos un trabajo";
            delete_warning.Text = "Necesitas seleccionar un trabajo";
        }

        private void english_CheckedChanged(object sender, EventArgs e)
        {
            language = "english";

            // strip - english
            strip_home.Text = "Home";
            strip_about.Text = "About";
            strip_add.Text = "Add";
            strip_edit.Text = "Edit";
            strip_delete.Text = "Delete";
            strip_execute.Text = "Execute";
            strip_state.Text = "State";
            strip_log.Text = "Log";

            // add - english
            add_name_label.Text = "Name : ";
            add_source.Text = "Source";
            add_destination.Text = "Destination";
            add_full.Text = "Full";
            add_differential.Text = "Differential";
            add_extension_label.Text = "Extension to encrypt : (.png,.txt,.mp3...)";
            add_save.Text = "Save";

            // edit - english
            edit_select_label.Text = "   Select a job";
            edit_name_label.Text = "Name";
            edit_sourceFolder.Text = "Source";
            edit_destinationFolder.Text = "Destination";
            edit_full.Text = "Full";
            edit_differential.Text = "Differential";
            edit_extension_label.Text = "Extension to encrypt : (.png,.txt,.mp3...)";
            edit_label.Text = "Edit";

            // delete - english
            delete_label.Text = "Select the backup job to delete :";
            delete_validation.Text = "Delete";

            // execute - english
            execute_label.Text = "Select the backup job(s) to execute :";
            execute_play.Text = "Execute";
            execute_warning.Text = "You need to select at least one job";
            delete_warning.Text = "You need to select a job";
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
            for (int i = 0; i < this.controller.Model.BackupJobList.Count; i++ )
            {
                this.edit_backup_list.Items.Add("["+i+"] " + this.controller.Model.BackupJobList[i].Name);
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

        private void strip_state_Click(object sender, EventArgs e)
        {
            this.controller.Model.openStateFile();
        }

        private void strip_log_Click(object sender, EventArgs e)
        {
            this.controller.Model.openLogFile();
        }
    }
}
