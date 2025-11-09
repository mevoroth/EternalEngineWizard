using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EternalEngineWizard
{
	public class WizardUtils
	{
		public static void ExecuteInstallation(UserControlInstallation InUserControlInstallation)
		{
			EternalEngineWizardStorage WizardStorage = InUserControlInstallation.WizardStorage;

			string[] InstallationCommands = File.ReadAllLines("InstallationTemplates/Installation.template.bat");

			List<string> InstallationFiles = new List<string>(Directory.GetFiles("InstallationTemplates/Project", "*", SearchOption.AllDirectories));
			InstallationFiles.Insert(0, "InstallationTemplates/Solution.template.cs");

			_ProgressTracker = new ProgressTracker(InstallationCommands.Length + InstallationFiles.Count + 2 + 1);

			using (DirectoryBackup CurrentDirectoryBackup = new DirectoryBackup())
			{
				Directory.SetCurrentDirectory(WizardStorage.ProjectFolder);

				for (int InstallationCommandIndex = 0; InstallationCommandIndex < InstallationCommands.Length; ++InstallationCommandIndex)
					ExecuteCommand(InUserControlInstallation, WizardStorage.ProjectFolder, InstallationCommands[InstallationCommandIndex]);
			}

			string[] ReplacementPatterns = new string[]
			{
				WizardStorage.ProjectName,
				"Platform.win64"
			};

			string[] OutputInstallationFiles = new string[InstallationFiles.Count];
			for (int FileIndex = 0; FileIndex < InstallationFiles.Count; ++FileIndex)
			{
				string OutInstallationFile = Regex.Replace(InstallationFiles[FileIndex], "(Solution|Project)", WizardStorage.ProjectName)
					.Replace("InstallationTemplates/", "\\")
					.Replace(".template", "")
					.Replace(".cs", ".sharpmake.cs");

				OutputInstallationFiles[FileIndex] = WizardStorage.ProjectFolder + OutInstallationFile;
			}

			for (int FileIndex = 0; FileIndex < InstallationFiles.Count; ++FileIndex)
			{
				_ProgressTracker.Step();
				InUserControlInstallation.SetProgress(_ProgressTracker.GetProgress(), string.Format("Writing: {0}", InstallationFiles[FileIndex]));

				string FileContent = File.ReadAllText(InstallationFiles[FileIndex]);

				for (int ReplacementPatternIndex = 0; ReplacementPatternIndex < ReplacementPatterns.Length; ++ReplacementPatternIndex)
					FileContent = FileContent.Replace("{" + ReplacementPatternIndex + "}", ReplacementPatterns[ReplacementPatternIndex]);

				string OutputInstallationFolder = Path.GetDirectoryName(OutputInstallationFiles[FileIndex]);
				if (!Directory.Exists(OutputInstallationFolder))
					Directory.CreateDirectory(OutputInstallationFolder);

				File.WriteAllText(OutputInstallationFiles[FileIndex], FileContent);
			}

			using (DirectoryBackup CurrentDirectoryBackup = new DirectoryBackup())
			{
				Directory.SetCurrentDirectory(WizardStorage.ProjectFolder + "\\..\\Sharpmake");

				string MSBuildPath = FindLatestVisualStudioPath() + @"\MSBuild\Current\Bin\MSBuild.exe";
				ExecuteCommand(InUserControlInstallation, WizardStorage.ProjectFolder + "\\..\\Sharpmake", MSBuildPath, "Sharpmake.sln /t:\"Sharpmake_Application\" /p:Configuration=Release /p:Platform=\"Any CPU\"");

				Directory.SetCurrentDirectory(WizardStorage.ProjectFolder);

				ExecuteCommand(InUserControlInstallation, WizardStorage.ProjectFolder, WizardStorage.ProjectFolder + "\\..\\Sharpmake\\Sharpmake.Application\\bin\\Release\\net6.0\\Sharpmake.Application.exe", string.Format("/sources('{0}.sharpmake.cs')", WizardStorage.ProjectName));
			}


			_ProgressTracker.Step();
			InUserControlInstallation.SetProgress(1.0f, "Installation completed");

			_ProgressTracker = null;
		}

		private static string FindLatestVisualStudioPath()
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

		private static void ExecuteCommand(UserControlInstallation InUserControlInstallation, string InWorkingDirectory, string InFileName, string InArguments)
		{
			_ProgressTracker.Step();
			InUserControlInstallation.SetProgress(_ProgressTracker.GetProgress(), InFileName + " " + InArguments);

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

		private static void ExecuteCommand(UserControlInstallation InUserControlInstallation, string InWorkingDirectory, string InCommand)
		{
			string[] CommandTokens = InCommand.Split(' ');
			string CommandArguments = String.Join(" ", CommandTokens.Skip(1).Take(CommandTokens.Length - 1).ToArray());

			ExecuteCommand(InUserControlInstallation, InWorkingDirectory, CommandTokens[0], CommandArguments);
		}

		private class ProgressTracker
		{
			public ProgressTracker(int InStepsCount)
			{
				_StepsCount = InStepsCount;
			}

			public void Step()
			{
				++_CurrentStep;
			}

			public float GetProgress()
			{
				return (float)_CurrentStep / (float)_StepsCount;
			}

			private int _CurrentStep = 0;
			private int _StepsCount = 0;
		}

		private class DirectoryBackup : IDisposable
		{
			public DirectoryBackup()
			{
				_BackupDirectory = Directory.GetCurrentDirectory();
			}

			~DirectoryBackup()
			{
				Dispose();
			}

			public void Dispose()
			{
				Directory.SetCurrentDirectory(_BackupDirectory);
			}

			private string _BackupDirectory = null;
		}

		private static ProgressTracker _ProgressTracker = null;
	}
}
