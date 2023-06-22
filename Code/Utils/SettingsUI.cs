using ColossalFramework.UI;
using ICities;
using System;
using UnityEngine;
using static Emergency_Lights_Manager_V2.Utils.Enums;

namespace Emergency_Lights_Manager_V2.Utils 
{
	public class SettingsUI 
	{
		private static UITabstrip strip;

		private static UITabContainer container;

		private UICheckBox chkManualRearFire;

		private UICheckBox chkManualRearAmbulance;

		private UIDropDown ddFireLeftRear;

		private UIDropDown ddFireRightRear;

		private UIDropDown ddAmbulanceLeftRear;

		private UIDropDown ddAmbulanceRightRear;

		private static bool _customSettingsVisiblity;

		public static bool CustomSettingsVisibility
		{
			get
			{
				return _customSettingsVisiblity;
			}
			set
			{
				_customSettingsVisiblity = value;
				if (_customSettingsVisiblity)
				{
					strip.Show();
					container.Show();
				}
				else
				{
					strip.Hide();
					container.Hide();
				}
			}
		}

		private void RearFireVisibility(bool chkd)
		{
			ddFireLeftRear.parent.isVisible = chkd;
			ddFireRightRear.parent.isVisible = chkd;
		}

		private void RearAmbulanceVisibility(bool chkd)
		{
			ddAmbulanceLeftRear.parent.isVisible = chkd;
			ddAmbulanceRightRear.parent.isVisible = chkd;
		}

		public void OnSettingsUI(UIHelperBase helper)
		{
			Utils.LoadSettings();
			UIDropDown uIDropDown = (UIDropDown)helper.AddDropdown("Select Preset", Utils.EmergencyLightPresets, Utils.SelectedPreset, delegate(int sel)
			{
				if (sel == 1)
				{
					CustomSettingsVisibility = true;
				}
				else
				{
					CustomSettingsVisibility = false;
				}
				Utils.SelectedPreset = sel;
				if (Mod.loaded)
				{
					Utils.Apply();
				}
			});
			UIScrollablePanel uIScrollablePanel = ((UIHelper)helper).self as UIScrollablePanel;
			uIScrollablePanel.autoLayout = false;
			int num = 100;
			int num2 = 10;
			int num3 = 40;
			strip = uIScrollablePanel.AddUIComponent<UITabstrip>();
			strip.relativePosition = new Vector3(num2, num);
			strip.size = new Vector2(744 - num2, num3);
			container = uIScrollablePanel.AddUIComponent<UITabContainer>();
			container.relativePosition = new Vector3(num2, num3 + num);
			container.size = new Vector3(744 - num2, 713 - num);
			strip.tabPages = container;
			UIButton uIButton = (UIButton)UITemplateManager.Peek("OptionsButtonTemplate");
			UIButton uIButton2 = strip.AddTab("Police Car", uIButton, fillText: true);
			uIButton2.textColor = uIButton.textColor;
			uIButton2.pressedTextColor = uIButton.pressedTextColor;
			uIButton2.hoveredTextColor = uIButton.hoveredTextColor;
			uIButton2.focusedTextColor = uIButton.hoveredTextColor;
			uIButton2.disabledTextColor = uIButton.hoveredTextColor;
			UIPanel uIPanel = strip.tabContainer.components[0] as UIPanel;
			uIPanel.autoLayout = true;
			uIPanel.wrapLayout = true;
			uIPanel.autoLayoutDirection = LayoutDirection.Horizontal;
			UIHelper uIHelper = new UIHelper(uIPanel);
			uIHelper.AddSpace(15);
			uIHelper.AddDropdown("Left", ColorUtils.ColorNames, Array.IndexOf(ColorUtils.ColorNames, Utils.settings[Setting.PoliceLeft]), delegate(int sel)
			{
				Utils.settings[Setting.PoliceLeft] = ColorUtils.ColorNames[sel];
				Utils.ExportSettings();
				if (Mod.loaded)
				{
					Utils.Apply();
				}
			});
			uIHelper.AddDropdown("Right", ColorUtils.ColorNames, Array.IndexOf(ColorUtils.ColorNames, Utils.settings[Setting.PoliceRight]), delegate(int sel)
			{
				Utils.settings[Setting.PoliceRight] = ColorUtils.ColorNames[sel];
				Utils.ExportSettings();
				if (Mod.loaded)
				{
					Utils.Apply();
				}
			});
			uIButton2 = strip.AddTab("Fire Truck");
			uIButton2.textColor = uIButton.textColor;
			uIButton2.pressedTextColor = uIButton.pressedTextColor;
			uIButton2.hoveredTextColor = uIButton.hoveredTextColor;
			uIButton2.focusedTextColor = uIButton.hoveredTextColor;
			uIButton2.disabledTextColor = uIButton.hoveredTextColor;
			uIPanel = (strip.tabContainer.components[1] as UIPanel);
			uIPanel.autoLayout = true;
			uIPanel.wrapLayout = true;
			uIPanel.autoLayoutDirection = LayoutDirection.Horizontal;
			uIHelper = new UIHelper(uIPanel);
			uIHelper.AddSpace(15);
			uIHelper.AddDropdown("Left", ColorUtils.ColorNames, Array.IndexOf(ColorUtils.ColorNames, Utils.settings[Setting.FireLeft]), delegate(int sel)
			{
				Utils.settings[Setting.FireLeft] = ColorUtils.ColorNames[sel];
				Utils.ExportSettings();
				if (Mod.loaded)
				{
					Utils.Apply();
				}
			});
			uIHelper.AddDropdown("Right", ColorUtils.ColorNames, Array.IndexOf(ColorUtils.ColorNames, Utils.settings[Setting.FireRight]), delegate(int sel)
			{
				Utils.settings[Setting.FireRight] = ColorUtils.ColorNames[sel];
				Utils.ExportSettings();
				if (Mod.loaded)
				{
					Utils.Apply();
				}
			});
			uIHelper.AddSpace(15);
			chkManualRearFire = (uIHelper.AddCheckbox("Configure Rear Lights Separately", Convert.ToBoolean(Utils.settings[Setting.ManualRearFire]), delegate(bool chkd)
			{
				Utils.settings[Setting.ManualRearFire] = chkd.ToString();
				Utils.ExportSettings();
				RearFireVisibility(chkd);
				if (Mod.loaded)
				{
					Utils.Apply();
				}
			}) as UICheckBox);
			chkManualRearFire.width = 744f;
			uIHelper.AddSpace(15);
			ddFireLeftRear = (UIDropDown)uIHelper.AddDropdown("Left", ColorUtils.ColorNames, Array.IndexOf(ColorUtils.ColorNames, Utils.settings[Setting.FireLeftRear]), delegate(int sel)
			{
				Utils.settings[Setting.FireLeftRear] = ColorUtils.ColorNames[sel];
				Utils.ExportSettings();
				if (Mod.loaded)
				{
					Utils.Apply();
				}
			});
			ddFireRightRear = (UIDropDown)uIHelper.AddDropdown("Right", ColorUtils.ColorNames, Array.IndexOf(ColorUtils.ColorNames, Utils.settings[Setting.FireRightRear]), delegate(int sel)
			{
				Utils.settings[Setting.FireRightRear] = ColorUtils.ColorNames[sel];
				Utils.ExportSettings();
				if (Mod.loaded)
				{
					Utils.Apply();
				}
			});
			RearFireVisibility(Convert.ToBoolean(Utils.settings[Setting.ManualRearFire]));
			uIButton2 = strip.AddTab("Ambulance");
			uIButton2.textColor = uIButton.textColor;
			uIButton2.pressedTextColor = uIButton.pressedTextColor;
			uIButton2.hoveredTextColor = uIButton.hoveredTextColor;
			uIButton2.focusedTextColor = uIButton.hoveredTextColor;
			uIButton2.disabledTextColor = uIButton.hoveredTextColor;
			uIPanel = (strip.tabContainer.components[2] as UIPanel);
			uIPanel.autoLayout = true;
			uIPanel.wrapLayout = true;
			uIPanel.autoLayoutDirection = LayoutDirection.Horizontal;
			uIHelper = new UIHelper(uIPanel);
			uIHelper.AddSpace(15);
			uIHelper.AddDropdown("Left", ColorUtils.ColorNames, Array.IndexOf(ColorUtils.ColorNames, Utils.settings[Setting.AmbulanceLeft]), delegate(int sel)
			{
				Utils.settings[Setting.AmbulanceLeft] = ColorUtils.ColorNames[sel];
				Utils.ExportSettings();
				if (Mod.loaded)
				{
					Utils.Apply();
				}
			});
			uIHelper.AddDropdown("Right", ColorUtils.ColorNames, Array.IndexOf(ColorUtils.ColorNames, Utils.settings[Setting.AmbulanceRight]), delegate(int sel)
			{
				Utils.settings[Setting.AmbulanceRight] = ColorUtils.ColorNames[sel];
				Utils.ExportSettings();
				if (Mod.loaded)
				{
					Utils.Apply();
				}
			});
			uIHelper.AddSpace(15);
			chkManualRearAmbulance = (uIHelper.AddCheckbox("Configure Rear Lights Separately", Convert.ToBoolean(Utils.settings[Setting.ManualRearAmbulance]), delegate(bool chkd)
			{
				Utils.settings[Setting.ManualRearAmbulance] = chkd.ToString();
				Utils.ExportSettings();
				RearAmbulanceVisibility(chkd);
				if (Mod.loaded)
				{
					Utils.Apply();
				}
			}) as UICheckBox);
			chkManualRearAmbulance.width = 744f;
			uIHelper.AddSpace(15);
			ddAmbulanceLeftRear = (UIDropDown)uIHelper.AddDropdown("Left", ColorUtils.ColorNames, Array.IndexOf(ColorUtils.ColorNames, Utils.settings[Setting.AmbulanceLeftRear]), delegate(int sel)
			{
				Utils.settings[Setting.AmbulanceLeftRear] = ColorUtils.ColorNames[sel];
				Utils.ExportSettings();
				if (Mod.loaded)
				{
					Utils.Apply();
				}
			});
			ddAmbulanceRightRear = (UIDropDown)uIHelper.AddDropdown("Right", ColorUtils.ColorNames, Array.IndexOf(ColorUtils.ColorNames, Utils.settings[Setting.AmbulanceRightRear]), delegate(int sel)
			{
				Utils.settings[Setting.AmbulanceRightRear] = ColorUtils.ColorNames[sel];
				Utils.ExportSettings();
				if (Mod.loaded)
				{
					Utils.Apply();
				}
			});
			RearAmbulanceVisibility(Convert.ToBoolean(Utils.settings[Setting.ManualRearAmbulance]));
            
            uIButton2 = strip.AddTab("Rotary (e.g. Snow Plow)", uIButton, fillText: true);
			uIButton2.textColor = uIButton.textColor;
			uIButton2.pressedTextColor = uIButton.pressedTextColor;
			uIButton2.hoveredTextColor = uIButton.hoveredTextColor;
			uIButton2.focusedTextColor = uIButton.hoveredTextColor;
			uIButton2.disabledTextColor = uIButton.hoveredTextColor;
			uIPanel = strip.tabContainer.components[3] as UIPanel;
			uIPanel.autoLayout = true;
			uIPanel.wrapLayout = true;
			uIPanel.autoLayoutDirection = LayoutDirection.Horizontal;
			uIHelper = new UIHelper(uIPanel);
			uIHelper.AddSpace(15);
			uIHelper.AddDropdown("Left", ColorUtils.ColorNames, Array.IndexOf(ColorUtils.ColorNames, Utils.settings[Setting.SnowPlowLeft]), delegate(int sel)
			{
				Utils.settings[Setting.SnowPlowLeft] = ColorUtils.ColorNames[sel];
				Utils.ExportSettings();
				if (Mod.loaded)
				{
					Utils.Apply();
				}
			});
			uIHelper.AddDropdown("Right", ColorUtils.ColorNames, Array.IndexOf(ColorUtils.ColorNames, Utils.settings[Setting.SnowPlowRight]), delegate(int sel)
			{
				Utils.settings[Setting.SnowPlowRight] = ColorUtils.ColorNames[sel];
				Utils.ExportSettings();
				if (Mod.loaded)
				{
					Utils.Apply();
				}
			});

			strip.selectedIndex = -1;
			strip.selectedIndex = 0;
			int selectedIndex = uIDropDown.selectedIndex;
			uIDropDown.selectedIndex = -1;
			uIDropDown.selectedIndex = selectedIndex;

		}

	}
}
