using System;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MatrixVectorMultiplication
{
    public partial class Form1 : Form
    {
        private int[,] matrix;  // �������, ����������� �� �����
        private int[] vector;   // ������, ����������� �� �����
        private int[] result;   // ��������� ��������� ������� �� ������

        public Form1()
        {
            InitializeComponent();
        }
        private void btnMatrixFile_Click(object sender, EventArgs e)
        {
            // ���������� ������� ������ "������� ���� �������"

            OpenFileDialog openFileDialog = new OpenFileDialog(); // �������� ������� ����������� ���� ��� ������ �����
            openFileDialog.Filter = "��������� ����� (*.txt)|*.txt"; // ��������� ������� ���������� ������
            openFileDialog.Title = "������� ���� �������"; // ��������� ��������� ����������� ����

            if (openFileDialog.ShowDialog() == DialogResult.OK) // ���� ������������ ������ ���� � ����� "��"
            {
                string filePath = openFileDialog.FileName; // ��������� ���� ���������� �����
                ReadMatrixFromFile(filePath);   // ������ ������� �� �����
                DisplayMatrix();                // ����������� ������� � ��������� ����
            }
        }

        private void btnVectorFile_Click(object sender, EventArgs e)
        {
            // ���������� ������� ������ "������� ���� �������"

            OpenFileDialog openFileDialog = new OpenFileDialog();
            // �������� ���������� ����������� ���� ��� �������� �����
            openFileDialog.Filter = "��������� ����� (*.txt)|*.txt";
            // ��������� ������� ������ ��� ����������� ������ ��������� ������ � ����������� .txt
            openFileDialog.Title = "������� ���� �������";
            // ��������� ��������� ����������� ����

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // ���� ������������ ������ ���� � ����� "��" � ���������� ����

                string filePath = openFileDialog.FileName;
                // ��������� ���� � ���������� �����
                ReadVectorFromFile(filePath);
                // ����� ������ ��� ������ ������� �� �����
                DisplayVector();
                // ����� ������ ��� ����������� ������� � ��������� ����
            }
        }
        private void btnMultiply_Click(object sender, EventArgs e)
        {
            // ���������� ������� ������ "��������"

            try
            {
                UpdateMatrixFromTextBox(); // ���������� ������� �� ���������� ����
                UpdateVectorFromTextBox(); // ���������� ������� �� ���������� ����

                if (matrix == null || vector == null)
                {
                    MessageBox.Show("����� ������� � ������� �� ���������.");
                    return;
                }
                // ��������, ��������� �� ����� ������� � �������. ���� ���� �� ���� �� ��� �� ��������,
                // ��������� ��������� �� ������ � ������� �����������.

                if (matrix.GetLength(1) != vector.Length)
                {
                    MessageBox.Show("���������� �������� ������� �� ��������� � ������������ �������.");
                    return;
                }
                // ��������, ��������� �� ���������� �������� ������� � ������������ �������.
                // ���� �� ���������, ��������� ��������� �� ������ � ������� �����������.

                MultiplyMatrixVector(); // ����� ������ ��� ��������� ������� �� ������
                DisplayResult();        // ����� ������ ��� ����������� ���������� � ��������� ����
            }
            catch (Exception ex)
            {
                MessageBox.Show("��������� ������ ��� ��������� ������� �� ������: " + ex.Message);
            }
        }
        private void ReadMatrixFromFile(string filePath)
        {
            // ������ ������� �� �����

            string[] lines = File.ReadAllLines(filePath);
            // ������ ���� ����� �� ����� � ���������� �� � ������ �����

            int rows = lines.Length;
            // ����������� ���������� ����� � ������� �� ���������� ����� � �����

            int cols = lines[0].Split(' ').Length;
            // ����������� ���������� �������� � ������� �� ���������� �������� � ������ ������ ����� (����������� ��������)

            matrix = new int[rows, cols];
            // �������� ���������� ������� ��� �������� ������� � ������������ rows x cols

            for (int i = 0; i < rows; i++)
            {
                string[] values = lines[i].Split(' ');
                // ���������� ������ �� ��������, ��������� ������ � �������� �����������

                if (values.Length != cols)
                {
                    MessageBox.Show("������������ ���������� ��������� � ������ " + (i + 1) + ".");
                    return; // ��������� ���������� ������, ����� �� ��������� ������������ �������
                }

                for (int j = 0; j < cols; j++)
                {
                    if (!int.TryParse(values[j], out int element))
                    {
                        MessageBox.Show("������������ �������� � ������ " + (i + 1) + ", ������� " + (j + 1) + ".");
                        return; // ��������� ���������� ������, ����� �� ��������� ������������ �������
                    }

                    matrix[i, j] = element;
                    // �������������� �������� �� ������ � ����� ����� � ���������� ��� � ������ �������
                }
            }
        }

        private void ReadVectorFromFile(string filePath)
        {
            // ������ ������� �� �����

            string[] lines = File.ReadAllLines(filePath);
            // ������ ���� ����� �� ����� � ���������� �� � ������ �����

            bool isSpaceSeparated = false;

            // �������� ������� ����� �������
            if (lines.Length == 1 && lines[0].Contains(" "))
            {
                isSpaceSeparated = true;
                lines = lines[0].Split(' ');
            }

            vector = new int[lines.Length];
            // �������� ����������� ������� ��� �������� ������� � ������������, ������ ���������� �����/���������

            for (int i = 0; i < lines.Length; i++)
            {
                if (isSpaceSeparated)
                {
                    if (!int.TryParse(lines[i], out int element))
                    {
                        MessageBox.Show("������������ �������� ������� � ������ " + (i + 1) + ".");
                        return; // ��������� ���������� ������, ����� �� ��������� ������������ ������
                    }

                    vector[i] = element;
                }
                else
                {
                    if (!int.TryParse(lines[i], out int element))
                    {
                        MessageBox.Show("������������ �������� ������� � ������ " + (i + 1) + ".");
                        return; // ��������� ���������� ������, ����� �� ��������� ������������ ������
                    }

                    vector[i] = element;
                }
            }
        }

        private void MultiplyMatrixVector()
        {
            // ��������� ������� �� ������

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            result = new int[rows];
            // �������� ����������� ������� ��� �������� ���������� � ������������, ������ ���������� ����� �������

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i] += matrix[i, j] * vector[j];
                    // ��������� ��������� ������� �� ��������������� �������� ������� � ���������� ����������
                }
            }
        }
        private void DisplayMatrix()
        {
            // ����������� ������� � ��������� ����

            StringBuilder sb = new StringBuilder();

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(matrix[i, j] + " ");
                    // ���������� �������� �������� ������� � ������ � �������������� ��������� � �������� �����������
                }
                sb.AppendLine();
                // ���������� ������� ����� ������ ��� �������� �� ��������� ������ �������
            }

            txtMatrix.Text = sb.ToString();
            // ��������� ������ � ��������� ���� ��� ����������� �������
        }
        private void DisplayVector()
        {
            // ����������� ������� � ��������� ����

            StringBuilder sb = new StringBuilder();

            int length = vector.Length;

            for (int i = 0; i < length; i++)
            {
                sb.AppendLine(vector[i].ToString());
                // ���������� �������� �������� ������� � ������ � �������� ����� ������ ��� �������� �� ��������� ������
            }

            txtVector.Text = sb.ToString();
            // ��������� ������ � ��������� ���� ��� ����������� �������
        }

        private void DisplayResult()
        {
            // ����������� ���������� � ��������� ����

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
            {
                sb.AppendLine(result[i].ToString());
                // ���������� �������� �������� ���������� � ������ � �������� ����� ������ ��� �������� �� ��������� ������
            }

            txtResult.Text = sb.ToString();
            // ��������� ������ � ��������� ���� ��� ����������� ����������
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // ���������� ������� ������ "���������"

            if (result == null)
            {
                MessageBox.Show("��������� �� ������. ������� ��������� ���������.");
                // ���������, ��� ��������� ��������� ����������. ���� ���, ������� ��������� �� ������.
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "��������� ����� (*.txt)|*.txt";
            saveFileDialog.Title = "��������� ����������";
            saveFileDialog.ShowDialog();

            // �������� ����������� ���� ��� ������ ����� ����������

            if (saveFileDialog.FileName != "")
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, txtResult.Text);
                    // ���������� ����� �� ���������� ���� � ����������� � ��������� ����
                    MessageBox.Show("���������� ��������� �������.");
                    // ������� ��������� �� �������� ���������� �����������
                }
                catch (Exception ex)
                {
                    MessageBox.Show("��������� ������ ��� ���������� �����������: " + ex.Message);
                    // ������� ��������� �� ������ ��� ���������� �����������
                }
            }
        }
        private void btnSaveVector_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "��������� ����� (*.txt)|*.txt";
            saveFileDialog.Title = "��������� ������";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    UpdateVectorFromTextBox(); // ���������� ������� �� ���������� ����
                    File.WriteAllText(saveFileDialog.FileName, txtVector.Text);
                    MessageBox.Show("������ ������� ��������.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("��������� ������ ��� ���������� �������: " + ex.Message);
                }
            }
        }

        private void btnSaveMatrix_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "��������� ����� (*.txt)|*.txt";
            saveFileDialog.Title = "��������� �������";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    UpdateMatrixFromTextBox(); // ���������� ������� �� ���������� ����
                    File.WriteAllText(saveFileDialog.FileName, txtMatrix.Text);
                    MessageBox.Show("������� ������� ���������.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("��������� ������ ��� ���������� �������: " + ex.Message);
                }
            }
        }
        private void UpdateMatrixFromTextBox()
        {
            string[] lines = txtMatrix.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int rows = lines.Length;
            int cols = 0;

            // ����������� ������������� ���������� ��������� � ������
            foreach (string line in lines)
            {
                string[] values = line.TrimEnd().Split(' ');
                cols = Math.Max(cols, values.Length);
            }

            matrix = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                string line = lines[i].TrimEnd(); // �������� �������� ������ �� ������

                int lastDigitIndex = line.LastIndexOfAny("0123456789".ToCharArray()); // ������ ��������� �����

                if (lastDigitIndex >= 0)
                {
                    line = line.Substring(0, lastDigitIndex + 1); // �������� �������� ����� ��������� �����
                }

                string[] values = line.Split(' ');

                if (values.Length != cols)
                {
                    MessageBox.Show("������������ ���������� ��������� � ������ " + (i + 1) + ".");
                    return; // ��������� ���������� ������, ����� �� ��������� ������������ �������
                }

                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = int.Parse(values[j]);
                }

                lines[i] = line; // ���������� �������� lines[i] � ����������������� �������
            }

            // ���������� ���������� ���� txtMatrix � ��������� �������� ����� ��������� ����� � ������ ������
            txtMatrix.Text = string.Join(Environment.NewLine, lines);
        }

        private void UpdateVectorFromTextBox()
        {
            string[] lines = txtVector.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int length = lines.Length;
            vector = new int[length];

            for (int i = 0; i < length; i++)
            {
                string line = lines[i].TrimEnd(); // �������� �������� ������ �� ������

                vector[i] = int.Parse(line);

                lines[i] = line; // ���������� �������� lines[i] � ����������������� �������
            }

            // ���������� ���������� ���� txtVector � ��������� �������� ����� ���������� ������� � ������ ������
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
            e.Graphics.DrawString("�������:", font, Brushes.Black, x, y);
            y += lineHeight + offset;
            string[] matrixLines = txtMatrix.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in matrixLines)
            {
                e.Graphics.DrawString(line, font, Brushes.Black, x, y);
                y += lineHeight;
            }
            y += offset;

            // Print Vector
            e.Graphics.DrawString("������:", font, Brushes.Black, x, y);
            y += lineHeight + offset;
            string[] vectorLines = txtVector.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in vectorLines)
            {
                e.Graphics.DrawString(line, font, Brushes.Black, x, y);
                y += lineHeight;
            }
            y += offset;

            // ������ ����������
            e.Graphics.DrawString("��������� ���������:", font, Brushes.Black, x, y);
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
            // ��������, �������� �� ��������� ������ ������ ��� ��������
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // ������ ������� KeyPress, ����� ������ �� ��� ������ � ��������� ����
            }
        }

        private void txtVector_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ��������, �������� �� ��������� ������ ������ (��� ������� �� 48 �� 57) ��� �������� �������� ������ (��� ������� 13)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\r' && e.KeyChar != '\b')
            {
                e.Handled = true; // ������ ������� KeyPress, ����� ������ �� ��� ������ � ��������� ����
            }
        }

        private void txtResult_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ��������, �������� �� ��������� ������ ������ ��� ��������
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // ������ ������� KeyPress, ����� ������ �� ��� ������ � ��������� ����
            }
        }
    }
}