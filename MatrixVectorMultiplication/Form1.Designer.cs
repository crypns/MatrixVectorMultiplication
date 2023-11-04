namespace MatrixVectorMultiplication
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
            btnVectorFile = new Button();
            btnMultiply = new Button();
            btnSave = new Button();
            txtResult = new TextBox();
            txtMatrix = new TextBox();
            txtVector = new TextBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            btnSaveVector = new Button();
            groupBox3 = new GroupBox();
            btnPrint = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnSaveMatrix = new Button();
            btnMatrixFile = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnVectorFile
            // 
            btnVectorFile.Dock = DockStyle.Fill;
            btnVectorFile.Location = new Point(290, 363);
            btnVectorFile.Name = "btnVectorFile";
            btnVectorFile.Size = new Size(281, 24);
            btnVectorFile.TabIndex = 1;
            btnVectorFile.Text = "Загрузить вектор";
            btnVectorFile.UseVisualStyleBackColor = true;
            btnVectorFile.Click += btnVectorFile_Click;
            // 
            // btnMultiply
            // 
            btnMultiply.Dock = DockStyle.Fill;
            btnMultiply.Location = new Point(577, 363);
            btnMultiply.Name = "btnMultiply";
            btnMultiply.Size = new Size(282, 24);
            btnMultiply.TabIndex = 2;
            btnMultiply.Text = "Умножить";
            btnMultiply.UseVisualStyleBackColor = true;
            btnMultiply.Click += btnMultiply_Click;
            // 
            // btnSave
            // 
            btnSave.Dock = DockStyle.Fill;
            btnSave.Location = new Point(577, 393);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(282, 24);
            btnSave.TabIndex = 3;
            btnSave.Text = "Сохранить результат";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtResult
            // 
            txtResult.Dock = DockStyle.Fill;
            txtResult.Location = new Point(3, 19);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.Size = new Size(276, 332);
            txtResult.TabIndex = 4;
            txtResult.WordWrap = false;
            txtResult.KeyPress += txtResult_KeyPress;
            // 
            // txtMatrix
            // 
            txtMatrix.Dock = DockStyle.Fill;
            txtMatrix.Location = new Point(3, 19);
            txtMatrix.Multiline = true;
            txtMatrix.Name = "txtMatrix";
            txtMatrix.ScrollBars = ScrollBars.Both;
            txtMatrix.Size = new Size(275, 332);
            txtMatrix.TabIndex = 5;
            txtMatrix.WordWrap = false;
            txtMatrix.KeyPress += txtMatrix_KeyPress;
            // 
            // txtVector
            // 
            txtVector.Dock = DockStyle.Fill;
            txtVector.Location = new Point(3, 19);
            txtVector.Multiline = true;
            txtVector.Name = "txtVector";
            txtVector.Size = new Size(275, 332);
            txtVector.TabIndex = 6;
            txtVector.WordWrap = false;
            txtVector.KeyPress += txtVector_KeyPress;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtMatrix);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(281, 354);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Матрица";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtVector);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(290, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(281, 354);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            groupBox2.Text = "Вектор";
            // 
            // btnSaveVector
            // 
            btnSaveVector.Dock = DockStyle.Fill;
            btnSaveVector.Location = new Point(290, 393);
            btnSaveVector.Name = "btnSaveVector";
            btnSaveVector.Size = new Size(281, 24);
            btnSaveVector.TabIndex = 13;
            btnSaveVector.Text = "Сохранить вектор";
            btnSaveVector.UseVisualStyleBackColor = true;
            btnSaveVector.Click += btnSaveVector_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txtResult);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(577, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(282, 354);
            groupBox3.TabIndex = 11;
            groupBox3.TabStop = false;
            groupBox3.Text = "Результат умножения";
            // 
            // btnPrint
            // 
            btnPrint.Dock = DockStyle.Fill;
            btnPrint.Location = new Point(577, 423);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(282, 24);
            btnPrint.TabIndex = 5;
            btnPrint.Text = "Печать";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.Controls.Add(btnPrint, 2, 3);
            tableLayoutPanel1.Controls.Add(btnSave, 2, 2);
            tableLayoutPanel1.Controls.Add(groupBox3, 2, 0);
            tableLayoutPanel1.Controls.Add(btnMultiply, 2, 1);
            tableLayoutPanel1.Controls.Add(btnSaveVector, 1, 2);
            tableLayoutPanel1.Controls.Add(btnSaveMatrix, 0, 2);
            tableLayoutPanel1.Controls.Add(btnVectorFile, 1, 1);
            tableLayoutPanel1.Controls.Add(btnMatrixFile, 0, 1);
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBox2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.Size = new Size(862, 450);
            tableLayoutPanel1.TabIndex = 14;
            // 
            // btnSaveMatrix
            // 
            btnSaveMatrix.Dock = DockStyle.Fill;
            btnSaveMatrix.Location = new Point(3, 393);
            btnSaveMatrix.Name = "btnSaveMatrix";
            btnSaveMatrix.Size = new Size(281, 24);
            btnSaveMatrix.TabIndex = 12;
            btnSaveMatrix.Text = "Сохранить матрицу";
            btnSaveMatrix.UseVisualStyleBackColor = true;
            btnSaveMatrix.Click += btnSaveMatrix_Click;
            // 
            // btnMatrixFile
            // 
            btnMatrixFile.Dock = DockStyle.Fill;
            btnMatrixFile.Location = new Point(3, 363);
            btnMatrixFile.Name = "btnMatrixFile";
            btnMatrixFile.Size = new Size(281, 24);
            btnMatrixFile.TabIndex = 0;
            btnMatrixFile.Text = "Загрузить матрицу из файла";
            btnMatrixFile.UseVisualStyleBackColor = true;
            btnMatrixFile.Click += btnMatrixFile_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(862, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Умножение матрицы N * M на вектор";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button btnVectorFile;
        private Button btnMultiply;
        private Button btnSave;
        private TextBox txtResult;
        private TextBox txtMatrix;
        private TextBox txtVector;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button btnSaveVector;
        private Button btnPrint;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnSaveMatrix;
        private Button btnMatrixFile;
    }
}