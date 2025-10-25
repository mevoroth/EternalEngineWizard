using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EternalEngineWizard
{
	public partial class UserControlInstallation : UserControl
	{
		public EternalEngineWizardStorage WizardStorage { get; set; } = null;

		public UserControlInstallation()
		{
			InitializeComponent();
		}
	}
}
