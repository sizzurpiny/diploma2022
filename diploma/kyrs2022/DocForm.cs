using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace kyrs2022
{
    public partial class DocForm : Form
    {/// <summary>
    /// создание документа в docx формате по фаблону который находится в папке Resursces, под названием documentas
    /// </summary>
        public DocForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //кнопка создания документа 
        {
            string p = "";
            string o = "";
            if (radioButton1.Checked)
            {
                p = radioButton1.Text;
            }
            else {
                p = radioButton2.Text;
            }
            if (radioButton1.Checked)
            {
                o = radioButton3.Text;
            }
            else
            {
                o = radioButton4.Text;
            }

            if (Convert.ToInt32(date1.Text) > 2000 || Convert.ToInt32(date2.Text) > 2000 || Convert.ToInt32(date1.Text) < 2025 || Convert.ToInt32(date2.Text) < 2025)
            {


                var helper = new WordHelper(@"C:\Users\Admin\Desktop\kyrs2022\kyrs2022\docs\documentas.docx");
                var items = new Dictionary<string, string>
                {
                    { "<name>", name.Text + " " + secname.Text + " " + thirdname.Text },
                    { "<spec>" , spec.Text },
                    { "<forma>",  p},
                    { "<obsh>", o},
                    { "<data1>", date1.Text},
                    { "<data2>", date2.Text},
                };
                helper.Proccess(items);
            }
            else MessageBox.Show("Не верная дата");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void date11_TextChanged(object sender, EventArgs e)
        {
            date1.Text = date11.Text;
            date11.Text = date1.Text;
        }

        private void date22_TextChanged(object sender, EventArgs e)
        {
            date2.Text = date22.Text;
            date22.Text = date2.Text;
        }

        private void gunaButton1_Click(object sender, EventArgs e) // переадресация на форму панели администратора
        {
            Admin ad = new Admin(DateBase.Name_User + " " + DateBase.Secname_user + " " + DateBase.Email);
            ad.Show();
            this.Hide();
        }
    }
}
