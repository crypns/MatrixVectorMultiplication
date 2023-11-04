using System;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MatrixVectorMultiplication
{
    public partial class Form1 : Form
    {
        private int[,] matrix;  // Матрица, загружаемая из файла
        private int[] vector;   // Вектор, загружаемый из файла
        private int[] result;   // Результат умножения матрицы на вектор

        public Form1()
        {
            InitializeComponent();
        }
        private void btnMatrixFile_Click(object sender, EventArgs e)
        {
            // Обработчик нажатия кнопки "Выбрать файл матрицы"

            OpenFileDialog openFileDialog = new OpenFileDialog(); // Создание объекта диалогового окна для выбора файла
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt"; // Установка фильтра расширений файлов
            openFileDialog.Title = "Выбрать файл матрицы"; // Установка заголовка диалогового окна

            if (openFileDialog.ShowDialog() == DialogResult.OK) // Если пользователь выбрал файл и нажал "ОК"
            {
                string filePath = openFileDialog.FileName; // Получение пути выбранного файла
                ReadMatrixFromFile(filePath);   // Чтение матрицы из файла
                DisplayMatrix();                // Отображение матрицы в текстовом поле
            }
        }

        private void btnVectorFile_Click(object sender, EventArgs e)
        {
            // Обработчик нажатия кнопки "Выбрать файл вектора"

            OpenFileDialog openFileDialog = new OpenFileDialog();
            // Создание экземпляра диалогового окна для открытия файла
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            // Установка фильтра файлов для отображения только текстовых файлов с расширением .txt
            openFileDialog.Title = "Выбрать файл вектора";
            // Установка заголовка диалогового окна

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Если пользователь выбрал файл и нажал "ОК" в диалоговом окне

                string filePath = openFileDialog.FileName;
                // Получение пути к выбранному файлу
                ReadVectorFromFile(filePath);
                // Вызов метода для чтения вектора из файла
                DisplayVector();
                // Вызов метода для отображения вектора в текстовом поле
            }
        }
        private void btnMultiply_Click(object sender, EventArgs e)
        {
            // Обработчик нажатия кнопки "Умножить"

            try
            {
                UpdateMatrixFromTextBox(); // Обновление матрицы из текстового поля
                UpdateVectorFromTextBox(); // Обновление вектора из текстового поля

                if (matrix == null || vector == null)
                {
                    MessageBox.Show("Файлы матрицы и вектора не загружены.");
                    return;
                }
                // Проверка, загружены ли файлы матрицы и вектора. Если хотя бы один из них не загружен,
                // выводится сообщение об ошибке и функция завершается.

                if (matrix.GetLength(1) != vector.Length)
                {
                    MessageBox.Show("Количество столбцов матрицы не совпадает с размерностью вектора.");
                    return;
                }
                // Проверка, совпадает ли количество столбцов матрицы с размерностью вектора.
                // Если не совпадает, выводится сообщение об ошибке и функция завершается.

                MultiplyMatrixVector(); // Вызов метода для умножения матрицы на вектор
                DisplayResult();        // Вызов метода для отображения результата в текстовом поле
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при умножении матрицы на вектор: " + ex.Message);
            }
        }
        private void ReadMatrixFromFile(string filePath)
        {
            // Чтение матрицы из файла

            string[] lines = File.ReadAllLines(filePath);
            // Чтение всех строк из файла и сохранение их в массив строк

            int rows = lines.Length;
            // Определение количества строк в матрице по количеству строк в файле

            int cols = lines[0].Split(' ').Length;
            // Определение количества столбцов в матрице по количеству значений в первой строке файла (разделенных пробелом)

            matrix = new int[rows, cols];
            // Создание двумерного массива для хранения матрицы с размерностью rows x cols

            for (int i = 0; i < rows; i++)
            {
                string[] values = lines[i].Split(' ');
                // Разделение строки на значения, используя пробел в качестве разделителя

                if (values.Length != cols)
                {
                    MessageBox.Show("Неправильное количество элементов в строке " + (i + 1) + ".");
                    return; // Прерываем выполнение метода, чтобы не сохранять неправильную матрицу
                }

                for (int j = 0; j < cols; j++)
                {
                    if (!int.TryParse(values[j], out int element))
                    {
                        MessageBox.Show("Неправильное значение в строке " + (i + 1) + ", столбце " + (j + 1) + ".");
                        return; // Прерываем выполнение метода, чтобы не сохранять неправильную матрицу
                    }

                    matrix[i, j] = element;
                    // Преобразование значения из строки в целое число и сохранение его в ячейку матрицы
                }
            }
        }

        private void ReadVectorFromFile(string filePath)
        {
            // Чтение вектора из файла

            string[] lines = File.ReadAllLines(filePath);
            // Чтение всех строк из файла и сохранение их в массив строк

            bool isSpaceSeparated = false;

            // Проверка формата ввода вектора
            if (lines.Length == 1 && lines[0].Contains(" "))
            {
                isSpaceSeparated = true;
                lines = lines[0].Split(' ');
            }

            vector = new int[lines.Length];
            // Создание одномерного массива для хранения вектора с размерностью, равной количеству строк/элементов

            for (int i = 0; i < lines.Length; i++)
            {
                if (isSpaceSeparated)
                {
                    if (!int.TryParse(lines[i], out int element))
                    {
                        MessageBox.Show("Неправильное значение вектора в строке " + (i + 1) + ".");
                        return; // Прерываем выполнение метода, чтобы не сохранять неправильный вектор
                    }

                    vector[i] = element;
                }
                else
                {
                    if (!int.TryParse(lines[i], out int element))
                    {
                        MessageBox.Show("Неправильное значение вектора в строке " + (i + 1) + ".");
                        return; // Прерываем выполнение метода, чтобы не сохранять неправильный вектор
                    }

                    vector[i] = element;
                }
            }
        }

        private void MultiplyMatrixVector()
        {
            // Умножение матрицы на вектор

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            result = new int[rows];
            // Создание одномерного массива для хранения результата с размерностью, равной количеству строк матрицы

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i] += matrix[i, j] * vector[j];
                    // Умножение элементов матрицы на соответствующие элементы вектора и накопление результата
                }
            }
        }
        private void DisplayMatrix()
        {
            // Отображение матрицы в текстовом поле

            StringBuilder sb = new StringBuilder();

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(matrix[i, j] + " ");
                    // Добавление значения элемента матрицы в строку с использованием табуляции в качестве разделителя
                }
                sb.AppendLine();
                // Добавление символа новой строки для перехода на следующую строку матрицы
            }

            txtMatrix.Text = sb.ToString();
            // Установка текста в текстовом поле для отображения матрицы
        }
        private void DisplayVector()
        {
            // Отображение вектора в текстовом поле

            StringBuilder sb = new StringBuilder();

            int length = vector.Length;

            for (int i = 0; i < length; i++)
            {
                sb.AppendLine(vector[i].ToString());
                // Добавление значения элемента вектора в строку с символом новой строки для перехода на следующую строку
            }

            txtVector.Text = sb.ToString();
            // Установка текста в текстовом поле для отображения вектора
        }

        private void DisplayResult()
        {
            // Отображение результата в текстовом поле

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
            {
                sb.AppendLine(result[i].ToString());
                // Добавление значения элемента результата в строку с символом новой строки для перехода на следующую строку
            }

            txtResult.Text = sb.ToString();
            // Установка текста в текстовом поле для отображения результата
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Обработчик нажатия кнопки "Сохранить"

            if (result == null)
            {
                MessageBox.Show("Результат не найден. Сначала выполните умножение.");
                // Проверяем, что результат умножения существует. Если нет, выводим сообщение об ошибке.
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            saveFileDialog.Title = "Сохранить результаты";
            saveFileDialog.ShowDialog();

            // Открытие диалогового окна для выбора файла сохранения

            if (saveFileDialog.FileName != "")
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, txtResult.Text);
                    // Записываем текст из текстового поля с результатом в выбранный файл
                    MessageBox.Show("Результаты сохранены успешно.");
                    // Выводим сообщение об успешном сохранении результатов
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при сохранении результатов: " + ex.Message);
                    // Выводим сообщение об ошибке при сохранении результатов
                }
            }
        }
        private void btnSaveVector_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            saveFileDialog.Title = "Сохранить вектор";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    UpdateVectorFromTextBox(); // Обновление вектора из текстового поля
                    File.WriteAllText(saveFileDialog.FileName, txtVector.Text);
                    MessageBox.Show("Вектор успешно сохранен.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при сохранении вектора: " + ex.Message);
                }
            }
        }

        private void btnSaveMatrix_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            saveFileDialog.Title = "Сохранить матрицу";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    UpdateMatrixFromTextBox(); // Обновление матрицы из текстового поля
                    File.WriteAllText(saveFileDialog.FileName, txtMatrix.Text);
                    MessageBox.Show("Матрица успешно сохранена.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при сохранении матрицы: " + ex.Message);
                }
            }
        }
        private void UpdateMatrixFromTextBox()
        {
            string[] lines = txtMatrix.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int rows = lines.Length;
            int cols = 0;

            // Определение максимального количества элементов в строке
            foreach (string line in lines)
            {
                string[] values = line.TrimEnd().Split(' ');
                cols = Math.Max(cols, values.Length);
            }

            matrix = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                string line = lines[i].TrimEnd(); // Удаление пробелов справа от строки

                int lastDigitIndex = line.LastIndexOfAny("0123456789".ToCharArray()); // Индекс последней цифры

                if (lastDigitIndex >= 0)
                {
                    line = line.Substring(0, lastDigitIndex + 1); // Удаление символов после последней цифры
                }

                string[] values = line.Split(' ');

                if (values.Length != cols)
                {
                    MessageBox.Show("Неправильное количество элементов в строке " + (i + 1) + ".");
                    return; // Прерываем выполнение метода, чтобы не сохранять неправильную матрицу
                }

                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = int.Parse(values[j]);
                }

                lines[i] = line; // Обновление значения lines[i] с отформатированной строкой
            }

            // Обновление текстового поля txtMatrix с удалением символов после последней цифры в каждой строке
            txtMatrix.Text = string.Join(Environment.NewLine, lines);
        }

        private void UpdateVectorFromTextBox()
        {
            string[] lines = txtVector.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int length = lines.Length;
            vector = new int[length];

            for (int i = 0; i < length; i++)
            {
                string line = lines[i].TrimEnd(); // Удаление пробелов справа от строки

                vector[i] = int.Parse(line);

                lines[i] = line; // Обновление значения lines[i] с отформатированной строкой
            }

            // Обновление текстового поля txtVector с удалением пробелов после последнего символа в каждой строке
            txtVector.Text = string.Join(Environment.NewLine, lines);
        }

        private void PrintMatrixVectorResult()
        {
            PrintDocument document = new PrintDocument();
            document.PrintPage += new PrintPageEventHandler(Document_PrintPage);
            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = document;
            previewDialog.ShowDialog();
        }
        private void Document_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 12);
            float lineHeight = font.GetHeight();

            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float offset = 10;

            // Print Matrix
            e.Graphics.DrawString("Матрица:", font, Brushes.Black, x, y);
            y += lineHeight + offset;
            string[] matrixLines = txtMatrix.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in matrixLines)
            {
                e.Graphics.DrawString(line, font, Brushes.Black, x, y);
                y += lineHeight;
            }
            y += offset;

            // Print Vector
            e.Graphics.DrawString("Вектор:", font, Brushes.Black, x, y);
            y += lineHeight + offset;
            string[] vectorLines = txtVector.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in vectorLines)
            {
                e.Graphics.DrawString(line, font, Brushes.Black, x, y);
                y += lineHeight;
            }
            y += offset;

            // Печать результата
            e.Graphics.DrawString("Результат умножения:", font, Brushes.Black, x, y);
            y += lineHeight + offset;
            string[] resultLines = txtResult.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in resultLines)
            {
                e.Graphics.DrawString(line, font, Brushes.Black, x, y);
                y += lineHeight;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintMatrixVectorResult();
        }

        private void txtMatrix_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверка, является ли введенный символ цифрой или пробелом
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Отмена события KeyPress, чтобы символ не был введен в текстовое поле
            }
        }

        private void txtVector_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверка, является ли введенный символ цифрой (код клавиши от 48 до 57) или символом переноса строки (код клавиши 13)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\r' && e.KeyChar != '\b')
            {
                e.Handled = true; // Отмена события KeyPress, чтобы символ не был введен в текстовое поле
            }
        }

        private void txtResult_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверка, является ли введенный символ цифрой или пробелом
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Отмена события KeyPress, чтобы символ не был введен в текстовое поле
            }
        }
    }
}