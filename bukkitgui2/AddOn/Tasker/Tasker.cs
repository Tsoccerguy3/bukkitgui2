﻿// Tasker.cs in bukkitgui2/bukkitgui2
// Created 2014/01/17
// Last edited at 2014/06/24 18:52
// ©Bertware, visit http://bertware.net

using System.Windows.Forms;

namespace Net.Bertware.Bukkitgui2.AddOn.Tasker
{
	internal class Tasker : IAddon
	{
		private UserControl _tab;


		/// <summary>
		///     The addon name, ideally this name is the same as used in the tabpage
		/// </summary>
		public string Name
		{
			get { return "Tasker"; }
		}

		/// <summary>
		///     True if this addon has a tab page
		/// </summary>
		public bool HasTab
		{
			get { return true; }
		}

		/// <summary>
		///     True if this addon has a config field
		/// </summary>
		public bool HasConfig
		{
			get { return false; }
		}

		/// <summary>
		///     Initialize all functions and the tabcontrol
		/// </summary>
		public void Initialize()
		{
			_tab = new TaskerTab {Text = Name, ParentAddon = this};
			ConfigPage = null;
		}

		public void Dispose()
		{
			// nothing to do
		}

		/// <summary>
		///     The tab control for this addon
		/// </summary>
		/// <returns>Returns the tabpage</returns>
		UserControl IAddon.TabPage
		{
			get { return _tab; }
		}

		public UserControl ConfigPage { get; private set; }

		public bool CanDisable
		{
			get { return true; }
		}
	}
}