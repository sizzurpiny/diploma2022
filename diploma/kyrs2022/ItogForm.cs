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
    public partial class ItogForm : Form
    {/// <summary>
    /// Форма подытоживания респондента после его ответов на все вопросы.
    /// </summary>
        public ItogForm()
        {
            InitializeComponent();
        }

        private void ItogForm_Load(object sender, EventArgs e)//код баллирования и заполнения диаграммы и конечный ее вывод вместе с рекомендацией, а так же запись всех данных в базу данных
        {
            int[] nums = { Itog.Progg, Itog.Manager, Itog.Vrach, Itog.Inginer, Itog.Psiholog, Itog.Urist, Itog.Designer };

            int temp;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] > nums[j])
                    {
                        temp = nums[i];
                        nums[i] = nums[j];
                        nums[j] = temp;
                    }
                }
            }





            chart1.Series["Graf"].Points.AddXY("Программист", Itog.Progg);
            chart1.Series["Graf"].Points.AddXY("Менеджер", Itog.Manager);
            chart1.Series["Graf"].Points.AddXY("Врач", Itog.Vrach);
            chart1.Series["Graf"].Points.AddXY("Дизайнер", Itog.Designer);
            chart1.Series["Graf"].Points.AddXY("Психолог", Itog.Psiholog);
            chart1.Series["Graf"].Points.AddXY("Юрист", Itog.Urist);

                if (nums[nums.Length - 1] == Itog.Progg)
                {
                    label.Text = "Программист";
                }
            if (nums[nums.Length - 1] == Itog.Manager)
            {
                label.Text = "Менеджер";
            }
            if (nums[nums.Length - 1] == Itog.Vrach)
            {
                label.Text = "Врач";
            }
            if (nums[nums.Length - 1] == Itog.Inginer)
            {
                label.Text = "Инженер";
            }
            if (nums[nums.Length - 1] == Itog.Psiholog)
            {
                label.Text = "Психолог";
            }
            if (nums[nums.Length - 1] == Itog.Urist)
            {
                label.Text = "Юрист";
            }
            if (nums[nums.Length - 1] == Itog.Designer)
            {
                label.Text = "Дизайнер";
            }

            if (nums[nums.Length - 2] == Itog.Progg && label.Text != "Программист")
            {
                label2.Text = "Программист";
            }
            if (nums[nums.Length - 2] == Itog.Manager && label.Text != "Менеджер")
            {
                label2.Text = "Менеджер";
            }
            if (nums[nums.Length - 2] == Itog.Vrach && label.Text != "Врач")
            {
                label2.Text = "Врач";
            }
            if (nums[nums.Length - 2] == Itog.Inginer && label.Text != "Инженер")
            {
                label2.Text = "Инженер";
            }
            if (nums[nums.Length - 2] == Itog.Psiholog && label.Text != "Психолог")
            {
                label2.Text = "Психолог";
            }
            if (nums[nums.Length - 2] == Itog.Urist && label.Text != "Юрист")
            {
                label2.Text = "Юрист";
            }
            if (nums[nums.Length - 2] == Itog.Designer && label.Text != "Дизайнер")
            {
                label2.Text = "Дизайнер";
            }
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("fun.stim@yandex.ru");
                mail.To.Add(new MailAddress(DateBase.Email));
                mail.Subject = "Здравствуйте";
                mail.Body = $"Здравствуйте, вы прошли наш тест на профориентацию на специальность {label.Text}.Отпишите нам, если вас интересует данная специальность. Если она вам интересна, мы найдем подходящее вам обучение с последующей работой. \n";

                SmtpClient client = new SmtpClient();
                client.Host = "smtp.yandex.ru";
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("fun.stim@yandex.ru", "GFulg#MLG");
                client.Send(mail);
            }
            catch {
                MessageBox.Show("Вы не подключены к интернету. Сообщение на почту вам не придет.");
            }


            SqlConnection connection = new SqlConnection(DateBase.connStr);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand($"INSERT INTO [Time_date] (maname, Rekom) "
                    + $"VALUES ('{DateBase.Name_User + ' ' + DateBase.Secname_user}', '{label.Text}')", connection);
                cmd.ExecuteNonQuery();

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

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Profile lc = new Profile();
            lc.Show();
            this.Hide();
        }
    }
}
