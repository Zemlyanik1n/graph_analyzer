namespace Analyzer
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
            this.analyzeButton = new System.Windows.Forms.Button();
            this.semanticButton = new System.Windows.Forms.Button();
            this.enterTextBox = new System.Windows.Forms.TextBox();
            this.semanticTextBox = new System.Windows.Forms.TextBox();
            this.checkTextBox = new System.Windows.Forms.TextBox();
            this.constBox = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.viewBox = new System.Windows.Forms.TextBox();
            this.typeBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // analyzeButton
            // 
            this.analyzeButton.Location = new System.Drawing.Point(58, 6);
            this.analyzeButton.Margin = new System.Windows.Forms.Padding(6);
            this.analyzeButton.Name = "analyzeButton";
            this.analyzeButton.Size = new System.Drawing.Size(266, 66);
            this.analyzeButton.TabIndex = 0;
            this.analyzeButton.Text = "Анализ";
            this.analyzeButton.UseVisualStyleBackColor = true;
            this.analyzeButton.Click += new System.EventHandler(this.analyzeButton_Click);
            // 
            // semanticButton
            // 
            this.semanticButton.Enabled = false;
            this.semanticButton.Location = new System.Drawing.Point(1636, 15);
            this.semanticButton.Margin = new System.Windows.Forms.Padding(6);
            this.semanticButton.Name = "semanticButton";
            this.semanticButton.Size = new System.Drawing.Size(266, 66);
            this.semanticButton.TabIndex = 1;
            this.semanticButton.Text = "Семантика";
            this.semanticButton.UseVisualStyleBackColor = true;
            this.semanticButton.Visible = false;
            this.semanticButton.Click += new System.EventHandler(this.semanticButton_Click);
            // 
            // enterTextBox
            // 
            this.enterTextBox.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.enterTextBox.Location = new System.Drawing.Point(58, 109);
            this.enterTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.enterTextBox.Multiline = true;
            this.enterTextBox.Name = "enterTextBox";
            this.enterTextBox.Size = new System.Drawing.Size(1844, 152);
            this.enterTextBox.TabIndex = 3;
            this.enterTextBox.TextChanged += new System.EventHandler(this.enterTextBox_TextChanged);
            // 
            // semanticTextBox
            // 
            this.semanticTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.semanticTextBox.Location = new System.Drawing.Point(15, 621);
            this.semanticTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.semanticTextBox.Multiline = true;
            this.semanticTextBox.Name = "semanticTextBox";
            this.semanticTextBox.Size = new System.Drawing.Size(276, 446);
            this.semanticTextBox.TabIndex = 4;
            // 
            // checkTextBox
            // 
            this.checkTextBox.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkTextBox.Location = new System.Drawing.Point(58, 282);
            this.checkTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.checkTextBox.Multiline = true;
            this.checkTextBox.Name = "checkTextBox";
            this.checkTextBox.Size = new System.Drawing.Size(1844, 194);
            this.checkTextBox.TabIndex = 5;
            // 
            // constBox
            // 
            this.constBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.constBox.Location = new System.Drawing.Point(893, 621);
            this.constBox.Multiline = true;
            this.constBox.Name = "constBox";
            this.constBox.Size = new System.Drawing.Size(240, 446);
            this.constBox.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox2.Location = new System.Drawing.Point(300, 621);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(577, 446);
            this.textBox2.TabIndex = 7;
            // 
            // viewBox
            // 
            this.viewBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.viewBox.Location = new System.Drawing.Point(1139, 621);
            this.viewBox.Multiline = true;
            this.viewBox.Name = "viewBox";
            this.viewBox.Size = new System.Drawing.Size(372, 446);
            this.viewBox.TabIndex = 8;
            // 
            // typeBox
            // 
            this.typeBox.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.typeBox.Location = new System.Drawing.Point(1517, 621);
            this.typeBox.Multiline = true;
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(388, 446);
            this.typeBox.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 567);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 32);
            this.label1.TabIndex = 10;
            this.label1.Text = "Имя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(328, 567);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 32);
            this.label2.TabIndex = 11;
            this.label2.Text = "Вид";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(883, 567);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 32);
            this.label3.TabIndex = 12;
            this.label3.Text = "Значение";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1139, 567);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 32);
            this.label4.TabIndex = 13;
            this.label4.Text = "Вид";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1556, 567);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 32);
            this.label5.TabIndex = 14;
            this.label5.Text = "Тип";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1917, 1097);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.typeBox);
            this.Controls.Add(this.viewBox);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.constBox);
            this.Controls.Add(this.checkTextBox);
            this.Controls.Add(this.semanticTextBox);
            this.Controls.Add(this.enterTextBox);
            this.Controls.Add(this.semanticButton);
            this.Controls.Add(this.analyzeButton);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button analyzeButton;
        private Button semanticButton;
        private TextBox enterTextBox;
        private TextBox semanticTextBox;
        private TextBox checkTextBox;
        private TextBox constBox;
        private TextBox textBox2;
        private TextBox viewBox;
        private TextBox typeBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}