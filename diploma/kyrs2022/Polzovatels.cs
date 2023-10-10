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
using Excel = Microsoft.Office.Interop.Excel;

namespace kyrs2022
{
    public partial class Polzovatels : Form
    {
        /// <summary>
        /// форма добавления/изменения/удаления пользователей, а так же их выгрузка в xls формат
        /// </summary>
        public Polzovatels()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e) //переход на форму панели администратора
        {
            Admin ad = new Admin(DateBase.Name_User + " " + DateBase.Secname_user + " " + DateBase.Email);
            ad.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e) //добавить
        {
            try
            {
                int pop = emailtext.Text.Split('@').Length;
                if (emailtext.Text.Contains("@") && emailtext.Text.Contains(".") && emailtext.Text.Split('@').Length == 2)
                {
                    if (passtext.Text.Length >= 8)
                    {
                        {
                            if (nametext.Text.Length != 0 && secnametext.Text.Length != 0 && numtext.Text.Length == 11)
                            {
                                SqlConnection connection = new SqlConnection(DateBase.connStr);
                                try
                                {
                                    connection.Open();
                                    Registr rg = new Registr();
                                    passtext.Text = rg.GetHash(passtext.Text);
                                    SqlCommand cmd = new SqlCommand($"INSERT INTO [User] (Name_User, Secname_user, Email, Pass, PhoneNum, Active, Role) "
                                        + $"VALUES ('{nametext.Text}', '{secnametext.Text}', '{emailtext.Text}', '{passtext.Text}', '{numtext.Text}','{0}','{roletext.SelectedItem}')", connection);
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Добавление прошло успешно!");

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    obnov(sender);
                                    passtext.Text = "";
                                    connection.Close();
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)//изменить
        {

            SqlConnection connection = new SqlConnection(DateBase.connStr);
            try
            {
                Registr reg = new Registr();
                connection.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE [User] SET [Name_User] = '{nametext.Text}',[Secname_user]= '{secnametext.Text}',[Email]= '{emailtext.Text}', [Phonenum]= '{numtext.Text}', [Role]= '{roletext.SelectedItem}' WHERE [Id_USER] = '{idtext.Text}'", connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Изменение прошло успешно!");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                obnov(sender);
                connection.Close();
            }
        }

        private void Polzovatels_Load(object sender, EventArgs e) //заполнение датагрида из базы данных
        {
            SqlConnection connection = new SqlConnection(DateBase.connStr);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter($@"SELECT [User].Id_USER, [Name_User], [Secname_user], [Email], [Pass], [PhoneNum], [Role] FROM [User]", connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.DataSource = ds.Tables[0];
            connection.Close();

            roletext.Text = "Пользователь";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e) // заполнение текстбоксов после нажатия на одну из ячеек датагрида
        {
            try
            {
                idtext.Text = dataGridView.Rows[dataGridView.CurrentRow.Index].Cells[0].Value.ToString();
                nametext.Text = dataGridView.Rows[dataGridView.CurrentRow.Index].Cells[1].Value.ToString();
                secnametext.Text = dataGridView.Rows[dataGridView.CurrentRow.Index].Cells[2].Value.ToString();
                emailtext.Text = dataGridView.Rows[dataGridView.CurrentRow.Index].Cells[3].Value.ToString();
                numtext.Text = dataGridView.Rows[dataGridView.CurrentRow.Index].Cells[5].Value.ToString();
                roletext.Text = dataGridView.Rows[dataGridView.CurrentRow.Index].Cells[6].Value.ToString();
            }
            catch { }

        }
        public void obnov(object sender) //метод обновления данных в датагриде после внесения каких-либо поправок
        {
            SqlConnection connection = new SqlConnection(DateBase.connStr);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter($@"SELECT [User].Id_USER, [Name_User], [Secname_user], [Email], [Pass], [PhoneNum], [Role] FROM [User]", connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.DataSource = ds.Tables[0];
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e) //удаление
        { 
        SqlConnection connection = new SqlConnection(DateBase.connStr);
            try
            {
                
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter($@"DELETE FROM [User] WHERE Id_user = '{idtext.Text}'", connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                obnov(sender);
            }
        }

        private void button5_Click(object sender, EventArgs e) //выгрузка из базы данных в xls формат
        {
            object pop = "efkve";
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            //Книга.
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            //Таблица.
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            ExcelApp.Cells[1, 1] = "Id";
            ExcelApp.Cells[1, 2] = "Имя";
            ExcelApp.Cells[1, 3] = "Фамилия";
            ExcelApp.Cells[1, 4] = "Почта";
            ExcelApp.Cells[1, 5] = "Пароль";
            ExcelApp.Cells[1, 6] = "Телефон";
            ExcelApp.Cells[1, 7] = "Роль";
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                
                for (int j = 0; j < dataGridView.ColumnCount; j++)
                {
                    ExcelApp.Cells[i + 1, j + 2].Interior.Color = Excel.XlRgbColor.rgbRed;
                    ExcelApp.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value;
                }
                ExcelApp.Cells[i + 1, 1].Interior.Color = Excel.XlRgbColor.rgbPurple;
            }
            for (int p = 1; p <= 7; p++)
            {
                ExcelApp.Cells[1, p].Interior.Color = Excel.XlRgbColor.rgbAqua;
            }

            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;
        }

        private void numtext1_TextChanged(object sender, EventArgs e)
        {
            numtext.Text = numtext1.Text;
            numtext1.Text = numtext.Text;
        }
    }
}
