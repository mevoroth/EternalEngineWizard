using EternalEngine;
using Sharpmake;
using System.Xml.Linq;

[module: Sharpmake.Include(@"eternal-engine\eternal-engine.sharpmake.cs")]
[module: Sharpmake.Include(@"eternal-engine-components\eternal-engine-components.sharpmake.cs")]
[module: Sharpmake.Include(@"eternal-engine-core\eternal-engine-core.sharpmake.cs")]
[module: Sharpmake.Include(@"eternal-engine-extern\eternal-engine-extern.sharpmake.cs")]
[module: Sharpmake.Include(@"eternal-engine-graphics\eternal-engine-graphics.sharpmake.cs")]
[module: Sharpmake.Include(@"eternal-engine-shaders\eternal-engine-shaders.sharpmake.cs")]
[module: Sharpmake.Include(@"eternal-engine-utils\eternal-engine-utils.sharpmake.cs")]
[module: Sharpmake.Include(@"{0}\{0}.sharpmake.cs")]

namespace EternalEngine
{
	public class EternalEngineSettings
	{
		public static readonly Platform ProjectPlatforms = {1};
		public static readonly DevEnv ProjectDevEnvs = DevEnv.vs2022;
		public static readonly Optimization ProjectOptimizations = Optimization.Debug | Optimization.Release;

		public static readonly string VulkanPath = @"D:\Code\VulkanSDK\1.3.231.1";
		public static readonly string FBXSDKPath = @"D:\Software\FBXSDK\2020.3.4";

		public static readonly string WinPixEventRuntimeVersion = @"1.0.231030001";
		public static readonly string MicrosoftDirect3DD3D12Version = @"1.711.3-preview";
	}
}

namespace {0}
{
	[Sharpmake.Generate]
	public class {0}Solution : Sharpmake.Solution
	{
		public {0}Solution()
		{
			Name = "{0}";

			AddTargets(new Target(
				EternalEngineSettings.ProjectPlatforms,
				EternalEngineSettings.ProjectDevEnvs,
				EternalEngineSettings.ProjectOptimizations
			));
		}

		[Configure()]
		public void ConfigureAll(Configuration InConfiguration, Target InTarget)
		{
			InConfiguration.SolutionFileName = "[solution.Name]_[target.DevEnv]";
			InConfiguration.SolutionPath = @"[solution.SharpmakeCsPath]";

			InConfiguration.AddProject<EternalEngine.EternalEngineExternProject>(InTarget);
			InConfiguration.AddProject<EternalEngine.EternalEngineGraphicsProject>(InTarget);
			InConfiguration.AddProject<EternalEngine.EternalEngineUtilsProject>(InTarget);
			InConfiguration.AddProject<EternalEngine.EternalEngineShadersProject>(InTarget);
			InConfiguration.AddProject<EternalEngine.EternalEngineComponentsProject>(InTarget);
			InConfiguration.AddProject<EternalEngine.EternalEngineCoreProject>(InTarget);
			InConfiguration.AddProject<{0}Project>(InTarget);
		}
	}

	public static class Main
	{
		[Sharpmake.Main]
		public static void SharpmakeMain(Sharpmake.Arguments InArguments)
		{
			KitsRootPaths.SetUseKitsRootForDevEnv(DevEnv.vs2022, KitsRootEnum.KitsRoot10, Options.Vc.General.WindowsTargetPlatformVersion.Latest);
			InArguments.Generate<{0}Solution>();
			InArguments.Generate<EternalEngineSolution>();
		}
	}
}
