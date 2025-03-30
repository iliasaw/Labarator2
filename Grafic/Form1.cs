using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Grafic
{
    public partial class Form1 : Form
    {
        // Добавим константы для категоризации файлов по размеру (в байтах)
        private const long SMALL_FILE_LIMIT = 1024 * 100; // 100 KB
        private const long MEDIUM_FILE_LIMIT = 1024 * 1024; // 1 MB
        // Файлы больше MEDIUM_FILE_LIMIT считаются большими

        public Form1()
        {
            InitializeComponent();
            // Инициализируем область графика, чтобы график отображался
            chartFiles.ChartAreas.Clear();
            chartFiles.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea("ChartArea1"));
            // Делаем сетку менее заметной
            chartFiles.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartFiles.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chartFiles.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chartFiles.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chartFiles.BringToFront();
        }

        // Категории расширений файлов
        private readonly string[] graphicsExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };
        private readonly string[] officeExtensions = new[] { ".docx", ".xlsx", ".pdf", ".txt" };
        private readonly string[] archiveExtensions = new[] { ".zip", ".rar", ".7z" };
        private readonly string[] exeExtensions = new[] { ".exe", ".dll" };

        // Обработчик открытия директории
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = fbd.SelectedPath;
                    treeViewDirectories.Nodes.Clear();
                    LoadDirectory(selectedPath, treeViewDirectories.Nodes);
                    treeViewDirectories.ExpandAll();
                    UpdateListView(selectedPath);
                }
            }
        }

        // Рекурсивное заполнение дерева директорий и файлов
        private void LoadDirectory(string path, TreeNodeCollection nodes)
        {
            try
            {
                TreeNode node = nodes.Add(Path.GetFileName(path));
                node.Tag = path;
                // Добавление поддиректорий
                foreach (string dir in Directory.GetDirectories(path))
                {
                    LoadDirectory(dir, node.Nodes);
                }
                // Добавление файлов
                foreach (string file in Directory.GetFiles(path))
                {
                    TreeNode fileNode = node.Nodes.Add(Path.GetFileName(file));
                    fileNode.Tag = file;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка доступа: " + ex.Message);
            }
        }

        // При выборе узла в дереве обновляем список файлов
        private void treeViewDirectories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = e.Node?.Tag as string;
            if (Directory.Exists(path))
            {
                UpdateListView(path);
            }
            else if (File.Exists(path))
            {
                UpdateListViewForFile(path);
            }
        }

        // Заполнение списка для директории
        private void UpdateListView(string directoryPath)
        {
            listViewFiles.Items.Clear();
            try
            {
                DirectoryInfo di = new DirectoryInfo(directoryPath);
                FileInfo[] files = di.GetFiles();
                foreach (FileInfo fi in files)
                {
                    ListViewItem lvi = CreateListViewItem(fi);
                    listViewFiles.Items.Add(lvi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения директории: " + ex.Message);
            }
            UpdateChart();
            UpdateStatus();
        }

        // Заполнение списка для отдельного файла
        private void UpdateListViewForFile(string filePath)
        {
            listViewFiles.Items.Clear();
            try
            {
                FileInfo fi = new FileInfo(filePath);
                ListViewItem lvi = CreateListViewItem(fi);
                listViewFiles.Items.Add(lvi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения файла: " + ex.Message);
            }
            UpdateChart();
            UpdateStatus();
        }

        // Создание элемента списка для файла
        private ListViewItem CreateListViewItem(FileInfo fi)
        {
            ListViewItem lvi = new ListViewItem(fi.Name);
            lvi.SubItems.Add(fi.Length.ToString());
            lvi.SubItems.Add(fi.Extension);
            lvi.SubItems.Add(fi.CreationTime.ToString());
            lvi.Checked = true;
            string ext = fi.Extension.ToLower();
            if (graphicsExtensions.Contains(ext))
            {
                lvi.BackColor = Color.LightBlue;
            }
            else if (officeExtensions.Contains(ext))
            {
                lvi.BackColor = Color.LightGreen;
            }
            else if (archiveExtensions.Contains(ext))
            {
                lvi.BackColor = Color.Orange;
            }
            else if (exeExtensions.Contains(ext))
            {
                lvi.BackColor = Color.LightCoral;
            }
            return lvi;
        }

        // Обновление диаграммы согласно выбранным файлам (вариант 1: по расширению)
        private void UpdateChart()
        {
            chartFiles.Series.Clear();
            
            // Устанавливаем заголовок графика
            chartFiles.Titles.Clear();
            chartFiles.Titles.Add("Средний размер файлов по группам");
            
            Series series = chartFiles.Series.Add("AverageSize");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.Violet;
            series.IsValueShownAsLabel = true;
            
            if (chartFiles.ChartAreas.Count > 0)
            {
                chartFiles.ChartAreas[0].AxisX.Title = "Группы файлов";
                chartFiles.ChartAreas[0].AxisY.Title = "Средний размер (байт)";
                chartFiles.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 9);
                chartFiles.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 9);
            }
            
            // Словари для подсчета сумм и количества файлов
            Dictionary<string, long> totalSizes = new Dictionary<string, long>
            {
                { "Малые", 0 },
                { "Средние", 0 },
                { "Большие", 0 }
            };
            
            Dictionary<string, int> fileCount = new Dictionary<string, int>
            {
                { "Малые", 0 },
                { "Средние", 0 },
                { "Большие", 0 }
            };
            
            // Подсчет сумм и количества файлов по группам
            foreach (ListViewItem item in listViewFiles.Items)
            {
                if (item.Checked)
                {
                    long size = long.Parse(item.SubItems[1].Text);
                    string category;
                    
                    if (size <= SMALL_FILE_LIMIT)
                        category = "Малые";
                    else if (size <= MEDIUM_FILE_LIMIT)
                        category = "Средние";
                    else
                        category = "Большие";
                        
                    totalSizes[category] += size;
                    fileCount[category]++;
                }
            }
            
            // Вычисление и отображение средних значений
            foreach (var category in new[] { "Малые", "Средние", "Большие" })
            {
                double average = fileCount[category] > 0 
                    ? (double)totalSizes[category] / fileCount[category] 
                    : 0;
                    
                int pointIndex = series.Points.AddXY(category, average);
                series.Points[pointIndex].ToolTip = 
                    $"Группа: {category}\nСредний размер: {average:N0} байт\nКоличество файлов: {fileCount[category]}";
            }
        }

        // Обновление статусной строки (общий размер файлов и число выбранных файлов)
        private void UpdateStatus()
        {
            long totalBytes = 0;
            int totalFiles = listViewFiles.Items.Count;
            int selectedFiles = 0;
            foreach (ListViewItem item in listViewFiles.Items)
            {
                totalBytes += long.Parse(item.SubItems[1].Text);
                if (item.Checked)
                    selectedFiles++;
            }
            toolStripStatusLabelTotalBytes.Text = "Total Bytes: " + totalBytes;
            toolStripStatusLabelSelected.Text = "Selected Files: " + selectedFiles + "/" + totalFiles;
        }

        // При изменении состояния чекбокса в списке обновляем диаграмму и статус
        private void listViewFiles_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdateChart();
            UpdateStatus();
        }

        // Сохранение содержимого списка файлов в текстовый файл
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text files (*.txt)|*.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            // Сохраняем структуру дерева
                            SaveTreeStructure(treeViewDirectories.Nodes, sw, "");
                        }
                        MessageBox.Show("Структура дерева успешно сохранена.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка сохранения файла: " + ex.Message);
                    }
                }
            }
        }

        private void SaveTreeStructure(TreeNodeCollection nodes, StreamWriter writer, string indent)
        {
            foreach (TreeNode node in nodes)
            {
                writer.WriteLine($"{indent}{node.Text}");
                if (node.Nodes.Count > 0)
                {
                    SaveTreeStructure(node.Nodes, writer, indent + "    ");
                }
            }
        }

        // Выход из приложения
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Настройка шрифта для списка файлов
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    listViewFiles.Font = fd.Font;
                }
            }
        }

        // Настройка цвета для списка файлов
        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    listViewFiles.ForeColor = cd.Color;
                }
            }
        }

        // Добавить метод для обработки клика по Help
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var helpForm = new HelpForm())
            {
                helpForm.ShowDialog(this);
            }
        }
    }
}
