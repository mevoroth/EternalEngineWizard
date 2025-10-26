namespace EternalEngineWizard
{
	partial class UserControlInstallation
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
			this.ProgressBarInstallation = new System.Windows.Forms.ProgressBar();
			this.LabelInstallationPercentage = new System.Windows.Forms.Label();
			this.LabelInstallationProgress = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ProgressBarInstallation
			// 
			this.ProgressBarInstallation.Location = new System.Drawing.Point(100, 150);
			this.ProgressBarInstallation.Name = "ProgressBarInstallation";
			this.ProgressBarInstallation.Size = new System.Drawing.Size(440, 23);
			this.ProgressBarInstallation.Step = 1;
			this.ProgressBarInstallation.TabIndex = 0;
			// 
			// LabelInstallationPercentage
			// 
			this.LabelInstallationPercentage.AutoSize = true;
			this.LabelInstallationPercentage.Location = new System.Drawing.Point(120, 190);
			this.LabelInstallationPercentage.Name = "LabelInstallationPercentage";
			this.LabelInstallationPercentage.Size = new System.Drawing.Size(21, 13);
			this.LabelInstallationPercentage.TabIndex = 1;
			this.LabelInstallationPercentage.Text = "0%";
			// 
			// LabelInstallationProgress
			// 
			this.LabelInstallationProgress.AutoSize = true;
			this.LabelInstallationProgress.Location = new System.Drawing.Point(180, 190);
			this.LabelInstallationProgress.MinimumSize = new System.Drawing.Size(300, 0);
			this.LabelInstallationProgress.Name = "LabelInstallationProgress";
			this.LabelInstallationProgress.Size = new System.Drawing.Size(300, 13);
			this.LabelInstallationProgress.TabIndex = 2;
			// 
			// UserControlInstallation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.LabelInstallationProgress);
			this.Controls.Add(this.LabelInstallationPercentage);
			this.Controls.Add(this.ProgressBarInstallation);
			this.Name = "UserControlInstallation";
			this.Size = new System.Drawing.Size(640, 480);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public void SetProgress(float InProgressNormalized, string InProgressLabel)
		{
			ProgressBarInstallation.Value = (int)(InProgressNormalized * 100.0f);
			LabelInstallationPercentage.Text = string.Format("%d%", (int)(InProgressNormalized * 100.0f));
			LabelInstallationProgress.Text = InProgressLabel;
		}

		private System.Windows.Forms.ProgressBar ProgressBarInstallation;
		private System.Windows.Forms.Label LabelInstallationPercentage;
		private System.Windows.Forms.Label LabelInstallationProgress;
	}
}
