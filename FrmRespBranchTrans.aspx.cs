using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class FrmRespBranchTrans : System.Web.UI.Page
{
    ClsInsertTrans ITrans = new ClsInsertTrans();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsRespBranchTrans MV = new ClsRespBranchTrans();
    DbConnection conn = new DbConnection();
    ClsCashReciept CR = new ClsCashReciept();
    ClsLoanInfo LI = new ClsLoanInfo();
    ClsAccopen AO = new ClsAccopen();
    ClsAuthorized AT = new ClsAuthorized();
    ClsOpenClose OC = new ClsOpenClose();
    ClsCommon CC = new ClsCommon();
    DataTable DT = new DataTable();
    DataTable DT1 = new DataTable();
    DataTable DtVoucherView;

    int resultout = 0;
    string RefId, ResBrCode, ST = "";
    double DebitAmt;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                MV.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                Getinfo();
                BindTransGrid();
                BindResGrid();
                BindBranch();
                ddlPMTMode.Focus();
            }

            autoglname.ContextKey = Session["BRCD"].ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Index Changed Event

    protected void ddlPMTMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPMTMode.SelectedValue == "3")
            {
                DivCash.Visible = true;
                DdlCRDR.Focus();
            }
            else if (ddlPMTMode.SelectedValue == "7")
            {
                DivCash.Visible = true;
                DdlCRDR.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void DdlCRDR_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DdlCRDR.SelectedValue == "2")
            {
                ddlActivity.Focus();
            }
            else if (DdlCRDR.SelectedValue == "1" && ddlPMTMode.SelectedValue == "3")
            {
                ViewState["GlCode"] = "99";
                TxtPtype.Text = "99";
                TxtAccNo.Text = "0";
                txtLoanBrCode.Text = Session["BRCD"].ToString();
                ViewState["LoanBrCode"] = Session["BRCD"].ToString();
                DivCash.Visible = false;
                ddlActivity.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPMTMode.SelectedValue == "3")
            {
                TxtNarration.Focus();
            }
            if (ddlPMTMode.SelectedValue == "7")
            {
                if (ddlActivity.SelectedValue == "1")
                {
                    DivBranch.Visible = true;
                    ddlLoanBrName.Focus();
                }
                else if (ddlActivity.SelectedValue == "2")
                {
                    txtLoanBrCode.Text = Session["BRCD"].ToString();
                    ViewState["LoanBrCode"] = Session["BRCD"].ToString();
                    DivBranch.Visible = false;
                    TxtPtype.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlLoanBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtLoanBrCode.Text = "";
            txtLoanBrCode.Text = ddlLoanBrName.SelectedValue.ToString();
            if (ddlActivity.SelectedValue == "1")
            {
                ViewState["LoanBrCode"] = txtLoanBrCode.Text.Trim().ToString();

                autoglname.ContextKey = ViewState["LoanBrCode"].ToString();
                ViewState["EntryDate"] = MV.openDay(ViewState["LoanBrCode"].ToString());
                txtWorkingDate.Text = ViewState["EntryDate"].ToString();
            }
            else if (ddlActivity.SelectedValue == "2")
            {
                ViewState["LoanBrCode"] = Session["BRCD"].ToString();
                autoglname.ContextKey = Session["BRCD"].ToString();
                ViewState["EntryDate"] = MV.openDay(txtLoanBrCode.Text.Trim().ToString());
                txtWorkingDate.Text = ViewState["EntryDate"].ToString();
            }
            BindTransGrid();
            TxtPtype.Focus();
            return;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdvoucher_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdvoucher.PageIndex = e.NewPageIndex;
        BindTransGrid();
    }

    protected void grdvoucher_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Font.Bold = true;
        }
    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string[] ID = objlink.CommandArgument.Split('_');
            ViewState["ID"] = objlink.CommandArgument.ToString();

            DT = new DataTable();
            DT = MV.GetSetAmount(ID[0].ToString(), ID[1].ToString(), ID[2].ToString(), Session["BRCD"].ToString());
            if (DT.Rows.Count > 0)
            {
                //EntryDate = ID[2].ToString();
                //VoucherNo = ID[1].ToString();
                ResBrCode = DT.Rows[0]["BRCD"].ToString();
                TxtCRBAL.Text = Convert.ToDouble(DT.Rows[0]["Credit"].ToString()).ToString();
                TxtDRBAL.Text = Convert.ToDouble(DT.Rows[0]["Debit"].ToString()).ToString();

                DT1 = new DataTable();
                DT1 = MV.GetADMSubGl(DT.Rows[0]["BRCD"].ToString());

                if (Convert.ToDouble(TxtDRBAL.Text) > 0)
                {
                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(), "0", "0", DT1.Rows[0]["ADMGlCode"].ToString(), DT1.Rows[0]["ADMSubGlCode"].ToString(), "0", "", TxtDRBAL.Text.Trim().ToString(), "1", "7", "TR", "", "", "0", "1900-01-01", Session["EntryDate"].ToString(), Session["MID"].ToString());
                }
                else if (Convert.ToDouble(TxtCRBAL.Text) > 0)
                {
                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(), "0", "0", DT1.Rows[0]["ADMGlCode"].ToString(), DT1.Rows[0]["ADMSubGlCode"].ToString(), "0", "", TxtCRBAL.Text.Trim().ToString(), "2", "7", "TR", "", "", "0", "1900-01-01", Session["EntryDate"].ToString(), Session["MID"].ToString());
                }

                if (resultout > 0)
                {
                    //ddlPMTMode.Enabled = false;
                    DivBranch.Visible = false;
                    DdlCRDR.SelectedValue = "0";
                    ddlActivity.SelectedValue = "0";
                    Getinfo();
                    BindTransGrid();
                    ClearText();
                    DdlCRDR.Focus();
                    lblMessage.Text = "Successfully Added...!!";
                    ModalPopup.Show(this.Page);
                }
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Text Change Event

    protected void TxtPtype_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;

            AC1 = MV.Getaccno(ViewState["LoanBrCode"].ToString(), TxtPtype.Text.Trim().ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                ViewState["GlCode"] = AC[0].ToString();
                TxtPname.Text = AC[1].ToString();
                AutoAccname.ContextKey = ViewState["LoanBrCode"].ToString() + "_" + TxtPtype.Text + "_" + ViewState["GlCode"].ToString();

                string YN = CC.GetIntACCYN(ViewState["LoanBrCode"].ToString(), TxtPtype.Text);
                if (Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()) >= 100 && YN != "Y") //--abhishek as per GL LEVEL Requirment
                {
                    TxtAccNo.Text = "";
                    TxtCustName.Text = "";
                    txtCustNo.Text = "";
                    TxtBalance.Text = "";
                    TxtTotalBal.Text = "";

                    TxtAccNo.Text = TxtPtype.Text.ToString();
                    TxtCustName.Text = TxtPname.Text.ToString();
                    txtCustNo.Text = "0";

                    string[] DTE = Session["EntryDate"].ToString().Split('/');
                    TxtBalance.Text = OC.GetOpenClose("CLOSING", DTE[2].ToString(), DTE[1].ToString(), TxtPtype.Text.Trim().ToString(), "0", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString()).ToString();
                    TxtTotalBal.Text = OC.GetOpenClose("MAIN_CLOSING", DTE[2].ToString(), DTE[1].ToString(), TxtPtype.Text.Trim().ToString(), "0", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString()).ToString();

                    //TxtBalance.Text = MV.GetOpenClose(ViewState["LoanBrCode"].ToString(), TxtPtype.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();
                    //TxtTotalBal.Text = MV.GetOpenClose(ViewState["LoanBrCode"].ToString(), TxtPtype.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "MainBal").ToString();

                    TxtNarration.Focus();
                }
                else
                {
                    TxtAccNo.Text = "";
                    TxtCustName.Text = "";
                    TxtBalance.Text = "";
                    TxtTotalBal.Text = "";
                    hdfGlCode.Value = Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()).ToString();

                    TxtAccNo.Focus();
                }
            }
            else
            {
                lblMessage.Text = "Enter Valid Product code...!!";
                ModalPopup.Show(this.Page);
                ClearText();
                TxtPtype.Focus();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
    }

    protected void TxtPname_TextChanged(object sender, EventArgs e)
    {
        string[] TD = TxtPname.Text.Split('_');
        if (TD.Length > 1)
        {
            TxtPname.Text = TD[0].ToString();
            TxtPtype.Text = TD[1].ToString();

            string[] AC = MV.Getaccno(ViewState["LoanBrCode"].ToString(), TxtPtype.Text.Trim().ToString()).Split('_');
            ViewState["GlCode"] = AC[0].ToString();
            AutoAccname.ContextKey = ViewState["LoanBrCode"].ToString() + "_" + TxtPtype.Text;

            string YN = CC.GetIntACCYN(ViewState["LoanBrCode"].ToString(), TxtPtype.Text);
            if (Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()) >= 100 && YN != "Y")
            {
                TxtAccNo.Text = "";
                TxtCustName.Text = "";
                txtCustNo.Text = "";
                TxtBalance.Text = "";
                TxtTotalBal.Text = "";

                TxtAccNo.Text = TxtPtype.Text.ToString();
                TxtCustName.Text = TxtPname.Text.ToString();
                txtCustNo.Text = "0";

                string[] DTE = Session["EntryDate"].ToString().Split('/');
                TxtBalance.Text = OC.GetOpenClose("CLOSING", DTE[2].ToString(), DTE[1].ToString(), TxtPtype.Text.Trim().ToString(), "0", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString()).ToString();
                TxtTotalBal.Text = OC.GetOpenClose("MAIN_CLOSING", DTE[2].ToString(), DTE[1].ToString(), TxtPtype.Text.Trim().ToString(), "0", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString()).ToString();

                //TxtBalance.Text = MV.GetOpenClose(ViewState["LoanBrCode"].ToString(), TxtPtype.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();
                //TxtTotalBal.Text = MV.GetOpenClose(ViewState["LoanBrCode"].ToString(), TxtPtype.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "MainBal").ToString();

                TxtNarration.Focus();
            }
            else
            {
                TxtAccNo.Text = "";
                TxtCustName.Text = "";
                TxtBalance.Text = "";
                TxtTotalBal.Text = "";
                hdfGlCode.Value = Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()).ToString();

                TxtAccNo.Focus();
            }
        }
        else
        {
            lblMessage.Text = "Enter Valid Product code...!!";
            ModalPopup.Show(this.Page);
            ClearText();
            TxtPtype.Focus();
        }
    }

    protected void TxtAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";

            AT = BD.Getstage1(TxtAccNo.Text, ViewState["LoanBrCode"].ToString(), TxtPtype.Text);
            if (AT != null)
            {
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise...!!";
                    ModalPopup.Show(this.Page);
                    TxtAccNo.Text = "";
                    TxtCustName.Text = "";
                    txtCustNo.Text = "";
                    TxtBalance.Text = "";
                    TxtTotalBal.Text = "";
                    TxtAccNo.Focus();
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = MV.GetCustName(TxtPtype.Text, TxtAccNo.Text, ViewState["LoanBrCode"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');

                        TxtCustName.Text = CustName[0].ToString();
                        txtCustNo.Text = CustName[1].ToString();

                        if (Convert.ToInt32(txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString()) < 0)
                        {
                            TxtAccNo.Text = "";
                            TxtCustName.Text = "";
                            txtCustNo.Text = "";
                            TxtBalance.Text = "";
                            TxtTotalBal.Text = "";
                            TxtAccNo.Focus();
                            WebMsgBox.Show("Please Enter valid Account Number Customer Not Exist...!!", this.Page);
                            return;
                        }

                        TxtBalance.Text = MV.GetOpenClose(ViewState["LoanBrCode"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                        TxtTotalBal.Text = MV.GetOpenClose(ViewState["LoanBrCode"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "MainBal").ToString();

                        DebitAmt = MV.DebitAmount(ViewState["LoanBrCode"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["Mid"].ToString());
                        if (DebitAmt.ToString() != "")
                        {
                            TxtBalance.Text = (Convert.ToDouble(TxtBalance.Text) - Convert.ToDouble(DebitAmt)).ToString();
                        }

                        TxtNarration.Focus();
                    }
                    if (TxtAccNo.Text != "" && TxtPtype.Text != "")
                    {
                        DataTable dtmodal = new DataTable();
                        string sql1 = "EXEC A_VOUCHINFO 'MODAL','" + TxtPtype.Text + "','" + ViewState["LoanBrCode"].ToString() + "','" + conn.ConvertDate(Session["ENTRYDATE"].ToString()) + "','" + TxtAccNo.Text + "'";
                        dtmodal = conn.GetDatatable(sql1);
                        if (dtmodal.Rows.Count > 0)
                        {
                            resultout = CR.GetInfo(GrdView, ViewState["LoanBrCode"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);
                            if (resultout > 0)
                            {
                                string Modal_Flag = "VOUCHERVIEW";
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#" + Modal_Flag + "').modal('show');");
                                sb.Append(@"</script>");

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                            }
                        }
                    }
                }
            }
            else
            {
                lblMessage.Text = "Enter Valid Account Number...!!";
                ModalPopup.Show(this.Page);
                TxtAccNo.Text = "";
                TxtCustName.Text = "";
                txtCustNo.Text = "";
                TxtBalance.Text = "";
                TxtTotalBal.Text = "";
                TxtAccNo.Focus();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtCustName_TextChanged(object sender, EventArgs e)
    {
        string[] TD = TxtCustName.Text.Split('_');
        if (TD.Length > 1)
        {
            TxtCustName.Text = TD[0].ToString();
            TxtAccNo.Text = TD[1].ToString();
            txtCustNo.Text = TD[2].ToString();

            if (Convert.ToInt32(txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString()) < 0)
            {
                TxtAccNo.Text = "";
                TxtCustName.Text = "";
                txtCustNo.Text = "";
                TxtBalance.Text = "";
                TxtTotalBal.Text = "";
                TxtAccNo.Focus();
                WebMsgBox.Show("Please Enter valid Account Number Customer Not Exist...!!", this.Page);
                return;
            }

            TxtBalance.Text = MV.GetOpenClose(ViewState["LoanBrCode"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
            TxtTotalBal.Text = MV.GetOpenClose(ViewState["LoanBrCode"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "MainBal").ToString();

            if (TxtAccNo.Text != "" && TxtPtype.Text != "")
            {
                DataTable dtmodal = new DataTable();
                string sql1 = "EXEC A_VOUCHINFO 'MODAL','" + TxtPtype.Text + "','" + ViewState["LoanBrCode"].ToString() + "','" + conn.ConvertDate(Session["ENTRYDATE"].ToString()) + "','" + TxtAccNo.Text + "'";
                dtmodal = conn.GetDatatable(sql1);
                if (dtmodal.Rows.Count > 0)
                {
                    resultout = CR.GetInfo(GrdView, ViewState["LoanBrCode"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);
                    if (resultout > 0)
                    {
                        string Modal_Flag = "VOUCHERVIEW";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$('#" + Modal_Flag + "').modal('show');");
                        sb.Append(@"</script>");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                    }
                }
            }
            TxtNarration.Focus();
        }
        else
        {
            lblMessage.Text = "Enter Valid Account Number...!!";
            ModalPopup.Show(this.Page);
            TxtAccNo.Text = "";
            TxtCustName.Text = "";
            txtCustNo.Text = "";
            TxtBalance.Text = "";
            TxtTotalBal.Text = "";
            TxtAccNo.Focus();
        }
    }

    protected void TxtAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (DdlCRDR.SelectedValue == "2")
            {
                if (ddlPMTMode.SelectedValue != "3")
                {
                    double BAL = Convert.ToDouble(TxtBalance.Text.Trim().ToString() == "" ? "0" : TxtBalance.Text.Trim().ToString());
                    double curbal = Convert.ToDouble(TxtAmount.Text.Trim().ToString() == "" ? "0" : TxtAmount.Text.Trim().ToString());

                    if ((Convert.ToInt32(ViewState["GlCode"].ToString()) < 100) && Convert.ToInt32(ViewState["GlCode"].ToString()) != 3)
                    {
                        if (curbal > BAL)
                        {
                            lblMessage.Text = "Insufficient Account Balance...!!";
                            TxtAmount.Text = "";
                            TxtAmount.Focus();
                            ModalPopup.Show(this.Page);
                            return;
                        }
                    }
                }
            }

            Submit.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Click Event Here

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            string AC, AN;
            AN = AC = "";

            if (DdlCRDR.SelectedValue == "1")
            {
                if (txtLoanBrCode.Text.Trim().ToString() == "")
                {
                    lblMessage.Text = "Select branch first...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }

            if (DdlCRDR.SelectedValue == "0")
            {
                lblMessage.Text = "Select transaction type...!!";
                ModalPopup.Show(this.Page);
                return;
            }

            if (ddlPMTMode.SelectedValue == "0")
            {
                lblMessage.Text = "Select payment mode first...!!!";
                ModalPopup.Show(this.Page);
                return;
            }

            if (ddlPMTMode.SelectedValue == "7")
            {
                if (TxtPtype.Text.Trim().ToString() == "")
                {
                    lblMessage.Text = "Enter product type first...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }

                if (TxtAccNo.Text.Trim().ToString() == "")
                {
                    lblMessage.Text = "Enter account number first...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }

            if (TxtNarration.Text.Trim().ToString() == "")
            {
                lblMessage.Text = "Enter narration first...!!";
                ModalPopup.Show(this.Page);
                return;
            }

            if (Convert.ToDouble(TxtAmount.Text.Trim().ToString() == "" ? "0" : TxtAmount.Text.Trim().ToString()) <= 0.00)
            {
                lblMessage.Text = "Enter proper amount First...!!";
                ModalPopup.Show(this.Page);
                return;
            }

            //If Account Number is blank Then Set Acc No and Cust No to Zero
            string YN = CC.GetIntACCYN(ViewState["LoanBrCode"].ToString(), TxtPtype.Text);
            if (Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()) >= 100 && YN != "Y")
            {
                AC = "0";
                AN = TxtPname.Text.ToString();
            }
            else
            {
                AC = TxtAccNo.Text.Trim().ToString();
                AN = TxtCustName.Text.ToString();
            }

            //Check Amount is grater than zero or not
            if (Convert.ToDouble(TxtAmount.Text.Trim().ToString() == "" ? "0" : TxtAmount.Text.Trim().ToString()) > 0.00)
            {
                if (txtLoanBrCode.Text.Trim().ToString() == "")
                    ResBrCode = Session["BRCD"].ToString();
                else
                    ResBrCode = txtLoanBrCode.Text.Trim().ToString();

                //Insert Data into Temporary Table (Avs_TempMultiTransfer) in Database here
                if (ddlPMTMode.SelectedValue == "0")
                {
                    ddlPMTMode.Focus();

                    lblMessage.Text = "Select payment mode first...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                else if (ddlPMTMode.SelectedValue == "3")
                {
                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(), ResBrCode.ToString(), txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString(), "99", "99", "0", "", TxtAmount.Text.Trim().ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "3", "CR", "99/0", TxtNarration.Text.ToString(), "0", "1900-01-01", Session["EntryDate"].ToString(), Session["MID"].ToString());
                }
                else if (ddlPMTMode.SelectedValue == "7")
                {
                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(), ResBrCode.ToString(), txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text.Trim().ToString(), AC.Trim().ToString(), AN.ToString(), TxtAmount.Text.Trim().ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", TxtPtype.Text.Trim().ToString() + "/" + AC.Trim().ToString() + " - " + AN.ToString() + "", TxtNarration.Text.ToString(), "0", "1900-01-01", Session["EntryDate"].ToString(), Session["MID"].ToString());
                }

                if (resultout > 0)
                {
                    ddlPMTMode.Enabled = false;
                    lblMessage.Text = "Successfully Added...!!";
                    ModalPopup.Show(this.Page);
                }
            }
            else
            {
                lblMessage.Text = "Enter Amount First...!!";
                ModalPopup.Show(this.Page);
                return;
            }

            if (resultout > 0)
            {
                DivBranch.Visible = false;
                DdlCRDR.SelectedValue = "0";
                ddlActivity.SelectedValue = "0";
                Getinfo();
                BindTransGrid();
                ClearText();
                DdlCRDR.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            string PayMast, SetNo = "";
            string[] ID;

            PayMast = "MULTITRANSFER";
            RefId = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
            ViewState["RID"] = (Convert.ToInt32(RefId) + 1).ToString();

            if (Convert.ToDouble(TxtDiff.Text.Trim().ToString() == "" ? "0" : TxtDiff.Text.Trim().ToString()) == 0.00)
            {
                if (ddlPMTMode.SelectedValue == "3")
                {
                    //Get All Transaction From Temporary Table
                    DataTable DT = new DataTable();
                    DT = MV.GetTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (DT.Rows.Count > 0)
                    {
                        //Generate Set Number Here
                        SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            //Insert Data to Original Table Here
                            resultout = ITrans.InsertMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i]["GLCODE"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString(), DT.Rows[i]["ACCNO"].ToString(),
                                        DT.Rows[i]["PARTICULARS"].ToString(), DT.Rows[i]["PARTICULARS2"].ToString(), Convert.ToDouble(DT.Rows[i]["AMOUNT"].ToString()), DT.Rows[i]["TrxType"].ToString(), "4", "CP", SetNo, DT.Rows[i]["InstNo"].ToString(), DT.Rows[i]["InstDate"].ToString(), "0", "0", "1001",
                                        "", DT.Rows[i]["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PayMast.ToString(), DT.Rows[i]["CUSTNO"].ToString(), DT.Rows[i]["CustName"].ToString(), DT.Rows[i]["RefBrcd"].ToString(), DT.Rows[i]["OrgBrCd"].ToString(), DT.Rows[i]["ResBrCd"].ToString(), ViewState["RID"].ToString());
                        }
                    }
                }
                else if (ddlPMTMode.SelectedValue == "7")
                {
                    //Get All Transaction From Temporary Table
                    DataTable DT = new DataTable();
                    DT = MV.GetTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (DT.Rows.Count > 0)
                    {
                        //Generate Set Number Here
                        SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            //Insert Data to Original Table Here
                            resultout = ITrans.InsertMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i]["GLCODE"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString(), DT.Rows[i]["ACCNO"].ToString(),
                                        DT.Rows[i]["PARTICULARS"].ToString(), DT.Rows[i]["PARTICULARS2"].ToString(), Convert.ToDouble(DT.Rows[i]["AMOUNT"].ToString()), DT.Rows[i]["TrxType"].ToString(), "7", "TR", SetNo, DT.Rows[i]["InstNo"].ToString(), DT.Rows[i]["InstDate"].ToString(), "0", "0", "1001",
                                        "", DT.Rows[i]["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PayMast.ToString(), DT.Rows[i]["CUSTNO"].ToString(), DT.Rows[i]["CustName"].ToString(), DT.Rows[i]["RefBrcd"].ToString(), DT.Rows[i]["OrgBrCd"].ToString(), DT.Rows[i]["ResBrCd"].ToString(), ViewState["RID"].ToString());
                        }
                    }
                }

                ClearAll();
                //Delete All Data From Temporary Table Here
                MV.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                grdvoucher.Visible = false;

                if (resultout > 0)
                {
                    ID = ViewState["ID"].ToString().Split('_');
                    MV.UpdateStatus(ID[0].ToString(), ID[1].ToString(), ID[2].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                    BindResGrid();
                    lblMessage.Text = "Transfer Seccessfully With Set No : '" + SetNo + "' ...!!";
                    ModalPopup.Show(this.Page);
                    btnPost.Enabled = false;
                    btnView.Enabled = false;
                    return;
                }
            }
            else
            {
                lblMessage.Text = "Amount Difference in Credit and Debit Transaction...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    private DataTable VoucherView(DataTable dt)
    {
        dt.Columns.Add("BrCd");
        dt.Columns.Add("EDate");
        dt.Columns.Add("SubGl");
        dt.Columns.Add("AccNo");
        dt.Columns.Add("Parti1");
        dt.Columns.Add("Parti2");
        dt.Columns.Add("Amount");
        dt.Columns.Add("TrxType");
        dt.AcceptChanges();
        return dt;
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        string BrCode = "";

        try
        {
            if (Convert.ToDouble(TxtDiff.Text) == 0.00)
            {
                DtVoucherView = new DataTable();
                DtVoucherView = VoucherView(DtVoucherView);

                if (ddlPMTMode.SelectedValue == "3")
                {
                    //Get all Transaction From Temporary Table
                    DataTable DT = new DataTable();
                    DT = MV.GetTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (DT.Rows.Count > 0)
                    {
                        //Insert Credit and Debit Entry for Orginating Branch to DataTable
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            DtVoucherView.Rows.Add(DT.Rows[i]["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString(), DT.Rows[i]["ACCNO"].ToString(), DT.Rows[i]["PARTICULARS"].ToString(), DT.Rows[i]["PARTICULARS2"].ToString(), Convert.ToDouble(DT.Rows[i]["AMOUNT"].ToString()), DT.Rows[i]["TrxType"].ToString() == "1" ? "Cr" : "Dr");
                            BrCode = DT.Rows[i]["BRCD"].ToString();
                        }
                    }
                }
                else if (ddlPMTMode.SelectedValue == "7")
                {
                    //Get all Transaction From Temporary Table
                    DataTable DT = new DataTable();
                    DT = MV.GetTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (DT.Rows.Count > 0)
                    {
                        //Insert Credit and Debit Entry for Orginating Branch to DataTable
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            DtVoucherView.Rows.Add(DT.Rows[i]["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString(), DT.Rows[i]["ACCNO"].ToString(), DT.Rows[i]["PARTICULARS"].ToString(), DT.Rows[i]["PARTICULARS2"].ToString(), Convert.ToDouble(DT.Rows[i]["AMOUNT"].ToString()), DT.Rows[i]["TrxType"].ToString() == "1" ? "Cr" : "Dr");
                            BrCode = DT.Rows[i]["BRCD"].ToString();
                        }
                    }
                }

                if (DtVoucherView.Rows.Count > 0)
                {
                    grdVoucherView.DataSource = DtVoucherView;
                    grdVoucherView.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
                    return;
                }
            }
            else
            {
                lblMessage.Text = "Amount Difference in Credit and Debit Transaction...!!";
                ModalPopup.Show(this.Page);
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
        ClearText();
    }

    protected void Exit_Click(object sender, EventArgs e)
    {
        //Delete All Data From Temporary Table Here
        MV.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        return;
    }

    protected void lnkbtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string id = objlink.CommandArgument;

            resultout = MV.DeleteSingleRecTable(id, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (resultout > 0)
            {
                lblMessage.Text = "Record Deleted Successfully...!!";
                ModalPopup.Show(this.Page);
            }

            BindTransGrid();
            Getinfo();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Public Function

    public void BindBranch()
    {
        try
        {
            BD.BindBRANCHNAME(ddlLoanBrName, null);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void Getinfo()
    {
        try
        {
            DataTable DT = new DataTable();
            DT = MV.GetCRDR(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                TxtCRBAL.Text = DT.Rows[0]["CREDIT"].ToString();
                TxtDRBAL.Text = DT.Rows[0]["DEBIT"].ToString();
                double CR, DR;
                CR = DR = 0;
                CR = Convert.ToDouble(TxtCRBAL.Text);
                DR = Convert.ToDouble(TxtDRBAL.Text);

                TxtDiff.Text = (CR - DR).ToString();

                if (CR == DR)
                {
                    btnView.Enabled = true;
                    btnPost.Enabled = true;
                    btnPost.Focus();
                }
                else
                {
                    btnView.Enabled = false;
                    btnPost.Enabled = false;
                    TxtPtype.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindTransGrid()
    {
        try
        {
            int RC = MV.Getinfotable(grdvoucher, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindResGrid()
    {
        try
        {
            int RC = MV.GetResBrTrans(grdResponding, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void NewWindowsVerify(string url)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=600,height=250,left=50,top=50,resizable=no');", true);
    }

    public void ClearText()
    {
        ddlLoanBrName.SelectedValue = "0";
        txtLoanBrCode.Text = "";
        txtWorkingDate.Text = "";

        TxtPtype.Text = "";
        TxtPname.Text = "";
        TxtAccNo.Text = "";
        TxtCustName.Text = "";
        txtCustNo.Text = "";

        TxtBalance.Text = "";
        TxtTotalBal.Text = "";

        TxtNarration.Text = "";
        TxtAmount.Text = "";
    }

    public void ClearAll()
    {
        DdlCRDR.SelectedIndex = 0;
        TxtCRBAL.Text = "";
        TxtDRBAL.Text = "";
        TxtDiff.Text = "";

        TxtPtype.Text = "";
        TxtPname.Text = "";
        TxtAccNo.Text = "";
        TxtCustName.Text = "";

        TxtBalance.Text = "";
        TxtTotalBal.Text = "";

        TxtNarration.Text = "";
        TxtAmount.Text = "";
    }

    #endregion

}