using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static Emergency_Lights_Manager_V2.Utils.Enums;

namespace Emergency_Lights_Manager_V2.Utils 
{
	public class Utils 
	{
		public static Dictionary<Setting, string> settings;

		public static Dictionary<Setting, string> defaultSettings = new Dictionary<Setting, string>
		{
			{
				Setting.Preset,
				"0"
			},
			{
				Setting.PoliceLeft,
				"Red"
			},
			{
				Setting.PoliceRight,
				"Blue"
			},
			{
				Setting.FireLeft,
				"Light Blue"
			},
			{
				Setting.FireRight,
				"Light Blue"
			},
			{
				Setting.AmbulanceLeft,
				"Red"
			},
			{
				Setting.AmbulanceRight,
				"Blue"
			},
			{
				Setting.ManualRearFire,
				"False"
			},
			{
				Setting.ManualRearAmbulance,
				"False"
			},
			{
				Setting.FireLeftRear,
				"Light Blue"
			},
			{
				Setting.FireRightRear,
				"Light Blue"
			},
			{
				Setting.AmbulanceLeftRear,
				"Red"
			},
			{
				Setting.AmbulanceRightRear,
				"Blue"
			},
            {Setting.SnowPlowLeft,
                "Orange"
            },
            {Setting.SnowPlowRight,
                "Orange"
            }
		};

		public static string[] EmergencyLightPresets = new string[7]
		{
			"Default",
			"Custom",
			"No Lights (All Off)",
			"American (Red-Blue)",
			"European (Blue-Blue)",
			"Japanese (Red-Red)",
            "Ideal Settings For Ninja's Vehicles"
		};

		private static int _selectedPreset;

		public static int SelectedPreset
		{
			get
			{
				return _selectedPreset;
			}
			set
			{
				if (_selectedPreset != value)
				{
					_selectedPreset = value;
					settings[Setting.Preset] = _selectedPreset.ToString();
				}
			}
		}

		

		public static void LoadSettings()
		{
			if (settings != null)
			{
				return;
			}
			settings = new Dictionary<Setting, string>();
			if (File.Exists("EmergencyLightsManager.txt"))
			{
				string[] array = File.ReadAllText("EmergencyLightsManager.txt").Replace("\r", string.Empty).Split('\n');
				string[] array2 = array;
				foreach (string text in array2)
				{
					string[] array3 = text.Split('=');
					if (array3.Length == 2 && !(array3[0].Trim() == string.Empty) && !(array3[1].Trim() == string.Empty))
					{
						Setting key;
						try
						{
							key = (Setting)(int)Enum.Parse(typeof(Setting), array3[0]);
						}
						catch
						{
							continue;
						}
						settings[key] = array3[1];
					}
				}
			}
			foreach (int value in Enum.GetValues(typeof(Setting)))
			{
				if (!settings.ContainsKey((Setting)value))
				{
					settings.Add((Setting)value, defaultSettings[(Setting)value]);
				}
			}
			try
			{
				_selectedPreset = Convert.ToInt32(settings[Setting.Preset]);
				_selectedPreset = ((_selectedPreset >= 0) ? ((_selectedPreset <= EmergencyLightPresets.Length - 1) ? _selectedPreset : 0) : 0);
			}
			catch
			{
				_selectedPreset = 0;
			}
		}

		public static void ExportSettings()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<Setting, string> setting in settings)
			{
				stringBuilder.AppendLine(setting.Key.ToString() + "=" + setting.Value.ToString());
			}
			File.WriteAllText("EmergencyLightsManager.txt", stringBuilder.ToString());
		}

		public static void Apply()
		{
			switch (SelectedPreset)
			{
			case 0:
				ColorUtils.UpdateColorsBoth(Service.Police, ColorUtils.Colors["Red"], ColorUtils.Colors["Blue"]);
				ColorUtils.UpdateColorsBoth(Service.Fire, ColorUtils.Colors["Light Blue"], ColorUtils.Colors["Light Blue"]);
				ColorUtils.UpdateColorsBoth(Service.Ambulance, ColorUtils.Colors["Red"], ColorUtils.Colors["Blue"]);
                ColorUtils.UpdatePlowColors(ColorUtils.Colors["Orange"], ColorUtils.Colors["Orange"]);
				break;
			case 1:
				ColorUtils.UpdateColors(Service.Police, ColorUtils.Colors[settings[Setting.PoliceLeft]], ColorUtils.Colors[settings[Setting.PoliceRight]]);
				if (Convert.ToBoolean(settings[Setting.ManualRearFire]))
				{
					ColorUtils.UpdateColors(Service.Fire, ColorUtils.Colors[settings[Setting.FireLeft]], ColorUtils.Colors[settings[Setting.FireRight]]);
					ColorUtils.UpdateColors(Service.Fire, ColorUtils.Colors[settings[Setting.FireLeftRear]], ColorUtils.Colors[settings[Setting.FireRightRear]], rear: true);
				}
				else
				{
					ColorUtils.UpdateColorsBoth(Service.Fire, ColorUtils.Colors[settings[Setting.FireLeft]], ColorUtils.Colors[settings[Setting.FireRight]]);
				}
				if (Convert.ToBoolean(settings[Setting.ManualRearAmbulance]))
				{
					ColorUtils.UpdateColors(Service.Ambulance, ColorUtils.Colors[settings[Setting.AmbulanceLeft]], ColorUtils.Colors[settings[Setting.AmbulanceRight]]);
					ColorUtils.UpdateColors(Service.Ambulance, ColorUtils.Colors[settings[Setting.AmbulanceLeftRear]], ColorUtils.Colors[settings[Setting.AmbulanceRightRear]], rear: true);
				}
				else
				{
					ColorUtils.UpdateColorsBoth(Service.Ambulance, ColorUtils.Colors[settings[Setting.AmbulanceLeft]], ColorUtils.Colors[settings[Setting.AmbulanceRight]]);
				}
                ColorUtils.UpdatePlowColors(ColorUtils.Colors[settings[Setting.SnowPlowLeft]], ColorUtils.Colors[settings[Setting.SnowPlowRight]]);
				break;
			case 2:
				ColorUtils.UpdateColorsBoth(Service.Police, ColorUtils.Colors["Off"], ColorUtils.Colors["Off"]);
				ColorUtils.UpdateColorsBoth(Service.Fire, ColorUtils.Colors["Off"], ColorUtils.Colors["Off"]);
				ColorUtils.UpdateColorsBoth(Service.Ambulance, ColorUtils.Colors["Off"], ColorUtils.Colors["Off"]);
                ColorUtils.UpdatePlowColors(ColorUtils.Colors["Off"], ColorUtils.Colors["Off"]);
				break;
			case 3:
				ColorUtils.UpdateColorsBoth(Service.Police, ColorUtils.Colors["Red"], ColorUtils.Colors["Blue"]);
				ColorUtils.UpdateColorsBoth(Service.Fire, ColorUtils.Colors["Red"], ColorUtils.Colors["Blue"]);
				ColorUtils.UpdateColorsBoth(Service.Ambulance, ColorUtils.Colors["Red"], ColorUtils.Colors["Blue"]);
                ColorUtils.UpdatePlowColors(ColorUtils.Colors["Red"], ColorUtils.Colors["Orange"]);
				break;
			case 4:
				ColorUtils.UpdateColorsBoth(Service.Police, ColorUtils.Colors["Blue"], ColorUtils.Colors["Blue"]);
				ColorUtils.UpdateColorsBoth(Service.Fire, ColorUtils.Colors["Light Blue"], ColorUtils.Colors["Light Blue"]);
				ColorUtils.UpdateColorsBoth(Service.Ambulance, ColorUtils.Colors["Blue"], ColorUtils.Colors["Blue"]);
                ColorUtils.UpdatePlowColors(ColorUtils.Colors["Blue"], ColorUtils.Colors["Orange"]);
				break;
			case 5:
                ColorUtils.UpdateColorsBoth(Service.Police, ColorUtils.Colors["Red"], ColorUtils.Colors["Red"]);
				ColorUtils.UpdateColorsBoth(Service.Fire, ColorUtils.Colors["Red"], ColorUtils.Colors["Red"]);
				ColorUtils.UpdateColorsBoth(Service.Ambulance, ColorUtils.Colors["Red"], ColorUtils.Colors["Red"]);
                ColorUtils.UpdatePlowColors(ColorUtils.Colors["Red"], ColorUtils.Colors["Orange"]);
				break;
            case 6:
				ColorUtils.UpdateColorsBoth(Service.Police, ColorUtils.Colors["Red"], ColorUtils.Colors["Blue"]);
                ColorUtils.UpdateColors(Service.Fire, ColorUtils.Colors["Red"], ColorUtils.Colors["Red"]);
    			ColorUtils.UpdateColors(Service.Fire, ColorUtils.Colors["Red"], ColorUtils.Colors["Red"], rear: true);
				ColorUtils.UpdateColors(Service.Ambulance, ColorUtils.Colors["Red"], ColorUtils.Colors["Red"]);
				ColorUtils.UpdateColors(Service.Ambulance, ColorUtils.Colors["White"], ColorUtils.Colors["White"], rear: true);
                ColorUtils.UpdatePlowColors(ColorUtils.Colors["Red"], ColorUtils.Colors["Orange"]);
				break;
			}
		}
	}
}
