namespace EternalEngineWizard
{
	partial class FormWizardSetup
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
			this.UserControlWizardSetup = new EternalEngineWizard.UserControlWizardSetup();
			this.UserControlInstallation = new EternalEngineWizard.UserControlInstallation();
			this.SuspendLayout();
			// 
			// UserControlWizardSetup
			// 
			this.UserControlWizardSetup.Location = new System.Drawing.Point(372, 132);
			this.UserControlWizardSetup.Name = "UserControlWizardSetup";
			this.UserControlWizardSetup.Size = new System.Drawing.Size(640, 480);
			this.UserControlWizardSetup.TabIndex = 0;
			this.UserControlWizardSetup.WizardStorage = null;
			this.UserControlWizardSetup.Load += new System.EventHandler(this.UserControlWizardSetup_Load);
			// 
			// UserControlInstallation
			// 
			this.UserControlInstallation.Location = new System.Drawing.Point(372, 132);
			this.UserControlInstallation.Name = "UserControlInstallation";
			this.UserControlInstallation.Size = new System.Drawing.Size(640, 480);
			this.UserControlInstallation.TabIndex = 1;
			this.UserControlInstallation.Visible = false;
			this.UserControlInstallation.WizardStorage = null;
			this.UserControlInstallation.Load += new System.EventHandler(this.UserControlInstallation_Load);
			// 
			// FormWizardSetup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1024, 768);
			this.Controls.Add(this.UserControlInstallation);
			this.Controls.Add(this.UserControlWizardSetup);
			this.Name = "FormWizardSetup";
			this.Text = "Eternal Engine Wizard";
			this.ResumeLayout(false);

		}

		#endregion

		public UserControlWizardSetup UserControlWizardSetup;
		public UserControlInstallation UserControlInstallation;
	}
}

