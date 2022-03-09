using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Obiadki
{
    public partial class Form1 : Form
    {
        public static List<string> Soups = new List<string>();
        public static List<string> Dishes = new List<string>();
        public static bool isModified = false;
        public Form1()
        {
            InitializeComponent();
            Soups = DownloadMealFromFile("zupy.txt");
            Dishes = DownloadMealFromFile("drugiedania.txt");
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox2.SelectionAlignment = HorizontalAlignment.Center;
            radioButton1.Checked = true;
            ShowMeals();

        }
        List<string> DownloadMealFromFile(string fileName)
        {
            try
            {
                List<string> meals = new List<string>();
                StreamReader file = new StreamReader(fileName);
                while (!file.EndOfStream)
                {
                    meals.Add(file.ReadLine());
                }
                file.Close();
                return meals;
            }
            catch (IOException)
            {
                MessageBox.Show("Problem z plikiem lub jego lokalizacją!");
                return null;
            }
        }
        void ShowMeals()
        {
            SortLists();
            List<string> meals;
            if (radioButton2.Checked) meals = Dishes;
            else meals = Soups;
            if (meals.Count <= 0) richTextBox3.Text = null;
            string dishesToText = meals[0];
            for (int i = 1; i < meals.Count; i++)
                dishesToText += "\n" + meals[i];
            richTextBox3.Text = dishesToText;
        }
        
        string DrawMeal(List<string> listOfMeals)
        {           
                Random r = new Random();
                int rInt = r.Next(0, listOfMeals.Count);
                return listOfMeals[rInt];
        }
        void AddMealWindow()
        {
            Form2 add = new Form2();
            if (radioButton2.Checked) add.CheckRadioButton(2);
            else add.CheckRadioButton(1);
            add.ShowDialog();
            ShowMeals();
        }
        void RemoveMeal()
        {
            if (richTextBox3.SelectedText.Length == 0)
            {
                MessageBox.Show("By usunąć danie pierwsze zaznacz całą nazwę w polu obok!");
                return;
            }
            if (radioButton2.Checked)
            {
                if (!Dishes.Contains(richTextBox3.SelectedText))
                    MessageBox.Show("Nie zaznaczono dania bądź zaznaczono tylko jego część");
                else Dishes.Remove(richTextBox3.SelectedText);
            }
            else
            {
                if (!Soups.Contains(richTextBox3.SelectedText))
                    MessageBox.Show("Nie zaznaczono dania bądź zaznaczono tylko jego część");
                else Soups.Remove(richTextBox3.SelectedText);
            }
            isModified = true;
            ShowMeals();
        }
        void SaveMeals(List<string> meals, string fileName)
        {
            try
            {
                StreamWriter file = new StreamWriter(fileName);
                foreach (string meal in meals)
                    file.WriteLine(meal);
                file.Close();
            }
            catch (IOException)
            {
                MessageBox.Show("Problem z plikiem lub jego lokalizacją!");
            }
        }
        void SortLists()
        {
            Soups.Sort();
            Dishes.Sort();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = DrawMeal(Soups);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = DrawMeal(Dishes);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowMeals();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddMealWindow();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RemoveMeal();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isModified) return;
            SortLists();
            SaveMeals(Soups, "zupy.txt");
            SaveMeals(Dishes, "drugiedania.txt");
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Najpierw zaznacz danie które chcesz usunąć następnie wciśnij ten przycisk", button5);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ShowMeals();
        }
    }
}
