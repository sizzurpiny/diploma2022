using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kyrs2022
{
    public partial class LC : Form
    {
        /// <summary>
        /// Это форма пользователя
        /// Представлен ряд функционала, который возможен пользователю, а именно: 
        /// Получить больше информации об профессиях, 
        /// пройти тест на определение профессиональной ориентации, 
        /// открыть свой профиль и получить рекомендации (открыть форму с запарсенными данными университетов, которые представлены в зависимости от результата тестирования).
        /// </summary>

        public static string nameuser;
        public static string secname;
        public static string email;
        

        public LC(string pop)
        {
            InitializeComponent();
            nameuser = pop.Split(' ')[0];
            secname = pop.Split(' ')[1];
            email = pop.Split(' ')[2];
        }
        private void label2_Click(object sender, EventArgs e)
        {
            Auth au = new Auth();
            au.Show();
            this.Show();
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)//выход из аккаунта
        {
            Auth au = new Auth();
            au.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)//переход в профиль 
        {
            Profile au = new Profile();
            au.Show();
            this.Hide();
        }

        private void LC_Load(object sender, EventArgs e)//загрузка в модель из базы данных и приветствие пользователя в зависимости от времени суток
        {
            DateTime now = DateTime.Now;
            if (now.Hour >= 0 && now.Hour <6)
            {
                labelTime.Text = "Доброй ночи,";
            }
            if (now.Hour >= 6 && now.Hour < 12)
            {
                labelTime.Text = "Доброе утро,";
            }
            if (now.Hour >= 12 && now.Hour < 18)
            {
                labelTime.Text = "Добрый день,";
            }
            if (now.Hour >= 18 && now.Hour <= 24)
            {
                labelTime.Text = "Добрый вечер,";
            }


            namelabel.Text = nameuser;
            seclabel.Text = secname;

            fullname.Text = namelabel.Text + " " + seclabel.Text;

            SqlConnection connection = new SqlConnection(DateBase.connStr);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter($@"SELECT [Time_date].Id_Time_date, [maname], [rekom] FROM [Time_date] WHERE maname = '{nameuser + " " + secname}'", connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DataSource = ds.Tables[0];
            try
            {
                if (dataGridView1.Rows[0].Cells[0].Value != null)
                {
                    button5.Visible = true;
                }
            }
            catch { }
            connection.Close();





        }

        private void button4_Click(object sender, EventArgs e)
        {
            MoreInfo au = new MoreInfo();
            au.Show();
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            TestForm au = new TestForm();
            au.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Rec au = new Rec();
            au.Show();
            this.Hide();
        }
    }
}
