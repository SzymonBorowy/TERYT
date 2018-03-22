using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Text.RegularExpressions;

namespace teryt_1
{
    public partial class Form1 : Form
    {
        List<String> lista_linkow = new List<String>();
        List<String> lista_dat = new List<String>();
        //string url = "http://airbox.home/index.html";
        string url = "http://eteryt.stat.gov.pl/eTeryt/rejestr_teryt/udostepnianie_danych/baza_teryt/uzytkownicy_indywidualni/pobieranie/pliki_pelne.aspx?contrast=default";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pobierz_kod_strony(url);
        }
        public void pobierz_kod_strony(string link)
        {
            WebClient client = new WebClient();                     //logowanie do strony 

            Byte[] pageData = client.DownloadData(link);            //pobranie kodu strony z linku
            string pageHtml = Encoding.UTF8.GetString(pageData);    //rozkodawanie kodu strony
            szukaj_linkow(pageHtml);                            //szukanie linków 
            szukaj_dat(pageHtml);

        }
        
        private void szukaj_linkow(string kod_strony)
        {
            string strona = @"href=\""javascript(.*?)\""";
            Regex regex = new Regex(strona, RegexOptions.IgnoreCase);
            foreach (Match match in regex.Matches(kod_strony))
            {
                string index = match.Value;
                index = index.Replace("href=\"javascript:", "");
                index = index.Replace(@"""", "");
                lista_linkow.Add( index);
                MessageBox.Show(index);
            }
        }

        private void szukaj_dat(string kod_strony)
        {
            string strona = "<input name =\"ctl00$body$TBData\" type=\"text\" value=\"(.*?)\"";
            Regex regex = new Regex(strona, RegexOptions.IgnoreCase);
            foreach (Match match in regex.Matches(kod_strony))
            {
                string index = match.Value;
                index = index.Replace("<input name =\"ctl00$body$TBData\" type=\"text\" value=\"", "");
                index = index.Replace("\"", "");
                lista_dat.Add(index);
                MessageBox.Show(index);
            }
        }
    }
}
