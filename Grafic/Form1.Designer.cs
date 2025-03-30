namespace Grafic
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Новые определения контролов
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainerVertical;
        private System.Windows.Forms.SplitContainer splitContainerHorizontal;
        private System.Windows.Forms.TreeView treeViewDirectories;
        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFiles;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTotalBytes;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSelected;

        /// <summary>
        ///  Метод, необходимый для поддержки конструктора - не изменяйте
        ///  содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            // Инициализация контролов
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerVertical = new System.Windows.Forms.SplitContainer();
            this.splitContainerHorizontal = new System.Windows.Forms.SplitContainer();
            this.treeViewDirectories = new System.Windows.Forms.TreeView();
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.chartFiles = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelTotalBytes = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSelected = new System.Windows.Forms.ToolStripStatusLabel();
            
            // ----- MenuStrip -----
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.fileToolStripMenuItem,
                this.settingsToolStripMenuItem,
                this.helpToolStripMenuItem});
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            
            // File menu
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.openToolStripMenuItem,
                this.saveToolStripMenuItem,
                this.toolStripSeparator1,
                this.exitToolStripMenuItem});
            
            // Open
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            
            // Save
            this.saveToolStripMenuItem.Text = "Save...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            
            // Exit
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            
            // Settings menu
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.fontToolStripMenuItem,
                this.colorToolStripMenuItem});
            
            // Font
            this.fontToolStripMenuItem.Text = "Font...";
            this.fontToolStripMenuItem.Click += new System.EventHandler(this.fontToolStripMenuItem_Click);
            
            // Color
            this.colorToolStripMenuItem.Text = "Color...";
            this.colorToolStripMenuItem.Click += new System.EventHandler(this.colorToolStripMenuItem_Click);
            
            // Help menu
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            
            // ----- splitContainerVertical (Главная область разделена на верхнюю и нижнюю части) -----
            this.splitContainerVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerVertical.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // В верхней части разместим splitContainerHorizontal
            this.splitContainerVertical.Panel1.Controls.Add(this.splitContainerHorizontal);
            // В нижней части разместим Chart
            this.splitContainerVertical.Panel2.Controls.Add(this.chartFiles);
            this.splitContainerVertical.SplitterDistance = 300;
            
            // ----- splitContainerHorizontal (левая - TreeView, правая - ListView) -----
            this.splitContainerHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerHorizontal.Panel1.Controls.Add(this.treeViewDirectories);
            this.splitContainerHorizontal.Panel2.Controls.Add(this.listViewFiles);
            
            // ----- TreeView -----
            this.treeViewDirectories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDirectories.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDirectories_AfterSelect);
            
            // ----- ListView -----
            this.listViewFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            this.listViewFiles.CheckBoxes = true;
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.GridLines = true;
            this.listViewFiles.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewFiles_ItemChecked);
            // Добавляем колонки
            this.listViewFiles.Columns.Add("Name", 150);
            this.listViewFiles.Columns.Add("Size (bytes)", 100);
            this.listViewFiles.Columns.Add("Extension", 70);
            this.listViewFiles.Columns.Add("Creation Time", 130);
            
            // ----- ChartFiles -----
            this.chartFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            // Инициализация области графика и серий будет выполнена в коде
            
            // ----- StatusStrip -----
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.toolStripStatusLabelTotalBytes,
                this.toolStripStatusLabelSelected});
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripStatusLabelTotalBytes.Text = "Total Bytes: 0";
            this.toolStripStatusLabelSelected.Text = "Selected Files: 0";
            
            // ----- Добавление контролов на форму -----
            this.Controls.Add(this.splitContainerVertical);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Text = "Grafic";
            this.Icon = new System.Drawing.Icon("app.ico");
            
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
        }

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
