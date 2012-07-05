namespace StoneEditor
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opendialog = new System.Windows.Forms.OpenFileDialog();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.layerListbox = new System.Windows.Forms.ListBox();
            this.contentPathTextbox = new System.Windows.Forms.TextBox();
            this.showEmptyCheckbox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.texturePicker = new StoneEditor.TileDisplay();
            this.mapDisplay = new StoneEditor.TileDisplay();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1274, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.newToolStripMenuItem.Text = "New Map";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.openToolStripMenuItem.Text = "Open Map";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.saveToolStripMenuItem.Text = "Save Map";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // opendialog
            // 
            this.opendialog.FileName = "openFileDialog1";
            this.opendialog.InitialDirectory = "G:\\Programming\\Stoneforge\\Stoneforge\\Stoneforge\\StoneforgeContent";
            // 
            // layerListbox
            // 
            this.layerListbox.FormattingEnabled = true;
            this.layerListbox.Location = new System.Drawing.Point(968, 245);
            this.layerListbox.Name = "layerListbox";
            this.layerListbox.Size = new System.Drawing.Size(294, 108);
            this.layerListbox.TabIndex = 3;
            this.layerListbox.SelectedIndexChanged += new System.EventHandler(this.layerListbox_SelectedIndexChanged);
            // 
            // contentPathTextbox
            // 
            this.contentPathTextbox.Location = new System.Drawing.Point(969, 32);
            this.contentPathTextbox.Name = "contentPathTextbox";
            this.contentPathTextbox.ReadOnly = true;
            this.contentPathTextbox.Size = new System.Drawing.Size(292, 20);
            this.contentPathTextbox.TabIndex = 4;
            this.contentPathTextbox.Text = "G:\\Programming\\Stoneforge\\Stoneforge\\Stoneforge\\StoneforgeContent";
            // 
            // showEmptyCheckbox
            // 
            this.showEmptyCheckbox.AutoSize = true;
            this.showEmptyCheckbox.Location = new System.Drawing.Point(978, 59);
            this.showEmptyCheckbox.Name = "showEmptyCheckbox";
            this.showEmptyCheckbox.Size = new System.Drawing.Size(110, 17);
            this.showEmptyCheckbox.TabIndex = 5;
            this.showEmptyCheckbox.Text = "Show Empty Cells";
            this.showEmptyCheckbox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1208, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Break";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // texturePicker
            // 
            this.texturePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.texturePicker.Location = new System.Drawing.Point(969, 359);
            this.texturePicker.Name = "texturePicker";
            this.texturePicker.Size = new System.Drawing.Size(293, 225);
            this.texturePicker.TabIndex = 1;
            this.texturePicker.Text = "tileDisplay2";
            // 
            // mapDisplay
            // 
            this.mapDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.mapDisplay.Location = new System.Drawing.Point(13, 27);
            this.mapDisplay.Name = "mapDisplay";
            this.mapDisplay.Size = new System.Drawing.Size(949, 557);
            this.mapDisplay.TabIndex = 0;
            this.mapDisplay.Text = "tileDisplay1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 596);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.showEmptyCheckbox);
            this.Controls.Add(this.contentPathTextbox);
            this.Controls.Add(this.layerListbox);
            this.Controls.Add(this.texturePicker);
            this.Controls.Add(this.mapDisplay);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "StoneEditor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TileDisplay mapDisplay;
        private TileDisplay texturePicker;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog opendialog;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.ListBox layerListbox;
        private System.Windows.Forms.TextBox contentPathTextbox;
        private System.Windows.Forms.CheckBox showEmptyCheckbox;
        private System.Windows.Forms.Button button1;
    }
}

