using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmDPLNRegister : System.Web.UI.Page
{
    ClsOpenClose OC = new ClsOpenClose();
    ClsRegisterReport RR = new ClsRegisterReport();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsBindBrDetails ASM = new ClsBindBrDetails();
    decimal TotalBal = 0;
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string bankcd,ECSYN, FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                TxtDate.Text = Session["EntryDate"].ToString();
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }

    }
    protected void BtnLoan_Click(object sender, EventArgs e)
    {
        try
        {
            gridadd.Visible = true;
            Griddeposit.Visible = false;
            BtnLoanR.Visible = true;
            BtnDepositR.Visible = false;
            btnDDSR.Visible = false;
            gridDDS.Visible = false;
            //string[] TD = TxtDate.Text.Split('/');
            //DataTable DT = RR.GetInfo(Session["BRCD"].ToString(), TxtSubgl.Text);
            //if (DT.Rows.Count > 0)
            //{
            //    for (int i = 0; i <= DT.Rows.Count - 1; i++)
            //    {
            //        double Balance = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), DT.Rows[i]["LOANGLCODE"].ToString(), DT.Rows[i]["custaccno"].ToString(), Session["BRCD"].ToString(), TxtDate.Text.ToString(), ViewState["GL"].ToString());
            //        if (Balance != 0)
            //        {
            //            int RC = RR.InsertData(DT.Rows[i]["LOANGLCODE"].ToString(), DT.Rows[i]["custno"].ToString(), DT.Rows[i]["custaccno"].ToString(), DT.Rows[i]["limit"].ToString(), Convert.ToDateTime(DT.Rows[i]["duedate"]).ToString("dd/MM/yyyy"), DT.Rows[i]["intRate"].ToString(), Convert.ToDateTime(DT.Rows[i]["sanssiondate"]).ToString("dd/MM/yyyy"), DT.Rows[i]["installment"].ToString(), DT.Rows[i]["period"].ToString(), Balance.ToString());
            //            if (RC > 0)
            //            {
            //            }
            //        }
            //    }
            //}
            BindGrid();
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DPLNReg_Grd_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindGrid()
    {
        try
        {
            int RC = RR.BindADD(gridadd, TxtSubgl.Text, Session["BRCD"].ToString(), TxtDate.Text);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void gridadd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gridadd.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnDeposit_Click(object sender, EventArgs e)
    {
        try
        {
            gridadd.Visible = false;
            Griddeposit.Visible = true;
            BtnLoanR.Visible = false;
            BtnDepositR.Visible = true;
            btnDDSR.Visible = false;
            gridDDS.Visible = false;
            //string[] TD = TxtDate.Text.Split('/');
            //DataTable DT = RR.DepositInfo(Session["BRCD"].ToString(), TxtSubgl.Text);
            //if (DT.Rows.Count > 0)
            //{
            //    for (int i = 0; i <= DT.Rows.Count - 1; i++)
            //    {
            //        double Balance = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), DT.Rows[i]["DEPOSITGLCODE"].ToString(), DT.Rows[i]["custaccno"].ToString(), Session["BRCD"].ToString(), TxtDate.Text.ToString(), ViewState["GL"].ToString());
            //        if (Balance != 0)
            //        {
            //            int RC = RR.insertdeposit(DT.Rows[i]["custno"].ToString(), DT.Rows[i]["custaccno"].ToString(), DT.Rows[i]["DEPOSITGLCODE"].ToString(), DT.Rows[i]["prnamt"].ToString(), DT.Rows[i]["rateofint"].ToString(), Convert.ToDateTime(DT.Rows[i]["openingdate"]).ToString("dd/MM/yyyy"), Convert.ToDateTime(DT.Rows[i]["duedate"]).ToString("dd/MM/yyyy"), DT.Rows[i]["period"].ToString(), DT.Rows[i]["intamt"].ToString(), DT.Rows[i]["maturityamt"].ToString(), Balance.ToString());
            //            if (RC > 0)
            //            {
            //            }
            //        }
            //    }
            //}
            BindGrid1();
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DPLNReg_Grd_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindGrid1()
    {
        try
        {
            int RC = RR.BindDepo(Griddeposit, TxtSubgl.Text, Session["BRCD"].ToString(), TxtDate.Text);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtSubgl_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] GL = BD.GetAccTypeGL(TxtSubgl.Text, Session["BRCD"].ToString()).Split('_');
            TxtSGLName.Text = GL[0].ToString();
            ViewState["GL"] = GL[1].ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Griddeposit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Griddeposit.PageIndex = e.NewPageIndex;
            BindGrid1();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnLoanR_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DPLNReg_Rpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string redirectURL = "FrmRView.aspx?rptname=RptLoanReg.rdlc&FL=LN&Tdate=" + TxtDate.Text + "&PType=" + TxtSubgl.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnDepositR_Click(object sender, EventArgs e)
    {
        ECSYN = ASM.GetECSYN (Session["BRCD"].ToString());
        try
        {
            if (ECSYN == "Y")
            {
                FL = "Insert";//ankita 14/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DPLNReg_Rpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?rptname=RptDepositReg_EMP.rdlc&FL=DP&Tdate=" + TxtDate.Text + "&PType=" + TxtSubgl.Text;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {
                // Added for check category wise
                string Category = RR.GetCategory(Session["BRCD"].ToString(), TxtSubgl.Text.ToString()).ToString();
                if (Category == "RD" || Category == "DD" || Category == "LKP" || Category == "CUM")
                {
                    FL = "Insert";//ankita 14/09/2017
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DPLNReg_Rpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    string redirectURL = "FrmRView.aspx?rptname=RptDepositReg_Category.rdlc&FL=DP&Tdate=" + TxtDate.Text + "&PType=" + TxtSubgl.Text;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else
                {
                    FL = "Insert";//ankita 14/09/2017
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DPLNReg_Rpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    string redirectURL = "FrmRView.aspx?rptname=RptDepositReg.rdlc&FL=DP&Tdate=" + TxtDate.Text + "&PType=" + TxtSubgl.Text;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnDSS_Click(object sender, EventArgs e)
    {

        gridadd.Visible = false;
        Griddeposit.Visible = false;
        BtnLoanR.Visible = false;
        BtnDepositR.Visible = false;
        btnDDSR.Visible = true;
        gridDDS.Visible = true;
        BindDDS();
        FL = "Insert";//ankita 14/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DPLNReg_Rpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
    }
    public void BindDDS()
    {
        try
        {
            int RC = RR.BindDDS(gridDDS, TxtSubgl.Text, Session["BRCD"].ToString(), TxtDate.Text);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnDDSR_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DPLNReg_Rpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string redirectURL = "FrmRView.aspx?rptname=RptDDSR.rdlc&BRCD=" + Session["BRCD"] + "&FDate=" + TxtDate.Text + "&UserName=" + Session["UserName"].ToString() + "&PType=" + TxtSubgl.Text + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void gridDDS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gridDDS.PageIndex = e.NewPageIndex;
            BindDDS();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void gridDDS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalBal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Closing"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            ((Label)e.Row.FindControl("lblBal")).Text = TotalBal.ToString();
        }
    }
}