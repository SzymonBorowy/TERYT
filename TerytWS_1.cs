using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;

using System.Xml;

namespace OdpnInniUczniowieServiceDll
{
    public class TerytWS
    {
        private string _sciezka = @"C:\Temp\Teryt\";

        public Plik PlikTERC;
        public Plik PlikSIMC;
        public Plik PlikULIC;

        public void Aktualizuj()
        {
            try
            {
                if (Directory.Exists(_sciezka) == false)
                    Directory.CreateDirectory(_sciezka);

                var proxy = new ChannelFactory<TerytWS1.ITerytWs1>("custom");
                proxy.Credentials.UserName.UserName = "TestPubliczny";
                proxy.Credentials.UserName.Password = "1234abcd";
                var result = proxy.CreateChannel();

                if (result.CzyZalogowany())
                {
                    DateTime data = DateTime.Today;

                    PlikTERC = new Plik(_sciezka, result.PobierzKatalogTERC(result.PobierzDateAktualnegoKatTerc()));
                    PlikTERC.ZapiszPlik();

                    PlikSIMC = new Plik(_sciezka, result.PobierzKatalogSIMC(result.PobierzDateAktualnegoKatSimc()));
                    PlikSIMC.ZapiszPlik();

                    PlikULIC = new Plik(_sciezka, result.PobierzKatalogULIC(result.PobierzDateAktualnegoKatUlic()));
                    PlikULIC.ZapiszPlik();



                    var czegoNieUsuwac = new string[]
                        {
                            PlikTERC.FolderWKtorymJestZapisane(),
                            PlikSIMC.FolderWKtorymJestZapisane(),
                            PlikULIC.FolderWKtorymJestZapisane()
                        };

                    foreach (var dic in Directory.GetDirectories(_sciezka))
                    {
                        if (czegoNieUsuwac.Contains(dic) == false)
                            Directory.Delete(dic, true);
                    }
                }
            }
            catch
            {
            }
        }

        public PlikTeryt DajStrukturePelengoTERYTa()
        {
            var plik = new PlikTeryt();

            var rows = PlikTERC.DajXmlDocument().DocumentElement.SelectNodes("catalog/row");
            foreach (XmlNode row in rows)
            {
                var podzial = new TerytPodzial();
                podzial.woj = row["WOJ"].InnerText;
                podzial.pow = row["POW"].InnerText;
                podzial.gmi = row["GMI"].InnerText;
                podzial.rodz = row["RODZ"].InnerText;
                podzial.nazwa = row["NAZWA"].InnerText;
                podzial.NAZDOD = row["NAZWA_DOD"].InnerText;
                podzial.stan_na = row["STAN_NA"].InnerText;
                plik.Podzial.Add(podzial);
            }

            rows = PlikSIMC.DajXmlDocument().DocumentElement.SelectNodes("catalog/row");
            foreach (XmlNode row in rows)
            {
                var miejscowosc = new TerytMiejscowosci();
                miejscowosc.woj = row["WOJ"].InnerText;
                miejscowosc.pow = row["POW"].InnerText;
                miejscowosc.gmi = row["GMI"].InnerText;
                miejscowosc.RODZ_GMI = row["RODZ_GMI"].InnerText;
                miejscowosc.rm = row["RM"].InnerText;
                miejscowosc.mz = row["MZ"].InnerText;
                miejscowosc.nazwa = row["NAZWA"].InnerText;
                miejscowosc.sym = row["SYM"].InnerText;
                miejscowosc.sympod = row["SYMPOD"].InnerText;
                miejscowosc.stan_na = row["STAN_NA"].InnerText;
                plik.Miejscowosci.Add(miejscowosc);
            }

            rows = PlikULIC.DajXmlDocument().DocumentElement.SelectNodes("catalog/row");
            foreach (XmlNode row in rows)
            {
                var ulica = new TerytUlice();
                ulica.teruli_id = null;
                ulica.teruli_woj = row["WOJ"].InnerText;
                ulica.teruli_pow = row["POW"].InnerText;
                ulica.teruli_gmi = row["GMI"].InnerText;
                ulica.teruli_rodz_gmi = row["RODZ_GMI"].InnerText;
                ulica.teruli_sym = row["SYM"].InnerText;
                ulica.teruli_sym_ul = row["SYM_UL"].InnerText;
                ulica.teruli_cecha = row["CECHA"].InnerText;
                ulica.teruli_nazwa_1 = row["NAZWA_1"].InnerText;
                ulica.teruli_nazwa_2 = row["NAZWA_2"].InnerText;
                ulica.teruli_stan_na = row["STAN_NA"].InnerText;
                ulica.teruli_terpodid = null;
                plik.Ulice.Add(ulica);
            }
            return plik;
        }

        public class Plik
        {
            public TerytWS1.PlikKatalog PlikKatalog;
            public string TempFile;

            public string FolderWKtorymJestZapisane()
            {
                return TempFile + PlikKatalog.nazwa_pliku;
            }

            public XmlDocument DajXmlDocument()
            {
                var doc = new XmlDocument();
                var sr = new StreamReader(TempFile + PlikKatalog.nazwa_pliku + @"\" + PlikKatalog.nazwa_pliku + ".xml", System.Text.Encoding.UTF8);
                doc.Load(sr);
                sr.DiscardBufferedData();
                sr.Dispose();
                sr.Close();
                return doc;
            }

            public Plik(string tempFile, TerytWS1.PlikKatalog plikKatalog)
            {
                PlikKatalog = plikKatalog;
                TempFile = tempFile;
            }

            public void ZapiszPlik()
            {
                if (CzyJestPlikAktualny() == false)
                {
                    var rozkodowaneDaneJakoByte = System.Convert.FromBase64String(PlikKatalog.plik_zawartosc);
                    File.WriteAllBytes(TempFile + PlikKatalog.nazwa_pliku + ".zip", rozkodowaneDaneJakoByte);

                    RozpakujPlik();
                }
            }

            private void RozpakujPlik()
            {
                if (CzyJestPlikAktualny() == false)
                {
                    System.IO.Compression.ZipFile.ExtractToDirectory(TempFile + PlikKatalog.nazwa_pliku + ".zip", TempFile + PlikKatalog.nazwa_pliku);
                    File.Delete(TempFile + PlikKatalog.nazwa_pliku + ".zip");
                }
            }

            private bool CzyJestPlikAktualny()
            {
                return Directory.Exists(TempFile + PlikKatalog.nazwa_pliku);
            }
        }
    }
}
