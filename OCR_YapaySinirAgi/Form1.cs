using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCR_YapaySinirAgi
{
    public partial class Form1 : Form
    {
        Button[,] matrisButonlari = new Button[7, 5];
        private YSA ysaModel; // YSA modelimiz

        public Form1()
        {
            InitializeComponent();
        }

        private void btn1_4_Click(object sender, EventArgs e) { }

        private void txtOgrenmeOraniAyarla_TextChanged(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Butonları matris dizisine yerleştir
            matrisButonlari[0, 0] = btn0_0;
            matrisButonlari[0, 1] = btn0_1;
            matrisButonlari[0, 2] = btn0_2;
            matrisButonlari[0, 3] = btn0_3;
            matrisButonlari[0, 4] = btn0_4;

            matrisButonlari[1, 0] = btn1_0;
            matrisButonlari[1, 1] = btn1_1;
            matrisButonlari[1, 2] = btn1_2;
            matrisButonlari[1, 3] = btn1_3;
            matrisButonlari[1, 4] = btn1_4;

            matrisButonlari[2, 0] = btn2_0;
            matrisButonlari[2, 1] = btn2_1;
            matrisButonlari[2, 2] = btn2_2;
            matrisButonlari[2, 3] = btn2_3;
            matrisButonlari[2, 4] = btn2_4;

            matrisButonlari[3, 0] = btn3_0;
            matrisButonlari[3, 1] = btn3_1;
            matrisButonlari[3, 2] = btn3_2;
            matrisButonlari[3, 3] = btn3_3;
            matrisButonlari[3, 4] = btn3_4;

            matrisButonlari[4, 0] = btn4_0;
            matrisButonlari[4, 1] = btn4_1;
            matrisButonlari[4, 2] = btn4_2;
            matrisButonlari[4, 3] = btn4_3;
            matrisButonlari[4, 4] = btn4_4;

            matrisButonlari[5, 0] = btn5_0;
            matrisButonlari[5, 1] = btn5_1;
            matrisButonlari[5, 2] = btn5_2;
            matrisButonlari[5, 3] = btn5_3;
            matrisButonlari[5, 4] = btn5_4;

            matrisButonlari[6, 0] = btn6_0;
            matrisButonlari[6, 1] = btn6_1;
            matrisButonlari[6, 2] = btn6_2;
            matrisButonlari[6, 3] = btn6_3;
            matrisButonlari[6, 4] = btn6_4;

            // Tıklama olaylarını ekle
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matrisButonlari[i, j].Click += MatrisButon_Click;
                }
            }
        }

        private void MatrisButon_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.BackColor == Color.Black)
                btn.BackColor = Color.White;
            else
                btn.BackColor = Color.Black;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 5; j++)
                    matrisButonlari[i, j].BackColor = Color.White;
        }

        private void MatrisiGoster()
        {
            string matrisText = "";

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matrisText += (matrisButonlari[i, j].BackColor == Color.Black ? "1 " : "0 ");
                }
                matrisText += "\r\n";
            }

            txtMatrisGosterimi.Text = matrisText;
        }

        private void btnGoster_Click(object sender, EventArgs e)
        {
            MatrisiGoster();
        }

        private void btnEgit_Click(object sender, EventArgs e)
        {
            try
            {
                double ogrenmeOrani = double.Parse(txtOgrenmeOrani.Text);
                int yineleme = int.Parse(txtYinelemeAyarla.Text);

                ysaModel = new YSA(35, 3, 5, ogrenmeOrani);

                double hata = ysaModel.Egit(OrnekVeri.Girdiler, OrnekVeri.Ciktilar, yineleme);

                lblHesaplananHataOrani.Text = hata.ToString("F15");
                MessageBox.Show("Eğitim tamamlandı!", "YSA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private double[] MatrisiDiziyeDonustur()
        {
            double[] girdi = new double[35];
            int index = 0;

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    girdi[index++] = (matrisButonlari[i, j].BackColor == Color.Black) ? 1.0 : 0.0;
                }
            }

            return girdi;
        }

        private void btnTanimla_Click(object sender, EventArgs e)
        {
            if (ysaModel == null)
            {
                MessageBox.Show("Lütfen önce ağı eğitin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Matristen 0-1 girdi dizisi oluştur
            double[] girdi = MatrisiDiziyeDonustur();

            // 2. Sinir ağı ile tahmin yap
            double[] sonuc = ysaModel.TahminYap(girdi);

            // 3. En yüksek değerli çıkış nöronunu bul
            int tahminIndex = Array.IndexOf(sonuc, sonuc.Max());
            string[] harfler = { "A", "B", "C", "D", "E" };
            string tahmin = harfler[tahminIndex];

            // 4. Tahmin değerlerini liste kutusuna yaz
            lstCikisDegerleri.Items.Clear();
            for (int i = 0; i < sonuc.Length; i++)
            {
                lstCikisDegerleri.Items.Add($"{harfler[i]} - {sonuc[i]:F15}");
            }

            // 5. Tahmini ayrı göster
            MessageBox.Show("Tahmin edilen harf: " + tahmin, "SONUÇ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
