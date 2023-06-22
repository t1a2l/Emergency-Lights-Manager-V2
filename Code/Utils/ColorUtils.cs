using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Emergency_Lights_Manager_V2.Utils.Enums;

namespace Emergency_Lights_Manager_V2.Utils 
{
	public class ColorUtils 
	{
		public static Dictionary<string, Color> Colors = new Dictionary<string, Color>
		{
			{
				"White",
				new Color(1f, 0.98f, 0.96f)
			},
			{
				"Red",
				new Color(1f, 0f, 0f)
			},
			{
				"Blue",
				new Color(0f, 0.5f, 1f)
			},
			{
				"Light Blue",
				new Color(0.5f, 0.75f, 1f)
			},
			{
				"Green",
				new Color(0f, 1f, 0f)
			},
			{
				"Purple",
				new Color(0.5f, 0f, 1f)
			},
			{
				"Orange",
				new Color(1f, 0.75f, 0f)
			},
			{
				"Off",
				new Color(0f, 0f, 0f)
			}
		};

		public static string[] ColorNames = Colors.Keys.ToArray();

		public static void UpdatePlowColors(Color left, Color right)
        {
            ChangeLightColor("Snowplow Light 1",  left);
            ChangeLightColor("Snowplow Light 2", right);
        }


		public static void UpdateColorsBoth(Service sv, Color left, Color right)
		{
			UpdateColors(sv, left, right);
			UpdateColors(sv, left, right, rear: true);
		}

		public static void UpdateColors(Service sv, Color left, Color right, bool rear = false)
		{
			UpdateColor(sv, Side.Left, left, rear);
			UpdateColor(sv, Side.Right, right, rear);
		}

		private static void UpdateColor(Service sv, Side s, Color c, bool rear = false)
		{
			if (!rear || sv != 0)
			{
				object obj;
				switch (sv)
				{
				case Service.Police:
					obj = "Police Car";
					break;
				case Service.Fire:
					obj = "Fire Truck";
					break;
				default:
					obj = "Ambulance";
					break;
				}
				string str = (string)obj;
				string str2 = (s != 0) ? "Right" : "Left";
				string str3 = (!rear) ? string.Empty : "2";
				ChangeLightColor(str + " Light " + str2 + str3, c);
			}
		}

		private static void ChangeLightColor(string effectName, Color color)
		{
			EffectCollection effectCollection = UnityEngine.Object.FindObjectOfType<EffectCollection>();
			if (effectCollection == null)
			{
				return;
			}
			EffectInfo effectInfo = effectCollection.m_effects.FirstOrDefault((EffectInfo e) => e.name == effectName);
			if (!(effectInfo == null))
			{
				Light component = effectInfo.GetComponent<Light>();
				if (!(component == null))
				{
					component.color = color;
				}
			}
		}
	}
}
