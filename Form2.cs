using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Obiadki
{
    public partial class Form2 : Form
    {
        int numberOfRadioButton;
        public Form2()
        {
            InitializeComponent();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center; 
        }
        public void CheckRadioButton(int radioButtonNumber)
        {
            numberOfRadioButton = radioButtonNumber;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                if (numberOfRadioButton == 1) Form1.Soups.Add(richTextBox1.Text);
                else Form1.Dishes.Add(richTextBox1.Text);
                Form1.isModified = true;
                Close();
            }
            else MessageBox.Show("Nie wprowadzono żadnego dania!");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
