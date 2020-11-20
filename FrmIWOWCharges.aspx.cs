using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class FrmIWOWCharges : System.Web.UI.Page
{
    CLSIWOWCharges CH = new CLSIWOWCharges();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCommon CM = new ClsCommon();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    int Result = 0;
    string STR = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            TxtEffectDate.Text = Session["EntryDate"].ToString();
            SetData("I");
            TxtLastApplyDt.Text = CH.GetLastAppliedDate(Session["BRCD"].ToString());
        }
    }
    protected void ENDN(bool TF)
    {
        TxtEffectDate.Enabled = TF;
        TxtPlacc.Enabled = TF;
        TxtSTax.Enabled = TF;
        TxtCharges.Enabled = TF;
        DdlReturnType.Enabled = TF;
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
    protected void DdlReturnType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (DdlReturnType.SelectedValue == "1")
            {
                SetData("I");
            }

            else
            {
                lblMessage.Text = "";
                lblMessage.Text = "Select Return Type .........!";
                ModalPopup.Show(this.Page);
                DdlReturnType.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Btn_Apply_Click(object sender, EventArgs e)
    {
        try
        {
            string RES = CH.PostCharges("POST", Session["EntryDate"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
            int value;
            if (int.TryParse(RES, out value))
            {
                WebMsgBox.Show("Charges Posted Successfully with Setno : " + RES, this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ClearingRetcharges_Apply _"  + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
            else
            {
                WebMsgBox.Show( RES +"on" +TxtEffectDate.Text, this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    
    protected void Btn_TrailRun_Click(object sender, EventArgs e)
    {
        try
        {
            ////Result = CH.GetTrialRunGrid(Grd_Trail, "INW", "TRAILSHOW", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), TxtCharges.Text, Session["MID"].ToString());
            Result = CH.BindTrailRun("TRAIL", Grd_Trail, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ClearingRetcharges_Trail _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            if (Result <= 0)
            {
                lblMessage.Text = "";
                lblMessage.Text = "No Return Cheques Available....!";
                ModalPopup.Show(this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }

    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }


    protected void Btn_Report_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ClearingRetcharges_Rpt _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

             string redirectURL = "FrmRView.aspx?BRCD="+Session["BRCD"].ToString()+"&FLAG=TRAIL&EDT="+Session["EntryDate"].ToString()+"&rptname=RptIWOWCharges.rdlc";
             ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}