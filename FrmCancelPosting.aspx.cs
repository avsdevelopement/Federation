using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmCancelPosting : System.Web.UI.Page
{
    Cls_RecoBindDropdown BD = new Cls_RecoBindDropdown();
    ClsCancelPosting BLCP = new ClsCancelPosting();
    string CMid = "", CPaymast = "";
    int Res = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBr();
            ddlBrCode.SelectedValue = Session["BRCD"].ToString();
            TxtEntryDate.Text = Session["EntryDate"].ToString();
            TxtSetNo.Focus();
        }
    }

    public void BindBr()
    {
        BD.Ddl = ddlBrCode;
        BD.FnBL_BindDropDown(BD);
        ddlBrCode.SelectedValue = Session["BRCD"].ToString();// Amruta 
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
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
            if (TxtSetNo.Text == "")
                BLCP.Flag = "ShowAll";
            else
                BLCP.Flag = "Show";

            BLCP.GD = Grd_EntryDetail;
            BLCP.Mid = Session["MID"].ToString();
            BLCP.Brcd = Session["BRCD"].ToString();
            BLCP.Edt = TxtEntryDate.Text;
            BLCP.Setno = TxtSetNo.Text;

            Res = BLCP.FnBl_BindData(BLCP);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string setno = objlink.CommandArgument;
            ViewState["SetNo"] = setno.ToString();

            BLCP.Flag = "GetMID";
            BLCP.Mid = Session["MID"].ToString();
            BLCP.Brcd = Session["BRCD"].ToString();
            BLCP.Edt = TxtEntryDate.Text;
            BLCP.Setno = ViewState["SetNo"].ToString();
            CMid = BLCP.FnBl_GetData(BLCP);
            if (CMid != null)
            {
                if (CMid == Session["MID"].ToString())
                {
                    WebMsgBox.Show("Not allowed for current user, Chnage user first...!", this.Page);
                    return;
                }
                else
                {
                    CancelProcess();
                }
            }
            else
            {
                WebMsgBox.Show("Error while searching Maker ID...!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void CancelProcess()
    {
        try
        {
            BLCP.Flag = "GetPayMast";
            BLCP.Mid = Session["MID"].ToString();
            BLCP.Brcd = Session["BRCD"].ToString();
            BLCP.Edt = TxtEntryDate.Text;
            BLCP.Setno = ViewState["SetNo"].ToString();
            
            CPaymast = BLCP.FnBl_GetData(BLCP);
            if (CPaymast != null)
            {
                if (CPaymast == "RECO")
                {
                    BLCP.Flag = CPaymast;
                    BLCP.Mid = Session["MID"].ToString();
                    BLCP.Brcd = Session["BRCD"].ToString();
                    BLCP.Edt = TxtEntryDate.Text;
                    BLCP.Setno = ViewState["SetNo"].ToString();
                    BLCP.Paymast = CPaymast;

                    Res = BLCP.FnBl_CancelPosting(BLCP);
                    if (Res > 0)
                    {
                        BindGrid();
                        WebMsgBox.Show("Setno " + ViewState["SetNo"].ToString() + " successfully canceled ...!!", this.Page);
                        return;
                    }
                    else
                    {
                        WebMsgBox.Show("Error occured while canceling ...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    WebMsgBox.Show("Only for a recovery voucher ...!!", this.Page);
                    return;
                }
            }
            else
            {
                WebMsgBox.Show("Error while searching data ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}