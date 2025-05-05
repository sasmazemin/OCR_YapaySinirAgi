using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR_YapaySinirAgi
{
    class YSA
    {
        int girisSayisi;
        int gizliSayisi;
        int cikisSayisi;

        double[][] g2w; // Giriş → Gizli
        double[][] w2c; // Gizli → Çıkış

        double[] gizliAktivasyon;
        double[] cikisAktivasyon;

        double ogrenmeOrani;

        Random rnd = new Random();

        public YSA(int giris, int gizli, int cikis, double ogrenmeOrani)
        {
            this.girisSayisi = giris;
            this.gizliSayisi = gizli;
            this.cikisSayisi = cikis;
            this.ogrenmeOrani = ogrenmeOrani;

            g2w = new double[giris][];
            for (int i = 0; i < giris; i++)
            {
                g2w[i] = new double[gizli];
                for (int j = 0; j < gizli; j++)
                    g2w[i][j] = rnd.NextDouble() - 0.5; // -0.5 ile 0.5 arasında rastgele
            }

            w2c = new double[gizli][];
            for (int i = 0; i < gizli; i++)
            {
                w2c[i] = new double[cikis];
                for (int j = 0; j < cikis; j++)
                    w2c[i][j] = rnd.NextDouble() - 0.5;
            }

            gizliAktivasyon = new double[gizli];
            cikisAktivasyon = new double[cikis];
        }

        private double Sigmoid(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        private double SigmoidTurev(double x)
        {
            return x * (1.0 - x); // sigmoid türevi
        }

        public double[] TahminYap(double[] girdi)
        {
            // Giriş → Gizli
            for (int j = 0; j < gizliSayisi; j++)
            {
                double toplam = 0;
                for (int i = 0; i < girisSayisi; i++)
                    toplam += girdi[i] * g2w[i][j];

                gizliAktivasyon[j] = Sigmoid(toplam);
            }

            // Gizli → Çıkış
            for (int k = 0; k < cikisSayisi; k++)
            {
                double toplam = 0;
                for (int j = 0; j < gizliSayisi; j++)
                    toplam += gizliAktivasyon[j] * w2c[j][k];

                cikisAktivasyon[k] = Sigmoid(toplam);
            }

            return cikisAktivasyon;
        }

        public double Egit(double[][] egitimGirdileri, double[][] egitimCiktilari, int iterasyon)
        {
            double hata = 0;

            for (int iter = 0; iter < iterasyon; iter++)
            {
                hata = 0;

                for (int ornek = 0; ornek < egitimGirdileri.Length; ornek++)
                {
                    double[] giris = egitimGirdileri[ornek];
                    double[] hedef = egitimCiktilari[ornek];

                    // İleri yayılım
                    double[] tahmin = TahminYap(giris);

                    // Hata hesapla (çıkış katmanı)
                    double[] cikisHatalari = new double[cikisSayisi];
                    for (int k = 0; k < cikisSayisi; k++)
                    {
                        double fark = hedef[k] - tahmin[k];
                        cikisHatalari[k] = fark * SigmoidTurev(tahmin[k]);
                        hata += fark * fark;
                    }

                    // Gizli katman hataları
                    double[] gizliHatalari = new double[gizliSayisi];
                    for (int j = 0; j < gizliSayisi; j++)
                    {
                        double toplam = 0;
                        for (int k = 0; k < cikisSayisi; k++)
                            toplam += cikisHatalari[k] * w2c[j][k];
                        gizliHatalari[j] = toplam * SigmoidTurev(gizliAktivasyon[j]);
                    }

                    // Gizli → Çıkış ağırlık güncelleme
                    for (int j = 0; j < gizliSayisi; j++)
                    {
                        for (int k = 0; k < cikisSayisi; k++)
                        {
                            w2c[j][k] += ogrenmeOrani * cikisHatalari[k] * gizliAktivasyon[j];
                        }
                    }

                    // Giriş → Gizli ağırlık güncelleme
                    for (int i = 0; i < girisSayisi; i++)
                    {
                        for (int j = 0; j < gizliSayisi; j++)
                        {
                            g2w[i][j] += ogrenmeOrani * gizliHatalari[j] * giris[i];
                        }
                    }
                }

                hata /= egitimGirdileri.Length; // Ortalama hata
            }

            return hata;
        }


    }
}
