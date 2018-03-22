using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace terytDLL
{
    public class teryt_DLL
    {
        System.Timers.Timer _timer = new System.Timers.Timer();

        private List<byte[]> _listaplikow = new List<byte[]>();
        private List<string> _listanazwPobrane = new List<string>();
        private List<string> _listanazwWczytane = new List<string>();
        private string _sciezka = @"C:\Temp\Teryt\";

        public void Start()
        {
            //Aktualizuj();
            _timer.Start();
            _timer.Interval = 10; //pierwsze uruchomienie
            _timer.Enabled = true;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
        }
        protected void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Stop();
            File.WriteAllText(@"C:\Temp\x.txt", "");
            File.WriteAllText(@"C:\Temp\x.txt", "start " + DateTime.Now + Environment.NewLine);
            File.AppendAllText(@"C:\Temp\x.txt", "akualizuj" + Environment.NewLine);
            Aktualizuj();
            File.AppendAllText(@"C:\Temp\x.txt", "koniec" + DateTime.Now + Environment.NewLine);
            int minuta = 60000;
            int godzina = minuta * 60;
            _timer.Interval = minuta;
            _timer.Start();
        }
        public void Aktualizuj()
        {
            PobierzNazwyPlikow();
            ZalogujIPobierzDane();
            _listaplikow.Clear();
            _listanazwPobrane.Clear();
            _listanazwWczytane.Clear();
        }
        private void ZalogujIPobierzDane()
        {
            try
            {
                File.AppendAllText(@"C:\Temp\x.txt", "log1" + Environment.NewLine);
                var proxy = new ChannelFactory<terytDLL.ServiceReferenceWCF.ITerytWs1>("custom");    //polącz sie z usługą   
                File.AppendAllText(@"C:\Temp\x.txt", "log1.1" + Environment.NewLine);
                proxy.Credentials.UserName.UserName = "TestPubliczny";                              //login 
                proxy.Credentials.UserName.Password = "1234abcd";                                   //hasło 
                var result = proxy.CreateChannel();                                                 //utwórz kanał  
                var test = result.CzyZalogowany();                                                  //sprawdz czy zalogowany 
                if(test == true)
                {
                    DateTime data = DateTime.Today;
                    File.AppendAllText(@"C:\Temp\x.txt", "log1.2" + Environment.NewLine);
                    //TERC
                    var plik = result.PobierzKatalogTERC(result.PobierzDateAktualnegoKatTerc());               //pobierz plik za pomocą funkcji wbudowanej
                    byte[] rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);     //rozkoduj plik na bajty
                    _listaplikow.Add(rozkodowaneDaneJakoByte);                                                 //dodaj plik do listy plików
                    _listanazwPobrane.Add(plik.nazwa_pliku);                                                   //dodaj nazwę pliku do listy nazw
                    File.AppendAllText(@"C:\Temp\x.txt", "1" + Environment.NewLine);
                    //TERC_ADR
                    plik = result.PobierzKatalogTERCAdr(result.PobierzDateAktualnegoKatTerc());
                    rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);
                    _listaplikow.Add(rozkodowaneDaneJakoByte);
                    _listanazwPobrane.Add(plik.nazwa_pliku);
                    File.AppendAllText(@"C:\Temp\x.txt", "2" + Environment.NewLine);
                    //SIMC
                    plik = result.PobierzKatalogSIMC(result.PobierzDateAktualnegoKatSimc());
                    rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);
                    _listaplikow.Add(rozkodowaneDaneJakoByte);
                    _listanazwPobrane.Add(plik.nazwa_pliku);
                    File.AppendAllText(@"C:\Temp\x.txt", "3" + Environment.NewLine);
                    //SIMC_ADR
                    plik = result.PobierzKatalogSIMCAdr(result.PobierzDateAktualnegoKatSimc());
                    rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);
                    _listaplikow.Add(rozkodowaneDaneJakoByte);
                    _listanazwPobrane.Add(plik.nazwa_pliku);
                    File.AppendAllText(@"C:\Temp\x.txt", "4" + Environment.NewLine);
                    //SIMC_STAT
                    plik = result.PobierzKatalogSIMCStat(result.PobierzDateAktualnegoKatSimc());
                    rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);
                    _listaplikow.Add(rozkodowaneDaneJakoByte);
                    _listanazwPobrane.Add(plik.nazwa_pliku);
                    File.AppendAllText(@"C:\Temp\x.txt", "5" + Environment.NewLine);
                    //ULIC
                    plik = result.PobierzKatalogULIC(result.PobierzDateAktualnegoKatUlic());
                    rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);
                    _listaplikow.Add(rozkodowaneDaneJakoByte);
                    _listanazwPobrane.Add(plik.nazwa_pliku);
                    File.AppendAllText(@"C:\Temp\x.txt", "6" + Environment.NewLine);
                    //ULIC_ADR
                    plik = result.PobierzKatalogULICAdr(result.PobierzDateAktualnegoKatUlic());
                    rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);
                    _listaplikow.Add(rozkodowaneDaneJakoByte);
                    _listanazwPobrane.Add(plik.nazwa_pliku);
                    File.AppendAllText(@"C:\Temp\x.txt", "7" + Environment.NewLine);
                    //ULIC_Bez_dzielnic
                    plik = result.PobierzKatalogULICBezDzielnic(result.PobierzDateAktualnegoKatUlic());
                    rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);
                    _listaplikow.Add(rozkodowaneDaneJakoByte);
                    _listanazwPobrane.Add(plik.nazwa_pliku);
                    File.AppendAllText(@"C:\Temp\x.txt", "8" + Environment.NewLine);
                    //NTS
                    plik = result.PobierzKatalogNTS(result.PobierzDateAktualnegoKatNTS());
                    rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);
                    _listaplikow.Add(rozkodowaneDaneJakoByte);
                    _listanazwPobrane.Add(plik.nazwa_pliku);
                    File.AppendAllText(@"C:\Temp\x.txt", "9" + Environment.NewLine);
                    //WMRODZ
                    plik = result.PobierzKatalogWMRODZ(data);
                    rozkodowaneDaneJakoByte = System.Convert.FromBase64String(plik.plik_zawartosc);
                    _listaplikow.Add(rozkodowaneDaneJakoByte);
                    _listanazwPobrane.Add(plik.nazwa_pliku);
                    File.AppendAllText(@"C:\Temp\x.txt", "log2" + Environment.NewLine);

                    AktualizujPlikiTeryt();                                                 //przejscei do matody aktualizujacej
                }
            }
            catch
            { }
        }
        private void AktualizujPlikiTeryt()
        {
            File.AppendAllText(@"C:\Temp\x.txt", "aktualizacja" + Environment.NewLine);
            try
            {
                Directory.CreateDirectory(_sciezka);                                        //utworzenie głównego foldeu
            }
            catch
            { }
            PorownajListy();//porównuje listy i usuwa z folderu nieaktualne pliki

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
        private void PobierzNazwyPlikow()
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(_sciezka);                             //wczytanie scieżki
                DirectoryInfo[] foldery = di.GetDirectories();                              //wczytanie wszystkich folderów                
                if (foldery.Length > 0)
                {
                    foreach (DirectoryInfo folder in foldery)
                        _listanazwWczytane.Add(folder.Name);                                //wpisanie do listy nazw Wczytanych nazw folderów
                }
            }
            catch
            { }

        }
        private int PorownajElementyList(string listaA, string listaB)
        {
            if (listaA == listaB)
                return 0;
            else
                return 1;
        }
        private void PorownajListy()
        {
            _listanazwPobrane.Sort();
            _listanazwWczytane.Sort();
            if (_listanazwPobrane.Count == _listanazwWczytane.Count)                                //jeśli są równe listy(liczba elemantów)
            {
                for (int i = 0; i < _listanazwPobrane.Count(); i++)
                {
                    if (PorownajElementyList(_listanazwPobrane[i], _listanazwWczytane[i]) == 1)     //porównanie dwóch odpowiadających sobie elementów
                        Directory.Delete(_sciezka + _listanazwWczytane[i], true);                   //usunięcie nieaktualnego
                }
            }
            else
            {
                foreach (var i in _listanazwWczytane)                                               //jeśli listy nie są równe(liczba elemantów)
                    Directory.Delete(_sciezka + i, true);                                           //usuń wszystkie
            }

        }
    }
}
