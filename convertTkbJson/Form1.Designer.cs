namespace convertTkbJson
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
            this.openFileButton = new System.Windows.Forms.Button();
            this.fbdSource = new System.Windows.Forms.FolderBrowserDialog();
            this.openMissionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(12, 12);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(150, 124);
            this.openFileButton.TabIndex = 0;
            this.openFileButton.Text = "Choose File";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // openMissionButton
            // 
            this.openMissionButton.Location = new System.Drawing.Point(168, 12);
            this.openMissionButton.Name = "openMissionButton";
            this.openMissionButton.Size = new System.Drawing.Size(150, 124);
            this.openMissionButton.TabIndex = 1;
            this.openMissionButton.Text = "Choose Mission";
            this.openMissionButton.UseVisualStyleBackColor = true;
            this.openMissionButton.Click += new System.EventHandler(this.openMissionButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 157);
            this.Controls.Add(this.openMissionButton);
            this.Controls.Add(this.openFileButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.FolderBrowserDialog fbdSource;
        private System.Windows.Forms.Button openMissionButton;
    }
}

