using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmInwCharges : System.Web.UI.Page
{
    ClsInwCharges IC = new ClsInwCharges();
    CLSIWOWCharges CH = new CLSIWOWCharges();
    DataTable DT = new DataTable();
    ClsBindDropdown BD = new ClsBindDropdown();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtEffectDate.Text = Session["EntryDate"].ToString();
            IC.BindInwCharges(grdCharged);
            SetData("I");
        }
    }
    protected void SetData(string FL)
    {
        try
        {
            DT = CH.GetIWOWCharges(FL);
            if (DT.Rows.Count > 0)
            {
                //--DESCRIPTION	CHARGES	PLACC	ST_SUBGL	LASTAPPLY
                TxtLastApplyDt.Text = DT.Rows[0]["LASTAPPLY"].ToString().Replace("12:00:00", "");
                TxtPlacc.Text = DT.Rows[0]["PLACC"].ToString();
                TxtSTax.Text = DT.Rows[0]["ST_SUBGL"].ToString();
                TxtCharges.Text = DT.Rows[0]["CHARGES"].ToString();
                if (TxtPlacc.Text != null)
                {
                    string STR = BD.GetAccTypeGL(TxtPlacc.Text, Session["BRCD"].ToString());
                    string[] CU = STR.Split('_');
                    TxtPLName.Text = CU[0].ToString();

                }
                if (TxtSTax.Text != null)
                {
                    TxtSTaxName.Text = BD.GetAccType(TxtSTax.Text, Session["BRCD"].ToString());
                }

            }
            else
            {
                lblMessage.Text = "";
                lblMessage.Text = "Charges Master not Found....!";
                ModalPopup.Show(this.Page);
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_Clear_Click(object sender, EventArgs e)
    {
        ClearData();
    }

    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    public void ClearData()
    {
       
    }
}