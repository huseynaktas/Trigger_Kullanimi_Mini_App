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

namespace Trigger_Kullanimi_Mini_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-3JI920O\SQLEXPRESS;Initial Catalog=DbProje16;Integrated Security=True");

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLKITAPLAR", conn); // TBLKITAPLAR tablosundaki tüm verileri çek
            DataTable dt = new DataTable(); // Verileri tutacağımız bir tablo oluştur
            da.Fill(dt); // Verileri tabloya doldur
            dataGridView1.DataSource = dt; // Tabloyu datagridview'e aktar
        }

        void sayac()
        {
            conn.Open(); // Bağlantıyı aç
            SqlCommand cmd = new SqlCommand("Select * From TBLSAYAC", conn); // TBLSAYAC tablosundaki tüm verileri çek
            SqlDataReader dr = cmd.ExecuteReader(); // Sorguyu çalıştır
            while (dr.Read())
            {
                lblKitapAdet.Text = dr[0].ToString(); // Toplam kitap sayısını label'a yazdır
            }
            conn.Close(); // Bağlantıyı kapat
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele(); // Form yüklendiğinde listele fonksiyonunu çalıştır
            sayac(); // Form yüklendiğinde sayac fonksiyonunu çalıştır
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            conn.Open(); // Bağlantıyı aç
            SqlCommand cmd = new SqlCommand("Insert Into TBLKITAPLAR (AD,YAZAR, SAYFA, YAYINEVI, TUR ) Values (@p1,@p2, @p3, @p4, @p5)", conn);
            cmd.Parameters.AddWithValue("@p1", txtAd.Text); 
            cmd.Parameters.AddWithValue("@p2", txtYazar.Text);
            cmd.Parameters.AddWithValue("@p3", int.Parse(txtSayfa.Text));
            cmd.Parameters.AddWithValue("@p4", txtYayinEvi.Text);
            cmd.Parameters.AddWithValue("@p5", txtTur.Text);
            cmd.ExecuteNonQuery(); // Sorguyu çalıştır
            conn.Close(); // Bağlantıyı kapat
            MessageBox.Show("Kitap Eklendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information); // Ekleme işlemi başarılı mesajı göster
            listele(); // Listele fonksiyonunu çalıştır
            sayac(); // Sayac fonksiyonunu çalıştır
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex; // Seçilen satırı al
            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString(); // Seçilen satırın 0. hücresini id textbox'ına yazdır
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString(); // Seçilen satırın 1. hücresini ad textbox'ına yazdır
            txtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString(); // Seçilen satırın 2. hücresini yazar textbox'ına yazdır
            txtSayfa.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString(); // Seçilen satırın 3. hücresini sayfa textbox'ına yazdır
            txtYayinEvi.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString(); // Seçilen satırın 4. hücresini yayınevi textbox'ına yazdır
            txtTur.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString(); // Seçilen satırın 5. hücresini tür textbox'ına yazdır

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Delete From TBLKITAPLAR Where ID=@p1", conn);
            cmd.Parameters.AddWithValue("@p1", txtId.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Kitap Sistemden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            sayac();
        }
    }
}
