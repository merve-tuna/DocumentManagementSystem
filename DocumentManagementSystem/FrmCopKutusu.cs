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
    public partial class FrmCopKutusu : Form
    {
        public FrmCopKutusu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); // Bu sayfayı kapatır (Böylece ana sayfa otomatik açılır)
        }
    }
}
