using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmModelPopUp : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsLoanInstallmen LI = new ClsLoanInstallmen();
    ClsLoanSchedule LS = new ClsLoanSchedule();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsInsertTrans ITrans = new ClsInsertTrans();
    DataTable DT = new DataTable();

    ClsGetLoanStatDetails LTD = new ClsGetLoanStatDetails();

    string FL = "";
    string CustNo, AccNo, Prod;
    string DrawPower = "0";
    int resultint = 0, IntCalType = 0, IntApp = 0, IntId = 0;
    decimal TotalCr = 0, TotalDE = 0, TotalIn = 0, TotalPI = 0, TotalIR = 0, TotalNC = 0, TotalSC = 0, TotalCC = 0, TotalSC1 = 0, TotalOC = 0, TotalI = 0, TotalBC = 0, TotalBal = 0, ToptalCL = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["CustNo"] != null && Request.QueryString["AccNo"] != null && Request.QueryString["Prod"] != null)
            {
                CustNo = Request.QueryString["CustNo"].ToString();
                AccNo = Request.QueryString["AccNo"].ToString();
                Prod = Request.QueryString["Prod"].ToString();
                ShowDiv();
            }
        }
    }
    protected void ShowDiv()
    {
        try
        {
            DataSet dt = new DataSet();
            DataTable DT = new DataTable();
            DataTable DT1 = new DataTable();
            DataTable DTOD = new DataTable();
            string OpDate = LI.GetAccOpenDate(Session["BRCD"].ToString(), CustNo.Trim().ToString(), Prod.Trim().ToString(), AccNo.Trim().ToString());
            dt = GetLoanStatDetails(OpDate.ToString(), Session["EntryDate"].ToString(), Prod, AccNo, Session["BRCD"].ToString());
            if (dt.Tables[0].Rows.Count > 0)
            {

                GridRecords.DataSource = dt;
                GridRecords.DataBind();
                txtCustId.Text = dt.Tables[0].Rows[0]["CustNO"].ToString();
                txtBranchCode.Text = Session["BRCD"].ToString();
                txtAccId.Text = dt.Tables[0].Rows[0]["ACCNO"].ToString();
                //txtAddress.Text = dt.Tables[0].Rows[0]["BR_Add1"].ToString();
                txtAcName.Text = dt.Tables[0].Rows[0]["CustName"].ToString();
                GLcode.Text = Prod;
                txtCity.Text = dt.Tables[0].Rows[0]["City"].ToString() + "-" + dt.Tables[0].Rows[0]["PINCODE"].ToString();
                txtLD.Text = dt.Tables[0].Rows[0]["LASTINTDATE1"].ToString();
                txtED.Text = dt.Tables[0].Rows[0]["EDATE1"].ToString();
                txtCustAddress.Text = dt.Tables[0].Rows[0]["FLAT_ROOMNO"].ToString();
                txtSD.Text = dt.Tables[0].Rows[0]["SANSSIONDATE"].ToString();
                DT = LI.FetchNomineeDetails(dt.Tables[0].Rows[0]["CustNO"].ToString(), Prod.ToString(), Session["BRCD"].ToString());
                if (DT.Rows.Count > 0)
                {
                    GridGurantor.DataSource = DT;
                    GridGurantor.DataBind();
                    //txtNomiee.Text = DT.Rows[0]["Nominee"].ToString();
                }
                DT1 = LI.FetchLoanDetails(dt.Tables[0].Rows[0]["CustNO"].ToString(), dt.Tables[0].Rows[0]["ACCNO"].ToString(), Session["BRCD"].ToString(), Prod);
                if (DT1.Rows.Count > 0)
                {
                    txtIRate.Text = DT1.Rows[0]["INTRate"].ToString() + "%";
                    txtlimit.Text = DT1.Rows[0]["LIMIT"].ToString();
                }
                DTOD = LI.FetchOpeningDate(dt.Tables[0].Rows[0]["CustNO"].ToString(), dt.Tables[0].Rows[0]["ACCNO"].ToString(), Session["BRCD"].ToString(), Prod);
                if (DTOD.Rows.Count > 0)
                {

                    txtPh.Text = DTOD.Rows[0]["Mobile"].ToString();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
            }
            else
            {
                lblMessage.Text = "Details Not Found For This Account Number...!!";
                ModalPopup.Show(this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    public DataSet GetLoanStatDetails(string FDate, string TDate, string PT, string AC, string FBC)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = LTD.GetLnStatData(FDate, TDate, PT, AC, FBC);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    protected void GridRecords_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalCr += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Credit_PR"));
            TotalDE += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Debit_PR"));
            TotalIn += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InterestAmt"));
            TotalPI += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PenalInt"));
            TotalIR += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "IntReceivable"));
            TotalNC += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoticeCharge"));
            TotalSC += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ServiceCharge"));
            TotalCC += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CourtCharge"));
            TotalSC1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SerCharge"));
            TotalOC += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OtherCharge"));
            TotalI += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Insurance"));
            TotalBC += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BankCharge"));
            TotalBal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TBal"));
            ToptalCL += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BALANCE"));

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            ((Label)e.Row.FindControl("lblTCr")).Text = TotalCr.ToString();
            ((Label)e.Row.FindControl("lblTDe")).Text = TotalDE.ToString();
            ((Label)e.Row.FindControl("lblTIn")).Text = TotalIn.ToString();
            ((Label)e.Row.FindControl("lblTPI")).Text = TotalPI.ToString();
            ((Label)e.Row.FindControl("lblTIR")).Text = TotalIR.ToString();
            ((Label)e.Row.FindControl("lblTNC")).Text = TotalNC.ToString();
            ((Label)e.Row.FindControl("lblTSC")).Text = TotalSC.ToString();
            ((Label)e.Row.FindControl("lblTCC")).Text = TotalCC.ToString();
            ((Label)e.Row.FindControl("lblTSC1")).Text = TotalSC1.ToString();
            ((Label)e.Row.FindControl("lblTOC")).Text = TotalOC.ToString();
            ((Label)e.Row.FindControl("lblTI")).Text = TotalI.ToString();
            ((Label)e.Row.FindControl("lblTBC")).Text = TotalBC.ToString();
            ((Label)e.Row.FindControl("lblTTC")).Text = TotalBal.ToString();

        }
    }
    protected void GridGurantor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((Label)e.Row.FindControl("lblGurantor")).Text = "Gurantor" + (IntId + 1).ToString() + ":";
            IntId = IntId + 1;
        }
    }
}