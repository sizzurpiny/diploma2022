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
    /// <summary>
    ///    Панель администратора
    ///    Представлены 4 функции: 
    ///    Отправка письма на электронную почту, 
    ///    Выгрузка тестирований в Excel xls формате, 
    ///    Изменение/Удаление/Добавление данных пользователей, 
    ///    так же их выгрузка в xls формат и Создание документа, в котором возможно создать документ об просьбе директору Московского Приборостроительного Техникума.
    /// </summary>
    public partial class Admin : Form 
    {
        public static string nameuser;
        public static string secname;
        public static string email;
        public Admin(string pop)
        {
            InitializeComponent();

            nameuser = pop.Split(' ')[0];
            secname = pop.Split(' ')[1];
            email = pop.Split(' ')[2];
            //загрузка данных прохождения тестирований
            SqlConnection connection = new SqlConnection(DateBase.connStr);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter($@"SELECT [Time_date].Id_Time_date, [maname], [Rekom] FROM [Time_date]", connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DataSource = ds.Tables[0];
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e) //выход из аккаунта
        {
            Auth au = new Auth();
            au.Show();
            this.Hide();
        }

        private void Admin_Load(object sender, EventArgs e) //загрузка приветствия, в зависимости от времени суток
        {
            DateTime now = DateTime.Now;
            if (now.Hour >= 0 && now.Hour < 6)
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


            namelabel.Text = DateBase.Name_User;
            secnameuser.Text = DateBase.Secname_user;

            namefull.Text = namelabel.Text + " " + secnameuser.Text;
        }

        private void button3_Click(object sender, EventArgs e) //кнопка перехода на форму добавления пользователей
        {
            Polzovatels ps = new Polzovatels();
            ps.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e) //выгрузка тестирований
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            //Книга.
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            //Таблица.
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            ExcelApp.Cells[1, 1] = "Id";
            ExcelApp.Cells[1, 2] = "Имя";
            ExcelApp.Cells[1, 3] = "Рекомендация";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    ExcelApp.Cells[i + 1, j + 2].Interior.Color = Excel.XlRgbColor.rgbRed;
                    ExcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
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

        private void button4_Click(object sender, EventArgs e) //переход на форму отправки сообщения через емейл
        {
            email em = new email();
            em.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e) //переход на форму создания документа
        {
            DocForm em = new DocForm();
            em.Show();
            this.Hide();
        }
    }
}
