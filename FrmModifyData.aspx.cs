using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmModifyData : System.Web.UI.Page
{
    Cls_RecoBindDropdown BD = new Cls_RecoBindDropdown();

    //Cls_BL_RDM_WebService RWS = new Cls_BL_RDM_WebService();
    //ClsBL_RecoveryStatement RS = new ClsBL_RecoveryStatement();

    ClsRecoveryOperation RO = new ClsRecoveryOperation();
    DbConnection Conn = new DbConnection();
    int Res = 0;
    string MM, YY, EDT, STRRes;
    DataTable dt = new DataTable();
    double TotBal = 0, TotInst = 0, TotIntr = 0, SumFooterBal = 0, SumFooterInst = 0, SumFooterIntr = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["BRCD"] = Request.QueryString["BRCD"].ToString();
            ViewState["MM"] = Request.QueryString["MM"].ToString();
            ViewState["YY"] = Request.QueryString["YY"].ToString();
            ViewState["RECCODE"] = Request.QueryString["RECCODE"].ToString();
            ViewState["RECDIV"] = Request.QueryString["RECDIV"].ToString();
            ViewState["CUSTNO"] = Request.QueryString["CNO"].ToString();
            ViewState["ID"] = Request.QueryString["ID"].ToString();
            ViewState["BANKCODE"] = Request.QueryString["BKCD"].ToString();
            ViewState["FL"] = Request.QueryString["FL"].ToString();
            BindGrid(ViewState["FL"].ToString());
        }
    }

    public void BindGrid(string FL)
    {
        try
        {
            if (FL == "SENDMOD")
            {
                RO.CustNO = ViewState["CUSTNO"].ToString();
                RO.BRCD = Session["BRCD"].ToString();
                RO.RECDIV = ViewState["RECDIV"].ToString();
                RO.RECCODE = ViewState["RECCODE"].ToString();
                RO.MID = Session["MID"].ToString();
                RO.MM = ViewState["MM"].ToString();
                RO.YY = ViewState["YY"].ToString();
                RO.FL = "SELMOD";
                RO.BANKCODE = ViewState["BANKCODE"].ToString();
                RO.GRD = Grid_SingleModify;
                RO.ASONDT = Session["EntryDate"].ToString();

                Res = RO.FnBl_BindSingle(RO);
            }
            else if (FL=="POSTMOD")
            {
                RO.CustNO = ViewState["CUSTNO"].ToString();
                RO.BRCD = Session["BRCD"].ToString();
                RO.RECDIV = ViewState["RECDIV"].ToString();
                RO.RECCODE = ViewState["RECCODE"].ToString();
                RO.MID = Session["MID"].ToString();
                RO.MM = ViewState["MM"].ToString();
                RO.YY = ViewState["YY"].ToString();
                RO.FL = "SELPOSTMOD";
                RO.BANKCODE = ViewState["BANKCODE"].ToString();
                RO.GRD = Grid_SingleModify;
                RO.ASONDT = Session["EntryDate"].ToString();

                Res = RO.FnBl_BindSingle(RO);
            }
        }
        catch (Exception Ex)
        {
            
        }
    }

    protected void BtnModal_Modify_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow GR in Grid_SingleModify.Rows)
            {
                Label lb = (Label)Grid_SingleModify.Rows[GR.RowIndex].FindControl("Lbl_ID");
                EDT = Session["EntryDate"].ToString();

                TextBox TSBal = (TextBox)Grid_SingleModify.Rows[GR.RowIndex].FindControl("TxtBal") as TextBox;
                TextBox TSInst = (TextBox)Grid_SingleModify.Rows[GR.RowIndex].FindControl("TxtInst") as TextBox;
                TextBox TSIntr = (TextBox)Grid_SingleModify.Rows[GR.RowIndex].FindControl("TxtIntr") as TextBox;

                RO.BRCD =  ViewState["BRCD"].ToString();
                RO.MID = Session["MID"].ToString();
                RO.ASONDT = EDT;
               
                RO.ID = lb.Text;
                RO.BANKCODE = ViewState["BANKCODE"].ToString();
                RO.SBAL = TSBal.Text == "" ? "0" : TSBal.Text;
                RO.SINST = TSInst.Text == "" ? "0" : TSInst.Text;
                RO.SINTR = TSIntr.Text == "" ? "0" : TSIntr.Text;

                if (ViewState["FL"].ToString() == "SENDMOD")
                {
                    RO.FL = "SENDMOD";
                }
                else if (ViewState["FL"].ToString() == "POSTMOD")
                {
                    RO.FL = "POSTMOD";
                }


                Res = RO.FnBl_ModifyX(RO);

            }
            if (Res > 0)
            {

                WebMsgBox.Show("Updated Succesfully......!", this.Page);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();", true);
                //window.opener.location.href = window.opener.location.href;
            }
            else
            {
                WebMsgBox.Show("Error occured while updating (already authorized)......!", this.Page);
               
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void BtnModal_Exit_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(Page.GetType(), "script", "window.close();", true);
        
    }
    protected void Grid_SingleModify_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string Amt=string.IsNullOrEmpty(((TextBox)e.Row.FindControl("TxtBal")).Text)?"0":((TextBox)e.Row.FindControl("TxtBal")).Text;
                TotBal = Convert.ToDouble(Amt);
                SumFooterBal += TotBal;

                string Glcode = string.IsNullOrEmpty(((TextBox)e.Row.FindControl("TxtGlcode")).Text) ? "0" : ((TextBox)e.Row.FindControl("TxtGlcode")).Text;
                if (Glcode.ToString() != "3")
                {
                    TextBox TxtBoxInt = ((TextBox)e.Row.FindControl("TxtIntr"));
                    TxtBoxInt.Enabled = true;
                }

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string AmtInst = string.IsNullOrEmpty(((TextBox)e.Row.FindControl("TxtInst")).Text) ? "0" : ((TextBox)e.Row.FindControl("TxtInst")).Text;
                TotInst = Convert.ToDouble(AmtInst);
                SumFooterInst += TotInst;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string AmtIntr = string.IsNullOrEmpty(((TextBox)e.Row.FindControl("TxtIntr")).Text) ? "0" : ((TextBox)e.Row.FindControl("TxtIntr")).Text;
                TotIntr = Convert.ToDouble(AmtIntr);
                SumFooterIntr += TotIntr;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label Lblbal = ((Label)e.Row.FindControl("Lbl_S_Bal"));
                Lblbal.Text = SumFooterBal.ToString();

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
               
                Label LbInst = ((Label)e.Row.FindControl("Lbl_S_Inst"));
                LbInst.Text = SumFooterInst.ToString();

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
              
                Label LblIntr = ((Label)e.Row.FindControl("Lbl_S_Intr"));
                LblIntr.Text = SumFooterIntr.ToString();

            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}