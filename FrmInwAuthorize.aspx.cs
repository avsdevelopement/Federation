using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmInwAuthorize : System.Web.UI.Page
{
    ClsOutAuth CLSOUTAUTH = new ClsOutAuth();
    ClsCommon CM = new ClsCommon();
    int result = 0;
    DataSet dt = new DataSet();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            BindGrid();
            BindUnpassGrid();
            if (Rdb_Single.Checked == true)
            {
                Div_Lot.Visible = false;
                Div_Single.Visible = true;
                btnSearch.Visible = true;
                Btn_Submit.Visible = false;
                TxtInstNo.Focus();
            }
            else
            {
                Div_Lot.Visible = true;
                Div_Single.Visible = false;
                btnSearch.Visible = false;
                Btn_Submit.Visible = true;
                TxtFSetno.Focus();
            }
        }
    }


    public void BindGrid()
    {
        CLSOUTAUTH.Getinfotable(grdOwgData, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "I");
    }
    public void BindUnpassGrid()
    {
        CLSOUTAUTH.GetUpassInfo(Grd_rejected, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "I");
    }

    public void NewWindows(string url)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=1000,height=600,left=50,top=50,resizable=no');", true);
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumid = objlink.CommandArgument;

            // Get SetNo and ScrollNo from Linkbuton
            string[] setscroll = strnumid.Split('-');


            string MID = CM.GetIOMid("IW", Session["BRCD"].ToString(), setscroll[0].ToString(), Session["EntryDate"].ToString());
            if (MID != null && MID != Session["MID"].ToString())
            {
                string url = "frmInwordAuthoDo.aspx" + "?setno=" + setscroll[0].ToString() + "&scrollno=" + setscroll[1].ToString() + "&InstNo=" + setscroll[2].ToString() + "&op=authorize";
                NewWindows(url);
            }
            else
            {
                WebMsgBox.Show("Warning : User " + Session["LOGINCODE"].ToString() + " is restricted to authorized...!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton objlink = (LinkButton)sender;
        string strnumid = objlink.CommandArgument;

        // Get SetNo and ScrollNo from Linkbuton
        string[] setscroll = strnumid.Split('-');

        string url = "FrmOutAuthDo.aspx" + "?setno=" + setscroll[0] + "&scrollno=" + setscroll[1] + "&op=delete";
        NewWindows(url);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (TxtInstNo.Text == "")
        {
            BindGrid();
        }
        else
        {
            CLSOUTAUTH.GetinfotableInstNo(grdOwgData, Session["MID"].ToString(), Session["BRCD"].ToString(), TxtInstNo.Text.ToString());
        }
    }
    protected void grdOwgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdOwgData.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void TxtInstNo_TextChanged(object sender, EventArgs e)
    {

    }


    protected void LnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumid = objlink.CommandArgument;

            // Get SetNo and ScrollNo from Linkbuton
            string[] setscroll = strnumid.Split('-');

            string MID = CM.GetIOMid("IW", Session["BRCD"].ToString(), setscroll[0].ToString(), Session["EntryDate"].ToString());
            if (MID != null && MID != Session["MID"].ToString())
            {
                string url = "frmInwardclear.aspx" + "?setno=" + setscroll[0] + "&scrollno=" + setscroll[1] + "&FLAG=UN";
                NewWindows(url);
            }
            else
            {
                WebMsgBox.Show("Warning : User " + Session["LOGINCODE"].ToString() + " is restricted to authorized...!", this.Page);
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Grd_rejected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grd_rejected.PageIndex = e.NewPageIndex;
        BindUnpassGrid();
    }
    protected void Rdb_Single_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            
                Div_Lot.Visible = false;
                Div_Single.Visible = true;
                btnSearch.Visible = true;
                Btn_Submit.Visible = false;
                TxtInstNo.Focus();
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Rdb_Lot_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            
                Div_Lot.Visible = true;
                Div_Single.Visible = false;
                btnSearch.Visible = false;
                Btn_Submit.Visible = true;
                TxtFSetno.Focus();
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            result=CLSOUTAUTH.UpdtLotSt("IWC",Session["EntryDate"].ToString(),TxtFSetno.Text,TxtTSetno.Text,Session["BRCD"].ToString(),Session["MID"].ToString());
            if (result > 0)
            {
                WebMsgBox.Show("Set Numbers From " + TxtFSetno.Text + " to " + TxtTSetno.Text + " authorised successfully..!!", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_Autho _" + TxtFSetno.Text + "_" + TxtTSetno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
               TxtFSetno.Text="";
               TxtTSetno.Text = "";
               BindGrid();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}