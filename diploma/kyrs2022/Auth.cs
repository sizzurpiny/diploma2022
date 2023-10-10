using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kyrs2022
{
    public partial class Auth : Form
    {   /// <summary>
    /// Форма авторизации является начальной формой при запуске программы. Здесь происходит главные действия при авторизации какого-либо пользователя.
    /// </summary>
        public Auth()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) //переход на форму регистрации
        {
            Registr au = new Registr();
            au.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e) //кнопка авторизации
        {
            SqlConnection connection = new SqlConnection(DateBase.connStr);
            try
            {
                connection.Open();
            Registr reg = new Registr();
            passtext.Text = reg.GetHash(passtext.Text);
            SqlDataAdapter adapter = new SqlDataAdapter($@"SELECT [Pass], [User].Id_USER, [Name_User], [Secname_user], [Email],  [PhoneNum], [Active], [Role] FROM [User] WHERE Email = '{emailtext.Text}'", connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

                DateBase.Id_user = ds.Tables[0].Rows[0].Field<int>(ds.Tables[0].Columns[1]);
                DateBase.Name_User = ds.Tables[0].Rows[0].Field<string>(ds.Tables[0].Columns[2]);
                DateBase.Secname_user = ds.Tables[0].Rows[0].Field<string>(ds.Tables[0].Columns[3]);
                DateBase.Email = ds.Tables[0].Rows[0].Field<string>(ds.Tables[0].Columns[4]);
                DateBase.Phonenum = ds.Tables[0].Rows[0].Field<string>(ds.Tables[0].Columns[5]); 
                DateBase.Active = ds.Tables[0].Rows[0].Field<int>(ds.Tables[0].Columns[6]);
                DateBase.Role = ds.Tables[0].Rows[0].Field<string>(ds.Tables[0].Columns[7]);

                List<String> MassPass = new List<String>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    MassPass.Add(row.Field<string>("Pass"));
                }
                if (MassPass[0] == passtext.Text)
                {
                    if (DateBase.Role == "Пользователь")
                    {
                        LC useri = new LC(DateBase.Name_User + " " + DateBase.Secname_user + " " + DateBase.Email);
                        useri.Show();
                        Hide();
                    }
                    if (DateBase.Role == "Админ")
                    {
                        Admin useri = new Admin(DateBase.Name_User + " " + DateBase.Secname_user + " " + DateBase.Email);
                        useri.Show();
                        Hide();
                    }    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильно указаны данные пользователя");
            }
            finally
            {
                connection.Close();
            }

        }

        private void Auth_Load(object sender, EventArgs e)
        {

            passtext.UseSystemPasswordChar = true;

        }

        private void emailtext_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaPictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            passtext.UseSystemPasswordChar = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            passtext.UseSystemPasswordChar = true;
            pictureBox3.Visible = false;
            pictureBox2.Visible = true;
        }
    }
}
