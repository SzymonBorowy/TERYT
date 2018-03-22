using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace teryt_pliki_aktualizacyjne
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtDataOd.Text = "2017-06-29";
            txtDataDo.Text = "2017-06-29";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            txtDataOd.BackColor = Color.White;
            txtDataDo.BackColor = Color.White;
            PobieraniePlikowAktualizacyjnych pobierz = new PobieraniePlikowAktualizacyjnych();

            DateTime data1 = new DateTime(), data2 = new DateTime(), dataObecna = DateTime.Today;
            //data Od
            if (sprawdzDate(txtDataOd.Text,txtDataOd) == 1 && sprawdzDate(txtDataDo.Text, txtDataDo) == 1 )
            {
                data1 = DateTime.Parse(txtDataOd.Text);
                data2 = DateTime.Parse(txtDataDo.Text);
                if (data1 <= dataObecna)
                {
                    if(data2 <= dataObecna)
                    {
                        if(data1 <= data2)
                        {
                            pobierz.Start(data1, data2);
                        }   
                        else
                        {
                            MessageBox.Show("Data Od mus być wcześniej od daty Do");
                            txtDataOd.BackColor = Color.Red;
                        }
                    }                            
                    else
                    {
                        MessageBox.Show("Data Do musi być wczesnije niż dzisiaj");
                        txtDataDo.BackColor = Color.Red;
                    }   
                }                  
                else
                {
                    MessageBox.Show("Data Od musi być wczesnije niż dzisiaj");
                    txtDataOd.BackColor = Color.Red;
                }
            }           
        }
        private int sprawdzDate(string data, MaskedTextBox nazwa)
        {
            if (data.Length == 10)
            {
                try
                {
                    DateTime data1 = DateTime.Parse(data);
                    return 1;
                }
                catch
                {
                    nazwa.BackColor = Color.Red;
                    return 0;
                }
            }
            else
            {
                nazwa.BackColor = Color.Red;
                return 0;
            }
                
        }
    }
}
