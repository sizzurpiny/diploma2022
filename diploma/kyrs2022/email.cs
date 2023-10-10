using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kyrs2022
{
    public partial class email : Form
    {/// <summary>
    /// отправка сообщения на емейл путем введения адреса пользователем со статической административной почты.
    /// </summary>
        public email()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("fun.stim@yandex.ru");
                mail.To.Add(new MailAddress(textemail.Text));
                mail.Subject = texttema.Text;
                mail.Body = textdiscription.Text;

                SmtpClient client = new SmtpClient();
                client.Host = "smtp.yandex.ru";
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("fun.stim@yandex.ru", "GFulg#MLG");
                client.Send(mail);

                MessageBox.Show("Сообщение успешно отправлено.");
                textdiscription.Text = "";
                textemail.Text = "";
                texttema.Text = "";
            }
            catch
            {
                MessageBox.Show("Произошла ошибка в отправке сообщения. Проверьте правильность написания Емейл адреса");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Admin ad = new Admin(DateBase.Name_User + " " + DateBase.Secname_user + " " + DateBase.Email);
            ad.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void texttema_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
