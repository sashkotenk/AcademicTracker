<<<<<<< HEAD
﻿using System;
using System.Drawing; // Потрібно для кольорів та шрифтів
using System.Windows.Forms;
using Tracker.Core;

namespace Tracker.Desktop
{
    public partial class MainDashboard : Form
    {
        private readonly AcademicJournal _journalService;

        // --- UI Colors (Modern Palette) ---
        private readonly Color _primaryColor = Color.FromArgb(58, 110, 165); // Професійний синій
        private readonly Color _accentColor = Color.FromArgb(255, 107, 107);  // Акцент (наприклад, для помилок)
        private readonly Color _backgroundColor = Color.FromArgb(240, 242, 245); // Світло-сірий фон (як у Facebook/Web)
        private readonly Color _cardColor = Color.White;
        private readonly Color _textColor = Color.FromArgb(45, 52, 54);

        // --- Fonts ---
        private readonly Font _headerFont = new Font("Segoe UI", 14F, FontStyle.Bold);
        private readonly Font _labelFont = new Font("Segoe UI", 11F, FontStyle.Regular);
        private readonly Font _inputFont = new Font("Segoe UI", 12F, FontStyle.Regular);

        // --- Controls ---
        private Panel _pnlHeader = null!;
        private Label _lblTitle = null!;

        private Panel _pnlInputCard = null!; // Ліва панель (картка)
        private Panel _pnlListCard = null!;  // Права панель (картка)

        private TextBox _txtName = null!;
        private TextBox _txtEmail = null!;
        private NumericUpDown _numGrade = null!;
        private Button _btnSubmit = null!;
        private ListBox _lstResults = null!;
        private Label _lblAverageScore = null!;

        public MainDashboard()
        {
            _journalService = new AcademicJournal();
            InitializeComponent();

            // Налаштування самої форми
            this.Text = "Academic Tracker Pro";
            this.ClientSize = new Size(1280, 720);
            this.BackColor = _backgroundColor;
            this.StartPosition = FormStartPosition.CenterScreen;

            BuildModernInterface();
        }

        private void BuildModernInterface()
        {
            // 1. Top Header Bar (Верхня смуга)
            _pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = _primaryColor,
                Padding = new Padding(30, 0, 0, 0) // Відступ зліва
            };

            _lblTitle = new Label
            {
                Text = "Academic Performance Journal",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 20) // Позиція всередині хедера
            };
            _pnlHeader.Controls.Add(_lblTitle);
            this.Controls.Add(_pnlHeader);

            // 2. Left Card (Input Section) - Біла картка зліва
            _pnlInputCard = CreateCardPanel(new Point(30, 100), new Size(400, 500));
            this.Controls.Add(_pnlInputCard);

            // Заголовок картки вводу
            var lblInputHeader = new Label
            {
                Text = "New Student Entry",
                Font = _headerFont,
                ForeColor = _primaryColor,
                AutoSize = true,
                Location = new Point(20, 20)
            };
            _pnlInputCard.Controls.Add(lblInputHeader);

            // Поля вводу
            _txtName = AddLabeledInput(_pnlInputCard, "Full Name:", 70);
            _txtEmail = AddLabeledInput(_pnlInputCard, "Email Address:", 150);

            // Окремо додаємо NumericUpDown, бо це не TextBox
            var lblGrade = new Label { Text = "Score (0-100):", Location = new Point(20, 230), Font = _labelFont, ForeColor = _textColor, AutoSize = true };
            _numGrade = new NumericUpDown
            {
                Location = new Point(20, 260),
                Width = 360,
                Font = _inputFont,
                Minimum = 0,
                Maximum = 100,
                BorderStyle = BorderStyle.FixedSingle
            };
            _pnlInputCard.Controls.Add(lblGrade);
            _pnlInputCard.Controls.Add(_numGrade);

            // Стильна кнопка
            _btnSubmit = new Button
            {
                Text = "ADD RECORD",
                Location = new Point(20, 330),
                Size = new Size(360, 50),
                BackColor = _primaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            _btnSubmit.FlatAppearance.BorderSize = 0; // Прибираємо рамку
            _btnSubmit.Click += OnSubmitClicked;
            _pnlInputCard.Controls.Add(_btnSubmit);


            // 3. Right Card (Results Section) - Біла картка справа
            // Вираховуємо ширину, щоб заповнити екран
            int rightCardWidth = this.ClientSize.Width - 480;
            _pnlListCard = CreateCardPanel(new Point(450, 100), new Size(rightCardWidth, 500));
            this.Controls.Add(_pnlListCard);

            var lblListHeader = new Label
            {
                Text = "Class Register",
                Font = _headerFont,
                ForeColor = _primaryColor,
                AutoSize = true,
                Location = new Point(20, 20)
            };
            _pnlListCard.Controls.Add(lblListHeader);

            // Список студентів
            _lstResults = new ListBox
            {
                Location = new Point(20, 70),
                Size = new Size(rightCardWidth - 40, 350),
                Font = new Font("Consolas", 11F), // Моноширинний шрифт для рівних колонок
                BorderStyle = BorderStyle.None, // Без рамки виглядає чистіше
                BackColor = Color.FromArgb(248, 249, 250), // Дуже світлий фон для списку
                ForeColor = _textColor,
                ItemHeight = 30,
                DrawMode = DrawMode.OwnerDrawFixed // Дозволяє кастомну відмальовку (опціонально), поки залишимо стандарт
            };
            _pnlListCard.Controls.Add(_lstResults);

            // Лейбл середнього балу внизу правої картки
            _lblAverageScore = new Label
            {
                Text = "Average Score: 0.00",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = _textColor,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(20, 440),
                Size = new Size(rightCardWidth - 40, 40)
            };
            _pnlListCard.Controls.Add(_lblAverageScore);

            // Адаптація при зміні розміру вікна (Anchor)
            _pnlListCard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            _lstResults.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            _lblAverageScore.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        }

        // Допоміжний метод для створення "карток" (білих панелей)
        private Panel CreateCardPanel(Point location, Size size)
        {
            return new Panel
            {
                Location = location,
                Size = size,
                BackColor = _cardColor,
                // На жаль, прості тіні у WinForms робити важко, тому просто використовуємо контраст
                BorderStyle = BorderStyle.None
            };
        }

        // Допоміжний метод для швидкого додавання текстових полів
        private TextBox AddLabeledInput(Panel parent, string labelText, int yPos)
        {
            var label = new Label
            {
                Text = labelText,
                Location = new Point(20, yPos),
                Font = _labelFont,
                ForeColor = _textColor,
                AutoSize = true
            };

            var textBox = new TextBox
            {
                Location = new Point(20, yPos + 30),
                Width = 360,
                Font = _inputFont, // Більший шрифт автоматично робить поле вищим
                BorderStyle = BorderStyle.FixedSingle
            };

            parent.Controls.Add(label);
            parent.Controls.Add(textBox);
            return textBox;
        }

        // --- Logic (майже без змін) ---

        private void OnSubmitClicked(object? sender, EventArgs e)
        {
            try
            {
                string name = _txtName.Text.Trim();
                string email = _txtEmail.Text.Trim();
                int grade = (int)_numGrade.Value;

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Please fill in all required fields!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Learner newLearner = new Learner(name, email);
                _journalService.RegisterGrade(newLearner, grade);

                RefreshDataList();

                // Очистка та фокус
                _txtName.Clear();
                _txtEmail.Clear();
                _numGrade.Value = 0;
                _txtName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshDataList()
        {
            _lstResults.BeginUpdate();
            _lstResults.Items.Clear();

            foreach (var entry in _journalService.GetEntries())
            {
                // Форматуємо гарно, з вирівнюванням
                string displayLine = $"{entry.Key.FullName,-25} | {entry.Value,3} pts | {entry.Key.Email}";
                _lstResults.Items.Add(displayLine);
            }

            double avg = _journalService.CalculateGroupAverage();
            _lblAverageScore.Text = $"Class Average: {avg:F2}";

            // Змінюємо колір середнього балу залежно від успішності
            if (avg >= 90) _lblAverageScore.ForeColor = Color.Green;
            else if (avg < 60 && avg > 0) _lblAverageScore.ForeColor = _accentColor;
            else _lblAverageScore.ForeColor = _textColor;

            _lstResults.EndUpdate();
        }
    }
=======
﻿using System;
using System.Drawing; // Потрібно для кольорів та шрифтів
using System.Windows.Forms;
using Tracker.Core;

namespace Tracker.Desktop
{
    public partial class MainDashboard : Form
    {
        private readonly AcademicJournal _journalService;

        // --- UI Colors (Modern Palette) ---
        private readonly Color _primaryColor = Color.FromArgb(58, 110, 165); // Професійний синій
        private readonly Color _accentColor = Color.FromArgb(255, 107, 107);  // Акцент (наприклад, для помилок)
        private readonly Color _backgroundColor = Color.FromArgb(240, 242, 245); // Світло-сірий фон (як у Facebook/Web)
        private readonly Color _cardColor = Color.White;
        private readonly Color _textColor = Color.FromArgb(45, 52, 54);

        // --- Fonts ---
        private readonly Font _headerFont = new Font("Segoe UI", 14F, FontStyle.Bold);
        private readonly Font _labelFont = new Font("Segoe UI", 11F, FontStyle.Regular);
        private readonly Font _inputFont = new Font("Segoe UI", 12F, FontStyle.Regular);

        // --- Controls ---
        private Panel _pnlHeader = null!;
        private Label _lblTitle = null!;

        private Panel _pnlInputCard = null!; // Ліва панель (картка)
        private Panel _pnlListCard = null!;  // Права панель (картка)

        private TextBox _txtName = null!;
        private TextBox _txtEmail = null!;
        private NumericUpDown _numGrade = null!;
        private Button _btnSubmit = null!;
        private ListBox _lstResults = null!;
        private Label _lblAverageScore = null!;

        public MainDashboard()
        {
            _journalService = new AcademicJournal();
            InitializeComponent();

            // Налаштування самої форми
            this.Text = "Academic Tracker Pro";
            this.ClientSize = new Size(1280, 720);
            this.BackColor = _backgroundColor;
            this.StartPosition = FormStartPosition.CenterScreen;

            BuildModernInterface();
        }

        private void BuildModernInterface()
        {
            // 1. Top Header Bar (Верхня смуга)
            _pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = _primaryColor,
                Padding = new Padding(30, 0, 0, 0) // Відступ зліва
            };

            _lblTitle = new Label
            {
                Text = "Academic Performance Journal",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 20) // Позиція всередині хедера
            };
            _pnlHeader.Controls.Add(_lblTitle);
            this.Controls.Add(_pnlHeader);

            // 2. Left Card (Input Section) - Біла картка зліва
            _pnlInputCard = CreateCardPanel(new Point(30, 100), new Size(400, 500));
            this.Controls.Add(_pnlInputCard);

            // Заголовок картки вводу
            var lblInputHeader = new Label
            {
                Text = "New Student Entry",
                Font = _headerFont,
                ForeColor = _primaryColor,
                AutoSize = true,
                Location = new Point(20, 20)
            };
            _pnlInputCard.Controls.Add(lblInputHeader);

            // Поля вводу
            _txtName = AddLabeledInput(_pnlInputCard, "Full Name:", 70);
            _txtEmail = AddLabeledInput(_pnlInputCard, "Email Address:", 150);

            // Окремо додаємо NumericUpDown, бо це не TextBox
            var lblGrade = new Label { Text = "Score (0-100):", Location = new Point(20, 230), Font = _labelFont, ForeColor = _textColor, AutoSize = true };
            _numGrade = new NumericUpDown
            {
                Location = new Point(20, 260),
                Width = 360,
                Font = _inputFont,
                Minimum = 0,
                Maximum = 100,
                BorderStyle = BorderStyle.FixedSingle
            };
            _pnlInputCard.Controls.Add(lblGrade);
            _pnlInputCard.Controls.Add(_numGrade);

            // Стильна кнопка
            _btnSubmit = new Button
            {
                Text = "ADD RECORD",
                Location = new Point(20, 330),
                Size = new Size(360, 50),
                BackColor = _primaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            _btnSubmit.FlatAppearance.BorderSize = 0; // Прибираємо рамку
            _btnSubmit.Click += OnSubmitClicked;
            _pnlInputCard.Controls.Add(_btnSubmit);


            // 3. Right Card (Results Section) - Біла картка справа
            // Вираховуємо ширину, щоб заповнити екран
            int rightCardWidth = this.ClientSize.Width - 480;
            _pnlListCard = CreateCardPanel(new Point(450, 100), new Size(rightCardWidth, 500));
            this.Controls.Add(_pnlListCard);

            var lblListHeader = new Label
            {
                Text = "Class Register",
                Font = _headerFont,
                ForeColor = _primaryColor,
                AutoSize = true,
                Location = new Point(20, 20)
            };
            _pnlListCard.Controls.Add(lblListHeader);

            // Список студентів
            _lstResults = new ListBox
            {
                Location = new Point(20, 70),
                Size = new Size(rightCardWidth - 40, 350),
                Font = new Font("Consolas", 11F), // Моноширинний шрифт для рівних колонок
                BorderStyle = BorderStyle.None, // Без рамки виглядає чистіше
                BackColor = Color.FromArgb(248, 249, 250), // Дуже світлий фон для списку
                ForeColor = _textColor,
                ItemHeight = 30,
                DrawMode = DrawMode.OwnerDrawFixed // Дозволяє кастомну відмальовку (опціонально), поки залишимо стандарт
            };
            _pnlListCard.Controls.Add(_lstResults);

            // Лейбл середнього балу внизу правої картки
            _lblAverageScore = new Label
            {
                Text = "Average Score: 0.00",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = _textColor,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(20, 440),
                Size = new Size(rightCardWidth - 40, 40)
            };
            _pnlListCard.Controls.Add(_lblAverageScore);

            // Адаптація при зміні розміру вікна (Anchor)
            _pnlListCard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            _lstResults.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            _lblAverageScore.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        }

        // Допоміжний метод для створення "карток" (білих панелей)
        private Panel CreateCardPanel(Point location, Size size)
        {
            return new Panel
            {
                Location = location,
                Size = size,
                BackColor = _cardColor,
                // На жаль, прості тіні у WinForms робити важко, тому просто використовуємо контраст
                BorderStyle = BorderStyle.None
            };
        }

        // Допоміжний метод для швидкого додавання текстових полів
        private TextBox AddLabeledInput(Panel parent, string labelText, int yPos)
        {
            var label = new Label
            {
                Text = labelText,
                Location = new Point(20, yPos),
                Font = _labelFont,
                ForeColor = _textColor,
                AutoSize = true
            };

            var textBox = new TextBox
            {
                Location = new Point(20, yPos + 30),
                Width = 360,
                Font = _inputFont, // Більший шрифт автоматично робить поле вищим
                BorderStyle = BorderStyle.FixedSingle
            };

            parent.Controls.Add(label);
            parent.Controls.Add(textBox);
            return textBox;
        }

        // --- Logic (майже без змін) ---

        private void OnSubmitClicked(object? sender, EventArgs e)
        {
            try
            {
                string name = _txtName.Text.Trim();
                string email = _txtEmail.Text.Trim();
                int grade = (int)_numGrade.Value;

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Please fill in all required fields!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Learner newLearner = new Learner(name, email);
                _journalService.RegisterGrade(newLearner, grade);

                RefreshDataList();

                // Очистка та фокус
                _txtName.Clear();
                _txtEmail.Clear();
                _numGrade.Value = 0;
                _txtName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshDataList()
        {
            _lstResults.BeginUpdate();
            _lstResults.Items.Clear();

            foreach (var entry in _journalService.GetEntries())
            {
                // Форматуємо гарно, з вирівнюванням
                string displayLine = $"{entry.Key.FullName,-25} | {entry.Value,3} pts | {entry.Key.Email}";
                _lstResults.Items.Add(displayLine);
            }

            double avg = _journalService.CalculateGroupAverage();
            _lblAverageScore.Text = $"Class Average: {avg:F2}";

            // Змінюємо колір середнього балу залежно від успішності
            if (avg >= 90) _lblAverageScore.ForeColor = Color.Green;
            else if (avg < 60 && avg > 0) _lblAverageScore.ForeColor = _accentColor;
            else _lblAverageScore.ForeColor = _textColor;

            _lstResults.EndUpdate();
        }
    }
>>>>>>> 27d15c0 (Lab 3)
}