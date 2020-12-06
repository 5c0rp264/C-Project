
namespace GraphicalApp_1
{
    partial class Form1
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
            this.addBackupButton = new System.Windows.Forms.Button();
            this.editBackupButton = new System.Windows.Forms.Button();
            this.deleteBackupButton = new System.Windows.Forms.Button();
            this.executeBackupButton = new System.Windows.Forms.Button();
            this.aboutButton = new System.Windows.Forms.Button();
            this.homeButton = new System.Windows.Forms.Button();
            this.homePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.addPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.editPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.deletePanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.executePanel = new System.Windows.Forms.Panel();
            this.executeBackup = new System.Windows.Forms.Label();
            this.homePanel.SuspendLayout();
            this.addPanel.SuspendLayout();
            this.editPanel.SuspendLayout();
            this.deletePanel.SuspendLayout();
            this.executePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // addBackupButton
            // 
            this.addBackupButton.Location = new System.Drawing.Point(-1, -3);
            this.addBackupButton.Name = "addBackupButton";
            this.addBackupButton.Size = new System.Drawing.Size(217, 23);
            this.addBackupButton.TabIndex = 0;
            this.addBackupButton.Text = "Add backup job";
            this.addBackupButton.UseVisualStyleBackColor = true;
            this.addBackupButton.Click += new System.EventHandler(this.addBackupButton_Click);
            // 
            // editBackupButton
            // 
            this.editBackupButton.Location = new System.Drawing.Point(213, -3);
            this.editBackupButton.Name = "editBackupButton";
            this.editBackupButton.Size = new System.Drawing.Size(217, 23);
            this.editBackupButton.TabIndex = 1;
            this.editBackupButton.Text = "Edit backup job";
            this.editBackupButton.UseVisualStyleBackColor = true;
            this.editBackupButton.Click += new System.EventHandler(this.editBackupButton_Click);
            // 
            // deleteBackupButton
            // 
            this.deleteBackupButton.Location = new System.Drawing.Point(427, -3);
            this.deleteBackupButton.Name = "deleteBackupButton";
            this.deleteBackupButton.Size = new System.Drawing.Size(217, 23);
            this.deleteBackupButton.TabIndex = 2;
            this.deleteBackupButton.Text = "Delete backup job";
            this.deleteBackupButton.UseVisualStyleBackColor = true;
            this.deleteBackupButton.Click += new System.EventHandler(this.deleteBackupButton_Click);
            // 
            // executeBackupButton
            // 
            this.executeBackupButton.Location = new System.Drawing.Point(641, -3);
            this.executeBackupButton.Name = "executeBackupButton";
            this.executeBackupButton.Size = new System.Drawing.Size(217, 23);
            this.executeBackupButton.TabIndex = 3;
            this.executeBackupButton.Text = "Execute backup job";
            this.executeBackupButton.UseVisualStyleBackColor = true;
            this.executeBackupButton.Click += new System.EventHandler(this.executeBackupButton_Click);
            // 
            // aboutButton
            // 
            this.aboutButton.Location = new System.Drawing.Point(770, 419);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(75, 23);
            this.aboutButton.TabIndex = 5;
            this.aboutButton.Text = "About";
            this.aboutButton.UseVisualStyleBackColor = true;
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // homeButton
            // 
            this.homeButton.Location = new System.Drawing.Point(12, 419);
            this.homeButton.Name = "homeButton";
            this.homeButton.Size = new System.Drawing.Size(75, 23);
            this.homeButton.TabIndex = 6;
            this.homeButton.Text = "Home";
            this.homeButton.UseVisualStyleBackColor = true;
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // homePanel
            // 
            this.homePanel.Controls.Add(this.label1);
            this.homePanel.Location = new System.Drawing.Point(12, 23);
            this.homePanel.Name = "homePanel";
            this.homePanel.Size = new System.Drawing.Size(833, 387);
            this.homePanel.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(342, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome on EasySave !";
            // 
            // addPanel
            // 
            this.addPanel.Controls.Add(this.label2);
            this.addPanel.Location = new System.Drawing.Point(12, 23);
            this.addPanel.Name = "addPanel";
            this.addPanel.Size = new System.Drawing.Size(833, 387);
            this.addPanel.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(342, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Add a backup job";
            // 
            // editPanel
            // 
            this.editPanel.Controls.Add(this.label3);
            this.editPanel.Location = new System.Drawing.Point(12, 23);
            this.editPanel.Name = "editPanel";
            this.editPanel.Size = new System.Drawing.Size(836, 387);
            this.editPanel.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(342, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Choose which backup to edit";
            // 
            // deletePanel
            // 
            this.deletePanel.Controls.Add(this.label4);
            this.deletePanel.Location = new System.Drawing.Point(12, 23);
            this.deletePanel.Name = "deletePanel";
            this.deletePanel.Size = new System.Drawing.Size(836, 390);
            this.deletePanel.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(336, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Choose wich backup to delete";
            // 
            // executePanel
            // 
            this.executePanel.Controls.Add(this.executeBackup);
            this.executePanel.Location = new System.Drawing.Point(12, 23);
            this.executePanel.Name = "executePanel";
            this.executePanel.Size = new System.Drawing.Size(836, 390);
            this.executePanel.TabIndex = 11;
            // 
            // executeBackup
            // 
            this.executeBackup.AutoSize = true;
            this.executeBackup.Location = new System.Drawing.Point(342, 151);
            this.executeBackup.Name = "executeBackup";
            this.executeBackup.Size = new System.Drawing.Size(166, 13);
            this.executeBackup.TabIndex = 0;
            this.executeBackup.Text = "Chhose which backup to execute";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 450);
            this.Controls.Add(this.homeButton);
            this.Controls.Add(this.aboutButton);
            this.Controls.Add(this.executeBackupButton);
            this.Controls.Add(this.deleteBackupButton);
            this.Controls.Add(this.editBackupButton);
            this.Controls.Add(this.addBackupButton);
            this.Controls.Add(this.editPanel);
            this.Controls.Add(this.homePanel);
            this.Controls.Add(this.addPanel);
            this.Controls.Add(this.executePanel);
            this.Controls.Add(this.deletePanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.homePanel.ResumeLayout(false);
            this.homePanel.PerformLayout();
            this.addPanel.ResumeLayout(false);
            this.addPanel.PerformLayout();
            this.editPanel.ResumeLayout(false);
            this.editPanel.PerformLayout();
            this.deletePanel.ResumeLayout(false);
            this.deletePanel.PerformLayout();
            this.executePanel.ResumeLayout(false);
            this.executePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addBackupButton;
        private System.Windows.Forms.Button editBackupButton;
        private System.Windows.Forms.Button deleteBackupButton;
        private System.Windows.Forms.Button executeBackupButton;
        private System.Windows.Forms.Button aboutButton;
        private System.Windows.Forms.Button homeButton;
        private System.Windows.Forms.Panel homePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel addPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel editPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel deletePanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel executePanel;
        private System.Windows.Forms.Label executeBackup;
    }
}

