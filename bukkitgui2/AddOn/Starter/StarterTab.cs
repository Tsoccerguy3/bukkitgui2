﻿// StarterTab.cs in bukkitgui2/bukkitgui2
// Created 2014/01/17
// 
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file,
// you can obtain one at http://mozilla.org/MPL/2.0/.
// 
// ©Bertware, visit http://bertware.net

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using Net.Bertware.Bukkitgui2.AddOn.Console;
using Net.Bertware.Bukkitgui2.Core;
using Net.Bertware.Bukkitgui2.Core.Configuration;
using Net.Bertware.Bukkitgui2.Core.Logging;
using Net.Bertware.Bukkitgui2.Core.Util.Performance;
using Net.Bertware.Bukkitgui2.MinecraftServers;
using Net.Bertware.Bukkitgui2.UI;

namespace Net.Bertware.Bukkitgui2.AddOn.Starter
{
	public partial class StarterTab : MetroUserControl, IAddonTab
	{
		private readonly Dictionary<string, IMinecraftServer> _servers;

		public IAddon ParentAddon { get; set; }

		/// <summary>
		///     The reference to the custom control used by some servers
		/// </summary>
		private UserControl _customControl;

		/// <summary>
		///     True if initialization is finished and everything can handle user input
		/// </summary>
		private Boolean _ready;

		public StarterTab()
		{
			InitializeComponent();
			_servers = MinecraftServerLoader.GetAvailableServers();

			LoadUi();
		}

		/// <summary>
		///     Add content and settings to the UI
		/// </summary>
		private void LoadUi()
		{
			Logger.Log(LogLevel.Info, "StarterTab", "Loading UI");
			// Add all servers to the list
			CBServerType.Items.Clear();
			foreach (string servername in _servers.Keys)
			{
				CBServerType.Items.Add(servername);
			}

			int selectedServer = Config.ReadInt("Starter", "ServerType", 0);

			// check if this server id exists
			if (selectedServer < CBServerType.Items.Count)
			{
				CBServerType.SelectedIndex = selectedServer;
			}
			else
			{
				if (CBServerType.Items.Count > 0) CBServerType.SelectedIndex = 0;
			}
			LoadServer();
			//Selecting a server will enable/disable the available/unavailable features

			// Cache total amount of ram, set maximum values
			int totalMb = Convert.ToInt32(MemoryCounter.TotalMemoryMb());
			TBMaxRam.Maximum = totalMb;
			TBMinRam.Maximum = totalMb;
			NumMaxRam.Maximum = totalMb;
			NumMinRam.Maximum = totalMb;

			int minRamValue = Config.ReadInt("Starter", "MinRam", 128);
			int maxRamValue = Config.ReadInt("Starter", "MaxRam", 1024);

			// check for sub-zero values
			if (minRamValue < 0)
			{
				minRamValue = 0;
			}
			if (maxRamValue < 0)
			{
				maxRamValue = 0;
			}

			// value should be less than maximum value
			if (maxRamValue < NumMaxRam.Maximum)
			{
				NumMaxRam.Value = maxRamValue;
			}
			else
			{
				NumMaxRam.Value = 1024;
			}
			if (minRamValue < NumMinRam.Maximum)
			{
				NumMinRam.Value = minRamValue;
			}
			else
			{
				NumMaxRam.Value = 1024;
			}

			// Add options for installed java versions
			CBJavaVersion.Items.Clear();
			if (JavaApi.IsInstalled(JavaVersion.Jre6X32))
			{
				CBJavaVersion.Items.Add("Java 6 - 32 bit");
			}
			if (JavaApi.IsInstalled(JavaVersion.Jre6X64))
			{
				CBJavaVersion.Items.Add("Java 6 - 64 bit");
			}
			if (JavaApi.IsInstalled(JavaVersion.Jre7X32))
			{
				CBJavaVersion.Items.Add("Java 7 - 32 bit");
			}
			if (JavaApi.IsInstalled(JavaVersion.Jre7X64))
			{
				CBJavaVersion.Items.Add("Java 7 - 64 bit");
			}
			if (JavaApi.IsInstalled(JavaVersion.Jre8X32))
			{
				CBJavaVersion.Items.Add("Java 8 - 32 bit");
			}
			if (JavaApi.IsInstalled(JavaVersion.Jre8X64))
			{
				CBJavaVersion.Items.Add("Java 8 - 64 bit");
			}

			int javaType = Config.ReadInt("Starter", "JavaVersion", 0);
			if (javaType < CBJavaVersion.Items.Count)
			{
				CBJavaVersion.SelectedIndex = javaType;
			}
			else
			{
				if (CBJavaVersion.Items.Count > 0) CBJavaVersion.SelectedIndex = 0;
			}

			TxtJarFile.Text = Config.ReadString("Starter", "JarFile", "");
			TxtOptArg.Text = Config.ReadString("Starter", "OptionalArguments", "");
			TxtOptFlag.Text = Config.ReadString("Starter", "OptionalFlags", "");

			Logger.Log(LogLevel.Info, "StarterTab", "UI Loaded");
			_ready = true;
		}

		/// <summary>
		///     Load all settings/buttons for a selected server
		/// </summary>
		private void LoadServer()
		{
			GBServer.UseWaitCursor = true;

			IMinecraftServer server = GetSelectedServer();
			if (server == null) return;

			Logger.Log(LogLevel.Info, "StarterTab", "Loading server: " + server.Name);

			// If this server doesn't use a custom assembly, use the java settings
			CBJavaVersion.Enabled = !server.HasCustomAssembly;
			NumMaxRam.Enabled = !server.HasCustomAssembly;
			NumMinRam.Enabled = !server.HasCustomAssembly;
			TBMaxRam.Enabled = !server.HasCustomAssembly;
			TBMinRam.Enabled = !server.HasCustomAssembly;
			TxtOptArg.Enabled = !server.HasCustomAssembly;
			TxtOptFlag.Enabled = !server.HasCustomAssembly;
			TxtJarFile.Enabled = !server.HasCustomAssembly;

			// If there is a custom settings control, load it
			if (server.HasCustomSettingsControl)
			{
				_customControl = server.CustomSettingsControl;
				GBCustomSettings.Controls.Clear();
				GBCustomSettings.Controls.Add(_customControl);
				GBCustomSettings.Controls[0].Dock = DockStyle.Fill;
			}
			else
			{
				_customControl = null;
				GBCustomSettings.Controls.Clear();
			}

			GBServer.UseWaitCursor = false;
			Logger.Log(LogLevel.Info, "StarterTab", "Loaded server: " + server.Name);
		}

		/// <summary>
		///     Get the IMinecraftServer object for the selected item
		/// </summary>
		/// <returns>The selected server (object)</returns>
		public IMinecraftServer GetSelectedServer()
		{
			if (CBServerType.SelectedItem == null) return null;
			string serverName = CBServerType.SelectedItem.ToString();
			return _servers[serverName];
		}

		/// <summary>
		///     Get the selected java version
		/// </summary>
		/// <returns>The selected java version as enum</returns>
		public JavaVersion GetSelectedJavaVersion()
		{
			if (CBJavaVersion.SelectedIndex < 0) return 0;

			string selectedText = CBJavaVersion.SelectedItem.ToString();
			if (Regex.IsMatch(selectedText, "(.*?)6(.*?)32"))
			{
				return JavaVersion.Jre6X32;
			}
			if (Regex.IsMatch(selectedText, "(.*?)6(.*?)64"))
			{
				return JavaVersion.Jre6X64;
			}
			if (Regex.IsMatch(selectedText, "(.*?)7(.*?)32"))
			{
				return JavaVersion.Jre7X32;
			}
			if (Regex.IsMatch(selectedText, "(.*?)7(.*?)64"))
			{
				return JavaVersion.Jre7X64;
			}
			if (Regex.IsMatch(selectedText, "(.*?)8(.*?)32"))
			{
				return JavaVersion.Jre8X32;
			}
			if (Regex.IsMatch(selectedText, "(.*?)8(.*?)64"))
			{
				return JavaVersion.Jre8X64;
			}
			return JavaVersion.Jre7X32;
		}

		/// <summary>
		///     Launch the server, get all settings from
		/// </summary>
		public void DoServerLaunch(Boolean automated = false)
		{
			if (InvokeRequired)
			{
				Invoke((MethodInvoker) (() => DoServerLaunch(automated)));
			}
			else
			{
				ConsoleTab.Reference.MCCOut.Clear();

				if (!ValidateInput())
				{
					MetroMessageBox.Show(FindForm(),
						"The server could not be started: one or more settings are incorrect. See the starter tab for more details",
						"Server could not be started", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				IMinecraftServer server = GetSelectedServer();
				Starter starter = ParentAddon as Starter;

				Logger.Log(LogLevel.Info, "StarterTab", "starting server: " + server.Name);

				// We need access to a starter object (the parent)
				if (starter == null)
				{
					Logger.Log(LogLevel.Severe, "StarterTab", "Failed to start server", "No starter object found");
					return;
				}
                
				if (!server.HasCustomAssembly)
				{
					starter.LaunchServer(
						server,
						GetSelectedJavaVersion(),
						TxtJarFile.Text,
						Convert.ToUInt32(NumMinRam.Value),
						Convert.ToUInt32(NumMaxRam.Value),
						TxtOptArg.Text,
						TxtOptFlag.Text,
						automated);
				}
				else
				{
					starter.LaunchServer(server, _customControl);
				}
			}
		}

		/// <summary>
		///     Validate the input. Return true if input is valid and server can be started.
		/// </summary>
		/// <returns>Returns true if all input is valid</returns>
		/// <remarks>
		///     Important checks: RAM less than 1024Mb on 32bit, java installed, valid .jar file
		///     This method is NOT thread-safe
		/// </remarks>
		public Boolean ValidateInput()
		{
			if (GetSelectedServer().HasCustomAssembly) return true;

			BtnLaunch.Enabled = false;
			errorProvider.SetError(BtnLaunch, "The provided settings are invalid");

			if (!File.Exists(TxtJarFile.Text))
			{
				errorProvider.SetError(TxtJarFile,
					"File does not exist. Press the download button or change the file path to an existing file.");
				return false;
			}
			if (!TxtJarFile.Text.EndsWith(".jar"))
			{
				errorProvider.SetError(TxtJarFile,
					"File should be a *.jar file. This file is not a .jar file, or the extension is incorrect.");
				return false;
			}
			if (new FileInfo(TxtJarFile.Text).Length < 1024)
			{
				errorProvider.SetError(TxtJarFile, "File is corrupt. Download again.");
				return false;
			}

			if (JavaApi.Is32Bitversion(GetSelectedJavaVersion()))
			{
				if (NumMaxRam.Value > 1024)
				{
					NumMaxRam.Value = 1024;
					errorProvider.SetError(NumMaxRam,
						"You are using a 32bit version of java. In this case, RAM is limited to 1024mb due to java limitations.");
				}
				else
				{
					errorProvider.SetError(NumMaxRam, "");
				}
				if (NumMinRam.Value > 1024)
				{
					NumMinRam.Value = 1024;
					errorProvider.SetError(NumMinRam,
						"You are using a 32bit version of java. In this case, RAM is limited to 1024mb due to java limitations.");
				}
				if (NumMinRam.Value > 768)
				{
					errorProvider.SetError(NumMinRam,
						"To prevent issues, a value below 768mb is recommended here. Recommended value: 128mb");
				}
				else
				{
					errorProvider.SetError(NumMinRam, "");
				}
			}

			BtnLaunch.Enabled = true;
			errorProvider.SetError(BtnLaunch, "");
			errorProvider.SetError(TxtJarFile, "");
			return true;
		}


		// UI events

		/// <summary>
		///     Handle SelectedIndexChanged event for server type combobox, and load the new server type
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CbServerTypeSelectedIndexChanged(object sender, EventArgs e)
		{
			if (!_ready)
			{
				return; //if not initialized, don't detect changes
			}
			Config.WriteInt("Starter", "ServerType", CBServerType.SelectedIndex);
			LoadServer();
		}

		/// <summary>
		///     Launch a new server
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnLaunch_Click(object sender, EventArgs e)
		{
			DoServerLaunch();
			MainForm.Reference.GoToTab("console");
		}

		/// <summary>
		///     Trackbar scrolled, also adjust numeric value
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TbMinRamScroll(object sender, EventArgs e)
		{
			if (!_ready)
			{
				return; //if not initialized, don't detect changes
			}
			Config.WriteInt("Starter", "MinRam", TBMinRam.Value);
			NumMinRam.Value = TBMinRam.Value;
		}

		/// <summary>
		///     Trackbar scrolled, also adjust numeric value
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TbMaxRamScroll(object sender, EventArgs e)
		{
			if (!_ready)
			{
				return; //if not initialized, don't detect changes
			}
			Config.WriteInt("Starter", "MaxRam", TBMaxRam.Value);
			NumMaxRam.Value = TBMaxRam.Value;
		}

		/// <summary>
		///     Numeric value changed, adjust trackbar and check if minimum value is smaller than the maximum value
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NumMinRam_ValueChanged(object sender, EventArgs e)
		{
			// If trackbar doesn't show the same amount, adjust trackbar
			if (TBMinRam.Value != NumMinRam.Value)
			{
				TBMinRam.Value = Convert.ToInt32(NumMinRam.Value);
			}

		    if (TBMinRam.Value > TBMaxRam.Value)
		    {
		        TBMaxRam.Value = TBMinRam.Value;
		    }

            if (!_ready)
			{
				return; //if not initialized, don't detect changes
			}
			Config.WriteInt("Starter", "MinRam", Convert.ToInt32(NumMinRam.Value));

			// if minram goes higer than maxram, adjust maxram
			if (NumMinRam.Value > NumMaxRam.Value)
			{
				NumMaxRam.Value = NumMinRam.Value; // keep the value of the item we're changing
			}
			ValidateInput();
		}

		/// <summary>
		///     Numeric value changed, adjust trackbar and check if minimum value is smaller than the maximum value
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NumMaxRam_ValueChanged(object sender, EventArgs e)
		{
			if (TBMaxRam.Value != NumMaxRam.Value)
			{
				TBMaxRam.Value = Convert.ToInt32(NumMaxRam.Value);
			}

		    if (TBMaxRam.Value < TBMinRam.Value)
		    {
		        TBMinRam.Value = TBMaxRam.Value;
		    }

            if (!_ready)
			{
				return; //if not initialized, don't detect changes
			}
			Config.WriteInt("Starter", "MaxRam", Convert.ToInt32(NumMaxRam.Value));

			if (NumMinRam.Value > NumMaxRam.Value)
			{
				NumMinRam.Value = NumMaxRam.Value; // keep the value of the item we're changing
			}
			ValidateInput();
		}

		/// <summary>
		///     Browse for a jar server file
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnBrowseJarFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog
			{
				Title = "Select server file",
				InitialDirectory = Share.AssemblyLocation,
				Filter = ("Java .jar files |*.jar"),
				CheckFileExists = true,
				Multiselect = false
			};
			dialog.ShowDialog();
			TxtJarFile.Text = dialog.FileName; //this will also trigger the save of this value
		}

		/// <summary>
		///     Handle changed text for the jar file path. Save the new value.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtJarFile_TextChanged(object sender, EventArgs e)
		{
			if (!_ready)
			{
				return; //if not initialized, don't detect changes
			}
			ValidateInput();
			Config.WriteString("Starter", "JarFile", TxtJarFile.Text);
		}

		/// <summary>
		///     Handle changed text for the custom arguments. Save the new value.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtOptArg_TextChanged(object sender, EventArgs e)
		{
			if (!_ready)
			{
				return; //if not initialized, don't detect changes
			}
			Config.WriteString("Starter", "OptionalArguments", TxtOptArg.Text);
		}

		/// <summary>
		///     Handle changed text for the custom flags. Save the new value.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtOptFlag_TextChanged(object sender, EventArgs e)
		{
			if (!_ready)
			{
				return; //if not initialized, don't detect changes
			}
			Config.WriteString("Starter", "OptionalFlags", TxtOptFlag.Text);
		}

		/// <summary>
		///     Handle a change in the selected java version. Save the new value.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CbJavaVersionSelectedIndexChanged(object sender, EventArgs e)
		{
			if (!_ready)
			{
				return; //if not initialized, don't detect changes
			}
			ValidateInput();
			Config.WriteInt("Starter", "JavaVersion", CBJavaVersion.SelectedIndex);
		}

		/// <summary>
		///     Get the path of the selected java instance
		/// </summary>
		/// <returns></returns>
		public string GetSelectedJavaPath()
		{
			return JavaApi.GetJavaPath(GetSelectedJavaVersion());
		}

		/// <summary>
		///     Get the path of the jar file
		/// </summary>
		/// <returns></returns>
		public string GetSelectedServerPath()
		{
			return TxtJarFile.Text;
		}

		/// <summary>
		///     Get the path of the jar file
		/// </summary>
		/// <returns></returns>
		public Control GetCustomSettingsControl()
		{
			if (GBCustomSettings.Controls.Count < 1) return null;
			return GBCustomSettings.Controls[0];
		}
	}
}