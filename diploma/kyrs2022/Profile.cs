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
    public partial class Profile : Form
    {
        /// <summary>
        /// профиль пользователя. Нирчем не отличается от личного кабинета, за исключением возможности изменить пароль пользователю
        /// </summary>
        public Profile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Auth au = new Auth();
            au.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MoreInfo au = new MoreInfo();
            au.Show();
            this.Hide();
        }

        private void Profile_Load(object sender, EventArgs e) 
        {

            SqlConnection connection = new SqlConnection(DateBase.connStr);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter($@"SELECT [Time_date].Id_Time_date, [maname], [rekom] FROM [Time_date] WHERE maname = '{LC.nameuser + " " + LC.secname}'", connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DataSource = ds.Tables[0];
            try
            {
                if (dataGridView1.Rows[0].Cells[0].Value != null)
                {
                    button6.Visible = true;
                }
            }
            catch { }
            connection.Close();

            nametext.Text = DateBase.Name_User;
            sectext.Text = DateBase.Secname_user;
            emailtext.Text = DateBase.Email;

            

        }

        private void button5_Click(object sender, EventArgs e)//изменение пароля
        {
            SqlConnection connection = new SqlConnection(DateBase.connStr);
            try
            {
                Registr reg = new Registr();
                connection.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE [User] Set [Pass] = '{reg.GetHash(passtext.Text)}' WHERE [Email] = '{emailtext.Text}'", connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Изменение прошло успешно!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally {
                connection.Open();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TestForm au = new TestForm();
            au.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Rec au = new Rec();
            au.Show();
            this.Hide();
        }
    }
}
