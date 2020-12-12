
namespace EasySave_graphical
{
    partial class graphical_interface
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(graphical_interface));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.strip_home = new System.Windows.Forms.ToolStripMenuItem();
            this.strip_about = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.strip_add = new System.Windows.Forms.ToolStripMenuItem();
            this.strip_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.strip_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.strip_execute = new System.Windows.Forms.ToolStripMenuItem();
            this.strip_state = new System.Windows.Forms.ToolStripMenuItem();
            this.strip_log = new System.Windows.Forms.ToolStripMenuItem();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.homeTab = new System.Windows.Forms.TabPage();
            this.spanish = new System.Windows.Forms.RadioButton();
            this.french = new System.Windows.Forms.RadioButton();
            this.english = new System.Windows.Forms.RadioButton();
            this.language_container = new System.Windows.Forms.ListView();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.Addtab = new System.Windows.Forms.TabPage();
            this.add_save = new System.Windows.Forms.Button();
            this.add_extension_label = new System.Windows.Forms.Label();
            this.add_name_label = new System.Windows.Forms.Label();
            this.add_differential = new System.Windows.Forms.RadioButton();
            this.add_full = new System.Windows.Forms.RadioButton();
            this.add_destination = new System.Windows.Forms.Button();
            this.add_source = new System.Windows.Forms.Button();
            this.add_extension = new System.Windows.Forms.TextBox();
            this.add_name = new System.Windows.Forms.TextBox();
            this.EditTab = new System.Windows.Forms.TabPage();
            this.edit_select_label = new System.Windows.Forms.Label();
            this.edit_backup_list = new System.Windows.Forms.ListBox();
            this.edit_label = new System.Windows.Forms.Button();
            this.edit_extension_label = new System.Windows.Forms.Label();
            this.edit_name_label = new System.Windows.Forms.Label();
            this.edit_differential = new System.Windows.Forms.RadioButton();
            this.edit_full = new System.Windows.Forms.RadioButton();
            this.edit_destinationFolder = new System.Windows.Forms.Button();
            this.edit_sourceFolder = new System.Windows.Forms.Button();
            this.edit_extension = new System.Windows.Forms.TextBox();
            this.edit_name = new System.Windows.Forms.TextBox();
            this.DeleteTab = new System.Windows.Forms.TabPage();
            this.delete_warning = new System.Windows.Forms.Label();
            this.delete_validation = new System.Windows.Forms.Button();
            this.delete_label = new System.Windows.Forms.Label();
            this.delete_backup_list = new System.Windows.Forms.ListBox();
            this.executeTab = new System.Windows.Forms.TabPage();
            this.execute_resume = new System.Windows.Forms.Button();
            this.loading = new System.Windows.Forms.PictureBox();
            this.execute_warning = new System.Windows.Forms.Label();
            this.execute_stop = new System.Windows.Forms.Button();
            this.execute_play = new System.Windows.Forms.Button();
            this.execute_pause = new System.Windows.Forms.Button();
            this.execute_label = new System.Windows.Forms.Label();
            this.execute_backup_list = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.homeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.Addtab.SuspendLayout();
            this.EditTab.SuspendLayout();
            this.DeleteTab.SuspendLayout();
            this.executeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.strip_home,
            this.strip_about});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(774, 35);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // strip_home
            // 
            this.strip_home.Name = "strip_home";
            this.strip_home.Size = new System.Drawing.Size(52, 31);
            this.strip_home.Text = "Home";
            this.strip_home.Click += new System.EventHandler(this.easySaveToolStripMenuItem_Click);
            // 
            // strip_about
            // 
            this.strip_about.Name = "strip_about";
            this.strip_about.Size = new System.Drawing.Size(52, 31);
            this.strip_about.Text = "About";
            this.strip_about.Click += new System.EventHandler(this.strip_about_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.AutoSize = false;
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.Right;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.strip_add,
            this.strip_edit,
            this.strip_delete,
            this.strip_execute,
            this.strip_state,
            this.strip_log});
            this.menuStrip2.Location = new System.Drawing.Point(608, 35);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(166, 415);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            this.menuStrip2.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip2_ItemClicked);
            // 
            // strip_add
            // 
            this.strip_add.AutoSize = false;
            this.strip_add.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.strip_add.Image = ((System.Drawing.Image)(resources.GetObject("strip_add.Image")));
            this.strip_add.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.strip_add.Name = "strip_add";
            this.strip_add.Size = new System.Drawing.Size(160, 60);
            this.strip_add.Text = "Add";
            this.strip_add.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // strip_edit
            // 
            this.strip_edit.AutoSize = false;
            this.strip_edit.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.strip_edit.Image = ((System.Drawing.Image)(resources.GetObject("strip_edit.Image")));
            this.strip_edit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.strip_edit.Name = "strip_edit";
            this.strip_edit.Size = new System.Drawing.Size(160, 60);
            this.strip_edit.Text = "Edit";
            this.strip_edit.Click += new System.EventHandler(this.strip_edit_Click);
            // 
            // strip_delete
            // 
            this.strip_delete.AutoSize = false;
            this.strip_delete.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.strip_delete.Image = ((System.Drawing.Image)(resources.GetObject("strip_delete.Image")));
            this.strip_delete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.strip_delete.Name = "strip_delete";
            this.strip_delete.Size = new System.Drawing.Size(160, 60);
            this.strip_delete.Text = "Delete";
            this.strip_delete.Click += new System.EventHandler(this.strip_delete_Click);
            // 
            // strip_execute
            // 
            this.strip_execute.AutoSize = false;
            this.strip_execute.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.strip_execute.Image = ((System.Drawing.Image)(resources.GetObject("strip_execute.Image")));
            this.strip_execute.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.strip_execute.Name = "strip_execute";
            this.strip_execute.Size = new System.Drawing.Size(160, 60);
            this.strip_execute.Text = "Execute";
            this.strip_execute.Click += new System.EventHandler(this.stripe_execute_Click);
            // 
            // strip_state
            // 
            this.strip_state.AutoSize = false;
            this.strip_state.Font = new System.Drawing.Font("Verdana", 12F);
            this.strip_state.Image = ((System.Drawing.Image)(resources.GetObject("strip_state.Image")));
            this.strip_state.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.strip_state.Name = "strip_state";
            this.strip_state.Size = new System.Drawing.Size(160, 60);
            this.strip_state.Text = "State";
            this.strip_state.Click += new System.EventHandler(this.strip_state_Click);
            // 
            // strip_log
            // 
            this.strip_log.AutoSize = false;
            this.strip_log.Font = new System.Drawing.Font("Verdana", 12F);
            this.strip_log.Image = ((System.Drawing.Image)(resources.GetObject("strip_log.Image")));
            this.strip_log.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.strip_log.Name = "strip_log";
            this.strip_log.Size = new System.Drawing.Size(160, 60);
            this.strip_log.Text = "Log";
            this.strip_log.Click += new System.EventHandler(this.strip_log_Click);
            // 
            // TabControl
            // 
            this.TabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.TabControl.Controls.Add(this.homeTab);
            this.TabControl.Controls.Add(this.Addtab);
            this.TabControl.Controls.Add(this.EditTab);
            this.TabControl.Controls.Add(this.DeleteTab);
            this.TabControl.Controls.Add(this.executeTab);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.ItemSize = new System.Drawing.Size(0, 1);
            this.TabControl.Location = new System.Drawing.Point(0, 35);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(608, 415);
            this.TabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl.TabIndex = 2;
            // 
            // homeTab
            // 
            this.homeTab.Controls.Add(this.spanish);
            this.homeTab.Controls.Add(this.french);
            this.homeTab.Controls.Add(this.english);
            this.homeTab.Controls.Add(this.language_container);
            this.homeTab.Controls.Add(this.logoBox);
            this.homeTab.Location = new System.Drawing.Point(4, 5);
            this.homeTab.Name = "homeTab";
            this.homeTab.Size = new System.Drawing.Size(600, 406);
            this.homeTab.TabIndex = 4;
            this.homeTab.Text = "Home";
            this.homeTab.UseVisualStyleBackColor = true;
            // 
            // spanish
            // 
            this.spanish.Appearance = System.Windows.Forms.Appearance.Button;
            this.spanish.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("spanish.BackgroundImage")));
            this.spanish.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.spanish.Cursor = System.Windows.Forms.Cursors.Hand;
            this.spanish.Location = new System.Drawing.Point(423, 261);
            this.spanish.Name = "spanish";
            this.spanish.Size = new System.Drawing.Size(153, 97);
            this.spanish.TabIndex = 16;
            this.spanish.UseVisualStyleBackColor = true;
            this.spanish.CheckedChanged += new System.EventHandler(this.spanish_CheckedChanged);
            // 
            // french
            // 
            this.french.Appearance = System.Windows.Forms.Appearance.Button;
            this.french.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("french.BackgroundImage")));
            this.french.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.french.Cursor = System.Windows.Forms.Cursors.Hand;
            this.french.Location = new System.Drawing.Point(225, 261);
            this.french.Name = "french";
            this.french.Size = new System.Drawing.Size(153, 97);
            this.french.TabIndex = 15;
            this.french.UseVisualStyleBackColor = true;
            this.french.CheckedChanged += new System.EventHandler(this.french_CheckedChanged);
            // 
            // english
            // 
            this.english.Appearance = System.Windows.Forms.Appearance.Button;
            this.english.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("english.BackgroundImage")));
            this.english.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.english.Cursor = System.Windows.Forms.Cursors.Hand;
            this.english.Location = new System.Drawing.Point(30, 261);
            this.english.Name = "english";
            this.english.Size = new System.Drawing.Size(153, 97);
            this.english.TabIndex = 14;
            this.english.UseVisualStyleBackColor = true;
            this.english.CheckedChanged += new System.EventHandler(this.english_CheckedChanged);
            // 
            // language_container
            // 
            this.language_container.BackColor = System.Drawing.SystemColors.Control;
            this.language_container.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.language_container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.language_container.HideSelection = false;
            this.language_container.Location = new System.Drawing.Point(0, 226);
            this.language_container.Name = "language_container";
            this.language_container.Size = new System.Drawing.Size(600, 180);
            this.language_container.TabIndex = 17;
            this.language_container.UseCompatibleStateImageBehavior = false;
            // 
            // logoBox
            // 
            this.logoBox.BackColor = System.Drawing.SystemColors.Control;
            this.logoBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.logoBox.Image = ((System.Drawing.Image)(resources.GetObject("logoBox.Image")));
            this.logoBox.Location = new System.Drawing.Point(0, 0);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(600, 226);
            this.logoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoBox.TabIndex = 13;
            this.logoBox.TabStop = false;
            // 
            // Addtab
            // 
            this.Addtab.Controls.Add(this.add_save);
            this.Addtab.Controls.Add(this.add_extension_label);
            this.Addtab.Controls.Add(this.add_name_label);
            this.Addtab.Controls.Add(this.add_differential);
            this.Addtab.Controls.Add(this.add_full);
            this.Addtab.Controls.Add(this.add_destination);
            this.Addtab.Controls.Add(this.add_source);
            this.Addtab.Controls.Add(this.add_extension);
            this.Addtab.Controls.Add(this.add_name);
            this.Addtab.Location = new System.Drawing.Point(4, 24);
            this.Addtab.Name = "Addtab";
            this.Addtab.Padding = new System.Windows.Forms.Padding(3);
            this.Addtab.Size = new System.Drawing.Size(600, 387);
            this.Addtab.TabIndex = 0;
            this.Addtab.Text = "Add";
            this.Addtab.UseVisualStyleBackColor = true;
            // 
            // add_save
            // 
            this.add_save.Location = new System.Drawing.Point(235, 330);
            this.add_save.Name = "add_save";
            this.add_save.Size = new System.Drawing.Size(130, 38);
            this.add_save.TabIndex = 12;
            this.add_save.Text = "Save";
            this.add_save.UseVisualStyleBackColor = true;
            this.add_save.Click += new System.EventHandler(this.button1_Click);
            // 
            // add_extension_label
            // 
            this.add_extension_label.AutoSize = true;
            this.add_extension_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.add_extension_label.Location = new System.Drawing.Point(86, 250);
            this.add_extension_label.Name = "add_extension_label";
            this.add_extension_label.Size = new System.Drawing.Size(313, 22);
            this.add_extension_label.TabIndex = 11;
            this.add_extension_label.Text = "Extension to encrypt : (.png,.jpg,.txt...)";
            // 
            // add_name_label
            // 
            this.add_name_label.AutoSize = true;
            this.add_name_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.add_name_label.Location = new System.Drawing.Point(90, 34);
            this.add_name_label.Name = "add_name_label";
            this.add_name_label.Size = new System.Drawing.Size(72, 22);
            this.add_name_label.TabIndex = 10;
            this.add_name_label.Text = "Name : ";
            // 
            // add_differential
            // 
            this.add_differential.AutoSize = true;
            this.add_differential.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.add_differential.Location = new System.Drawing.Point(316, 217);
            this.add_differential.Name = "add_differential";
            this.add_differential.Size = new System.Drawing.Size(94, 21);
            this.add_differential.TabIndex = 9;
            this.add_differential.TabStop = true;
            this.add_differential.Text = "Differential";
            this.add_differential.UseVisualStyleBackColor = true;
            // 
            // add_full
            // 
            this.add_full.AutoSize = true;
            this.add_full.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.add_full.Location = new System.Drawing.Point(221, 217);
            this.add_full.Name = "add_full";
            this.add_full.Size = new System.Drawing.Size(48, 21);
            this.add_full.TabIndex = 8;
            this.add_full.TabStop = true;
            this.add_full.Text = "Full";
            this.add_full.UseVisualStyleBackColor = true;
            // 
            // add_destination
            // 
            this.add_destination.Cursor = System.Windows.Forms.Cursors.Hand;
            this.add_destination.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.add_destination.Location = new System.Drawing.Point(90, 146);
            this.add_destination.Name = "add_destination";
            this.add_destination.Size = new System.Drawing.Size(409, 53);
            this.add_destination.TabIndex = 7;
            this.add_destination.Text = "Destination";
            this.add_destination.UseVisualStyleBackColor = true;
            this.add_destination.Click += new System.EventHandler(this.add_destination_Click);
            // 
            // add_source
            // 
            this.add_source.Cursor = System.Windows.Forms.Cursors.Hand;
            this.add_source.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.add_source.Location = new System.Drawing.Point(90, 80);
            this.add_source.Name = "add_source";
            this.add_source.Size = new System.Drawing.Size(409, 51);
            this.add_source.TabIndex = 6;
            this.add_source.Text = "Source";
            this.add_source.UseVisualStyleBackColor = true;
            this.add_source.Click += new System.EventHandler(this.add_source_Click);
            // 
            // add_extension
            // 
            this.add_extension.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.add_extension.Location = new System.Drawing.Point(90, 288);
            this.add_extension.MaxLength = 40;
            this.add_extension.Name = "add_extension";
            this.add_extension.Size = new System.Drawing.Size(409, 23);
            this.add_extension.TabIndex = 5;
            // 
            // add_name
            // 
            this.add_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.add_name.ForeColor = System.Drawing.Color.Black;
            this.add_name.Location = new System.Drawing.Point(168, 34);
            this.add_name.MaxLength = 40;
            this.add_name.Name = "add_name";
            this.add_name.Size = new System.Drawing.Size(331, 23);
            this.add_name.TabIndex = 1;
            this.add_name.TextChanged += new System.EventHandler(this.add_name_TextChanged);
            // 
            // EditTab
            // 
            this.EditTab.Controls.Add(this.edit_select_label);
            this.EditTab.Controls.Add(this.edit_backup_list);
            this.EditTab.Controls.Add(this.edit_label);
            this.EditTab.Controls.Add(this.edit_extension_label);
            this.EditTab.Controls.Add(this.edit_name_label);
            this.EditTab.Controls.Add(this.edit_differential);
            this.EditTab.Controls.Add(this.edit_full);
            this.EditTab.Controls.Add(this.edit_destinationFolder);
            this.EditTab.Controls.Add(this.edit_sourceFolder);
            this.EditTab.Controls.Add(this.edit_extension);
            this.EditTab.Controls.Add(this.edit_name);
            this.EditTab.Location = new System.Drawing.Point(4, 24);
            this.EditTab.Name = "EditTab";
            this.EditTab.Padding = new System.Windows.Forms.Padding(3);
            this.EditTab.Size = new System.Drawing.Size(600, 387);
            this.EditTab.TabIndex = 1;
            this.EditTab.Text = "Edit";
            this.EditTab.UseVisualStyleBackColor = true;
            // 
            // edit_select_label
            // 
            this.edit_select_label.AutoSize = true;
            this.edit_select_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.edit_select_label.Location = new System.Drawing.Point(8, 15);
            this.edit_select_label.Name = "edit_select_label";
            this.edit_select_label.Size = new System.Drawing.Size(119, 22);
            this.edit_select_label.TabIndex = 23;
            this.edit_select_label.Text = "   Select a job";
            // 
            // edit_backup_list
            // 
            this.edit_backup_list.FormattingEnabled = true;
            this.edit_backup_list.Items.AddRange(new object[] {
            "Backup1",
            "Backup2",
            "Backup3",
            "Backup1",
            "Backup2",
            "Backup3",
            "Backup1",
            "Backup2",
            "Backup3",
            "Backup1",
            "Backup2",
            "Backup3",
            "Backup1",
            "Backup2",
            "Backup3",
            "Backup1",
            "Backup2",
            "Backup3",
            "Backup1",
            "Backup2",
            "Backup3",
            "Backup1",
            "Back--up2",
            "Backup3",
            "Backup7",
            "Backup2",
            "Backup3",
            "Backup1",
            "Backup2",
            "Backup3",
            "Backup1",
            "Back--up2",
            "Backup3",
            "Backup7"});
            this.edit_backup_list.Location = new System.Drawing.Point(8, 50);
            this.edit_backup_list.Name = "edit_backup_list";
            this.edit_backup_list.Size = new System.Drawing.Size(138, 316);
            this.edit_backup_list.TabIndex = 22;
            this.edit_backup_list.SelectedIndexChanged += new System.EventHandler(this.edit_backup_list_SelectedIndexChanged);
            // 
            // edit_label
            // 
            this.edit_label.Enabled = false;
            this.edit_label.Location = new System.Drawing.Point(316, 330);
            this.edit_label.Name = "edit_label";
            this.edit_label.Size = new System.Drawing.Size(130, 38);
            this.edit_label.TabIndex = 21;
            this.edit_label.Text = "Edit";
            this.edit_label.UseVisualStyleBackColor = true;
            this.edit_label.Click += new System.EventHandler(this.edit_label_Click);
            // 
            // edit_extension_label
            // 
            this.edit_extension_label.AutoSize = true;
            this.edit_extension_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.edit_extension_label.Location = new System.Drawing.Point(167, 250);
            this.edit_extension_label.Name = "edit_extension_label";
            this.edit_extension_label.Size = new System.Drawing.Size(313, 22);
            this.edit_extension_label.TabIndex = 20;
            this.edit_extension_label.Text = "Extension to encrypt : (.png,.jpg,.txt...)";
            // 
            // edit_name_label
            // 
            this.edit_name_label.AutoSize = true;
            this.edit_name_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.edit_name_label.Location = new System.Drawing.Point(171, 34);
            this.edit_name_label.Name = "edit_name_label";
            this.edit_name_label.Size = new System.Drawing.Size(72, 22);
            this.edit_name_label.TabIndex = 19;
            this.edit_name_label.Text = "Name : ";
            // 
            // edit_differential
            // 
            this.edit_differential.AutoSize = true;
            this.edit_differential.Enabled = false;
            this.edit_differential.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.edit_differential.Location = new System.Drawing.Point(397, 217);
            this.edit_differential.Name = "edit_differential";
            this.edit_differential.Size = new System.Drawing.Size(94, 21);
            this.edit_differential.TabIndex = 18;
            this.edit_differential.TabStop = true;
            this.edit_differential.Text = "Differential";
            this.edit_differential.UseVisualStyleBackColor = true;
            // 
            // edit_full
            // 
            this.edit_full.AutoSize = true;
            this.edit_full.Enabled = false;
            this.edit_full.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.edit_full.Location = new System.Drawing.Point(302, 217);
            this.edit_full.Name = "edit_full";
            this.edit_full.Size = new System.Drawing.Size(48, 21);
            this.edit_full.TabIndex = 17;
            this.edit_full.TabStop = true;
            this.edit_full.Text = "Full";
            this.edit_full.UseVisualStyleBackColor = true;
            // 
            // edit_destinationFolder
            // 
            this.edit_destinationFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.edit_destinationFolder.Enabled = false;
            this.edit_destinationFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.edit_destinationFolder.Location = new System.Drawing.Point(171, 146);
            this.edit_destinationFolder.Name = "edit_destinationFolder";
            this.edit_destinationFolder.Size = new System.Drawing.Size(409, 53);
            this.edit_destinationFolder.TabIndex = 16;
            this.edit_destinationFolder.Text = "Destination";
            this.edit_destinationFolder.UseVisualStyleBackColor = true;
            this.edit_destinationFolder.Click += new System.EventHandler(this.edit_destinationFolder_Click_1);
            // 
            // edit_sourceFolder
            // 
            this.edit_sourceFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.edit_sourceFolder.Enabled = false;
            this.edit_sourceFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.edit_sourceFolder.Location = new System.Drawing.Point(171, 80);
            this.edit_sourceFolder.Name = "edit_sourceFolder";
            this.edit_sourceFolder.Size = new System.Drawing.Size(409, 51);
            this.edit_sourceFolder.TabIndex = 15;
            this.edit_sourceFolder.Text = "Source";
            this.edit_sourceFolder.UseVisualStyleBackColor = true;
            this.edit_sourceFolder.Click += new System.EventHandler(this.edit_sourceFolder_Click);
            // 
            // edit_extension
            // 
            this.edit_extension.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.edit_extension.Location = new System.Drawing.Point(171, 288);
            this.edit_extension.MaxLength = 40;
            this.edit_extension.Name = "edit_extension";
            this.edit_extension.ReadOnly = true;
            this.edit_extension.Size = new System.Drawing.Size(409, 23);
            this.edit_extension.TabIndex = 14;
            // 
            // edit_name
            // 
            this.edit_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.edit_name.ForeColor = System.Drawing.Color.Black;
            this.edit_name.Location = new System.Drawing.Point(249, 34);
            this.edit_name.MaxLength = 40;
            this.edit_name.Name = "edit_name";
            this.edit_name.ReadOnly = true;
            this.edit_name.Size = new System.Drawing.Size(331, 23);
            this.edit_name.TabIndex = 13;
            // 
            // DeleteTab
            // 
            this.DeleteTab.Controls.Add(this.delete_warning);
            this.DeleteTab.Controls.Add(this.delete_validation);
            this.DeleteTab.Controls.Add(this.delete_label);
            this.DeleteTab.Controls.Add(this.delete_backup_list);
            this.DeleteTab.Location = new System.Drawing.Point(4, 24);
            this.DeleteTab.Name = "DeleteTab";
            this.DeleteTab.Size = new System.Drawing.Size(600, 387);
            this.DeleteTab.TabIndex = 2;
            this.DeleteTab.Text = "Delete";
            this.DeleteTab.UseVisualStyleBackColor = true;
            // 
            // delete_warning
            // 
            this.delete_warning.AutoSize = true;
            this.delete_warning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.delete_warning.ForeColor = System.Drawing.Color.Red;
            this.delete_warning.Location = new System.Drawing.Point(163, 256);
            this.delete_warning.Name = "delete_warning";
            this.delete_warning.Size = new System.Drawing.Size(211, 20);
            this.delete_warning.TabIndex = 10;
            this.delete_warning.Text = "You need to select a backup";
            this.delete_warning.Visible = false;
            // 
            // delete_validation
            // 
            this.delete_validation.Location = new System.Drawing.Point(180, 307);
            this.delete_validation.Name = "delete_validation";
            this.delete_validation.Size = new System.Drawing.Size(185, 42);
            this.delete_validation.TabIndex = 3;
            this.delete_validation.Text = "Delete";
            this.delete_validation.UseVisualStyleBackColor = true;
            this.delete_validation.Click += new System.EventHandler(this.delete_validation_Click);
            // 
            // delete_label
            // 
            this.delete_label.AutoSize = true;
            this.delete_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.delete_label.Location = new System.Drawing.Point(141, 46);
            this.delete_label.Name = "delete_label";
            this.delete_label.Size = new System.Drawing.Size(278, 25);
            this.delete_label.TabIndex = 1;
            this.delete_label.Text = "Select the backup job to delete";
            // 
            // delete_backup_list
            // 
            this.delete_backup_list.FormattingEnabled = true;
            this.delete_backup_list.Items.AddRange(new object[] {
            "Backup1",
            "Backup2",
            "Backup3"});
            this.delete_backup_list.Location = new System.Drawing.Point(113, 105);
            this.delete_backup_list.Name = "delete_backup_list";
            this.delete_backup_list.Size = new System.Drawing.Size(328, 121);
            this.delete_backup_list.TabIndex = 0;
            this.delete_backup_list.SelectedIndexChanged += new System.EventHandler(this.delete_backup_list_SelectedIndexChanged);
            // 
            // executeTab
            // 
            this.executeTab.Controls.Add(this.execute_resume);
            this.executeTab.Controls.Add(this.loading);
            this.executeTab.Controls.Add(this.execute_warning);
            this.executeTab.Controls.Add(this.execute_stop);
            this.executeTab.Controls.Add(this.execute_play);
            this.executeTab.Controls.Add(this.execute_pause);
            this.executeTab.Controls.Add(this.execute_label);
            this.executeTab.Controls.Add(this.execute_backup_list);
            this.executeTab.Location = new System.Drawing.Point(4, 24);
            this.executeTab.Name = "executeTab";
            this.executeTab.Size = new System.Drawing.Size(600, 387);
            this.executeTab.TabIndex = 3;
            this.executeTab.Text = "Execute";
            this.executeTab.UseVisualStyleBackColor = true;
            // 
            // execute_resume
            // 
            this.execute_resume.Enabled = false;
            this.execute_resume.Location = new System.Drawing.Point(204, 337);
            this.execute_resume.Name = "execute_resume";
            this.execute_resume.Size = new System.Drawing.Size(185, 42);
            this.execute_resume.TabIndex = 11;
            this.execute_resume.Text = "Resume";
            this.execute_resume.UseVisualStyleBackColor = true;
            this.execute_resume.Visible = false;
            this.execute_resume.Click += new System.EventHandler(this.execute_resume_Click);
            // 
            // loading
            // 
            this.loading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.loading.Image = ((System.Drawing.Image)(resources.GetObject("loading.Image")));
            this.loading.Location = new System.Drawing.Point(471, 110);
            this.loading.Name = "loading";
            this.loading.Size = new System.Drawing.Size(64, 64);
            this.loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loading.TabIndex = 10;
            this.loading.TabStop = false;
            // 
            // execute_warning
            // 
            this.execute_warning.AutoSize = true;
            this.execute_warning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.execute_warning.ForeColor = System.Drawing.Color.Red;
            this.execute_warning.Location = new System.Drawing.Point(147, 236);
            this.execute_warning.Name = "execute_warning";
            this.execute_warning.Size = new System.Drawing.Size(267, 20);
            this.execute_warning.TabIndex = 9;
            this.execute_warning.Text = "You need to select at least 1 backup";
            this.execute_warning.Visible = false;
            // 
            // execute_stop
            // 
            this.execute_stop.Enabled = false;
            this.execute_stop.Location = new System.Drawing.Point(395, 291);
            this.execute_stop.Name = "execute_stop";
            this.execute_stop.Size = new System.Drawing.Size(185, 42);
            this.execute_stop.TabIndex = 8;
            this.execute_stop.Text = "Stop";
            this.execute_stop.UseVisualStyleBackColor = true;
            this.execute_stop.Click += new System.EventHandler(this.execute_stop_Click);
            // 
            // execute_play
            // 
            this.execute_play.Location = new System.Drawing.Point(13, 291);
            this.execute_play.Name = "execute_play";
            this.execute_play.Size = new System.Drawing.Size(185, 42);
            this.execute_play.TabIndex = 7;
            this.execute_play.Text = "Play";
            this.execute_play.UseVisualStyleBackColor = true;
            this.execute_play.Click += new System.EventHandler(this.execute_play_Click);
            // 
            // execute_pause
            // 
            this.execute_pause.Enabled = false;
            this.execute_pause.Location = new System.Drawing.Point(204, 291);
            this.execute_pause.Name = "execute_pause";
            this.execute_pause.Size = new System.Drawing.Size(185, 42);
            this.execute_pause.TabIndex = 6;
            this.execute_pause.Text = "Pause";
            this.execute_pause.UseVisualStyleBackColor = true;
            this.execute_pause.Click += new System.EventHandler(this.execute_pause_Click);
            // 
            // execute_label
            // 
            this.execute_label.AutoSize = true;
            this.execute_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.execute_label.Location = new System.Drawing.Point(121, 29);
            this.execute_label.Name = "execute_label";
            this.execute_label.Size = new System.Drawing.Size(329, 25);
            this.execute_label.TabIndex = 5;
            this.execute_label.Text = "Select the backup job(s) to execute :";
            // 
            // execute_backup_list
            // 
            this.execute_backup_list.FormattingEnabled = true;
            this.execute_backup_list.Items.AddRange(new object[] {
            "Backup1",
            "Backup2",
            "Backup3"});
            this.execute_backup_list.Location = new System.Drawing.Point(151, 74);
            this.execute_backup_list.Name = "execute_backup_list";
            this.execute_backup_list.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.execute_backup_list.Size = new System.Drawing.Size(263, 134);
            this.execute_backup_list.TabIndex = 4;
            this.execute_backup_list.SelectedIndexChanged += new System.EventHandler(this.execute_backup_list_SelectedIndexChanged);
            // 
            // graphical_interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 450);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "graphical_interface";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EasySave - MiProSoft";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.homeTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.Addtab.ResumeLayout(false);
            this.Addtab.PerformLayout();
            this.EditTab.ResumeLayout(false);
            this.EditTab.PerformLayout();
            this.DeleteTab.ResumeLayout(false);
            this.DeleteTab.PerformLayout();
            this.executeTab.ResumeLayout(false);
            this.executeTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem strip_home;
        private System.Windows.Forms.ToolStripMenuItem strip_about;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem strip_add;
        private System.Windows.Forms.ToolStripMenuItem strip_edit;
        private System.Windows.Forms.ToolStripMenuItem strip_delete;
        private System.Windows.Forms.ToolStripMenuItem strip_execute;
        private System.Windows.Forms.ToolStripMenuItem strip_state;
        private System.Windows.Forms.ToolStripMenuItem strip_log;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage EditTab;
        private System.Windows.Forms.TabPage DeleteTab;
        private System.Windows.Forms.TabPage executeTab;
        private System.Windows.Forms.TabPage homeTab;
        private System.Windows.Forms.RadioButton spanish;
        private System.Windows.Forms.RadioButton french;
        private System.Windows.Forms.RadioButton english;
        private System.Windows.Forms.ListView language_container;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.TabPage Addtab;
        private System.Windows.Forms.Button add_destination;
        private System.Windows.Forms.Button add_source;
        private System.Windows.Forms.TextBox add_extension;
        private System.Windows.Forms.TextBox add_name;
        private System.Windows.Forms.RadioButton add_differential;
        private System.Windows.Forms.RadioButton add_full;
        private System.Windows.Forms.Label add_name_label;
        private System.Windows.Forms.Label add_extension_label;
        private System.Windows.Forms.Button add_save;
        private System.Windows.Forms.Button delete_validation;
        private System.Windows.Forms.Label delete_label;
        private System.Windows.Forms.ListBox delete_backup_list;
        private System.Windows.Forms.Button execute_stop;
        private System.Windows.Forms.Button execute_play;
        private System.Windows.Forms.Button execute_pause;
        private System.Windows.Forms.Label execute_label;
        private System.Windows.Forms.ListBox execute_backup_list;
        private System.Windows.Forms.Label edit_select_label;
        private System.Windows.Forms.ListBox edit_backup_list;
        private System.Windows.Forms.Button edit_label;
        private System.Windows.Forms.Label edit_extension_label;
        private System.Windows.Forms.Label edit_name_label;
        private System.Windows.Forms.RadioButton edit_differential;
        private System.Windows.Forms.RadioButton edit_full;
        private System.Windows.Forms.Button edit_destinationFolder;
        private System.Windows.Forms.Button edit_sourceFolder;
        private System.Windows.Forms.TextBox edit_extension;
        private System.Windows.Forms.TextBox edit_name;
        private System.Windows.Forms.Label execute_warning;
        private System.Windows.Forms.Label delete_warning;
        private System.Windows.Forms.PictureBox loading;
        private System.Windows.Forms.Button execute_resume;
    }
}

