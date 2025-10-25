using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EternalEngineWizard
{
	internal static class Program
	{
		static EternalEngineWizardStorage WizardStorage = new EternalEngineWizardStorage();

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormWizardSetup(WizardStorage));
		}
	}
}
