using DevExpress.XtraEditors;
using Sistema2Eletro.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Sistema2Eletro
{
    public partial class ucBradescoGNRE : DevExpress.XtraEditors.XtraUserControl
    {
        private static ucBradescoGNRE _instance;

        public static ucBradescoGNRE instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucBradescoGNRE();
                return _instance;
            }
        }


        public ucBradescoGNRE()
        {
            InitializeComponent();
        }

        private void editXML_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(dlgFIle.ShowDialog()==DialogResult.OK)
            {
                string sTextoXMl = File.ReadAllText(dlgFIle.FileName);
                var sr = new System.IO.StringReader(sTextoXMl);
                var xs = new XmlSerializer(typeof(TLote_GNRE));

                TLote_GNRE oResult = (TLote_GNRE)xs.Deserialize(sr);

                decimal eTotal = 0;

                foreach(TLote_GNRETDadosGNRE oGNRE in oResult.guias)
                {
                    eTotal += oGNRE.valorGNRE;
                }


            }
        }
    }
}
