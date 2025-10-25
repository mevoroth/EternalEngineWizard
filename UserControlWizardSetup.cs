using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EternalEngineWizard
{
	public partial class UserControlWizardSetup : UserControl
	{
		public EternalEngineWizardStorage WizardStorage { get; set; } = null;

		public UserControlWizardSetup()
		{
			InitializeComponent();
		}

		private void ButtonBrowseInstallationFolder_Click(object InSender, EventArgs InEvent)
		{
			using (FolderBrowserDialog FolderDialog = new FolderBrowserDialog())
			{
				FolderDialog.SelectedPath = @"C:\";

				if (FolderDialog.ShowDialog() == DialogResult.OK)
				{
					TextBoxInstallationFolder.Text = FolderDialog.SelectedPath;
				}
			}
		}

		private void ButtonNext_Click(object InSender, EventArgs InEvent)
		{
			string CandidateProjectName			= TextBoxProjectName.Text;
			string CandidateInstallationFolder	= TextBoxInstallationFolder.Text;

			string CandidateProjectFolder		= CandidateInstallationFolder + "\\" + CandidateProjectName;

			if (CandidateProjectName.Length == 0 ||
				CandidateInstallationFolder.Length == 0 ||
				!Path.IsPathRooted(CandidateInstallationFolder))
			{
				MessageBox.Show("Wrong path", "Wrong path", MessageBoxButtons.OK);

				return;
			}

			try
			{
				string CandidateProjectFolderFullPath = Path.GetFullPath(CandidateProjectFolder);
			}
			catch (Exception PathException)
			{
				MessageBox.Show("Wrong path", "Wrong path", MessageBoxButtons.OK);

				return;
			}

			if (!Directory.Exists(CandidateProjectFolder))
			{
				Directory.CreateDirectory(CandidateProjectFolder);
			}

			//if (Directory.EnumerateFileSystemEntries(CandidateProjectFolder).Any())
			//{
			//	return;
			//}

			WizardStorage.ProjectName			= CandidateProjectName;
			WizardStorage.InstallationFolder	= CandidateInstallationFolder;
			WizardStorage.ProjectFolder			= CandidateProjectFolder;

			((FormWizardSetup)Parent).ExecuteInstallation();
		}
	}
}
