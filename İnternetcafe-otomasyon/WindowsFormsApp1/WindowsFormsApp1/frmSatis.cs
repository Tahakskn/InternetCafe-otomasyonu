using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmSatis : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=InternetCafe;Integrated Security=True;Pooling=False");
        public frmSatis()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void frmSatis_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'internetCafeDataSet.TBL_SaatUcreti' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tBL_SaatUcretiTableAdapter.Fill(this.internetCafeDataSet.TBL_SaatUcreti);
            radioButtonSuresiz.Checked = true;
            Yenile();
            comboBosMasalar.Text = "";
            timer1.Start();


        }

        public void Yenile()
        {
            Veritabani.SepetListele(dataGridView1);
            Veritabani.ListviewdeKayitlariGoster(listView1);
            Veritabani.ComboBosMasalarıGetir(comboBosMasalar);
            foreach (Control item in Controls)
            {
                if (item is Button)
                {
                    if (item.Name != btnMasaAc.Name)
                    {
                        Veritabani.baglanti.Open();
                        SqlCommand komut = new SqlCommand("select * from TBL_Masalar", Veritabani.baglanti);
                        SqlDataReader dr = komut.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr["Durumu"].ToString() == "Boş" && dr["Masa"].ToString() == item.Text)
                            {
                                item.BackColor = Color.LightGreen;
                            }
                            if (dr["Durumu"].ToString() == "Dolu" && dr["Masa"].ToString() == item.Text)
                            {
                                item.BackColor = Color.Red;
                            }
                        }
                        Veritabani.baglanti.Close();
                    }
                }

            }
        }

        private void button20_Click(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }
        Button btn;

        private void SecileneG(object sender, MouseEventArgs e)
        {
            btn = sender as Button;
            if (btn.BackColor == Color.Red)
            {
                süreliMasaAçmaİsteğiGönderToolStripMenuItem.Visible = false;
                süresizMasaAçmaİsteğiGönderToolStripMenuItem.Visible = false;

            }
            if (btn.BackColor == Color.LightGreen)
            {
                süreliMasaAçmaİsteğiGönderToolStripMenuItem.Visible = true;
                süresizMasaAçmaİsteğiGönderToolStripMenuItem.Visible = true;
            }
        }
        RadioButton radio;

        private void RBSeciliyeGore(object sender, EventArgs e)
        {
            radio = sender as RadioButton;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnMasaAc_Click(object sender, EventArgs e)
        {
            if (Kullanici.KullaniciID == 1)
            {
                string sqlsorgu = "insert into TBL_Sepet(MasaID,Masa,AcilisTuru,Baslangic,Tarih) values('" + comboBosMasalar.Text.Substring(5) + "','" + comboBosMasalar.Text + "'" +
                        ",'" + radio.Text + "',@Baslangic,@Tarih)";
                SqlCommand komut = new SqlCommand();
                komut.Parameters.AddWithValue("@Baslangic", DateTime.Parse(DateTime.Now.ToString()));
                komut.Parameters.AddWithValue("@Tarih", DateTime.Parse(DateTime.Now.ToString()));
                Veritabani.ESG(komut, sqlsorgu);
                MessageBox.Show(comboBosMasalar.Text.Substring(5) + "nolu masa açıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Yenile();
                radioButtonSuresiz.Checked = true;
                
            }
            else
            {
                MessageBox.Show("Böyle bir yetkiniz bulunmuyor", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmSatis_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==dataGridView1.Columns["Hesapla"].Index)
            {
                if (comboSaatUcreti.Text !="")
                {
                    DateTime BitisTarihi = DateTime.Now;
                    DateTime BaslangicTarihi = DateTime.Parse(dataGridView1.CurrentRow.Cells["BaslamaSaati"].Value.ToString());
                    TimeSpan saatfarki = BitisTarihi - BaslangicTarihi;
                    double saatfarki2 = saatfarki.TotalHours;
                    dataGridView1.CurrentRow.Cells["Sure"].Value = saatfarki2.ToString("0.00");
                    double toplamtutar = saatfarki2 * double.Parse(comboSaatUcreti.Text);
                    dataGridView1.CurrentRow.Cells["Tutar"].Value = toplamtutar.ToString("0.00");
                    dataGridView1.CurrentRow.Cells["BitisSaati"].Value = BitisTarihi;
                }
                if (comboSaatUcreti.Text=="")
                {
                    MessageBox.Show("Saat Ücreti belirtiniz.!","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            if(e.ColumnIndex == dataGridView1.Columns["MasaKapat"].Index)
            {
                if (dataGridView1.CurrentRow.Cells["BitisSaati"].Value!=null)
                {
                    frmMasaKapat frm = new frmMasaKapat();
                    frm.textBox1.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                    frm.textBox2.Text = dataGridView1.CurrentRow.Cells["_MasaID"].Value.ToString();
                    frm.textBox3.Text = dataGridView1.CurrentRow.Cells["_Masa"].Value.ToString();
                    frm.textBox4.Text = dataGridView1.CurrentRow.Cells["AcilisTuru"].Value.ToString();
                    frm.textBox5.Text = dataGridView1.CurrentRow.Cells["BaslamaSaati"].Value.ToString();
                    frm.textBox6.Text = dataGridView1.CurrentRow.Cells["BitisSaati"].Value.ToString();
                    frm.textBox7.Text = comboSaatUcreti.Text;
                    frm.textBox8.Text = dataGridView1.CurrentRow.Cells["Sure"].Value.ToString();
                    frm.textBox9.Text = dataGridView1.CurrentRow.Cells["Tutar"].Value.ToString();
                    frm.textBox10.Text = dataGridView1.CurrentRow.Cells["_Tarih"].Value.ToString();
                    frm.ShowDialog(); 
                }
                else if (dataGridView1.CurrentRow.Cells["BitisSaati"].Value == null)
                {
                    MessageBox.Show("Önce hesaplama yapılmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }
        string istek = "";

        private void süresizMasaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            istek = "Yöneticiyi çağırıyor. ";
            istekler();

        }

        private void istekler()
        {
            string sorgu = "insert into TBL_Hareketler(KullaniciID,MasaID,Masa,IstekTuru,Aciklama,Tarih) values(" +
                         "'" + Kullanici.KullaniciID + "','" + btn.Text.Substring(5) + "','" + btn.Text + "','" + istek + "','Yapılmadı',@Tarih)";
            SqlCommand komut = new SqlCommand();
            komut.Parameters.AddWithValue("@Tarih",DateTime.Parse(DateTime.Now.ToString()));
            Veritabani.ESG(komut, sorgu);
            Veritabani.ListviewdeKayitlariGoster(listView1);
        }

        private void süresizMasaAçmaİsteğiGönderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            istek = "Süresiz masa açma talebi göndeeriliyor...";
            istekler();
        }

        private void masaDeğiştirmeİsteğiGönderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            istek = "Masa değiştirme isteği gönderildi! ";
            istekler();
        }
        ToolStripMenuItem item;

        private void secilensureyeGore(object sender, EventArgs e)
        {
            item = sender as ToolStripMenuItem;
            istek = item.Text + "dakika masa açma talebi gönderildi";
            istekler();
        }
        int sayac = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if (sayac>29)
            {
                if (comboSaatUcreti.Text != "")
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++) 
                    {

                        DateTime BitisTarihi = DateTime.Now;
                        DateTime BaslangicTarihi = DateTime.Parse(dataGridView1.Rows[i].Cells["BaslamaSaati"].Value.ToString());
                        TimeSpan saatfarki = BitisTarihi - BaslangicTarihi;
                        double saatfarki2 = saatfarki.TotalHours;
                        dataGridView1.Rows[i].Cells["Sure"].Value = saatfarki2.ToString("0.00");
                        double toplamtutar = saatfarki2 * double.Parse(comboSaatUcreti.Text);
                        dataGridView1.Rows[i].Cells["Tutar"].Value = toplamtutar.ToString("0.00");
                        dataGridView1.Rows[i].Cells["BitisSaati"].Value = BitisTarihi; 
                    }
                }
                if (comboSaatUcreti.Text == "")
                {
                    MessageBox.Show("Saat Ücreti belirtiniz.!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnMasaDegistir_Click(object sender, EventArgs e)
        {
            int SepetID = int.Parse(dataGridView1.CurrentRow.Cells["ID"].Value.ToString());
            int MasaID = int.Parse(dataGridView1.CurrentRow.Cells["_MasaID"].Value.ToString());
            string sql = "update TBL_Sepet set MasaID='"+int.Parse(comboBosMasalar.Text.Substring(5))+"',Masa='"+comboBosMasalar.Text+"' where SepetID='"+ SepetID + "'";
            SqlCommand cmd = new SqlCommand();
            Veritabani.ESG(cmd, sql);
            string sql2 = "update TBL_Masalar set Durumu='Boş' where MasaID='"+MasaID+"'";
            SqlCommand cmd2 = new SqlCommand();
            Veritabani.ESG(cmd2, sql2);
            string sql3 = "update TBL_Masalar set Durumu='Dolu' where MasaID='" + int.Parse(comboBosMasalar.Text.Substring(5)) + "'";
            SqlCommand cmd3 = new SqlCommand();
            Veritabani.ESG(cmd3, sql3);
            Yenile();
            MessageBox.Show("Masa değiştirme işlemi gerçekleştirildi.!","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["Sure"].Value !=null)
                {
                    if (dataGridView1.Rows[i].Cells["AcilisTuru"].Value.ToString() !="Süresiz")
                    {
                        double sure = double.Parse(dataGridView1.Rows[i].Cells["Sure"].Value.ToString()) * 60;
                        double Acilisturu = double.Parse(dataGridView1.Rows[i].Cells["AcilisTuru"].Value.ToString());
                        if (Acilisturu - sure<=5.0)
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                }
            }
        }

        private void btnGeriAl_Click(object sender, EventArgs e)
        {
            frmSatislarListele frm = new frmSatislarListele();
            frm.btnGeriAl.Enabled = true;
            frm.ShowDialog();
        }
    }
}
