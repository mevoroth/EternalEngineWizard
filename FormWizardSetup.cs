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
		public static string FindLatestVisualStudioPath()
		{
			// Path to vswhere.exe
			string VisualStudioWherePath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
				"Microsoft Visual Studio", "Installer", "vswhere.exe");

			if (!File.Exists(VisualStudioWherePath))
			{
				throw new FileNotFoundException("vswhere.exe not found. Is Visual Studio 2017+ installed?");
			}

			Process VisualStudioWhereProcess = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = VisualStudioWherePath,
					Arguments = "-latest -prerelease -products * -requires Microsoft.Component.MSBuild -property installationPath",
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				}
			};

			VisualStudioWhereProcess.Start();
			string VisualStudioWhereProcessOutput = VisualStudioWhereProcess.StandardOutput.ReadToEnd().Trim();
			VisualStudioWhereProcess.WaitForExit();

			if (string.IsNullOrEmpty(VisualStudioWhereProcessOutput))
			{
				throw new FileNotFoundException("MSBuild not found.");
			}

			// vswhere can return multiple paths, take the first one
			string MSBuildPath = VisualStudioWhereProcessOutput.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

			return MSBuildPath;
		}

		public static void ExecuteCommand(string InWorkingDirectory, string InFileName, string InArguments)
		{
			StringBuilder OutputStringBuilder = new StringBuilder();
			StringBuilder ErrorStringBuilder = new StringBuilder();

			Process CommandProcess = new Process();
			CommandProcess.StartInfo.FileName = InFileName;
			CommandProcess.StartInfo.WorkingDirectory = InWorkingDirectory;
			CommandProcess.StartInfo.Arguments = InArguments;
			CommandProcess.StartInfo.CreateNoWindow = true;
			CommandProcess.StartInfo.RedirectStandardInput = true;
			CommandProcess.StartInfo.RedirectStandardOutput = true;
			CommandProcess.StartInfo.RedirectStandardError = true;
			CommandProcess.StartInfo.UseShellExecute = false;

			CommandProcess.OutputDataReceived += (InSender, InEvent) =>
			{
				if (InEvent.Data != null)
					OutputStringBuilder.AppendLine(InEvent.Data);
			};

			CommandProcess.ErrorDataReceived += (InSender, InEvent) =>
			{
				if (InEvent.Data != null)
					ErrorStringBuilder.AppendLine(InEvent.Data);
			};

			Console.WriteLine("==================================================");
			Console.WriteLine("Executing '{0}'", InFileName + " " + InArguments);

			CommandProcess.Start();
			CommandProcess.BeginOutputReadLine();
			CommandProcess.BeginErrorReadLine();
			CommandProcess.WaitForExit();

			Console.WriteLine(OutputStringBuilder.ToString());
			Console.WriteLine(ErrorStringBuilder.ToString());
		}

		public static void ExecuteCommand(string InWorkingDirectory, string InCommand)
		{
			string[] CommandTokens = InCommand.Split(' ');
			string CommandArguments = String.Join(" ", CommandTokens.Skip(1).Take(CommandTokens.Length - 1).ToArray());

			ExecuteCommand(InWorkingDirectory, CommandTokens[0], CommandArguments);
		}

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

		private void UserControlInstallation_Load(object sender, EventArgs e)
		{
			UserControlInstallation.WizardStorage = WizardStorage;
		}

		public void ExecuteInstallation()
		{
			UserControlWizardSetup.Visible = false;
			UserControlInstallation.Visible = true;

			//string[] InstallationCommands = File.ReadAllLines("InstallationTemplates/Installation.template.bat");

			//using (DirectoryBackup CurrentDirectoryBackup = new DirectoryBackup())
			//{
			//	Directory.SetCurrentDirectory(WizardStorage.ProjectFolder);

			//	for (int InstallationCommandIndex = 0; InstallationCommandIndex < InstallationCommands.Length; ++InstallationCommandIndex)
			//		ExecuteCommand(WizardStorage.ProjectFolder, InstallationCommands[InstallationCommandIndex]);
			//}

			//string[] ReplacementPatterns = new string[]
			//{
			//	WizardStorage.ProjectName,
			//	"Platform.win64"
			//};
			//List<string> InstallationFiles = new List<string>(Directory.GetFiles("InstallationTemplates/Project"));
			//InstallationFiles.Insert(0, "InstallationTemplates/Solution.template.cs");

			//string[] OutputInstallationFiles = new string[InstallationFiles.Count];
			//for (int FileIndex = 0; FileIndex < InstallationFiles.Count; ++FileIndex)
			//{
			//	string OutInstallationFile = Regex.Replace(InstallationFiles[FileIndex], "(Solution|Project)", WizardStorage.ProjectName)
			//		.Replace("InstallationTemplates/", "\\")
			//		.Replace(".template", "")
			//		.Replace(".cs", ".sharpmake.cs");

			//	OutputInstallationFiles[FileIndex] = WizardStorage.ProjectFolder + OutInstallationFile;
			//}

			//for (int FileIndex = 0; FileIndex < InstallationFiles.Count; ++FileIndex)
			//{
			//	string FileContent = File.ReadAllText(InstallationFiles[FileIndex]);

			//	for (int ReplacementPatternIndex = 0; ReplacementPatternIndex < ReplacementPatterns.Length; ++ReplacementPatternIndex)
			//		FileContent = FileContent.Replace("{" + ReplacementPatternIndex + "}", ReplacementPatterns[ReplacementPatternIndex]);

			//	string OutputInstallationFolder = Path.GetDirectoryName(OutputInstallationFiles[FileIndex]);
			//	if (!Directory.Exists(OutputInstallationFolder))
			//		Directory.CreateDirectory(OutputInstallationFolder);

			//	File.WriteAllText(OutputInstallationFiles[FileIndex], FileContent);
			//}

			using (DirectoryBackup CurrentDirectoryBackup = new DirectoryBackup())
			{
				Directory.SetCurrentDirectory(WizardStorage.ProjectFolder + "\\..\\Sharpmake");

				string MSBuildPath = FindLatestVisualStudioPath() + @"\MSBuild\Current\Bin\MSBuild.exe";
				ExecuteCommand(WizardStorage.ProjectFolder + "\\..\\Sharpmake", MSBuildPath, "Sharpmake.sln /t:\"Sharpmake_Application\" /p:Configuration=Release /p:Platform=\"Any CPU\"");

				Directory.SetCurrentDirectory(WizardStorage.ProjectFolder);

				ExecuteCommand(WizardStorage.ProjectFolder, WizardStorage.ProjectFolder + "\\..\\Sharpmake\\Sharpmake.Application\\bin\\Release\\net6.0\\Sharpmake.Application.exe", string.Format("/sources('{0}.sharpmake.cs')", WizardStorage.ProjectName));
			}
		}

		private class DirectoryBackup : IDisposable
		{
			public DirectoryBackup()
			{
				BackupDirectory = Directory.GetCurrentDirectory();
			}

			~DirectoryBackup()
			{
				Dispose();
			}

			public void Dispose()
			{
				Directory.SetCurrentDirectory(BackupDirectory);
			}

			private string BackupDirectory = null;
		}
	}
}
