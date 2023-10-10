using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kyrs2022
{
    public partial class Rec : Form
    {

        /// <summary>
        /// Запаршенные данные, которые будут появлятся здесь в зависимости от рекомендаций которые получит пользователь. Попасть на эту форму невозможно, если не прошел тестирование. 
        /// Кнопка становится видимой после прохождения тестирования
        /// </summary>
        public Rec()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Profile pr = new Profile();
            this.Hide();
            pr.Show();
            
        }

        private void Rec_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(DateBase.connStr);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter($@"SELECT [Time_date].Id_Time_date, [maname], [rekom] FROM [Time_date] WHERE maname = '{LC.nameuser + " " + LC.secname}'", connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DataSource = ds.Tables[0];
            connection.Close();

            label2.Text = dataGridView1[2, Convert.ToInt32(dataGridView1.Rows.Count - 2)].Value.ToString();

            string urlman = null;

            if (label2.Text == "Программист")
            {
                urlman = @"https://vuzoteka.ru/%D0%B2%D1%83%D0%B7%D1%8B/%D0%9F%D1%80%D0%B8%D0%BA%D0%BB%D0%B0%D0%B4%D0%BD%D0%B0%D1%8F-%D0%B8%D0%BD%D1%84%D0%BE%D1%80%D0%BC%D0%B0%D1%82%D0%B8%D0%BA%D0%B0-09-03-03";
            }
            else if (label2.Text == "Дизайнер")
            {
                urlman = @"https://vuzoteka.ru/%D0%B2%D1%83%D0%B7%D1%8B/%D0%94%D0%B8%D0%B7%D0%B0%D0%B9%D0%BD-54-03-01?";
            }
            else if (label2.Text == "Юрист")
            {
                urlman = @"https://vuzoteka.ru/%D0%B2%D1%83%D0%B7%D1%8B/%D0%AE%D1%80%D0%B8%D1%81%D0%BF%D1%80%D1%83%D0%B4%D0%B5%D0%BD%D1%86%D0%B8%D1%8F-40-03-01";

            }
            else if (label2.Text == "Психолог")
            {
                urlman = @"https://vuzoteka.ru/%D0%B2%D1%83%D0%B7%D1%8B/%D0%9F%D1%81%D0%B8%D1%85%D0%BE%D0%BB%D0%BE%D0%B3%D0%B8%D1%8F-37-03-01";

            }
            else if (label2.Text == "Врач")
            {
                urlman = @"https://vuzoteka.ru/%D0%B2%D1%83%D0%B7%D1%8B/%D0%9B%D0%B5%D1%87%D0%B5%D0%B1%D0%BD%D0%BE%D0%B5-%D0%B4%D0%B5%D0%BB%D0%BE-31-05-01";

            }
            else if (label2.Text == "Менеджер")
            {
                urlman = @"https://vuzoteka.ru/%D0%B2%D1%83%D0%B7%D1%8B/%D0%A1%D0%B8%D1%81%D1%82%D0%B5%D0%BC%D0%BD%D1%8B%D0%B9-%D0%B0%D0%BD%D0%B0%D0%BB%D0%B8%D0%B7-%D0%B8-%D1%83%D0%BF%D1%80%D0%B0%D0%B2%D0%BB%D0%B5%D0%BD%D0%B8%D0%B5-27-03-03";

            }
            else if (label2.Text == "Инженер")
            {
                urlman = @"https://vuzoteka.ru/%D0%B2%D1%83%D0%B7%D1%8B/%D0%A2%D0%B5%D1%85%D0%BD%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B0%D1%8F-%D1%84%D0%B8%D0%B7%D0%B8%D0%BA%D0%B0-16-03-01";

            }

            var result = Parsing(url: urlman);
            string[] resultfrompar = (string[])result;


            
            int i = 0;

            List<MyTable> re = new List<MyTable>(3);
            for (int o = 0; o < resultfrompar.Length - 2; o++)
            {
                try
                {
                    string[] splittedres = resultfrompar[o].Split('|');
                    re.Add(new MyTable(splittedres[0], splittedres[1], splittedres[2]));
                }
                catch { }
            }
            dataViewGrid.DataSource = re;
            dataViewGrid.Columns[0].HeaderText = "Название Университета";
            dataViewGrid.Columns[1].HeaderText = "Город";
            dataViewGrid.Columns[2].HeaderText = "Баллы ЕГЭ";
        }

        private  static object Parsing(string url) // парсинг данных с интернет-ресурса
        {
            try
            {
                int i = -1;
                string[] result = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                using (HttpClientHandler hdl = new HttpClientHandler { AllowAutoRedirect = false, AutomaticDecompression = System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.None })
                {
                    using (var clnt = new HttpClient(hdl))
                    {
                        using (HttpResponseMessage resp = clnt.GetAsync(url).Result)
                        {
                            if (resp.IsSuccessStatusCode)
                            {
                                var html = resp.Content.ReadAsStringAsync().Result;
                                if (!string.IsNullOrEmpty(html))
                                {
                                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                    doc.LoadHtml(html);

                                    var tables = doc.DocumentNode.SelectNodes(".//div[@class='institute-rows']//div[@class='institute-row']//div[@class='institute-search-wrapper']");
                                    if (tables != null && tables.Count > 0)
                                    {
                                        foreach (var table in tables)
                                        {
                                            var reos = new List<List<string>>(); 
                                            var titleNode = table.SelectSingleNode(".//div[@class='institute-search-caption']//div[@class='institute-search']//a");
                                            if (titleNode != null)
                                            {
                                                var city = table.SelectSingleNode(".//div[@class='labels-wrap item-features-search border-r5']//div[@class='label-part']//div[@class='institute-search-value']//a");
                                                var ball = table.SelectSingleNode(".//div[@class='labels-wrap item-features-search border-r5']//div[@class='label-part number-3']//div[@class='institute-search-value']");
                                                var res = new List<List<string>>();
                                                i++;
                                                result[i] = titleNode.InnerText + "|" + city.InnerText + "|" + ball.InnerText;
                                            }
                                        }

                                        return result;
                                    }
                                    else {
                                        MessageBox.Show("No tables bro");
                                    
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
    }
}
