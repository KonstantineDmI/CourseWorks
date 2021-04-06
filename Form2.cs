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
    public partial class Form2 : Form
    {
        SqlConnection sqlConnection;
        
        
        public Form2()
        {
            
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private async void Form2_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\konst\source\repos\Kursovaya\Kursovaya\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT *FROM[Shop]", sqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"])+ "  Название: " + Convert.ToString(sqlReader["Название"] +"  Цена: " + Convert.ToString(sqlReader["Цена"]) +"  Количество: "+ Convert.ToString(sqlReader["Количество"] +" "+ Convert.ToString(sqlReader["формат"]))));
                    
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null) sqlReader.Close();
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if(label5.Visible == true)
            {
                label5.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {

                if (radioButton1.Checked == true)
                {
                    
                    SqlCommand command = new SqlCommand("INSERT INTO [Shop] (Название, Цена, Количество, формат)VALUES(@Название, @Цена, @Количество, @формат)", sqlConnection);
                    command.Parameters.AddWithValue("Название", textBox1.Text);
                    command.Parameters.AddWithValue("Цена", textBox2.Text);
                    command.Parameters.AddWithValue("Количество", textBox7.Text);
                    command.Parameters.AddWithValue("формат", "шт");


                    await command.ExecuteNonQueryAsync();
                }
                if (radioButton2.Checked == true)
                {
                    
                    SqlCommand command = new SqlCommand("INSERT INTO [Shop] (Название, Цена, Количество, формат)VALUES(@Название, @Цена, @Количество, @формат)", sqlConnection);
                    command.Parameters.AddWithValue("Название", textBox1.Text);
                    command.Parameters.AddWithValue("Цена", textBox2.Text);
                    command.Parameters.AddWithValue("Количество", textBox7.Text);
                    command.Parameters.AddWithValue("формат", "м");
                    await command.ExecuteNonQueryAsync();
                }

               

            }
            else
            {
                label5.Visible = true;
                label5.Text = "Поля 'название ' и 'цена ' - должны быть заполнены ! ";
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox7.Clear();



        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT *FROM[Shop]", sqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "  Название: " + Convert.ToString(sqlReader["Название"] + "  Цена: " + Convert.ToString(sqlReader["Цена"]) + "  Количество: " + Convert.ToString(sqlReader["Количество"] + " " + Convert.ToString(sqlReader["формат"]))));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null) sqlReader.Close();
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if(label9.Visible == true)
            {
                label9.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
               !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text)&&
               !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE[Shop] SET [Название] = @Название,[Цена] = @Цена,[Количество] = @Количество WHERE [Id] = @Id", sqlConnection) ;
                command.Parameters.AddWithValue("Id",textBox4.Text);
                command.Parameters.AddWithValue("Название",textBox5.Text);
                command.Parameters.AddWithValue("Цена", textBox6.Text);
                command.Parameters.AddWithValue("Количество",1 );

                await command.ExecuteNonQueryAsync() ;
            }
            else 
            {
                label9.Visible = true;
                label9.Text = "Поля 'ID', 'название ' и 'цена ' - должны быть заполнены !";
            }
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private async void button3_Click(object sender, EventArgs e)
        {

            if (label10.Visible == true)
            {
                label10.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Shop] WHERE [Id]= @Id", sqlConnection);
                command.Parameters.AddWithValue("Id", textBox3.Text);
                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label10.Visible = true;
                label10.Text = "Поле 'Id' должно быть заполнено!";
            }
            textBox3.Clear();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void fAQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("В данный момент, Вы находитесь в режиме администратора, что позволяет Вам редактировать таблицу (добавлять товар, удалять, править). Ввел данные в 'добавить товар' - товар не добавился. Что делать?  --- Инструменты ---> Обновить. Аналогично с удалением товара, обновлением. ","Коротко о том, что здесь происходит", MessageBoxButtons.OK);
        }
    }
}
