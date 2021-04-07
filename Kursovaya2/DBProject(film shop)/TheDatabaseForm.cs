using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DBProject_film_shop_
{
    public partial class TheDatabaseForm : Form
    {
        SqlConnection sqlConnection;
        public TheDatabaseForm()
        {
            InitializeComponent();
        }

        private async void TheDatabaseForm_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\konst\source\repos\DBProject(film shop)\DBProject(film shop)\Database.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            string querty = "SELECT * FROM [Customer]";
            SqlCommand command = new SqlCommand(querty, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();

            List<string[]> customers = new List<string[]>();
            while (reader.Read())
            {

                customers.Add(new string[5]);
                customers[customers.Count - 1][0] = reader[1].ToString();
                customers[customers.Count - 1][1] = reader[2].ToString();
                customers[customers.Count - 1][2] = reader[3].ToString();
                customers[customers.Count - 1][3] = reader[4].ToString();
                customers[customers.Count - 1][3] = reader[5].ToString();
            }
            reader.Close();

            foreach(string[] col in customers)
            {

                dataGridView1.Rows.Add(col);
            }

            for(int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    comboBox1.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    comboBox3.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
                }
            }



            querty = "SELECT * FROM [Customer] ORDER BY [Stats] DESC";
            command = new SqlCommand(querty, sqlConnection);
            reader = command.ExecuteReader();

            List<string[]> cust = new List<string[]>();
            while (reader.Read())
            {

                cust.Add(new string[2]);
                cust[cust.Count - 1][0] = reader[1].ToString();
                cust[cust.Count - 1][1] = reader[5].ToString();


            }
            reader.Close();

            foreach (string[] col in cust)
            {

                dataGridView6.Rows.Add(col);
            }

            querty = "SELECT * FROM [FilmInfo]";
            command = new SqlCommand(querty, sqlConnection);
            reader = command.ExecuteReader();

            List<string[]> films = new List<string[]>();
            while (reader.Read())
            {

                films.Add(new string[8]);
                films[films.Count - 1][0] = reader[1].ToString();
                films[films.Count - 1][1] = reader[3].ToString();
                films[films.Count - 1][2] = reader[5].ToString();
                films[films.Count - 1][3] = reader[2].ToString();
                films[films.Count - 1][4] = reader[4].ToString();
                films[films.Count - 1][5] = reader[6].ToString();
                films[films.Count - 1][6] = reader[7].ToString();
                films[films.Count - 1][7] = reader[8].ToString();

            }
            reader.Close();


            foreach (string[] col in films)
            {

                dataGridView2.Rows.Add(col);
            }

            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                if (dataGridView2.Rows[i].Cells[0].Value != null)
                {
                    
                    comboBox2.Items.Add(dataGridView2.Rows[i].Cells[0].Value.ToString());
                    comboBox4.Items.Add(dataGridView2.Rows[i].Cells[0].Value.ToString());
                }
            }


            command = new SqlCommand("DELETE FROM [TotalReport] WHERE [DateOfTook] <= DATEADD(day, -30, GETDATE())", sqlConnection);
            await command.ExecuteNonQueryAsync();

            querty = "SELECT * FROM [TotalReport]";
            command = new SqlCommand(querty, sqlConnection);
            reader = command.ExecuteReader();

            List<string[]> totalReport = new List<string[]>();
            List<string[]> totalReport1 = new List<string[]>();
            while (reader.Read())
            {

                totalReport.Add(new string[6]);
                totalReport[totalReport.Count - 1][0] = reader[0].ToString();
                totalReport[totalReport.Count - 1][1] = reader[1].ToString();
                totalReport[totalReport.Count - 1][2] = reader[2].ToString();
                totalReport[totalReport.Count - 1][3] = reader[3].ToString();
                totalReport[totalReport.Count - 1][4] = reader[4].ToString();
                totalReport[totalReport.Count - 1][5] = reader[5].ToString();

                totalReport1.Add(new string[6]);
                totalReport1[totalReport1.Count - 1][0] = reader[1].ToString();
                totalReport1[totalReport1.Count - 1][1] = reader[2].ToString();
                totalReport1[totalReport1.Count - 1][2] = reader[3].ToString();
                totalReport1[totalReport1.Count - 1][3] = reader[4].ToString();
                totalReport1[totalReport1.Count - 1][4] = reader[5].ToString();
                totalReport1[totalReport1.Count - 1][5] = reader[6].ToString();



            }
            reader.Close();

            foreach (string[] col in totalReport)
            {
                dataGridView4.Rows.Add(col);
                dataGridView7.Rows.Add(col);


            }
            foreach (string[] col in totalReport1)
            {

                dataGridView5.Rows.Add(col);

            }

            int sum = 0;
            for(int i = 0; i < dataGridView4.RowCount; i++)
            {
                if(dataGridView4.Rows[i].Cells[4].Value != null)
                {
                    if(Convert.ToDateTime(dataGridView4.Rows[i].Cells[3].Value) < DateTime.Now)
                    {
                        for(int j = 0; j < dataGridView2.RowCount; j++)
                        {

                            if(dataGridView2.Rows[j].Cells[0].Value != null && dataGridView2.Rows[j].Cells[0].Value.ToString() == dataGridView4.Rows[i].Cells[1].Value.ToString())
                            {
                                sum = Convert.ToInt32(dataGridView4.Rows[i].Cells[5].Value);
                                sum += (Convert.ToInt32(dataGridView2.Rows[j].Cells[1].Value) * 10) / 100;
                                dataGridView4.Rows[i].Cells[5].Value = sum;


                                command = new SqlCommand("UPDATE[TotalReport] SET [SumToPay] = @SumToPay WHERE [Id] = @Id", sqlConnection);
                                command.Parameters.AddWithValue("SumToPay", sum);
                                command.Parameters.AddWithValue("Id", Convert.ToInt32(dataGridView4.Rows[i].Cells[0].Value));
                                await command.ExecuteNonQueryAsync();
                            }
                        }
                        
                    }
                }
            }
            
            for(int i = 0; i < dataGridView4.RowCount; i++)
            {

                if(dataGridView4.Rows[i].Cells[4].Value != null)
                {
                    
                   
                }
            }



           



        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                comboBox3.Enabled = false;
            }
            else
            {
                

                textBox1.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                comboBox3.Enabled = true;
                textBox1.Clear();
                textBox4.Clear();
                textBox3.Clear();
                textBox5.Clear();
            }
            
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            
            string nameOfCust = null;
            TimeSpan ts = new TimeSpan(120, 0, 0);
            TimeSpan ts1 = dateTimePicker2.Value - dateTimePicker3.Value;
            int totalSum = Convert.ToInt32(ts1.Days) + 1;
            if (textBox1.Text != "")
            {
                nameOfCust = textBox1.Text;
                SqlCommand command = new SqlCommand("INSERT INTO [Customer] ([NameOfCust], [AddressOfCust], [MainWorkSpace], [Phone]) VALUES (@NameOfCust, @AddressOfCust, @MainWorkSpace, @Phone)", sqlConnection);
                command.Parameters.AddWithValue("NameOfCust", textBox1.Text);
                command.Parameters.AddWithValue("AddressOfCust", textBox3.Text);
                command.Parameters.AddWithValue("MainWorkSpace", textBox4.Text);
                command.Parameters.AddWithValue("Phone", textBox5.Text);
                await command.ExecuteNonQueryAsync();
            }
            else
            {
                if(comboBox3.SelectedItem != null)
                     nameOfCust = comboBox3.SelectedItem.ToString();
            }

           

            int cost = 0;
            int razd = 0;
            if (dateTimePicker2.Value - dateTimePicker3.Value <= ts && comboBox4.SelectedItem != null && dateTimePicker3.Value < dateTimePicker2.Value)
            {

                dataGridView4.Rows.Clear();
                dataGridView7.Rows.Clear();
                SqlCommand command = new SqlCommand("INSERT INTO [TotalReport] ([BookName], [DateOfTook], [DateOfBring], [NameOfCust], [SumToPay], [Razd]) VALUES (@BookName, @DateOfTook, @DateOfBring, @NameOfCust, @SumToPay, @Razd)", sqlConnection);
                command.Parameters.AddWithValue("BookName", comboBox4.SelectedItem.ToString());
                command.Parameters.AddWithValue("DateOfTook", dateTimePicker3.Value);
                command.Parameters.AddWithValue("DateOfBring", dateTimePicker2.Value);
                command.Parameters.AddWithValue("NameOfCust", nameOfCust);

                
                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    if (dataGridView2.Rows[i].Cells[0].Value != null)
                    {
                        if (dataGridView2.Rows[i].Cells[0].Value.ToString() == comboBox4.SelectedItem.ToString())
                        {
                            cost = Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value);
                            razd = Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value);
                            break;
                        }
                    }
                }

                command.Parameters.AddWithValue("SumToPay", totalSum * cost);
                command.Parameters.AddWithValue("Razd", razd);
                await command.ExecuteNonQueryAsync();


                command = new SqlCommand(("UPDATE [Customer] SET [Stats] = [Stats] + 1 WHERE [NameOfCust] = @NameOfCust"), sqlConnection);
                command.Parameters.AddWithValue("NameOfCust", nameOfCust);
                await command.ExecuteNonQueryAsync();



                string querty = "SELECT * FROM [TotalReport]";
                command = new SqlCommand(querty, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                List<string[]> totalReport = new List<string[]>();
                while (reader.Read())
                {

                    totalReport.Add(new string[6]);
                    totalReport[totalReport.Count - 1][0] = reader[0].ToString();
                    totalReport[totalReport.Count - 1][1] = reader[1].ToString();
                    totalReport[totalReport.Count - 1][2] = reader[2].ToString();
                    totalReport[totalReport.Count - 1][3] = reader[3].ToString();
                    totalReport[totalReport.Count - 1][4] = reader[4].ToString();
                    totalReport[totalReport.Count - 1][5] = reader[5].ToString();
                    
                }
                reader.Close();

                foreach (string[] col in totalReport)
                {
                    dataGridView4.Rows.Add(col);
                    dataGridView7.Rows.Add(col);

                }
                int sum = 0;
                //for (int i = 0; i < dataGridView4.RowCount; i++)
                //{
                //    if (dataGridView4.Rows[i].Cells[4].Value != null)
                //    {
                //        if (Convert.ToDateTime(dataGridView4.Rows[i].Cells[3].Value) < DateTime.Now)
                //        {
                //            sum = Convert.ToInt32(dataGridView4.Rows[i].Cells[5].Value);
                //            sum += (sum * 50) / 100;
                //            dataGridView4.Rows[i].Cells[5].Value = sum;
                //        }
                //    }
                //}

                


            }
            else
            {
                MessageBox.Show("Проверьте, корректность введенных данных");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //listBox1.Items.Clear();
            //for(int i = 0; i < dataGridView4.RowCount; i++)
            //{
            //    if(dataGridView4.Rows[i].Cells[4].Value != null)
            //    {
            //        if (comboBox1.SelectedItem != null  && dataGridView4.Rows[i].Cells[4].Value.ToString() == comboBox1.SelectedItem.ToString())
            //        {
            //            listBox1.Items.Add(comboBox1.SelectedItem.ToString() + " взял фильм, под названием: " + dataGridView4.Rows[i].Cells[1].Value + ".");

            //        }
            //    }
               
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //listBox1.Items.Clear();
            //for (int i = 0; i < dataGridView4.RowCount; i++)
            //{
            //    if (dataGridView4.Rows[i].Cells[1].Value != null)
            //    {
            //        if (comboBox2.SelectedItem != null && dataGridView4.Rows[i].Cells[1].Value.ToString() == comboBox2.SelectedItem.ToString())
            //        {

            //            listBox1.Items.Add("Информация о фильме " + comboBox2.SelectedItem.ToString() + " :");
            //            listBox1.Items.Add("взят с даты: " + dataGridView4.Rows[i].Cells[2].Value + " по дату: " + dataGridView4.Rows[i].Cells[3].Value);
            //            listBox1.Items.Add(" взят на ФИО - " + dataGridView4.Rows[i].Cells[4].Value + " сумма к оплате: " + dataGridView4.Rows[i].Cells[5].Value + " грн.");
            //            listBox1.Items.Add("");

            //        }
            //    }

            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //listBox1.Items.Clear();
            //for (int i = 0; i < dataGridView4.RowCount; i++)
            //{
            //    if (dataGridView4.Rows[i].Cells[1].Value != null)
            //    {
            //        if (Convert.ToDateTime(dataGridView4.Rows[i].Cells[2].Value) == dateTimePicker1.Value)
            //        {

            //            listBox1.Items.Add("Фильмы, которые были выданы " + dateTimePicker1.Value.ToString() + " :");
            //            listBox1.Items.Add("взяты по дату: " + dataGridView4.Rows[i].Cells[2].Value);
            //            listBox1.Items.Add("взят на ФИО - " + dataGridView4.Rows[i].Cells[4].Value + " сумма к оплате: " + dataGridView4.Rows[i].Cells[5].Value + " грн.");
            //            listBox1.Items.Add("");

            //        }
            //    }

            //}
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("DELETE FROM [TotalReport] WHERE [Id]= @Id", sqlConnection);
            command.Parameters.AddWithValue("Id", textBox2.Text);
            await command.ExecuteNonQueryAsync();

            dataGridView4.Rows.Clear();
            dataGridView7.Rows.Clear();

            string querty = "SELECT * FROM [TotalReport]";
            command = new SqlCommand(querty, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> totalReport = new List<string[]>();
            while (reader.Read())
            {

                totalReport.Add(new string[6]);
                totalReport[totalReport.Count - 1][0] = reader[0].ToString();
                totalReport[totalReport.Count - 1][1] = reader[1].ToString();
                totalReport[totalReport.Count - 1][2] = reader[2].ToString();
                totalReport[totalReport.Count - 1][3] = reader[3].ToString();
                totalReport[totalReport.Count - 1][4] = reader[4].ToString();
                totalReport[totalReport.Count - 1][5] = reader[5].ToString();

            }
            reader.Close();

            foreach (string[] col in totalReport)
            {
                dataGridView4.Rows.Add(col);
                dataGridView7.Rows.Add(col);

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
           
            listBox2.Items.Add("============================================");
            listBox2.Items.Add("Должники:");
            for (int i = 0; i < dataGridView5.RowCount; i++)
            {
                if (dataGridView5.Rows[i].Cells[0].Value != null && Convert.ToDateTime(dataGridView5.Rows[i].Cells[2].Value) < DateTime.Now)
                {
                    TimeSpan ts = DateTime.Now - Convert.ToDateTime(dataGridView5.Rows[i].Cells[2].Value);
                    listBox2.Items.Add("ФИО должника: " + dataGridView5.Rows[i].Cells[3].Value + 
                        "    дней просрочки: " + ts.Days.ToString() + "  сумма долга: " + dataGridView5.Rows[i].Cells[4].Value);

                }
            }
            listBox2.Items.Add("============================================");
        }

        private void button8_Click(object sender, EventArgs e)
        {

            string[] nameOfBook = new string[dataGridView5.RowCount];
            listBox2.Items.Clear();
            listBox2.Items.Add("============================================");


            for (int i = 0; i < dataGridView5.RowCount; i++)
            {
                if (dataGridView5.Rows[i].Cells[0].Value != null)
                {
                    if(textBox6.Text == dataGridView5.Rows[i].Cells[5].Value.ToString())
                    {
                        nameOfBook[i] = dataGridView5.Rows[i].Cells[0].Value.ToString();  
                    }

                }

            }
            
            for(int i = 0; i < dataGridView2.RowCount; i++)
            {
                
                if (dataGridView2.Rows[i].Cells[0].Value != null)
                {
                   
                    if (nameOfBook.Contains(dataGridView2.Rows[i].Cells[0].Value.ToString()))
                    {
                        listBox2.Items.Add(" Название книги:   " + dataGridView2.Rows[i].Cells[0].Value);
                        listBox2.Items.Add(" цена: " + dataGridView2.Rows[i].Cells[1].Value + "       палитурка: " + dataGridView2.Rows[i].Cells[3].Value);
                        listBox2.Items.Add(" кол -во копий:   " + dataGridView2.Rows[i].Cells[4].Value + "       автор: " + dataGridView2.Rows[i].Cells[5].Value);
                        listBox2.Items.Add(" издатель:   " + dataGridView2.Rows[i].Cells[6].Value + "      кол-во страниц: " + dataGridView2.Rows[i].Cells[7].Value);
                    }
                   
                }
            }

           

            
            listBox2.Items.Add("============================================");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int sum = 0;
            listBox2.Items.Clear();
            listBox2.Items.Add("============================================");

            for (int i = 0; i < dataGridView5.RowCount; i++)
            {

                if (dataGridView5.Rows[i].Cells[0].Value != null && textBox7.Text == dataGridView5.Rows[i].Cells[5].Value.ToString())
                {
                    sum += Convert.ToInt32(dataGridView5.Rows[i].Cells[4].Value);

                }
            }
            listBox2.Items.Add("Общий доход с раздела == " + textBox7.Text + " == составляет : " + sum);



            listBox2.Items.Add("============================================");


        }

        private void button10_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            for (int i = 0; i < dataGridView5.RowCount; i++)
            {

                if (dataGridView5.Rows[i].Cells[0].Value != null && Convert.ToDateTime(dataGridView5.Rows[i].Cells[2].Value) > DateTime.Now)
                {
                    TimeSpan ts = Convert.ToDateTime(dataGridView5.Rows[i].Cells[2].Value) - DateTime.Now;
                    listBox2.Items.Add("Название книги: " + dataGridView5.Rows[i].Cells[0].Value + " ФИО клиента: " + dataGridView5.Rows[i].Cells[3].Value +
                        "  до срока сдачи осталось: " + ts.Days.ToString() + " дней");

                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            int j = 0;
            for (int i = 0; i < dataGridView6.RowCount; i++)
            {

                if (dataGridView6.Rows[i].Cells[0].Value != null && j < 3)
                {
                    j++;
                    listBox2.Items.Add(" ФИО читателя: " + dataGridView6.Rows[i].Cells[0].Value + "               кол-во в прокатов: " + dataGridView6.Rows[i].Cells[1].Value);

                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            for (int i = 0; i < dataGridView5.RowCount; i++)
            {

                if (dataGridView5.Rows[i].Cells[0].Value != null && Convert.ToDateTime(dataGridView5.Rows[i].Cells[2].Value) < DateTime.Now &&   comboBox1.SelectedItem != null && dataGridView5.Rows[i].Cells[3].Value.ToString() == comboBox1.SelectedItem.ToString())
                {
                   
                    listBox2.Items.Add("Название книги: " + dataGridView5.Rows[i].Cells[0].Value + " ФИО клиента: " + dataGridView5.Rows[i].Cells[3].Value +
                        "  сумма к оплате: " + dataGridView5.Rows[i].Cells[4].Value);

                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            int totalSum = 0;
            listBox2.Items.Clear();
            for (int i = 0; i < dataGridView5.RowCount; i++)
            {

                if (dataGridView5.Rows[i].Cells[0].Value != null)
                {
                    totalSum += Convert.ToInt32(dataGridView5.Rows[i].Cells[4].Value);
                    

                }
            }
            listBox2.Items.Add("Общая сумма со всех книг составит: " + totalSum);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox2.Items.Add("============================================");
            listBox2.Items.Add("Информация о книгах: ");
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {

                if (dataGridView2.Rows[i].Cells[0].Value != null)
                {

                    listBox2.Items.Add("");
                    listBox2.Items.Add("");
                    listBox2.Items.Add(" Название книги:   " + dataGridView2.Rows[i].Cells[0].Value);
                    listBox2.Items.Add(" цена: " + dataGridView2.Rows[i].Cells[1].Value + "       палитурка: " + dataGridView2.Rows[i].Cells[3].Value);
                    listBox2.Items.Add(" кол -во копий:   " + dataGridView2.Rows[i].Cells[4].Value + "       автор: " + dataGridView2.Rows[i].Cells[5].Value);
                    listBox2.Items.Add(" издатель:   " + dataGridView2.Rows[i].Cells[6].Value + "      кол-во страниц: " + dataGridView2.Rows[i].Cells[7].Value);
                    listBox2.Items.Add("");
                    listBox2.Items.Add("");

                }
            }




            listBox2.Items.Add("============================================");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox2.Items.Add("============================================");
            listBox2.Items.Add("Информация о клиентах: ");
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                if (dataGridView1.Rows[i].Cells[0].Value != null)
                {

                    listBox2.Items.Add("");
                    listBox2.Items.Add("");
                    listBox2.Items.Add(" ФИО:   " + dataGridView1.Rows[i].Cells[0].Value);
                    listBox2.Items.Add(" Адрес: " + dataGridView1.Rows[i].Cells[1].Value);
                    listBox2.Items.Add(" Место работы:   " + dataGridView1.Rows[i].Cells[2].Value);
                    listBox2.Items.Add(" Телефон:   " + dataGridView1.Rows[i].Cells[3].Value);
                    listBox2.Items.Add("");
                    listBox2.Items.Add("");

                }
            }




            listBox2.Items.Add("============================================");
        }
    }
}
