using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmODList : System.Web.UI.Page
{
    DbConnection Conn = new DbConnection();
    ClsBindDropdown DD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }

                DD.BindSecActivity(DdlActivity);
                DD.BindAccActivity(DdlAccActivity);
                DD.DdlODActivity(DdlODActivity);
                DD.DdlODInstActivity(DdlODInstActivity);
                TxtFBrID.Focus();
                //added by ankita 07/10/2017 to make user frndly
                TxtAsonDate.Text = Session["EntryDate"].ToString();
                TxtFBrID.Text = Session["BRCD"].ToString();
                TxtTBrID.Text = Session["BRCD"].ToString();
                
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime AsOnDate = Convert.ToDateTime(Conn.ConvertDate(TxtAsonDate.Text.ToString()).ToString()).Date;
            DateTime WorkDate = Convert.ToDateTime(Conn.ConvertDate(Session["EntryDate"].ToString()).ToString()).Date;
            string Dex1 = "";
            if (AsOnDate < WorkDate)
            {
                if (CHK_With_Address.Checked == true)
                {
                    Dex1 = "Y";
                }
                else
                {
                    Dex1 = "N";
                }

                if ((Rdeatils.SelectedValue == "1") && (Dex1 == "N"))
                {
                    string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBrID.Text + "&TBRCD=" + TxtTBrID.Text + "&FPRCD=" + TxtFSubgl.Text + "&TPRCD=" + TxtTSubgl.Text + "&FAccNo=" + TxtFACID.Text + "&TAccNo=" + TxtTACID.Text + "&Date=" + TxtAsonDate.Text + "&AType=" + DdlActivity.SelectedValue + "&Reference=" + TxtRef.Text + "&ODAmt=" + TxtODAmt.Text + "&ODInst=" + TxtODInst.Text + "&FSanction=" + TxtFSan.Text + "&TSanction=" + TxtTSan.Text + "&FSantionDT=" + TxtFDate.Text + "&TSantionDT=" + TxtTDate.Text + "&S1Type=" + DdlODActivity.SelectedValue + "&S2Type=" + DdlODInstActivity.SelectedValue + "&AccType=" + rbtnType.SelectedValue + "&AccStatus=" + DdlAccActivity.SelectedValue + "&DEX=" + Dex1 + " &rptname=RptODAccountList.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                if ((Rdeatils.SelectedValue == "1") && (Dex1 == "Y"))
                {
                    string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBrID.Text + "&TBRCD=" + TxtTBrID.Text + "&FPRCD=" + TxtFSubgl.Text + "&TPRCD=" + TxtTSubgl.Text + "&FAccNo=" + TxtFACID.Text + "&TAccNo=" + TxtTACID.Text + "&Date=" + TxtAsonDate.Text + "&AType=" + DdlActivity.SelectedValue + "&Reference=" + TxtRef.Text + "&ODAmt=" + TxtODAmt.Text + "&ODInst=" + TxtODInst.Text + "&FSanction=" + TxtFSan.Text + "&TSanction=" + TxtTSan.Text + "&FSantionDT=" + TxtFDate.Text + "&TSantionDT=" + TxtTDate.Text + "&S1Type=" + DdlODActivity.SelectedValue + "&S2Type=" + DdlODInstActivity.SelectedValue + "&AccType=" + rbtnType.SelectedValue + "&AccStatus=" + DdlAccActivity.SelectedValue + "&DEX=" + Dex1 + " &rptname=RptODAccountList_Address.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                if (Rdeatils.SelectedValue == "2")
                {
                    string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBrID.Text + "&TBRCD=" + TxtTBrID.Text + "&FPRCD=" + TxtFSubgl.Text + "&TPRCD=" + TxtTSubgl.Text + "&FAccNo=" + TxtFACID.Text + "&TAccNo=" + TxtTACID.Text + "&Date=" + TxtAsonDate.Text + "&AType=" + DdlActivity.SelectedValue + "&Reference=" + TxtRef.Text + "&ODAmt=" + TxtODAmt.Text + "&ODInst=" + TxtODInst.Text + "&FSanction=" + TxtFSan.Text + "&TSanction=" + TxtTSan.Text + "&FSantionDT=" + TxtFDate.Text + "&TSantionDT=" + TxtTDate.Text + "&S1Type=" + DdlODActivity.SelectedValue + "&S2Type=" + DdlODInstActivity.SelectedValue + "&AccType=" + rbtnType.SelectedValue + "&AccStatus=" + DdlAccActivity.SelectedValue + " &rptname=RptODAccountListSumry.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                if (Rdeatils.SelectedValue == "3")
                {
                    string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBrID.Text + "&TBRCD=" + TxtTBrID.Text + "&FPRCD=" + TxtFSubgl.Text + "&TPRCD=" + TxtTSubgl.Text + "&FAccNo=" + TxtFACID.Text + "&TAccNo=" + TxtTACID.Text + "&Date=" + TxtAsonDate.Text + "&AType=" + DdlActivity.SelectedValue + "&Reference=" + TxtRef.Text + "&ODAmt=" + TxtODAmt.Text + "&ODInst=" + TxtODInst.Text + "&FSanction=" + TxtFSan.Text + "&TSanction=" + TxtTSan.Text + "&FSantionDT=" + TxtFDate.Text + "&TSantionDT=" + TxtTDate.Text + "&S1Type=" + DdlODActivity.SelectedValue + "&S2Type=" + DdlODInstActivity.SelectedValue + "&AccType=" + rbtnType.SelectedValue + "&AccStatus=" + DdlAccActivity.SelectedValue + " &rptname=RptAllLnBalList_DDSFD.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                if (Rdeatils.SelectedValue == "4")
                {
                    string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBrID.Text + "&TBRCD=" + TxtTBrID.Text + "&FPRCD=" + TxtFSubgl.Text + "&TPRCD=" + TxtTSubgl.Text + "&FAccNo=" + TxtFACID.Text + "&TAccNo=" + TxtTACID.Text + "&Date=" + TxtAsonDate.Text + "&AType=" + DdlActivity.SelectedValue + "&Reference=" + TxtRef.Text + "&ODAmt=" + TxtODAmt.Text + "&ODInst=" + TxtODInst.Text + "&FSanction=" + TxtFSan.Text + "&TSanction=" + TxtTSan.Text + "&FSantionDT=" + TxtFDate.Text + "&TSantionDT=" + TxtTDate.Text + "&S1Type=" + DdlODActivity.SelectedValue + "&S2Type=" + DdlODInstActivity.SelectedValue + "&AccType=" + rbtnType.SelectedValue + "&AccStatus=" + DdlAccActivity.SelectedValue + " &rptname=RptAllLnBalList_CustRef.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            else
            {
                WebMsgBox.Show("DayEnd Not Complete...", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    public void ClearData()
    {
        try
        {
            DdlAccActivity.SelectedValue = "0";
            TxtFBrID.Text = "";
            TxtTBrID.Text = "";
            TxtFSubgl.Text = "";
            TxtTSubgl.Text = "";
            TxtFACID.Text = "";
            TxtTACID.Text = "";
            TxtAsonDate.Text = "";
            TxtRef.Text = "";
            DdlODActivity.SelectedValue = "0";
            TxtODAmt.Text = "";
            DdlODInstActivity.SelectedValue = "0";
            TxtODInst.Text = "";
            TxtFSan.Text = "";
            TxtTSan.Text = "";
            TxtFDate.Text = "";
            TxtTDate.Text = "";
            DdlActivity.SelectedValue = "0";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
}