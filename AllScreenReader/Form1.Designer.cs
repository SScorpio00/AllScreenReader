namespace AllScreenReader
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
            components = new System.ComponentModel.Container();
            pContainer = new Panel();
            systemMenu = new ContextMenuStrip(components);
            ts_Close = new ToolStripMenuItem();
            sitesToolStripMenuItem = new ToolStripMenuItem();
            tsExit = new ToolStripMenuItem();
            btnMenu = new Button();
            btnSinglePage = new Button();
            systemMenu.SuspendLayout();
            SuspendLayout();
            // 
            // pContainer
            // 
            pContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pContainer.AutoSize = true;
            pContainer.Location = new Point(0, 0);
            pContainer.Name = "pContainer";
            pContainer.Size = new Size(803, 450);
            pContainer.TabIndex = 0;
            // 
            // systemMenu
            // 
            systemMenu.Items.AddRange(new ToolStripItem[] { ts_Close, sitesToolStripMenuItem, tsExit });
            systemMenu.Name = "systemMenu";
            systemMenu.Size = new Size(181, 92);
            // 
            // ts_Close
            // 
            ts_Close.Name = "ts_Close";
            ts_Close.Size = new Size(180, 22);
            ts_Close.Text = "Close";
            ts_Close.Click += tsClose_Click;
            // 
            // sitesToolStripMenuItem
            // 
            sitesToolStripMenuItem.Name = "sitesToolStripMenuItem";
            sitesToolStripMenuItem.Size = new Size(180, 22);
            sitesToolStripMenuItem.Text = "Sites";
            // 
            // tsExit
            // 
            tsExit.Name = "tsExit";
            tsExit.Size = new Size(180, 22);
            tsExit.Text = "Exit";
            tsExit.Click += tsExit_Click;
            // 
            // btnMenu
            // 
            btnMenu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMenu.BackColor = Color.Transparent;
            btnMenu.Location = new Point(774, 0);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(26, 25);
            btnMenu.TabIndex = 1;
            btnMenu.UseVisualStyleBackColor = false;
            btnMenu.Click += btnMenu_Click;
            // 
            // btnSinglePage
            // 
            btnSinglePage.Anchor = AnchorStyles.Top;
            btnSinglePage.BackColor = Color.Transparent;
            btnSinglePage.Location = new Point(372, 0);
            btnSinglePage.Margin = new Padding(0);
            btnSinglePage.Name = "btnSinglePage";
            btnSinglePage.Size = new Size(26, 25);
            btnSinglePage.TabIndex = 4;
            btnSinglePage.UseVisualStyleBackColor = false;
            btnSinglePage.Click += btnSinglePage_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(803, 450);
            ControlBox = false;
            Controls.Add(btnSinglePage);
            Controls.Add(btnMenu);
            Controls.Add(pContainer);
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "Form1";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "All Screen Reader";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            systemMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pContainer;
        private ContextMenuStrip systemMenu;
        private ToolStripMenuItem ts_Close;
        private Button btnMenu;
        private ToolStripMenuItem tsExit;
        private ToolStripMenuItem sitesToolStripMenuItem;
        private Button btnSinglePage;
    }
}
