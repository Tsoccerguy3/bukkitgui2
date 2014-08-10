﻿namespace Net.Bertware.Bukkitgui2.MinecraftServers.Tools
{
    partial class JsonApiCredentialsSettingsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.GBSuperStartRemoteServer = new System.Windows.Forms.GroupBox();
			this.MTxtRemotePassword = new System.Windows.Forms.MaskedTextBox();
			this.MTxtRemoteSalt = new System.Windows.Forms.MaskedTextBox();
			this.Label5 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.NumRemotePort = new System.Windows.Forms.NumericUpDown();
			this.TxtRemoteUsername = new System.Windows.Forms.TextBox();
			this.TxtRemoteHost = new System.Windows.Forms.TextBox();
			this.GBSuperStartRemoteServer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumRemotePort)).BeginInit();
			this.SuspendLayout();
			// 
			// GBSuperStartRemoteServer
			// 
			this.GBSuperStartRemoteServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GBSuperStartRemoteServer.Controls.Add(this.MTxtRemotePassword);
			this.GBSuperStartRemoteServer.Controls.Add(this.MTxtRemoteSalt);
			this.GBSuperStartRemoteServer.Controls.Add(this.Label5);
			this.GBSuperStartRemoteServer.Controls.Add(this.Label4);
			this.GBSuperStartRemoteServer.Controls.Add(this.Label3);
			this.GBSuperStartRemoteServer.Controls.Add(this.Label2);
			this.GBSuperStartRemoteServer.Controls.Add(this.Label1);
			this.GBSuperStartRemoteServer.Controls.Add(this.NumRemotePort);
			this.GBSuperStartRemoteServer.Controls.Add(this.TxtRemoteUsername);
			this.GBSuperStartRemoteServer.Controls.Add(this.TxtRemoteHost);
			this.GBSuperStartRemoteServer.Location = new System.Drawing.Point(3, 3);
			this.GBSuperStartRemoteServer.Name = "GBSuperStartRemoteServer";
			this.GBSuperStartRemoteServer.Size = new System.Drawing.Size(635, 107);
			this.GBSuperStartRemoteServer.TabIndex = 8;
			this.GBSuperStartRemoteServer.TabStop = false;
			this.GBSuperStartRemoteServer.Text = "Remote Server";
			// 
			// MTxtRemotePassword
			// 
			this.MTxtRemotePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MTxtRemotePassword.Location = new System.Drawing.Point(479, 39);
			this.MTxtRemotePassword.Name = "MTxtRemotePassword";
			this.MTxtRemotePassword.Size = new System.Drawing.Size(150, 20);
			this.MTxtRemotePassword.TabIndex = 11;
			this.MTxtRemotePassword.UseSystemPasswordChar = true;
			// 
			// MTxtRemoteSalt
			// 
			this.MTxtRemoteSalt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MTxtRemoteSalt.Location = new System.Drawing.Point(87, 65);
			this.MTxtRemoteSalt.Name = "MTxtRemoteSalt";
			this.MTxtRemoteSalt.Size = new System.Drawing.Size(287, 20);
			this.MTxtRemoteSalt.TabIndex = 12;
			this.MTxtRemoteSalt.UseSystemPasswordChar = true;
			// 
			// Label5
			// 
			this.Label5.Location = new System.Drawing.Point(9, 68);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(72, 13);
			this.Label5.TabIndex = 9;
			this.Label5.Text = "salt:";
			this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Label4
			// 
			this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Label4.Location = new System.Drawing.Point(383, 15);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(90, 13);
			this.Label4.TabIndex = 8;
			this.Label4.Text = "port:";
			this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Label3
			// 
			this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Label3.Location = new System.Drawing.Point(380, 42);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(93, 13);
			this.Label3.TabIndex = 7;
			this.Label3.Text = "password:";
			this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Label2
			// 
			this.Label2.Location = new System.Drawing.Point(9, 42);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(72, 13);
			this.Label2.TabIndex = 6;
			this.Label2.Text = "username:";
			this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Label1
			// 
			this.Label1.Location = new System.Drawing.Point(9, 16);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(72, 13);
			this.Label1.TabIndex = 5;
			this.Label1.Text = "host:";
			this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// NumRemotePort
			// 
			this.NumRemotePort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.NumRemotePort.Location = new System.Drawing.Point(479, 13);
			this.NumRemotePort.Maximum = new decimal(new int[] {
            65532,
            0,
            0,
            0});
			this.NumRemotePort.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			this.NumRemotePort.Name = "NumRemotePort";
			this.NumRemotePort.Size = new System.Drawing.Size(150, 20);
			this.NumRemotePort.TabIndex = 9;
			this.NumRemotePort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NumRemotePort.Value = new decimal(new int[] {
            20059,
            0,
            0,
            0});
			// 
			// TxtRemoteUsername
			// 
			this.TxtRemoteUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TxtRemoteUsername.Location = new System.Drawing.Point(87, 39);
			this.TxtRemoteUsername.Name = "TxtRemoteUsername";
			this.TxtRemoteUsername.Size = new System.Drawing.Size(287, 20);
			this.TxtRemoteUsername.TabIndex = 10;
			// 
			// TxtRemoteHost
			// 
			this.TxtRemoteHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TxtRemoteHost.Location = new System.Drawing.Point(87, 13);
			this.TxtRemoteHost.Name = "TxtRemoteHost";
			this.TxtRemoteHost.Size = new System.Drawing.Size(287, 20);
			this.TxtRemoteHost.TabIndex = 8;
			// 
			// JsonApiCredentialsSettingsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.GBSuperStartRemoteServer);
			this.Name = "JsonApiCredentialsSettingsControl";
			this.Size = new System.Drawing.Size(641, 150);
			this.GBSuperStartRemoteServer.ResumeLayout(false);
			this.GBSuperStartRemoteServer.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumRemotePort)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		internal System.Windows.Forms.GroupBox GBSuperStartRemoteServer;
		internal System.Windows.Forms.MaskedTextBox MTxtRemotePassword;
		internal System.Windows.Forms.MaskedTextBox MTxtRemoteSalt;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.NumericUpDown NumRemotePort;
		internal System.Windows.Forms.TextBox TxtRemoteUsername;
		internal System.Windows.Forms.TextBox TxtRemoteHost;
    }
}