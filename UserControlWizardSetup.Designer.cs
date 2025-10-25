using System.Windows.Forms;

namespace EternalEngineWizard
{
	partial class UserControlWizardSetup : UserControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ButtonNext = new System.Windows.Forms.Button();
			this.ButtonBrowseInstallationFolder = new System.Windows.Forms.Button();
			this.TextBoxProjectName = new System.Windows.Forms.TextBox();
			this.LabelProjectName = new System.Windows.Forms.Label();
			this.TextBoxInstallationFolder = new System.Windows.Forms.TextBox();
			this.LabelInstallationFolder = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ButtonNext
			// 
			this.ButtonNext.Location = new System.Drawing.Point(430, 390);
			this.ButtonNext.Name = "ButtonNext";
			this.ButtonNext.Size = new System.Drawing.Size(75, 23);
			this.ButtonNext.TabIndex = 3;
			this.ButtonNext.Text = "Next >";
			this.ButtonNext.UseVisualStyleBackColor = true;
			this.ButtonNext.Click += new System.EventHandler(this.ButtonNext_Click);
			// 
			// ButtonBrowseInstallationFolder
			// 
			this.ButtonBrowseInstallationFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.ButtonBrowseInstallationFolder.Location = new System.Drawing.Point(450, 210);
			this.ButtonBrowseInstallationFolder.Margin = new System.Windows.Forms.Padding(0);
			this.ButtonBrowseInstallationFolder.Name = "ButtonBrowseInstallationFolder";
			this.ButtonBrowseInstallationFolder.Size = new System.Drawing.Size(24, 23);
			this.ButtonBrowseInstallationFolder.TabIndex = 2;
			this.ButtonBrowseInstallationFolder.Text = "...";
			this.ButtonBrowseInstallationFolder.UseVisualStyleBackColor = true;
			this.ButtonBrowseInstallationFolder.Click += new System.EventHandler(this.ButtonBrowseInstallationFolder_Click);
			// 
			// TextBoxProjectName
			// 
			this.TextBoxProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TextBoxProjectName.Location = new System.Drawing.Point(140, 80);
			this.TextBoxProjectName.Margin = new System.Windows.Forms.Padding(10);
			this.TextBoxProjectName.Name = "TextBoxProjectName";
			this.TextBoxProjectName.Size = new System.Drawing.Size(300, 26);
			this.TextBoxProjectName.TabIndex = 0;
			// 
			// LabelProjectName
			// 
			this.LabelProjectName.AutoSize = true;
			this.LabelProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LabelProjectName.Location = new System.Drawing.Point(100, 50);
			this.LabelProjectName.Name = "LabelProjectName";
			this.LabelProjectName.Size = new System.Drawing.Size(104, 20);
			this.LabelProjectName.TabIndex = 8;
			this.LabelProjectName.Text = "Project Name";
			// 
			// TextBoxInstallationFolder
			// 
			this.TextBoxInstallationFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TextBoxInstallationFolder.Location = new System.Drawing.Point(140, 210);
			this.TextBoxInstallationFolder.Margin = new System.Windows.Forms.Padding(10);
			this.TextBoxInstallationFolder.Name = "TextBoxInstallationFolder";
			this.TextBoxInstallationFolder.Size = new System.Drawing.Size(300, 26);
			this.TextBoxInstallationFolder.TabIndex = 1;
			// 
			// LabelInstallationFolder
			// 
			this.LabelInstallationFolder.AutoSize = true;
			this.LabelInstallationFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LabelInstallationFolder.Location = new System.Drawing.Point(100, 180);
			this.LabelInstallationFolder.Name = "LabelInstallationFolder";
			this.LabelInstallationFolder.Size = new System.Drawing.Size(130, 20);
			this.LabelInstallationFolder.TabIndex = 6;
			this.LabelInstallationFolder.Text = "Installation folder";
			// 
			// UserControlWizardSetup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ButtonNext);
			this.Controls.Add(this.ButtonBrowseInstallationFolder);
			this.Controls.Add(this.TextBoxProjectName);
			this.Controls.Add(this.LabelProjectName);
			this.Controls.Add(this.TextBoxInstallationFolder);
			this.Controls.Add(this.LabelInstallationFolder);
			this.Name = "UserControlWizardSetup";
			this.Size = new System.Drawing.Size(640, 480);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button ButtonNext;
		private System.Windows.Forms.Button ButtonBrowseInstallationFolder;
		private System.Windows.Forms.TextBox TextBoxProjectName;
		private System.Windows.Forms.Label LabelProjectName;
		private System.Windows.Forms.TextBox TextBoxInstallationFolder;
		private System.Windows.Forms.Label LabelInstallationFolder;
	}
}
