using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HastaneOtomasyonProjesi
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        public string Tcnumara;
        sqlBaglantisi bgl = new sqlBaglantisi();

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = Tcnumara;
            
            //Ad Soyad cekme
            SqlCommand komut1 = new SqlCommand("Select * From Tbl_Sekreterler where SekreterTc=@p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", lblTc.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while(dr1.Read())
            {
                lblAdSoyad.Text = dr1[0].ToString();

            }
            bgl.baglanti().Close();

            //Bransları Datagride aktarma
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select BransAd From Tbl_Branslar ",bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Dokotorları Listeye Aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select*From Tbl_Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Branşı Combobox a aktarma
            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while(dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutKaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)", bgl.baglanti());
            komutKaydet.Parameters.AddWithValue("@r1",mskTarih.Text);
            komutKaydet.Parameters.AddWithValue("@r2", mskSaat.Text);
            komutKaydet.Parameters.AddWithValue("@r3", cmbBrans.Text);
            komutKaydet.Parameters.AddWithValue("@r4", cmbDoktor.Text);
            komutKaydet.ExecuteNonQuery(); //degisiklikleri gerceklestirmek icin 
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu.");

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorBrans=@p1 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbDoktor.Items.Add(dr[0] + " " + dr[1]);

            }
            bgl.baglanti().Close();
        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@d1) ",bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", richDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu.");


        }

        private void btnDoktorPaneli_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli fdp = new FrmDoktorPaneli();
            fdp.Show();


        }

        private void btnBransPaneli_Click(object sender, EventArgs e)
        {
            FrmBrans frb = new FrmBrans();
            frb.Show();
        }

        private void btnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frl = new FrmRandevuListesi();
            frl.Show();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular frd = new FrmDuyurular();
            frd.Show();

        }
    }
}
