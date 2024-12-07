using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class GeneralObjects : Form
    {
        SAPbobsCOM.BusinessPartners oBP;

        public GeneralObjects()
        {
            InitializeComponent();
        }

        // the whole window
        private void GeneralObjects_Load(object sender, EventArgs e)
        {
            SAPbobsCOM.Recordset oRec;

            // Business Partner, from Form 1
            oBP = (SAPbobsCOM.BusinessPartners)Form1.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

            // Record Set, from form 1
            oRec = (SAPbobsCOM.Recordset)Form1.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            // Assign query to oRec
            oRec.DoQuery("SELECT * FROM OCRD WHERE CardType = 'C'");

            // Assign the ORec to the record set of the browser in Business Partner in Form 1
            oBP.Browser.Recordset = oRec;

            this.textBoxGen.Clear();
            oBP.Browser.MoveFirst();
            this.textBoxGen.Text = oBP.CardCode;
        }

        private void First_Click(object sender, EventArgs e)
        {
            oBP.Browser.MoveFirst();
            this.textBoxGen.Text = oBP.CardCode;
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            try
            {
                oBP.Browser.MovePrevious();
                this.textBoxGen.Text = oBP.CardCode;
            }
            catch {
                oBP.Browser.MoveFirst();
                this.textBoxGen.Text = oBP.CardCode;
            }
        }

        private void Next_Click(object sender, EventArgs e)
        {
            oBP.Browser.MoveNext();
            this.textBoxGen.Text = oBP.CardCode;
        }

        private void Last_Click(object sender, EventArgs e)
        {
            oBP.Browser.MoveLast();
            this.textBoxGen.Text = oBP.CardCode;
        }
    }
}
