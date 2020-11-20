using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmInOut : System.Web.UI.Page
{

    ClsBindDropdown BD = new ClsBindDropdown();
    scustom CS = new scustom();
    ClsInwordClear OWGCL = new ClsInwordClear();
    ClsOutClear OWGCLG = new ClsOutClear();
    ClsInOut IO = new ClsInOut();
    ClsCommon CM = new ClsCommon();
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

            BD.BindIO(DdlType, Session["BRCD"].ToString());
            CS.BindBankName(ddlBankName);
            IO.GetSumGrid(GrdDetails, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "ZONEGRID");
            GetDetails();
        }
    }

    public void Getamount(string FL)
    {
        double AMT = 0;
        AMT = OWGCL.GetAmount(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), FL);
        TxtActualAmt.Text = Convert.ToString(AMT);
    }
    public void GetDiffrence(string FL)
    {
        double AMT = 0;
        AMT = OWGCL.GetDifference(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), FL);
        TxtDiffAmt.Text = Convert.ToString(AMT);
    }

    public void GetDetails()
    {
        try
        {
             DataTable DT;
            DT = CM.IWOWSum("DETAILS", "CDETAILS", Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            if (DT.Rows.Count > 0)
            {
                TxtIwcRecSum.Text = string.IsNullOrEmpty(DT.Rows[0]["SumIWC"].ToString())?"0":DT.Rows[0]["SumIWC"].ToString();
                TxtIwcReturnSum.Text = string.IsNullOrEmpty(DT.Rows[0]["SumIWCR"].ToString()) ? "0" : DT.Rows[0]["SumIWCR"].ToString();
                TxtIWCUnclearSum.Text = string.IsNullOrEmpty(DT.Rows[0]["SumIWCU"].ToString()) ? "0" : DT.Rows[0]["SumIWCU"].ToString();



                TxtOwcSendSum.Text = string.IsNullOrEmpty(DT.Rows[0]["SumOWC"].ToString()) ? "0" : DT.Rows[0]["SumOWC"].ToString();
                TxtOwcReturnSum.Text = string.IsNullOrEmpty(DT.Rows[0]["SumOWCR"].ToString()) ? "0" : DT.Rows[0]["SumOWCR"].ToString();
                TxtOWCUnclearSum.Text = string.IsNullOrEmpty(DT.Rows[0]["SumOWCU"].ToString()) ? "0" : DT.Rows[0]["SumOWCU"].ToString();

                TxtClgHouseHOCr.Text = string.IsNullOrEmpty(DT.Rows[0]["ClgHouseCr"].ToString()) ? "0" : DT.Rows[0]["ClgHouseCr"].ToString();
                TxtClgHouseHODr.Text = string.IsNullOrEmpty(DT.Rows[0]["ClgHouseDr"].ToString()) ? "0" : DT.Rows[0]["ClgHouseDr"].ToString();

                TxtTotalIWCClg.Text = string.IsNullOrEmpty(DT.Rows[0]["TotalIWC"].ToString()) ? "0" : DT.Rows[0]["TotalIWC"].ToString();
                TxtTotalOWCClg.Text = string.IsNullOrEmpty(DT.Rows[0]["TotalOWC"].ToString()) ? "0" : DT.Rows[0]["TotalOWC"].ToString();


                TxtClgDiff.Text = (Convert.ToDouble(TxtOwcSendSum.Text) - Convert.ToDouble(TxtIwcRecSum.Text)).ToString();
                TxtReturnClgDiff.Text = (Convert.ToDouble(TxtOwcReturnSum.Text) - Convert.ToDouble(TxtIwcReturnSum.Text)).ToString();

                TxtDrClearing.Text = string.IsNullOrEmpty(DT.Rows[0]["DebitTotal"].ToString()) ? "0" : DT.Rows[0]["DebitTotal"].ToString();
                TxtCrClearing.Text = string.IsNullOrEmpty(DT.Rows[0]["CreditTotal"].ToString()) ? "0" : DT.Rows[0]["CreditTotal"].ToString();

                if (Convert.ToDouble(TxtDrClearing.Text) == Convert.ToDouble(TxtCrClearing.Text))
                {
                    TxtMsg.Text = "Clearing Tally successfully";
                }
                else
                {
                    
                    TxtMsg.Text = "Clearing not Tally Diff = " + (Convert.ToDouble(TxtDrClearing.Text) - Convert.ToDouble(TxtCrClearing.Text)) + "";

                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DdlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DdlType.SelectedValue == "1")
        {
            rdbCredit.Checked = true;
            rdbDebit.Checked = false;
            Getamount("IN");
            GetDiffrence("IN");
        }
        else if(DdlType.SelectedValue=="2")
        {
            rdbDebit.Checked = true;
            rdbCredit.Checked = false;
            Getamount("OUT");
            GetDiffrence("OUT");
        }
        else if (DdlType.SelectedValue == "3")
        {
            rdbDebit.Checked = true;
            rdbCredit.Checked = false;
            Getamount("IWRT");
            GetDiffrence("IWRT");
        }
        else if (DdlType.SelectedValue == "4")
        {
            rdbDebit.Checked = false;
            rdbCredit.Checked = true;
            Getamount("OWRT");
            GetDiffrence("OWRT");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtDiffAmt.Text != "0")
            {
                string ET = "";
                string setno = BD.GetSetNo(Session["EntryDate"].ToString(), "InOutSetno", Session["BRCD"].ToString()); //OWGCL.GetNewSetNo(BRCD);
                int ScrollNo, RC;
                string GLCODE = BD.GetAccTypeGL(ddlBankName.SelectedValue, Session["BRCD"].ToString());
                string[] GLC = GLCODE.Split('_');
                ScrollNo = 1;
                ET = rdbCredit.Checked == true ? "C" : "D";


                if (rdbCredit.Checked == true && DdlType.SelectedValue == "1")
                {
                    RC = OWGCL.InsertNewSetNo(GLC[1].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), ddlBankName.SelectedValue, "0", "0", "", "Inward Clearing", ddlBankName.SelectedValue, "0", "0", "01/01/1900", "0", TxtContraAmt.Text, Session["MID"].ToString(), "", Convert.ToInt32(setno), ScrollNo, Convert.ToInt32(ddlBankName.SelectedValue), "C", "1001",Dll_Session.SelectedValue);
                    if (RC > 0)
                    {
                        lblMessage.Text = "Record Added sucessfully with Recipt No " + setno;
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Zoneposting_Add _" + setno + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                        Getamount("IN");
                        GetDiffrence("IN");
                        ModalPopup.Show(this.Page);
                        Clear();
                    }
                }
                else if (rdbDebit.Checked == true && DdlType.SelectedValue == "2")
                {
                    RC = OWGCLG.InsertNewSetNo(GLC[1].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), ddlBankName.SelectedValue, "0", "0", "", "Outward Clearing", ddlBankName.SelectedValue, "0", "0", "01/01/1900", "0", TxtContraAmt.Text, Session["MID"].ToString(), "", Convert.ToInt32(setno), ScrollNo, ddlBankName.SelectedValue, "D", "O", "1001", Dll_Session.SelectedValue);
                    if (RC > 0)
                    {
                        lblMessage.Text = "Record Added sucessfully with Recipt No " + setno;
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Zoneposting_Add _" + setno + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        Getamount("OUT");
                        GetDiffrence("OUT");
                        ModalPopup.Show(this.Page);
                        Clear();
                    }
                }

                else if (rdbCredit.Checked == true && DdlType.SelectedValue == "4")
                {
                    RC = OWGCLG.InsertNewSetNoReturn(GLC[1].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), ddlBankName.SelectedValue, "0", "0", "", "OW Clg Return", ddlBankName.SelectedValue, "0", "0", "01/01/1900", "0", TxtContraAmt.Text, Session["MID"].ToString(), "", Convert.ToInt32(setno), ScrollNo, ddlBankName.SelectedValue, "C", "O", "1001", Dll_Session.SelectedValue);
                    if (RC > 0)
                    {
                        lblMessage.Text = "Record Added sucessfully with Recipt No " + setno;
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Zoneposting_Add _" + setno + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        Getamount("OWRT");
                        GetDiffrence("OWRT");
                        ModalPopup.Show(this.Page);
                        Clear();
                    }
                }

                else if (rdbDebit.Checked == true && DdlType.SelectedValue == "3")
                {
                    RC = OWGCLG.InsertNewSetNoReturn(GLC[1].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), ddlBankName.SelectedValue, "0", "0", "", "IW Clg Return", ddlBankName.SelectedValue, "0", "0", "01/01/1900", "0", TxtContraAmt.Text, Session["MID"].ToString(), "", Convert.ToInt32(setno), ScrollNo, ddlBankName.SelectedValue, "D", "I", "1001", Dll_Session.SelectedValue);
                    if (RC > 0)
                    {
                        lblMessage.Text = "Record Added sucessfully with Recipt No " + setno;
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Zoneposting_Add _" + setno + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        Getamount("IWRT");
                        GetDiffrence("IWRT");
                        ModalPopup.Show(this.Page);
                        Clear();
                    }
                }
            }
            else
            {
                WebMsgBox.Show("Voucher Already Posted .....!", this.Page);
                Clear();
                DdlType.Focus();

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    public void Clear()
    {
        TxtDiffAmt.Text = "";
        TxtActualAmt.Text = "";
        TxtContraAmt.Text = "";
        ddlBankName.SelectedValue = "0";
        DdlType.SelectedValue = "0";
        DdlType.Focus();
    }
    protected void btmClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
}