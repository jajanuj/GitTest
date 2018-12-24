namespace ServerApp
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.ledServerToClient = new AOISystem.Utilities.Forms.LedRound();
            this.slbServerToClient = new AOISystem.Utilities.Forms.SlideButton();
            this.txtBuffer = new System.Windows.Forms.TextBox();
            this.lblBuffer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.slbClientToServer = new AOISystem.Utilities.Forms.SlideButton();
            this.ledClientToServer = new AOISystem.Utilities.Forms.LedRound();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(13, 13);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(88, 43);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(44, 12);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Message";
            // 
            // txtMessage
            // 
            this.txtMessage.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtMessage.Location = new System.Drawing.Point(88, 59);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(100, 22);
            this.txtMessage.TabIndex = 2;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(194, 57);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // ledServerToClient
            // 
            this.ledServerToClient.Location = new System.Drawing.Point(123, 133);
            this.ledServerToClient.Name = "ledServerToClient";
            this.ledServerToClient.On = false;
            this.ledServerToClient.Padding = new System.Windows.Forms.Padding(1);
            this.ledServerToClient.Size = new System.Drawing.Size(27, 27);
            this.ledServerToClient.TabIndex = 4;
            this.ledServerToClient.Text = "ServerToClient";
            this.ledServerToClient.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ledServerToClient.TextFont = new System.Drawing.Font("Lucida Sans Unicode", 14F, System.Drawing.FontStyle.Bold);
            // 
            // slbServerToClient
            // 
            this.slbServerToClient.BackColor = System.Drawing.Color.Transparent;
            this.slbServerToClient.Checked = false;
            this.slbServerToClient.CheckStyleX = AOISystem.Utilities.Forms.CheckStyle.iOS;
            this.slbServerToClient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.slbServerToClient.Location = new System.Drawing.Point(194, 133);
            this.slbServerToClient.Name = "slbServerToClient";
            this.slbServerToClient.Size = new System.Drawing.Size(87, 27);
            this.slbServerToClient.TabIndex = 5;
            this.slbServerToClient.Click += new System.EventHandler(this.slbServerToClient_Click);
            // 
            // txtBuffer
            // 
            this.txtBuffer.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtBuffer.Location = new System.Drawing.Point(88, 99);
            this.txtBuffer.Name = "txtBuffer";
            this.txtBuffer.Size = new System.Drawing.Size(100, 22);
            this.txtBuffer.TabIndex = 13;
            // 
            // lblBuffer
            // 
            this.lblBuffer.AutoSize = true;
            this.lblBuffer.Location = new System.Drawing.Point(86, 84);
            this.lblBuffer.Name = "lblBuffer";
            this.lblBuffer.Size = new System.Drawing.Size(36, 12);
            this.lblBuffer.TabIndex = 12;
            this.lblBuffer.Text = "Buffer";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "Message";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "Buffer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "ServerToClient";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "ClientToServer";
            // 
            // slbClientToServer
            // 
            this.slbClientToServer.BackColor = System.Drawing.Color.Transparent;
            this.slbClientToServer.Checked = false;
            this.slbClientToServer.CheckStyleX = AOISystem.Utilities.Forms.CheckStyle.iOS;
            this.slbClientToServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.slbClientToServer.Location = new System.Drawing.Point(194, 166);
            this.slbClientToServer.Name = "slbClientToServer";
            this.slbClientToServer.Size = new System.Drawing.Size(87, 27);
            this.slbClientToServer.TabIndex = 18;
            this.slbClientToServer.Click += new System.EventHandler(this.slbClientToServer_Click);
            // 
            // ledClientToServer
            // 
            this.ledClientToServer.Location = new System.Drawing.Point(123, 166);
            this.ledClientToServer.Name = "ledClientToServer";
            this.ledClientToServer.On = false;
            this.ledClientToServer.Padding = new System.Windows.Forms.Padding(1);
            this.ledClientToServer.Size = new System.Drawing.Size(27, 27);
            this.ledClientToServer.TabIndex = 17;
            this.ledClientToServer.Text = "ClientToServer";
            this.ledClientToServer.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ledClientToServer.TextFont = new System.Drawing.Font("Lucida Sans Unicode", 14F, System.Drawing.FontStyle.Bold);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(145, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 204);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.slbClientToServer);
            this.Controls.Add(this.ledClientToServer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBuffer);
            this.Controls.Add(this.lblBuffer);
            this.Controls.Add(this.slbServerToClient);
            this.Controls.Add(this.ledServerToClient);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnStart);
            this.Name = "MainForm";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnUpdate;
        private AOISystem.Utilities.Forms.LedRound ledServerToClient;
        private AOISystem.Utilities.Forms.SlideButton slbServerToClient;
        private System.Windows.Forms.TextBox txtBuffer;
        private System.Windows.Forms.Label lblBuffer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private AOISystem.Utilities.Forms.LedRound ledClientToServer;
        private AOISystem.Utilities.Forms.SlideButton slbClientToServer;
        private System.Windows.Forms.Button button1;

    }
}

