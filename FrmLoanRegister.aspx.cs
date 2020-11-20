using iTextSharp.xmp.impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmLoanRegister : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            autoglname.ContextKey = Session["BRCD"].ToString();
        }
    }
    #region TEXT CHANGE
    protected void txtLType_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if (txtLType.Text == "")
            {
                txtLType.Focus();
                txtLType.Text = "";
            }
            int result = 0;
            string GlS1;
            int.TryParse(txtLType.Text, out result);
            txtLCode.Text = customcs.GetProductName(result.ToString(), Session["BRCD"].ToString());
            GlS1 = BD.GetAccTypeGL(txtLType.Text, Session["BRCD"].ToString());
            if (GlS1 != null)
            {
                string[] GLS = GlS1.Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                int GL = 0;
                int.TryParse(ViewState["DRGL"].ToString(), out GL);
                btnReport.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter Valid Product code!....", this.Page);
                txtLType.Text = "";
                txtLCode.Text = "";
                txtLType.Focus();
            }
        }
        catch (Exception eX)
        {
            ExceptionLogging.SendErrorToText(eX);
        }

    }
    protected void txtLCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtLCode.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtLCode.Text = CT[0].ToString();
                txtLType.Text = CT[1].ToString();
                //TxtGLCD.Text = CT[2].ToString();
                string[] GLS = BD.GetAccTypeGL(txtLType.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["TGL"] = GLS[1].ToString();

                int GL = 0;
                int.TryParse(ViewState["TGL"].ToString(), out GL);

                if (txtLCode.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    txtLType.Text = "";
                    txtLType.Focus();

                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmRView.aspx?FDAT=" + txtFDT.Text + "&TDAT=" + txtTDT.Text + "&PRD=" + txtLType.Text + "&BRCD=" + Session["BRCD"].ToString() + "&UNAME="+Session["USERNAME"].ToString()+"&rptname=RptLoanRegister.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}