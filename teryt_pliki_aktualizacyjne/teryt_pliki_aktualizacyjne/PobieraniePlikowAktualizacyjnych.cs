using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace teryt_pliki_aktualizacyjne
{
    class PobieraniePlikowAktualizacyjnych
    {
        private List<byte[]> _listaplikow = new List<byte[]>();
        private List<string> _listanazwPobrane = new List<string>();
        private string _sciezka = @"C:\Temp\Teryt1\";
        public void Start(DateTime dataOd, DateTime dataDo)
        {
            ZalogujIPobierzDane(dataOd, dataDo);
        }
        private void ZalogujIPobierzDane(DateTime dataOd, DateTime dataDo)
        {
            try
            {
                var proxy = new ChannelFactory<ServiceReference1.ITerytWs1>("custom");    //polącz sie z usługą   //usunięto 
                proxy.Credentials.UserName.UserName = "TestPubliczny";                              //login 
                proxy.Credentials.UserName.Password = "1234abcd";                                   //hasło 
                var result = proxy.CreateChannel();                                                 //utwórz kanał  
                var test = result.CzyZalogowany();                                                  //sprawdz czy zalogowany 
                if (test == true)
                {
                    //TERC
                    var plik = result.PobierzZmianyTercUrzedowy(dataOd, dataDo);                               //pobierz plik za pomocą funkcji wbudowanej
                    byte[] rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);     //rozkoduj plik na bajty
                    _listaplikow.Add(rozkodowaneDaneJakoByte);                                                 //dodaj plik do listy plików
                    _listanazwPobrane.Add(plik.nazwa_pliku);                                                   //dodaj nazwę pliku do listy nazw
                    //TERC_ADR
                    plik = result.PobierzZmianyTercAdresowy(dataOd, dataDo);
                    rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);
                    _listaplikow.Add(rozkodowaneDaneJakoByte);
                    _listanazwPobrane.Add(plik.nazwa_pliku);
                    //SIMC
                    plik = result.PobierzZmianySimcUrzedowy(dataOd, dataDo);
                    rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);
                    _listaplikow.Add(rozkodowaneDaneJakoByte);
                    _listanazwPobrane.Add(plik.nazwa_pliku);
                    //SIMC_ADR
                    plik = result.PobierzZmianySimcAdresowy(dataOd, dataDo);
                    rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);
                    _listaplikow.Add(rozkodowaneDaneJakoByte);
                    _listanazwPobrane.Add(plik.nazwa_pliku);
                    //SIMC_STAT
                    plik = result.PobierzZmianySimcStatystyczny(dataOd, dataDo);
                    rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);
                    _listaplikow.Add(rozkodowaneDaneJakoByte);
                    _listanazwPobrane.Add(plik.nazwa_pliku);
                    //ULIC
                    plik = result.PobierzZmianyUlicUrzedowy(dataOd, dataDo);
                    rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);
                    _listaplikow.Add(rozkodowaneDaneJakoByte);
                    _listanazwPobrane.Add(plik.nazwa_pliku);
                    //ULIC_ADR
                    plik = result.PobierzZmianyUlicAdresowy(dataOd, dataDo);
                    rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);
                    _listaplikow.Add(rozkodowaneDaneJakoByte);
                    _listanazwPobrane.Add(plik.nazwa_pliku);
                    //NTS
                    plik = result.PobierzZmianyNTS(dataOd, dataDo);
                    rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);
                    _listaplikow.Add(rozkodowaneDaneJakoByte);
                    _listanazwPobrane.Add(plik.nazwa_pliku);
                    //File.AppendAllText(@"C:\Temp\x.txt", "9" + Environment.NewLine);

                    ZapiszPliki();                                                                  //przejscei do metody zapisującej
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ZapiszPliki()
        {
            try
            {
                Directory.CreateDirectory(_sciezka);                                        //utworzenie głównego foldeu
            }
            catch
            { }

            for (int i = 0; i < _listaplikow.Count(); i++)
            {
                if (Directory.Exists(_sciezka + _listanazwPobrane[i]))                      //sprawdzenie czy folder istnieje
                { }
                else
                {
                    File.WriteAllBytes(_sciezka + _listanazwPobrane[i] + ".zip", _listaplikow[i]);          //zapisanie
                    System.IO.Compression.ZipFile.ExtractToDirectory(_sciezka + _listanazwPobrane[i] + ".zip", _sciezka + _listanazwPobrane[i]); //rozpakowanie zipa
                    FileInfo file = new FileInfo(_sciezka + _listanazwPobrane[i] + ".zip"); //wczytanie zipa
                    file.Delete();                                                          //usuniecie zipa
                }
            }
        }

    }
}
