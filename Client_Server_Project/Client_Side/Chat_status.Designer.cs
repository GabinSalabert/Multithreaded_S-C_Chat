namespace Client_Side
{
    partial class Chat_status
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.msg_area = new System.Windows.Forms.RichTextBox();
            this.send_message = new System.Windows.Forms.TextBox();
            this.send = new System.Windows.Forms.Button();
            this.channels_to_select = new System.Windows.Forms.ComboBox();
            this.Connect_new_channel_dm = new System.Windows.Forms.Button();
            this.channels_list = new System.Windows.Forms.ListBox();
            this.info = new System.Windows.Forms.Label();
            this.client_to_connect = new System.Windows.Forms.ComboBox();
            this.check_dm = new System.Windows.Forms.RadioButton();
            this.check_channel = new System.Windows.Forms.RadioButton();
            this.dms_list = new System.Windows.Forms.ListBox();
            this.acces_channel = new System.Windows.Forms.CheckBox();
            this.acces_dm = new System.Windows.Forms.CheckBox();
            this.newCh = new System.Windows.Forms.Button();
            this.chan_notif = new System.Windows.Forms.Label();
            this.dm_notif = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // msg_area
            // 
            this.msg_area.Location = new System.Drawing.Point(9, 261);
            this.msg_area.Margin = new System.Windows.Forms.Padding(2);
            this.msg_area.Name = "msg_area";
            this.msg_area.Size = new System.Drawing.Size(374, 190);
            this.msg_area.TabIndex = 0;
            this.msg_area.Text = "";
            // 
            // send_message
            // 
            this.send_message.Location = new System.Drawing.Point(9, 457);
            this.send_message.Margin = new System.Windows.Forms.Padding(2);
            this.send_message.Name = "send_message";
            this.send_message.Size = new System.Drawing.Size(313, 20);
            this.send_message.TabIndex = 1;
            // 
            // send
            // 
            this.send.Location = new System.Drawing.Point(326, 457);
            this.send.Margin = new System.Windows.Forms.Padding(2);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(56, 19);
            this.send.TabIndex = 2;
            this.send.Text = "send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // channels_to_select
            // 
            this.channels_to_select.FormattingEnabled = true;
            this.channels_to_select.Location = new System.Drawing.Point(154, 34);
            this.channels_to_select.Margin = new System.Windows.Forms.Padding(2);
            this.channels_to_select.Name = "channels_to_select";
            this.channels_to_select.Size = new System.Drawing.Size(102, 21);
            this.channels_to_select.TabIndex = 4;
            this.channels_to_select.Text = "Choose channel";
            // 
            // Connect_new_channel_dm
            // 
            this.Connect_new_channel_dm.Location = new System.Drawing.Point(288, 34);
            this.Connect_new_channel_dm.Margin = new System.Windows.Forms.Padding(2);
            this.Connect_new_channel_dm.Name = "Connect_new_channel_dm";
            this.Connect_new_channel_dm.Size = new System.Drawing.Size(80, 21);
            this.Connect_new_channel_dm.TabIndex = 5;
            this.Connect_new_channel_dm.Text = "Connect";
            this.Connect_new_channel_dm.UseVisualStyleBackColor = true;
            this.Connect_new_channel_dm.Click += new System.EventHandler(this.Connect_new_channel_dm_Click);
            // 
            // channels_list
            // 
            this.channels_list.FormattingEnabled = true;
            this.channels_list.Location = new System.Drawing.Point(243, 93);
            this.channels_list.Margin = new System.Windows.Forms.Padding(2);
            this.channels_list.Name = "channels_list";
            this.channels_list.Size = new System.Drawing.Size(126, 147);
            this.channels_list.TabIndex = 6;
            this.channels_list.SelectedIndexChanged += new System.EventHandler(this.channels_list_SelectedIndexChanged);
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.Location = new System.Drawing.Point(7, 245);
            this.info.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(29, 13);
            this.info.TabIndex = 7;
            this.info.Text = "infos";
            // 
            // client_to_connect
            // 
            this.client_to_connect.FormattingEnabled = true;
            this.client_to_connect.Location = new System.Drawing.Point(32, 34);
            this.client_to_connect.Margin = new System.Windows.Forms.Padding(2);
            this.client_to_connect.Name = "client_to_connect";
            this.client_to_connect.Size = new System.Drawing.Size(92, 21);
            this.client_to_connect.TabIndex = 8;
            this.client_to_connect.Text = "Choose client ";
            this.client_to_connect.UseWaitCursor = true;
            // 
            // check_dm
            // 
            this.check_dm.AutoSize = true;
            this.check_dm.Location = new System.Drawing.Point(54, 13);
            this.check_dm.Margin = new System.Windows.Forms.Padding(2);
            this.check_dm.Name = "check_dm";
            this.check_dm.Size = new System.Drawing.Size(53, 17);
            this.check_dm.TabIndex = 9;
            this.check_dm.TabStop = true;
            this.check_dm.Text = "Direct";
            this.check_dm.UseVisualStyleBackColor = true;
            this.check_dm.CheckedChanged += new System.EventHandler(this.check_dm_CheckedChanged);
            // 
            // check_channel
            // 
            this.check_channel.AutoSize = true;
            this.check_channel.Location = new System.Drawing.Point(171, 13);
            this.check_channel.Margin = new System.Windows.Forms.Padding(2);
            this.check_channel.Name = "check_channel";
            this.check_channel.Size = new System.Drawing.Size(64, 17);
            this.check_channel.TabIndex = 10;
            this.check_channel.TabStop = true;
            this.check_channel.Text = "Channel";
            this.check_channel.UseVisualStyleBackColor = true;
            this.check_channel.CheckedChanged += new System.EventHandler(this.check_channel_CheckedChanged);
            // 
            // dms_list
            // 
            this.dms_list.FormattingEnabled = true;
            this.dms_list.Location = new System.Drawing.Point(26, 93);
            this.dms_list.Margin = new System.Windows.Forms.Padding(2);
            this.dms_list.Name = "dms_list";
            this.dms_list.Size = new System.Drawing.Size(126, 147);
            this.dms_list.TabIndex = 11;
            this.dms_list.SelectedIndexChanged += new System.EventHandler(this.dms_list_SelectedIndexChanged);
            // 
            // acces_channel
            // 
            this.acces_channel.AutoSize = true;
            this.acces_channel.Location = new System.Drawing.Point(243, 72);
            this.acces_channel.Margin = new System.Windows.Forms.Padding(2);
            this.acces_channel.Name = "acces_channel";
            this.acces_channel.Size = new System.Drawing.Size(70, 17);
            this.acces_channel.TabIndex = 14;
            this.acces_channel.Text = "Channels";
            this.acces_channel.UseVisualStyleBackColor = true;
            this.acces_channel.CheckedChanged += new System.EventHandler(this.acces_channel_CheckedChanged);
            // 
            // acces_dm
            // 
            this.acces_dm.AutoSize = true;
            this.acces_dm.Location = new System.Drawing.Point(26, 72);
            this.acces_dm.Margin = new System.Windows.Forms.Padding(2);
            this.acces_dm.Name = "acces_dm";
            this.acces_dm.Size = new System.Drawing.Size(48, 17);
            this.acces_dm.TabIndex = 15;
            this.acces_dm.Text = "DMs";
            this.acces_dm.UseVisualStyleBackColor = true;
            this.acces_dm.CheckedChanged += new System.EventHandler(this.acces_private_CheckedChanged);
            // 
            // newCh
            // 
            this.newCh.Location = new System.Drawing.Point(278, 13);
            this.newCh.Margin = new System.Windows.Forms.Padding(2);
            this.newCh.Name = "newCh";
            this.newCh.Size = new System.Drawing.Size(104, 19);
            this.newCh.TabIndex = 16;
            this.newCh.Text = "New Channel";
            this.newCh.UseVisualStyleBackColor = true;
            this.newCh.Click += new System.EventHandler(this.newCh_Click);
            // 
            // chan_notif
            // 
            this.chan_notif.AutoSize = true;
            this.chan_notif.ForeColor = System.Drawing.Color.Red;
            this.chan_notif.Location = new System.Drawing.Point(318, 73);
            this.chan_notif.Name = "chan_notif";
            this.chan_notif.Size = new System.Drawing.Size(35, 13);
            this.chan_notif.TabIndex = 17;
            this.chan_notif.Text = "New !";
            this.chan_notif.Visible = false;
            // 
            // dm_notif
            // 
            this.dm_notif.AutoSize = true;
            this.dm_notif.ForeColor = System.Drawing.Color.Red;
            this.dm_notif.Location = new System.Drawing.Point(79, 73);
            this.dm_notif.Name = "dm_notif";
            this.dm_notif.Size = new System.Drawing.Size(35, 13);
            this.dm_notif.TabIndex = 18;
            this.dm_notif.Text = "New !";
            this.dm_notif.Visible = false;
            // 
            // Chat_status
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(390, 484);
            this.Controls.Add(this.dm_notif);
            this.Controls.Add(this.chan_notif);
            this.Controls.Add(this.newCh);
            this.Controls.Add(this.acces_dm);
            this.Controls.Add(this.acces_channel);
            this.Controls.Add(this.dms_list);
            this.Controls.Add(this.check_channel);
            this.Controls.Add(this.check_dm);
            this.Controls.Add(this.client_to_connect);
            this.Controls.Add(this.info);
            this.Controls.Add(this.channels_list);
            this.Controls.Add(this.Connect_new_channel_dm);
            this.Controls.Add(this.channels_to_select);
            this.Controls.Add(this.send);
            this.Controls.Add(this.send_message);
            this.Controls.Add(this.msg_area);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Chat_status";
            this.Text = "Client UI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox msg_area;
        private System.Windows.Forms.TextBox send_message;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.ComboBox channels_to_select;
        private System.Windows.Forms.Button Connect_new_channel_dm;
        private System.Windows.Forms.ListBox channels_list;
        private System.Windows.Forms.Label info;
        private System.Windows.Forms.ComboBox client_to_connect;
        private System.Windows.Forms.RadioButton check_dm;
        private System.Windows.Forms.RadioButton check_channel;
        private System.Windows.Forms.ListBox dms_list;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox acces_channel;
        private System.Windows.Forms.CheckBox acces_dm;
        private System.Windows.Forms.Button newCh;
        private System.Windows.Forms.Label chan_notif;
        private System.Windows.Forms.Label dm_notif;
    }
}