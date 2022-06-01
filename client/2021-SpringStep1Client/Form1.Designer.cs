namespace _2021_SpringStep1Client
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.button_Disconnect = new System.Windows.Forms.Button();
            this.Button_connect = new System.Windows.Forms.Button();
            this.clientLogs = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPost = new System.Windows.Forms.TextBox();
            this.button_SendPost = new System.Windows.Forms.Button();
            this.button_AllPosts = new System.Windows.Forms.Button();
            this.textBox_usernameFriend = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_postID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button_deletePost = new System.Windows.Forms.Button();
            this.button_addFriend = new System.Windows.Forms.Button();
            this.friendLogs = new System.Windows.Forms.RichTextBox();
            this.button_friendsPost = new System.Windows.Forms.Button();
            this.button_myPosts = new System.Windows.Forms.Button();
            this.button_removeFriend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Username:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Port:";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(100, 25);
            this.textBoxIP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(207, 22);
            this.textBoxIP.TabIndex = 3;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(100, 73);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(207, 22);
            this.textBoxPort.TabIndex = 4;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(100, 122);
            this.textBoxUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(207, 22);
            this.textBoxUsername.TabIndex = 5;
            // 
            // button_Disconnect
            // 
            this.button_Disconnect.Enabled = false;
            this.button_Disconnect.Location = new System.Drawing.Point(359, 102);
            this.button_Disconnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Disconnect.Name = "button_Disconnect";
            this.button_Disconnect.Size = new System.Drawing.Size(101, 40);
            this.button_Disconnect.TabIndex = 7;
            this.button_Disconnect.Text = "Disconnect";
            this.button_Disconnect.UseVisualStyleBackColor = true;
            this.button_Disconnect.Click += new System.EventHandler(this.button_Disconnect_Click);
            // 
            // Button_connect
            // 
            this.Button_connect.Location = new System.Drawing.Point(359, 25);
            this.Button_connect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button_connect.Name = "Button_connect";
            this.Button_connect.Size = new System.Drawing.Size(101, 40);
            this.Button_connect.TabIndex = 8;
            this.Button_connect.Text = "Connect";
            this.Button_connect.UseVisualStyleBackColor = true;
            this.Button_connect.Click += new System.EventHandler(this.Button_connect_Click);
            // 
            // clientLogs
            // 
            this.clientLogs.Location = new System.Drawing.Point(501, 25);
            this.clientLogs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clientLogs.Name = "clientLogs";
            this.clientLogs.ReadOnly = true;
            this.clientLogs.Size = new System.Drawing.Size(348, 450);
            this.clientLogs.TabIndex = 9;
            this.clientLogs.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 441);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Post:";
            // 
            // textBoxPost
            // 
            this.textBoxPost.Enabled = false;
            this.textBoxPost.Location = new System.Drawing.Point(100, 441);
            this.textBoxPost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPost.Name = "textBoxPost";
            this.textBoxPost.Size = new System.Drawing.Size(207, 22);
            this.textBoxPost.TabIndex = 11;
            // 
            // button_SendPost
            // 
            this.button_SendPost.Enabled = false;
            this.button_SendPost.Location = new System.Drawing.Point(395, 439);
            this.button_SendPost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_SendPost.Name = "button_SendPost";
            this.button_SendPost.Size = new System.Drawing.Size(91, 27);
            this.button_SendPost.TabIndex = 12;
            this.button_SendPost.Text = "Send";
            this.button_SendPost.UseVisualStyleBackColor = true;
            this.button_SendPost.Click += new System.EventHandler(this.button_SendPost_Click);
            // 
            // button_AllPosts
            // 
            this.button_AllPosts.Enabled = false;
            this.button_AllPosts.Location = new System.Drawing.Point(518, 501);
            this.button_AllPosts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_AllPosts.Name = "button_AllPosts";
            this.button_AllPosts.Size = new System.Drawing.Size(100, 35);
            this.button_AllPosts.TabIndex = 13;
            this.button_AllPosts.Text = "All Posts";
            this.button_AllPosts.UseVisualStyleBackColor = true;
            this.button_AllPosts.Click += new System.EventHandler(this.button_AllPosts_Click);
            // 
            // textBox_usernameFriend
            // 
            this.textBox_usernameFriend.Enabled = false;
            this.textBox_usernameFriend.Location = new System.Drawing.Point(100, 390);
            this.textBox_usernameFriend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_usernameFriend.Name = "textBox_usernameFriend";
            this.textBox_usernameFriend.Size = new System.Drawing.Size(207, 22);
            this.textBox_usernameFriend.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 393);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Username:";
            // 
            // textBox_postID
            // 
            this.textBox_postID.Enabled = false;
            this.textBox_postID.Location = new System.Drawing.Point(100, 496);
            this.textBox_postID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_postID.Name = "textBox_postID";
            this.textBox_postID.Size = new System.Drawing.Size(207, 22);
            this.textBox_postID.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 499);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "Post ID:";
            // 
            // button_deletePost
            // 
            this.button_deletePost.Enabled = false;
            this.button_deletePost.Location = new System.Drawing.Point(395, 491);
            this.button_deletePost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_deletePost.Name = "button_deletePost";
            this.button_deletePost.Size = new System.Drawing.Size(91, 27);
            this.button_deletePost.TabIndex = 18;
            this.button_deletePost.Text = "Delete";
            this.button_deletePost.UseVisualStyleBackColor = true;
            this.button_deletePost.Click += new System.EventHandler(this.button_deletePost_Click);
            // 
            // button_addFriend
            // 
            this.button_addFriend.Enabled = false;
            this.button_addFriend.Location = new System.Drawing.Point(395, 376);
            this.button_addFriend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_addFriend.Name = "button_addFriend";
            this.button_addFriend.Size = new System.Drawing.Size(91, 36);
            this.button_addFriend.TabIndex = 19;
            this.button_addFriend.Text = "Add Friend";
            this.button_addFriend.UseVisualStyleBackColor = true;
            this.button_addFriend.Click += new System.EventHandler(this.button_addFriend_Click);
            // 
            // friendLogs
            // 
            this.friendLogs.BackColor = System.Drawing.SystemColors.Control;
            this.friendLogs.Location = new System.Drawing.Point(109, 168);
            this.friendLogs.Name = "friendLogs";
            this.friendLogs.ReadOnly = true;
            this.friendLogs.Size = new System.Drawing.Size(186, 149);
            this.friendLogs.TabIndex = 20;
            this.friendLogs.Text = "";
            // 
            // button_friendsPost
            // 
            this.button_friendsPost.Enabled = false;
            this.button_friendsPost.Location = new System.Drawing.Point(733, 501);
            this.button_friendsPost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_friendsPost.Name = "button_friendsPost";
            this.button_friendsPost.Size = new System.Drawing.Size(100, 35);
            this.button_friendsPost.TabIndex = 21;
            this.button_friendsPost.Text = "Friend\'s Post";
            this.button_friendsPost.UseVisualStyleBackColor = true;
            this.button_friendsPost.Click += new System.EventHandler(this.button_friendsPost_Click);
            // 
            // button_myPosts
            // 
            this.button_myPosts.Enabled = false;
            this.button_myPosts.Location = new System.Drawing.Point(634, 569);
            this.button_myPosts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_myPosts.Name = "button_myPosts";
            this.button_myPosts.Size = new System.Drawing.Size(100, 35);
            this.button_myPosts.TabIndex = 22;
            this.button_myPosts.Text = "My Posts";
            this.button_myPosts.UseVisualStyleBackColor = true;
            this.button_myPosts.Click += new System.EventHandler(this.button_myPosts_Click);
            // 
            // button_removeFriend
            // 
            this.button_removeFriend.Enabled = false;
            this.button_removeFriend.Location = new System.Drawing.Point(131, 332);
            this.button_removeFriend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_removeFriend.Name = "button_removeFriend";
            this.button_removeFriend.Size = new System.Drawing.Size(143, 29);
            this.button_removeFriend.TabIndex = 23;
            this.button_removeFriend.Text = "Remove Friend";
            this.button_removeFriend.UseVisualStyleBackColor = true;
            this.button_removeFriend.Click += new System.EventHandler(this.button_removeFriend_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 639);
            this.Controls.Add(this.button_removeFriend);
            this.Controls.Add(this.button_myPosts);
            this.Controls.Add(this.button_friendsPost);
            this.Controls.Add(this.friendLogs);
            this.Controls.Add(this.button_addFriend);
            this.Controls.Add(this.button_deletePost);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_postID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_usernameFriend);
            this.Controls.Add(this.button_AllPosts);
            this.Controls.Add(this.button_SendPost);
            this.Controls.Add(this.textBoxPost);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.clientLogs);
            this.Controls.Add(this.Button_connect);
            this.Controls.Add(this.button_Disconnect);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Button button_Disconnect;
        private System.Windows.Forms.Button Button_connect;
        private System.Windows.Forms.RichTextBox clientLogs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPost;
        private System.Windows.Forms.Button button_SendPost;
        private System.Windows.Forms.Button button_AllPosts;
        private System.Windows.Forms.TextBox textBox_usernameFriend;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_postID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_deletePost;
        private System.Windows.Forms.Button button_addFriend;
        private System.Windows.Forms.RichTextBox friendLogs;
        private System.Windows.Forms.Button button_friendsPost;
        private System.Windows.Forms.Button button_myPosts;
        private System.Windows.Forms.Button button_removeFriend;
    }
}

