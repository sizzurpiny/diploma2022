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
    public partial class TestForm : Form
    {

        /// <summary>
        /// тестирование - главное для чего эта программа предназначена
        /// Путем тестирования выявляются наклонности к той или иной специальности
        /// </summary>
        int count = 0;
        int id = 0;

        public TestForm()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //баллирование
        {
            if (id != 0)
            {
                switch (testData.Right)
                {
                    case 1:
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques1)
                        {
                            Itog.Progg += 15;
                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques2)
                        {
                            Itog.Progg += 10;
                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques3)
                        {
                            Itog.Progg += 5;

                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques4)
                        {
                            Itog.Progg += 0;
                        }
                        break;
                    case 2:
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques1)
                        {
                            Itog.Manager += 15;
                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques2)
                        {
                            Itog.Manager += 10;
                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques3)
                        {
                            Itog.Manager += 5;

                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques4)
                        {
                            Itog.Manager += 0;
                        }
                        break;
                    case 3:
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques1)
                        {
                            Itog.Vrach += 15;
                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques2)
                        {
                            Itog.Vrach += 10;
                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques3)
                        {
                            Itog.Vrach += 5;

                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques4)
                        {
                            Itog.Vrach += 0;
                        }
                        break;
                    case 4:
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques1)
                        {
                            Itog.Inginer += 15;
                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques2)
                        {
                            Itog.Inginer += 10;
                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques3)
                        {
                            Itog.Inginer += 5;

                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques4)
                        {
                            Itog.Inginer += 0;
                        }
                        break;
                    case 5:
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques1)
                        {
                            Itog.Designer += 15;
                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques2)
                        {
                            Itog.Designer += 10;
                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques3)
                        {
                            Itog.Designer += 5;

                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques4)
                        {
                            Itog.Designer += 0;
                        }
                        break;
                    case 6:
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques1)
                        {
                            Itog.Psiholog += 15;
                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques2)
                        {
                            Itog.Psiholog += 10;
                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques3)
                        {
                            Itog.Psiholog += 5;

                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques4)
                        {
                            Itog.Psiholog += 0;
                        }
                        break;
                    case 7:
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques1)
                        {
                            Itog.Urist += 15;
                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques2)
                        {
                            Itog.Urist += 10;
                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques3)
                        {
                            Itog.Urist += 5;

                        }
                        if (group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) == radioques4)
                        {
                            Itog.Urist += 0;
                        }
                        break;

                }
            }
            if (id == 14)
            {
                ItogForm dc = new ItogForm();
                dc.Show();
                this.Hide();
            }
            else
            {
                id++;
                //group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked)
                SqlConnection connection = new SqlConnection(DateBase.connStr);
                try
                {
                    label1.Text = id + "/14";
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter($@"SELECT [Right], [Test].Id_Test, [Name_Test], [Ques_1], [Ques_2], [Ques_3], [Ques_4], [Right] FROM [Test] WHERE Id_test = {id}", connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    testData.Id_test = ds.Tables[0].Rows[0].Field<int>(ds.Tables[0].Columns[1]);
                    testData.name_test = ds.Tables[0].Rows[0].Field<string>(ds.Tables[0].Columns[2]);
                    testData.Ques_1 = ds.Tables[0].Rows[0].Field<string>(ds.Tables[0].Columns[3]);
                    testData.Ques_2 = ds.Tables[0].Rows[0].Field<string>(ds.Tables[0].Columns[4]);
                    testData.Ques_3 = ds.Tables[0].Rows[0].Field<string>(ds.Tables[0].Columns[5]);
                    testData.Ques_4 = ds.Tables[0].Rows[0].Field<string>(ds.Tables[0].Columns[6]);
                    testData.Right = ds.Tables[0].Rows[0].Field<int>(ds.Tables[0].Columns[7]);

                    Name.Text = testData.name_test;
                    radioques1.Text = testData.Ques_1;
                    radioques2.Text = testData.Ques_2;
                    radioques3.Text = testData.Ques_3;
                    radioques4.Text = testData.Ques_4;




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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Profile au = new Profile();
            au.Show();
            this.Hide();
        }

        private void TestForm_Load(object sender, EventArgs e)//загрузка формы
        {
            id++;
            //group.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked)
            SqlConnection connection = new SqlConnection(DateBase.connStr);
            try
            {
                label1.Text = id + "/14";
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter($@"SELECT [Right], [Test].Id_Test, [Name_Test], [Ques_1], [Ques_2], [Ques_3], [Ques_4], [Right] FROM [Test] WHERE Id_test = {id}", connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                testData.Id_test = ds.Tables[0].Rows[0].Field<int>(ds.Tables[0].Columns[1]);
                testData.name_test = ds.Tables[0].Rows[0].Field<string>(ds.Tables[0].Columns[2]);
                testData.Ques_1 = ds.Tables[0].Rows[0].Field<string>(ds.Tables[0].Columns[3]);
                testData.Ques_2 = ds.Tables[0].Rows[0].Field<string>(ds.Tables[0].Columns[4]);
                testData.Ques_3 = ds.Tables[0].Rows[0].Field<string>(ds.Tables[0].Columns[5]);
                testData.Ques_4 = ds.Tables[0].Rows[0].Field<string>(ds.Tables[0].Columns[6]);
                testData.Right = ds.Tables[0].Rows[0].Field<int>(ds.Tables[0].Columns[7]);

                Name.Text = testData.name_test;
                radioques1.Text = testData.Ques_1;
                radioques2.Text = testData.Ques_2;
                radioques3.Text = testData.Ques_3;
                radioques4.Text = testData.Ques_4;




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

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }
    }
}
