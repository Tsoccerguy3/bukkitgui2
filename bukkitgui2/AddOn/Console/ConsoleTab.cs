﻿// ConsoleTab.cs in bukkitgui2/bukkitgui2
// Created 2014/01/17
// Last edited at 2014/08/17 11:19
// ©Bertware, visit http://bertware.net

using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Controls;
using Net.Bertware.Bukkitgui2.Core.Configuration;
using Net.Bertware.Bukkitgui2.MinecraftInterop.OutputHandler;
using Net.Bertware.Bukkitgui2.MinecraftInterop.PlayerHandler;
using Net.Bertware.Bukkitgui2.MinecraftInterop.ProcessHandler;

namespace Net.Bertware.Bukkitgui2.AddOn.Console
{
    public partial class ConsoleTab : MetroUserControl, IAddonTab
    {
        public IAddon ParentAddon { get; set; }
	    public static ConsoleTab Reference;

        public ConsoleTab()
        {
            Reference = this;
            InitializeComponent();

            MinecraftOutputHandler.OutputParsed += PrintOutput;
            ProcessHandler.ServerStarting += HandleServerStart;
            ProcessHandler.ServerStopped += HandleServerStop;
            CIConsoleInput.CommandSent += HandleCommandSent;
            PlayerHandler.PlayerListAddition += HandlePlayerAddition;
            PlayerHandler.PlayerListDeletion += HandlePlayerDeletion;


            CIConsoleInput.AutoCompletion = Config.ReadBool("console", "autocompletion", true);
            MCCOut.MessageColorInfo = Color.FromArgb(Config.ReadInt("console", "color_info", Color.Blue.ToArgb()));
            MCCOut.MessageColorPlayerAction =
                Color.FromArgb(Config.ReadInt("console", "color_playeraction", Color.DarkGreen.ToArgb()));
            MCCOut.MessageColorSevere = Color.FromArgb(Config.ReadInt("console", "color_severe", Color.DarkRed.ToArgb()));
            MCCOut.MessageColorWarning =
                Color.FromArgb(Config.ReadInt("console", "color_warning", Color.DarkOrange.ToArgb()));

            MCCOut.ShowDate = Config.ReadBool("console", "date", false);
            MCCOut.ShowTime = Config.ReadBool("console", "time", true);
        }

        /// <summary>
        ///     Remove a player from the listview
        /// </summary>
        /// <param name="player"></param>
        private void HandlePlayerDeletion(Player player)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker) (() => HandlePlayerDeletion(player)));
            }
            else
            {
                SlvPlayers.Items.RemoveByKey(player.Name);
            }
        }

        /// <summary>
        ///     Add a player to the listview
        /// </summary>
        /// <param name="player"></param>
        private void HandlePlayerAddition(Player player)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker) (() => HandlePlayerAddition(player)));
            }
            else
            {
                String[] text = {player.Name, player.Ip};
                ListViewItem lvi = new ListViewItem(text) {Tag = player, Name = player.Name};
                AddListViewItem(lvi);
            }
        }

        /// <summary>
        ///     Add an item to the listview, thread-safe
        /// </summary>
        /// <param name="item">the item to add</param>
        private void AddListViewItem(ListViewItem item)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker) (() => AddListViewItem(item)));
            }
            else
            {
                SlvPlayers.Items.Add(item);
            }
        }

        /// <summary>
        ///     Handle starting server, prepare UI and display text
        /// </summary>
        private void HandleServerStart()
        {
            // clearing console is already performed by the starter routine, so the starter routine can display its own information too.
            SlvPlayers.Items.Clear();
            CIConsoleInput.ClearAutoCompletionHistory();
            WriteOut("Starting a new server");
        }

        /// <summary>
        ///     Handle stopped server, clear UI.
        /// </summary>
        private void HandleServerStop()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker) HandleServerStop);
            }
            else
            {
                SlvPlayers.Items.Clear();
                CIConsoleInput.ClearAutoCompletionHistory();
                WriteOut("The server has stopped");
            }
        }

        /// <summary>
        ///     Print output to the console
        /// </summary>
        /// <param name="text"></param>
        /// <param name="outputParseResult"></param>
        private void PrintOutput(string text, OutputParseResult outputParseResult)
        {
            MCCOut.WriteOutput(outputParseResult.Type, outputParseResult.Message);
        }

        /// <summary>
        ///     Handle a commandsent event from the textbox and redirect it to the server
        /// </summary>
        /// <param name="text"></param>
        private static void HandleCommandSent(string text)
        {
            if (ProcessHandler.IsRunning)
            {
                ProcessHandler.SendInput(text);
            }
        }

        /// <summary>
        ///     Write output to the console. Will be prefixed with [GUI]
        /// </summary>
        /// <param name="text"></param>
        public static void WriteOut(string text)
        {
            Reference.MCCOut.WriteOutput(MessageType.Info, "[GUI] " + text);
        }

		private void ContextPlayersKick_Click(object sender, EventArgs e)
		{
			if (SlvPlayers.SelectedItems.Count < 1) return;
			((Player)(SlvPlayers.SelectedItems[0].Tag)).Kick();
		}

		private void ContextPlayersBan_Click(object sender, EventArgs e)
		{
			if (SlvPlayers.SelectedItems.Count < 1) return;
			((Player)(SlvPlayers.SelectedItems[0].Tag)).Ban();
		}

		private void ContextPlayersBanIp_Click(object sender, EventArgs e)
		{
			if (SlvPlayers.SelectedItems.Count < 1) return;
			((Player)(SlvPlayers.SelectedItems[0].Tag)).BanIp();
		}

		private void ContextPlayersOp_Click(object sender, EventArgs e)
		{
			if (SlvPlayers.SelectedItems.Count < 1) return;
			((Player)(SlvPlayers.SelectedItems[0].Tag)).SetOp(true);
		}

		private void ContextPlayersDeOp_Click(object sender, EventArgs e)
		{
			if (SlvPlayers.SelectedItems.Count < 1) return;
			((Player)(SlvPlayers.SelectedItems[0].Tag)).SetOp(false);
		}
    }
}