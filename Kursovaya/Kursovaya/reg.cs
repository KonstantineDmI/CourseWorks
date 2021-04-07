using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kursovaya.Properties;

namespace Kursovaya
{
    public partial class reg : Form
    {
        
        public reg()
        {
             

        InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            
            while (label2.Text == "")
            {
                label2.Text = textBox1.Text;
                if (label2.Text == "")
                {
                    MessageBox.Show("Введите пароль для регистрации!", "Внимание", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Вы успешно зарегистрировались", "Внимание", MessageBoxButtons.OK);
                    Properties.Settings.Default.pass = label2.Text;
                    Properties.Settings.Default.Save();
                    Form1 f1 = new Form1();
                    f1.Show();
                    this.Hide();

                }
            }

            if (button3.Text == "Войти")
            {
                
                if (textBox1.Text != label2.Text)
                {
                    MessageBox.Show("Вы ввели неправильный пароль", "Внимание", MessageBoxButtons.OK);
                }
                else
                {
                    Form1 f1 = new Form1();
                    f1.Hide();
                    this.Hide();
                    MessageBox.Show("Вы успешно вошли", "Внимание", MessageBoxButtons.OK);
                    Form2 f2 = new Form2();
                    f2.Show();
                    
                }

            }







        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
           
        }

       private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                button3.Enabled = false;
            }
            else
            {
                button3.Enabled = true;
            }
        }

        private void label2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void reg_Load(object sender, EventArgs e)
        {
            label2.Text = Properties.Settings.Default.pass;
            Properties.Settings.Default.Save();

            if (label2.Text != "")
            {
                button3.Text = "Войти";
            }
            else
            {
                button3.Text = "Регистрация";
            }
        }
    }
}
