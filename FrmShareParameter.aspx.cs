using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmShareParameter : System.Web.UI.Page
{
    ClsShareParameter SP = new ClsShareParameter();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    int resultint = 0;
    string AC1, CUNAME;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            AutoGlName1.ContextKey = Session["BRCD"].ToString();
            AutoGlName2.ContextKey = Session["BRCD"].ToString();
            AutoGlName3.ContextKey = Session["BRCD"].ToString();
            AutoGlName4.ContextKey = Session["BRCD"].ToString();
            AutoGlName5.ContextKey = Session["BRCD"].ToString();
            AutoGlName6.ContextKey = Session["BRCD"].ToString();
            AutoGlName7.ContextKey = Session["BRCD"].ToString();
            AutoGlName8.ContextKey = Session["BRCD"].ToString();
            AutoGlName9.ContextKey = Session["BRCD"].ToString();
            AutoGlName10.ContextKey = Session["BRCD"].ToString();
            AutoGlName11.ContextKey = Session["BRCD"].ToString();
            AutoGlName12.ContextKey = Session["BRCD"].ToString();
            AutoGlName13.ContextKey = Session["BRCD"].ToString();
            AutoGlName14.ContextKey = Session["BRCD"].ToString();
        }
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "AD";
            Submit.Text = "Submit";
            ClearData();
            ENDN(true);
            lblActivity.Text = "Add Parameter";
            grdMaster.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "MD";
            Submit.Text = "Modify";
            ClearData();
            ENDN(true);
            lblActivity.Text = "Modify Parameter";
            BindGrid();
            grdMaster.Visible = true;
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
            ViewState["Flag"] = "DL";
            ENDN(false);
            ClearData();
            Submit.Text = "Delete";
            lblActivity.Text = "Delete Parameter";
            BindGrid();
            grdMaster.Visible = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkAuthorized_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "AT";
            ClearData();
            Submit.Text = "Authorize";
            ENDN(false);
            lblActivity.Text = "Authorize Parameter";
            BindGrid();
            grdMaster.Visible = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearData()
    {
        try
        {
            txtShrValue.Text = "";
            txtShrFrom.Text = "";
            txtNoOfShr.Text = "";

            txtEnterenceFee.Text = "";
            txtWelfareFee.Text = "";
            txtWelLoanFee.Text = "";

            txtShrPrCode.Text = "";
            txtShrProdName.Text = "";

            txtSavProd.Text = "";
            txtSavProdName.Text = "";

            txtEntryFee.Text = "";
            txtEntryProdName.Text = "";

            txtOther1.Text = "";
            txtWelProdName.Text = "";

            txtOther2.Text = "";
            txtPrinProdName.Text = "";

            txtOther3.Text = "";
            txtOtherPrName3.Text = "";

            txtOther4.Text = "";
            txtOtherPrName4.Text = "";

            txtOther5.Text = "";
            txtOtherPrName5.Text = "";

            txtMemWelfare.Text = "";
            txtWelLoanProdName.Text = "";

            txtServiceChg.Text = "";
            txtServiceProdName.Text = "";

            txtShrSuspence.Text = "";
            txtShrSusName.Text = "";

            Txtdiv1.Text = "";
            Txtdiv1name.Text = "";

            Txtdiv2.Text = "";
            Txtdiv2name.Text = "";

            Txtdiv3.Text = "";
            Txtdiv3name.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ENDN(bool TF)
    {
        try
        {
            txtShrValue.Enabled = TF;
            txtShrFrom.Enabled = TF;
            txtNoOfShr.Enabled = TF;

            txtEnterenceFee.Enabled = TF;
            txtWelfareFee.Enabled = TF;
            txtWelLoanFee.Enabled = TF;

            txtShrPrCode.Enabled = TF;
            txtShrProdName.Enabled = TF;

            txtSavProd.Enabled = TF;
            txtSavProdName.Enabled = TF;

            txtEntryFee.Enabled = TF;
            txtEntryProdName.Enabled = TF;

            txtOther1.Enabled = TF;
            txtWelProdName.Enabled = TF;

            txtOther2.Enabled = TF;
            txtPrinProdName.Enabled = TF;

            txtMemWelfare.Enabled = TF;
            txtWelLoanProdName.Enabled = TF;

            txtServiceChg.Enabled = TF;
            txtServiceProdName.Enabled = TF;

            txtShrSuspence.Enabled = TF;
            txtShrSusName.Enabled = TF;

            Txtdiv1.Enabled = TF;
            Txtdiv1name.Enabled = TF;

            Txtdiv2.Enabled = TF;
            Txtdiv2name.Enabled = TF;

            Txtdiv3.Enabled = TF;
            Txtdiv3name.Enabled = TF;
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
            SP.BindData(grdMaster, Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() == "AD")
            {
                if (txtShrValue.Text.ToString() == "")
                {
                    lblMessage.Text = "Enter Default Share value First...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                else if (txtNoOfShr.Text.ToString() == "")
                {
                    lblMessage.Text = "Enter Default No of Share First...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                else if (txtEnterenceFee.Text.ToString() == "")
                {
                    lblMessage.Text = "Enter Default Enterence Fee First...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                //else if (txtWelfareFee.Text.ToString() == "")
                //{
                //    lblMessage.Text = "Enter Default Welfare Fee First...!!";
                //    ModalPopup.Show(this.Page);
                //    return;
                //}
                //else if (txtWelLoanFee.Text.ToString() == "")
                //{
                //    lblMessage.Text = "Enter Default Welfare Loan Fee First...!!";
                //    ModalPopup.Show(this.Page);
                //    return;
                //}
                else
                {//sangeeta 7 july 2018
                    resultint = SP.InsertData(Session["BRCD"].ToString(), txtShrValue.Text.Trim().ToString() == "" ? "0" : txtShrValue.Text, txtShrFrom.Text.Trim().ToString() == "" ? "0" : txtShrFrom.Text.Trim().ToString(), txtNoOfShr.Text.Trim().ToString(), txtEnterenceFee.Text.Trim().ToString() == " " ? "0" : txtEnterenceFee.Text, txtWelfareFee.Text.Trim().ToString() == "" ? "0" : txtWelfareFee.Text, txtWelLoanFee.Text.Trim().ToString() == "" ? "0" : txtWelLoanFee.Text, txtShrPrCode.Text.Trim().ToString(), txtEntryFee.Text.Trim().ToString(), txtEntryProdName.Text, txtSavProd.Text.Trim().ToString(), txtOther1.Text.Trim().ToString(), txtWelProdName.Text, txtOther2.Text.Trim().ToString(), txtPrinProdName.Text, txtOther3.Text.Trim().ToString(), txtOtherPrName3.Text, txtOther4.Text.Trim().ToString(), txtOtherPrName4.Text, txtOther5.Text.Trim().ToString(), txtOtherPrName5.Text, txtMemWelfare.Text.Trim().ToString(), txtWelLoanProdName.Text, txtServiceChg.Text.Trim().ToString(), txtServiceProdName.Text, Session["EntryDate"].ToString(), Session["MID"].ToString(), txtShrSuspence.Text, Txtdiv1.Text, Txtdiv2.Text, Txtdiv3.Text);

                    if (resultint > 0)
                    {
                        lblMessage.Text = "Insert Details Successfully...!!";
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_Para_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        return;
                    }
                }
            }
            else if (ViewState["Flag"].ToString() == "MD")
            {
                if (txtShrValue.Text.ToString() == "")
                {
                    lblMessage.Text = "Enter Default Share value First...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                else if (txtNoOfShr.Text.ToString() == "")
                {
                    lblMessage.Text = "Enter Default No of Share First...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                else if (txtEnterenceFee.Text.ToString() == "")
                {
                    lblMessage.Text = "Enter Default Enterence Fee First...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                //else if (txtWelfareFee.Text.ToString() == "")
                //{
                //    lblMessage.Text = "Enter Default Welfare Fee First...!!";
                //    ModalPopup.Show(this.Page);
                //    return;
                //}
                //else if (txtWelLoanFee.Text.ToString() == "")
                //{
                //    lblMessage.Text = "Enter Default Welfare Loan Fee First...!!";
                //    ModalPopup.Show(this.Page);
                //    return;
                //}
                else
                {
                    //resultint = SP.ModifyData(ViewState["Id"].ToString(), Session["BRCD"].ToString(), txtShrValue.Text.Trim().ToString(), txtShrFrom.Text.Trim().ToString() == "" ? "0" : txtShrFrom.Text.Trim().ToString(), txtNoOfShr.Text.Trim().ToString(), txtEnterenceFee.Text.Trim().ToString(), txtWelfareFee.Text.Trim().ToString(), txtWelLoanFee.Text.Trim().ToString(), txtShrPrCode.Text.Trim().ToString(), txtEntryFee.Text.Trim().ToString(), txtSavProd.Text.Trim().ToString(), txtOther1.Text.Trim().ToString(), txtOther2.Text.Trim().ToString(), txtOther3.Text.Trim().ToString(), txtOther4.Text.Trim().ToString(), txtOther5.Text.Trim().ToString(), txtMemWelfare.Text.Trim().ToString(), txtServiceChg.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), txtShrSuspence.Text, Txtdiv1.Text, Txtdiv2.Text, Txtdiv3.Text);
                    resultint = SP.ModifyData(ViewState["Id"].ToString(), Session["BRCD"].ToString(), txtShrValue.Text.Trim().ToString(), txtShrFrom.Text.Trim().ToString() == "" ? "0" : txtShrFrom.Text.Trim().ToString(), txtNoOfShr.Text.Trim().ToString(), txtEnterenceFee.Text.Trim().ToString(), txtWelfareFee.Text.Trim().ToString(), txtWelLoanFee.Text.Trim().ToString(), txtShrPrCode.Text.Trim().ToString(), txtEntryFee.Text.Trim().ToString(), txtEntryProdName.Text, txtSavProd.Text.Trim().ToString(), txtOther1.Text.Trim().ToString(), txtWelProdName.Text, txtOther2.Text.Trim().ToString(), txtPrinProdName.Text, txtOther3.Text.Trim().ToString(), txtOtherPrName3.Text, txtOther4.Text.Trim().ToString(), txtOtherPrName4.Text, txtOther5.Text.Trim().ToString(), txtOtherPrName5.Text, txtMemWelfare.Text.Trim().ToString(), txtWelLoanProdName.Text, txtServiceChg.Text.Trim().ToString(), txtServiceProdName.Text, Session["EntryDate"].ToString(), Session["MID"].ToString(), txtShrSuspence.Text, Txtdiv1.Text, Txtdiv2.Text, Txtdiv3.Text);
                    if (resultint > 0)
                    {
                        lblMessage.Text = "Modify Successfully...!!";
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_Para_Mod _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        BindGrid();
                        return;
                    }
                }
            }
            else if (ViewState["Flag"].ToString() == "DL")
            {
                resultint = SP.DeleteData(Session["BRCD"].ToString(), ViewState["Id"].ToString(), Session["MID"].ToString());

                if (resultint > 0)
                {
                    lblMessage.Text = "Delete Successfully...!!";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_Para_Del _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearData();
                    BindGrid();
                    return;
                }
            }
            else if (ViewState["Flag"].ToString() == "AT")
            {
                int Mid = SP.CheckMid(Session["BRCD"].ToString(), ViewState["Id"].ToString());

                if (Mid != Convert.ToInt32(Session["MID"].ToString()))
                {
                    resultint = SP.AuthoriseData(Session["BRCD"].ToString(), ViewState["Id"].ToString(), Session["MID"].ToString());

                    if (resultint > 0)
                    {
                        lblMessage.Text = "Authorized Successfully...!!";
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_Para_Auth _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        BindGrid();
                        return;
                    }
                }
                else
                {
                    lblMessage.Text = "Maker Not Authorise...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Exit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdMaster.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumid = objlink.CommandArgument;

            ViewState["Id"] = strnumid.ToString();
            callData();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void callData()
    {
        try
        {
            if (ViewState["Flag"].ToString() != "AD")
            {
                DataTable DT = new DataTable();
                DT = SP.GetInfo(Session["BRCD"].ToString(), ViewState["Id"].ToString());

                if (DT.Rows.Count > 0)
                {
                    txtShrValue.Text = DT.Rows[0]["SHR_VALUE"].ToString();
                    txtShrFrom.Text = DT.Rows[0]["SHR_FROM"].ToString();
                    txtNoOfShr.Text = DT.Rows[0]["NO_OF_SHARES"].ToString();

                    txtEnterenceFee.Text = DT.Rows[0]["EnterenceAmt"].ToString();
                    txtWelfareFee.Text = DT.Rows[0]["WelFareAmt"].ToString();
                    txtWelLoanFee.Text = DT.Rows[0]["WelFareLoanAmt"].ToString();

                    txtShrPrCode.Text = DT.Rows[0]["SHARES_GL"].ToString();
                    txtShrProdName.Text = DT.Rows[0]["ShrProdName"].ToString();

                    txtSavProd.Text = DT.Rows[0]["SAVING_GL"].ToString();
                    txtSavProdName.Text = DT.Rows[0]["SavProdName"].ToString();

                    txtEntryFee.Text = DT.Rows[0]["ENTRY_GL"].ToString();
                    txtEntryProdName.Text = DT.Rows[0]["EntryProdName"].ToString();

                    txtOther1.Text = DT.Rows[0]["OTHERS_GL1"].ToString();
                    txtWelProdName.Text = DT.Rows[0]["WelProdName"].ToString();

                    txtOther2.Text = DT.Rows[0]["OTHERS_GL2"].ToString();
                    txtPrinProdName.Text = DT.Rows[0]["PrintProdName"].ToString();

                    txtOther3.Text = DT.Rows[0]["OTHERS_GL3"].ToString();
                    txtOtherPrName3.Text = DT.Rows[0]["GlName3"].ToString();

                    txtOther4.Text = DT.Rows[0]["OTHERS_GL4"].ToString();
                    txtOtherPrName4.Text = DT.Rows[0]["GlName4"].ToString();

                    txtOther5.Text = DT.Rows[0]["OTHERS_GL5"].ToString();
                    txtOtherPrName5.Text = DT.Rows[0]["GlName5"].ToString();

                    txtMemWelfare.Text = DT.Rows[0]["MemberWel_GL"].ToString();
                    txtWelLoanProdName.Text = DT.Rows[0]["WelLoanProdName"].ToString();

                    txtServiceChg.Text = DT.Rows[0]["Service_GL"].ToString();
                    txtServiceProdName.Text = DT.Rows[0]["ServiceProdName"].ToString();

                    txtShrSuspence.Text = DT.Rows[0]["BCLASS_GL"].ToString();
                    txtShrSusName.Text = DT.Rows[0]["bclassname"].ToString();

                    Txtdiv1.Text = DT.Rows[0]["div1"].ToString();
                    Txtdiv1name.Text = DT.Rows[0]["div1name"].ToString();

                    Txtdiv2.Text = DT.Rows[0]["div2"].ToString();
                    Txtdiv2name.Text = DT.Rows[0]["div2name"].ToString();

                    Txtdiv3.Text = DT.Rows[0]["div3"].ToString();
                    Txtdiv3name.Text = DT.Rows[0]["div3name"].ToString();

                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtShrPrCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AC1 = SP.Getaccno(txtShrPrCode.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                txtShrProdName.Text = AC[1].ToString();

                txtEntryFee.Focus();
                return;
            }
            else
            {
                txtShrPrCode.Text = "";
                txtShrProdName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtShrPrCode.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtShrProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CUNAME = txtShrProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtShrProdName.Text = custnob[0].ToString();
                txtShrPrCode.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                txtEntryFee.Focus();
                return;
            }
            else
            {
                txtShrPrCode.Text = "";
                txtShrProdName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtShrPrCode.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtEntryFee_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AC1 = SP.Getaccno(txtEntryFee.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                txtEntryProdName.Text = AC[1].ToString();

                txtSavProd.Focus();
                return;
            }
            else
            {
                txtEntryFee.Text = "";
                txtEntryProdName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtEntryFee.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtEntryProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CUNAME = txtEntryProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtEntryProdName.Text = custnob[0].ToString();
                txtEntryFee.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                txtSavProd.Focus();
                return;
            }
            else
            {
                txtEntryFee.Text = "";
                txtEntryProdName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtEntryFee.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtSavProd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AC1 = SP.Getaccno(txtSavProd.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                txtSavProdName.Text = AC[1].ToString();

                txtOther1.Focus();
                return;
            }
            else
            {
                txtSavProd.Text = "";
                txtSavProdName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtSavProd.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtSavProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CUNAME = txtSavProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtSavProdName.Text = custnob[0].ToString();
                txtSavProd.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                txtOther1.Focus();
                return;
            }
            else
            {
                txtSavProd.Text = "";
                txtSavProdName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtSavProd.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtOther1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AC1 = SP.Getaccno(txtOther1.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                txtWelProdName.Text = AC[1].ToString();

                txtOther2.Focus();
                return;
            }
            else
            {
                txtOther1.Text = "";
                txtWelProdName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtOther1.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtWelProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CUNAME = txtWelProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtWelProdName.Text = custnob[0].ToString();
                txtOther1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                txtOther2.Focus();
                return;
            }
            else
            {
                txtOther1.Text = "";
                txtWelProdName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtOther1.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtOther2_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AC1 = SP.Getaccno(txtOther2.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                txtPrinProdName.Text = AC[1].ToString();

                txtServiceChg.Focus();
                return;
            }
            else
            {
                txtOther2.Text = "";
                txtPrinProdName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtOther2.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtPrinProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CUNAME = txtPrinProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtPrinProdName.Text = custnob[0].ToString();
                txtOther2.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                txtServiceChg.Focus();
                return;
            }
            else
            {
                txtOther2.Text = "";
                txtPrinProdName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtOther2.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtServiceChg_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AC1 = SP.Getaccno(txtServiceChg.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                txtServiceProdName.Text = AC[1].ToString();

                txtMemWelfare.Focus();
                return;
            }
            else
            {
                txtServiceChg.Text = "";
                txtServiceProdName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtServiceChg.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtServiceProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CUNAME = txtServiceProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtServiceProdName.Text = custnob[0].ToString();
                txtServiceChg.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                txtMemWelfare.Focus();
                return;
            }
            else
            {
                txtServiceChg.Text = "";
                txtServiceProdName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtServiceChg.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtMemWelfare_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AC1 = SP.Getaccno(txtMemWelfare.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                txtWelLoanProdName.Text = AC[1].ToString();

                txtShrSuspence.Focus();
                return;
            }
            else
            {
                txtMemWelfare.Text = "";
                txtWelLoanProdName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtMemWelfare.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtWelLoanProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CUNAME = txtWelLoanProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtWelLoanProdName.Text = custnob[0].ToString();
                txtMemWelfare.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                txtShrSuspence.Focus();
                return;
            }
            else
            {
                txtMemWelfare.Text = "";
                txtWelLoanProdName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtMemWelfare.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtShrSuspence_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AC1 = SP.Getaccno(txtShrSuspence.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                txtShrSusName.Text = AC[1].ToString();
                txtOther3.Focus();
                return;
            }
            else
            {
                txtShrSuspence.Text = "";
                txtShrSusName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtShrSuspence.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtShrSusName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CUNAME = txtShrSusName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtShrSusName.Text = custnob[0].ToString();
                txtShrSuspence.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                txtOther3.Focus();
                return;
            }
            else
            {
                txtShrSuspence.Text = "";
                txtShrSusName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtShrSuspence.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txtdiv1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AC1 = SP.Getaccno(Txtdiv1.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                Txtdiv1name.Text = AC[1].ToString();
                Txtdiv2.Focus();
                return;
            }
            else
            {
                Txtdiv1.Text = "";
                Txtdiv1name.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                Txtdiv1.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txtdiv1name_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CUNAME = Txtdiv1name.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                Txtdiv1name.Text = custnob[0].ToString();
                Txtdiv1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                Txtdiv2.Focus();
                return;
            }
            else
            {
                Txtdiv1.Text = "";
                Txtdiv1name.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                Txtdiv1.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txtdiv2_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AC1 = SP.Getaccno(Txtdiv2.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                Txtdiv2name.Text = AC[1].ToString();
                Txtdiv3.Focus();
                return;
            }
            else
            {
                Txtdiv2.Text = "";
                Txtdiv2name.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                Txtdiv2.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txtdiv2name_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CUNAME = Txtdiv2name.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                Txtdiv2name.Text = custnob[0].ToString();
                Txtdiv2.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                Txtdiv3.Focus();
                return;
            }
            else
            {
                Txtdiv2.Text = "";
                Txtdiv2name.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                Txtdiv2.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txtdiv3_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AC1 = SP.Getaccno(Txtdiv3.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                Txtdiv3name.Text = AC[1].ToString();

                Submit.Focus();
                return;
            }
            else
            {
                Txtdiv3.Text = "";
                Txtdiv3name.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                Txtdiv3.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txtdiv3name_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CUNAME = Txtdiv3name.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                Txtdiv3name.Text = custnob[0].ToString();
                Txtdiv3.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                Submit.Focus();
                return;
            }
            else
            {
                Txtdiv3.Text = "";
                Txtdiv3name.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                Txtdiv3.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtOtherPrCode3_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AC1 = SP.Getaccno(txtOther3.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                txtOtherPrName3.Text = AC[1].ToString();
                txtOther4.Focus();
                return;
            }
            else
            {
                txtOther3.Text = "";
                txtOtherPrName3.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtOther3.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtOtherPrName3_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CUNAME = txtOtherPrName3.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtOtherPrName3.Text = custnob[0].ToString();
                txtOther3.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                txtOther4.Focus();
                return;
            }
            else
            {
                txtOther3.Text = "";
                txtOtherPrName3.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtOther3.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtOtherPrCode4_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AC1 = SP.Getaccno(txtOther4.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                txtOtherPrName4.Text = AC[1].ToString();
                txtOther5.Focus();
                return;
            }
            else
            {
                txtOther4.Text = "";
                txtOtherPrName4.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtOther4.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtOtherPrName4_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CUNAME = txtOtherPrName4.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtOtherPrName4.Text = custnob[0].ToString();
                txtOther4.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                txtOther5.Focus();
                return;
            }
            else
            {
                txtOther4.Text = "";
                txtOtherPrName4.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtOther4.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtOtherPrCode5_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AC1 = SP.Getaccno(txtOther5.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                txtOtherPrName5.Text = AC[1].ToString();
                Txtdiv1.Focus();
                return;
            }
            else
            {
                txtOther5.Text = "";
                txtOtherPrName5.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtOther5.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtOtherPrName5_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CUNAME = txtOtherPrName5.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtOtherPrName5.Text = custnob[0].ToString();
                txtOther5.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                Txtdiv1.Focus();
                return;
            }
            else
            {
                txtOther5.Text = "";
                txtOtherPrName5.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                txtOther5.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}