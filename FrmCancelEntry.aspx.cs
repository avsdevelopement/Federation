using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmCancelEntry : System.Web.UI.Page
{
    ClsCancelEntry CE = new ClsCancelEntry();
    DbConnection conn = new DbConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //TxtEntryDate.Text = Session["EntryDate"].ToString();
           

            //TxtChequeDate.Text = Session["EntryDate"].ToString();
            // autoglname.ContextKey = Session["BRCD"].ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
    }
    protected void RdbChkSetno_CheckedChanged(object sender, EventArgs e)
    {
        EDDIV.Visible = false;
        VNDIV.Visible = true;
        btnSubmit.Text = "Show by Voucher";
        TxtSetNo.Focus();
    }
    protected void RdbChkEDT_CheckedChanged(object sender, EventArgs e)
    {
        EDDIV.Visible = true;
        VNDIV.Visible = false;
        btnSubmit.Text = "Show by Date";
        TxtEntryDate.Focus();

    }
    protected void RdbChkBoth_CheckedChanged(object sender, EventArgs e)
    {
        EDDIV.Visible = true;
        VNDIV.Visible = true;
        TxtSetNo.Focus();
    }

 

    public void BindGrid()
    {
        try
        {
            CE.Getinfotable(grdShow, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(),"OTH");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            BindByVoucher();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindByVoucher()
    {
        CE.GetByVoucherGrid(Session["BRCD"].ToString(), Session["MID"].ToString(), grdShow, TxtSetNo.Text, TxtEntryDate.Text,"OTH");
    }


    protected void grdShow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdShow.PageIndex = e.NewPageIndex;
            BindByVoucher();
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
            //string brcd = Session["BRCD"].ToString();
            ViewState["SetNo"] = setno.ToString();
            CallUpdate();
            Clear();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void Clear()
    {
        TxtSetNo.Text = "";
        TxtEntryDate.Text = "";
    }

    protected void CallUpdate()
    {
        try
        {
            string RC = CheckStage();
            string sql;
            int result;

            if (RC != "1003")
            {
                result = CE.CancelVoucher(ViewState["SetNo"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(),"","","","","");
                if (result > 0)
                {
                    WebMsgBox.Show("Entry Canceled for Voucher No-" + ViewState["SetNo"].ToString() + "....", this.Page);
                    BindGrid();
                }
            }
            else
            {
                WebMsgBox.Show("The Voucher is already Authorized, Cannot delete!.....", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string CheckStage()
    {
        string RC = "";
        string ed = Session["EntryDate"].ToString();
        try
        {
            string sql = "select Stage from ALLVCR where SETNO='" + ViewState["SetNo"].ToString() + "' and EntryDate='" + conn.ConvertDate(ed) + "' and STAGE<>1004";
            RC = conn.sExecuteScalar(sql);
            return RC;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RC;
        }

    }
}