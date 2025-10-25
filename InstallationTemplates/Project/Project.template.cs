using Sharpmake;
using EternalEngine;

namespace {0}
{
	[Sharpmake.Generate]
	public class {0}Project : EternalEngineBaseExecutableProject
	{
		public {0}Project()
			: base()
		{
			Name = "{0}";
			Module = Name;
		}

		public override void ConfigureAll(Configuration InConfiguration, ITarget InTarget)
		{
			base.ConfigureAll(InConfiguration, InTarget);
		}
	}
}
