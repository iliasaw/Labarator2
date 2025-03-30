using System.Windows.Forms;
using System.Drawing;

namespace Grafic
{
    public class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Справка";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;

            TextBox textBox = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Text = @"Программа для визуализации содержимого директорий

Основные возможности:
1. Просмотр структуры директорий в виде дерева
2. Отображение списка файлов с информацией:
   - Имя файла
   - Размер в байтах
   - Расширение
   - Время создания

3. Цветовая маркировка файлов:
   - Графические файлы (png, jpg, jpeg, bmp, gif) - голубой
   - Офисные файлы (docx, xlsx, pdf, txt) - зеленый
   - Архивы (zip, rar, 7z) - оранжевый
   - Исполняемые файлы (exe, dll) - красный

4. Визуализация данных:
   - График среднего размера файлов по группам
   - Группы: малые (до 100KB), средние (до 1MB), большие (более 1MB)

5. Дополнительные функции:
   - Настройка шрифта списка файлов
   - Настройка цвета текста
   - Сохранение структуры директории в текстовый файл

Статусная строка показывает:
- Общий размер всех файлов
- Количество выбранных/общее количество файлов"
            };

            this.Controls.Add(textBox);
        }
    }
} 