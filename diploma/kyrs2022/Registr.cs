using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kyrs2022
{
    public partial class Registr : Form
    {
        /// <summary>
        /// регистрация пользователя - начало использования программы
        /// </summary>
        public Registr()
        {
            InitializeComponent();
        }
        public string GetHash(string parol)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(parol));

            return Convert.ToBase64String(hash);
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Auth au = new Auth();
            au.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e) //кнопка регистрации
        {
            int pop = emailtext.Text.Split('@').Length;
            if (emailtext.Text.Contains("@") && emailtext.Text.Contains(".") && emailtext.Text.Split('@').Length == 2 )
            {
                if (passtext.Text.Length >= 8)
                {
                    if (passtext.Text == passtextagain.Text)
                    {
                        if (nametext.Text.Length != 0 && secnametext.Text.Length != 0 && phonetext.Text.Length == 11 )
                        {
                            SqlConnection connection = new SqlConnection(DateBase.connStr);
                            try
                            {
                                    connection.Open();
                                    passtext.Text = GetHash(passtext.Text);
                                    SqlCommand cmd = new SqlCommand($"INSERT INTO [User] (Name_User, Secname_user, Email, Pass, PhoneNum, Active, Role) "
                                        + $"VALUES ('{nametext.Text}', '{secnametext.Text}', '{emailtext.Text}', '{passtext.Text}', '{phonetext.Text}','{0}','{"Пользователь"}')", connection);
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Регистрация прошла успешно!");
                                    Auth auth = new Auth();
                                    auth.Show();
                                    this.Hide();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                connection.Close();
                            }

                        }
                        else 
                        {
                            MessageBox.Show("Вы оставили данные незаполненными");
                        }
                    }
                    else 
                    {
                        MessageBox.Show("Вы неправильно ввели повторный пароль");
                    } 
                }
                else 
                {
                    MessageBox.Show("Недействительный Пароль");
                }
            }
            else 
            {
                MessageBox.Show("Недействительный Емейл");
            }
        }

        private void Registr_Load(object sender, EventArgs e)
        {
            passtext.UseSystemPasswordChar = true;
            passtextagain.UseSystemPasswordChar = true;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void gunaLineTextBox4_TextChanged(object sender, EventArgs e)
        {
            phonetext.Text = gunaLineTextBox4.Text;
            gunaLineTextBox4.Text = phonetext.Text;
        }

        private void phonetext_TextChanged(object sender, EventArgs e)
        {
            gunaLineTextBox4.Text = phonetext.Text;
        }

        private void gunaPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
