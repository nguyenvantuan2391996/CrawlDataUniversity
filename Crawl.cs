using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;

namespace CrawlDataUniversity
{
    public partial class Crawl : Form
    {
        public Crawl()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        public static ArrayList listLink = new ArrayList();

        private void button1_Click(object sender, EventArgs e)
        {
            string name = "";
            string score = "";
            string khoithi = "";
            string html = "";

            HttpRequest request = new HttpRequest();
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) coc_coc_browser/86.0.172 Chrome/80.0.3987.172 Safari/537.36";

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

            for (int i = 0; i < listLink.Count; i++)
            {
                html = request.Get("https://kenhtuyensinh.vn/" + listLink[i]).ToString();

                // Load html
                document.LoadHtml(html);

                using (StreamWriter sw = new StreamWriter((i + 1).ToString() + ".txt", true))
                {
                    try
                    {
                        for (int j = 1; j < 200; j++)
                        {
                            name = document.DocumentNode.SelectSingleNode("//*[@id=\"table-school\"]/tbody/tr[" + j.ToString() + "]/td[3]").InnerText;
                            score = document.DocumentNode.SelectSingleNode("//*[@id=\"table-school\"]/tbody/tr[" + j.ToString() + "]/td[5]").InnerText;
                            khoithi = document.DocumentNode.SelectSingleNode("//*[@id=\"table-school\"]/tbody/tr[" + j.ToString() + "]/td[4]").InnerText;
                            sw.WriteLine(name.Replace("\n", "") + "\t" + khoithi.Replace("\n", "") + "\t" + score.Replace("\r\n", "\t"));
                        }
                    }
                    catch (Exception)
                    {

                    }
                }

                processed.Text = (i + 1).ToString();
                processed.Refresh();

            }
            MessageBox.Show("Done");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SumLink.Text = "";
            processed.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string result = "";
            try
            {
                using (StreamReader sr = new StreamReader("university.txt"))
                {
                    while ((result = sr.ReadLine()) != null)
                    {
                        // thêm vào list view danh sách
                        listLink.Add(result);
                    }
                }

                SumLink.Text = "/ " + listLink.Count.ToString();

                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
