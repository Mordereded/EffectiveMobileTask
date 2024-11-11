using System.Data;
using System.Globalization;
using Project_test_task.Uility;
using System.Text;
using System.Reflection;
using Project.Modules.Logger;
using Project.Modules.ConsoleManager;
using Project.Modules.DataBase;


namespace Project_test_task
{

    public partial class MainMenu : Form
    {
        private readonly DataBase dataBase;
        private readonly CompositeLogger logger;
        private readonly FileResultSaver resultSaver;
        public MainMenu()
        {
            InitializeComponent();
            logger = CompositeLogger.Instance;
            LoggerStartSettings();
            LoggerDefaultSenders();
            dataBase = DataBase.getInstence();
            RefreshTable();
            resultSaver = new FileResultSaver();
            FillSelector();
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.InsertSingleData(12.5, 1, DateTime.Now);
            RefreshTable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dataBase.MakeDataTable();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            DataBaseFillItemsCountExecuter dataBaseFillerExecuter = new DataBaseFillItemsCountExecuter
                (new DataBaseFillWithGenerator(dataBase));
            dataBaseFillerExecuter.Execute(10);
            RefreshTable();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            dataBase.DeleteTable();
            RefreshTable();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            DataBaseFillAllItemsExecuter dataBaseFillAllItemsExecutor = new DataBaseFillAllItemsExecuter
                (new DataBaseFillWithFile(dataBase, SelectFile()));
            dataBaseFillAllItemsExecutor.Execute();
            RefreshTable();

        }

        private string SelectFile()
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "q:\\";
                openFileDialog.Filter = "DB (*.db)|*.db|CSV files (*.csv)|*.csv|JSON files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        private void RefreshTable()
        {
            FillSelector();
            dataGridView1.DataSource = dataBase.MakeDataTable();
            dataGridView1.Columns["OrderId"].HeaderText = "Номер заказа";
            dataGridView1.Columns["Weight"].HeaderText = "Вес кг.";
            dataGridView1.Columns["CityDistrict"].HeaderText = "Район";
            dataGridView1.Columns["DeliveryDateTime"].HeaderText = "Дата и время доставки";

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            OrderInsert insert = new OrderInsert();
            insert.ShowDialog();
            RefreshTable();
        }

        /// <summary>
        /// ListBox с метками для активации и деактивации логеров 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            string? item = checkedListBox1.Items[e.Index].ToString();
            var loggerInstance = loggerFactory.CreateLogger(item);

            if (e.NewValue == CheckState.Checked)
            {
                logger.AddLogger(loggerInstance);
                logger.Log($"Добавление нового логера -> {item}");
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                Type loggerType = loggerInstance.GetType();
                logger.RemoveLogger(loggerType);
                logger.Log($"Удаление логера -> {item}");
            }
        }
        /// <summary>
        /// Начальная инициализация логеров, все работают по умолчанию
        /// </summary>
        private void LoggerStartSettings()
        {
            comboBox1.SelectedIndex = 0;
            checkedListBox1.SetItemChecked(0, true);
            checkedListBox1.SetItemChecked(1, true);
            checkedListBox1.SetItemChecked(2, true);
            /*logger.AddLogger(new FormLogger());
            logger.AddLogger(new ConsoleLogger());
            logger.AddLogger(new FileLogger());*/
        }
        private void LoggerDefaultSenders()
        {
            if (logger.Exists<FileLogger>())
            {
                var fileLogger = logger.GetLogger<FileLogger>();
                fileLogger.ChangeFilePath(Config.ConfigFileLogger());
            }
            if (logger.Exists<FormLogger>())
            {
                var formLogger = logger.GetLogger<FormLogger>();
                formLogger.SetRichTextBox(richTextBox1);
            }
        }
        private void LoggerChangeFileSender()
        {
            try
            {
                if (logger.Exists<FileLogger>())
                {
                    if (textBoxFilePath.Text == string.Empty) return;
                    var fileLogger = logger.GetLogger<FileLogger>();
                    fileLogger.ChangeFilePath(textBoxFilePath.Text);
                }
            }
            catch (Exception ex)
            {
                logger.Log($"Произошла ошибка при изменении названия файла -> {ex.Message}");
            }

        }
        private void LoggerChangeFormSender(RichTextBox richTextBox)
        {
            if (logger.Exists<FormLogger>())
            {
                var formLogger = logger.GetLogger<FormLogger>();
                formLogger.SetRichTextBox(richTextBox);
            }

        }


        private void FilltextBoxWithFilePath(TextBox textBox)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "q:\\";
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.Title = "Выберите файл или введите имя для создания нового";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    if (File.Exists(filePath))
                    {
                        MessageBox.Show($"Файл '{filePath}' был открыт.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox.Text = filePath;
                    }
                    else
                    {
                        try
                        {
                            using (FileStream fs = File.Create(filePath))
                            {

                                byte[] info = new UTF8Encoding(true).GetBytes("Новый файл создан.");
                                fs.Write(info, 0, info.Length);
                            }
                            MessageBox.Show($"Файл '{filePath}' был создан.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox.Text = filePath;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при создании файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                logger.Log($"Изменене файла для вывода лога -> {textBoxFilePath.Text} ");
            }
        }


        private void textBoxFilePath_MouseClick(object sender, MouseEventArgs e)
        {

            FilltextBoxWithFilePath(textBoxFilePath);

        }
        private void Button8_Click(object sender, EventArgs e)
        {
            ConsoleManager.Hide();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            LoggerChangeFileSender();
        }


        private void Button9_Click(object sender, EventArgs e)
        {
            InsertDistrict insert = new InsertDistrict();
            insert.ShowDialog();
            RefreshTable();
        }

        private void TextBoxFileResultPath_MouseClick(object sender, MouseEventArgs e)
        {
            FilltextBoxWithFilePath(textBoxResultFilePath);
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            resultSaver.SetPath(textBoxResultFilePath.Text);
        }
        private void FillSelector()
        {
            Dictionary<long, string> districts = dataBase.SelectDistricts();
            comboBox2District.Items.Clear();
            foreach (var distr in districts)
            {
                comboBox2District.Items.Add(distr.Value);
            }
        }

        private void resultButton_Click(object sender, EventArgs e)
        {
            string selectedDistrict = comboBox2District.SelectedItem?.ToString();
            OrderFilterByDate orderFilter = new(selectedDistrict, resultSaver);
            DataTable filteredData = orderFilter.Filter();
            dataGridView1.DataSource = filteredData;
        }

        private void Button9_Click_1(object sender, EventArgs e)
        {
            dataBase.BackUpDatabaseFromFile(SelectFile());
            dataBase.RefreshDataBase();
        }
        private void Button11_Click(object sender, EventArgs e)
        {
            dataBase.RefreshDataBase();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                LoggerChangeFormSender(richTextBox1);
                return;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                LoggerChangeFormSender(richTextBox2);
                return;
            }
        }
        private string GetSaveDirectoryPath()
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Выберите каталог для сохранения базы данных";

                // Открытие диалога для выбора каталога
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    return folderBrowserDialog.SelectedPath;
                }
            }

            return null;  // Если каталог не был выбран, возвращаем null
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string directoryPath = GetSaveDirectoryPath();
            if (!string.IsNullOrEmpty(directoryPath))
            {
                string filePath = Path.Combine(directoryPath, "backup.db");
                dataBase.SaveDatabaseToFile(filePath);
            }
        }
    }
}
