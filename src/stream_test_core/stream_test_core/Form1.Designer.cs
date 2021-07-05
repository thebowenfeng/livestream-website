namespace stream_test_core
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.cbxCamera = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chckRecord = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(180, 135);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(522, 278);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(180, 436);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(210, 76);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(473, 436);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(187, 76);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(855, 175);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 39);
            this.txtUsername.TabIndex = 2;
            // 
            // cbxCamera
            // 
            this.cbxCamera.FormattingEnabled = true;
            this.cbxCamera.Items.AddRange(new object[] {
            "Back Camera",
            "Front Camera"});
            this.cbxCamera.Location = new System.Drawing.Point(373, 42);
            this.cbxCamera.Name = "cbxCamera";
            this.cbxCamera.Size = new System.Drawing.Size(388, 40);
            this.cbxCamera.TabIndex = 3;
            this.cbxCamera.SelectedIndexChanged += new System.EventHandler(this.cbxCamera_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chckRecord
            // 
            this.chckRecord.AutoSize = true;
            this.chckRecord.Location = new System.Drawing.Point(855, 319);
            this.chckRecord.Name = "chckRecord";
            this.chckRecord.Size = new System.Drawing.Size(130, 36);
            this.chckRecord.TabIndex = 4;
            this.chckRecord.Text = "Record?";
            this.chckRecord.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 563);
            this.Controls.Add(this.chckRecord);
            this.Controls.Add(this.cbxCamera);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "stop";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ComboBox cbxCamera;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chckRecord;
    }
}

