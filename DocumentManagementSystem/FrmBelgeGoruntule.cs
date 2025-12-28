using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentManagementSystem
{
    public partial class FrmBelgeGoruntule : Form
    {
        public FrmBelgeGoruntule()
        {
            InitializeComponent();
        }

        // Bu metodu ana formdan çağıracağız
        public void BelgeBilgisiAta(string belgeAdi)
        {
            // Formun üst başlığını (pencere adını) değiştiriyoruz
            this.Text = $"{belgeAdi} - Belge İçeriği";

            // İstersen içerideki zengin metin kutusuna da başlık atabilirsin
            // richTextBox1.Text = "BELGE ADI: " + belgeAdi + "\n\n" + richTextBox1.Text;
        }
    }
}