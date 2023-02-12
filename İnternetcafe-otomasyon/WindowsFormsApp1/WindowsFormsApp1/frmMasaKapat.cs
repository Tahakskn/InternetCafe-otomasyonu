using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmMasaKapat : Form
    {
        public frmMasaKapat()
        {
            InitializeComponent();
        }

        private void btnMasaKapat_Click(object sender, EventArgs e)
        {
            string sorgu = "insert into TBL_Satis(KullaniciID,MasaID,AcilisTuru,BaslangicSaati,BitisSaati,Sure,Tutar,Aciklama,Tarih) " +
                "values('"+Kullanici.KullaniciID+"','"+int.Parse(textBox2.Text)+"','"+textBox4.Text+"',@Baslangic,@Bitis,@Sure,@Tutar,'Yapılmadı',@Tarih)";
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Baslangic", DateTime.Parse(textBox5.Text));
            cmd.Parameters.AddWithValue("@Bitis", DateTime.Parse(textBox6.Text));
            cmd.Parameters.AddWithValue("@Sure", decimal.Parse(textBox8.Text));
            cmd.Parameters.AddWithValue("@Tutar", decimal.Parse(textBox9.Text));
            cmd.Parameters.AddWithValue("@Tarih", DateTime.Parse(textBox10.Text));
            Veritabani.ESG(cmd, sorgu);
            this.Close();
            frmSatis frm = (frmSatis)Application.OpenForms["frmSatis"];
            frm.Yenile();
        }
         
        private void btnCikis_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}
