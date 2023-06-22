using ICities;

namespace Emergency_Lights_Manager_V2 
{
	public class Mod : ILoadingExtension, IUserMod
	{
		public string Name => "Emergency Lights Manager V2";

		public string Description => "Change colors of emergency vehicle lights and rotary lights - full customization.";

		public static bool loaded;

		public void OnCreated(ILoading loading)
		{
			Utils.Utils.LoadSettings();
		}

		public void OnReleased()
		{
			Utils.Utils.ExportSettings();
		}

		public void OnLevelLoaded(LoadMode mode)
		{
			loaded = true;
			Utils.Utils.Apply();
		}

		public void OnLevelUnloading()
		{
			loaded = false;
		}

	}
}
