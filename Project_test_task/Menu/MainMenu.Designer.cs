namespace Project_test_task
{
    partial class MainMenu
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
            button1 = new Button();
            dataGridView1 = new DataGridView();
            button2_WithGenerator = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button2 = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            button9 = new Button();
            label5 = new Label();
            comboBox2District = new ComboBox();
            resultButton = new Button();
            buttonAddDistrict = new Button();
            label3 = new Label();
            richTextBox1 = new RichTextBox();
            tabPage2 = new TabPage();
            groupBox4 = new GroupBox();
            richTextBox2 = new RichTextBox();
            groupBox3 = new GroupBox();
            button8 = new Button();
            groupBox2 = new GroupBox();
            label4 = new Label();
            button10 = new Button();
            textBoxResultFilePath = new TextBox();
            label2 = new Label();
            label1 = new Label();
            comboBox1 = new ComboBox();
            button7 = new Button();
            textBoxFilePath = new TextBox();
            button6 = new Button();
            groupBox1 = new GroupBox();
            checkedListBox1 = new CheckedListBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            sqliteCommand1 = new Microsoft.Data.Sqlite.SqliteCommand();
            button11 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(6, 198);
            button1.Name = "button1";
            button1.Size = new Size(110, 46);
            button1.TabIndex = 0;
            button1.Text = "Добавить тестовый заказ";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(3, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(645, 192);
            dataGridView1.TabIndex = 4;
            // 
            // button2_WithGenerator
            // 
            button2_WithGenerator.Location = new Point(40, 297);
            button2_WithGenerator.Name = "button2_WithGenerator";
            button2_WithGenerator.Size = new Size(151, 50);
            button2_WithGenerator.TabIndex = 5;
            button2_WithGenerator.Text = "Добавить рандомные значения";
            button2_WithGenerator.UseVisualStyleBackColor = true;
            button2_WithGenerator.Click += button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 14F);
            button3.Location = new Point(613, 192);
            button3.Name = "button3";
            button3.Size = new Size(35, 30);
            button3.TabIndex = 6;
            button3.Text = "🗘";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(122, 250);
            button4.Name = "button4";
            button4.Size = new Size(106, 41);
            button4.TabIndex = 7;
            button4.Text = "Удалить таблицы";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(6, 250);
            button5.Name = "button5";
            button5.Size = new Size(110, 41);
            button5.TabIndex = 9;
            button5.Text = "Добавить из файла";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button2
            // 
            button2.Location = new Point(6, 197);
            button2.Name = "button2";
            button2.Size = new Size(110, 46);
            button2.TabIndex = 10;
            button2.Text = "Добавить заказ";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(3, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(665, 446);
            tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button11);
            tabPage1.Controls.Add(button9);
            tabPage1.Controls.Add(button3);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(comboBox2District);
            tabPage1.Controls.Add(resultButton);
            tabPage1.Controls.Add(buttonAddDistrict);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(richTextBox1);
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(button5);
            tabPage1.Controls.Add(button4);
            tabPage1.Controls.Add(button2_WithGenerator);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(657, 418);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Работа с таблицей";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.Location = new Point(253, 203);
            button9.Name = "button9";
            button9.Size = new Size(85, 39);
            button9.TabIndex = 17;
            button9.Text = "Тесты";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click_1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(243, 350);
            label5.Name = "label5";
            label5.Size = new Size(50, 15);
            label5.TabIndex = 16;
            label5.Text = "Районы";
            // 
            // comboBox2District
            // 
            comboBox2District.FormattingEnabled = true;
            comboBox2District.Location = new Point(197, 368);
            comboBox2District.Name = "comboBox2District";
            comboBox2District.Size = new Size(151, 23);
            comboBox2District.TabIndex = 15;
            // 
            // resultButton
            // 
            resultButton.Location = new Point(40, 353);
            resultButton.Name = "resultButton";
            resultButton.Size = new Size(151, 50);
            resultButton.TabIndex = 14;
            resultButton.Text = "Результат фильтрации по району";
            resultButton.UseVisualStyleBackColor = true;
            resultButton.Click += resultButton_Click;
            // 
            // buttonAddDistrict
            // 
            buttonAddDistrict.Location = new Point(122, 197);
            buttonAddDistrict.Name = "buttonAddDistrict";
            buttonAddDistrict.Size = new Size(106, 45);
            buttonAddDistrict.TabIndex = 13;
            buttonAddDistrict.Text = "Добавить район";
            buttonAddDistrict.UseVisualStyleBackColor = true;
            buttonAddDistrict.Click += Button9_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(485, 207);
            label3.Name = "label3";
            label3.Size = new Size(34, 15);
            label3.TabIndex = 12;
            label3.Text = "Логи";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(355, 225);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(293, 186);
            richTextBox1.TabIndex = 11;
            richTextBox1.Text = "";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox4);
            tabPage2.Controls.Add(groupBox3);
            tabPage2.Controls.Add(groupBox2);
            tabPage2.Controls.Add(groupBox1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(657, 418);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Настройки";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(richTextBox2);
            groupBox4.Location = new Point(6, 173);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(655, 238);
            groupBox4.TabIndex = 7;
            groupBox4.TabStop = false;
            groupBox4.Text = "Внешний логгер";
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(6, 22);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(636, 223);
            richTextBox2.TabIndex = 6;
            richTextBox2.Text = "";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(button8);
            groupBox3.Location = new Point(468, 8);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(97, 100);
            groupBox3.TabIndex = 5;
            groupBox3.TabStop = false;
            groupBox3.Text = "Консоль";
            // 
            // button8
            // 
            button8.Location = new Point(11, 59);
            button8.Name = "button8";
            button8.Size = new Size(80, 27);
            button8.TabIndex = 0;
            button8.Text = "Скрыть";
            button8.UseVisualStyleBackColor = true;
            button8.Click += Button8_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(button10);
            groupBox2.Controls.Add(textBoxResultFilePath);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(comboBox1);
            groupBox2.Controls.Add(button7);
            groupBox2.Controls.Add(textBoxFilePath);
            groupBox2.Controls.Add(button6);
            groupBox2.Location = new Point(192, 6);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(270, 161);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Изменение результирующих логгов";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 105);
            label4.Name = "label4";
            label4.Size = new Size(149, 15);
            label4.TabIndex = 8;
            label4.Text = "Путь к файлу результатов";
            // 
            // button10
            // 
            button10.Location = new Point(176, 123);
            button10.Name = "button10";
            button10.Size = new Size(75, 23);
            button10.TabIndex = 7;
            button10.Text = "Изменить";
            button10.UseVisualStyleBackColor = true;
            button10.Click += Button10_Click;
            // 
            // textBoxResultFilePath
            // 
            textBoxResultFilePath.Location = new Point(6, 123);
            textBoxResultFilePath.Name = "textBoxResultFilePath";
            textBoxResultFilePath.Size = new Size(164, 23);
            textBoxResultFilePath.TabIndex = 6;
            textBoxResultFilePath.MouseClick += TextBoxFileResultPath_MouseClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 18);
            label2.Name = "label2";
            label2.Size = new Size(120, 15);
            label2.TabIndex = 5;
            label2.Text = "Путь к файлу логгов";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 61);
            label1.Name = "label1";
            label1.Size = new Size(88, 15);
            label1.TabIndex = 4;
            label1.Text = "Выбор формы";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Главная", "Вторичная" });
            comboBox1.Location = new Point(6, 79);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(164, 23);
            comboBox1.TabIndex = 3;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // button7
            // 
            button7.Location = new Point(176, 79);
            button7.Name = "button7";
            button7.Size = new Size(75, 23);
            button7.TabIndex = 2;
            button7.Text = "Изменить";
            button7.UseVisualStyleBackColor = true;
            // 
            // textBoxFilePath
            // 
            textBoxFilePath.Location = new Point(6, 36);
            textBoxFilePath.Name = "textBoxFilePath";
            textBoxFilePath.Size = new Size(164, 23);
            textBoxFilePath.TabIndex = 1;
            textBoxFilePath.MouseClick += textBoxFilePath_MouseClick;
            // 
            // button6
            // 
            button6.Location = new Point(176, 36);
            button6.Name = "button6";
            button6.Size = new Size(75, 24);
            button6.TabIndex = 0;
            button6.Text = "Изменить";
            button6.UseVisualStyleBackColor = true;
            button6.Click += Button6_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkedListBox1);
            groupBox1.Location = new Point(6, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(162, 81);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Логирование";
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Items.AddRange(new object[] { "Консоль", "Форма", "Файл" });
            checkedListBox1.Location = new Point(0, 22);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(162, 58);
            checkedListBox1.TabIndex = 4;
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
            // 
            // sqliteCommand1
            // 
            sqliteCommand1.CommandTimeout = 30;
            sqliteCommand1.Connection = null;
            sqliteCommand1.Transaction = null;
            sqliteCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // button11
            // 
            button11.Location = new Point(197, 297);
            button11.Name = "button11";
            button11.Size = new Size(151, 50);
            button11.TabIndex = 18;
            button11.Text = "Восстановить базу данных";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // MainMenu
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(667, 450);
            Controls.Add(tabControl1);
            Name = "MainMenu";
            Text = "Меню";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private DataGridView dataGridView1;
        private Button button2_WithGenerator;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button2;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private GroupBox groupBox1;
        private CheckedListBox checkedListBox1;
        private RichTextBox richTextBox1;
        private GroupBox groupBox2;
        private TextBox textBoxFilePath;
        private Button button6;
        private Label label2;
        private Label label1;
        private ComboBox comboBox1;
        private Button button7;
        private GroupBox groupBox3;
        private Button button8;
        private RichTextBox richTextBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private GroupBox groupBox4;
        private Microsoft.Data.Sqlite.SqliteCommand sqliteCommand1;
        private Label label3;
        private Button buttonAddDistrict;
        private Button resultButton;
        private Button button10;
        private TextBox textBoxResultFilePath;
        private Label label4;
        private ComboBox comboBox2District;
        private Label label5;
        private Button button9;
        private Button button11;
    }
}
