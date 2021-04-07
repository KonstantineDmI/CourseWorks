using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Kursovaya
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            
        }
        SqlConnection sqlConnection = new SqlConnection();

        private async void Form3_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\konst\source\repos\Kursovaya\Kursovaya\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            string querty = "SELECT * FROM Shop ORDER BY Id";
            SqlCommand command = new SqlCommand(querty, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read()) 
            {
                data.Add(new string[5]);
                data[data.Count - 1][0]= reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();

            }
            reader.Close();
            sqlConnection.Close();
            foreach(string[]s in data)
            {
                dataGridView1.Rows.Add(s);
            }
           
        }
        float sum = 0;
        float sumperc = 0;
        float f = 0;
        
        
        private void button5_Click(object sender, EventArgs e)
        {
            
            
            for ( int i = 0; i < dataGridView1.RowCount; i++)
            {
                
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {


                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {


                        if (textBox8.Text == dataGridView1.Rows[i].Cells[1].Value.ToString())
                        {
                            
                            if(Convert.ToInt32(textBox7.Text) < 10)
                            {
                                sum += (Convert.ToInt32(textBox7.Text) * Convert.ToInt32(dataGridView1.Rows[i].Cells[1 + 1].Value));
                                listBox2.Items.Add("Товар :  " + dataGridView1.Rows[i].Cells[1].Value.ToString() + ".|   Цена за кол-во  " + Convert.ToInt32(textBox7.Text) + "  шт/м составляет : " + Convert.ToInt32(textBox7.Text) * Convert.ToInt32(dataGridView1.Rows[i].Cells[1 + 1].Value) + "  грн.");
                               
                            }
                            else if(Convert.ToInt32(textBox7.Text) >= 10)
                            {
                                
                                float perc = ((Convert.ToInt32(textBox7.Text) * Convert.ToInt32(dataGridView1.Rows[i].Cells[1 + 1].Value))*10) /100;
                                float res = (Convert.ToInt32(textBox7.Text) * Convert.ToInt32(dataGridView1.Rows[i].Cells[1 + 1].Value) - perc);
                                sumperc += res;
                                listBox2.Items.Add("Товар :  " + dataGridView1.Rows[i].Cells[1].Value.ToString() + ".|   Цена за кол-во  " + Convert.ToInt32(textBox7.Text) + "  шт/м составляет : " + res + "  грн.");
                               


                            }
                           
                            break;
                        }                    
                    }                
                }
            }
            textBox8.Clear();
            textBox7.Clear();
            
            f = sum + sumperc;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вернхняя таблица - товар, который есть. Нижняя таблица - Ваша корзина, куда падают сделаные заказы. В textBox'ы вводим соответсвенно название товара, который хотим купить, и его количествою. Если товара нету - сообщения не будет, и в 'чек' ничего не добавится. Кнопка 'печать', что ниже меня - отвечает за распечатку 'чека'. ", "Немного о том, что тут происходит", MessageBoxButtons.OK);
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }
        Bitmap bitmap;
        private void button1_Click_1(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            bitmap = new Bitmap(this.Size.Width, this.Size.Height, g);
            Image im = bitmap;
            e.Graphics.DrawString("Чек", new Font("Arial", 40, FontStyle.Italic), Brushes.Black, new Point(350, 50));
            int lng = 120;
            foreach (var item in listBox2.Items)
            {
               
                
                e.Graphics.DrawString(Convert.ToString(item), new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(150, lng));
                lng += 30;
                
            }
            
            
            e.Graphics.DrawString("Итого : "+f+" грн.", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(150, lng+30));
            Pen p = new Pen(Color.Green, 5);
            e.Graphics.DrawRectangle(p, 150, lng+30, 200, 30);



        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            try
            {
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Выделите товар, который хотите удалить из корзины!","Внимание!", MessageBoxButtons.OK);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
