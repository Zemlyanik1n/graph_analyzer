using System.Security.Cryptography.X509Certificates;

namespace Analyzer
{
    public partial class Form1 : Form
    {
        bool a = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void analyzeButton_Click(object sender, EventArgs e)
        {
            var str = enterTextBox.Text.ToUpper();
            a = Analyzer.Check(str, out string res, out int i);
            enterTextBox.Focus();
            enterTextBox.SelectionStart = i;
            checkTextBox.Text = res;
            if (a) 
            {
                semanticButton.Enabled = true;
                semanticButton.Visible = true;
            }
        }

        private void semanticButton_Click(object sender, EventArgs e)
        {
            semanticTextBox.Clear();
            textBox2.Clear();
            constBox.Clear();
            viewBox.Clear();
            typeBox.Clear();
            for (int v = 0; v < Analyzer.Ids.Count; v++)
            {
                semanticTextBox.Text += Analyzer.Ids[v].ToString() + "\r\n";
                textBox2.Text += Analyzer.IdsType[v].ToString() + "\r\n";
            }

            for (int v = 0; v < Analyzer.Cnts.Count; v++)
            {
                constBox.Text += Analyzer.Cnts[v].ToString() + "\r\n";
                viewBox.Text += Analyzer.CntsView[v].ToString() + "\r\n";
                typeBox.Text += Analyzer.CntsType[v].ToString() + "\r\n";
            }
        }

        private void enterTextBox_TextChanged(object sender, EventArgs e)
        {
            semanticButton.Visible = false;
            semanticButton.Enabled = false;
            semanticTextBox.Clear();
            textBox2.Clear();
            constBox.Clear();
            viewBox.Clear();
            typeBox.Clear();
        }
    }
}