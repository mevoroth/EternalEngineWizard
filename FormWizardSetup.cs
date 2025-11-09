using LibGit2Sharp;
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

namespace EternalEngineWizard
{
	public partial class FormWizardSetup : Form
	{
		public FormWizardSetup(EternalEngineWizardStorage InWizardStorage)
		{
			WizardStorage = InWizardStorage;

			InitializeComponent();
		}

		private EternalEngineWizardStorage WizardStorage = null;

		private void UserControlWizardSetup_Load(object InSender, EventArgs InEvent)
		{
			UserControlWizardSetup.WizardStorage = WizardStorage;
		}

		private void UserControlInstallation_Load(object InSender, EventArgs InEvent)
		{
			UserControlInstallation.WizardStorage = WizardStorage;
		}

		public void ExecuteInstallation()
		{
			UserControlWizardSetup.Visible = false;
			UserControlInstallation.Visible = true;

			WizardUtils.ExecuteInstallation(UserControlInstallation);
		}
	}
}
